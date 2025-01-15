using System;
using System.CodeDom.Compiler;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023EE RID: 9198
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class SortCondition : OpenXmlLeafElement
	{
		// Token: 0x17004DDF RID: 19935
		// (get) Token: 0x06010C1C RID: 68636 RVA: 0x002E6C17 File Offset: 0x002E4E17
		public override string LocalName
		{
			get
			{
				return "sortCondition";
			}
		}

		// Token: 0x17004DE0 RID: 19936
		// (get) Token: 0x06010C1D RID: 68637 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004DE1 RID: 19937
		// (get) Token: 0x06010C1E RID: 68638 RVA: 0x002E6C1E File Offset: 0x002E4E1E
		internal override int ElementTypeId
		{
			get
			{
				return 12924;
			}
		}

		// Token: 0x06010C1F RID: 68639 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004DE2 RID: 19938
		// (get) Token: 0x06010C20 RID: 68640 RVA: 0x002E6C25 File Offset: 0x002E4E25
		internal override string[] AttributeTagNames
		{
			get
			{
				return SortCondition.attributeTagNames;
			}
		}

		// Token: 0x17004DE3 RID: 19939
		// (get) Token: 0x06010C21 RID: 68641 RVA: 0x002E6C2C File Offset: 0x002E4E2C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SortCondition.attributeNamespaceIds;
			}
		}

		// Token: 0x17004DE4 RID: 19940
		// (get) Token: 0x06010C22 RID: 68642 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06010C23 RID: 68643 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "descending")]
		public BooleanValue Descending
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17004DE5 RID: 19941
		// (get) Token: 0x06010C24 RID: 68644 RVA: 0x002E6C33 File Offset: 0x002E4E33
		// (set) Token: 0x06010C25 RID: 68645 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "sortBy")]
		public EnumValue<SortByValues> SortBy
		{
			get
			{
				return (EnumValue<SortByValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17004DE6 RID: 19942
		// (get) Token: 0x06010C26 RID: 68646 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06010C27 RID: 68647 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "ref")]
		public StringValue Reference
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

		// Token: 0x17004DE7 RID: 19943
		// (get) Token: 0x06010C28 RID: 68648 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x06010C29 RID: 68649 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "customList")]
		public StringValue CustomList
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

		// Token: 0x17004DE8 RID: 19944
		// (get) Token: 0x06010C2A RID: 68650 RVA: 0x002E6C42 File Offset: 0x002E4E42
		// (set) Token: 0x06010C2B RID: 68651 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "dxfId")]
		public UInt32Value FormatId
		{
			get
			{
				return (UInt32Value)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17004DE9 RID: 19945
		// (get) Token: 0x06010C2C RID: 68652 RVA: 0x002E6C51 File Offset: 0x002E4E51
		// (set) Token: 0x06010C2D RID: 68653 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "iconSet")]
		public EnumValue<IconSetTypeValues> IconSet
		{
			get
			{
				return (EnumValue<IconSetTypeValues>)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17004DEA RID: 19946
		// (get) Token: 0x06010C2E RID: 68654 RVA: 0x002E6C60 File Offset: 0x002E4E60
		// (set) Token: 0x06010C2F RID: 68655 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "iconId")]
		public UInt32Value IconId
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

		// Token: 0x06010C31 RID: 68657 RVA: 0x002E6C70 File Offset: 0x002E4E70
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "descending" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "sortBy" == name)
			{
				return new EnumValue<SortByValues>();
			}
			if (namespaceId == 0 && "ref" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "customList" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "dxfId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "iconSet" == name)
			{
				return new EnumValue<IconSetTypeValues>();
			}
			if (namespaceId == 0 && "iconId" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010C32 RID: 68658 RVA: 0x002E6D1F File Offset: 0x002E4F1F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SortCondition>(deep);
		}

		// Token: 0x06010C33 RID: 68659 RVA: 0x002E6D28 File Offset: 0x002E4F28
		// Note: this type is marked as 'beforefieldinit'.
		static SortCondition()
		{
			byte[] array = new byte[7];
			SortCondition.attributeNamespaceIds = array;
		}

		// Token: 0x0400763F RID: 30271
		private const string tagName = "sortCondition";

		// Token: 0x04007640 RID: 30272
		private const byte tagNsId = 53;

		// Token: 0x04007641 RID: 30273
		internal const int ElementTypeIdConst = 12924;

		// Token: 0x04007642 RID: 30274
		private static string[] attributeTagNames = new string[] { "descending", "sortBy", "ref", "customList", "dxfId", "iconSet", "iconId" };

		// Token: 0x04007643 RID: 30275
		private static byte[] attributeNamespaceIds;
	}
}
