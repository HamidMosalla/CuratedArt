import { useState, useEffect } from 'react';
import axios from 'axios';

function useFetchUsers() {
  const [data, setData] = useState([]);
  const [maxPageSize, setMaxPageSize] = useState(1);
  const [error, setError] = useState(null);

  useEffect(() => {
    async function fetchData() {
      try {
        const response = await axios.get('https://gorest.co.in/public/v2/users');
        setData(response.data);
        const pages = Number(response.headers["x-pagination-pages"]);
        setMaxPageSize(pages === 0 ? 1 : pages);
      } catch (err : any) {
        setError(err);
      }
    }

    fetchData();
  }, []);

  return { data, maxPageSize, error };
}

export default useFetchUsers;
