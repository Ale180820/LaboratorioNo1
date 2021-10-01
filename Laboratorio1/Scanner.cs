using System;
using System.Text.RegularExpressions;

namespace Laboratorio1 {
    public class Scanner {

        private string _operation = "";
        private int _index = 0;
        private string error = "error léxico";

        //Valores iniciales para el scanner
        public Scanner(string operation) {
            _operation = operation + (char)TokenType.EOF;
            _index = 0;
        }

        public Token GetToken() {
            Token result = new Token() { Value = (char)0 };
            bool tokenFound = false;
            while (!tokenFound) {
                char peek = _operation[_index];
                
                // Quitar espacios en blanco
                while (char.IsWhiteSpace(peek)) {
                    _index++;
                    peek = _operation[_index];
                }

                switch (peek) {
                    case '\0':
                        tokenFound = true;
                        result.Tag = TokenType.EOF;
                        break;
                    case (char)TokenType.LParen:
                    case (char)TokenType.RParen:
                    case (char)TokenType.Less:
                    case (char)TokenType.Plus:
                    case (char)TokenType.Star:
                    case (char)TokenType.Div:
                        tokenFound = true;
                        result.Tag = (TokenType)peek;
                        break;
                    default:
                        //Numbers
                        if (isDigit(peek)) {
                            tokenFound = true;
                            result.Tag = TokenType.Symbol;
                            result.Value = peek;
                        }
                        else {
                            throw new Exception(error);
                        }
                        break;

                } // SWITCH - peek
                _index++;
            } //WHILE - tokenFound
            return result;
        } //GetToken

         private bool isDigit(char digit) {
            if (Regex.IsMatch(digit.ToString(), @"^[0-9]$")){
                return true;
            }
            else{
                return false;
            } 
         } // isDigit
    }
}
