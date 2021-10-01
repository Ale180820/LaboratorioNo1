using System;

namespace Laboratorio1 {
    public class Parser {
        Scanner _scanner;
        Token _token;
        //Valores default de sum/rest y div/multi
        int plusLess = 0;
        int divStar = 1;
        //Valores para calculo de resultado
        string num;
        double tems;
        double result;

        //Error
        private string error = "error de sintaxis";
        
        //Transiciones de la gramática
        private void Match(TokenType tag) {
            if (_token.Tag == tag) {
                if (_token.Tag == TokenType.Symbol) {
                    num += _token.Value;
                    result = Convert.ToDouble(num);
                }
                _token = _scanner.GetToken();
            }
            else {
                throw new Exception(error);
            }
        }//Match

        private double E() {
            switch (_token.Tag) {
                case TokenType.LParen:
                case TokenType.Symbol:
                case TokenType.Less:
                    return T() + EP();
                default:
                    throw new Exception(error);
            }
        }//E

        private double EP() {
            switch (_token.Tag) {
                //Suma
                case TokenType.Plus:
                    Match(TokenType.Plus);
                    return T() + EP();
                //Resta
                case TokenType.Less:
                    Match(TokenType.Less);
                    return -T() + EP();
                default:
                    return plusLess;
            }
        }//EP

        private double T() {
            switch (_token.Tag) {
                case TokenType.LParen:
                case TokenType.Symbol:
                case TokenType.Less:
                    return G() * TP();
                default:
                    throw new Exception(error);
            }
        }//T

        private double TP() {
            switch (_token.Tag) {
                //Multiplicación
                case TokenType.Star:
                    Match(TokenType.Star);
                    return G() * TP();
                //División
                case TokenType.Div:
                    Match(TokenType.Div);
                    return (Math.Pow(G(),-1)) * TP();
                default:
                    return divStar;
            }
        }//EP

        private double G() {
            switch (_token.Tag) {
                //Inverso aditivo
                case TokenType.Less:
                    Match(TokenType.Less);
                    return -G();
                case TokenType.LParen:
                case TokenType.Symbol:
                    return F();
                default:
                    throw new Exception(error);
            }
        }//T

        private double F() {
            switch (_token.Tag) {
                //Parentesis para generar la prioridad
                case TokenType.LParen:
                    Match(TokenType.LParen);
                    tems = E();
                    Match(TokenType.RParen);
                    return tems;
                //Numeros
                case TokenType.Symbol:
                    Match(TokenType.Symbol);
                    return FP();
                default:
                    throw new Exception(error);
            }
        }//F
        private double FP() {
            switch (_token.Tag) {
                //Numero
                case TokenType.Symbol:
                    Match(_token.Tag);
                    return FP();
                //Valor de retorno de numero con n dígitos 
                default:
                    num = "";
                    return result;
            }
        }//FP

        public double Parse(string operation) {
            _scanner = new Scanner(operation + (char)TokenType.EOF);
            _token = _scanner.GetToken();
            switch (_token.Tag) {
                case TokenType.LParen:
                case TokenType.Symbol:
                case TokenType.Less:
                    result = E();
                    break;
                default:
                    break;
            }
            Match(TokenType.EOF);
            return result;
        }//Parse
    }
}
