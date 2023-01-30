module Transactions.Rules

open Transactions.Domain

module Accounts =
    let private nextTransactionId account =
        match account.Transactions with
        | [] -> 0
        | _ ->
            account.Transactions
            |> List.map (fun t -> t.Id)
            |> List.max
            |> (+) 1

    let private buildNewAccount account transaction =
        let newTransactions = List.append account.Transactions [transaction]
        let newBalance =
            newTransactions
            |> List.map(fun t -> 
                match t.Type  with
                | Deposit -> t.Amount
                | Withdraw -> -t.Amount)
            |> List.sum
        {
            account with
                Balance = newBalance
                Transactions = newTransactions
        }

    let deposit amount account = 
        let newTransaction = Transaction.NewDeposit (nextTransactionId account) amount
        buildNewAccount account newTransaction

    let withdraw amount account = 
        let newTransaction = Transaction.NewWithdraw (nextTransactionId account) amount
        buildNewAccount account newTransaction