import { useEffect, useState } from 'react';
import './App.css';
import { Button, Form } from 'react-bootstrap';
import { LocalizationProvider } from '@mui/x-date-pickers';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs'
import { DateTimePicker } from '@mui/x-date-pickers/DateTimePicker';
import dayjs from 'dayjs';

function App() {
    const [forecasts, setForecasts] = useState();
    const [granularity, setGranularity] = useState('none');
    const [startDateTime, setStartDateTime] = useState(dayjs('2023-12-16'));
    const [endDateTime, setEndDateTime] = useState(dayjs('2023-12-18'));

    useEffect(() => {
        fetchHistory();
    }, []);

    const contents = forecasts === undefined
        ? <p><em>Loading... </em></p>
        : <table className="table table-striped" aria-labelledby="tabelLabel">
            <thead>
                <tr>
                    <th>Time</th>
                    <th>Event Report</th>
                </tr>
            </thead>
            <tbody>
                {forecasts.map(forecast =>
                    <tr key={forecast.time}>
                        <td>{forecast.time}</td>
                        <td><pre>{forecast.eventReport}</pre></td>
                    </tr>
                )}
            </tbody>
        </table>;

    return (
        <div>
            <h1 id="tabelLabel">Chat Aggregator</h1>
            <p>This helps user view chat history at varying levels of time-based aggregation in a given time period</p>
            <label>Select Granularity</label>
            <Form.Select aria-label="Default select example" label="Granularity" onChange={(e) => setGranularity(e.target.value)}>
                <option value="none">None</option>
                <option value="minute">Minute</option>
                <option value="hour">Hour</option>
            </Form.Select>
            <p> </p>
            <label>Select Period</label>
            <p></p>
            <LocalizationProvider dateAdapter={AdapterDayjs}>
                <DateTimePicker value={startDateTime} onChange={(newValue) => setStartDateTime(newValue)} label="Start Date & Time" />
                <DateTimePicker value={endDateTime} onChange={(newValue) => setEndDateTime(newValue)} label="End Date & Time" />
            </LocalizationProvider>
            <p> </p>
            <Button onClick={fetchHistory}>Fetch</Button>
            {contents}
        </div>
    );

    async function fetchHistory() {
        const response = await fetch(`https://localhost:7006/ChatAggregator?granularity=${granularity}&startTime=${new Date(startDateTime).toISOString()}&endTime=${new Date(endDateTime).toISOString()}`);
        const data = await response.json();
        setForecasts(data);
    }
}

export default App;