using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D40 RID: 11584
	[GeneratedCode("DomGen", "2.0")]
	internal class BookmarkStart : OpenXmlLeafElement
	{
		// Token: 0x1700864E RID: 34382
		// (get) Token: 0x06018B57 RID: 101207 RVA: 0x0034429C File Offset: 0x0034249C
		public override string LocalName
		{
			get
			{
				return "bookmarkStart";
			}
		}

		// Token: 0x1700864F RID: 34383
		// (get) Token: 0x06018B58 RID: 101208 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008650 RID: 34384
		// (get) Token: 0x06018B59 RID: 101209 RVA: 0x003442A3 File Offset: 0x003424A3
		internal override int ElementTypeId
		{
			get
			{
				return 11476;
			}
		}

		// Token: 0x06018B5A RID: 101210 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008651 RID: 34385
		// (get) Token: 0x06018B5B RID: 101211 RVA: 0x003442AA File Offset: 0x003424AA
		internal override string[] AttributeTagNames
		{
			get
			{
				return BookmarkStart.attributeTagNames;
			}
		}

		// Token: 0x17008652 RID: 34386
		// (get) Token: 0x06018B5C RID: 101212 RVA: 0x003442B1 File Offset: 0x003424B1
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BookmarkStart.attributeNamespaceIds;
			}
		}

		// Token: 0x17008653 RID: 34387
		// (get) Token: 0x06018B5D RID: 101213 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06018B5E RID: 101214 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "name")]
		public StringValue Name
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

		// Token: 0x17008654 RID: 34388
		// (get) Token: 0x06018B5F RID: 101215 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x06018B60 RID: 101216 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "colFirst")]
		public Int32Value ColumnFirst
		{
			get
			{
				return (Int32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17008655 RID: 34389
		// (get) Token: 0x06018B61 RID: 101217 RVA: 0x002E1683 File Offset: 0x002DF883
		// (set) Token: 0x06018B62 RID: 101218 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "colLast")]
		public Int32Value ColumnLast
		{
			get
			{
				return (Int32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17008656 RID: 34390
		// (get) Token: 0x06018B63 RID: 101219 RVA: 0x003442B8 File Offset: 0x003424B8
		// (set) Token: 0x06018B64 RID: 101220 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(23, "displacedByCustomXml")]
		public EnumValue<DisplacedByCustomXmlValues> DisplacedByCustomXml
		{
			get
			{
				return (EnumValue<DisplacedByCustomXmlValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17008657 RID: 34391
		// (get) Token: 0x06018B65 RID: 101221 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x06018B66 RID: 101222 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(23, "id")]
		public StringValue Id
		{
			get
			{
				return (StringValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x06018B68 RID: 101224 RVA: 0x003442C8 File Offset: 0x003424C8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
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

		// Token: 0x06018B69 RID: 101225 RVA: 0x00344355 File Offset: 0x00342555
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BookmarkStart>(deep);
		}

		// Token: 0x0400A42E RID: 42030
		private const string tagName = "bookmarkStart";

		// Token: 0x0400A42F RID: 42031
		private const byte tagNsId = 23;

		// Token: 0x0400A430 RID: 42032
		internal const int ElementTypeIdConst = 11476;

		// Token: 0x0400A431 RID: 42033
		private static string[] attributeTagNames = new string[] { "name", "colFirst", "colLast", "displacedByCustomXml", "id" };

		// Token: 0x0400A432 RID: 42034
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23, 23, 23 };
	}
}
