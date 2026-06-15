import logo from './logo.svg';
import './App.css';
import Dashboard from './pages/Dashboard';
import Login from './pages/Login';
import Register from './pages/Register';
import { Route, Routes } from 'react-router-dom';
import ProtectedRoute from './components/ProtectedRoute';
import Header from './pages/Header';
import Footer from './pages/Footer';
import { AuthProvider } from './context/AuthContext';
import ProjectList from './pages/ProjectList';
import TaskList from './pages/TaskList';
import NavBar from './pages/Navbar';

function App() {
  return (
    <>
      <AuthProvider>
        <Header />
        <div className='main-layout'>
     
          <div className='content'>
            <Routes>
              <Route path="/" element={<Login />} />
              <Route path="/login" element={<Login />} />
              <Route path="/register" element={<Register />} />
              <Route path="/dashboard" element={<ProtectedRoute><Dashboard /> </ProtectedRoute>} />
              <Route path="/project/:id" element={<ProtectedRoute><ProjectList /></ProtectedRoute>} />
              <Route path="/task/:projectId" element={<ProtectedRoute><TaskList /></ProtectedRoute>} />
            </Routes>
          </div>
        </div>

        <Footer />
      </AuthProvider>


    </>

  );
}

export default App;
