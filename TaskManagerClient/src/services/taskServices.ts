import axios from "axios";

const url: string = "http://localhost:5080";

export const getTaskCount = async (token: string) => {
  try {
    const response = await axios.get(`${url}/task/count`, {
      headers: {
        Authorization: "Bearer " + token,
      },
    });
    return response.data;
  } catch (error) {
    console.log(error);
  }
};
