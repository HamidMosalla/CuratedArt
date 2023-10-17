// import { Box } from "@mui/material";
// import SimpleTable from "./TableWithServerSorting";
// import Columns from "../Columns";
// import useFetchUsers from "./useFetchUsers";
// import { useState, useMemo } from "react";
// import { SortingState } from "@tanstack/react-table";

// const TableWithServerSortingUsage = () => {
//     const [sorting, setSorting] = useState<SortingState>([]);

//     const sortingInfo = useMemo(() => {
//       const sortedInfo = sorting.map((s) => ({
//         columnName: s.id,
//         sortingDirection: s.desc ? "DESC" : "ASC"
//       }));
  
//       // Convert the sorting info to a string representation
//       return JSON.stringify(sortedInfo);
//   }, [sorting]);

//     const { data: users, isLoading, isFetching, error } = useFetchUsers(sortingInfo);

//     // Optional: Handle the error if you'd like.
//     if (error) {
//         console.error("There was an error fetching users:", error);
//         // Render error UI or handle as desired.
//     }

//     return (
//         <Box padding={6}>
//             {users && (
//                 <SimpleTable
//                     data={users}
//                     columns={Columns}
//                     isLoading={isLoading || isFetching}
//                     sorting={sorting}
//                     setSorting={setSorting}
//                 />
//             )}
//         </Box>
//     );
// };

// export default TableWithServerSortingUsage;
