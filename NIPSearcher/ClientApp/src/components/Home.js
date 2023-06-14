import React, { Component } from 'react';

export class Home extends Component {
    static displayName = Home.name;

    constructor(props) {
        super(props);
        this.state = { subiect: [], loading: false, inputValue: '' };
        this.handleInputChange = this.handleInputChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    static renderSubiectTable(subject) {
        return (

            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Nazwa</th>
                        <td>Adres</td>
                        <th>Status VAT</th>
                        <th>KRS</th>
                        <th>Konta bankowe</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>{subject.name}</td>
                        <td>{subject.residenceAddress}</td>
                        <td>{subject.statusVat}</td>
                        <td>{subject.krs}</td>
                        <td>
                            {subject.accounts != null ?
                                subject.accounts['$values'].map(account => (
                                    <span key={account.id}>{account.number}<br /></span>
                                )) : <></>}
                        </td>
                    </tr>
                </tbody>
            </table>
        );
    }

    handleInputChange(event) {

        this.setState({ inputValue: event.target.value });
    }

    handleSubmit(event) {

        this.setState({ loading: true, subiect: [] });
        event.preventDefault();
        this.getDataByNip();
    }

    render() {
        let loading = this.state.loading ?
            <p><br></br> <em>Pobieranie.</em></p>
            : null;

        let table = this.state.subiect.nip ?
            Home.renderSubiectTable(this.state.subiect)
            : null;

        return (
            <div>
                <h3 id="tabelLabel">Szukanie firmy</h3>
                <form onSubmit={this.handleSubmit}>
                    <label>
                        Szukaj NIP:<span>&nbsp;</span>
                        <input type="text" pattern="[0-9]{10}" value={this.state.inputValue} onChange={this.handleInputChange} />
                    </label>
                    <button type="submit">Szukaj</button>
                </form>
                {loading}
                {table}
            </div>
        );
    }

    async getDataByNip() {
        const response = await fetch(`api/getnip/${this.state.inputValue}`);
        const data = await response.json();
        this.setState({ subiect: data, loading: false });
    }
}
