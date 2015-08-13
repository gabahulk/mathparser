using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interpretador_de_funções
{
    /// <summary>
    ///  Essa classe tem como objetivo interpretar uma função "f(x)" onde x é um vetor,
    ///  bem como executar alguns outros métodos como derivadas e "tolkenização" matematica (ver método "tolkenize").
    ///  OBS:Ao utilizar os métodos adicionais, é uma boa pratica criar uma variavel que receba a original, evitando que a mesma seja alterada.
    /// </summary>
    public class Interpretador
    {



        public Interpretador()
        {
        }

        /// <summary>
        ///  Esse método divide a string com a função em tolkiens e retona uma lista encadeada com eles.
        ///  A separação é feita nos operadores (salvo-1 o caso quando o c# retorna um numero em notação científica, 
        ///  em que se tem a letra "E" no numero e portanto o sinal que segue a letra fara parte do tolken do mesmo[ex.1E-02 = 0.01],
        ///  isso ocorre quando o numero é muito grande ou muito pequeno) e uma lista será retornada com a função já separada.
        /// </summary>
        public List<string> tolkenize(string funcao)
        {
            List<string> aux_tolk = new List<string>();
            string aux = "";
            char c_anterior = 'q';

            int count = 1;
            foreach (char c in funcao)
            {
                if (c != '+' && (c != '-' || (c == '-' && c_anterior == 'E')) && c != '*' && c != '/' && c != '^' && c != '(' && c != ')' && aux != "sqrt" && aux != "sen" && aux != "cos" && aux != "tg" && aux != "ln") // se não for um operador ou funcao
                {
                    aux += c.ToString(); //adiciona caracter para um auxilar que posteriormente irá virar um numero dentro de um nó
                }
                else
                {
                    if (aux != "" && aux != " ")
                    {
                        aux_tolk.Add(aux);//quando encontrou o operador, adicionou o numero (ou função) à lista
                    }
                    if (c == '+' || c == '-' || c == '*' || c == '/' || c == '^' || c == '(' || c == ')')
                    {
                        aux_tolk.Add(c.ToString());//adiciona o operador na lista
                        aux = "";
                    }
                    else
                    {
                        aux = c.ToString();
                    }



                }
                if (count == funcao.Length && (c != '+' && c != '-' && c != '*' && c != '/' && c != '^' && c != '(' && c != ')')) // esse if faz com que o ultimo termo que não seja um operador seja adicionado pois antes o programa só adicionava quando encontrava um operador
                {
                    aux_tolk.Add(aux);
                }

                count++;
                c_anterior = c;
            }//nesse ponto toda a conta foi quebrada e adicionada em uma lista

            return (aux_tolk);
        }

        private string reverter(string palavra)
        {
            char[] arrChar = palavra.ToCharArray();
            Array.Reverse(arrChar);
            string invertida = new String(arrChar);

            return invertida;
        }

        private double calcular(string conta)
        {
            List<string> tokens_aux = new List<string>();
            string aux = "";
            tokens_aux = tolkenize(conta);
            int index = 0, j = 1;
            double a, b;
            if (tokens_aux[0] == "-")
            {
                a = Convert.ToDouble(tokens_aux[1]);
                a = -a;
                tokens_aux.RemoveAt(0);
                tokens_aux[0] = "" + a;
            }
            for (int i = 2; i < tokens_aux.Count; i++)
            {
                if (tokens_aux[i] == "-" && (tokens_aux[i - 1] == "-" || tokens_aux[i - 1] == "+" || tokens_aux[i - 1] == "*" || tokens_aux[i - 1] == "/" || tokens_aux[i - 1] == "^"))
                {

                    a = Convert.ToDouble(tokens_aux[i + 1]);
                    a = -a;
                    tokens_aux.RemoveAt(i);
                    tokens_aux[i] = "" + a;
                }
            }

            for (int i = 0; i < tokens_aux.Count; i++)
            {
                if (tokens_aux[i] == "sqrt" || tokens_aux[i] == "sen" || tokens_aux[i] == "cos" || tokens_aux[i] == "tg" || tokens_aux[i] == "^" || tokens_aux[i] == "ln")
                {
                    switch (tokens_aux[i])
                    {
                        case "^":
                            a = Convert.ToDouble(tokens_aux[index - 1]);
                            if (tokens_aux[index + 1] != "-")
                            {
                                b = Convert.ToDouble(tokens_aux[index + 1]);
                            }
                            else
                            {

                                while (tokens_aux[index + j] == "-")
                                {
                                    aux += tokens_aux[index + j];
                                    tokens_aux.RemoveAt(index + j);

                                }
                                if (aux.Length % 2 > 0)
                                {
                                    aux = "-" + tokens_aux[index + j];
                                }
                                else
                                {
                                    aux = tokens_aux[index + j];
                                }
                                b = Convert.ToDouble(aux);
                                aux = ""; j = 1;
                            }
                            tokens_aux.RemoveAt(index + 1);
                            if (index > 0)
                            {
                                tokens_aux.RemoveAt(index);
                                index--;
                            }
                            if (i > 0)
                                i--;
                            a = Math.Pow(a, b);
                            a = Math.Round(a, 8);
                            tokens_aux[index] = a.ToString();
                            break;
                        case "sqrt":
                            if (tokens_aux[index + 1] != "-")
                            {
                                b = Convert.ToDouble(tokens_aux[index + 1]);
                            }
                            else
                            {

                                while (tokens_aux[index + j] == "-")
                                {
                                    aux += tokens_aux[index + j];
                                    tokens_aux.RemoveAt(index + j);

                                }
                                if (aux.Length % 2 > 0)
                                {
                                    aux = "-" + tokens_aux[index + j];
                                }
                                else
                                {
                                    aux = tokens_aux[index + j];
                                }
                                b = Convert.ToDouble(aux);
                                aux = ""; j = 1;
                            }
                            tokens_aux.RemoveAt(index + 1);

                            if (i > 0)
                                i--;
                            a = Math.Sqrt(b);
                            a = Math.Round(a, 8);
                            tokens_aux[index] = a.ToString();
                            break;
                        case "sen":
                            if (tokens_aux[index + 1] != "-")
                            {
                                b = Convert.ToDouble(tokens_aux[index + 1]);
                            }
                            else
                            {

                                while (tokens_aux[index + j] == "-")
                                {
                                    aux += tokens_aux[index + j];
                                    tokens_aux.RemoveAt(index + j);

                                }
                                if (aux.Length % 2 > 0)
                                {
                                    aux = "-" + tokens_aux[index + j];
                                }
                                else
                                {
                                    aux = tokens_aux[index + j];
                                }
                                b = Convert.ToDouble(aux);
                                aux = ""; j = 1;
                            }
                            tokens_aux.RemoveAt(index + 1);
                            if (i > 0)
                                i--;
                            a = Math.Sin(b);
                            a = Math.Round(a, 8);
                            tokens_aux[index] = a.ToString();
                            if (index > 0)
                            {
                                index--;
                            }
                            break;
                        case "cos":
                            if (tokens_aux[index + 1] != "-")
                            {
                                b = Convert.ToDouble(tokens_aux[index + 1]);
                            }
                            else
                            {

                                while (tokens_aux[index + j] == "-")
                                {
                                    aux += tokens_aux[index + j];
                                    tokens_aux.RemoveAt(index + j);

                                }
                                if (aux.Length % 2 > 0)
                                {
                                    aux = "-" + tokens_aux[index + j];
                                }
                                else
                                {
                                    aux = tokens_aux[index + j];
                                }
                                b = Convert.ToDouble(aux);
                                aux = ""; j = 1;
                            }
                            tokens_aux.RemoveAt(index + 1);

                            if (i > 0)
                                i--;
                            a = Math.Cos(b);
                            a = Math.Round(a, 8);
                            tokens_aux[index] = a.ToString();
                            if (index > 0)
                            {
                                index--;
                            }
                            break;
                        case "tg":
                            if (tokens_aux[index + 1] != "-")
                            {
                                b = Convert.ToDouble(tokens_aux[index + 1]);
                            }
                            else
                            {

                                while (tokens_aux[index + j] == "-")
                                {
                                    aux += tokens_aux[index + j];
                                    tokens_aux.RemoveAt(index + j);

                                }
                                if (aux.Length % 2 > 0)
                                {
                                    aux = "-" + tokens_aux[index + j];
                                }
                                else
                                {
                                    aux = tokens_aux[index + j];
                                }
                                b = Convert.ToDouble(aux);
                                aux = ""; j = 1;
                            }
                            tokens_aux.RemoveAt(index + 1);

                            if (i > 0)
                                i--;
                            a = Math.Tan(b);
                            a = Math.Round(a, 8);
                            tokens_aux[index] = a.ToString();
                            if (index > 0)
                            {
                                index--;
                            }
                            break;
                        case "ln":
                            if (tokens_aux[index + 1] != "-")
                            {
                                b = Convert.ToDouble(tokens_aux[index + 1]);
                            }
                            else
                            {

                                while (tokens_aux[index + j] == "-")
                                {
                                    aux += tokens_aux[index + j];
                                    tokens_aux.RemoveAt(index + j);

                                }
                                if (aux.Length % 2 > 0)
                                {
                                    aux = "-" + tokens_aux[index + j];
                                }
                                else
                                {
                                    aux = tokens_aux[index + j];
                                }
                                b = Convert.ToDouble(aux);
                                aux = ""; j = 1;
                            }
                            tokens_aux.RemoveAt(index + 1);

                            if (i > 0)
                                i--;
                            a = Math.Log(b, Math.E);
                            a = Math.Round(a, 8);
                            tokens_aux[index] = a.ToString();
                            if (index > 0)
                            {
                                index--;
                            }
                            break;
                    }
                }
                index++;
            }
            index = 0;
            for (int i = 0; i < tokens_aux.Count; i++)
            {
                if (tokens_aux[i] == "*" || tokens_aux[i] == "/")
                {
                    switch (tokens_aux[i])
                    {
                        case "/":
                            a = Convert.ToDouble(tokens_aux[index - 1]);
                            if (tokens_aux[index + 1] != "-")
                            {
                                b = Convert.ToDouble(tokens_aux[index + 1]);
                            }
                            else
                            {

                                while (tokens_aux[index + j] == "-")
                                {
                                    aux += tokens_aux[index + j];
                                    tokens_aux.RemoveAt(index + j);

                                }
                                if (aux.Length % 2 > 0)
                                {
                                    aux = "-" + tokens_aux[index + j];
                                }
                                else
                                {
                                    aux = tokens_aux[index + j];
                                }
                                b = Convert.ToDouble(aux);
                                aux = ""; j = 1;
                            }
                            tokens_aux.RemoveAt(index + 1);
                            if (index > 0)
                            {
                                tokens_aux.RemoveAt(index);
                                index--;
                            }
                            if (i > 0)
                                i--;
                            a = a / b;
                            a = Math.Round(a, 8);
                            tokens_aux[index] = a.ToString();
                            break;
                        case "*":
                            a = Convert.ToDouble(tokens_aux[index - 1]);
                            if (tokens_aux[index + 1] != "-")
                            {
                                b = Convert.ToDouble(tokens_aux[index + 1]);
                            }
                            else
                            {

                                while (tokens_aux[index + j] == "-")
                                {
                                    aux += tokens_aux[index + j];
                                    tokens_aux.RemoveAt(index + j);

                                }
                                if (aux.Length % 2 > 0)
                                {
                                    aux = "-" + tokens_aux[index + j];
                                }
                                else
                                {
                                    aux = tokens_aux[index + j];
                                }
                                b = Convert.ToDouble(aux);
                                aux = ""; j = 1;
                            }
                            tokens_aux.RemoveAt(index + 1);
                            if (index > 0)
                            {
                                tokens_aux.RemoveAt(index);
                                index--;
                            }
                            if (i > 0)
                                i--;
                            a = a * b;
                            a = Math.Round(a, 8);
                            tokens_aux[index] = a.ToString();
                            break;
                    }
                }
                index++;
            }
            index = 0;
            for (int i = 0; i < tokens_aux.Count; i++)
            {
                if (tokens_aux[i] == "+" || tokens_aux[i] == "-")
                {
                    switch (tokens_aux[i])
                    {
                        case "+":

                            if (index - 1 >= 0)
                            {
                                if (tokens_aux[index - 1] != "+" && tokens_aux[index - 1] != "-" && tokens_aux[index - 1] != "*" && tokens_aux[index - 1] != "/")
                                {
                                    a = Convert.ToDouble(tokens_aux[index - 1]);
                                }
                                else
                                {
                                    a = 0;
                                }
                            }
                            else
                            {
                                a = 0;
                            }
                            if (tokens_aux[index + 1] != "-")
                            {
                                b = Convert.ToDouble(tokens_aux[index + 1]);
                            }
                            else
                            {

                                while (tokens_aux[index + j] == "-")
                                {
                                    aux += tokens_aux[index + j];
                                    tokens_aux.RemoveAt(index + j);

                                }
                                if (aux.Length % 2 > 0)
                                {
                                    aux = "-" + tokens_aux[index + j];
                                }
                                else
                                {
                                    aux = tokens_aux[index + j];
                                }
                                b = Convert.ToDouble(aux);
                                aux = ""; j = 1;
                            }
                            tokens_aux.RemoveAt(index + 1);
                            if (index > 0)
                            {
                                tokens_aux.RemoveAt(index);
                                index--;
                            }
                            if (i > 0)
                                i--;
                            a = a + b;
                            a = Math.Round(a, 8);
                            tokens_aux[index] = a.ToString();
                            break;
                        case "-":
                            if (index - 1 >= 0)
                            {
                                if (tokens_aux[index - 1] != "+" && tokens_aux[index - 1] != "-" && tokens_aux[index - 1] != "*" && tokens_aux[index - 1] != "/")
                                {
                                    a = Convert.ToDouble(tokens_aux[index - 1]);
                                }
                                else
                                {
                                    a = 0;
                                }
                            }
                            else
                            {
                                a = 0;
                            }
                            if (tokens_aux[index + 1] != "-")
                            {
                                b = Convert.ToDouble(tokens_aux[index + 1]);
                            }
                            else
                            {

                                while (tokens_aux[index + j] == "-")
                                {
                                    aux += tokens_aux[index + j];
                                    tokens_aux.RemoveAt(index + j);

                                }
                                if (aux.Length % 2 > 0)
                                {
                                    aux = "-" + tokens_aux[index + j];
                                }
                                else
                                {
                                    aux = tokens_aux[index + j];
                                }
                                b = Convert.ToDouble(aux);
                                aux = ""; j = 1;
                            }
                            tokens_aux.RemoveAt(index + 1);
                            if (index > 0)
                            {
                                tokens_aux.RemoveAt(index);
                                index--;
                            }
                            if (i > 0)
                                i--;
                            a = a - b;
                            a = Math.Round(a, 8);
                            tokens_aux[index] = a.ToString();
                            break;
                    }
                }
                index++;

            }

            a = Convert.ToDouble(tokens_aux[0]);
            return (a);
        }

        /// <summary>
        ///  Método que interpreta a função dada no ponto.Tenha em mente que as variaveis são passadas como um vetor, ou seja x[1],x[2],...,x[n];
        ///  o interpretador entende os operadores basicos(+,-,*,/,^) e algumas outras funçoes como sen,cos,tg,sqrt e ln.
        ///  O interpretador executa primeiro o que estiver entre parenteses. chaves e colchetes NÃO ESTÃO IMPLEMENTADOS
        ///  OBS1: O interpretados NÃO entenderá 2x[1], é NECESSARIA a utilização do operador * para multiplicação.
        ///  OBS2: O interpretador NÃO entenderá x sem alguma posição que indique que posição do vetor ele está.
        ///  OBS3.:O interpretador NÃO entenderá x[0]. Começe a partir de x[1].
        /// </summary>
        public double interpretar(string funcao, double[] valores)
        {
            //subistituir variaves pelos valores
            int count = 0;
            List<string> tokens = new List<string>();
            List<string> tokens_aux = new List<string>();
            tokens = tolkenize(funcao);
            foreach (string token in tokens)
            {
                tokens_aux.Add(token);
            }
            foreach (string token in tokens)
            {
                if (token.Contains('x') || token.Contains('X'))
                {
                    for (int j = 1; j <= valores.Length; j++)
                    {
                        if (token.Contains("[" + j + "]"))
                        {
                            tokens_aux[count] = "" + valores[j-1];
                        }
                    }
                }
                count++;
            }
            tokens.Clear();
            foreach (string token in tokens_aux)
            {
                tokens.Add(token);
            }
            int aux_num = 0;
            List<string> tokens_aux2 = new List<string>();
            int i = 0;
            string aux = "";
            foreach (string token in tokens)
            {
                tokens_aux.Add(token);
            }

            while (tokens.Contains(")"))
            {
                aux_num++;
                if (tokens[i] == ")")//acha o primeiro fecha parenteses
                {
                    tokens.RemoveAt(i);
                    for (int j = i - 1; j >= 0; j--)//o -1 serve para decrementar primeiro pois senão o programa iria pegar o ')'
                    {
                        if (tokens[j] == "(")
                        {
                            tokens_aux2.Reverse();
                            foreach (var tolkien in tokens_aux2)
                            {
                                aux += tolkien;
                            }
                            tokens_aux2.Clear();
                            tokens[j] = "" + calcular(aux);
                            aux = "";
                            i = 0;
                            aux_num = 0;
                            break;

                        }
                        else
                        {
                            tokens_aux2.Add(tokens[j]);
                            tokens.RemoveAt(j);
                        }
                    }
                }
                i++;
            }//aqui todos os parenteses  já foram calculados
            aux = "";
            foreach (string token in tokens)
            {
                aux += token;
            }
            tokens.Clear();
            tokens_aux.Clear();
            tokens_aux2.Clear();
            return (calcular(aux));
        }

        //métodos adicionais
        /// <summary>
        ///  método da derivada primeira tem como parametros a função, em relação a que variavel será feita a derivada e o vetor dos valores das variaveis
        /// </summary>
        public double derivada(string func, int alvo, double[] variaveis)
        {
            Interpretador inter = new Interpretador();
            double h = 1;
            double epslon = 0.01;
            double[] var = new double[variaveis.Length];
            double[] var2 = new double[variaveis.Length];
            for (int i = 0; i < variaveis.Length; i++)
            {
                if (alvo-1 == i)
                {
                    var[i] = variaveis[i] + h;
                    var2[i] = variaveis[i] - h;
                }
                else
                {
                    var[i] = variaveis[i];
                    var2[i] = variaveis[i];
                }
            }

            double d1 = ((inter.interpretar(func, var) - inter.interpretar(func, var2)) / (2 * h)), d2 = 0;
            for (int i = 0; i < 1000; i++)
            {
                h = h / 2;
                var[alvo-1] = variaveis[alvo-1] + h;
                var2[alvo-1] = variaveis[alvo-1] - h;
                d2 = ((inter.interpretar(func, var) - inter.interpretar(func, var2)) / (2 * h));
                if (Math.Abs(d2 - d1) < epslon)
                {
                    return (Math.Round(d2, 8));
                }
                d1 = Math.Round(d2, 8);
            }
            return (d2);
        }
        /// <summary>
        ///   método da derivada segunda tem como parametros a função, em relação a que variavel será feita a derivada e o vetor dos valores das variaveis
        /// </summary>
        public double derivada_segunda(string func, int alvo, double[] variaveis)
        {
            Interpretador inter = new Interpretador();
            double h = 0.1;
            double epslon = 0.001;
            double[] var = new double[variaveis.Length];
            double[] var2 = new double[variaveis.Length];
            double[] var3 = new double[variaveis.Length];
            for (int i = 0; i < variaveis.Length; i++)
            {
                if (i == alvo-1)
                {
                    var[alvo-1] = variaveis[alvo-1] + 2 * h;
                    var2[alvo-1] = variaveis[alvo-1];
                    var3[alvo-1] = variaveis[alvo-1] - 2 * h;
                }
                else
                {
                    var[i] = variaveis[alvo-1];
                    var2[i] = variaveis[alvo-1];
                    var3[i] = variaveis[alvo-1];
                }
            }


            double d1 = ((inter.interpretar(func, var) - 2 * (inter.interpretar(func, var2)) + inter.interpretar(func, var3)) / (4 * h * h)), d2 = 0;
            for (int i = 0; i < 10; i++)
            {
                h = h / 2;
                var[alvo-1] = variaveis[alvo-1] + 2 * h;
                var2[alvo-1] = variaveis[alvo-1];
                var3[alvo-1] = variaveis[alvo-1] - 2 * h;
                d2 = ((inter.interpretar(func, var) - 2 * (inter.interpretar(func, var2)) + inter.interpretar(func, var3)) / (4 * h * h));
                if (Math.Abs(d2 - d1) < epslon)
                {
                    return (Math.Round(d2, 8));
                }
                d1 = Math.Round(d2, 8);
            }
            return (d2);
        }
        /// <summary>
        ///  Esse método calcula a derivada parcial com relação a duas variáveis DIFERENTES e tem como parametros a função (formato string) , um vetor com as variaveis e as variaveis alvo-1.
        ///  Se, por exemplo, quiser fazer uma derivada em relação a x[1] e x[2] é só colocar 1 e 2 como alvo1-1 e alvo2-1,respectivamente.
        /// </summary>
        public double derivada_parcial(string func, double[] variaveis, int alvo1, int alvo2)
        {
            Interpretador inter = new Interpretador();
            double h = 0.1;
            double epslon = 0.0001;
            double d1 = 0;
            double d2 = 0;
            double diferença;
            double f1, f2, f3, f4;
            double xi = variaveis[alvo1-1];
            double xj = variaveis[alvo2-1];
            do
            {
                variaveis[alvo1-1] = xi + h;
                variaveis[alvo2-1] = xj + h;
                f1 = inter.interpretar(func, variaveis);
                variaveis[alvo2-1] = xj - h;
                f2 = inter.interpretar(func, variaveis);
                variaveis[alvo1-1] = xi - h;
                variaveis[alvo2-1] = xj + h;
                f3 = inter.interpretar(func, variaveis);
                variaveis[alvo2-1] = xj - h;
                f4 = inter.interpretar(func, variaveis);
                d2 = (f1 - f2 - f3 + f4) / (4 * Math.Pow(h, 2));
                diferença = d2 - d1;
                d1 = d2;
                h = h / 2;
            } while (Math.Abs(diferença) > epslon);
            return (d2);
        }
        /// <summary>
        ///  Esse método calcula o vetor gradiente da função em um ponto dado e tem como parametros a função (formato string) e um vetor com as variaveis.
        ///  Retorna um vetor de tamanho n (onde n é o tamanho do vetor "variaveis").
        /// </summary>
        public double[] gradiente(string func, double[] variaveis)
        {
            double[] gradiente = new double[variaveis.Length];
            for (int i = 1; i <= gradiente.Length; i++)
            {
                gradiente[i-1] = derivada(func, i, variaveis);
            }
            return (gradiente);
        }
        /// <summary>
        ///  Esse método calcula a matriz hessiana da função em um ponto dado e tem como parametros a função (formato string) e um vetor com as variaveis.
        ///  Retorna uma matriz nxn(onde n é o tamanho do vetor "variaveis").
        /// </summary>
        public double[,] hessiana(string func, double[] variaveis)
        {
            double[,] hes = new double[variaveis.Length, variaveis.Length];
            double[] var = new double[variaveis.Length];
            for (int i = 1; i <= var.Length; i++)
            {
                var[i-1] = variaveis[i-1];
            }
            for (int i = 1; i <= variaveis.Length; i++)
            {
                for (int j = i; j <= variaveis.Length; j++)
                {
                    if (j == i)
                    {
                        hes[i-1, j-1] = derivada_segunda(func, i, var);
                    }
                    else
                    {
                        hes[i-1, j-1] = derivada_parcial(func, var, i, j);
                        hes[j-1, i-1] = hes[i-1, j-1];
                    }

                }
            }
            return (hes);
        }


    }
}

