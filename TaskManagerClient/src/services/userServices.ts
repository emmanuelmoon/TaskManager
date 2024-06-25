import axios from "axios";

const url: string = "http://localhost:5080";

export const getUser = async (token: string) => {
  try {
    const response = await axios.get(`${url}/account/getuserinfo`, {
      headers: {
        Authorization: "Bearer " + token,
      },
    });
    return response.data;
  } catch (error) {
    throw Error("Unsuccessful");
  }
};

export const LoginUser = async (email: string, password: string) => {
  const response = await axios.post(`${url}/account/login`, {
    email,
    password,
  });
  return response.data;
};

export const RegisterUser = async (
  username: string,
  email: string,
  password: string
) => {
  const response = await axios.post(`${url}/account/register`, {
    username,
    email,
    password,
  });
  return response.data;
};
