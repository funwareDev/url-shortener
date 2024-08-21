import Navbar from './components/Navbar';
import getUrls from './hooks/postdata';
import About from './pages/About';
import CreateUrl from './pages/CreateUrl';
import Home from './pages/Home';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import Login from './pages/Login';
import { AuthProvider } from './services/AuthProvider';
import UrlDetails from './pages/UrlDetails';

function App() {

  return (
    <Router>
      <AuthProvider>
        <div className="App">
          <Navbar />
          <div className="content">
            <Routes>
              <Route path="/" element={<Home />} />
              <Route path="/create-url" element={<CreateUrl />} />
              <Route path="/login" element={<Login />} />
              <Route path="/about" element={<About />} />
              <Route path="/url/:Identificator" element={<UrlDetails/>} />
            </Routes>
          </div>
        </div>
      </AuthProvider>
    </Router>
  );
}

export default App;