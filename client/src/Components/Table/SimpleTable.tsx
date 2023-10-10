import { Paper, Table as MuiTable, TableBody, TableCell, TableHead, TableRow } from "@mui/material";
import {
    ColumnDef,
    flexRender,
    getCoreRowModel,
    // getSortedRowModel,
    useReactTable,
    SortingState,
    OnChangeFn
} from "@tanstack/react-table";
import { FC } from "react";
import ImportExportIcon from "@mui/icons-material/ImportExport";
import { pink } from "@mui/material/colors";

interface SimpleTableProps {
    data: any[];
    columns: ColumnDef<any>[];
    isLoading: boolean;
    sorting: SortingState;
    setSorting: OnChangeFn<SortingState>;
}

const SimpleTable: FC<SimpleTableProps> = ({ data, columns, isLoading, sorting, setSorting }) => {
    const { getHeaderGroups, getRowModel } = useReactTable({
        data,
        columns,
        getCoreRowModel: getCoreRowModel(),
        // getSortedRowModel: getSortedRowModel(),
        manualSorting: true,
        state: {
            sorting,
        },
        onSortingChange: setSorting,
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
                                        : flexRender(header.column.columnDef.header, header.getContext())}
                                    {header.column.getCanSort() && (
                                        <ImportExportIcon
                                            onClick={header.column.getToggleSortingHandler()}
                                            sx={{ fontSize: 20, color: pink[500] }}
                                        />
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

export default SimpleTable;
