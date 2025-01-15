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
		// Token: 0x06000087 RID: 135 RVA: 0x00002D28 File Offset: 0x00000F28
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

		// Token: 0x06000088 RID: 136 RVA: 0x00002D7C File Offset: 0x00000F7C
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
