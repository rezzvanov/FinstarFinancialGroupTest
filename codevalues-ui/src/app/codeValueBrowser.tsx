'use client'
import { useEffect, useState } from "react";
import FiltersPanel from "./filtersPanel";
import NavigationBar from "./navigationBar";
import Table from "./table";

type ResponseObject = {
    count: number;
    data: CodeValue[];
}

export type CodeValue = {
    id: number;
    code: number;
    value: string;
}

type Filters = {
    Code?: number;
    ValuePrefix?: string;
}

async function getResponse(filters: Filters, pageNumber: number, pageSize: number): Promise<ResponseObject> {
    const params = new URLSearchParams();

    if (filters.Code) {
        params.append('Code', filters.Code.toString());
    }
    if (filters.ValuePrefix) {
        params.append('ValuePrefix', filters.ValuePrefix);
    }

    params.append('PageNumber', pageNumber.toString());
    params.append('PageSize', pageSize.toString());

    const response = await fetch(`https://localhost/api/CodeValues?${params.toString()}`);

    if (!response.ok) {
        throw new Error(`API request failed: ${response.status}`);
    }

    return response.json();
}

export default function CodeValueBrowser() {
    const [pageNumber, setPageNumber] = useState(1);
    const [pageSize] = useState(5);
    const [data, setData] = useState<CodeValue[]>([]);
    const [totalCount, setTotalCount] = useState(0);
    const [filters, setFilters] = useState<Filters>({});

    const totalPages = Math.ceil(totalCount / pageSize);

    useEffect(() => {
        const loadData = async () => {
            try {
                const response = await getResponse(filters, pageNumber, pageSize);
                setData(response.data);
                setTotalCount(response.count);
            } catch (error) {
                console.error('Failed fetching data:', error);
            }
        };

        loadData();
    }, [pageNumber, filters]);

    const handleFilter = (newFilters: Omit<CodeValue, 'id'>) => {
        setFilters({
            Code: newFilters.code || undefined,
            ValuePrefix: newFilters.value || undefined
        });
        setPageNumber(1);
    };

    return (
        <>
            <FiltersPanel onFilter={handleFilter}></FiltersPanel>
            <Table data={data}></Table>
            <NavigationBar
                setPageNumber={setPageNumber}
                pageNumber={pageNumber}
                totalPages={totalPages}
            ></NavigationBar>
        </>
    )
}