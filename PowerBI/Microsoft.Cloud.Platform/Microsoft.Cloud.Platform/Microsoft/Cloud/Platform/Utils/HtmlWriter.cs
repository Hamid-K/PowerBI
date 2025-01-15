using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000222 RID: 546
	public sealed class HtmlWriter
	{
		// Token: 0x06000E69 RID: 3689 RVA: 0x00032AB0 File Offset: 0x00030CB0
		public static void WriteFormattedTable(TextWriter stream, string pageTitle, string pageDescription, IEnumerable<string> columns, IEnumerable<IEnumerable<string>> rows)
		{
			stream.WriteLine("<html><head><title>{0}</title>{1}</head>", pageTitle, HtmlWriter.GetHtmlStyleSheet());
			stream.WriteLine("<body>");
			stream.WriteLine("<h1>{0}</h1>", pageTitle);
			stream.WriteLine("<p>Created at: {0} on machine {1}</p>", DateTime.UtcNow, Environment.MachineName);
			stream.WriteLine("<p><strong>{0}</strong></p>", pageDescription);
			stream.WriteLine("<table>");
			stream.WriteLine("<tr>");
			foreach (string text in columns)
			{
				stream.WriteLine("<th>{0}</th>", text);
			}
			int num = columns.Count<string>();
			foreach (IEnumerable<string> enumerable in rows)
			{
				stream.WriteLine("<tr>");
				foreach (string text2 in enumerable.Take(num))
				{
					stream.WriteLine("<td>{0}</td>", string.IsNullOrEmpty(text2) ? "&nbsp;" : HttpUtility.HtmlEncode(text2.Trim(new char[] { '"' })));
				}
				stream.WriteLine("</tr>");
			}
			stream.WriteLine("</table>");
			stream.WriteLine("</body></html>");
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x00032C2C File Offset: 0x00030E2C
		public static string GetHtmlStyleSheet()
		{
			return "<style type=\"text/css\">\r\n                body\r\n                {\r\n\t                font-family: Calibri, Tahoma, Arial;\r\n                }\r\n\r\n                pre, code\r\n                {\r\n\t                width: 100%;\r\n\t                padding-top: 5px;\r\n\t                padding-bottom: 5px;\r\n\t                padding-left: 5px;\r\n\t                padding-right: 5px;\r\n\t                margin-left: 20px;\r\n\t                margin-right: 20px;\r\n                }\r\n\r\n                pre, code\r\n                {\r\n\t                background-color: Silver;\r\n\t                font-family: Consolas, Courier New;\r\n                }\r\n\r\n                pre\r\n                {\r\n\t                border: 1px black solid;\r\n\t                background-color: Gray;\r\n                }\r\n\r\n                .note\r\n                {\r\n\t                background-color: Yellow;\r\n                }\r\n\r\n                li\r\n                {\r\n\t                margin-left: 25px;\r\n                }\r\n\r\n                .host\r\n                {\r\n\t                color: Green;\r\n                }\r\n\r\n                .desc\r\n                {\r\n\t                font-style: italic;\r\n                }\r\n\r\n                .restart\r\n                {\r\n\t                color: Red;\r\n                }\r\n\r\n                table\r\n                {\r\n\t                border-collapse: collapse;\r\n\t                border: 1px black solid;\r\n                }\r\n\r\n                tr,td,th\r\n                {\r\n\t                border: 1px silver solid;\r\n\t                vertical-align: top;\r\n\t                padding: 5px;\r\n                }\r\n\r\n                th\r\n                {\r\n\t                padding-top: 10px;\r\n\t                padding-bottom: 10px;\r\n\t                background-color: Gray;\r\n\t                color: White;\r\n\t                border-bottom: 1px black solid;\r\n                }\r\n\r\n                .props\r\n                {\r\n\t                padding: 0px;\r\n\t                border: 1px black solid;\r\n                }\r\n\r\n                table.props\r\n                {\r\n\t                width: 100%;\r\n                }\r\n\r\n                th.props,td.props\r\n                {\r\n\t                padding-left: 3px;\r\n\t                padding-right: 3px;\r\n\t                vertical-align: middle;\r\n\t                border: 1px black solid;\r\n                }\r\n\r\n                th.props\r\n                {\r\n\t                background-color: Silver;\r\n\t                color: Black;\r\n\t                font-weight: bold;\r\n\t                text-align: left;\r\n\t                font-weight: normal;\r\n\t                width: 5%;\r\n                }\r\n\r\n                .temp\r\n                {\r\n\t                color: Green;\r\n                }\r\n\r\n                .id\r\n                {\r\n\t                font-family: Consolas, Courier New;\r\n\t                font-size: smaller;\r\n\t                font-weight: bold;\r\n                }\r\n\r\n                pre.stderr, pre.stdout, pre.exception\r\n                {\r\n                    background-color: Black;\r\n                    color: white;\r\n                }\r\n\r\n                pre.stderr\r\n                {\r\n                    color: #FF0000;\r\n                }\r\n\r\n                pre.exception\r\n                {\r\n                    color: #00FF00;\r\n                }\r\n\r\n                .issues\r\n                {\r\n                    color: #7d4320;\r\n                }\r\n\r\n                .fail\r\n                {\r\n                    color: Red;\r\n                }\r\n\r\n                .success\r\n                {\r\n                    color: Green;\r\n                }\r\n\r\n                .executing\r\n                {\r\n                    font-weight: bold;\r\n                }\r\n                </style>";
		}
	}
}
