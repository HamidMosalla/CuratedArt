// import React, { useCallback, useEffect, useRef, useState } from 'react';
// import { Paper, Select, MenuItem, Box, TextField, Grid, Skeleton, MuiTable } from "@mui/material";

// // ... other imports ...

// interface CustomRendererProps {
//     row: Row<any>;
//     cell: Cell<any, unknown>;
// }

// interface TableProps {
//     // ... other props ...

//     // Slots for custom renderers
//     customCellRenderer?: (props: CustomRendererProps) => JSX.Element;
// }

// const ErrorBoundary: React.FC = ({ children }) => {
//     const [hasError, setHasError] = useState(false);

//     useEffect(() => {
//         if (hasError) {
//             // Log the error to an error reporting service, if required
//         }
//     }, [hasError]);

//     if (hasError) {
//         return <div>Something went wrong. Please try refreshing the page or contact support.</div>;
//     }

//     return children;
// };

// const Table: FC<TableProps> = ({ 
//     columns, 
//     customCellRenderer,
//     // ... other props
// }) => {
//     // ... other code ...

//     const derivedSearchOptions = useMemo(() => columns.map(col => col.accessor), [columns]);

//     const rowRef = useRef<HTMLTableRowElement | null>(null);

//     const handleKeyDown = useCallback((event: React.KeyboardEvent<HTMLTableRowElement>) => {
//         if (event.key === "Enter" && rowRef.current) {
//             rowRef.current.click();
//         }
//     }, []);

//     return (
//         <ErrorBoundary>
//             <Paper elevation={2} style={{ padding: "1rem 0px" }}>
//                 <Box paddingX="1rem">
//                     {headerComponent && <Box>{headerComponent}</Box>}
//                     {setSearchTerm && (
//                         <Grid container spacing={2} alignItems="flex-end">
//                             <Grid item xs={12} sm={6}>
//                                 <Select
//                                     fullWidth
//                                     value={searchColumn}
//                                     onChange={(e) => setSearchColumn(e.target.value)}
//                                 >
//                                     {derivedSearchOptions.map(option => (
//                                         <MenuItem key={option} value={option}>
//                                             {option}
//                                         </MenuItem>
//                                     ))}
//                                 </Select>
//                             </Grid>

//                             {/* ... other code ... */}
//                         </Grid>
//                     )}
//                 </Box>

//                 {/* ... other code ... */}

//                 <TableBody>
//                     {!isFetching ? (
//                         getRowModel().rows.map((row) => (
//                             <StyledTableRow
//                                 ref={rowRef}
//                                 key={row.id}
//                                 onKeyDown={handleKeyDown}
//                                 tabIndex={0}
//                                 // ... other props ...
//                             >
//                                 {row.getVisibleCells().map((cell) => (
//                                     <TableCell onClick={() => onClickRow?.(cell, row)} key={cell.id}>
//                                         {customCellRenderer ? customCellRenderer({ row, cell }) : flexRender(cell.column.columnDef.cell, cell.getContext())}
//                                     </TableCell>
//                                 ))}
//                             </StyledTableRow>
//                         ))
//                     ) : (
//                         // ... other code ...
//                     )}
//                 </TableBody>
//             </Paper>
//         </ErrorBoundary>
//     );
// };

// export default memo(Table);


















// import React from 'react';
// import { useTable } from 'react-table';

// function TableWrapper({ columns, data, renderRowSubComponent }) {
//   const {
//     getTableProps,
//     getTableBodyProps,
//     headerGroups,
//     rows,
//     prepareRow,
//   } = useTable({ columns, data });

//   return (
//     <table {...getTableProps()}>
//       <thead>
//         {headerGroups.map(headerGroup => (
//           <tr {...headerGroup.getHeaderGroupProps()}>
//             {headerGroup.headers.map(column => (
//               <th {...column.getHeaderProps()}>{column.render('Header')}</th>
//             ))}
//           </tr>
//         ))}
//       </thead>
//       <tbody {...getTableBodyProps()}>
//         {rows.map(row => {
//           prepareRow(row);
//           return (
//             <React.Fragment key={row.id}>
//               <tr {...row.getRowProps()}>
//                 {row.cells.map(cell => (
//                   <td {...cell.getCellProps()}>{cell.render('Cell')}</td>
//                 ))}
//               </tr>
//               {renderRowSubComponent && (
//                 <tr>
//                   <td colSpan={columns.length}>
//                     {renderRowSubComponent({ row })}
//                   </td>
//                 </tr>
//               )}
//             </React.Fragment>
//           );
//         })}
//       </tbody>
//     </table>
//   );
// }

// export default TableWrapper;
