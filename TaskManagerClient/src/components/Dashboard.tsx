import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { getTaskCount } from "../services/taskServices";
import { RootState } from "../state/store";
import { useSelector } from "react-redux";
import { Container, Row, Col, Button, Card } from "react-bootstrap";

type TaskCount = {
  Completed: number;
  InProgress: number;
  Pending: number;
};

const Dashboard = () => {
  const [taskCount, setTaskCount] = useState<TaskCount>({
    Completed: 0,
    InProgress: 0,
    Pending: 0,
  });
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
    <Container className="mt-4">
      <Row className="mb-3">
        <Col className="text-center">
          <Button onClick={() => navigate("/user-profile")} variant="primary">
            User Profile
          </Button>
        </Col>
      </Row>
      <Row className="mb-3">
        <Col className="text-center">
          <h1>Dashboard</h1>
        </Col>
      </Row>
      <Row>
        {Object.entries(taskCount).map(([key, value]) => (
          <Col key={key} md={4} className="mb-3">
            <Card>
              <Card.Body>
                <Card.Title>{key}</Card.Title>
                <Card.Text>{value}</Card.Text>
              </Card.Body>
            </Card>
          </Col>
        ))}
      </Row>
      <Row className="mt-3">
        <Col className="text-center">
          <Button onClick={() => navigate("/tasklist")} variant="link">
            Click here to see Task list
          </Button>
        </Col>
      </Row>
    </Container>
  );
};

export default Dashboard;
