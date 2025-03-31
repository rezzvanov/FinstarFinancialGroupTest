import { useState } from 'react';
import { CodeValue } from './codeValueBrowser';

type FiltersPanelProps = {
    onFilter: (filters: Omit<CodeValue, 'id'>) => void;
};

export default function FiltersPanel(props: FiltersPanelProps) {
    const [code, setCode] = useState<string>('');
    const [value, setValue] = useState<string>('');

    const handleFilter = () => {
        props.onFilter({
            code: code ? parseInt(code) : 0,
            value: value
        });
    };

    return (
        <>
        <div>
            <div>
                <label>Code:
                    <input
                        type="number"
                        value={code}
                        onChange={(e) => setCode(e.target.value)}
                    />
                </label>
            </div>
            <div>
                <label>Value:
                    <input
                        type="text"
                        value={value}
                        onChange={(e) => setValue(e.target.value)}
                    />
                </label>
            </div>
            <button onClick={handleFilter}>Apply Filters</button>
        </div>
        </>
    );
}