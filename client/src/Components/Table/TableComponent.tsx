import { useEffect, useState } from "react";
import type { NextPage } from "next";
import axios from "axios";
import { Box } from "@mui/material";
import Table from "../components/Table";
import Columns from "./Columns";

const Home: NextPage = () => {
  const [users, setUsers] = useState<Api.Users.Data[] | undefined>(undefined);

  const fetchUsers = async () => {
    const { data } = await axios.get<Api.Users.FetchUsersResponse>(
      "/api/users"
    );

    setUsers(data.data);
  };

  useEffect(() => {
    fetchUsers();
  }, []);

  return (
    <Box padding={6}>{users && <Table data={users} columns={Columns} />}</Box>
  );
};

export default Home;