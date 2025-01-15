using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D47 RID: 11591
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class MoveBookmarkType : OpenXmlLeafElement
	{
		// Token: 0x1700866B RID: 34411
		// (get) Token: 0x06018B92 RID: 101266 RVA: 0x003444CC File Offset: 0x003426CC
		internal override string[] AttributeTagNames
		{
			get
			{
				return MoveBookmarkType.attributeTagNames;
			}
		}

		// Token: 0x1700866C RID: 34412
		// (get) Token: 0x06018B93 RID: 101267 RVA: 0x003444D3 File Offset: 0x003426D3
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MoveBookmarkType.attributeNamespaceIds;
			}
		}

		// Token: 0x1700866D RID: 34413
		// (get) Token: 0x06018B94 RID: 101268 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06018B95 RID: 101269 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x1700866E RID: 34414
		// (get) Token: 0x06018B96 RID: 101270 RVA: 0x002EC18F File Offset: 0x002EA38F
		// (set) Token: 0x06018B97 RID: 101271 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x1700866F RID: 34415
		// (get) Token: 0x06018B98 RID: 101272 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06018B99 RID: 101273 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "name")]
		public StringValue Name
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

		// Token: 0x17008670 RID: 34416
		// (get) Token: 0x06018B9A RID: 101274 RVA: 0x002BFA76 File Offset: 0x002BDC76
		// (set) Token: 0x06018B9B RID: 101275 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(23, "colFirst")]
		public Int32Value ColumnFirst
		{
			get
			{
				return (Int32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17008671 RID: 34417
		// (get) Token: 0x06018B9C RID: 101276 RVA: 0x002C8292 File Offset: 0x002C6492
		// (set) Token: 0x06018B9D RID: 101277 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(23, "colLast")]
		public Int32Value ColumnLast
		{
			get
			{
				return (Int32Value)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17008672 RID: 34418
		// (get) Token: 0x06018B9E RID: 101278 RVA: 0x003444DA File Offset: 0x003426DA
		// (set) Token: 0x06018B9F RID: 101279 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(23, "displacedByCustomXml")]
		public EnumValue<DisplacedByCustomXmlValues> DisplacedByCustomXml
		{
			get
			{
				return (EnumValue<DisplacedByCustomXmlValues>)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17008673 RID: 34419
		// (get) Token: 0x06018BA0 RID: 101280 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x06018BA1 RID: 101281 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(23, "id")]
		public StringValue Id
		{
			get
			{
				return (StringValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x06018BA2 RID: 101282 RVA: 0x003444EC File Offset: 0x003426EC
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
			if (23 == namespaceId && "name" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "colFirst" == name)
			{
				return new Int32Value();
			}
			if (23 == namespaceId && "colLast" == name)
			{
				return new Int32Value();
			}
			if (23 == namespaceId && "displacedByCustomXml" == name)
			{
				return new EnumValue<DisplacedByCustomXmlValues>();
			}
			if (23 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0400A444 RID: 42052
		private static string[] attributeTagNames = new string[] { "author", "date", "name", "colFirst", "colLast", "displacedByCustomXml", "id" };

		// Token: 0x0400A445 RID: 42053
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23, 23, 23, 23, 23 };
	}
}
