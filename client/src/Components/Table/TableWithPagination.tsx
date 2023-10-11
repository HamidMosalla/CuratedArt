import {
    Box,
    Paper,
    Skeleton,
    Table as MuiTable,
    TableHead,
    TableRow,
    TableCell,
    TableBody,
    TextField,
    Icon,
    Select,
    MenuItem,
    Grid,
} from "@mui/material";
import {
    Cell,
    ColumnDef,
    flexRender,
    getCoreRowModel,
    getSortedRowModel,
    Row,
    useReactTable,
} from "@tanstack/react-table";
import { debounce } from "lodash";
import { ChangeEvent, FC, memo, useMemo, useState } from "react";
import { StyledPagination, StyledTableRow } from "./styled";
import ImportExportIcon from "@mui/icons-material/ImportExport";
import { pink } from "@mui/material/colors";

interface TableProps {
    data: any[];
    columns: ColumnDef<any>[];
    isFetching?: boolean;
    skeletonCount?: number;
    skeletonHeight?: number;
    headerComponent?: JSX.Element;
    footerComponent?: JSX.Element;
    pageCount?: number;
    page?: (page: number) => void;
    setSearchTerm?: (searchTerm: string) => void;
    setSearchColumn?: {
        value: string;
        set: (searchColumnName: string) => void;
    };
    onClickRow?: (cell: Cell<any, unknown>, row: Row<any>) => void;
    searchLabel?: string;
}

const Table: FC<TableProps> = ({
    data,
    columns,
    isFetching,
    skeletonCount = 10,
    skeletonHeight = 28,
    headerComponent,
    footerComponent,
    pageCount,
    setSearchTerm: setSearchTerm,
    setSearchColumn: setSearchColumn,
    onClickRow,
    page,
    searchLabel = "Search",
}) => {
    const [paginationPage, setPaginationPage] = useState(1);

    const memoizedData = useMemo(() => data, [data]);
    const memoizedColumns = useMemo(() => columns, [columns]);

    const memoisedHeaderComponent = useMemo(() => headerComponent, [headerComponent]);

    const memoisedFooterComponent = useMemo(() => footerComponent, [footerComponent]);

    const { getHeaderGroups, getRowModel, getAllColumns } = useReactTable({
        data: memoizedData,
        columns: memoizedColumns,
        getCoreRowModel: getCoreRowModel(),
        getSortedRowModel: getSortedRowModel(),
        manualPagination: true,
        pageCount,
    });

    const skeletons = Array.from({ length: skeletonCount }, (x, i) => i);

    const columnCount = getAllColumns().length;

    const noDataFound = !isFetching && (!memoizedData || memoizedData.length === 0);

    const handleSearchChange = (e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        setSearchTerm && setSearchTerm(e.target.value);
    };

    const handlePageChange = (event: ChangeEvent<unknown>, currentPage: number) => {
        setPaginationPage(currentPage === 0 ? 1 : currentPage);
        page?.(currentPage === 0 ? 1 : currentPage);
    };

    return (
        <Paper elevation={2} style={{ padding: "1rem 0px" }}>
            <Box paddingX="1rem">
                {memoisedHeaderComponent && <Box>{memoisedHeaderComponent}</Box>}
                {setSearchTerm && setSearchColumn && (
                    <Grid container spacing={2} alignItems="flex-end">
                        {/* Grid item for Select */}
                        <Grid item xs={12} sm={6}>
                            <Select
                                fullWidth
                                value={setSearchColumn.value}
                                onChange={(e) => setSearchColumn.set(e.target.value as string)}
                            >
                                <MenuItem value="name">Name</MenuItem>
                                <MenuItem value="email">Email</MenuItem>
                                <MenuItem value="gender">Gender</MenuItem>
                            </Select>
                        </Grid>

                        {/* Grid item for TextField */}
                        <Grid item xs={12} sm={6}>
                            <TextField
                                fullWidth
                                onChange={debounce(handleSearchChange, 1000)}
                                size="small"
                                label={searchLabel}
                                margin="normal"
                                variant="standard"
                            />
                        </Grid>
                    </Grid>
                )}
            </Box>
            <Box style={{ overflowX: "auto" }}>
                <MuiTable>
                    {!isFetching && (
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
                    )}
                    <TableBody>
                        {!isFetching ? (
                            getRowModel().rows.map((row) => (
                                <StyledTableRow
                                    key={row.id}
                                    selected={row.getIsSelected()}
                                    disabled={row.original.status == "inactive"}
                                >
                                    {row.getVisibleCells().map((cell) => (
                                        <TableCell onClick={() => onClickRow?.(cell, row)} key={cell.id}>
                                            {flexRender(cell.column.columnDef.cell, cell.getContext())}
                                        </TableCell>
                                    ))}
                                </StyledTableRow>
                            ))
                        ) : (
                            <>
                                {skeletons.map((skeleton) => (
                                    <TableRow key={skeleton}>
                                        {Array.from({ length: columnCount }, (x, i) => i).map((elm) => (
                                            <TableCell key={elm}>
                                                <Skeleton height={skeletonHeight} />
                                            </TableCell>
                                        ))}
                                    </TableRow>
                                ))}
                            </>
                        )}
                    </TableBody>
                </MuiTable>
            </Box>
            {noDataFound && (
                <Box my={2} textAlign="center">
                    No Data Found
                </Box>
            )}
            {memoisedFooterComponent && <Box>{memoisedFooterComponent}</Box>}
            {pageCount && page && (
                <StyledPagination count={pageCount} page={paginationPage} onChange={handlePageChange} color="primary" />
            )}
        </Paper>
    );
};

export default memo(Table);
