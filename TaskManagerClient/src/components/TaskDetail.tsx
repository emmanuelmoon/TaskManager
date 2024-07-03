import { useEffect, useState } from "react";
import { getTaskDetail } from "../services/taskServices";
import Modal from "react-bootstrap/esm/Modal";
import { Button } from "react-bootstrap";

type TaskDetailType = {
  Id: number;
  Description: string;
  CreatedAt: string;
  DueDate: string;
  Status: string;
};

function TaskDetail(props) {
  const [taskDetail, setTaskDetail] = useState<TaskDetailType | null>(null);
  useEffect(() => {
    getTaskDetail(props.token, props.id).then((response) => {
      setTaskDetail(response);
      console.log(response);
    });
  }, [props.token, props.id]);

  if (taskDetail) {
    return (
      <Modal {...props} aria-labelledby="contained-modal-title-vcenter">
        <Modal.Header closeButton>
          <Modal.Title id="contained-modal-title-vcenter">
            {taskDetail.Description}
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <p>Created On: {new Date(taskDetail.CreatedAt).toDateString()}</p>
          <p>Due Date: {new Date(taskDetail.DueDate).toDateString()}</p>
          <p>Status: {taskDetail.Status}</p>
        </Modal.Body>
        <Modal.Footer>
          <Button onClick={props.onHide}>Close</Button>
        </Modal.Footer>
      </Modal>
    );
  }
  return null;
}

export default TaskDetail;
