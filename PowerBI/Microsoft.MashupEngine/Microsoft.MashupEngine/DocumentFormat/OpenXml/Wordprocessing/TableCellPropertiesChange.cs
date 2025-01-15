using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F5F RID: 12127
	[ChildElementInfo(typeof(PreviousTableCellProperties))]
	[GeneratedCode("DomGen", "2.0")]
	internal class TableCellPropertiesChange : OpenXmlCompositeElement
	{
		// Token: 0x17009065 RID: 36965
		// (get) Token: 0x0601A0DA RID: 106714 RVA: 0x0035CDDB File Offset: 0x0035AFDB
		public override string LocalName
		{
			get
			{
				return "tcPrChange";
			}
		}

		// Token: 0x17009066 RID: 36966
		// (get) Token: 0x0601A0DB RID: 106715 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009067 RID: 36967
		// (get) Token: 0x0601A0DC RID: 106716 RVA: 0x0035CDE2 File Offset: 0x0035AFE2
		internal override int ElementTypeId
		{
			get
			{
				return 11783;
			}
		}

		// Token: 0x0601A0DD RID: 106717 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009068 RID: 36968
		// (get) Token: 0x0601A0DE RID: 106718 RVA: 0x0035CDE9 File Offset: 0x0035AFE9
		internal override string[] AttributeTagNames
		{
			get
			{
				return TableCellPropertiesChange.attributeTagNames;
			}
		}

		// Token: 0x17009069 RID: 36969
		// (get) Token: 0x0601A0DF RID: 106719 RVA: 0x0035CDF0 File Offset: 0x0035AFF0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TableCellPropertiesChange.attributeNamespaceIds;
			}
		}

		// Token: 0x1700906A RID: 36970
		// (get) Token: 0x0601A0E0 RID: 106720 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601A0E1 RID: 106721 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x1700906B RID: 36971
		// (get) Token: 0x0601A0E2 RID: 106722 RVA: 0x002EC18F File Offset: 0x002EA38F
		// (set) Token: 0x0601A0E3 RID: 106723 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x1700906C RID: 36972
		// (get) Token: 0x0601A0E4 RID: 106724 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601A0E5 RID: 106725 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x0601A0E6 RID: 106726 RVA: 0x00293ECF File Offset: 0x002920CF
		public TableCellPropertiesChange()
		{
		}

		// Token: 0x0601A0E7 RID: 106727 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TableCellPropertiesChange(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A0E8 RID: 106728 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TableCellPropertiesChange(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A0E9 RID: 106729 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TableCellPropertiesChange(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A0EA RID: 106730 RVA: 0x0035CDF7 File Offset: 0x0035AFF7
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "tcPr" == name)
			{
				return new PreviousTableCellProperties();
			}
			return null;
		}

		// Token: 0x1700906D RID: 36973
		// (get) Token: 0x0601A0EB RID: 106731 RVA: 0x0035CE12 File Offset: 0x0035B012
		internal override string[] ElementTagNames
		{
			get
			{
				return TableCellPropertiesChange.eleTagNames;
			}
		}

		// Token: 0x1700906E RID: 36974
		// (get) Token: 0x0601A0EC RID: 106732 RVA: 0x0035CE19 File Offset: 0x0035B019
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TableCellPropertiesChange.eleNamespaceIds;
			}
		}

		// Token: 0x1700906F RID: 36975
		// (get) Token: 0x0601A0ED RID: 106733 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17009070 RID: 36976
		// (get) Token: 0x0601A0EE RID: 106734 RVA: 0x0035CE20 File Offset: 0x0035B020
		// (set) Token: 0x0601A0EF RID: 106735 RVA: 0x0035CE29 File Offset: 0x0035B029
		public PreviousTableCellProperties PreviousTableCellProperties
		{
			get
			{
				return base.GetElement<PreviousTableCellProperties>(0);
			}
			set
			{
				base.SetElement<PreviousTableCellProperties>(0, value);
			}
		}

		// Token: 0x0601A0F0 RID: 106736 RVA: 0x0035CE34 File Offset: 0x0035B034
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

		// Token: 0x0601A0F1 RID: 106737 RVA: 0x0035CE91 File Offset: 0x0035B091
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableCellPropertiesChange>(deep);
		}

		// Token: 0x0400AB9A RID: 43930
		private const string tagName = "tcPrChange";

		// Token: 0x0400AB9B RID: 43931
		private const byte tagNsId = 23;

		// Token: 0x0400AB9C RID: 43932
		internal const int ElementTypeIdConst = 11783;

		// Token: 0x0400AB9D RID: 43933
		private static string[] attributeTagNames = new string[] { "author", "date", "id" };

		// Token: 0x0400AB9E RID: 43934
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23 };

		// Token: 0x0400AB9F RID: 43935
		private static readonly string[] eleTagNames = new string[] { "tcPr" };

		// Token: 0x0400ABA0 RID: 43936
		private static readonly byte[] eleNamespaceIds = new byte[] { 23 };
	}
}
