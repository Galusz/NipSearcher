import React, { Component } from 'react';

export class FetchData extends Component {
    static displayName = FetchData.name;

    constructor(props) {
        super(props);
        this.state = { subjects: [], loading: true };
    }

    componentDidMount(){
        this.getHistory()
    }


    static renderSubiectTable(subjects) {
        return (

            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Nazwa</th>
                        <th>Adres</th>
                        <th>Status VAT</th>
                        <th>KRS</th>
                        <th>Konta bankowe</th>
                    </tr>
                </thead>
                <tbody>
                    {subjects['$values'].map((subject, index) => (
                        <tr key={index}>
                            <td>{subject.name}</td>
                            <td>{subject.residenceAddress}</td>
                            <td>{subject.statusVat}</td>
                            <td>{subject.krs}</td>
                            <td>
                                {subject.accounts != null ?
                                    subject.accounts['$values'].map((account, accountIndex) => (
                                        <span key={accountIndex}>{account.number}<br /></span>
                                    )) : null}
                            </td>
                        </tr>
                    ))}
                </tbody>
            </table>
        );
    }

    render() {
        let loading = this.state.loading ?
            <p><br></br> <em>Pobieranie.</em></p>
            : null;

        let table = this.state.subjects?.['$values'] ?
            FetchData.renderSubiectTable(this.state.subjects)
            : null;

        return (
            <div>
                <h3 id="tabelLabel">Historia</h3>
                {loading}
                {table}
            </div>
        );
    }

    async getHistory() {
        const response = await fetch(`api/gethistory`);
        const data = await response.json();
        this.setState({ subjects: data, loading: false });
    }
}
