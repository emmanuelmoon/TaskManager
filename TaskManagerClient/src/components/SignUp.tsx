import { SubmitHandler, useForm } from "react-hook-form";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { RegisterUser } from "../services/userServices";
import { AxiosError } from "axios";
import { useNavigate } from "react-router-dom";

const schema = z.object({
  username: z.string().min(3, 'Username must have at least 3 characters'),
  email: z.string().email(),
  password: z.string().min(6, 'Password must have at least 6 characters')
});

type FormFields = z.infer<typeof schema>;

const Login = () => {
  const navigate = useNavigate();

  const {
      register,
      handleSubmit,
      setError,
      formState: {errors, isSubmitting}
    } = useForm<FormFields>(
      {
        resolver: zodResolver(schema),
      },
    );

  const onSubmit: SubmitHandler<FormFields> = async(data) => {
    try {
      await RegisterUser(data.username, data.email, data.password);
      navigate('/login')
    } catch (error){
      setError('root', {
        type: 'manual',
        message: (error as AxiosError).response?.data as string
      });
    }
  }

  return (
    <form onSubmit={handleSubmit(onSubmit)}>
      <input {...register("email")}type="email" placeholder="Email" />
      {errors.email && <div>{errors.email.message}</div>}
      <input {...register("username")}type="text" placeholder="Username" />
      {errors.username && <div>{errors.username.message}</div>}
      <input {...register("password")}type="password" placeholder="Password" />
      {errors.password && <div>{errors.password.message}</div>}
      <button disabled={isSubmitting} type="submit">
        {isSubmitting ? "Registering..." : "Login"}
      </button>
      {errors.root && <div>{errors.root.message}</div>}
    </form>
  );
};

export default Login;