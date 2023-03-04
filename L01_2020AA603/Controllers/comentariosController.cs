using L01_2020AA603.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2020AA603.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class comentariosController : ControllerBase
    {
        private readonly blogDBContext _blogDBContext;

        public comentariosController(blogDBContext comentariosContexto)
        {
            _blogDBContext = comentariosContexto;
        }
        // metodo mostrar todo
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<comentarios> listadocomentarios = (from e in _blogDBContext.comentarios
                                                        select e).ToList();
            if (listadocomentarios.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadocomentarios);
        }
        // metodo guardar nuevo registro
        [HttpPost]
        [Route("Add")]
        public IActionResult Guardarcomentario([FromBody] comentarios come)
        {
            try
            {

                _blogDBContext.comentarios.Add(come);
                _blogDBContext.SaveChanges();
                return Ok(come);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        // metodo actualizar
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult actualizarcomentario(int id, [FromBody] comentarios comentariosModificar)
        {
            comentarios? comi = (from e in _blogDBContext.comentarios
                                    where e.publicacionId == id
                                    select e).FirstOrDefault();
            if (comi == null) return NotFound();

            comi.publicacionId= comentariosModificar.publicacionId;
            comi.comentario = comentariosModificar.comentario;
            comi.usuarioId = comentariosModificar.usuarioId;


            _blogDBContext.Entry(comi).State = EntityState.Modified;
            _blogDBContext.SaveChanges();

            return Ok(comentariosModificar);




        }
        // metodo eliminar
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult Eliminarcomentarios(int id)
        {
            comentarios? comi = (from e in _blogDBContext.comentarios
                                 where e.publicacionId == id
                                    select e).FirstOrDefault();
            if (comi == null) return NotFound();

           
            _blogDBContext.comentarios.Attach(comi);
            _blogDBContext.comentarios.Remove(comi);
            _blogDBContext.SaveChanges();
            return Ok(comi);
        }
        // metodo mostrar listado de comentarios
        [HttpGet]
        [Route("find/{publicacionId}")]
        public IActionResult usuariosRol(int publicacionId)
        {
            List<comentarios> comi = (from e in _blogDBContext.comentarios
                                         where e.publicacionId == publicacionId
                                         select e).ToList();
            if (comi == null) return NotFound();

            return Ok(comi);
        }
    }
}
