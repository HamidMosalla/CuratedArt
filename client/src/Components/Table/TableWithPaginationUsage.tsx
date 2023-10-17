import { Alert, Box, Button, Typography } from "@mui/material";
import TableWithPagination from "./TableWithPagination";
import Columns from "./Columns";
import { useQuery } from "@tanstack/react-query";
import { useState, useMemo } from "react";
import { SortingState } from "@tanstack/react-table";
import fetchUsers from "./api/fetchUsers";

const TableWithPaginationUsage = () => {
    const [currentPage, setCurrentPage] = useState<number | undefined>(1);
    const [searchTerm, setSearchTerm] = useState<string | undefined>("");
    const [searchColumn, setSearchColumn] = useState<string>("name");
    const memoizedSetSearchColumn = useMemo(() => ({ value: searchColumn, set: setSearchColumn }), [searchColumn, setSearchColumn]);

    const [sorting, setSorting] = useState<SortingState>([]);
    
    const sortingInfo = useMemo(() => {
        const sortedInfo = sorting.map((s) => ({
          columnName: s.id,
          sortingDirection: s.desc ? "DESC" : "ASC"
        }));
    
        // Convert the sorting info to a string representation
        return JSON.stringify(sortedInfo);
    }, [sorting]);

    const fetchUsersWithParameters = () => fetchUsers(sortingInfo, searchTerm, searchColumn, currentPage);

    const { data, isFetching, isError, error, isSuccess } = useQuery<Api.Users.FetchUsersResponse, Error>(
        ["users", sortingInfo, searchTerm, searchColumn, currentPage],
        fetchUsersWithParameters,
        {
            refetchOnWindowFocus: false,
            keepPreviousData: true,
        }
    );

    const onClickRow = (cell: any, row: any) => {
        console.log({ cell, row });
    };

    const CustomHeader = (
        <Box display="flex" justifyContent="space-between">
            <Typography variant="h4" alignItems="center">
                Custom Header
                <ul>
                    <li>User Info: User 1</li>
                    <li>User Age: 120</li>
                    <li>User Job: Ruler</li>
                </ul>
            </Typography>
            <Button>Action Button</Button>
        </Box>
    );

    const CustomFooter = (
        <>
            <Box display="flex" justifyContent="flex-end">
                <Typography variant="h4" alignItems="center">
                    Custom Footer
                </Typography>
            </Box>
            <br />
            <Box display="flex" justifyContent="flex-end">
                <Typography variant="h4" alignItems="center">
                    Sum Valus
                </Typography>
            </Box>
        </>
    );

    return (
        <Box padding={6}>
            {isError && <Alert severity="error">{error?.message}</Alert>}
            {isSuccess && (
                <TableWithPagination
                    data={data.data}
                    columns={Columns}
                    isFetching={isFetching}
                    headerComponent={CustomHeader}
                    footerComponent={CustomFooter}
                    onClickRow={onClickRow}
                    pageCount={data.maxPageSize}
                    page={setCurrentPage}
                    setSearchTerm={setSearchTerm}
                    setSearchColumn={memoizedSetSearchColumn}
                    searchLabel="Search by selected column"
                    sorting={sorting}
                    setSorting={setSorting}
                />
            )}
        </Box>
    );
};

export default TableWithPaginationUsage;
