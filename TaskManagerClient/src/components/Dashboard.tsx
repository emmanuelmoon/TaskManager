import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { getTaskCount } from "../services/taskServices";
import { RootState } from "../state/store";
import { useSelector } from "react-redux";

const Dashboard = () => {
  const [taskCount, setTaskCount] = useState<number>(0);
  const { user } = useSelector((state: RootState) => state.user);
  useEffect(() => {
    try {
      if (user) {
        getTaskCount(user.token).then((response) => {
          setTaskCount(response);
        });
      }
    } catch (error) {
      console.log(error);
    }
  });
  const navigate = useNavigate();

  return (
    <div>
      <button onClick={() => navigate("/user-profile")}>User Profile</button>
      <h1>Dashboard</h1>
      <p>{taskCount}</p>
    </div>
  );
};

export default Dashboard;
