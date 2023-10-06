import { Alert, Box, Button, Typography } from "@mui/material";
import axios from "axios";
import TableWithPagination from "./TableWithPagination";
import Columns from "./Columns";
import { useQuery } from "@tanstack/react-query";
import { useState } from "react";

const TableWithPaginationUsage = () => {
  const [currentPage, setCurrentPage] = useState<number | undefined>(1);
  const [search, setSearch] = useState<string | undefined>("");

  const fetchUsers = async () => {
    const params = {
      ...(!search && { page: currentPage }),
      ...(search && { name: search }),
    };
  
    try {
      const { data, headers } = await axios.get<Api.Users.Data[]>(
        "https://gorest.co.in/public/v2/users",
        {
          params,
        }
      );
      const maxPageSize = 
        Number(headers["x-pagination-pages"]) === 0
          ? 1
          : Number(headers["x-pagination-pages"]);
  
      return {
        data,
        maxPageSize,
      };
    } catch (error) {
      throw new Error("Failed to fetch users.");
    }
  };

  const { data, isFetching, isError, error, isSuccess } = useQuery<
    Api.Users.FetchUsersResponse,
    Error
  >(["users", currentPage, search], fetchUsers, {
    refetchOnWindowFocus: false,
    keepPreviousData: true,
  });

  const onClickRow = (cell: any, row: any) => {
    console.log({ cell, row });
  };

  const Header = (
    <Box display="flex" justifyContent="space-between">
      <Typography variant="h4" alignItems="center">
        User Table
      </Typography>
      <Button>Action Button</Button>
    </Box>
  );

  return (
    <Box padding={6}>
      {isError && <Alert severity="error">{error?.message}</Alert>}
      {isSuccess && (
        <TableWithPagination
          data={data.data}
          columns={Columns}
          isFetching={isFetching}
          headerComponent={Header}
          onClickRow={onClickRow}
          pageCount={data.maxPageSize}
          page={setCurrentPage}
          search={setSearch}
          searchLabel="Search by name"
        />
      )}
    </Box>
  );
};

export default TableWithPaginationUsage;
