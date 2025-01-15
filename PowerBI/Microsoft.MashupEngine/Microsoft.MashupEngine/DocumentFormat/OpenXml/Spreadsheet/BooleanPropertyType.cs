using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B97 RID: 11159
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class BooleanPropertyType : OpenXmlLeafElement
	{
		// Token: 0x17007B21 RID: 31521
		// (get) Token: 0x0601723F RID: 94783 RVA: 0x00333355 File Offset: 0x00331555
		internal override string[] AttributeTagNames
		{
			get
			{
				return BooleanPropertyType.attributeTagNames;
			}
		}

		// Token: 0x17007B22 RID: 31522
		// (get) Token: 0x06017240 RID: 94784 RVA: 0x0033335C File Offset: 0x0033155C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BooleanPropertyType.attributeNamespaceIds;
			}
		}

		// Token: 0x17007B23 RID: 31523
		// (get) Token: 0x06017241 RID: 94785 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06017242 RID: 94786 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public BooleanValue Val
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06017243 RID: 94787 RVA: 0x002DE6BC File Offset: 0x002DC8BC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017245 RID: 94789 RVA: 0x00333364 File Offset: 0x00331564
		// Note: this type is marked as 'beforefieldinit'.
		static BooleanPropertyType()
		{
			byte[] array = new byte[1];
			BooleanPropertyType.attributeNamespaceIds = array;
		}

		// Token: 0x04009B35 RID: 39733
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04009B36 RID: 39734
		private static byte[] attributeNamespaceIds;
	}
}
