using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E57 RID: 11863
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class TextType : OpenXmlLeafTextElement
	{
		// Token: 0x17008A49 RID: 35401
		// (get) Token: 0x06019367 RID: 103271 RVA: 0x003478D5 File Offset: 0x00345AD5
		internal override string[] AttributeTagNames
		{
			get
			{
				return TextType.attributeTagNames;
			}
		}

		// Token: 0x17008A4A RID: 35402
		// (get) Token: 0x06019368 RID: 103272 RVA: 0x003478DC File Offset: 0x00345ADC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TextType.attributeNamespaceIds;
			}
		}

		// Token: 0x17008A4B RID: 35403
		// (get) Token: 0x06019369 RID: 103273 RVA: 0x0031B86D File Offset: 0x00319A6D
		// (set) Token: 0x0601936A RID: 103274 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x0601936B RID: 103275 RVA: 0x0031B897 File Offset: 0x00319A97
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (1 == namespaceId && "space" == name)
			{
				return new EnumValue<SpaceProcessingModeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601936C RID: 103276 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		protected TextType()
		{
		}

		// Token: 0x0601936D RID: 103277 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		protected TextType(string text)
			: base(text)
		{
		}

		// Token: 0x0601936E RID: 103278 RVA: 0x003478E4 File Offset: 0x00345AE4
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0400A7A1 RID: 42913
		private static string[] attributeTagNames = new string[] { "space" };

		// Token: 0x0400A7A2 RID: 42914
		private static byte[] attributeNamespaceIds = new byte[] { 1 };
	}
}
