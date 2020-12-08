using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

using Microsoft.Extensions.Configuration;

using OfficeEcclesial.App.Database.Entities;
using OfficeEcclesial.App.Database.Enums;
using OfficeEcclesial.Database.Entities;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace OfficeEcclesial.App.Services
{
    public sealed class PdfService
    {
        #region Properties
        private IConfiguration Configuration { get; }
        #endregion

        #region Constructor
        public PdfService(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        #endregion

        public void GenerateCaritasMembersList(IReadOnlyList<CaritasMember> list, string path)
        {
            using (var fileStream = File.Open(path, FileMode.Create))
            {
                var pdfDoc = new PdfDocument(new PdfWriter(fileStream));
                var document = new Document(pdfDoc);

                document.Add(CreateHeader("Miembros de Cáritas"));

                document.Add(GetDateParagraph());

                document.Add(GenerateSimpleTable(list));

                document.Add(new Paragraph("\n"));

                document.Add(CreateCopyrightFooter());

                document.Close();
                fileStream.Close();
            }
        }
        public void GenerateCaritasProjectsList(IEnumerable<CaritasProject> elements, string path)
        {
            using (var fileStream = File.Open(path, FileMode.Create))
            {
                var pdfDoc = new PdfDocument(new PdfWriter(fileStream));
                var document = new Document(pdfDoc);

                var bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);
                var font = PdfFontFactory.CreateFont(StandardFonts.TIMES_ROMAN);

                document.Add(CreateHeader("Proyectos de Cáritas"));

                document.Add(GetDateParagraph());

                foreach (var project in elements)
                {
                    var projectTitle = new Text($"Nombre del proyecto: {project.Name}")
                        .SetFont(bold)
                        .SetFontSize(14f)
                        .SetTextAlignment(TextAlignment.CENTER);

                    var representant = new Text($"Información del representante")
                        .SetFont(bold)
                        .SetFontSize(12f)
                        .SetTextAlignment(TextAlignment.CENTER);

                    var name = new Text("Nombre: ")
                        .SetFont(bold);

                    var lastName = new Text("Apellidos: ")
                        .SetFont(bold);

                    var phone = new Text("Télefono: ")
                        .SetFont(bold);

                    var cellular = new Text("Celular: ")
                        .SetFont(bold);

                    var address = new Text("Dirección: ")
                        .SetFont(bold);

                    var projectHeader = new Paragraph()
                        .SetFont(font)
                        .Add(Environment.NewLine)
                        .Add(projectTitle)
                        .Add(Environment.NewLine)
                        .Add(Environment.NewLine)
                        .Add(representant)
                        .Add(Environment.NewLine)
                        .Add(name)
                        .Add(project.Representant.Name)
                        .Add(new Tab())
                        .Add(lastName)
                        .Add(project.Representant.LastName)
                        .Add(Environment.NewLine);

                    if (!string.IsNullOrEmpty(project.Representant.Phone))
                    {
                        projectHeader.Add(phone)
                            .Add(project.Representant.Phone)
                            .Add(new Tab());
                    }

                    if (!string.IsNullOrEmpty(project.Representant.Cellular))
                    {
                        projectHeader.Add(cellular)
                            .Add(project.Representant.Cellular)
                            .Add(new Tab());
                    }

                    if (!string.IsNullOrEmpty(project.Representant.Phone) || !string.IsNullOrEmpty(project.Representant.Cellular))
                    {
                        projectHeader.Add(Environment.NewLine);
                    }

                    projectHeader.Add(new Text("Edad: ").SetFont(bold))
                        .Add(project.Representant.Age.ToString())
                        .Add(new Tab())
                        .Add(new Text("Sexo: ").SetFont(bold))
                        .Add(project.Representant.Genre.ToString())
                        .Add(Environment.NewLine);

                    if (!string.IsNullOrEmpty(project.Representant.Address))
                    {
                        projectHeader.Add(new Text("Dirección: ").SetFont(bold))
                            .Add(project.Representant.Address)
                            .Add(Environment.NewLine);
                    }

                    document.Add(projectHeader);

                    var beneficiariesTable = GenerateCaritasBeneficiariesTable(project.Beneficiaries.OrderBy(x => x.Name).ToList());

                    var tableParagraph = new Paragraph()
                        .Add(new Text("Beneficiarios: ").SetFont(bold))
                        .Add(Environment.NewLine)
                        .Add(beneficiariesTable)
                        .Add(Environment.NewLine);

                    document.Add(tableParagraph);
                }

                document.Add(CreateCopyrightFooter());

                document.Close();
                fileStream.Close();
            }
        }

        internal void GeneratePastoralPenitenciariaBeneficiariesList(ObservableCollection<PastoralPenitenciariaBeneficiaries> elementsCollection, string fileName)
        {
            throw new NotImplementedException();
        }

        public void GeneratePastoralPenitenciariaMembersList(IReadOnlyList<PastoralPenitenciariaMembers> elementsCollection, string filePath)
        {
            using (var fileStream = File.Open(filePath, FileMode.Create))
            {
                var pdfDoc = new PdfDocument(new PdfWriter(fileStream));
                var document = new Document(pdfDoc);

                document.Add(CreateHeader("Miembros de la Pastoral Penitenciaria"));

                document.Add(GetDateParagraph());

                document.Add(GenerateSimpleTable(elementsCollection));

                document.Add(new Paragraph(Environment.NewLine));

                document.Add(CreateCopyrightFooter());

                document.Close();
                fileStream.Close();
            }
        }
        public void GenerateLiturgiaCantoresList(IReadOnlyList<LiturgiaCantores> elementsCollection, string filePath)
        {
            using (var fileStream = File.Open(filePath, FileMode.Create))
            {
                var pdfDoc = new PdfDocument(new PdfWriter(fileStream));
                var document = new Document(pdfDoc);

                document.Add(CreateHeader("Listado de Cantores"));

                document.Add(GetDateParagraph());

                document.Add(GenerateSimpleTable(elementsCollection));

                document.Add(new Paragraph(Environment.NewLine));

                document.Add(CreateCopyrightFooter());

                document.Close();
                fileStream.Close();
            }
        }
        public void GenerateLiturgiaLectoresList(IReadOnlyList<LiturgiaLectores> elementsCollection, string filePath)
        {
            using (var fileStream = File.Open(filePath, FileMode.Create))
            {
                var pdfDoc = new PdfDocument(new PdfWriter(fileStream));
                var document = new Document(pdfDoc);

                document.Add(CreateHeader("Listado de Lectores"));

                document.Add(GetDateParagraph());

                document.Add(GenerateSimpleTable(elementsCollection));

                document.Add(new Paragraph(Environment.NewLine));

                document.Add(CreateCopyrightFooter());

                document.Close();

                fileStream.Close();
            }
        }
        public void GenerateLiturgiaMembersList(IReadOnlyList<LiturgiaMembers> elementsCollection, string filePath)
        {
            using (var fileStream = File.Open(filePath, FileMode.Create))
            {
                var pdfDoc = new PdfDocument(new PdfWriter(fileStream));
                var document = new Document(pdfDoc);

                document.Add(CreateHeader("Miembros del grupo Liturgia"));

                document.Add(GetDateParagraph());

                document.Add(GenerateSimpleTable(elementsCollection));

                document.Add(new Paragraph(Environment.NewLine));

                document.Add(CreateCopyrightFooter());

                document.Close();
                fileStream.Close();
            }
        }
        public void GenerateLiturgiaMonaguillosList(IReadOnlyList<LiturgiaMonaguillos> elementsCollection, string filePath)
        {
            using (var fileStream = File.Open(filePath, FileMode.Create))
            {
                var pdfDoc = new PdfDocument(new PdfWriter(fileStream));
                var document = new Document(pdfDoc);

                document.Add(CreateHeader("Listado de Monaguillos"));

                document.Add(GetDateParagraph());

                document.Add(GenerateSimpleTable(elementsCollection));

                document.Add(new Paragraph("\n"));

                document.Add(CreateCopyrightFooter());

                document.Close();
                fileStream.Close();
            }
        }
        public void GenerateConsejoParroquialMembersList(IReadOnlyList<ConsejoParroquialMembers> elementsCollection, string filePath)
        {
            using (var fileStream = File.Open(filePath, FileMode.Create))
            {
                var pdfDoc = new PdfDocument(new PdfWriter(fileStream));
                var document = new Document(pdfDoc);

                document.Add(CreateHeader("Miembros del Consejo Parroquial"));

                document.Add(GetDateParagraph());

                document.Add(GenerateSimpleTable(elementsCollection));

                document.Add(new Paragraph("\n"));

                document.Add(CreateCopyrightFooter());

                document.Close();
                fileStream.Close();
            }
        }
        public void GenerateEmausMembersList(IReadOnlyList<EmausMembers> elementsCollection, string filePath)
        {
            using (var fileStream = File.Open(filePath, FileMode.Create))
            {
                var pdfDoc = new PdfDocument(new PdfWriter(fileStream));
                var document = new Document(pdfDoc);

                var bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);

                document.Add(CreateHeader("Miembros del grupo Emaus"));

                document.Add(GetDateParagraph());

                document.Add(GenerateSimpleTable(elementsCollection));

                document.Add(new Paragraph("\n"));

                document.Add(CreateCopyrightFooter());
                
                document.Close();
                fileStream.Close();
            }
        }
        public void GenerateCatequesisCatequistasList(IReadOnlyList<CatequesisCatequista> elementsCollection, string filePath)
        {
            using (var fileStream = File.Open(filePath, FileMode.Create))
            {
                var pdfDoc = new PdfDocument(new PdfWriter(fileStream));
                var document = new Document(pdfDoc);

                document.Add(CreateHeader("Listado de Catequistas"));

                document.Add(GetDateParagraph());

                document.Add(GenerateSimpleTable(elementsCollection));

                document.Add(new Paragraph("\n"));

                document.Add(CreateCopyrightFooter());

                document.Close();
                fileStream.Close();
            }
        }
        public void GenerateCatequistasAdultosList(IReadOnlyList<CatequistaAdultos> elementsCollection, string filePath)
        {
            using (var fileStream = File.Open(filePath, FileMode.Create))
            {
                var pdfDoc = new PdfDocument(new PdfWriter(fileStream));
                var document = new Document(pdfDoc);

                document.Add(CreateHeader("Listado de Catequistas de Adultos"));

                document.Add(GetDateParagraph());

                document.Add(GenerateSimpleTable(elementsCollection));

                document.Add(new Paragraph("\n"));

                document.Add(CreateCopyrightFooter());

                document.Close();
                fileStream.Close();
            }
        }
        private Table GenerateCaritasBeneficiariesTable(IList<Person> beneficiaries)
        {
            var table = new Table(UnitValue.CreatePercentArray(new[] { 5f, 18f, 18f, 7f, 7f, 10f, 12f, 23f })).UseAllAvailableWidth();

            table.AddHeaderCell(CreateTableHeader("No."));
            table.AddHeaderCell(CreateTableHeader("Nombre"));
            table.AddHeaderCell(CreateTableHeader("Apellidos"));
            table.AddHeaderCell(CreateTableHeader("Edad"));
            table.AddHeaderCell(CreateTableHeader("Sexo"));
            table.AddHeaderCell(CreateTableHeader("Teléfono"));
            table.AddHeaderCell(CreateTableHeader("Celular"));
            table.AddHeaderCell(CreateTableHeader("Dirección"));

            for (var i = 0; i < beneficiaries.Count; i++)
            {
                var person = beneficiaries[i];
                table.AddCell(CreateCell((i + 1).ToString(), true));
                table.AddCell(CreateCell(person.Name));
                table.AddCell(CreateCell(person.LastName));
                table.AddCell(CreateCell(person.Age.ToString(), true));
                table.AddCell(CreateCell(person.Genre == Genre.M ? "M" : "F", true));
                table.AddCell(CreateCell(person.Phone));
                table.AddCell(CreateCell(person.Cellular));
                table.AddCell(CreateCell(person.Address));
            }

            return table;
        }
        public void GenerateCatequesisMembersList(ObservableCollection<CatequesisMember> elementsCollection, string fileName)
        {
            throw new NotImplementedException();
        }

        private Table GenerateSimpleTable(IReadOnlyList<IPerson> elementsCollection)
        {
            var table = new Table(UnitValue.CreatePercentArray(new[] { 4f, 18f, 18f, 7f, 7f, 10f, 10f, 26f }))
                .UseAllAvailableWidth();

            table.AddHeaderCell(CreateTableHeader("No."));
            table.AddHeaderCell(CreateTableHeader("Nombre"));
            table.AddHeaderCell(CreateTableHeader("Apellidos"));
            table.AddHeaderCell(CreateTableHeader("Edad"));
            table.AddHeaderCell(CreateTableHeader("Sexo"));
            table.AddHeaderCell(CreateTableHeader("Teléfono"));
            table.AddHeaderCell(CreateTableHeader("Celular"));
            table.AddHeaderCell(CreateTableHeader("Dirección"));

            for (var i = 0; i < elementsCollection.Count; i++)
            {
                var person = elementsCollection[i];
                table.AddCell(CreateCell((i + 1).ToString(), true));
                table.AddCell(CreateCell(person.Name));
                table.AddCell(CreateCell(person.LastName));
                table.AddCell(CreateCell(person.Age.ToString(), true));
                table.AddCell(CreateCell(person.Genre == Genre.M ? "M" : "F", true));
                table.AddCell(CreateCell(person.Phone));
                table.AddCell(CreateCell(person.Cellular));
                table.AddCell(CreateCell(person.Address));
            }

            return table;
        }

        private Paragraph CreateHeader(string subtitle)
        {
            var bold = PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD);

            var header1 = new Text("Parroquia La Sagrada Familia De Contramaestre")
                .SetFont(bold)
                .SetFontSize(16f)
                .SetTextAlignment(TextAlignment.CENTER);

            var header2 = new Text(subtitle)
                .SetFont(bold)
                .SetFontSize(14f)
                .SetTextAlignment(TextAlignment.CENTER);

            var headerParagraph = new Paragraph()
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(header1)
                .Add(Environment.NewLine)
                .Add(header2)
                .Add(Environment.NewLine)
                .Add(Environment.NewLine);
            return headerParagraph;
        }
        private Paragraph CreateCopyrightFooter()
        {
            var link = new Link(" aquí", PdfAction.CreateURI(Configuration["Information:Download"]))
                .SetFontColor(ColorConstants.BLUE)
                .SetFontSize(8f);
            var bottom = new Paragraph()
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetFontSize(8f)
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                .Add("Creado con ")
                .Add(new Text("Office Eclesial\n").SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD)))
                .Add("Disponible para descarga")
                .Add(link);

            return bottom;

        }
        private Cell CreateTableHeader(string title)
        {
            var font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            var paragraph = new Paragraph(title)
                .SetFont(font)
                .SetFontSize(10f)
                .SetTextAlignment(TextAlignment.CENTER);

            var cell = new Cell()
                .SetMaxWidth(10)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .SetTextAlignment(TextAlignment.CENTER)
                .Add(paragraph);
            return cell;
        }

        private Paragraph GetDateParagraph()
        {
            var dateParagraph = new Paragraph();
            var dateSubtitle = new Text("Fecha: ")
                .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD));

            var valueDate = new Text(DateTime.Now.ToShortDateString());
            dateParagraph
                .Add(dateSubtitle)
                .Add(valueDate)
                .Add(Environment.NewLine);
            return dateParagraph;
        }
        private Cell CreateCell(string content, bool center = false)
        {
            var font = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            var paragraph = new Paragraph(content ?? string.Empty)
                .SetFont(font)
                .SetFontSize(10f);
            if (center)
                paragraph.SetTextAlignment(TextAlignment.CENTER);
            var cell = new Cell()
                .SetMaxWidth(10)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                .Add(paragraph);
            return cell;
        }


       
    }
}
