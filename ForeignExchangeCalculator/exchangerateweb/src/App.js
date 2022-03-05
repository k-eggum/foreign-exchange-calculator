import { useState } from 'react';
import './App.css';

function App() {
  const [currencyForm, setCurrencyForm] = useState({
    baseCurrency: 'EUR',
    targetCurrency: undefined,
    amount: 0,
    rateDate: undefined
  });
  const [calculationResult, setCalculationResult] = useState(null);
  const [calculationError, setCalculationError] = useState(null);
  const [loadingCalculation, setLoadingCalculation] = useState(false);

  const calculateAmount = () => {
    setLoadingCalculation(true);
    setCalculationResult(undefined);
    setCalculationError(undefined);

    fetch(
      `https://localhost:44327/api/convertcurrency?targetCurrency=${currencyForm.targetCurrency}&baseCurrency=${currencyForm.baseCurrency}${currencyForm.amount ? '&amount=' + currencyForm.amount : ''}${currencyForm.rateDate ? '&rateDate=' + currencyForm.rateDate : ''}`
    )
    .then(response => {
      if (!response.ok) {
        response.text().then((text) => {
          setCalculationError(text);
          setLoadingCalculation(false);
        });
      }
      else {
        response.json().then((result) => {
          setCalculationResult(result.amountInTargetCurrency);
          setLoadingCalculation(false);
        })
      }
    });
  }

  return (
    <div className="App">
      <h1>Calculate currency</h1>
      <label>
        Base currency:
        <input 
          type='text' 
          name='baseCurrency' 
          value={currencyForm.baseCurrency} 
          onChange={e => {
            setCurrencyForm({
              ...currencyForm,
              baseCurrency: e.target.value
            })
          }} 
        />
      </label>
      <br />
      <label>
        Target currency*:
        <input 
          type='text' 
          name='targetCurrency' 
          value={currencyForm.targetCurrency} 
          onChange={e => {
            setCurrencyForm({
              ...currencyForm,
              targetCurrency: e.target.value
            })
          }}
        />
      </label>
      <br />
      <label>
        Amount to convert*:
        <input 
          type='number' 
          name='amount' 
          value={currencyForm.amount} 
          onChange={e => {
            setCurrencyForm({
              ...currencyForm,
              amount: e.target.value
            })
          }}
        />
      </label>
      <br />
      <label>
        Conversion rate date:
        <input 
          type='date' 
          name='rateDate' 
          value={currencyForm.rateDate} 
          onChange={e => {
            setCurrencyForm({
              ...currencyForm,
              rateDate: e.target.value
            })
          }}
        />
      </label>
      <br />
      <button onClick={calculateAmount}>Calculate amount</button>
      <br />
      {calculationResult && 
        <label>Calculated amount: {calculationResult}</label>
      }
      {calculationError &&
        <label>Feilmelding: {calculationError}</label>
      }
      {loadingCalculation &&
        <label>Calculating...</label>
      }
    </div>
  );
}

export default App;
