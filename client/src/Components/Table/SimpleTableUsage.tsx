import { Box } from "@mui/material";
import SimpleTable from "./SimpleTable";
import Columns from "./Columns";
import useFetchUsers, { SortingInfo } from "./api/useFetchUsers";
import { useState, useMemo } from "react";
import { SortingState } from "@tanstack/react-table";

const SimpleTableUsage = () => {
    const [sorting, setSorting] = useState<SortingState>([]);

    const sortedInfo = sorting.map(
        (s) =>
            ({
                columnName: s.id,
                sortingDirection: s.desc ? "DESC" : "ASC",
            }) as SortingInfo
    );

    const sortByParam = useMemo(() => {
        return sortedInfo.length === 1 ? sortedInfo[0] : sortedInfo;
    }, [sortedInfo]);

    const { data: users, isLoading, isFetching, error } = useFetchUsers(sortByParam);

    // Optional: Handle the error if you'd like.
    if (error) {
        console.error("There was an error fetching users:", error);
        // Render error UI or handle as desired.
    }

    return (
        <Box padding={6}>
            {users && (
                <SimpleTable
                    data={users}
                    columns={Columns}
                    isLoading={isLoading || isFetching}
                    sorting={sorting}
                    setSorting={setSorting}
                />
            )}
        </Box>
    );
};

export default SimpleTableUsage;
