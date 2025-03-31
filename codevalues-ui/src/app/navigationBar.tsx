export type NavigationBarProps = {
    setPageNumber: (updater: (previous: number) => number) => void;
    pageNumber: number;
    totalPages: number;
}

export default function NavigationBar(props: NavigationBarProps) {
    const handlePrevious = () => {
        if (props.pageNumber > 1) {
            props.setPageNumber(previous => previous - 1);
        }
    };

    const handleNext = () => {
        if (props.pageNumber < props.totalPages) {
            props.setPageNumber(previous => previous + 1);
        }
    };

    return (
        <>
            <button onClick={handlePrevious} disabled={props.pageNumber <= 1}>Previous</button>
            <span> Page {props.pageNumber} of {props.totalPages} </span>
            <button onClick={handleNext} disabled={props.pageNumber >= props.totalPages}>Next</button>
        </>
    )
}