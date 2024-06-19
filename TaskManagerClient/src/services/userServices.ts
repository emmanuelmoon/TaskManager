import axios from "axios";

const url: string = 'http://localhost:5080';

export const getUser = async () => {
  const response = await axios.get(`${url}/account`);
  return response.data;
};

export const LoginUser = async (email: string, password: string) => {
  const response = await axios.post(`${url}/account/login`, {email, password});
  console.log(response.data);
  return response.data;
}