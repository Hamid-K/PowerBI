using System;
using System.Xml;

namespace Microsoft.ReportingServices.ProgressivePackaging
{
	// Token: 0x02000009 RID: 9
	internal sealed class ImageRequestMessageElement : ImageMessageElement
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000023D8 File Offset: 0x000005D8
		public ImageRequestMessageElement()
		{
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000023E0 File Offset: 0x000005E0
		public ImageRequestMessageElement(string imageUrl, string imageWidth, string imageHeight)
			: base(imageUrl, imageWidth, imageHeight)
		{
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000023EC File Offset: 0x000005EC
		public void Write(XmlWriter writer)
		{
			writer.WriteStartElement("ExternalImage");
			writer.WriteStartElement("Uri");
			writer.WriteString(base.ImageUrl);
			writer.WriteEndElement();
			writer.WriteStartElement("MaxWidth");
			writer.WriteValue(base.ImageWidth);
			writer.WriteEndElement();
			writer.WriteStartElement("MaxHeight");
			writer.WriteValue(base.ImageHeight);
			writer.WriteEndElement();
			writer.WriteEndElement();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002464 File Offset: 0x00000664
		public void Read(XmlReader reader)
		{
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.Element)
				{
					if ("Uri".Equals(reader.Name, StringComparison.Ordinal))
					{
						base.ImageUrl = this.ReadStringValue(reader);
					}
					else if ("MaxWidth".Equals(reader.Name, StringComparison.Ordinal))
					{
						base.ImageWidth = this.ReadStringValue(reader);
					}
					else if ("MaxHeight".Equals(reader.Name, StringComparison.Ordinal))
					{
						base.ImageHeight = this.ReadStringValue(reader);
					}
				}
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000024E8 File Offset: 0x000006E8
		private string ReadStringValue(XmlReader reader)
		{
			if (!reader.IsEmptyElement)
			{
				reader.Read();
			}
			return reader.Value;
		}

		// Token: 0x0400000B RID: 11
		internal const string NameSpace2011 = "http://schemas.microsoft.com/sqlserver/reporting/2011/01/getexternalimages";

		// Token: 0x0400000C RID: 12
		internal const string ClientRequestElement = "ClientRequest";

		// Token: 0x0400000D RID: 13
		internal const string ExternalImagesElement = "ExternalImages";

		// Token: 0x0400000E RID: 14
		internal const string ExternalImageElement = "ExternalImage";

		// Token: 0x0400000F RID: 15
		private const string UriElement = "Uri";

		// Token: 0x04000010 RID: 16
		private const string MaxWidthElement = "MaxWidth";

		// Token: 0x04000011 RID: 17
		private const string MaxHeightElement = "MaxHeight";
	}
}
