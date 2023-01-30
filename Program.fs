open System

[<EntryPoint>]
let main argv =
    Console.WriteLine("Hello from the transaction processor!")
    //Transactions.Driver.Driver.run()
    Transactions.Driver.AccountRepoDriver.run()
    Console.WriteLine("Bye!")
    0