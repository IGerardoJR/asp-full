using Aplicacion.ManejadorErrores;
using Dominio;
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
    public class ConsultaId
    {
        public class CursoUnico: IRequest<Curso>
        {
            public int Id { get; set; }
        }

        public class Manejador : IRequestHandler<CursoUnico, Curso>
        {
            private readonly CursosOnlineContext context;
            public Manejador(CursosOnlineContext _context)
            {
                context = _context;
            }

            public async Task<Curso> Handle(CursoUnico request, CancellationToken cancellationToken)
            {
                var curso = await context.Curso.FindAsync(request.Id);
                if(curso == null)
                {
                    throw new ManejadorExepcion(HttpStatusCode.NotFound, new { mensaje = "El curso no existe" });
                }
                return curso;
            }
        }
    }
}
