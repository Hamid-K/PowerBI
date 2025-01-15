using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FB6 RID: 12214
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class FontRelationshipType : OpenXmlLeafElement
	{
		// Token: 0x170093A6 RID: 37798
		// (get) Token: 0x0601A7B0 RID: 108464 RVA: 0x00362E34 File Offset: 0x00361034
		internal override string[] AttributeTagNames
		{
			get
			{
				return FontRelationshipType.attributeTagNames;
			}
		}

		// Token: 0x170093A7 RID: 37799
		// (get) Token: 0x0601A7B1 RID: 108465 RVA: 0x00362E3B File Offset: 0x0036103B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FontRelationshipType.attributeNamespaceIds;
			}
		}

		// Token: 0x170093A8 RID: 37800
		// (get) Token: 0x0601A7B2 RID: 108466 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601A7B3 RID: 108467 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "fontKey")]
		public StringValue FontKey
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

		// Token: 0x170093A9 RID: 37801
		// (get) Token: 0x0601A7B4 RID: 108468 RVA: 0x003480EF File Offset: 0x003462EF
		// (set) Token: 0x0601A7B5 RID: 108469 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "subsetted")]
		public OnOffValue Subsetted
		{
			get
			{
				return (OnOffValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170093AA RID: 37802
		// (get) Token: 0x0601A7B6 RID: 108470 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601A7B7 RID: 108471 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(19, "id")]
		public StringValue Id
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x0601A7B8 RID: 108472 RVA: 0x00362E44 File Offset: 0x00361044
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "fontKey" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "subsetted" == name)
			{
				return new OnOffValue();
			}
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0400AD23 RID: 44323
		private static string[] attributeTagNames = new string[] { "fontKey", "subsetted", "id" };

		// Token: 0x0400AD24 RID: 44324
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 19 };
	}
}
