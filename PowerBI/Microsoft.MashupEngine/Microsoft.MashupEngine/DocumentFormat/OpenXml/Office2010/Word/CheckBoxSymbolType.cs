using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024D0 RID: 9424
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class CheckBoxSymbolType : OpenXmlLeafElement
	{
		// Token: 0x1700530A RID: 21258
		// (get) Token: 0x060117C8 RID: 71624 RVA: 0x002EEEC1 File Offset: 0x002ED0C1
		internal override string[] AttributeTagNames
		{
			get
			{
				return CheckBoxSymbolType.attributeTagNames;
			}
		}

		// Token: 0x1700530B RID: 21259
		// (get) Token: 0x060117C9 RID: 71625 RVA: 0x002EEEC8 File Offset: 0x002ED0C8
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CheckBoxSymbolType.attributeNamespaceIds;
			}
		}

		// Token: 0x1700530C RID: 21260
		// (get) Token: 0x060117CA RID: 71626 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060117CB RID: 71627 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(52, "font")]
		public StringValue Font
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x1700530D RID: 21261
		// (get) Token: 0x060117CC RID: 71628 RVA: 0x002EB1A4 File Offset: 0x002E93A4
		// (set) Token: 0x060117CD RID: 71629 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(52, "val")]
		public HexBinaryValue Val
		{
			get
			{
				return (HexBinaryValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x060117CE RID: 71630 RVA: 0x002EEECF File Offset: 0x002ED0CF
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "font" == name)
			{
				return new StringValue();
			}
			if (52 == namespaceId && "val" == name)
			{
				return new HexBinaryValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x04007A10 RID: 31248
		private static string[] attributeTagNames = new string[] { "font", "val" };

		// Token: 0x04007A11 RID: 31249
		private static byte[] attributeNamespaceIds = new byte[] { 52, 52 };
	}
}
