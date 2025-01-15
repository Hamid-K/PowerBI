using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;

namespace Microsoft.Spatial
{
	// Token: 0x0200000D RID: 13
	public static class FormatterExtensions
	{
		// Token: 0x06000099 RID: 153 RVA: 0x00003110 File Offset: 0x00001310
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

		// Token: 0x0600009A RID: 154 RVA: 0x00003164 File Offset: 0x00001364
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
