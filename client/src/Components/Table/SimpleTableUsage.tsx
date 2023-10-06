import { Box } from "@mui/material";
import SimpleTable from "./SimpleTable";
import Columns from "./Columns";
import useFetchUsers from './api/useFetchUsers';

const SimpleTableUsage = () => {
  const { data: users, error } = useFetchUsers();

  // Optional: Handle the error if you'd like.
  if (error) {
    console.error("There was an error fetching users:", error);
    // Render error UI or handle as desired.
  }

  return (
    <Box padding={6}>{users && <SimpleTable data={users} columns={Columns} />}</Box>
  );
};

export default SimpleTableUsage;
