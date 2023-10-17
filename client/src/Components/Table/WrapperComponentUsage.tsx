// import React from 'react';
// import { Alert, Box, Typography, Button } from "@mui/material";
// import Table from './Table';
// import Columns from './Columns';  // Assuming this contains column definitions

// const CustomCellRenderer = ({ row, cell }: CustomRendererProps) => {
//     if (cell.column.columnDef.accessor === 'profilePicture') {
//         return <img src={cell.value} alt="Profile" width={50} />;
//     }

//     // Default rendering for other cells
//     return <>{cell.render('Cell')}</>;
// };

// const UserTable = () => {
//     const data = [
//         // Your list of user data here, for example:
//         // { id: 1, name: "John Doe", email: "john@example.com", profilePicture: "path_to_img.jpg", ... },
//         // ...
//     ];

//     const onClickRow = (cell: any, row: any) => {
//         console.log('Row clicked:', row);
//     };

//     return (
//         <Box padding={6}>
//             <Table
//                 data={data}
//                 columns={Columns}
//                 customCellRenderer={CustomCellRenderer}
//                 onClickRow={onClickRow}
//                 // Add other props like `isFetching`, `headerComponent`, etc. as needed
//             />
//         </Box>
//     );
// };

// export default UserTable;

















// import React from 'react';
// import TableWrapper from './TableWrapper';

// const App = () => {
//   const columns = React.useMemo(
//     () => [
//       {
//         Header: 'Name',
//         accessor: 'name',
//       },
//       {
//         Header: 'Age',
//         accessor: 'age',
//       },
//     ],
//     []
//   );

//   const data = React.useMemo(
//     () => [
//       { name: 'John', age: 25 },
//       { name: 'Doe', age: 30 },
//     ],
//     []
//   );

//   const renderRowSubComponent = React.useCallback(
//     ({ row }) => (
//       <div>
//         <strong>Additional Info for:</strong> {row.original.name}
//       </div>
//     ),
//     []
//   );

//   return (
//     <div>
//       <TableWrapper
//         columns={columns}
//         data={data}
//         renderRowSubComponent={renderRowSubComponent}
//       />
//     </div>
//   );
// };

// export default App;

