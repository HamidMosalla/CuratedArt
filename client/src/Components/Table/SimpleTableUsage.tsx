import { Box } from "@mui/material";
import SimpleTable from "./SimpleTable";
import Columns from "./Columns";
import useFetchUsers from "./api/useFetchUsers";
import { useState } from "react";
import { SortingState } from "@tanstack/react-table";

const SimpleTableUsage = () => {
    const [sorting, setSorting] = useState<SortingState>([]);
    const { data: users, isLoading, isFetching, error } = useFetchUsers({
        sortBy: sorting.map((s) => `${s.id}:${s.desc ? "DESC" : "ASC"}`).join(","),
    });

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
