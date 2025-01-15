using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BB0 RID: 11184
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SheetIdMap))]
	[ChildElementInfo(typeof(ReviewedList))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class Header : OpenXmlCompositeElement
	{
		// Token: 0x17007B91 RID: 31633
		// (get) Token: 0x06017329 RID: 95017 RVA: 0x002A6937 File Offset: 0x002A4B37
		public override string LocalName
		{
			get
			{
				return "header";
			}
		}

		// Token: 0x17007B92 RID: 31634
		// (get) Token: 0x0601732A RID: 95018 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007B93 RID: 31635
		// (get) Token: 0x0601732B RID: 95019 RVA: 0x00333C17 File Offset: 0x00331E17
		internal override int ElementTypeId
		{
			get
			{
				return 11155;
			}
		}

		// Token: 0x0601732C RID: 95020 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007B94 RID: 31636
		// (get) Token: 0x0601732D RID: 95021 RVA: 0x00333C1E File Offset: 0x00331E1E
		internal override string[] AttributeTagNames
		{
			get
			{
				return Header.attributeTagNames;
			}
		}

		// Token: 0x17007B95 RID: 31637
		// (get) Token: 0x0601732E RID: 95022 RVA: 0x00333C25 File Offset: 0x00331E25
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Header.attributeNamespaceIds;
			}
		}

		// Token: 0x17007B96 RID: 31638
		// (get) Token: 0x0601732F RID: 95023 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06017330 RID: 95024 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "guid")]
		public StringValue Guid
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

		// Token: 0x17007B97 RID: 31639
		// (get) Token: 0x06017331 RID: 95025 RVA: 0x002EC18F File Offset: 0x002EA38F
		// (set) Token: 0x06017332 RID: 95026 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "dateTime")]
		public DateTimeValue DateTime
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

		// Token: 0x17007B98 RID: 31640
		// (get) Token: 0x06017333 RID: 95027 RVA: 0x002E5814 File Offset: 0x002E3A14
		// (set) Token: 0x06017334 RID: 95028 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "maxSheetId")]
		public UInt32Value MaxSheetId
		{
			get
			{
				return (UInt32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17007B99 RID: 31641
		// (get) Token: 0x06017335 RID: 95029 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x06017336 RID: 95030 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "userName")]
		public StringValue UserName
		{
			get
			{
				return (StringValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17007B9A RID: 31642
		// (get) Token: 0x06017337 RID: 95031 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x06017338 RID: 95032 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(19, "id")]
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

		// Token: 0x17007B9B RID: 31643
		// (get) Token: 0x06017339 RID: 95033 RVA: 0x002E6EEB File Offset: 0x002E50EB
		// (set) Token: 0x0601733A RID: 95034 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "minRId")]
		public UInt32Value MinRevisionId
		{
			get
			{
				return (UInt32Value)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17007B9C RID: 31644
		// (get) Token: 0x0601733B RID: 95035 RVA: 0x002E6C60 File Offset: 0x002E4E60
		// (set) Token: 0x0601733C RID: 95036 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "maxRId")]
		public UInt32Value MaxRevisionId
		{
			get
			{
				return (UInt32Value)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x0601733D RID: 95037 RVA: 0x00293ECF File Offset: 0x002920CF
		public Header()
		{
		}

		// Token: 0x0601733E RID: 95038 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Header(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601733F RID: 95039 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Header(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017340 RID: 95040 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Header(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017341 RID: 95041 RVA: 0x00333C2C File Offset: 0x00331E2C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "sheetIdMap" == name)
			{
				return new SheetIdMap();
			}
			if (22 == namespaceId && "reviewedList" == name)
			{
				return new ReviewedList();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17007B9D RID: 31645
		// (get) Token: 0x06017342 RID: 95042 RVA: 0x00333C82 File Offset: 0x00331E82
		internal override string[] ElementTagNames
		{
			get
			{
				return Header.eleTagNames;
			}
		}

		// Token: 0x17007B9E RID: 31646
		// (get) Token: 0x06017343 RID: 95043 RVA: 0x00333C89 File Offset: 0x00331E89
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Header.eleNamespaceIds;
			}
		}

		// Token: 0x17007B9F RID: 31647
		// (get) Token: 0x06017344 RID: 95044 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007BA0 RID: 31648
		// (get) Token: 0x06017345 RID: 95045 RVA: 0x00333C90 File Offset: 0x00331E90
		// (set) Token: 0x06017346 RID: 95046 RVA: 0x00333C99 File Offset: 0x00331E99
		public SheetIdMap SheetIdMap
		{
			get
			{
				return base.GetElement<SheetIdMap>(0);
			}
			set
			{
				base.SetElement<SheetIdMap>(0, value);
			}
		}

		// Token: 0x17007BA1 RID: 31649
		// (get) Token: 0x06017347 RID: 95047 RVA: 0x00333CA3 File Offset: 0x00331EA3
		// (set) Token: 0x06017348 RID: 95048 RVA: 0x00333CAC File Offset: 0x00331EAC
		public ReviewedList ReviewedList
		{
			get
			{
				return base.GetElement<ReviewedList>(1);
			}
			set
			{
				base.SetElement<ReviewedList>(1, value);
			}
		}

		// Token: 0x17007BA2 RID: 31650
		// (get) Token: 0x06017349 RID: 95049 RVA: 0x00329822 File Offset: 0x00327A22
		// (set) Token: 0x0601734A RID: 95050 RVA: 0x0032982B File Offset: 0x00327A2B
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(2);
			}
			set
			{
				base.SetElement<ExtensionList>(2, value);
			}
		}

		// Token: 0x0601734B RID: 95051 RVA: 0x00333CB8 File Offset: 0x00331EB8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "guid" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "dateTime" == name)
			{
				return new DateTimeValue();
			}
			if (namespaceId == 0 && "maxSheetId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "userName" == name)
			{
				return new StringValue();
			}
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "minRId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "maxRId" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601734C RID: 95052 RVA: 0x00333D69 File Offset: 0x00331F69
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Header>(deep);
		}

		// Token: 0x0601734D RID: 95053 RVA: 0x00333D74 File Offset: 0x00331F74
		// Note: this type is marked as 'beforefieldinit'.
		static Header()
		{
			byte[] array = new byte[7];
			array[4] = 19;
			Header.attributeNamespaceIds = array;
			Header.eleTagNames = new string[] { "sheetIdMap", "reviewedList", "extLst" };
			Header.eleNamespaceIds = new byte[] { 22, 22, 22 };
		}

		// Token: 0x04009B8F RID: 39823
		private const string tagName = "header";

		// Token: 0x04009B90 RID: 39824
		private const byte tagNsId = 22;

		// Token: 0x04009B91 RID: 39825
		internal const int ElementTypeIdConst = 11155;

		// Token: 0x04009B92 RID: 39826
		private static string[] attributeTagNames = new string[] { "guid", "dateTime", "maxSheetId", "userName", "id", "minRId", "maxRId" };

		// Token: 0x04009B93 RID: 39827
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009B94 RID: 39828
		private static readonly string[] eleTagNames;

		// Token: 0x04009B95 RID: 39829
		private static readonly byte[] eleNamespaceIds;
	}
}
