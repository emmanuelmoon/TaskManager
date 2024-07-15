import { SubmitHandler, useForm } from "react-hook-form";
import { Link, useNavigate } from "react-router-dom";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";

import { AppDispatch, RootState } from "../state/store";
import { useDispatch, useSelector } from "react-redux";
import { login } from "../state/user/userSlice";
import { useEffect } from "react";
import { Alert, Button, Form } from "react-bootstrap";

const schema = z.object({
  email: z.string().email(),
  password: z.string().min(6, "Password must have at least 6 characters"),
});

type FormFields = z.infer<typeof schema>;

const Login = () => {
  const dispatch = useDispatch<AppDispatch>();
  const { user, error } = useSelector((state: RootState) => state.user);

  const navigate = useNavigate();

  useEffect(() => {
    if (user !== null) {
      navigate("/");
    }
  }, [user, navigate]);

  const {
    register,
    handleSubmit,
    // setError,
    formState: { errors, isSubmitting },
  } = useForm<FormFields>({
    resolver: zodResolver(schema),
  });

  const onSubmit: SubmitHandler<FormFields> = async (data) => {
    await dispatch(login(data));
  };

  return (
    <div className="d-flex justify-content-center">
      <Form onSubmit={handleSubmit(onSubmit)} className="w-50 p-3">
        <Form.Group controlId="formBasicEmail">
          <Form.Label>Email address</Form.Label>
          <Form.Control
            {...register("email")}
            type="email"
            placeholder="Enter email"
            isInvalid={!!errors.email}
          />
          {errors.email && (
            <Form.Control.Feedback type="invalid">
              {errors.email.message}
            </Form.Control.Feedback>
          )}
        </Form.Group>

        <Form.Group controlId="formBasicPassword">
          <Form.Label>Password</Form.Label>
          <Form.Control
            {...register("password")}
            type="password"
            placeholder="Password"
            isInvalid={!!errors.password}
          />
          {errors.password && (
            <Form.Control.Feedback type="invalid">
              {errors.password.message}
            </Form.Control.Feedback>
          )}
        </Form.Group>

        <Button variant="primary" type="submit" disabled={isSubmitting}>
          {isSubmitting ? "Logging in..." : "Login"}
        </Button>

        {error && (
          <Alert variant="danger" className="mt-3">
            {error}
          </Alert>
        )}
      </Form>
      <p className="mt-3">
        Don't have an account? <Link to="/signup">Sign up</Link>
      </p>
    </div>
  );
};

export default Login;
