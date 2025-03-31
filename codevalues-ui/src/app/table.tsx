import { CodeValue } from "./codeValueBrowser";

export type TableProps = {
    data: CodeValue[];
}

export default function Table(props: TableProps) {
    return (
        <table>
            <thead>
                <tr>
                    <th>Id</th>
                    <th>Code</th>
                    <th>Value</th>
                </tr>
            </thead>
            <tbody>
                {props.data.map(item => (
                    <tr key={item.id}>
                        <td>{item.id}</td>
                        <td>{item.code}</td>
                        <td>{item.value}</td>
                    </tr>
                ))}
            </tbody>
        </table>
    );
}