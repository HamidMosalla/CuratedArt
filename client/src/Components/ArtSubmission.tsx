import { Info } from "@mui/icons-material";
import { TextField, Button, Box, Container } from "@mui/material";
import { FormikProps, withFormik, FormikErrors, Form } from "formik";
import * as yup from "yup";
import MediaCard from "./MediaCard";

interface ArtSubmissionFormValues {
    title: string;
    artistName: string;
    releaseDate: string;
}

interface ArtSubmissionFormProps {
    message?: string;
    intitialTitle: string;
}

interface ArtSubmissionFormOtherProps {
    message?: string;
}

const validationSchema = yup.object({
    title: yup.string().required("Title is required"),
    artistName: yup
        .string()
        .min(3, "ArtistName should be of minimum 3 characters length")
        .required("ArtistName is required"),
});

function ArtSubmission(props: ArtSubmissionFormOtherProps & FormikProps<ArtSubmissionFormValues>) {
    const { touched, errors, isSubmitting, message, handleChange } = props;
    return (
        <Container maxWidth="sm">
            <Box>
                <MediaCard />
            </Box>
            <br />
            <Box>
                <Form>
                    {message && <Info>{message}</Info>}

                    <TextField
                        fullWidth
                        id="title"
                        name="title"
                        label="title"
                        value={props.values.title}
                        onChange={handleChange}
                        error={touched.title && Boolean(errors.title)}
                        helperText={touched.title && errors.title}
                    />

                    <TextField
                        fullWidth
                        id="artistName"
                        name="artistName"
                        label="artistName"
                        value={props.values.artistName}
                        onChange={handleChange}
                        error={touched.artistName && Boolean(errors.artistName)}
                        helperText={touched.artistName && errors.artistName}
                    />

                    <TextField
                        id="releaseDate"
                        label="releaseDate"
                        type="date"
                        value={props.values.releaseDate}
                        sx={{ width: 220 }}
                        InputLabelProps={{
                            shrink: true,
                        }}
                        onChange={handleChange}
                    />
                    <Button color="primary" variant="contained" fullWidth type="submit" disabled={isSubmitting}>
                        Submit
                    </Button>
                </Form>
            </Box>
        </Container>
    );
}

const defaultProps: ArtSubmissionFormOtherProps = {
    message: "",
};

ArtSubmission.defaultProps = defaultProps;

const ArtSubmittionFormWrapper = withFormik<ArtSubmissionFormProps, ArtSubmissionFormValues>({
    // Transform outer props into form values
    mapPropsToValues: (props) => ({
        title: props.intitialTitle || "",
        artistName: "",
        releaseDate: "2023-06-01",
    }),
    validationSchema,
    // Add a custom validation function (this can be async too!)
    validate: (values: ArtSubmissionFormValues) => {
        const errors: FormikErrors<ArtSubmissionFormValues> = {};
        if (values.title === "NoteBook") {
            errors.title = "No ChickFlic here, sorry";
        }
        return errors;
    },
    handleSubmit: (values) => {
        alert(JSON.stringify(values, null, 2));
    },
})(ArtSubmission);

export default ArtSubmittionFormWrapper;
