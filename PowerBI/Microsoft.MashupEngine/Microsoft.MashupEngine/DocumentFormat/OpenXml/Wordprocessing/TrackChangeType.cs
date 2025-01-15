using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D34 RID: 11572
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class TrackChangeType : OpenXmlLeafElement
	{
		// Token: 0x17008621 RID: 34337
		// (get) Token: 0x06018AFC RID: 101116 RVA: 0x00343FCF File Offset: 0x003421CF
		internal override string[] AttributeTagNames
		{
			get
			{
				return TrackChangeType.attributeTagNames;
			}
		}

		// Token: 0x17008622 RID: 34338
		// (get) Token: 0x06018AFD RID: 101117 RVA: 0x00343FD6 File Offset: 0x003421D6
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TrackChangeType.attributeNamespaceIds;
			}
		}

		// Token: 0x17008623 RID: 34339
		// (get) Token: 0x06018AFE RID: 101118 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06018AFF RID: 101119 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "author")]
		public StringValue Author
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

		// Token: 0x17008624 RID: 34340
		// (get) Token: 0x06018B00 RID: 101120 RVA: 0x002EC18F File Offset: 0x002EA38F
		// (set) Token: 0x06018B01 RID: 101121 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "date")]
		public DateTimeValue Date
		{
			get
			{
				return (DateTimeValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17008625 RID: 34341
		// (get) Token: 0x06018B02 RID: 101122 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06018B03 RID: 101123 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "id")]
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

		// Token: 0x06018B04 RID: 101124 RVA: 0x00343FE0 File Offset: 0x003421E0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "author" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "date" == name)
			{
				return new DateTimeValue();
			}
			if (23 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0400A409 RID: 41993
		private static string[] attributeTagNames = new string[] { "author", "date", "id" };

		// Token: 0x0400A40A RID: 41994
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23 };
	}
}
