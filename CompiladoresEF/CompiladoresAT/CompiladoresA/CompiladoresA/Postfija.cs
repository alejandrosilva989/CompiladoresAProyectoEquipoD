using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompiladoresA
{
    class Postfija
    {

        List<Char> alfabeto = new List<Char>();
        Dictionary<Char, int> operandos = new Dictionary<char, int>();

            public Postfija(Dictionary<Char, int> ope, List<Char> alf)
            {
                operandos = new Dictionary<char, int>(ope);
                alfabeto = new List<char>(alf);
            }

        public string convierteExOr(string expresionR)
        {
            string or = "";
            string oraux = "";
            int j, i = 0;

            while (i < expresionR.Length)
            {
                if (expresionR[i] == '[')
                {
                    or += '(';
                    or += expresionR[i + 1];
                    j = i + 2;
                    while (expresionR[j] != ']' && expresionR[j] != '-')
                    {
                        if (alfabeto.Contains(expresionR[j]))
                        {
                            or += '|';
                            or += expresionR[j];
                        }
                        j++;
                        if (expresionR[j] == ']')
                        {
                            or += ')';
                            i = j + 1;
                        }
                    }

                    if (expresionR[j] == '-')
                    {

                        for (int i2 = (int)expresionR[j - 1]; i2 <= (int)expresionR[j + 1]; i2++)
                        {
                            if (i2 != (int)expresionR[j - 1])
                                or += (Char)i2;
                            if (i2 != (int)expresionR[j + 1])
                                or += '|';
                        }
                        or += ')';
                        i = j + 2;
                    }


                }
                if (operandos.ContainsKey(expresionR[i]) || alfabeto.Contains(expresionR[i]))
                    or += expresionR[i];
                i++;

            }

            return or;
        }

        public string convierteAnd(string expresionER)
        {
            string and = "";
            for (int i = 0; i < expresionER.Length; i++)
            {
                if (i + 1 < expresionER.Length)
                {
                    if (operandos.ContainsKey(expresionER[i]) && operandos[expresionER[i]] == 0 && i == 0)
                        and += expresionER[i];
                    if (alfabeto.Contains(expresionER[i]) && alfabeto.Contains(expresionER[i + 1]) ||
                        operandos.ContainsKey(expresionER[i]) && alfabeto.Contains(expresionER[i + 1]) && operandos[expresionER[i]] == 3 ||
                        operandos.ContainsKey(expresionER[i]) && operandos.ContainsKey(expresionER[i + 1]) && operandos[expresionER[i]] == 0 && operandos[expresionER[i + 1]] == 0 && expresionER[i] != '(' && expresionER[i + 1] != '('
                        || alfabeto.Contains(expresionER[i]) && operandos.ContainsKey(expresionER[i + 1]) && operandos[expresionER[i + 1]] == 0 && expresionER[i + 1] == '(' ||
                        alfabeto.Contains(expresionER[i + 1]) && operandos.ContainsKey(expresionER[i]) && operandos[expresionER[i]] == 0 && expresionER[i] == ')' || operandos.ContainsKey(expresionER[i]) && expresionER[i + 1] == '(' && expresionER[i] != '(')
                    {
                        if (!and.Contains(expresionER[i]))
                        {
                            and += expresionER[i];
                            and += '&';
                            and += expresionER[i + 1];
                        }
                        else
                        {
                            and += '&';
                            and += expresionER[i + 1];
                        }
                    }
                    else
                    {
                        if (operandos.ContainsKey(expresionER[i + 1]) && alfabeto.Contains(expresionER[i]) && i + 1 < expresionER.Length - 1)
                        {
                            if (!and.Contains(expresionER[i]))
                                and += expresionER[i];
                            and += expresionER[i + 1];
                        }
                        else
                        {
                            and += expresionER[i + 1];
                        }
                    }
                }
                else
                if (alfabeto.Contains(expresionER[i]) && !and.Contains(expresionER[i]))
                {
                    and += '&';
                    and += expresionER[i];
                }
                else
                {
                    if (alfabeto.Contains(expresionER[i]) && !and.Contains(expresionER[i]))
                        and += expresionER[i];
                }

            }
            return and;
        }

        public string Postfija2(string expresionR)
        {
            expresionR = convierteAnd(convierteExOr(expresionR));
            string postfija = "";
            Stack<Char> pila = new Stack<Char>();
            Char tope;

            for (int i = 0; i < expresionR.Length; i++)
            {
                if (pila.Count == 0 && operandos.ContainsKey(expresionR[i]))
                    pila.Push(expresionR[i]);
                else
                {
                    if (alfabeto.Contains(expresionR[i]))
                    {
                        postfija += expresionR[i];

                    }
                    else
                    if (operandos[expresionR[i]] == 0)
                    {
                        if (expresionR[i] == '(')
                        {
                            pila.Push(expresionR[i]);
                        }
                        else
                        {
                            tope = pila.Pop();
                            while (tope != '(' && pila.Count > 0)
                            {
                                postfija += tope;
                                tope = pila.Pop();

                            }
                        }
                    }
                    else
                    {
                        if (operandos[expresionR[i]] == 3)
                        {
                            tope = pila.Pop();
                            if (operandos[expresionR[i]] > operandos[tope])
                            {
                                pila.Push(tope);
                                pila.Push(expresionR[i]);
                            }
                            else
                            {
                                while ((operandos[expresionR[i]] < operandos[tope] || operandos[expresionR[i]] == operandos[tope]) && pila.Count > 0)
                                {
                                    postfija += tope;
                                    if (pila.Count > 0)
                                    {
                                        tope = pila.Pop();
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                                pila.Push(tope);
                                pila.Push(expresionR[i]);

                            }
                        }
                        else
                        {
                            if (operandos[expresionR[i]] == 2)
                            {
                                tope = pila.Pop();
                                if (operandos[expresionR[i]] > operandos[tope])
                                {
                                    pila.Push(tope);
                                    pila.Push(expresionR[i]);
                                }
                                else
                                {
                                    if (operandos[expresionR[i]] < operandos[tope] || operandos[expresionR[i]] == operandos[tope])
                                    {
                                        while ((operandos[expresionR[i]] < operandos[tope] || operandos[expresionR[i]] == operandos[tope]))
                                        {
                                            postfija += tope;
                                            if (pila.Count > 0)
                                            {
                                                tope = pila.Pop();
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                        pila.Push(expresionR[i]);
                                    }

                                }
                            }
                            else
                            {
                                if (operandos[expresionR[i]] == 1)
                                {
                                    tope = pila.Pop();
                                    if (operandos[expresionR[i]] > operandos[tope])
                                    {
                                        pila.Push(tope);
                                        pila.Push(expresionR[i]);
                                    }
                                    else
                                    {
                                        if (operandos[expresionR[i]] <= operandos[tope])
                                        {
                                            while ((operandos[expresionR[i]] < operandos[tope] || operandos[expresionR[i]] == operandos[tope]) && pila.Count > 0)
                                            {
                                                postfija += tope;
                                                if (pila.Count > 0)
                                                {
                                                    tope = pila.Pop();
                                                }
                                                else
                                                {
                                                    break;
                                                }
                                            }
                                            pila.Push(tope);
                                            pila.Push(expresionR[i]);
                                        }


                                    }
                                }
                            }
                        }
                    }
                }
            }
            while (pila.Count > 0)
            {
                tope = pila.Pop();
                postfija += tope;
            }


            return postfija;
        }
    }
}
