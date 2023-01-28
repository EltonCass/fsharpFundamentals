module Transactions.Driver

open System

module UserConsole =

    let private promptUser () = 
        Console.Write("(d)eposit, (w)ithdraw or e(x)it: ")
        Console.ReadLine()

    let private getAmount() =
        Console.Write("Enter the amount of the transaction: ")
        Console.ReadLine() |> Decimal.Parse

    let userLoop() =
        let mutable running = true
        let mutable balance = 0m

        while running do
            printfn "Balance: %A" balance

            let action = promptUser()
            printfn "You told me to do this: %A" action

            balance <-
            match action with
                | "d" -> balance + getAmount()
                | "w" -> balance - getAmount()
                | _ -> 
                    running <- action <> "x"
                    balance
        
        