using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Mashup.Engine1.Runtime;
using mshtml;

namespace Microsoft.Mashup.Engine1.Library.Html
{
	// Token: 0x02000ABE RID: 2750
	internal class HtmlDocumentReader
	{
		// Token: 0x06004CED RID: 19693 RVA: 0x000FD900 File Offset: 0x000FBB00
		public Stream Read(HtmlDocument htmlDocument)
		{
			HtmlElement htmlElement = htmlDocument.CreateElement("SCRIPT");
			htmlElement.Id = "dataExplorerScript";
			HtmlElement htmlElement2 = ((htmlDocument.GetElementsByTagName("HEAD").Count == 0) ? htmlDocument.CreateElement("HEAD") : htmlDocument.GetElementsByTagName("HEAD")[0]);
			((IHTMLScriptElement)htmlElement.DomElement).text = HtmlDocumentReader.javascript;
			htmlElement2.AppendChild(htmlElement);
			string text = (string)htmlDocument.InvokeScript("getDataExplorerDom");
			if (text == null)
			{
				throw ValueException.NewDataSourceError<Message0>(Strings.WebPageJavascriptDisabled, TextValue.New(htmlDocument.Url.AbsoluteUri), null);
			}
			if (text[0] == '0')
			{
				MemoryStream memoryStream = new MemoryStream();
				StreamWriter streamWriter = new StreamWriter(memoryStream);
				streamWriter.Write(text.ToCharArray(), 1, text.Length - 1);
				streamWriter.Flush();
				memoryStream.Position = 0L;
				return memoryStream;
			}
			throw ValueException.NewDataSourceError<Message1>(Strings.JS_Error(text.Substring(1)), TextValue.New(htmlDocument.Url.AbsoluteUri), null);
		}

		// Token: 0x06004CEE RID: 19694 RVA: 0x000FD9FC File Offset: 0x000FBBFC
		private static string GetJavascript()
		{
			string text;
			using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("Microsoft.Mashup.Engine1.Library.Html.DomBuilder.js"))
			{
				using (StreamReader streamReader = new StreamReader(manifestResourceStream))
				{
					text = streamReader.ReadToEnd();
				}
			}
			return text;
		}

		// Token: 0x040028EA RID: 10474
		private static readonly string javascript = HtmlDocumentReader.GetJavascript();
	}
}
