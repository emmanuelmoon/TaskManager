import { useSelector } from "react-redux";
import { RootState } from "../state/store";
import { useEffect, useState } from "react";
import { getTask } from "../services/taskServices";
import Table from "react-bootstrap/Table";
import TaskDetail from "./TaskDetail";
import { Button } from "react-bootstrap";
import CreateTask from "./CreateTask";

type Task = {
  id: number;
  description: string;
  createdAt: string;
  dueDate: string;
  status: string;
};

export const TaskList = () => {
  const { user } = useSelector((state: RootState) => state.user);
  const [taskDetailId, setTaskDetailId] = useState<number | null>(null);
  const [currentPage, setCurrentPage] = useState(1);
  const [data, setData] = useState([]);
  const [hasNextPage, setHasNextPage] = useState(false);
  const [hasPreviousPage, setHasPreviousPage] = useState(false);
  const [filterText, setFilterText] = useState<string>("");
  const pageSize = 10;

  useEffect(() => {
    if (user !== null) {
      getTask(user.token, currentPage, pageSize, filterText).then(
        (response) => {
          console.log(response);
          setData(response.items);
          setHasNextPage(response.hasNextPage);
          setHasPreviousPage(response.hasPreviousPage);
        }
      );
    }
  }, [currentPage, filterText, user]);

  const [modalShow, setModalShow] = useState(false);
  const handleShow = (id: number) => {
    setTaskDetailId(id);
    setModalShow(true);
  };

  const [show, setShow] = useState<boolean>(false);

  const handleCreateClose = () => setShow(false);
  const handleCreateShow = () => setShow(true);

  const handleNextPage = () => {
    if (hasNextPage) {
      setCurrentPage(currentPage + 1);
    }
  };

  const handlePreviousPage = () => {
    if (hasPreviousPage) {
      setCurrentPage(currentPage - 1);
    }
  };

  const handleFilterTextChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setFilterText(event.target.value.toLowerCase()); // Ensure case-insensitive search
  };

  return (
    <div>
      <h1>Task List</h1>
      <div>
        <form>
          <input
            type="text"
            placeholder="Search tasks..."
            value={filterText}
            onChange={handleFilterTextChange}
          />
        </form>
      </div>
      <Button onClick={handleCreateShow}>Create Task</Button>
      <Table>
        <thead>
          <tr>
            <th>Description</th>
            <th>Status</th>
          </tr>
        </thead>
        <tbody>
          {data.map((task: Task) => (
            <tr key={task.id} onClick={() => handleShow(task.id)}>
              <td>{task.description}</td>
              <td>{task.status}</td>
            </tr>
          ))}
        </tbody>
      </Table>
      <Button onClick={handlePreviousPage} disabled={!hasPreviousPage}>
        Previous Page
      </Button>
      <Button onClick={handleNextPage} disabled={!hasNextPage}>
        Next Page
      </Button>
      <TaskDetail
        token={user?.token}
        show={modalShow}
        onHide={() => setModalShow(false)}
        id={taskDetailId as number}
      />
      <CreateTask
        token={user.token}
        show={show}
        handleClose={handleCreateClose}
      />
    </div>
  );
};
