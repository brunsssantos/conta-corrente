using System.Xml;

class Program
{
    static void Main(string[] args)
    {
        ContaCorrente conta1 = new ContaCorrente();

        conta1.saldo = 1000;
        conta1.numero = 12;
        conta1.limite = 0;
        conta1.movimentacoes = new Movimentacao[10];

        conta1.Sacar(200);

        conta1.Depositar(300);

        conta1.Depositar(500);

        conta1.Sacar(200);

        ContaCorrente conta2 = new ContaCorrente();

        conta2.saldo = 300;
        conta2.numero = 13;
        conta2.limite = 0;
        conta2.movimentacoes = new Movimentacao[10];

        conta1.TransferirPara(conta2, 400);

        conta1.ExibirExtrato();

        Console.WriteLine();

        conta2.ExibirExtrato();
    }
}

class ContaCorrente
{
    public int numero; //número da conta
    public double saldo; //quantidade de dinheiro
    public double limite; //limite extra
    public Movimentacao[] movimentacoes; //lista de transações feitas
    public int contadorMov; //conta quantas transações já foram feitas

    public void Depositar (double valor)
    {
        saldo += valor;
        RegistrarMovimentacao("Crédito", valor);
    }

    public void Sacar (double valor)
    {
        if (valor <= saldo + limite)
        {
            saldo -= valor;
            RegistrarMovimentacao("Débito", valor);
        }
        else
        {
            Console.WriteLine($"Saque de R${valor} não permitido. Saldo insuficiente.");
        }
    }

    public void TransferirPara(ContaCorrente destino, double valor)
    {
        if (valor <= saldo + limite)
        {
            Sacar(valor);
            destino.Depositar(valor);
            Console.WriteLine($"Transferência de R${valor} da conta {numero} para conta {destino.numero} realizada com sucesso.");
        }

        else
        {
            Console.WriteLine($"Transfêrencia não permitida. Saldo insuficiente.");
        }
    }

    public void ExibirExtrato()
    {
        Console.WriteLine($"Extrato da conta {numero}");
        for (int i = 0; i < contadorMov; i++)
        {
            Console.WriteLine($"{movimentacoes[i].tipo} de R${movimentacoes[i].valor}");
        }
        Console.WriteLine($"Saldo atual: R${saldo}");
    }

    public void RegistrarMovimentacao(string tipo, double valor)
    {
        if (contadorMov < movimentacoes.Length)
        {
            movimentacoes[contadorMov] = new Movimentacao();
            movimentacoes[contadorMov].tipo = tipo;
            movimentacoes[contadorMov].valor = valor;
            contadorMov++;
        }
        else
        {
            Console.WriteLine("Limite de movimentações atingido.");
        }
    }
}

class Movimentacao
{
    public string tipo; // Crédito ou Débito
    public double valor;
}