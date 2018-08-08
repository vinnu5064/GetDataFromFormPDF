using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
using iTextSharp.text.pdf;
using System.Data;
using Excel = Microsoft.Office.Interop.Excel;

namespace UIPathPDFCustomActivity
{
    public class ExtractionCustomActivity : CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> PDFFilePath { get; set; }

        [Category("Output")]
        public OutArgument<DataTable> Results { get; set; }

        [Category("Output")]
        public OutArgument<string> ErrorMessage { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            try
            {
                var pdfFilePath = PDFFilePath.Get(context);
                var pdfReader = new PdfReader(pdfFilePath);
                var fields = pdfReader.AcroFields.Fields;
               
                if (fields.Count > 0)
                {
                    DataTable dtResults = new DataTable();
                    dtResults.Columns.Add("FieldName", typeof(string));
                    dtResults.Columns.Add("FieldValue", typeof(string));

                    foreach (var item in fields.Keys)
                    {
                        var fieldkey = item.ToString();
                        var fieldValue = pdfReader.AcroFields.GetField(item.ToString());
                        dtResults.Rows.Add(fieldkey, fieldValue);
                    }
                    Results.Set(context, dtResults);
                }
                else
                {
                    var error = "An error occured while processing your request.";
                    ErrorMessage.Set(context, error);
                }
                
            }
            catch(Exception ex)
            {
                var error = "Error From Execute Method " +ex.Message;
                ErrorMessage.Set(context, error);
            }
        }
    }
}
