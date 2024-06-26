import { useSelector } from "react-redux";
import { RootState } from "../state/store";
import { useEffect, useState } from "react";
import { getTask } from "../services/taskServices";

type Task = {
  id: number;
  description: string;
  createdat: string;
  duedate: string;
  status: string;
};

export const TaskList = () => {
  const { user } = useSelector((state: RootState) => state.user);
  const [tasks, setTasks] = useState<Task[]>([]);
  useEffect(() => {
    if (user) {
      getTask(user.token).then((response) => {
        setTasks(response);
      });
    }
  }, [user]);

  return (
    <div>
      <h1>Task List</h1>
      <ul>
        {tasks.map((task: Task) => (
          <li key={task.id}>{task.description}</li>
        ))}
      </ul>
    </div>
  );
};
