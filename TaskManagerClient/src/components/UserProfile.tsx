import { useDispatch, useSelector } from "react-redux";
import { AppDispatch, RootState } from "../state/store";
import { useEffect, useState } from "react";
import { getUser } from "../services/userServices";
import { logout } from "../state/user/userSlice";
import { useNavigate } from "react-router-dom";

type User = {
  id: number;
  username: string;
  email: string;
};

const UserProfile = () => {
  const navigate = useNavigate();
  const dispatch = useDispatch<AppDispatch>();
  const [u, setU] = useState<User | null>(null);
  const { user } = useSelector((state: RootState) => state.user);

  useEffect(() => {
    try {
      if (user?.token) {
        getUser(user.token).then((data) => {
          setU(data);
        });
      }
    } catch (error) {
      console.log(error);
    }
  }, [user]);

  return (
    <div>
      {u ? (
        <div>
          <h1>User Profile</h1>
          <p>Username: {u.username}</p>
          <p>Email: {u.email}</p>
          <button
            onClick={() => {
              dispatch(logout());
              navigate("/");
            }}
          >
            Logout
          </button>
        </div>
      ) : (
        <h1>Loading...</h1>
      )}
    </div>
  );
};

export default UserProfile;
