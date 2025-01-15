using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B1B RID: 11035
	[ChildElementInfo(typeof(Location))]
	[ChildElementInfo(typeof(PivotTableStyle))]
	[ChildElementInfo(typeof(ConditionalFormats))]
	[ChildElementInfo(typeof(PivotFields))]
	[ChildElementInfo(typeof(RowFields))]
	[ChildElementInfo(typeof(RowItems))]
	[ChildElementInfo(typeof(ColumnFields))]
	[ChildElementInfo(typeof(ColumnItems))]
	[ChildElementInfo(typeof(PageFields))]
	[ChildElementInfo(typeof(DataFields))]
	[ChildElementInfo(typeof(Formats))]
	[ChildElementInfo(typeof(ColumnHierarchiesUsage))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PivotHierarchies))]
	[ChildElementInfo(typeof(PivotFilters))]
	[ChildElementInfo(typeof(RowHierarchiesUsage))]
	[ChildElementInfo(typeof(ChartFormats))]
	[ChildElementInfo(typeof(PivotTableDefinitionExtensionList))]
	internal class PivotTableDefinition : OpenXmlPartRootElement
	{
		// Token: 0x170075FD RID: 30205
		// (get) Token: 0x060166C7 RID: 91847 RVA: 0x002E621F File Offset: 0x002E441F
		public override string LocalName
		{
			get
			{
				return "pivotTableDefinition";
			}
		}

		// Token: 0x170075FE RID: 30206
		// (get) Token: 0x060166C8 RID: 91848 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170075FF RID: 30207
		// (get) Token: 0x060166C9 RID: 91849 RVA: 0x00329F97 File Offset: 0x00328197
		internal override int ElementTypeId
		{
			get
			{
				return 11033;
			}
		}

		// Token: 0x060166CA RID: 91850 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007600 RID: 30208
		// (get) Token: 0x060166CB RID: 91851 RVA: 0x00329F9E File Offset: 0x0032819E
		internal override string[] AttributeTagNames
		{
			get
			{
				return PivotTableDefinition.attributeTagNames;
			}
		}

		// Token: 0x17007601 RID: 30209
		// (get) Token: 0x060166CC RID: 91852 RVA: 0x00329FA5 File Offset: 0x003281A5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PivotTableDefinition.attributeNamespaceIds;
			}
		}

		// Token: 0x17007602 RID: 30210
		// (get) Token: 0x060166CD RID: 91853 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060166CE RID: 91854 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "name")]
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

		// Token: 0x17007603 RID: 30211
		// (get) Token: 0x060166CF RID: 91855 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x060166D0 RID: 91856 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "cacheId")]
		public UInt32Value CacheId
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007604 RID: 30212
		// (get) Token: 0x060166D1 RID: 91857 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x060166D2 RID: 91858 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "dataOnRows")]
		public BooleanValue DataOnRows
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17007605 RID: 30213
		// (get) Token: 0x060166D3 RID: 91859 RVA: 0x002E5B0D File Offset: 0x002E3D0D
		// (set) Token: 0x060166D4 RID: 91860 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "dataPosition")]
		public UInt32Value DataPosition
		{
			get
			{
				return (UInt32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17007606 RID: 30214
		// (get) Token: 0x060166D5 RID: 91861 RVA: 0x002E6C42 File Offset: 0x002E4E42
		// (set) Token: 0x060166D6 RID: 91862 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "autoFormatId")]
		public UInt32Value AutoFormatId
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

		// Token: 0x17007607 RID: 30215
		// (get) Token: 0x060166D7 RID: 91863 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x060166D8 RID: 91864 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "applyNumberFormats")]
		public BooleanValue ApplyNumberFormats
		{
			get
			{
				return (BooleanValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17007608 RID: 30216
		// (get) Token: 0x060166D9 RID: 91865 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x060166DA RID: 91866 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "applyBorderFormats")]
		public BooleanValue ApplyBorderFormats
		{
			get
			{
				return (BooleanValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17007609 RID: 30217
		// (get) Token: 0x060166DB RID: 91867 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x060166DC RID: 91868 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "applyFontFormats")]
		public BooleanValue ApplyFontFormats
		{
			get
			{
				return (BooleanValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x1700760A RID: 30218
		// (get) Token: 0x060166DD RID: 91869 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x060166DE RID: 91870 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "applyPatternFormats")]
		public BooleanValue ApplyPatternFormats
		{
			get
			{
				return (BooleanValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x1700760B RID: 30219
		// (get) Token: 0x060166DF RID: 91871 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x060166E0 RID: 91872 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "applyAlignmentFormats")]
		public BooleanValue ApplyAlignmentFormats
		{
			get
			{
				return (BooleanValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x1700760C RID: 30220
		// (get) Token: 0x060166E1 RID: 91873 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x060166E2 RID: 91874 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "applyWidthHeightFormats")]
		public BooleanValue ApplyWidthHeightFormats
		{
			get
			{
				return (BooleanValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x1700760D RID: 30221
		// (get) Token: 0x060166E3 RID: 91875 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x060166E4 RID: 91876 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "dataCaption")]
		public StringValue DataCaption
		{
			get
			{
				return (StringValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x1700760E RID: 30222
		// (get) Token: 0x060166E5 RID: 91877 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x060166E6 RID: 91878 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "grandTotalCaption")]
		public StringValue GrandTotalCaption
		{
			get
			{
				return (StringValue)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x1700760F RID: 30223
		// (get) Token: 0x060166E7 RID: 91879 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x060166E8 RID: 91880 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "errorCaption")]
		public StringValue ErrorCaption
		{
			get
			{
				return (StringValue)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x17007610 RID: 30224
		// (get) Token: 0x060166E9 RID: 91881 RVA: 0x002C9F8A File Offset: 0x002C818A
		// (set) Token: 0x060166EA RID: 91882 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "showError")]
		public BooleanValue ShowError
		{
			get
			{
				return (BooleanValue)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x17007611 RID: 30225
		// (get) Token: 0x060166EB RID: 91883 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x060166EC RID: 91884 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "missingCaption")]
		public StringValue MissingCaption
		{
			get
			{
				return (StringValue)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x17007612 RID: 30226
		// (get) Token: 0x060166ED RID: 91885 RVA: 0x002C930A File Offset: 0x002C750A
		// (set) Token: 0x060166EE RID: 91886 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "showMissing")]
		public BooleanValue ShowMissing
		{
			get
			{
				return (BooleanValue)base.Attributes[16];
			}
			set
			{
				base.Attributes[16] = value;
			}
		}

		// Token: 0x17007613 RID: 30227
		// (get) Token: 0x060166EF RID: 91887 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x060166F0 RID: 91888 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "pageStyle")]
		public StringValue PageStyle
		{
			get
			{
				return (StringValue)base.Attributes[17];
			}
			set
			{
				base.Attributes[17] = value;
			}
		}

		// Token: 0x17007614 RID: 30228
		// (get) Token: 0x060166F1 RID: 91889 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x060166F2 RID: 91890 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "pivotTableStyle")]
		public StringValue PivotTableStyleName
		{
			get
			{
				return (StringValue)base.Attributes[18];
			}
			set
			{
				base.Attributes[18] = value;
			}
		}

		// Token: 0x17007615 RID: 30229
		// (get) Token: 0x060166F3 RID: 91891 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x060166F4 RID: 91892 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "vacatedStyle")]
		public StringValue VacatedStyle
		{
			get
			{
				return (StringValue)base.Attributes[19];
			}
			set
			{
				base.Attributes[19] = value;
			}
		}

		// Token: 0x17007616 RID: 30230
		// (get) Token: 0x060166F5 RID: 91893 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x060166F6 RID: 91894 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "tag")]
		public StringValue Tag
		{
			get
			{
				return (StringValue)base.Attributes[20];
			}
			set
			{
				base.Attributes[20] = value;
			}
		}

		// Token: 0x17007617 RID: 30231
		// (get) Token: 0x060166F7 RID: 91895 RVA: 0x00329FAC File Offset: 0x003281AC
		// (set) Token: 0x060166F8 RID: 91896 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "updatedVersion")]
		public ByteValue UpdatedVersion
		{
			get
			{
				return (ByteValue)base.Attributes[21];
			}
			set
			{
				base.Attributes[21] = value;
			}
		}

		// Token: 0x17007618 RID: 30232
		// (get) Token: 0x060166F9 RID: 91897 RVA: 0x00329FBC File Offset: 0x003281BC
		// (set) Token: 0x060166FA RID: 91898 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "minRefreshableVersion")]
		public ByteValue MinRefreshableVersion
		{
			get
			{
				return (ByteValue)base.Attributes[22];
			}
			set
			{
				base.Attributes[22] = value;
			}
		}

		// Token: 0x17007619 RID: 30233
		// (get) Token: 0x060166FB RID: 91899 RVA: 0x002C99DC File Offset: 0x002C7BDC
		// (set) Token: 0x060166FC RID: 91900 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(0, "asteriskTotals")]
		public BooleanValue AsteriskTotals
		{
			get
			{
				return (BooleanValue)base.Attributes[23];
			}
			set
			{
				base.Attributes[23] = value;
			}
		}

		// Token: 0x1700761A RID: 30234
		// (get) Token: 0x060166FD RID: 91901 RVA: 0x002C87A2 File Offset: 0x002C69A2
		// (set) Token: 0x060166FE RID: 91902 RVA: 0x002BE33D File Offset: 0x002BC53D
		[SchemaAttr(0, "showItems")]
		public BooleanValue ShowItems
		{
			get
			{
				return (BooleanValue)base.Attributes[24];
			}
			set
			{
				base.Attributes[24] = value;
			}
		}

		// Token: 0x1700761B RID: 30235
		// (get) Token: 0x060166FF RID: 91903 RVA: 0x002CBE3C File Offset: 0x002CA03C
		// (set) Token: 0x06016700 RID: 91904 RVA: 0x002BE359 File Offset: 0x002BC559
		[SchemaAttr(0, "editData")]
		public BooleanValue EditData
		{
			get
			{
				return (BooleanValue)base.Attributes[25];
			}
			set
			{
				base.Attributes[25] = value;
			}
		}

		// Token: 0x1700761C RID: 30236
		// (get) Token: 0x06016701 RID: 91905 RVA: 0x002C8B45 File Offset: 0x002C6D45
		// (set) Token: 0x06016702 RID: 91906 RVA: 0x002BE375 File Offset: 0x002BC575
		[SchemaAttr(0, "disableFieldList")]
		public BooleanValue DisableFieldList
		{
			get
			{
				return (BooleanValue)base.Attributes[26];
			}
			set
			{
				base.Attributes[26] = value;
			}
		}

		// Token: 0x1700761D RID: 30237
		// (get) Token: 0x06016703 RID: 91907 RVA: 0x002C99EC File Offset: 0x002C7BEC
		// (set) Token: 0x06016704 RID: 91908 RVA: 0x002BE391 File Offset: 0x002BC591
		[SchemaAttr(0, "showCalcMbrs")]
		public BooleanValue ShowCalculatedMembers
		{
			get
			{
				return (BooleanValue)base.Attributes[27];
			}
			set
			{
				base.Attributes[27] = value;
			}
		}

		// Token: 0x1700761E RID: 30238
		// (get) Token: 0x06016705 RID: 91909 RVA: 0x002C8B55 File Offset: 0x002C6D55
		// (set) Token: 0x06016706 RID: 91910 RVA: 0x002BE3AD File Offset: 0x002BC5AD
		[SchemaAttr(0, "visualTotals")]
		public BooleanValue VisualTotals
		{
			get
			{
				return (BooleanValue)base.Attributes[28];
			}
			set
			{
				base.Attributes[28] = value;
			}
		}

		// Token: 0x1700761F RID: 30239
		// (get) Token: 0x06016707 RID: 91911 RVA: 0x002C99FC File Offset: 0x002C7BFC
		// (set) Token: 0x06016708 RID: 91912 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
		[SchemaAttr(0, "showMultipleLabel")]
		public BooleanValue ShowMultipleLabel
		{
			get
			{
				return (BooleanValue)base.Attributes[29];
			}
			set
			{
				base.Attributes[29] = value;
			}
		}

		// Token: 0x17007620 RID: 30240
		// (get) Token: 0x06016709 RID: 91913 RVA: 0x002CB9E8 File Offset: 0x002C9BE8
		// (set) Token: 0x0601670A RID: 91914 RVA: 0x002BE3E5 File Offset: 0x002BC5E5
		[SchemaAttr(0, "showDataDropDown")]
		public BooleanValue ShowDataDropDown
		{
			get
			{
				return (BooleanValue)base.Attributes[30];
			}
			set
			{
				base.Attributes[30] = value;
			}
		}

		// Token: 0x17007621 RID: 30241
		// (get) Token: 0x0601670B RID: 91915 RVA: 0x002CBE4C File Offset: 0x002CA04C
		// (set) Token: 0x0601670C RID: 91916 RVA: 0x002C1410 File Offset: 0x002BF610
		[SchemaAttr(0, "showDrill")]
		public BooleanValue ShowDrill
		{
			get
			{
				return (BooleanValue)base.Attributes[31];
			}
			set
			{
				base.Attributes[31] = value;
			}
		}

		// Token: 0x17007622 RID: 30242
		// (get) Token: 0x0601670D RID: 91917 RVA: 0x00329FCC File Offset: 0x003281CC
		// (set) Token: 0x0601670E RID: 91918 RVA: 0x002C142C File Offset: 0x002BF62C
		[SchemaAttr(0, "printDrill")]
		public BooleanValue PrintDrill
		{
			get
			{
				return (BooleanValue)base.Attributes[32];
			}
			set
			{
				base.Attributes[32] = value;
			}
		}

		// Token: 0x17007623 RID: 30243
		// (get) Token: 0x0601670F RID: 91919 RVA: 0x00329FDC File Offset: 0x003281DC
		// (set) Token: 0x06016710 RID: 91920 RVA: 0x002C1448 File Offset: 0x002BF648
		[SchemaAttr(0, "showMemberPropertyTips")]
		public BooleanValue ShowMemberPropertyTips
		{
			get
			{
				return (BooleanValue)base.Attributes[33];
			}
			set
			{
				base.Attributes[33] = value;
			}
		}

		// Token: 0x17007624 RID: 30244
		// (get) Token: 0x06016711 RID: 91921 RVA: 0x00329FEC File Offset: 0x003281EC
		// (set) Token: 0x06016712 RID: 91922 RVA: 0x002C1464 File Offset: 0x002BF664
		[SchemaAttr(0, "showDataTips")]
		public BooleanValue ShowDataTips
		{
			get
			{
				return (BooleanValue)base.Attributes[34];
			}
			set
			{
				base.Attributes[34] = value;
			}
		}

		// Token: 0x17007625 RID: 30245
		// (get) Token: 0x06016713 RID: 91923 RVA: 0x002CC6E3 File Offset: 0x002CA8E3
		// (set) Token: 0x06016714 RID: 91924 RVA: 0x002C1480 File Offset: 0x002BF680
		[SchemaAttr(0, "enableWizard")]
		public BooleanValue EnableWizard
		{
			get
			{
				return (BooleanValue)base.Attributes[35];
			}
			set
			{
				base.Attributes[35] = value;
			}
		}

		// Token: 0x17007626 RID: 30246
		// (get) Token: 0x06016715 RID: 91925 RVA: 0x00329FFC File Offset: 0x003281FC
		// (set) Token: 0x06016716 RID: 91926 RVA: 0x002C149C File Offset: 0x002BF69C
		[SchemaAttr(0, "enableDrill")]
		public BooleanValue EnableDrill
		{
			get
			{
				return (BooleanValue)base.Attributes[36];
			}
			set
			{
				base.Attributes[36] = value;
			}
		}

		// Token: 0x17007627 RID: 30247
		// (get) Token: 0x06016717 RID: 91927 RVA: 0x002CC6F3 File Offset: 0x002CA8F3
		// (set) Token: 0x06016718 RID: 91928 RVA: 0x002C14B8 File Offset: 0x002BF6B8
		[SchemaAttr(0, "enableFieldProperties")]
		public BooleanValue EnableFieldProperties
		{
			get
			{
				return (BooleanValue)base.Attributes[37];
			}
			set
			{
				base.Attributes[37] = value;
			}
		}

		// Token: 0x17007628 RID: 30248
		// (get) Token: 0x06016719 RID: 91929 RVA: 0x0032A00C File Offset: 0x0032820C
		// (set) Token: 0x0601671A RID: 91930 RVA: 0x002C14D4 File Offset: 0x002BF6D4
		[SchemaAttr(0, "preserveFormatting")]
		public BooleanValue PreserveFormatting
		{
			get
			{
				return (BooleanValue)base.Attributes[38];
			}
			set
			{
				base.Attributes[38] = value;
			}
		}

		// Token: 0x17007629 RID: 30249
		// (get) Token: 0x0601671B RID: 91931 RVA: 0x0032A01C File Offset: 0x0032821C
		// (set) Token: 0x0601671C RID: 91932 RVA: 0x002C14F0 File Offset: 0x002BF6F0
		[SchemaAttr(0, "useAutoFormatting")]
		public BooleanValue UseAutoFormatting
		{
			get
			{
				return (BooleanValue)base.Attributes[39];
			}
			set
			{
				base.Attributes[39] = value;
			}
		}

		// Token: 0x1700762A RID: 30250
		// (get) Token: 0x0601671D RID: 91933 RVA: 0x0032A02C File Offset: 0x0032822C
		// (set) Token: 0x0601671E RID: 91934 RVA: 0x002C150C File Offset: 0x002BF70C
		[SchemaAttr(0, "pageWrap")]
		public UInt32Value PageWrap
		{
			get
			{
				return (UInt32Value)base.Attributes[40];
			}
			set
			{
				base.Attributes[40] = value;
			}
		}

		// Token: 0x1700762B RID: 30251
		// (get) Token: 0x0601671F RID: 91935 RVA: 0x002D2251 File Offset: 0x002D0451
		// (set) Token: 0x06016720 RID: 91936 RVA: 0x002C1528 File Offset: 0x002BF728
		[SchemaAttr(0, "pageOverThenDown")]
		public BooleanValue PageOverThenDown
		{
			get
			{
				return (BooleanValue)base.Attributes[41];
			}
			set
			{
				base.Attributes[41] = value;
			}
		}

		// Token: 0x1700762C RID: 30252
		// (get) Token: 0x06016721 RID: 91937 RVA: 0x002CD16F File Offset: 0x002CB36F
		// (set) Token: 0x06016722 RID: 91938 RVA: 0x002C1544 File Offset: 0x002BF744
		[SchemaAttr(0, "subtotalHiddenItems")]
		public BooleanValue SubtotalHiddenItems
		{
			get
			{
				return (BooleanValue)base.Attributes[42];
			}
			set
			{
				base.Attributes[42] = value;
			}
		}

		// Token: 0x1700762D RID: 30253
		// (get) Token: 0x06016723 RID: 91939 RVA: 0x002D6090 File Offset: 0x002D4290
		// (set) Token: 0x06016724 RID: 91940 RVA: 0x002C1560 File Offset: 0x002BF760
		[SchemaAttr(0, "rowGrandTotals")]
		public BooleanValue RowGrandTotals
		{
			get
			{
				return (BooleanValue)base.Attributes[43];
			}
			set
			{
				base.Attributes[43] = value;
			}
		}

		// Token: 0x1700762E RID: 30254
		// (get) Token: 0x06016725 RID: 91941 RVA: 0x002C935A File Offset: 0x002C755A
		// (set) Token: 0x06016726 RID: 91942 RVA: 0x002C157C File Offset: 0x002BF77C
		[SchemaAttr(0, "colGrandTotals")]
		public BooleanValue ColumnGrandTotals
		{
			get
			{
				return (BooleanValue)base.Attributes[44];
			}
			set
			{
				base.Attributes[44] = value;
			}
		}

		// Token: 0x1700762F RID: 30255
		// (get) Token: 0x06016727 RID: 91943 RVA: 0x002D2261 File Offset: 0x002D0461
		// (set) Token: 0x06016728 RID: 91944 RVA: 0x002C1598 File Offset: 0x002BF798
		[SchemaAttr(0, "fieldPrintTitles")]
		public BooleanValue FieldPrintTitles
		{
			get
			{
				return (BooleanValue)base.Attributes[45];
			}
			set
			{
				base.Attributes[45] = value;
			}
		}

		// Token: 0x17007630 RID: 30256
		// (get) Token: 0x06016729 RID: 91945 RVA: 0x002C936A File Offset: 0x002C756A
		// (set) Token: 0x0601672A RID: 91946 RVA: 0x002C15B4 File Offset: 0x002BF7B4
		[SchemaAttr(0, "itemPrintTitles")]
		public BooleanValue ItemPrintTitles
		{
			get
			{
				return (BooleanValue)base.Attributes[46];
			}
			set
			{
				base.Attributes[46] = value;
			}
		}

		// Token: 0x17007631 RID: 30257
		// (get) Token: 0x0601672B RID: 91947 RVA: 0x002D2271 File Offset: 0x002D0471
		// (set) Token: 0x0601672C RID: 91948 RVA: 0x002C15D0 File Offset: 0x002BF7D0
		[SchemaAttr(0, "mergeItem")]
		public BooleanValue MergeItem
		{
			get
			{
				return (BooleanValue)base.Attributes[47];
			}
			set
			{
				base.Attributes[47] = value;
			}
		}

		// Token: 0x17007632 RID: 30258
		// (get) Token: 0x0601672D RID: 91949 RVA: 0x002CD17F File Offset: 0x002CB37F
		// (set) Token: 0x0601672E RID: 91950 RVA: 0x002C15EC File Offset: 0x002BF7EC
		[SchemaAttr(0, "showDropZones")]
		public BooleanValue ShowDropZones
		{
			get
			{
				return (BooleanValue)base.Attributes[48];
			}
			set
			{
				base.Attributes[48] = value;
			}
		}

		// Token: 0x17007633 RID: 30259
		// (get) Token: 0x0601672F RID: 91951 RVA: 0x0032A03C File Offset: 0x0032823C
		// (set) Token: 0x06016730 RID: 91952 RVA: 0x002C1608 File Offset: 0x002BF808
		[SchemaAttr(0, "createdVersion")]
		public ByteValue CreatedVersion
		{
			get
			{
				return (ByteValue)base.Attributes[49];
			}
			set
			{
				base.Attributes[49] = value;
			}
		}

		// Token: 0x17007634 RID: 30260
		// (get) Token: 0x06016731 RID: 91953 RVA: 0x0032A04C File Offset: 0x0032824C
		// (set) Token: 0x06016732 RID: 91954 RVA: 0x002C1624 File Offset: 0x002BF824
		[SchemaAttr(0, "indent")]
		public UInt32Value Indent
		{
			get
			{
				return (UInt32Value)base.Attributes[50];
			}
			set
			{
				base.Attributes[50] = value;
			}
		}

		// Token: 0x17007635 RID: 30261
		// (get) Token: 0x06016733 RID: 91955 RVA: 0x0032A05C File Offset: 0x0032825C
		// (set) Token: 0x06016734 RID: 91956 RVA: 0x002C1640 File Offset: 0x002BF840
		[SchemaAttr(0, "showEmptyRow")]
		public BooleanValue ShowEmptyRow
		{
			get
			{
				return (BooleanValue)base.Attributes[51];
			}
			set
			{
				base.Attributes[51] = value;
			}
		}

		// Token: 0x17007636 RID: 30262
		// (get) Token: 0x06016735 RID: 91957 RVA: 0x0032A06C File Offset: 0x0032826C
		// (set) Token: 0x06016736 RID: 91958 RVA: 0x002C165C File Offset: 0x002BF85C
		[SchemaAttr(0, "showEmptyCol")]
		public BooleanValue ShowEmptyColumn
		{
			get
			{
				return (BooleanValue)base.Attributes[52];
			}
			set
			{
				base.Attributes[52] = value;
			}
		}

		// Token: 0x17007637 RID: 30263
		// (get) Token: 0x06016737 RID: 91959 RVA: 0x0032A07C File Offset: 0x0032827C
		// (set) Token: 0x06016738 RID: 91960 RVA: 0x002C1678 File Offset: 0x002BF878
		[SchemaAttr(0, "showHeaders")]
		public BooleanValue ShowHeaders
		{
			get
			{
				return (BooleanValue)base.Attributes[53];
			}
			set
			{
				base.Attributes[53] = value;
			}
		}

		// Token: 0x17007638 RID: 30264
		// (get) Token: 0x06016739 RID: 91961 RVA: 0x0032A08C File Offset: 0x0032828C
		// (set) Token: 0x0601673A RID: 91962 RVA: 0x002C1694 File Offset: 0x002BF894
		[SchemaAttr(0, "compact")]
		public BooleanValue Compact
		{
			get
			{
				return (BooleanValue)base.Attributes[54];
			}
			set
			{
				base.Attributes[54] = value;
			}
		}

		// Token: 0x17007639 RID: 30265
		// (get) Token: 0x0601673B RID: 91963 RVA: 0x0032A09C File Offset: 0x0032829C
		// (set) Token: 0x0601673C RID: 91964 RVA: 0x002C16B0 File Offset: 0x002BF8B0
		[SchemaAttr(0, "outline")]
		public BooleanValue Outline
		{
			get
			{
				return (BooleanValue)base.Attributes[55];
			}
			set
			{
				base.Attributes[55] = value;
			}
		}

		// Token: 0x1700763A RID: 30266
		// (get) Token: 0x0601673D RID: 91965 RVA: 0x0032A0AC File Offset: 0x003282AC
		// (set) Token: 0x0601673E RID: 91966 RVA: 0x002C4825 File Offset: 0x002C2A25
		[SchemaAttr(0, "outlineData")]
		public BooleanValue OutlineData
		{
			get
			{
				return (BooleanValue)base.Attributes[56];
			}
			set
			{
				base.Attributes[56] = value;
			}
		}

		// Token: 0x1700763B RID: 30267
		// (get) Token: 0x0601673F RID: 91967 RVA: 0x0032A0BC File Offset: 0x003282BC
		// (set) Token: 0x06016740 RID: 91968 RVA: 0x002C4841 File Offset: 0x002C2A41
		[SchemaAttr(0, "compactData")]
		public BooleanValue CompactData
		{
			get
			{
				return (BooleanValue)base.Attributes[57];
			}
			set
			{
				base.Attributes[57] = value;
			}
		}

		// Token: 0x1700763C RID: 30268
		// (get) Token: 0x06016741 RID: 91969 RVA: 0x0032A0CC File Offset: 0x003282CC
		// (set) Token: 0x06016742 RID: 91970 RVA: 0x002C485D File Offset: 0x002C2A5D
		[SchemaAttr(0, "published")]
		public BooleanValue Published
		{
			get
			{
				return (BooleanValue)base.Attributes[58];
			}
			set
			{
				base.Attributes[58] = value;
			}
		}

		// Token: 0x1700763D RID: 30269
		// (get) Token: 0x06016743 RID: 91971 RVA: 0x0032A0DC File Offset: 0x003282DC
		// (set) Token: 0x06016744 RID: 91972 RVA: 0x002C4879 File Offset: 0x002C2A79
		[SchemaAttr(0, "gridDropZones")]
		public BooleanValue GridDropZones
		{
			get
			{
				return (BooleanValue)base.Attributes[59];
			}
			set
			{
				base.Attributes[59] = value;
			}
		}

		// Token: 0x1700763E RID: 30270
		// (get) Token: 0x06016745 RID: 91973 RVA: 0x0032A0EC File Offset: 0x003282EC
		// (set) Token: 0x06016746 RID: 91974 RVA: 0x0032A0FC File Offset: 0x003282FC
		[SchemaAttr(0, "immersive")]
		public BooleanValue StopImmersiveUi
		{
			get
			{
				return (BooleanValue)base.Attributes[60];
			}
			set
			{
				base.Attributes[60] = value;
			}
		}

		// Token: 0x1700763F RID: 30271
		// (get) Token: 0x06016747 RID: 91975 RVA: 0x0032A108 File Offset: 0x00328308
		// (set) Token: 0x06016748 RID: 91976 RVA: 0x0032A118 File Offset: 0x00328318
		[SchemaAttr(0, "multipleFieldFilters")]
		public BooleanValue MultipleFieldFilters
		{
			get
			{
				return (BooleanValue)base.Attributes[61];
			}
			set
			{
				base.Attributes[61] = value;
			}
		}

		// Token: 0x17007640 RID: 30272
		// (get) Token: 0x06016749 RID: 91977 RVA: 0x0032A124 File Offset: 0x00328324
		// (set) Token: 0x0601674A RID: 91978 RVA: 0x0032A134 File Offset: 0x00328334
		[SchemaAttr(0, "chartFormat")]
		public UInt32Value ChartFormat
		{
			get
			{
				return (UInt32Value)base.Attributes[62];
			}
			set
			{
				base.Attributes[62] = value;
			}
		}

		// Token: 0x17007641 RID: 30273
		// (get) Token: 0x0601674B RID: 91979 RVA: 0x0032A140 File Offset: 0x00328340
		// (set) Token: 0x0601674C RID: 91980 RVA: 0x0032A150 File Offset: 0x00328350
		[SchemaAttr(0, "rowHeaderCaption")]
		public StringValue RowHeaderCaption
		{
			get
			{
				return (StringValue)base.Attributes[63];
			}
			set
			{
				base.Attributes[63] = value;
			}
		}

		// Token: 0x17007642 RID: 30274
		// (get) Token: 0x0601674D RID: 91981 RVA: 0x0032A15C File Offset: 0x0032835C
		// (set) Token: 0x0601674E RID: 91982 RVA: 0x0032A16C File Offset: 0x0032836C
		[SchemaAttr(0, "colHeaderCaption")]
		public StringValue ColumnHeaderCaption
		{
			get
			{
				return (StringValue)base.Attributes[64];
			}
			set
			{
				base.Attributes[64] = value;
			}
		}

		// Token: 0x17007643 RID: 30275
		// (get) Token: 0x0601674F RID: 91983 RVA: 0x0032A178 File Offset: 0x00328378
		// (set) Token: 0x06016750 RID: 91984 RVA: 0x0032A188 File Offset: 0x00328388
		[SchemaAttr(0, "fieldListSortAscending")]
		public BooleanValue FieldListSortAscending
		{
			get
			{
				return (BooleanValue)base.Attributes[65];
			}
			set
			{
				base.Attributes[65] = value;
			}
		}

		// Token: 0x17007644 RID: 30276
		// (get) Token: 0x06016751 RID: 91985 RVA: 0x0032A194 File Offset: 0x00328394
		// (set) Token: 0x06016752 RID: 91986 RVA: 0x0032A1A4 File Offset: 0x003283A4
		[SchemaAttr(0, "customListSort")]
		public BooleanValue CustomListSort
		{
			get
			{
				return (BooleanValue)base.Attributes[66];
			}
			set
			{
				base.Attributes[66] = value;
			}
		}

		// Token: 0x06016753 RID: 91987 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal PivotTableDefinition(PivotTablePart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06016754 RID: 91988 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(PivotTablePart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17007645 RID: 30277
		// (get) Token: 0x06016755 RID: 91989 RVA: 0x0032A1B0 File Offset: 0x003283B0
		// (set) Token: 0x06016756 RID: 91990 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public PivotTablePart PivotTablePart
		{
			get
			{
				return base.OpenXmlPart as PivotTablePart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06016757 RID: 91991 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public PivotTableDefinition(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016758 RID: 91992 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public PivotTableDefinition(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016759 RID: 91993 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public PivotTableDefinition(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601675A RID: 91994 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public PivotTableDefinition()
		{
		}

		// Token: 0x0601675B RID: 91995 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(PivotTablePart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x0601675C RID: 91996 RVA: 0x0032A1C0 File Offset: 0x003283C0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "location" == name)
			{
				return new Location();
			}
			if (22 == namespaceId && "pivotFields" == name)
			{
				return new PivotFields();
			}
			if (22 == namespaceId && "rowFields" == name)
			{
				return new RowFields();
			}
			if (22 == namespaceId && "rowItems" == name)
			{
				return new RowItems();
			}
			if (22 == namespaceId && "colFields" == name)
			{
				return new ColumnFields();
			}
			if (22 == namespaceId && "colItems" == name)
			{
				return new ColumnItems();
			}
			if (22 == namespaceId && "pageFields" == name)
			{
				return new PageFields();
			}
			if (22 == namespaceId && "dataFields" == name)
			{
				return new DataFields();
			}
			if (22 == namespaceId && "formats" == name)
			{
				return new Formats();
			}
			if (22 == namespaceId && "conditionalFormats" == name)
			{
				return new ConditionalFormats();
			}
			if (22 == namespaceId && "chartFormats" == name)
			{
				return new ChartFormats();
			}
			if (22 == namespaceId && "pivotHierarchies" == name)
			{
				return new PivotHierarchies();
			}
			if (22 == namespaceId && "pivotTableStyleInfo" == name)
			{
				return new PivotTableStyle();
			}
			if (22 == namespaceId && "filters" == name)
			{
				return new PivotFilters();
			}
			if (22 == namespaceId && "rowHierarchiesUsage" == name)
			{
				return new RowHierarchiesUsage();
			}
			if (22 == namespaceId && "colHierarchiesUsage" == name)
			{
				return new ColumnHierarchiesUsage();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new PivotTableDefinitionExtensionList();
			}
			return null;
		}

		// Token: 0x17007646 RID: 30278
		// (get) Token: 0x0601675D RID: 91997 RVA: 0x0032A366 File Offset: 0x00328566
		internal override string[] ElementTagNames
		{
			get
			{
				return PivotTableDefinition.eleTagNames;
			}
		}

		// Token: 0x17007647 RID: 30279
		// (get) Token: 0x0601675E RID: 91998 RVA: 0x0032A36D File Offset: 0x0032856D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PivotTableDefinition.eleNamespaceIds;
			}
		}

		// Token: 0x17007648 RID: 30280
		// (get) Token: 0x0601675F RID: 91999 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007649 RID: 30281
		// (get) Token: 0x06016760 RID: 92000 RVA: 0x0032A374 File Offset: 0x00328574
		// (set) Token: 0x06016761 RID: 92001 RVA: 0x0032A37D File Offset: 0x0032857D
		public Location Location
		{
			get
			{
				return base.GetElement<Location>(0);
			}
			set
			{
				base.SetElement<Location>(0, value);
			}
		}

		// Token: 0x1700764A RID: 30282
		// (get) Token: 0x06016762 RID: 92002 RVA: 0x0032A387 File Offset: 0x00328587
		// (set) Token: 0x06016763 RID: 92003 RVA: 0x0032A390 File Offset: 0x00328590
		public PivotFields PivotFields
		{
			get
			{
				return base.GetElement<PivotFields>(1);
			}
			set
			{
				base.SetElement<PivotFields>(1, value);
			}
		}

		// Token: 0x1700764B RID: 30283
		// (get) Token: 0x06016764 RID: 92004 RVA: 0x0032A39A File Offset: 0x0032859A
		// (set) Token: 0x06016765 RID: 92005 RVA: 0x0032A3A3 File Offset: 0x003285A3
		public RowFields RowFields
		{
			get
			{
				return base.GetElement<RowFields>(2);
			}
			set
			{
				base.SetElement<RowFields>(2, value);
			}
		}

		// Token: 0x1700764C RID: 30284
		// (get) Token: 0x06016766 RID: 92006 RVA: 0x0032A3AD File Offset: 0x003285AD
		// (set) Token: 0x06016767 RID: 92007 RVA: 0x0032A3B6 File Offset: 0x003285B6
		public RowItems RowItems
		{
			get
			{
				return base.GetElement<RowItems>(3);
			}
			set
			{
				base.SetElement<RowItems>(3, value);
			}
		}

		// Token: 0x1700764D RID: 30285
		// (get) Token: 0x06016768 RID: 92008 RVA: 0x0032A3C0 File Offset: 0x003285C0
		// (set) Token: 0x06016769 RID: 92009 RVA: 0x0032A3C9 File Offset: 0x003285C9
		public ColumnFields ColumnFields
		{
			get
			{
				return base.GetElement<ColumnFields>(4);
			}
			set
			{
				base.SetElement<ColumnFields>(4, value);
			}
		}

		// Token: 0x1700764E RID: 30286
		// (get) Token: 0x0601676A RID: 92010 RVA: 0x0032A3D3 File Offset: 0x003285D3
		// (set) Token: 0x0601676B RID: 92011 RVA: 0x0032A3DC File Offset: 0x003285DC
		public ColumnItems ColumnItems
		{
			get
			{
				return base.GetElement<ColumnItems>(5);
			}
			set
			{
				base.SetElement<ColumnItems>(5, value);
			}
		}

		// Token: 0x1700764F RID: 30287
		// (get) Token: 0x0601676C RID: 92012 RVA: 0x0032A3E6 File Offset: 0x003285E6
		// (set) Token: 0x0601676D RID: 92013 RVA: 0x0032A3EF File Offset: 0x003285EF
		public PageFields PageFields
		{
			get
			{
				return base.GetElement<PageFields>(6);
			}
			set
			{
				base.SetElement<PageFields>(6, value);
			}
		}

		// Token: 0x17007650 RID: 30288
		// (get) Token: 0x0601676E RID: 92014 RVA: 0x0032A3F9 File Offset: 0x003285F9
		// (set) Token: 0x0601676F RID: 92015 RVA: 0x0032A402 File Offset: 0x00328602
		public DataFields DataFields
		{
			get
			{
				return base.GetElement<DataFields>(7);
			}
			set
			{
				base.SetElement<DataFields>(7, value);
			}
		}

		// Token: 0x17007651 RID: 30289
		// (get) Token: 0x06016770 RID: 92016 RVA: 0x0032A40C File Offset: 0x0032860C
		// (set) Token: 0x06016771 RID: 92017 RVA: 0x0032A415 File Offset: 0x00328615
		public Formats Formats
		{
			get
			{
				return base.GetElement<Formats>(8);
			}
			set
			{
				base.SetElement<Formats>(8, value);
			}
		}

		// Token: 0x17007652 RID: 30290
		// (get) Token: 0x06016772 RID: 92018 RVA: 0x0032A41F File Offset: 0x0032861F
		// (set) Token: 0x06016773 RID: 92019 RVA: 0x0032A429 File Offset: 0x00328629
		public ConditionalFormats ConditionalFormats
		{
			get
			{
				return base.GetElement<ConditionalFormats>(9);
			}
			set
			{
				base.SetElement<ConditionalFormats>(9, value);
			}
		}

		// Token: 0x17007653 RID: 30291
		// (get) Token: 0x06016774 RID: 92020 RVA: 0x0032A434 File Offset: 0x00328634
		// (set) Token: 0x06016775 RID: 92021 RVA: 0x0032A43E File Offset: 0x0032863E
		public ChartFormats ChartFormats
		{
			get
			{
				return base.GetElement<ChartFormats>(10);
			}
			set
			{
				base.SetElement<ChartFormats>(10, value);
			}
		}

		// Token: 0x17007654 RID: 30292
		// (get) Token: 0x06016776 RID: 92022 RVA: 0x0032A449 File Offset: 0x00328649
		// (set) Token: 0x06016777 RID: 92023 RVA: 0x0032A453 File Offset: 0x00328653
		public PivotHierarchies PivotHierarchies
		{
			get
			{
				return base.GetElement<PivotHierarchies>(11);
			}
			set
			{
				base.SetElement<PivotHierarchies>(11, value);
			}
		}

		// Token: 0x17007655 RID: 30293
		// (get) Token: 0x06016778 RID: 92024 RVA: 0x0032A45E File Offset: 0x0032865E
		// (set) Token: 0x06016779 RID: 92025 RVA: 0x0032A468 File Offset: 0x00328668
		public PivotTableStyle PivotTableStyle
		{
			get
			{
				return base.GetElement<PivotTableStyle>(12);
			}
			set
			{
				base.SetElement<PivotTableStyle>(12, value);
			}
		}

		// Token: 0x17007656 RID: 30294
		// (get) Token: 0x0601677A RID: 92026 RVA: 0x0032A473 File Offset: 0x00328673
		// (set) Token: 0x0601677B RID: 92027 RVA: 0x0032A47D File Offset: 0x0032867D
		public PivotFilters PivotFilters
		{
			get
			{
				return base.GetElement<PivotFilters>(13);
			}
			set
			{
				base.SetElement<PivotFilters>(13, value);
			}
		}

		// Token: 0x17007657 RID: 30295
		// (get) Token: 0x0601677C RID: 92028 RVA: 0x0032A488 File Offset: 0x00328688
		// (set) Token: 0x0601677D RID: 92029 RVA: 0x0032A492 File Offset: 0x00328692
		public RowHierarchiesUsage RowHierarchiesUsage
		{
			get
			{
				return base.GetElement<RowHierarchiesUsage>(14);
			}
			set
			{
				base.SetElement<RowHierarchiesUsage>(14, value);
			}
		}

		// Token: 0x17007658 RID: 30296
		// (get) Token: 0x0601677E RID: 92030 RVA: 0x0032A49D File Offset: 0x0032869D
		// (set) Token: 0x0601677F RID: 92031 RVA: 0x0032A4A7 File Offset: 0x003286A7
		public ColumnHierarchiesUsage ColumnHierarchiesUsage
		{
			get
			{
				return base.GetElement<ColumnHierarchiesUsage>(15);
			}
			set
			{
				base.SetElement<ColumnHierarchiesUsage>(15, value);
			}
		}

		// Token: 0x17007659 RID: 30297
		// (get) Token: 0x06016780 RID: 92032 RVA: 0x0032A4B2 File Offset: 0x003286B2
		// (set) Token: 0x06016781 RID: 92033 RVA: 0x0032A4BC File Offset: 0x003286BC
		public PivotTableDefinitionExtensionList PivotTableDefinitionExtensionList
		{
			get
			{
				return base.GetElement<PivotTableDefinitionExtensionList>(16);
			}
			set
			{
				base.SetElement<PivotTableDefinitionExtensionList>(16, value);
			}
		}

		// Token: 0x06016782 RID: 92034 RVA: 0x0032A4C8 File Offset: 0x003286C8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "cacheId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "dataOnRows" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "dataPosition" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "autoFormatId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "applyNumberFormats" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "applyBorderFormats" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "applyFontFormats" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "applyPatternFormats" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "applyAlignmentFormats" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "applyWidthHeightFormats" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "dataCaption" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "grandTotalCaption" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "errorCaption" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "showError" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "missingCaption" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "showMissing" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "pageStyle" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "pivotTableStyle" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "vacatedStyle" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "tag" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "updatedVersion" == name)
			{
				return new ByteValue();
			}
			if (namespaceId == 0 && "minRefreshableVersion" == name)
			{
				return new ByteValue();
			}
			if (namespaceId == 0 && "asteriskTotals" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showItems" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "editData" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "disableFieldList" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showCalcMbrs" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "visualTotals" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showMultipleLabel" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showDataDropDown" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showDrill" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "printDrill" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showMemberPropertyTips" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showDataTips" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "enableWizard" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "enableDrill" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "enableFieldProperties" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "preserveFormatting" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "useAutoFormatting" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "pageWrap" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "pageOverThenDown" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "subtotalHiddenItems" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "rowGrandTotals" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "colGrandTotals" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "fieldPrintTitles" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "itemPrintTitles" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "mergeItem" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showDropZones" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "createdVersion" == name)
			{
				return new ByteValue();
			}
			if (namespaceId == 0 && "indent" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "showEmptyRow" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showEmptyCol" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showHeaders" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "compact" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "outline" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "outlineData" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "compactData" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "published" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "gridDropZones" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "immersive" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "multipleFieldFilters" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "chartFormat" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "rowHeaderCaption" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "colHeaderCaption" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "fieldListSortAscending" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "customListSort" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016783 RID: 92035 RVA: 0x0032AA9F File Offset: 0x00328C9F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotTableDefinition>(deep);
		}

		// Token: 0x06016784 RID: 92036 RVA: 0x0032AAA8 File Offset: 0x00328CA8
		// Note: this type is marked as 'beforefieldinit'.
		static PivotTableDefinition()
		{
			byte[] array = new byte[67];
			PivotTableDefinition.attributeNamespaceIds = array;
			PivotTableDefinition.eleTagNames = new string[]
			{
				"location", "pivotFields", "rowFields", "rowItems", "colFields", "colItems", "pageFields", "dataFields", "formats", "conditionalFormats",
				"chartFormats", "pivotHierarchies", "pivotTableStyleInfo", "filters", "rowHierarchiesUsage", "colHierarchiesUsage", "extLst"
			};
			PivotTableDefinition.eleNamespaceIds = new byte[]
			{
				22, 22, 22, 22, 22, 22, 22, 22, 22, 22,
				22, 22, 22, 22, 22, 22, 22
			};
		}

		// Token: 0x040098E6 RID: 39142
		private const string tagName = "pivotTableDefinition";

		// Token: 0x040098E7 RID: 39143
		private const byte tagNsId = 22;

		// Token: 0x040098E8 RID: 39144
		internal const int ElementTypeIdConst = 11033;

		// Token: 0x040098E9 RID: 39145
		private static string[] attributeTagNames = new string[]
		{
			"name", "cacheId", "dataOnRows", "dataPosition", "autoFormatId", "applyNumberFormats", "applyBorderFormats", "applyFontFormats", "applyPatternFormats", "applyAlignmentFormats",
			"applyWidthHeightFormats", "dataCaption", "grandTotalCaption", "errorCaption", "showError", "missingCaption", "showMissing", "pageStyle", "pivotTableStyle", "vacatedStyle",
			"tag", "updatedVersion", "minRefreshableVersion", "asteriskTotals", "showItems", "editData", "disableFieldList", "showCalcMbrs", "visualTotals", "showMultipleLabel",
			"showDataDropDown", "showDrill", "printDrill", "showMemberPropertyTips", "showDataTips", "enableWizard", "enableDrill", "enableFieldProperties", "preserveFormatting", "useAutoFormatting",
			"pageWrap", "pageOverThenDown", "subtotalHiddenItems", "rowGrandTotals", "colGrandTotals", "fieldPrintTitles", "itemPrintTitles", "mergeItem", "showDropZones", "createdVersion",
			"indent", "showEmptyRow", "showEmptyCol", "showHeaders", "compact", "outline", "outlineData", "compactData", "published", "gridDropZones",
			"immersive", "multipleFieldFilters", "chartFormat", "rowHeaderCaption", "colHeaderCaption", "fieldListSortAscending", "customListSort"
		};

		// Token: 0x040098EA RID: 39146
		private static byte[] attributeNamespaceIds;

		// Token: 0x040098EB RID: 39147
		private static readonly string[] eleTagNames;

		// Token: 0x040098EC RID: 39148
		private static readonly byte[] eleNamespaceIds;
	}
}
