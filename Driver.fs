module Transactions.Driver

open System
open System.IO
open Transactions.Domain
open Transactions.Rules.Accounts
open Repository.Account
open Transaction.Utils.Railway

module Utils =
    let deleteAccountRepoFiles () =
        Directory.GetFiles(".", "account_*.json") |> Array.iter File.Delete

module AccountRepoDriver =
    let run () =
        Utils.deleteAccountRepoFiles()

        Account.Default
        |> deposit 100m
        |> withdraw 25m
        |> put
        |> ignore

        get 0 |> printfn "%A"
        get 1 |> printfn "%A"

        get 0
        >>> deposit 40m
        >>= put
        |> ignore

        get 0
        |> printfn "%A"

        get 1
        >>> deposit 40m
        >>= put
        |> printfn "%A"

module UserConsole =

    let private promptUser () = 
        Console.Write("(d)eposit, (w)ithdraw or e(x)it: ")
        Console.ReadLine()

    let private getAmount() =
        Console.Write("Enter the amount of the transaction: ")
        let input = Console.ReadLine()
        let (success, value) = input |> Decimal.TryParse
        match success with
        | true -> Ok value
        | false -> Error $"Parse of amount failed : {input}"

    let run () = 
        let rec loop account =
            printfn "Balance: %A" account.Balance

            let action = promptUser()
            printfn "You told me to do this: %A" action

            match action with 
            | "d" | "w" ->
                match getAmount() with
                | Ok value ->
                    match action with
                    | "d" -> loop (deposit value account) 
                    | "w" -> loop (withdraw value account)
                    | _ -> loop account
                | Error e ->
                    printfn "%A" e
                    loop account
            | "x" -> ()
            | _ -> 
                printfn $"Invalid Action: {action}" 
                loop account
        loop Account.Default
        ()
        