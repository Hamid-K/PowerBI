using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F68 RID: 12136
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class FootnoteEndnoteSeparatorReferenceType : OpenXmlLeafElement
	{
		// Token: 0x170090C8 RID: 37064
		// (get) Token: 0x0601A1AB RID: 106923 RVA: 0x0035D858 File Offset: 0x0035BA58
		internal override string[] AttributeTagNames
		{
			get
			{
				return FootnoteEndnoteSeparatorReferenceType.attributeTagNames;
			}
		}

		// Token: 0x170090C9 RID: 37065
		// (get) Token: 0x0601A1AC RID: 106924 RVA: 0x0035D85F File Offset: 0x0035BA5F
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FootnoteEndnoteSeparatorReferenceType.attributeNamespaceIds;
			}
		}

		// Token: 0x170090CA RID: 37066
		// (get) Token: 0x0601A1AD RID: 106925 RVA: 0x002EC050 File Offset: 0x002EA250
		// (set) Token: 0x0601A1AE RID: 106926 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "id")]
		public IntegerValue Id
		{
			get
			{
				return (IntegerValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601A1AF RID: 106927 RVA: 0x0035D866 File Offset: 0x0035BA66
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "id" == name)
			{
				return new IntegerValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0400ABCB RID: 43979
		private static string[] attributeTagNames = new string[] { "id" };

		// Token: 0x0400ABCC RID: 43980
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
