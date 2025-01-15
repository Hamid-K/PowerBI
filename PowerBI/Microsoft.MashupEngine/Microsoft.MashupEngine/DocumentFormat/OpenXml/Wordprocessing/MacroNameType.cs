using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F33 RID: 12083
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class MacroNameType : OpenXmlLeafElement
	{
		// Token: 0x17008F76 RID: 36726
		// (get) Token: 0x06019EBE RID: 106174 RVA: 0x00359E84 File Offset: 0x00358084
		internal override string[] AttributeTagNames
		{
			get
			{
				return MacroNameType.attributeTagNames;
			}
		}

		// Token: 0x17008F77 RID: 36727
		// (get) Token: 0x06019EBF RID: 106175 RVA: 0x00359E8B File Offset: 0x0035808B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MacroNameType.attributeNamespaceIds;
			}
		}

		// Token: 0x17008F78 RID: 36728
		// (get) Token: 0x06019EC0 RID: 106176 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06019EC1 RID: 106177 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
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

		// Token: 0x06019EC2 RID: 106178 RVA: 0x00344715 File Offset: 0x00342915
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0400AAE5 RID: 43749
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AAE6 RID: 43750
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
