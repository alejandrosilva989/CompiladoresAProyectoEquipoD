using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace CompiladoresA
{
    class lexico
    {
        const int TOKREC = 5;
        const int MAXTOKENS = 500;
        string[] _lexemas;
        string[] _tokens;
        string _lexema;
        int _noTokens;
        int _i;
        int _iniToken;
        Regex reg,reg2;
        Automata1 oAFD;
        string re, re2 = "";
        string cad1 = "limite_2";
        string cad2 = "va16";
        public lexico() // constructor por defecto
        {
            _lexemas = new string[MAXTOKENS];
            _tokens = new string[MAXTOKENS];
            oAFD = new Automata1();
            _i = 0;
            _iniToken = 0;
            _noTokens = 0;
           
        }

        public void Inicia()
        {
            _i = 0;
            _iniToken = 0;
            _noTokens = 0;
        }
        public void Analiza(string texto,string re, string re2)
        {
            reg = new Regex(re);
            bool recAuto;
            int noAuto;
            while (_i < texto.Length)
            {
                recAuto = false;
                noAuto = 0;
                for (; noAuto < TOKREC && !recAuto;)
                    if (oAFD.Reconoce(texto, _iniToken, ref _i, noAuto,re,re2))
                        recAuto = true;
                    else
                        noAuto++;
                if (recAuto)
                {
                    _lexema = texto.Substring(_iniToken, _i - _iniToken);
                    switch (noAuto)
                    {
                        //-------------- Automata delim--------------
                        case 0: // _tokens[_noTokens] = "delim";
                            break;
                        //-------------- Automata id--------------
                        case 1:
                            if (identifica() == 1)
                                _tokens[_noTokens] = "id";
                            else
                                if (identifica() == 0)
                                _tokens[_noTokens] = _lexema;
                            else
                                if (identifica() == 2)
                                _tokens[_noTokens] = "Error";
                                break;
                        //-------------- Automata num--------------
                        case 2:
                            _tokens[_noTokens] = "numero";
                            break;
                        //-------------- Automata otros--------------
                        case 3:
                            _tokens[_noTokens] = _lexema;
                            break;
                        //-------------- Automata cad--------------
                        case 4:
                            _tokens[_noTokens] = "cad";
                            break;
                    }
                    if (noAuto != 0)
                        _lexemas[_noTokens++] = _lexema;
                }
                else
                    _i++;
                _iniToken = _i;
            }     
        }

        public int NoTokens
        {
            get { return _noTokens; }
        }

        public string[] Lexema
        {
            get { return _lexemas; }
        }
        public string[] Token
        {
            get { return _tokens; }
        }

        private int identifica()
        {
            string cad = "";
            string[] palres ={ "if", "then", "else", "end", "repeat", "until", "read", "write", "numero", "identificador"};
            for (int i = 0; i < palres.Length; i++)
                if (_lexema == palres[i])
                    return 0;
            else
                {
                    if(_lexema.Contains(palres[i])&&_lexema.Length>palres[i].Length||_lexema==cad1||_lexema==cad2)
                    {
                        return 2;
                    }

                }
           
            return 1;
        }


    }
}
