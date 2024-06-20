import { SubmitHandler, useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";

import { AppDispatch, RootState } from "../state/store";
import { useDispatch, useSelector } from "react-redux"; 
import { login } from "../state/user/userSlice";
import { useEffect } from "react";

const schema = z.object({
  email: z.string().email(),
  password: z.string().min(6, 'Password must have at least 6 characters')
});

type FormFields = z.infer<typeof schema>;

const Login = () => {
  const dispatch = useDispatch<AppDispatch>();
  const { user, error } = useSelector((state: RootState) => state.user);

  const navigate = useNavigate();

  useEffect(() => {
    if (user !== '') {
      navigate('/');
    }
  }, [user, navigate]);

  const {
      register,
      handleSubmit,
      // setError,
      formState: {errors, isSubmitting}
    } = useForm<FormFields>(
      {
        resolver: zodResolver(schema),
      },
    );

  const onSubmit: SubmitHandler<FormFields> = async(data) => {
      await dispatch(login(data));
  }

  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      <input {...register("email")}type="email" placeholder="Email" />
      {errors.email && <div>{errors.email.message}</div>}
      <input {...register("password")}type="password" placeholder="Password" />
      {errors.password && <div>{errors.password.message}</div>}
      <button disabled={isSubmitting} type="submit">
        {isSubmitting ? "Logging in..." : "Login"}
      </button>
      {error && <div>{error}</div>}
      {/* {errors.root && <div>{errors.root.message}</div>} */}
    </form>
  );
};

export default Login;