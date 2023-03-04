using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using   L01_2020AA603.Models;
using Microsoft.EntityFrameworkCore;

namespace L01_2020AA603.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuariosController : ControllerBase
    {
        private readonly blogDBContext _blogDBContext;

        public usuariosController(blogDBContext usuariosContexto)
        {
            _blogDBContext = usuariosContexto;
        }
        // metodo mostrar todo
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<usuarios> listadousuarios = (from e in _blogDBContext.usuarios
                                           select e).ToList();
            if (listadousuarios.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadousuarios);
        }
        // metodo guardar nuevo registro
        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarEquipo([FromBody] usuarios usuario)
        {
            try
            {

                _blogDBContext.usuarios.Add(usuario);
                _blogDBContext.SaveChanges();
                return Ok(usuario);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        // metodo actualizar
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult actualizarequipo(int id, [FromBody] usuarios usuarioModificar)
        {
            usuarios? usuario = (from e in _blogDBContext.usuarios
                               where e.usuarioId == id
                               select e).FirstOrDefault();
            if (usuario == null) return NotFound();

            usuario.rolId = usuarioModificar.rolId;
            usuario.nombreUsuario = usuarioModificar.nombreUsuario;
            usuario.clave = usuarioModificar.clave;
            usuario.nombre = usuarioModificar.nombre;
            usuario.apellido = usuarioModificar.apellido;


            _blogDBContext.Entry(usuario).State = EntityState.Modified;
            _blogDBContext.SaveChanges();

            return Ok(usuarioModificar);




        }
        // metodo eliminar
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarEquipos(int id)
        {
            usuarios? usuario = (from e in _blogDBContext.usuarios
                               where e.usuarioId == id
                               select e).FirstOrDefault();
            if (usuario == null) return NotFound();

            return Ok(usuario);
            _blogDBContext.usuarios.Attach(usuario);
            _blogDBContext.usuarios.Remove(usuario);
            _blogDBContext.SaveChanges();
            return Ok(usuario);
        }
        // metodo mostrar usuarios por nombre y apelldio
        [HttpGet]
        [Route("find/{nombre}/{apellido}")]
        public IActionResult usuariosnom(string nombre , string apellido)
        {
            List<usuarios> usuario = (from e in _blogDBContext.usuarios
                                              where e.nombre.Contains(nombre) && e.apellido.Contains(apellido)
                                              select e).ToList();
            if (usuario == null) return NotFound();

            return Ok(usuario);
        }
        [HttpGet]
        [Route("find/{rol}")]
        public IActionResult usuariosrol(int rol)
        {
            List<usuarios> usuario = (from e in _blogDBContext.usuarios
                                              where e.rolId==rol
                                              select e).ToList();
            if (usuario == null) return NotFound();

            return Ok(usuario);
        }



    }
}
