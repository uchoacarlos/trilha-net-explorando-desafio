using System;
using System.Collections.Generic;
using System.Text;
using DesafioProjetoHospedagem.Models;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine("Bem-vindo ao sistema de reserva de hospedagem!");

        Reserva reserva = new Reserva();

        while (true)
        {
            Console.WriteLine("\nEscolha uma opção:");
            Console.WriteLine("1. Cadastrar suíte");
            Console.WriteLine("2. Cadastrar reserva");
            Console.WriteLine("3. Sair");

            Console.Write("\nOpção selecionada: ");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    CadastrarSuite(reserva);
                    break;

                case "2":
                    if (reserva.Suite != null)
                    {
                        CadastrarReserva(reserva);
                    }
                    else
                    {
                        Console.WriteLine("\nAntes de cadastrar uma reserva, é necessário cadastrar uma suíte. Por favor, cadastre uma suíte pelo menu antes de continuar.");
                    }
                    break;

                case "3":
                    Console.WriteLine("\nObrigado por utilizar nosso sistema de reserva. Até logo!");
                    return;

                default:
                    Console.WriteLine("\nOpção inválida! Por favor, selecione uma opção válida.");
                    break;
            }
        }
    }

    static void CadastrarSuite(Reserva reserva)
    {
        Console.Write("\nDigite o tipo da suíte: ");
        string tipoSuite = Console.ReadLine();
        Console.Write("Digite a capacidade da suíte: ");
        int capacidade = int.Parse(Console.ReadLine());
        Console.Write("Digite o valor da diária da suíte: ");
        decimal valorDiaria = decimal.Parse(Console.ReadLine());

        reserva.CadastrarSuite(new Suite(tipoSuite, capacidade, valorDiaria));
        Console.WriteLine("Suíte cadastrada com sucesso!");
    }

    static void CadastrarReserva(Reserva reserva)
    {
        List<Pessoa> hospedes = new List<Pessoa>();

        Console.Write("\nDigite o número de hóspedes: ");
        int numHospedes = int.Parse(Console.ReadLine());

        if (numHospedes <= reserva.Suite.Capacidade)
        {
            for (int i = 1; i <= numHospedes; i++)
            {
                Console.Write($"Digite o nome do hóspede {i}: ");
                string nomeHospede = Console.ReadLine();
                hospedes.Add(new Pessoa(nomeHospede));
            }

            reserva.CadastrarHospedes(hospedes);

            Console.Write("\nDigite a quantidade de dias da reserva: ");
            int diasReserva = int.Parse(Console.ReadLine());
            reserva.DiasReservados = diasReserva;

            decimal valorTotal = reserva.CalcularValorDiaria();

            Console.WriteLine("\nReserva realizada com sucesso!");
            Console.WriteLine($"Suíte: {reserva.Suite.TipoSuite}");
            Console.WriteLine($"Valor da diária: {reserva.Suite.ValorDiaria}");
            Console.WriteLine("Hóspedes:");
            foreach (var hospede in hospedes)
            {
                Console.WriteLine($"- {hospede.NomeCompleto}");
            }
            Console.WriteLine($"Dias da reserva: {reserva.DiasReservados}");
            Console.WriteLine($"Valor total: R$ {valorTotal}");

            Console.WriteLine("\nPressione qualquer tecla para voltar ao menu...");
            Console.ReadKey(true);
        }
        else
        {
            Console.WriteLine("\nO número de hóspedes excede a capacidade da suíte. Por favor, tente novamente.");
        }
    }
}
