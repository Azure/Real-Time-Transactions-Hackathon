# Challenge 4: This Challenge is Questionable

Woodgrove Bank is excited about the chance to use conversational AI to allow customers to interact with their bank accounts using natural language. They want to build a chatbot that will allow customers to ask questions about their transactions, which is where you come in.

In this task you add functionality to the transactions screen that allows you to ask questions about a member's transactions using natural language and viewing the results.

You will use the Microsoft Semantic Kernel with Azure OpenAI to create a SemanticFunction that will respond with the results of the users question.

## Challenge

Your team must:

1. Complete the semantic kernel code that accepts a user's question, sends transaction data for context, instructs the bot on how to answer and format the question, and returns the answer to the user.
2. In the web app, use the **Analyze Transactions** button in the account transactions view to ask questions about the transactions.

### Hints

- Search through the solution for the `TODO: Challenge 4` comments and follow the instructions provided.
- The HTTP-triggered function in `GetTransactionStatement.cs` named `GetTransactionsAnalyis` is already created for you. This is invoked when you select the **Analyze Transactions** button when viewing account transactions in the UI.
- Try asking different types of questions about the transactions, such as the number of transactions. Think of different ways to phrase your question while providing a little context. For example: `Classify the account as spender or saver. A spender has more debit transactions than credit transactions. A saver has more credit transactions than debit transactions`

### Success Criteria

To complete this challenge successfully, you must:

- Create a SemanticFunction that accepts a user's question, sends transaction data for context, instructs the bot on how to answer and format the question, and returns the answer to the user. This must use an Azure OpenAI completions endpoint and send the account transactions as context.
- Use the **Analyze Transactions** button in the account transactions view to ask various questions about the transactions. Set breakpoints in your code to follow the execution path and see how the SemanticFunction is invoked.

### Resources

- [What is Semantic Kernel?](https://learn.microsoft.com/semantic-kernel/overview/)
