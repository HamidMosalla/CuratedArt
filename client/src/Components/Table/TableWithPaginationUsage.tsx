import { Alert, Box, Button, Typography } from "@mui/material";
import axios from "axios";
import TableWithPagination from "./TableWithPagination";
import Columns from "./Columns";
import { useQuery } from "@tanstack/react-query";
import { useState, useMemo } from "react";

const TableWithPaginationUsage = () => {
    const [currentPage, setCurrentPage] = useState<number | undefined>(1);
    const [searchTerm, setSearchTerm] = useState<string | undefined>("");
    const [searchColumn, setSearchColumn] = useState<string>("name");
    const memoizedSetSearchColumn = useMemo(() => ({ value: searchColumn, set: setSearchColumn }), [searchColumn, setSearchColumn]);


    const fetchUsers = async () => {
        const params = {
            ...(!searchTerm && { page: currentPage }),
            ...(searchTerm && { [searchColumn]: searchTerm }),
        };

        try {
            const { data, headers } = await axios.get<Api.Users.Data[]>("https://gorest.co.in/public/v2/users", {
                params,
            });
            const maxPageSize = Number(headers["x-pagination-pages"]) === 0 ? 1 : Number(headers["x-pagination-pages"]);

            return {
                data,
                maxPageSize,
            };
        } catch (error) {
            throw new Error("Failed to fetch users.");
        }
    };

    const { data, isFetching, isError, error, isSuccess } = useQuery<Api.Users.FetchUsersResponse, Error>(
        ["users", currentPage, searchTerm],
        fetchUsers,
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
                />
            )}
        </Box>
    );
};

export default TableWithPaginationUsage;
