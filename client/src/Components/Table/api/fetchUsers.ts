import axios from "axios";

export interface SortingInfo {
    columnName: string;
    sortingDirection: string;
}

async function fetchUsers(
    sortingInfo = "",
    searchTerm: string | undefined,
    searchColumn: string,
    currentPage: number | undefined
) {
    const params = {
        ...(!searchTerm && { page: currentPage }),
        ...(searchTerm && { [searchColumn]: searchTerm }),
    };

    console.log("Sorting Info: ", sortingInfo);
    const { data, headers } = await axios.get<Api.Users.Data[]>("https://gorest.co.in/public/v2/users", {
        params,
    });
    const maxPageSize = Number(headers["x-pagination-pages"]) === 0 ? 1 : Number(headers["x-pagination-pages"]);
    return { data, maxPageSize };
}

export default fetchUsers;
