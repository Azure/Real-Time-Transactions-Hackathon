# Challenge 4: This Challenge is Questionable

Woodgrove Bank is excited about the chance to use conversational AI to allow customers to interact with their bank accounts using natural language. They want to build a chatbot that will allow customers to ask questions about their transactions, which is where you come in.

In this task you add functionality to the transactions screen that allows you to ask questions about a member's transactions using natural language and viewing the results.

You will use the Microsoft Semantic Kernel with Azure OpenAI to create a SemanticFunction that will respond with the results of the users question.

## Challenge

Your team must:

1. Create a SemanticFunction that accepts a user's question, sends transaction data for context, instructs the bot on how to answer and format the question, and returns the answer to the user.
2. Create a new function that will call the SemanticFunction and return the results to the user via a RESTful endpoint.
3. Add a button to the account transactions view that provides an input field for the user to ask questions about the transaction and displays the results in a dialog.

### Success Criteria

To complete this challenge successfully, you must:

- Create a SemanticFunction that accepts a user's question, sends transaction data for context, instructs the bot on how to answer and format the question, and returns the answer to the user. This must use an Azure OpenAI completions endpoint and send the account transactions as context.
- Create a new function triggered by a GET HTTP request with the `accountId` in the route and the user's question in the query string that will call the SemanticFunction and return the results to the user via a RESTful endpoint. This should retrieve the transactions for the account from Cosmos DB and pass them to the SemanticFunction. If transactions are not found, it should return a 404 Not Found status code.
- Update the account transactions view to add a button that provides an input field for the user to ask questions about the transaction and displays the results in a dialog. This should call the new function.

### Resources

- [What is Semantic Kernel?](https://learn.microsoft.com/semantic-kernel/overview/)
