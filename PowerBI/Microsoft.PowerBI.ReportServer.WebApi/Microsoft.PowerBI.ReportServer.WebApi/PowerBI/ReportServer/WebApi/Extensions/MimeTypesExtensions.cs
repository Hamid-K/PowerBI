using System;
using System.IO;
using System.Web;

namespace Microsoft.PowerBI.ReportServer.WebApi.Extensions
{
	// Token: 0x02000036 RID: 54
	public static class MimeTypesExtensions
	{
		// Token: 0x060000FE RID: 254 RVA: 0x00006DA8 File Offset: 0x00004FA8
		public static string GetMimeType(string fileName)
		{
			string extension = Path.GetExtension(fileName);
			if (extension == ".csv")
			{
				return "text/csv";
			}
			if (extension == ".json" || extension == ".jsonrpl")
			{
				return "application/json";
			}
			if (extension == ".mhtml")
			{
				return "multipart/related";
			}
			if (extension == ".emf")
			{
				return "image/emf";
			}
			if (!(extension == ".svg"))
			{
				return MimeMapping.GetMimeMapping(extension);
			}
			return "image/svg+xml";
		}
	}
}
