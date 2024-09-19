using Aplicacion.ManejadorErrores;
using MediatR;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Aplicacion.Cursos
{
    public class Eliminar
    {
        public class Ejecuta : IRequest
        {
            public int Id { get; set; }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {

            private readonly CursosOnlineContext context;
            public Manejador(CursosOnlineContext _context)
            {
                context = _context;
            }
            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var curso = await context.Curso.FindAsync(request.Id);
                if(curso == null)
                {
                    //throw new Exception("No se puede eliminar el curso");
                    throw new ManejadorExepcion(HttpStatusCode.NotFound, new { mensaje  = "No se encontro el curso" });
                }
                context.Remove(curso);
                var resultado = await context.SaveChangesAsync();
                if(resultado > 0)
                {
                    return Unit.Value;
                }
                //throw new Exception("No se pudieron guardar los cambios");
                throw new ManejadorExepcion(HttpStatusCode.BadRequest, new { mensaje = "No se pudieron guardar los cambios" });
            }
        }


    }
}
