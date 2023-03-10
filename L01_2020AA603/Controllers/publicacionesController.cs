using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using L01_2020AA603.Models;
using Microsoft.EntityFrameworkCore;
namespace L01_2020AA603.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class publicacionesController : ControllerBase
    {
        private readonly blogDBContext _blogDBContext;

        public publicacionesController(blogDBContext publicacionesContexto)
        {
            _blogDBContext = publicacionesContexto;
        }
        // metodo mostrar todo
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<publicaciones> listadopublicaciones = (from e in _blogDBContext.publicaciones
                                              select e).ToList();
            if (listadopublicaciones.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadopublicaciones);
        }
        // metodo guardar nuevo registro
        [HttpPost]
        [Route("Add")]
        public IActionResult Guardarpubli([FromBody] publicaciones publi)
        {
            try
            {

                _blogDBContext.publicaciones.Add(publi);
                _blogDBContext.SaveChanges();
                return Ok(publi);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        // metodo actualizar
        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult actualizarpubli(int id, [FromBody] publicaciones publicacionesModificar)
        {
            publicaciones? publi = (from e in _blogDBContext.publicaciones
                                 where e.publicacionId == id
                                 select e).FirstOrDefault();
            if (publi == null) return NotFound();

            publi.titulo = publicacionesModificar.titulo;
            publi.descripcion= publicacionesModificar.descripcion;
            publi.usuarioId = publicacionesModificar.usuarioId;


            _blogDBContext.Entry(publi).State = EntityState.Modified;
            _blogDBContext.SaveChanges();

            return Ok(publicacionesModificar);




        }
        // metodo eliminar
        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult Eliminarpubli(int id)
        {
            publicaciones? publi = (from e in _blogDBContext.publicaciones
                                 where e.publicacionId == id
                                 select e).FirstOrDefault();
            if (publi == null) return NotFound();

            
            _blogDBContext.publicaciones.Attach(publi);
            _blogDBContext.publicaciones.Remove(publi);
            _blogDBContext.SaveChanges();
            return Ok(publi);
        }
        // metodo mostrar las publicaciones del usuario
      
        [HttpGet]
        [Route("find/{usuarioid}")]
        public IActionResult usuariosrol(int usuarioid)
        {
            List<publicaciones> publi = (from e in _blogDBContext.publicaciones
                                      where e.usuarioId == usuarioid
                                      select e).ToList();
            if (publi == null) return NotFound();

            return Ok(publi);
        }
    }
}
