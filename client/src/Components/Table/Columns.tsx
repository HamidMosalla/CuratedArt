import { Chip, Checkbox } from "@mui/material";
import { ColumnDef } from "@tanstack/react-table";
import CustomizedMenus from "../MUI/CustomizedMenus";

const Columns: ColumnDef<any, any>[] = [
    {
        id: "select",
        accessorKey: "select",
        header: ({ table }) => {
            return (
                <Checkbox
                    checked={table.getIsAllPageRowsSelected()}
                    onChange={(value) => {
                        table.toggleAllPageRowsSelected(value.target.checked);
                    }}
                    size="large"
                />
            );
        },
        cell: ({ row }) => {
            return (
                <Checkbox
                    checked={row.getIsSelected()}
                    onChange={(value) => {
                        row.toggleSelected(value.target.checked);
                    }}
                    size="large"
                />
            );
        },
        enableSorting: false,
    },
    {
        accessorKey: "name",
        header: "Name",
    },
    {
        accessorKey: "email",
        header: "Email",
    },
    {
        accessorKey: "gender",
        header: "Gender",
    },
    {
        accessorKey: "status",
        header: "Status",
        cell: (row: any) => {
            return (
                <Chip label={row.getValue()} size="small" color={row.getValue() === "active" ? "primary" : "default"} />
            );
        },
    },
    {
        accessorKey: "actions",
        header: "Actions",
        enableSorting: false,
        cell: (row: any) => {
            return <CustomizedMenus />;
        },
    },
];

export default Columns;
