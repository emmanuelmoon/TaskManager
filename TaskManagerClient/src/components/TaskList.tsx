import { useSelector } from "react-redux";
import { RootState } from "../state/store";
import { useEffect, useState } from "react";
import { getTask } from "../services/taskServices";
import Table from "react-bootstrap/Table";
import TaskDetail from "./TaskDetail";

type Task = {
  id: number;
  description: string;
  createdAt: string;
  dueDate: string;
  status: string;
};

export const TaskList = () => {
  const { user } = useSelector((state: RootState) => state.user);
  const [tasks, setTasks] = useState<Task[]>([]);
  const [taskDetailId, setTaskDetailId] = useState<number | null>(null);

  useEffect(() => {
    if (user !== null) {
      getTask(user.token).then((response) => {
        setTasks(response);
      });
    }
  }, [user]);

  const [modalShow, setModalShow] = useState(false);
  const handleShow = (id: number) => {
    setTaskDetailId(id);
    setModalShow(true);
  };

  const [currentPage, setCurrentPage] = useState(1);
  const tasksPerPage = 3;
  const indexOfLastTask = currentPage * tasksPerPage;
  const firstIndex = indexOfLastTask - tasksPerPage;
  const tasksToDisplay = tasks.slice(firstIndex, indexOfLastTask);
  const npage = Math.ceil(tasks.length / tasksPerPage);
  const pageNumbers = [...Array(npage + 1).keys()].slice(1);

  function prePage() {
    if (currentPage > 1) {
      if (currentPage !== firstIndex) setCurrentPage(currentPage - 1);
    }
  }
  function nextPage() {
    if (currentPage !== npage) {
      setCurrentPage(currentPage + 1);
    }
  }
  function changePage(id: number) {
    setCurrentPage(id);
  }

  return (
    <div>
      <h1>Task List</h1>
      <Table>
        <thead>
          <tr>
            <th>Description</th>
            <th>Status</th>
          </tr>
        </thead>
        <tbody>
          {tasksToDisplay.map((task: Task) => (
            <tr key={task.id} onClick={() => handleShow(task.id)}>
              <td>{task.description}</td>
              <td>{task.status}</td>
            </tr>
          ))}
        </tbody>
      </Table>
      <nav>
        <ul className="pagination">
          <li className="page-item">
            <a className="page-link" href="#" onClick={prePage}>
              Previous
            </a>
          </li>
          {pageNumbers.map((number) => (
            <li
              key={number}
              className={`page-item
              ${currentPage === number ? "active" : ""}`}
            >
              <a
                onClick={() => changePage(number)}
                href="#"
                className="page-link"
              >
                {number}
              </a>
            </li>
          ))}
          <li className="page-item">
            <a className="page-link" href="#" onClick={nextPage}>
              Next
            </a>
          </li>
        </ul>
      </nav>
      <TaskDetail
        token={user.token}
        show={modalShow}
        onHide={() => setModalShow(false)}
        id={taskDetailId as number}
      />
    </div>
  );
};
