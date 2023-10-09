import { Pagination, styled, TableRow } from "@mui/material";

export const StyledTableRow = styled(TableRow)`
  &:nth-of-type(odd) {
    background-color: #f1f1f1;
  }
  
  &:last-child td,
  &:last-child th {
    border: 0;
  }
  
  :hover {
    background-color: #d9d9d9;
  }

  /* Apply background color if the 'selected' prop is true */
  ${props => props.selected && `
    background-color: pink !important;
  `}

  /* Apply styles for the 'disabled' prop */
  ${props => props.disabled && `
    background-color: #b2b2b2 !important; /* gray color for disabled rows */
    user-select: none !important; /* prevent text selection */
    pointer-events: none !important; /* prevent all interactions */
  `}
`;

export const StyledPagination = styled(Pagination)`
  display: flex;
  justify-content: center;
  margin-top: 1rem;
`;
