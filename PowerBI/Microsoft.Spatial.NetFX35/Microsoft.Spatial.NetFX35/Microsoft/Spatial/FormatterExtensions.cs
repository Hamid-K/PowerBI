using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;

namespace Microsoft.Spatial
{
	// Token: 0x02000015 RID: 21
	public static class FormatterExtensions
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x0000375C File Offset: 0x0000195C
		public static string Write(this SpatialFormatter<TextReader, TextWriter> formatter, ISpatial spatial)
		{
			Util.CheckArgumentNull(formatter, "formatter");
			StringBuilder stringBuilder = new StringBuilder();
			using (TextWriter textWriter = new StringWriter(stringBuilder, CultureInfo.InvariantCulture))
			{
				formatter.Write(spatial, textWriter);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000037B0 File Offset: 0x000019B0
		public static string Write(this SpatialFormatter<XmlReader, XmlWriter> formatter, ISpatial spatial)
		{
			Util.CheckArgumentNull(formatter, "formatter");
			StringBuilder stringBuilder = new StringBuilder();
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings
			{
				OmitXmlDeclaration = true
			};
			using (XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, xmlWriterSettings))
			{
				formatter.Write(spatial, xmlWriter);
			}
			return stringBuilder.ToString();
		}
	}
}
