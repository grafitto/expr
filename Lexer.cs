using System;
using System.Collections.Generic;

namespace expr
{
    class Lexer
    {
        private InputStream stream;
        public Lexer(string code)
        {
            this.stream = new InputStream(code);
        }

        public List<Token> Tokenize()
        {
            List<Token> tokens = new List<Token>();
            while(!this.stream.End()) {
                char character = this.stream.Next();
                if(Char.IsDigit(character)) {
                    string value = character.ToString();
                    // if(!this.stream.End()) {
                        do {
                            char next = this.stream.Next();
                            if(Char.IsDigit(next)) {
                                value += next;
                            }
                        } while (!this.stream.End() && Char.IsDigit(this.stream.Peek()));
                    // }
                    tokens.Add(new Token(TokenType.OPERAND, value));
                } else {
                    switch(character) {
                        case '+':
                            tokens.Add(new Token(TokenType.OPERATOR, character.ToString(), OperatorsType.PLUS));
                            break;
                        case '-':
                            tokens.Add(new Token(TokenType.OPERATOR, character.ToString(), OperatorsType.MINUS));
                            break;
                        case '/':
                            tokens.Add(new Token(TokenType.OPERATOR, character.ToString(), OperatorsType.DIVIDE));
                            break;
                        case '*':
                            tokens.Add(new Token(TokenType.OPERATOR, character.ToString(), OperatorsType.MULTIPLY));
                            break;
                        case ' ':
                            break;
                        default:
                            throw new Exception("Unknown operator: " + character);
                    }
                }
            }

            return tokens;
        }
    }

    class InputStream
    {
        private string code;
        private int position;
        public InputStream(string code)
        {
            this.code = code;
            this.position = 0;
        }

        public char Next()
        {
            return this.code[this.position++];
        }

        public char Peek()
        {
            return this.code[this.position];
        }

        public bool End()
        {
            return this.position == this.code.Length;
        }
    }

    class Token
    {
        public TokenType Type { get; }
        public OperatorsType Operator { get; }
        public string Value { get; }
        public Token(TokenType type, string value, OperatorsType op = OperatorsType.NONE)
        {
            this.Type = type;
            this.Value = value;
            this.Operator = op;
        }
    }
    
    public enum OperatorsType { NONE, PLUS, MINUS, DIVIDE, MULTIPLY }
    public enum TokenType { OPERATOR, OPERAND }
}
