import ReactDOM from 'react-dom/client'
import { Provider } from 'react-redux'
// import App from './App.tsx'
import {store} from './state/store'
import { createBrowserRouter, RouterProvider } from 'react-router-dom'
import Login from './components/Login.tsx'
import Dashboard from './components/Dashboard.tsx';
import ProtectedRoute from './components/ProtectedRoute.tsx';
import SignUp from './components/SignUp.tsx'

const router = createBrowserRouter([
  {
    path: '/',
    element: (
              <ProtectedRoute>
                <Dashboard />
              </ProtectedRoute>
             ),
  },
  {
    path: '/login',
    element: <Login />,
  },
  {
    path: '/signup',
    element: <SignUp />,
  }
])

ReactDOM.createRoot(document.getElementById('root')!).render(
  <Provider store={store}>
    <RouterProvider router={router} />
  </Provider >
)
