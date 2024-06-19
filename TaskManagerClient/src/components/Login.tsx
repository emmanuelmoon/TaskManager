import { SubmitHandler, useForm } from "react-hook-form";
import { LoginUser } from "../services/userServices";
import { AxiosError } from "axios";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";

const schema = z.object({
  email: z.string().email(),
  password: z.string().min(6, 'Password must have at least 6 characters')
});

type FormFields = z.infer<typeof schema>;

const Login = () => {
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
    try{
      const result = await LoginUser(data.email, data.password);
      console.log(result);
    } catch (error) {
      if (typeof error === "string") {
        error.toUpperCase();
    } else if (error instanceof AxiosError) {
        setError("root", {type: "manual", message: error.response?.data});
    }
    }
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
      {errors.root && <div>{errors.root.message}</div>}
    </form>
  );
};

export default Login;