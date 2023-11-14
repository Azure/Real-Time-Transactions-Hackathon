# Challenge 5: Home Improvement: AI Edition

You may have found that sometimes the SemanticFunction works and provides an accurate result, and sometimes it appears to calculate things incorrectly or have flaws in its logic.

In this challenge you improve the the capabilities in two ways:

- Improve the prompt text to reduce any dependency on the model's parametric knowledge (this is the knowledge the model has learned during pre-training) so that it only uses the data you supply and the user query in the processing.
- Improve the handling of numbers. Sometimes numbers are better handled with native code. You will need to improve SemanticFunction by leveraging the SequentialPlanner that uses a Semantic Kernel plugin for numeric operations in addition to your SemanticFunction.

## Challenge

Your team must:

1. Improve the SemanticFunction to only use contextual knowledge that you provide it.
2. Improve the handling of numbers.

### Hints

- Use system prompts to instruct the model to only answer questions about the context you provide.
- Create a new plugin for handling numbers that contains a method for handling numbers.

### Success Criteria

To complete this challenge successfully, you must:

- Demonstrate that the SemanticFunction only answers questions about the data context you provide and tells you when it doesn't have enough information to answer the question.
- Ask questions that require numeric operations and demonstrate that the SequentialPlanner uses your plugin to answer the question.

### Resources

- [Automatically orchestrate AI with planner](https://learn.microsoft.com/semantic-kernel/ai-orchestration/planner?tabs%253DCsharp)
- [Creating semantic plugins](https://learn.microsoft.com/semantic-kernel/ai-orchestration/plugins/?tabs=Csharp)
- [Semantic Kernel native functions](https://learn.microsoft.com/semantic-kernel/ai-orchestration/native-functions)
- [Prompt engineering techniques](https://learn.microsoft.com/azure/cognitive-services/openai/concepts/advanced-prompt-engineering?pivots%253Dprogramming-language-chat-completions)
