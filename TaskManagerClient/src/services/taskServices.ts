import axios from "axios";

const url: string = "http://localhost:5080/task";

export const getTaskCount = async (token: string) => {
  try {
    const response = await axios.get(`${url}/count`, {
      headers: {
        Authorization: "Bearer " + token,
      },
    });
    return response.data;
  } catch (error) {
    console.log(error);
  }
};

export const getTask = async (token: string) => {
  try {
    const response = await axios.get(`${url}/tasks`, {
      headers: {
        Authorization: "Bearer " + token,
      },
    });
    return response.data;
  } catch (error) {
    console.log(error);
  }
};
