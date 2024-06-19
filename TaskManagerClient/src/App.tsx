// import { useEffect, useState } from "react";
import Login from "./components/Login";
// import { getUser } from "./services/userServices";

const App = () => {
  // const [users, setUsers] = useState([])
  // useEffect(() => {
  //   const fetchUser = async () => {
  //     const users = await getUser();
  //     console.log(users);
  //     setUsers(users);
  //   }
  //   fetchUser();
  // },[])
  // return (
  //   <div>
  //     {users.map((user: {
  //       id: number;
  //       username: string;
  //       email: string;
  //       passwordHash: string;
  //       passwordSalt: string;
  //     }) => {
  //       return (
  //         <div key={user.id}>
  //           <h1>{user.username}</h1>
  //           <p>{user.email}</p>
  //         </div>
  //       )
  //     })}
  //   </div>
  // )
  return (
    <>
      <Login />
    </>
  )
}

export default App;