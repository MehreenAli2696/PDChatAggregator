import { useEffect, useState } from 'react';
import './App.css';
import { Button, Form } from 'react-bootstrap';

function App() {
    const [forecasts, setForecasts] = useState();
    const [granularity, setGranularity] = useState('none');

    useEffect(() => {
        fetchHistory();
    }, []);

    const contents = forecasts === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
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
            <h1 id="tabelLabel">Weather forecast</h1>
            <p>This component demonstrates fetching data from the server.</p>
            <Form.Select aria-label="Default select example" onChange={(e) => setGranularity(e.target.value)}>
                <option value="none">Select Granularity</option>
                <option value="none">None</option>
                <option value="minute">Minute</option>
                <option value="hour">Hour</option>
            </Form.Select>
            <Button onClick={fetchHistory}>Fetch</Button>
            {contents}
        </div>
    );
    
    async function fetchHistory() {
        const response = await fetch(`https://localhost:7006/ChatAggregator?granularity=${granularity}&startTime=2023-12-16%2013%3A00%3A00&endTime=2023-12-18%2013%3A00%3A00`);
        const data = await response.json();
        setForecasts(data);
    }
}

export default App;