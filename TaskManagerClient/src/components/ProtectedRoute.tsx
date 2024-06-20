import { useSelector } from 'react-redux';
import { useNavigate } from "react-router-dom";
import { RootState } from "../state/store";
import { PropsWithChildren, useEffect } from 'react';

type ProtectedRouteProps = PropsWithChildren;

const ProtectedRoute = ({ children }: ProtectedRouteProps) => {
  const { user } = useSelector((state: RootState) => {
    return state.user
  });
  const navigate = useNavigate();
  
  useEffect(() => {
    if (user === '') {
      navigate('/login', { replace: true });
    }
  }, [navigate, user]);
  return children;
}

export default ProtectedRoute;