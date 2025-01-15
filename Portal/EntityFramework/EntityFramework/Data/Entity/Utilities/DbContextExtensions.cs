using System;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace System.Data.Entity.Utilities
{
	// Token: 0x02000074 RID: 116
	internal static class DbContextExtensions
	{
		// Token: 0x06000431 RID: 1073 RVA: 0x0000F96E File Offset: 0x0000DB6E
		public static XDocument GetModel(this DbContext context)
		{
			return DbContextExtensions.GetModel(delegate(XmlWriter w)
			{
				EdmxWriter.WriteEdmx(context, w);
			});
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000F98C File Offset: 0x0000DB8C
		public static XDocument GetModel(Action<XmlWriter> writeXml)
		{
			XDocument xdocument;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (XmlWriter xmlWriter = XmlWriter.Create(memoryStream, new XmlWriterSettings
				{
					Indent = true
				}))
				{
					writeXml(xmlWriter);
				}
				memoryStream.Position = 0L;
				xdocument = XDocument.Load(memoryStream);
			}
			return xdocument;
		}
	}
}
