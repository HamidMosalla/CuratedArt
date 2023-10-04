import {
    Paper,
    Table as MuiTable,
    TableBody,
    TableCell,
    TableHead,
    TableRow,
  } from "@mui/material";
  import {
    ColumnDef,
    flexRender,
    getCoreRowModel,
    useReactTable,
  } from "@tanstack/react-table";
  import { FC } from "react";
  
  interface TableProps {
    data: any[];
    columns: ColumnDef<any>[];
  }
  
  export const Table: FC<TableProps> = ({ data, columns }) => {
    const { getHeaderGroups, getRowModel } = useReactTable({
      data,
      columns,
      getCoreRowModel: getCoreRowModel(),
    });
  
    return (
      <Paper elevation={2} style={{ padding: "1rem 0px" }}>
        <MuiTable>
          <TableHead>
            {getHeaderGroups().map((headerGroup) => (
              <TableRow key={headerGroup.id}>
                {headerGroup.headers.map((header) => (
                  <TableCell key={header.id}>
                    {header.isPlaceholder
                      ? null
                      : flexRender(
                          header.column.columnDef.header,
                          header.getContext()
                        )}
                  </TableCell>
                ))}
              </TableRow>
            ))}
          </TableHead>
          <TableBody>
            {getRowModel().rows.map((row) => (
              <TableRow key={row.id}>
                {row.getVisibleCells().map((cell) => (
                  <TableCell key={cell.id}>
                    {flexRender(cell.column.columnDef.cell, cell.getContext())}
                  </TableCell>
                ))}
              </TableRow>
            ))}
          </TableBody>
        </MuiTable>
      </Paper>
    );
  };