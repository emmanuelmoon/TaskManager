import { useDispatch, useSelector } from "react-redux";
import { AppDispatch, RootState } from "../state/store";
import { useEffect, useState } from "react";
import { getUser } from "../services/userServices";
import { logout } from "../state/user/userSlice";
import { useNavigate } from "react-router-dom";
import { Button, Container, Spinner } from "react-bootstrap";

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
    <Container className="mt-4">
      {u ? (
        <div className="text-center">
          <h1>User Profile</h1>
          <p>
            <strong>Username:</strong> {u.username}
          </p>
          <p>
            <strong>Email:</strong> {u.email}
          </p>
          <Button
            onClick={() => {
              dispatch(logout());
              navigate("/");
            }}
            variant="danger"
          >
            Logout
          </Button>
        </div>
      ) : (
        <div className="text-center">
          <h1>Loading...</h1>
          <Spinner animation="border" role="status">
            <span className="visually-hidden">Loading...</span>
          </Spinner>
        </div>
      )}
    </Container>
  );
};

export default UserProfile;
