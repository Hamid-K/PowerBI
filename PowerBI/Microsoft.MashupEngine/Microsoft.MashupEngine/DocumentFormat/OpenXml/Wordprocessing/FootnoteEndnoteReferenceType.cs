using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E7B RID: 11899
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class FootnoteEndnoteReferenceType : OpenXmlLeafElement
	{
		// Token: 0x17008ACF RID: 35535
		// (get) Token: 0x0601948A RID: 103562 RVA: 0x00348378 File Offset: 0x00346578
		internal override string[] AttributeTagNames
		{
			get
			{
				return FootnoteEndnoteReferenceType.attributeTagNames;
			}
		}

		// Token: 0x17008AD0 RID: 35536
		// (get) Token: 0x0601948B RID: 103563 RVA: 0x0034837F File Offset: 0x0034657F
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FootnoteEndnoteReferenceType.attributeNamespaceIds;
			}
		}

		// Token: 0x17008AD1 RID: 35537
		// (get) Token: 0x0601948C RID: 103564 RVA: 0x002EBFC4 File Offset: 0x002EA1C4
		// (set) Token: 0x0601948D RID: 103565 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "customMarkFollows")]
		public OnOffValue CustomMarkFollows
		{
			get
			{
				return (OnOffValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17008AD2 RID: 35538
		// (get) Token: 0x0601948E RID: 103566 RVA: 0x002BD46B File Offset: 0x002BB66B
		// (set) Token: 0x0601948F RID: 103567 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "id")]
		public IntegerValue Id
		{
			get
			{
				return (IntegerValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06019490 RID: 103568 RVA: 0x00348386 File Offset: 0x00346586
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "customMarkFollows" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "id" == name)
			{
				return new IntegerValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0400A815 RID: 43029
		private static string[] attributeTagNames = new string[] { "customMarkFollows", "id" };

		// Token: 0x0400A816 RID: 43030
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23 };
	}
}
