using System;
using System.Collections.Generic;

namespace expr
{

    class Parser
    {
        private Lexer lexer;
        private List<Token> tokens;
        private Dictionary<OperatorsType, int> PRECEDENCE = new Dictionary<OperatorsType, int>();
        public Parser(Lexer lexer)
        {
            this.lexer = lexer;
            this.tokens = lexer.Tokenize();
            this.PRECEDENCE.Add(OperatorsType.MULTIPLY, 14);
            this.PRECEDENCE.Add(OperatorsType.DIVIDE, 14);
            this.PRECEDENCE.Add(OperatorsType.PLUS, 13);
            this.PRECEDENCE.Add(OperatorsType.MINUS, 13);
        }

        private List<Token> CreatePostFix() {
            List<Token> operandList = new List<Token>();
            Stack<Token> operatorStack = new Stack<Token>();
            foreach(Token token in this.tokens) {
                if(token.Type == TokenType.OPERAND) {
                    operandList.Add(token);
                } else {
                    if(operatorStack.Count > 0) {
                        for(int i = 0; i < operatorStack.Count; i++) {
                            if(this.PRECEDENCE[operatorStack.Peek().Operator] >= this.PRECEDENCE[token.Operator]) {
                                operandList.Add(operatorStack.Pop());
                            } else {
                                break;
                            }
                        }
                    }
                    operatorStack.Push(token);
                }
            }
            Token[] remunants = operatorStack.ToArray();
            foreach(Token remunant in remunants)
                operandList.Add(remunant);

            return operandList;
        }

        public List<Token> Parse() {
            return this.CreatePostFix();
        }

    }
}
