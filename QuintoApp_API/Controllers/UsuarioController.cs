using AppQuinta.Models;
using AppQuinta.Repository.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace QuintoApp_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private IUsuarioRepository _usuarioRepository;

        // Método construtor
        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }
        // EndPoint Get
        // Obter todos Usuários
        [HttpGet]
        public ActionResult<IEnumerable<Usuario>> Get()
        {
            var usuarios = _usuarioRepository.ObterTodosUsuarios().ToList();
            if (usuarios is null)
            {
                return NotFound("Produtos não localizados");
            }
            return usuarios;
        }

        // Obter Usuário por Id
        [HttpGet("{id:int}", Name = "ObterUsuarios")]
        public ActionResult<Usuario> Get(int id)
        {
            var usuario = _usuarioRepository.ObterUsuario(id);

            if (usuario is null)
            {
                return NotFound("usuário não localizado");
            }
            return usuario;
        }

        // Cadastrar usuário
        [HttpPost]
        public ActionResult Post(Usuario usuario)
        {
            if (usuario is null)
            {
                return BadRequest();

            }
            _usuarioRepository.Cadastrar(usuario);

            return Ok(usuario);

        }
        
        // Atualizar Usuário
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Usuario usuario)
        {
            if (id != usuario.IdUsu)
            {
                return BadRequest();
            }
            _usuarioRepository.Atualizar(usuario);

            return Ok(usuario);
        }

        // Deletar Usuário
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var usuario = _usuarioRepository.ObterUsuario(id);
            if (usuario is null)
            {
                return NotFound("Usuário não localizado");
            }
            usuario.IdUsu = id;
            _usuarioRepository.Excluir(id);

            return Ok(usuario);
        }
    }

}

