using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FE4 RID: 12260
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class NonNegativeShortType : OpenXmlLeafElement
	{
		// Token: 0x17009506 RID: 38150
		// (get) Token: 0x0601AAA9 RID: 109225 RVA: 0x00365BE8 File Offset: 0x00363DE8
		internal override string[] AttributeTagNames
		{
			get
			{
				return NonNegativeShortType.attributeTagNames;
			}
		}

		// Token: 0x17009507 RID: 38151
		// (get) Token: 0x0601AAAA RID: 109226 RVA: 0x00365BEF File Offset: 0x00363DEF
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NonNegativeShortType.attributeNamespaceIds;
			}
		}

		// Token: 0x17009508 RID: 38152
		// (get) Token: 0x0601AAAB RID: 109227 RVA: 0x0034726F File Offset: 0x0034546F
		// (set) Token: 0x0601AAAC RID: 109228 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public Int16Value Val
		{
			get
			{
				return (Int16Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601AAAD RID: 109229 RVA: 0x0035A444 File Offset: 0x00358644
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new Int16Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0400ADE3 RID: 44515
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400ADE4 RID: 44516
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
