using ApiPessoa.Models.IPessoa;

namespace ApiPessoa
{
    public class Pessoa : IPessoa
    {
        public string? nome { get; set; }
        public string? idade { get; set; }

        public Pessoa(string nome, string idade)
        {
            this.nome = nome;
            this.idade = idade;
        }
    }
}