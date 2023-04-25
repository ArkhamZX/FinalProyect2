using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalProyect2.Data;

namespace FinalProyect2
{
    public partial class ExportJoseController : ExportController
    {
        private readonly JoseContext context;
        private readonly JoseService service;
        public ExportJoseController(JoseContext context, JoseService service)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet("/export/Jose/clientes/csv")]
        [HttpGet("/export/Jose/clientes/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportClientesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetClientes(), Request.Query), fileName);
        }

        [HttpGet("/export/Jose/clientes/excel")]
        [HttpGet("/export/Jose/clientes/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportClientesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetClientes(), Request.Query), fileName);
        }
        [HttpGet("/export/Jose/cuentabancos/csv")]
        [HttpGet("/export/Jose/cuentabancos/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportCuentaBancosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCuentaBancos(), Request.Query), fileName);
        }

        [HttpGet("/export/Jose/cuentabancos/excel")]
        [HttpGet("/export/Jose/cuentabancos/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportCuentaBancosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCuentaBancos(), Request.Query), fileName);
        }
        [HttpGet("/export/Jose/cuotainversions/csv")]
        [HttpGet("/export/Jose/cuotainversions/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportCuotaInversionsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCuotaInversions(), Request.Query), fileName);
        }

        [HttpGet("/export/Jose/cuotainversions/excel")]
        [HttpGet("/export/Jose/cuotainversions/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportCuotaInversionsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCuotaInversions(), Request.Query), fileName);
        }
        [HttpGet("/export/Jose/cuotaprestamos/csv")]
        [HttpGet("/export/Jose/cuotaprestamos/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportCuotaPrestamosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetCuotaPrestamos(), Request.Query), fileName);
        }

        [HttpGet("/export/Jose/cuotaprestamos/excel")]
        [HttpGet("/export/Jose/cuotaprestamos/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportCuotaPrestamosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetCuotaPrestamos(), Request.Query), fileName);
        }
        [HttpGet("/export/Jose/garantia/csv")]
        [HttpGet("/export/Jose/garantia/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportGarantiaToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetGarantia(), Request.Query), fileName);
        }

        [HttpGet("/export/Jose/garantia/excel")]
        [HttpGet("/export/Jose/garantia/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportGarantiaToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetGarantia(), Request.Query), fileName);
        }
        [HttpGet("/export/Jose/inversiones/csv")]
        [HttpGet("/export/Jose/inversiones/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportInversionesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetInversiones(), Request.Query), fileName);
        }

        [HttpGet("/export/Jose/inversiones/excel")]
        [HttpGet("/export/Jose/inversiones/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportInversionesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetInversiones(), Request.Query), fileName);
        }
        [HttpGet("/export/Jose/prestamos/csv")]
        [HttpGet("/export/Jose/prestamos/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportPrestamosToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetPrestamos(), Request.Query), fileName);
        }

        [HttpGet("/export/Jose/prestamos/excel")]
        [HttpGet("/export/Jose/prestamos/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportPrestamosToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetPrestamos(), Request.Query), fileName);
        }

        [HttpGet("/export/Jose/spgetcacularcuota/csv")]
        [HttpGet("/export/Jose/spgetcacularcuota/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportSpGetCacularCuotaToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetSpGetCacularCuota(), Request.Query), fileName);
        }

        [HttpGet("/export/Jose/spgetcacularcuota/excel")]
        [HttpGet("/export/Jose/spgetcacularcuota/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportSpGetCacularCuotaToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetSpGetCacularCuota(), Request.Query), fileName);
        }
    }
}
