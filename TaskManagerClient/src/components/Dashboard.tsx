import { useNavigate } from "react-router-dom";

const Dashboard = () => {
  const navigate = useNavigate();
  return (
    <div>
      <button onClick={() => navigate("/user-profile")}>User Profile</button>
      <h1>Dashboard</h1>
    </div>
  );
};

export default Dashboard;
