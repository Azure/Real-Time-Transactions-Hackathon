# Azure Cosmos DB & OpenAI Reference Architecture: Payments & Accounts Hackathon

Woodgrove Bank is a global bank that has been in business for over 100 years. They have a large customer base and are looking to expand their business by offering new services to their customers. Members have accounts, each account with corresponding balances, overdraft limits and credit/debit transactions. They are looking to build a new application that will allow their customers to manage their accounts and better understand their bank transactions that contribute to their overall balance. They have decided to use Azure Cosmos DB and Azure OpenAI to build these new capabilities.

Woodgrove Bank wants to ride the wave of conversational AI to allow customers to interact with their bank accounts using natural language. They want to build a chatbot that will allow customers to ask questions about their accounts and transactions. They also want to build a secure architecture that provides high availability and scalability to support their global customer base. They are interested in how they can use high consistency across multiple regions, allowing customers to read and write to database endpoints in their local region, and make sure that the data is consistent across all regions. A pattern they want to explore is to separate read and write operations since the transaction data has a high rate of volume. They do not want to impact the performance of their database operations by introducing heavy reads against the same resource handling incoming write operations. They also want to make sure that if a region goes down, their customers within the impacted region can still access their accounts and make transactions.

In this hackathon, you will build a POC that does the following:

- Replicate transaction data across multiple geographic regions for both reads and writes, while maintaining consistency. Updates are made efficiently with the patch operation.
- Apply business rules govern if a transaction is allowed.
- Create an AI powered co-pilot enables agents to analyze transactions using natural language.
