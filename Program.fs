open System

[<EntryPoint>]
let main argv =
    Console.WriteLine("Hello from the transaction processor!")
    Transactions.Driver.UserConsole.run()
    // Uncomment the following line to run the app on second functionality
    // basically it reads the transactions from an external file.
    //Transactions.Driver.AccountRepoDriver.run()
    Console.WriteLine("Bye!")
    0
