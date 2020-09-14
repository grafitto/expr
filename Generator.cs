using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace expr
{
    class Generator
    {
        private List<Token> tokens;
        public Generator()
        {
            this.tokens = new List<Token>();
        }

        public void Feed(string code)
        {
            Lexer lexer = new Lexer(code);
            Parser parser = new Parser(lexer);
            this.tokens = parser.Parse();
        }
        public int Evaluate()
        {
            var AssemblyMain = new DynamicMethod("_main", typeof(int), null, typeof(Program).Module);
            var il = AssemblyMain.GetILGenerator();
            foreach(Token token in this.tokens) {
                if(token.Type == TokenType.OPERAND) {
                    int value = int.Parse(token.Value);
                    il.Emit(OpCodes.Ldc_I4, value);
                } else {
                    switch(token.Operator) {
                        case OperatorsType.PLUS:
                            il.Emit(OpCodes.Add);
                            break;
                        case OperatorsType.MINUS:
                            il.Emit(OpCodes.Sub);
                            break;
                        case OperatorsType.MULTIPLY:
                            il.Emit(OpCodes.Mul);
                            break;
                        case OperatorsType.DIVIDE:
                            il.Emit(OpCodes.Div);
                            break;
                        default:
                            throw new Exception("Error evaluating operator");
                    }
                }
            }
            il.Emit(OpCodes.Ret);
            var AssemblyMethodCallable = (Func<int>)AssemblyMain.CreateDelegate(typeof(Func<int>));
            return AssemblyMethodCallable();
        }
    }
}
