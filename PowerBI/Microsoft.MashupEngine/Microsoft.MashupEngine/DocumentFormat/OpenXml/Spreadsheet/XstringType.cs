using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B33 RID: 11059
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class XstringType : OpenXmlLeafTextElement
	{
		// Token: 0x17007780 RID: 30592
		// (get) Token: 0x06016A3A RID: 92730 RVA: 0x0032D7BF File Offset: 0x0032B9BF
		internal override string[] AttributeTagNames
		{
			get
			{
				return XstringType.attributeTagNames;
			}
		}

		// Token: 0x17007781 RID: 30593
		// (get) Token: 0x06016A3B RID: 92731 RVA: 0x0032D7C6 File Offset: 0x0032B9C6
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return XstringType.attributeNamespaceIds;
			}
		}

		// Token: 0x17007782 RID: 30594
		// (get) Token: 0x06016A3C RID: 92732 RVA: 0x0031B86D File Offset: 0x00319A6D
		// (set) Token: 0x06016A3D RID: 92733 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(1, "space")]
		public EnumValue<SpaceProcessingModeValues> Space
		{
			get
			{
				return (EnumValue<SpaceProcessingModeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06016A3E RID: 92734 RVA: 0x0031B897 File Offset: 0x00319A97
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (1 == namespaceId && "space" == name)
			{
				return new EnumValue<SpaceProcessingModeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016A3F RID: 92735 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		protected XstringType()
		{
		}

		// Token: 0x06016A40 RID: 92736 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		protected XstringType(string text)
			: base(text)
		{
		}

		// Token: 0x06016A41 RID: 92737 RVA: 0x0032D7D0 File Offset: 0x0032B9D0
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0400995A RID: 39258
		private static string[] attributeTagNames = new string[] { "space" };

		// Token: 0x0400995B RID: 39259
		private static byte[] attributeNamespaceIds = new byte[] { 1 };
	}
}
