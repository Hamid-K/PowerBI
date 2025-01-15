using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x0200298A RID: 10634
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class CharType : OpenXmlLeafElement
	{
		// Token: 0x17006CB4 RID: 27828
		// (get) Token: 0x060151FA RID: 86522 RVA: 0x0031B8F7 File Offset: 0x00319AF7
		internal override string[] AttributeTagNames
		{
			get
			{
				return CharType.attributeTagNames;
			}
		}

		// Token: 0x17006CB5 RID: 27829
		// (get) Token: 0x060151FB RID: 86523 RVA: 0x0031B8FE File Offset: 0x00319AFE
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CharType.attributeNamespaceIds;
			}
		}

		// Token: 0x17006CB6 RID: 27830
		// (get) Token: 0x060151FC RID: 86524 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060151FD RID: 86525 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(21, "val")]
		public StringValue Val
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

		// Token: 0x060151FE RID: 86526 RVA: 0x0031B905 File Offset: 0x00319B05
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x040091B0 RID: 37296
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x040091B1 RID: 37297
		private static byte[] attributeNamespaceIds = new byte[] { 21 };
	}
}
