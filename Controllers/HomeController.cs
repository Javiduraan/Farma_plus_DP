using Farma_plus.Interfaces;
using Farma_plus.Models;
using Farma_plus.Models.Reports;
using Farma_plus.Repositories;
using iTextSharp.text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using iTextSharp.text.pdf;

namespace Farma_plus.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork,
                              ILogger<HomeController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }


        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BuscaFarmacias(string vMedicamento)
        {
            var Farmas = await _unitOfWork.SurtidoFarmas.GetFarmacias(vMedicamento);

            if (Farmas != null)
            {
                return Json(Farmas);
            }
            return Json(new { desc = "Sin Farmacias" });
        }

        [HttpPost]
        public async Task<IActionResult> BuscaRecetaPaciente(int numeroPensiones, int parentesco, int folioReceta)
        {
            var surtidoFarma = await _unitOfWork.SurtidoFarmas.GetSurtidoFarma(numeroPensiones, parentesco, folioReceta);

            if (surtidoFarma != null)
            {
                return Json(surtidoFarma);
            }
            return Json(new { desc = "Receta no encontrada" });
        }

        [HttpPost]
        public async Task<IActionResult> GuardaSurtidoRecetaAlterna([FromBody]List<SurtidoFarmaAlterno> surtidos)
        {
            bool falloInsert = false;
            foreach (var surtido in surtidos)
            {
                var resultadoInsert = await _unitOfWork.SurtidoFarmas.AddAsync(surtido);
                if (resultadoInsert <= 0)
                {
                    falloInsert = true;
                }
            }
            if (!falloInsert)
            {
                return Ok();
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> PruebaPdf(int folioReceta)
        {
            var lstpdf = new List<byte[]>();
            MemoryStream workStream = new MemoryStream();

            var detalleVale = await _unitOfWork.SurtidoFarmas.GetDetalleVale(folioReceta);
            foreach (var item in detalleVale)
            {
                Farma_plus.Models.Reports.Header header = new Farma_plus.Models.Reports.Header()
                {
                    NombreProveedor = item.NombreFarmacia,
                    DireccionProveedor = item.DomicilioFarmacia,
                    NumeroAfiliacion = item.NumPensiones,
                    Parentesco = item.Parentesco.ToString(),
                    ClaveMedico = item.ClaveMedico,
                    FolioReceta = item.FolioReceta,
                    NombreDH = item.NombreDh.ToString()
                };

                Farma_plus.Models.Reports.Footer footer = new Farma_plus.Models.Reports.Footer()
                {
                    Fecha = DateTime.Now.ToString()
                };

                lstpdf.Add(await new Rotativa.AspNetCore.ViewAsPdf("ValeSubrogacion", item)
                {
                    PageSize = Rotativa.AspNetCore.Options.Size.Letter,
                    PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                    PageMargins = new Rotativa.AspNetCore.Options.Margins(48, 5, 53, 5),
                    CustomSwitches = @"--header-html """ + Url.Action("Header", "Home", header, HttpContext.Request.Scheme).TrimEnd('/') + @""" " +
                                     @"--header-spacing 1 " +
                                     @"--footer-html """ + Url.Action("Footer", "Home", footer, HttpContext.Request.Scheme).TrimEnd('/') + @""" " +
                                     @"--footer-spacing 0"
                }.BuildFile(ControllerContext));
            }
            var pdf = CombineMultiplePDFs(lstpdf);
            return File(pdf, "application/pdf");
        }

        [HttpGet]
        public async Task<FileResult> CreatePdf(int folioReceta)
        {
            var lstpdf = new List<byte[]>();
            MemoryStream workStream = new MemoryStream();

            var detalleVale = await _unitOfWork.SurtidoFarmas.GetDetalleVale(folioReceta);

            var grupos = detalleVale.Where(x => x.ValeSubrogado.Contains("S"))
                                    .Select(a => new ValeSubrogadoDto {
                                        Cantidad = a.Cantidad,
                                        ClaveMedicamento = a.ClaveMedicamento,
                                        ClaveMedico = a.ClaveMedico,
                                        DescArticulo = a.DescArticulo,
                                        DomicilioFarmacia = a.DomicilioFarmacia,
                                        FechaHoraSurtido = a.FechaHoraSurtido,
                                        FolioReceta = a.FolioReceta,
                                        NombreDh = a.NombreDh,
                                        NombreFarmacia = a.NombreFarmacia == null ? "Pendiente" : a.NombreFarmacia,
                                        NuevoResurtido = a.NuevoResurtido,
                                        NumPensiones = a.NumPensiones,
                                        Parentesco = a.Parentesco,
                                        ValeSubrogado = a.ValeSubrogado
                                    }).ToList()
                                    .GroupBy(x => x.NombreFarmacia)
                                    .ToList();

            foreach (var grupo in grupos)
            {
                //foreach (var item in grupo)
                //{
                var item = grupo.First();
                    Farma_plus.Models.Reports.Header header = new Farma_plus.Models.Reports.Header()
                    {
                        NombreProveedor = item.NombreFarmacia,
                        DireccionProveedor = item.DomicilioFarmacia,
                        NumeroAfiliacion = item.NumPensiones,
                        Parentesco = item.Parentesco.ToString(),
                        ClaveMedico = item.ClaveMedico,
                        FolioReceta = item.FolioReceta,
                        NombreDH = item.NombreDh.ToString()
                    };

                    Farma_plus.Models.Reports.Footer footer = new Farma_plus.Models.Reports.Footer()
                    {
                        Fecha = DateTime.Now.ToString()
                    };

                    lstpdf.Add(await new Rotativa.AspNetCore.ViewAsPdf("ValeSubrogacion", grupo)///item)
                    {
                        PageSize = Rotativa.AspNetCore.Options.Size.Letter,
                        PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                        PageMargins = new Rotativa.AspNetCore.Options.Margins(48, 5, 53, 5),
                        CustomSwitches = @"--header-html """ + Url.Action("Header", "Home", header, HttpContext.Request.Scheme).TrimEnd('/') + @""" " +
                                         @"--header-spacing 1 " +
                                         @"--footer-html """ + Url.Action("Footer", "Home", footer, HttpContext.Request.Scheme).TrimEnd('/') + @""" " +
                                         @"--footer-spacing 0"
                    }.BuildFile(ControllerContext));
                //}
            }

            //foreach (var item in detalleVale)
            //{
            //    Farma_plus.Models.Reports.Header header = new Farma_plus.Models.Reports.Header()
            //    {
            //        NombreProveedor = item.NombreFarmacia,
            //        DireccionProveedor = item.DomicilioFarmacia,
            //        NumeroAfiliacion = item.NumPensiones,
            //        Parentesco = item.Parentesco.ToString(),
            //        ClaveMedico = item.ClaveMedico,
            //        FolioReceta = item.FolioReceta,
            //        NombreDH = item.NombreDh.ToString()
            //    };

            //    Farma_plus.Models.Reports.Footer footer = new Farma_plus.Models.Reports.Footer()
            //    {
            //        Fecha = DateTime.Now.ToString()
            //    };

            //    lstpdf.Add(await new Rotativa.AspNetCore.ViewAsPdf("ValeSubrogacion", item)
            //    {
            //        PageSize = Rotativa.AspNetCore.Options.Size.Letter,
            //        PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
            //        PageMargins = new Rotativa.AspNetCore.Options.Margins(48, 5, 53, 5),
            //        CustomSwitches = @"--header-html """ + Url.Action("Header", "Home", header, HttpContext.Request.Scheme).TrimEnd('/') + @""" " +
            //                         @"--header-spacing 1 " +
            //                         @"--footer-html """ + Url.Action("Footer", "Home", footer, HttpContext.Request.Scheme).TrimEnd('/') + @""" " +
            //                         @"--footer-spacing 0"
            //    }.BuildFile(ControllerContext));
            //}
            var pdf = CombineMultiplePDFs(lstpdf);
            return File(pdf, "application/pdf");
        }

        public static byte[] CombineMultiplePDFs(List<byte[]> pdfs)
        {
            // step 1: creation of a document-object
            Document document = new Document();

            // create new MemoryStream object which will be disposed at the end
            using (MemoryStream newFileStream = new MemoryStream())
            {
                // step 2: we create a writer that listens to the document
                PdfCopy writer = new PdfCopy(document, newFileStream);

                // step 3: we open the document
                document.Open();

                foreach (var pdfbytes in pdfs)
                {
                    // step 4: we create a reader for the PDF
                    using (MemoryStream pdfStream = new MemoryStream(pdfbytes))
                    {
                        PdfReader reader = new PdfReader(pdfStream);
                        // loop over the pages in that document
                        int pageCount = reader.NumberOfPages;
                        for (int i = 0; i < pageCount; i++)
                        {
                            // step 5: we retrieve the page
                            PdfImportedPage page = writer.GetImportedPage(reader, i + 1);
                            // step 6: we add the page to the document
                            writer.AddPage(page);
                        }
                        reader.Close();
                    }
                }

                // step 7: we close the document and writer
                writer.Close();
                document.Close();

                return newFileStream.ToArray();
            } // disposes the newFileStream object
        }

        [HttpPost]
        public async Task<IActionResult> BuscaMedicamento(string vMedicamento)
        {
            var Farmas = await _unitOfWork.SurtidoFarmas.GetMedicamentos(vMedicamento);

            if (Farmas != null)
            {
                return Json(Farmas);
            }
            return Json(new { desc = "No existe medicamento" });
        }
        public IActionResult Header(Farma_plus.Models.Reports.Header header)
        {
            return View(header);
        }

        public IActionResult Footer(Footer footer)
        {
            return View(footer);
        }
    }
}
