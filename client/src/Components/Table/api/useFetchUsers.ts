import { useState, useEffect } from 'react';
import axios from 'axios';

export interface SortingInfo {
  columnName: string;
  sortingDirection: string;
}

function useFetchUsers(sortingInfoArray: SortingInfo[] | SortingInfo ) {
  const [data, setData] = useState([]);
  const [maxPageSize, setMaxPageSize] = useState(1);
  const [error, setError] = useState(null);
  const [isLoading, setIsLoading] = useState(true);
  const [isFetching, setIsFetching] = useState(false);

  useEffect(() => {
    async function fetchData() {
      setIsFetching(true);
      try {
        console.log("sortingInfoArray", sortingInfoArray);
        const response = await axios.get(`https://gorest.co.in/public/v2/users?sort=${sortingInfoArray}`);
        setData(response.data);
        const pages = Number(response.headers["x-pagination-pages"]);
        setMaxPageSize(pages === 0 ? 1 : pages);
      } catch (err : any) {
        setError(err);
      } finally {
        setIsFetching(false);
        setIsLoading(false);
      }
    }

    fetchData();
  }, [sortingInfoArray]); // Re-run the effect when sortBy changes

  return { data, maxPageSize, error, isLoading, isFetching };
}

export default useFetchUsers;
