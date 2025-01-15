using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022BD RID: 8893
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Item), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ButtonRegular), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class GalleryRegular : OpenXmlCompositeElement
	{
		// Token: 0x170041E8 RID: 16872
		// (get) Token: 0x0600F25D RID: 62045 RVA: 0x002C92B0 File Offset: 0x002C74B0
		public override string LocalName
		{
			get
			{
				return "gallery";
			}
		}

		// Token: 0x170041E9 RID: 16873
		// (get) Token: 0x0600F25E RID: 62046 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x170041EA RID: 16874
		// (get) Token: 0x0600F25F RID: 62047 RVA: 0x002D222C File Offset: 0x002D042C
		internal override int ElementTypeId
		{
			get
			{
				return 13038;
			}
		}

		// Token: 0x0600F260 RID: 62048 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170041EB RID: 16875
		// (get) Token: 0x0600F261 RID: 62049 RVA: 0x002D2233 File Offset: 0x002D0433
		internal override string[] AttributeTagNames
		{
			get
			{
				return GalleryRegular.attributeTagNames;
			}
		}

		// Token: 0x170041EC RID: 16876
		// (get) Token: 0x0600F262 RID: 62050 RVA: 0x002D223A File Offset: 0x002D043A
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return GalleryRegular.attributeNamespaceIds;
			}
		}

		// Token: 0x170041ED RID: 16877
		// (get) Token: 0x0600F263 RID: 62051 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600F264 RID: 62052 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "description")]
		public StringValue Description
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

		// Token: 0x170041EE RID: 16878
		// (get) Token: 0x0600F265 RID: 62053 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600F266 RID: 62054 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "getDescription")]
		public StringValue GetDescription
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170041EF RID: 16879
		// (get) Token: 0x0600F267 RID: 62055 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x0600F268 RID: 62056 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "invalidateContentOnDrop")]
		public BooleanValue InvalidateContentOnDrop
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

		// Token: 0x170041F0 RID: 16880
		// (get) Token: 0x0600F269 RID: 62057 RVA: 0x002C92CC File Offset: 0x002C74CC
		// (set) Token: 0x0600F26A RID: 62058 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "columns")]
		public IntegerValue Columns
		{
			get
			{
				return (IntegerValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x170041F1 RID: 16881
		// (get) Token: 0x0600F26B RID: 62059 RVA: 0x002C92DB File Offset: 0x002C74DB
		// (set) Token: 0x0600F26C RID: 62060 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "rows")]
		public IntegerValue Rows
		{
			get
			{
				return (IntegerValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x170041F2 RID: 16882
		// (get) Token: 0x0600F26D RID: 62061 RVA: 0x002BDE3A File Offset: 0x002BC03A
		// (set) Token: 0x0600F26E RID: 62062 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "itemWidth")]
		public IntegerValue ItemWidth
		{
			get
			{
				return (IntegerValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x170041F3 RID: 16883
		// (get) Token: 0x0600F26F RID: 62063 RVA: 0x002BDE49 File Offset: 0x002BC049
		// (set) Token: 0x0600F270 RID: 62064 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "itemHeight")]
		public IntegerValue ItemHeight
		{
			get
			{
				return (IntegerValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x170041F4 RID: 16884
		// (get) Token: 0x0600F271 RID: 62065 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600F272 RID: 62066 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "getItemWidth")]
		public StringValue GetItemWidth
		{
			get
			{
				return (StringValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x170041F5 RID: 16885
		// (get) Token: 0x0600F273 RID: 62067 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600F274 RID: 62068 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "getItemHeight")]
		public StringValue GetItemHeight
		{
			get
			{
				return (StringValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x170041F6 RID: 16886
		// (get) Token: 0x0600F275 RID: 62069 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x0600F276 RID: 62070 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "showItemLabel")]
		public BooleanValue ShowItemLabel
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

		// Token: 0x170041F7 RID: 16887
		// (get) Token: 0x0600F277 RID: 62071 RVA: 0x002D2241 File Offset: 0x002D0441
		// (set) Token: 0x0600F278 RID: 62072 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "showInRibbon")]
		public EnumValue<GalleryShowInRibbonValues> ShowInRibbon
		{
			get
			{
				return (EnumValue<GalleryShowInRibbonValues>)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x170041F8 RID: 16888
		// (get) Token: 0x0600F279 RID: 62073 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600F27A RID: 62074 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "onAction")]
		public StringValue OnAction
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

		// Token: 0x170041F9 RID: 16889
		// (get) Token: 0x0600F27B RID: 62075 RVA: 0x002CE196 File Offset: 0x002CC396
		// (set) Token: 0x0600F27C RID: 62076 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
		{
			get
			{
				return (BooleanValue)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x170041FA RID: 16890
		// (get) Token: 0x0600F27D RID: 62077 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600F27E RID: 62078 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x170041FB RID: 16891
		// (get) Token: 0x0600F27F RID: 62079 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600F280 RID: 62080 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "image")]
		public StringValue Image
		{
			get
			{
				return (StringValue)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x170041FC RID: 16892
		// (get) Token: 0x0600F281 RID: 62081 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600F282 RID: 62082 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "imageMso")]
		public StringValue ImageMso
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

		// Token: 0x170041FD RID: 16893
		// (get) Token: 0x0600F283 RID: 62083 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600F284 RID: 62084 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "getImage")]
		public StringValue GetImage
		{
			get
			{
				return (StringValue)base.Attributes[16];
			}
			set
			{
				base.Attributes[16] = value;
			}
		}

		// Token: 0x170041FE RID: 16894
		// (get) Token: 0x0600F285 RID: 62085 RVA: 0x002CDD31 File Offset: 0x002CBF31
		// (set) Token: 0x0600F286 RID: 62086 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "showItemImage")]
		public BooleanValue ShowItemImage
		{
			get
			{
				return (BooleanValue)base.Attributes[17];
			}
			set
			{
				base.Attributes[17] = value;
			}
		}

		// Token: 0x170041FF RID: 16895
		// (get) Token: 0x0600F287 RID: 62087 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600F288 RID: 62088 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "getItemCount")]
		public StringValue GetItemCount
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

		// Token: 0x17004200 RID: 16896
		// (get) Token: 0x0600F289 RID: 62089 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600F28A RID: 62090 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "getItemLabel")]
		public StringValue GetItemLabel
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

		// Token: 0x17004201 RID: 16897
		// (get) Token: 0x0600F28B RID: 62091 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600F28C RID: 62092 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "getItemScreentip")]
		public StringValue GetItemScreentip
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

		// Token: 0x17004202 RID: 16898
		// (get) Token: 0x0600F28D RID: 62093 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600F28E RID: 62094 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "getItemSupertip")]
		public StringValue GetItemSupertip
		{
			get
			{
				return (StringValue)base.Attributes[21];
			}
			set
			{
				base.Attributes[21] = value;
			}
		}

		// Token: 0x17004203 RID: 16899
		// (get) Token: 0x0600F28F RID: 62095 RVA: 0x002BE2F5 File Offset: 0x002BC4F5
		// (set) Token: 0x0600F290 RID: 62096 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "getItemImage")]
		public StringValue GetItemImage
		{
			get
			{
				return (StringValue)base.Attributes[22];
			}
			set
			{
				base.Attributes[22] = value;
			}
		}

		// Token: 0x17004204 RID: 16900
		// (get) Token: 0x0600F291 RID: 62097 RVA: 0x002BEFAF File Offset: 0x002BD1AF
		// (set) Token: 0x0600F292 RID: 62098 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(0, "getItemID")]
		public StringValue GetItemID
		{
			get
			{
				return (StringValue)base.Attributes[23];
			}
			set
			{
				base.Attributes[23] = value;
			}
		}

		// Token: 0x17004205 RID: 16901
		// (get) Token: 0x0600F293 RID: 62099 RVA: 0x002BE32D File Offset: 0x002BC52D
		// (set) Token: 0x0600F294 RID: 62100 RVA: 0x002BE33D File Offset: 0x002BC53D
		[SchemaAttr(0, "sizeString")]
		public StringValue SizeString
		{
			get
			{
				return (StringValue)base.Attributes[24];
			}
			set
			{
				base.Attributes[24] = value;
			}
		}

		// Token: 0x17004206 RID: 16902
		// (get) Token: 0x0600F295 RID: 62101 RVA: 0x002BE349 File Offset: 0x002BC549
		// (set) Token: 0x0600F296 RID: 62102 RVA: 0x002BE359 File Offset: 0x002BC559
		[SchemaAttr(0, "getSelectedItemID")]
		public StringValue GetSelectedItemID
		{
			get
			{
				return (StringValue)base.Attributes[25];
			}
			set
			{
				base.Attributes[25] = value;
			}
		}

		// Token: 0x17004207 RID: 16903
		// (get) Token: 0x0600F297 RID: 62103 RVA: 0x002BE365 File Offset: 0x002BC565
		// (set) Token: 0x0600F298 RID: 62104 RVA: 0x002BE375 File Offset: 0x002BC575
		[SchemaAttr(0, "getSelectedItemIndex")]
		public StringValue GetSelectedItemIndex
		{
			get
			{
				return (StringValue)base.Attributes[26];
			}
			set
			{
				base.Attributes[26] = value;
			}
		}

		// Token: 0x17004208 RID: 16904
		// (get) Token: 0x0600F299 RID: 62105 RVA: 0x002C13E0 File Offset: 0x002BF5E0
		// (set) Token: 0x0600F29A RID: 62106 RVA: 0x002BE391 File Offset: 0x002BC591
		[SchemaAttr(0, "id")]
		public StringValue Id
		{
			get
			{
				return (StringValue)base.Attributes[27];
			}
			set
			{
				base.Attributes[27] = value;
			}
		}

		// Token: 0x17004209 RID: 16905
		// (get) Token: 0x0600F29B RID: 62107 RVA: 0x002BE39D File Offset: 0x002BC59D
		// (set) Token: 0x0600F29C RID: 62108 RVA: 0x002BE3AD File Offset: 0x002BC5AD
		[SchemaAttr(0, "idQ")]
		public StringValue QualifiedId
		{
			get
			{
				return (StringValue)base.Attributes[28];
			}
			set
			{
				base.Attributes[28] = value;
			}
		}

		// Token: 0x1700420A RID: 16906
		// (get) Token: 0x0600F29D RID: 62109 RVA: 0x002BE3B9 File Offset: 0x002BC5B9
		// (set) Token: 0x0600F29E RID: 62110 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
		[SchemaAttr(0, "tag")]
		public StringValue Tag
		{
			get
			{
				return (StringValue)base.Attributes[29];
			}
			set
			{
				base.Attributes[29] = value;
			}
		}

		// Token: 0x1700420B RID: 16907
		// (get) Token: 0x0600F29F RID: 62111 RVA: 0x002C931A File Offset: 0x002C751A
		// (set) Token: 0x0600F2A0 RID: 62112 RVA: 0x002BE3E5 File Offset: 0x002BC5E5
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
		{
			get
			{
				return (StringValue)base.Attributes[30];
			}
			set
			{
				base.Attributes[30] = value;
			}
		}

		// Token: 0x1700420C RID: 16908
		// (get) Token: 0x0600F2A1 RID: 62113 RVA: 0x002C2A6B File Offset: 0x002C0C6B
		// (set) Token: 0x0600F2A2 RID: 62114 RVA: 0x002C1410 File Offset: 0x002BF610
		[SchemaAttr(0, "screentip")]
		public StringValue Screentip
		{
			get
			{
				return (StringValue)base.Attributes[31];
			}
			set
			{
				base.Attributes[31] = value;
			}
		}

		// Token: 0x1700420D RID: 16909
		// (get) Token: 0x0600F2A3 RID: 62115 RVA: 0x002C2A7B File Offset: 0x002C0C7B
		// (set) Token: 0x0600F2A4 RID: 62116 RVA: 0x002C142C File Offset: 0x002BF62C
		[SchemaAttr(0, "getScreentip")]
		public StringValue GetScreentip
		{
			get
			{
				return (StringValue)base.Attributes[32];
			}
			set
			{
				base.Attributes[32] = value;
			}
		}

		// Token: 0x1700420E RID: 16910
		// (get) Token: 0x0600F2A5 RID: 62117 RVA: 0x002C932A File Offset: 0x002C752A
		// (set) Token: 0x0600F2A6 RID: 62118 RVA: 0x002C1448 File Offset: 0x002BF648
		[SchemaAttr(0, "supertip")]
		public StringValue Supertip
		{
			get
			{
				return (StringValue)base.Attributes[33];
			}
			set
			{
				base.Attributes[33] = value;
			}
		}

		// Token: 0x1700420F RID: 16911
		// (get) Token: 0x0600F2A7 RID: 62119 RVA: 0x002C4785 File Offset: 0x002C2985
		// (set) Token: 0x0600F2A8 RID: 62120 RVA: 0x002C1464 File Offset: 0x002BF664
		[SchemaAttr(0, "getSupertip")]
		public StringValue GetSupertip
		{
			get
			{
				return (StringValue)base.Attributes[34];
			}
			set
			{
				base.Attributes[34] = value;
			}
		}

		// Token: 0x17004210 RID: 16912
		// (get) Token: 0x0600F2A9 RID: 62121 RVA: 0x002C1470 File Offset: 0x002BF670
		// (set) Token: 0x0600F2AA RID: 62122 RVA: 0x002C1480 File Offset: 0x002BF680
		[SchemaAttr(0, "label")]
		public StringValue Label
		{
			get
			{
				return (StringValue)base.Attributes[35];
			}
			set
			{
				base.Attributes[35] = value;
			}
		}

		// Token: 0x17004211 RID: 16913
		// (get) Token: 0x0600F2AB RID: 62123 RVA: 0x002C4795 File Offset: 0x002C2995
		// (set) Token: 0x0600F2AC RID: 62124 RVA: 0x002C149C File Offset: 0x002BF69C
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
		{
			get
			{
				return (StringValue)base.Attributes[36];
			}
			set
			{
				base.Attributes[36] = value;
			}
		}

		// Token: 0x17004212 RID: 16914
		// (get) Token: 0x0600F2AD RID: 62125 RVA: 0x002C14A8 File Offset: 0x002BF6A8
		// (set) Token: 0x0600F2AE RID: 62126 RVA: 0x002C14B8 File Offset: 0x002BF6B8
		[SchemaAttr(0, "insertAfterMso")]
		public StringValue InsertAfterMso
		{
			get
			{
				return (StringValue)base.Attributes[37];
			}
			set
			{
				base.Attributes[37] = value;
			}
		}

		// Token: 0x17004213 RID: 16915
		// (get) Token: 0x0600F2AF RID: 62127 RVA: 0x002C14C4 File Offset: 0x002BF6C4
		// (set) Token: 0x0600F2B0 RID: 62128 RVA: 0x002C14D4 File Offset: 0x002BF6D4
		[SchemaAttr(0, "insertBeforeMso")]
		public StringValue InsertBeforeMso
		{
			get
			{
				return (StringValue)base.Attributes[38];
			}
			set
			{
				base.Attributes[38] = value;
			}
		}

		// Token: 0x17004214 RID: 16916
		// (get) Token: 0x0600F2B1 RID: 62129 RVA: 0x002C933A File Offset: 0x002C753A
		// (set) Token: 0x0600F2B2 RID: 62130 RVA: 0x002C14F0 File Offset: 0x002BF6F0
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQulifiedId
		{
			get
			{
				return (StringValue)base.Attributes[39];
			}
			set
			{
				base.Attributes[39] = value;
			}
		}

		// Token: 0x17004215 RID: 16917
		// (get) Token: 0x0600F2B3 RID: 62131 RVA: 0x002C33A2 File Offset: 0x002C15A2
		// (set) Token: 0x0600F2B4 RID: 62132 RVA: 0x002C150C File Offset: 0x002BF70C
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQulifiedId
		{
			get
			{
				return (StringValue)base.Attributes[40];
			}
			set
			{
				base.Attributes[40] = value;
			}
		}

		// Token: 0x17004216 RID: 16918
		// (get) Token: 0x0600F2B5 RID: 62133 RVA: 0x002D2251 File Offset: 0x002D0451
		// (set) Token: 0x0600F2B6 RID: 62134 RVA: 0x002C1528 File Offset: 0x002BF728
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
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

		// Token: 0x17004217 RID: 16919
		// (get) Token: 0x0600F2B7 RID: 62135 RVA: 0x002C33C2 File Offset: 0x002C15C2
		// (set) Token: 0x0600F2B8 RID: 62136 RVA: 0x002C1544 File Offset: 0x002BF744
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
		{
			get
			{
				return (StringValue)base.Attributes[42];
			}
			set
			{
				base.Attributes[42] = value;
			}
		}

		// Token: 0x17004218 RID: 16920
		// (get) Token: 0x0600F2B9 RID: 62137 RVA: 0x002C33D2 File Offset: 0x002C15D2
		// (set) Token: 0x0600F2BA RID: 62138 RVA: 0x002C1560 File Offset: 0x002BF760
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
		{
			get
			{
				return (StringValue)base.Attributes[43];
			}
			set
			{
				base.Attributes[43] = value;
			}
		}

		// Token: 0x17004219 RID: 16921
		// (get) Token: 0x0600F2BB RID: 62139 RVA: 0x002C33E2 File Offset: 0x002C15E2
		// (set) Token: 0x0600F2BC RID: 62140 RVA: 0x002C157C File Offset: 0x002BF77C
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
		{
			get
			{
				return (StringValue)base.Attributes[44];
			}
			set
			{
				base.Attributes[44] = value;
			}
		}

		// Token: 0x1700421A RID: 16922
		// (get) Token: 0x0600F2BD RID: 62141 RVA: 0x002D2261 File Offset: 0x002D0461
		// (set) Token: 0x0600F2BE RID: 62142 RVA: 0x002C1598 File Offset: 0x002BF798
		[SchemaAttr(0, "showLabel")]
		public BooleanValue ShowLabel
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

		// Token: 0x1700421B RID: 16923
		// (get) Token: 0x0600F2BF RID: 62143 RVA: 0x002C3402 File Offset: 0x002C1602
		// (set) Token: 0x0600F2C0 RID: 62144 RVA: 0x002C15B4 File Offset: 0x002BF7B4
		[SchemaAttr(0, "getShowLabel")]
		public StringValue GetShowLabel
		{
			get
			{
				return (StringValue)base.Attributes[46];
			}
			set
			{
				base.Attributes[46] = value;
			}
		}

		// Token: 0x1700421C RID: 16924
		// (get) Token: 0x0600F2C1 RID: 62145 RVA: 0x002D2271 File Offset: 0x002D0471
		// (set) Token: 0x0600F2C2 RID: 62146 RVA: 0x002C15D0 File Offset: 0x002BF7D0
		[SchemaAttr(0, "showImage")]
		public BooleanValue ShowImage
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

		// Token: 0x1700421D RID: 16925
		// (get) Token: 0x0600F2C3 RID: 62147 RVA: 0x002C3422 File Offset: 0x002C1622
		// (set) Token: 0x0600F2C4 RID: 62148 RVA: 0x002C15EC File Offset: 0x002BF7EC
		[SchemaAttr(0, "getShowImage")]
		public StringValue GetShowImage
		{
			get
			{
				return (StringValue)base.Attributes[48];
			}
			set
			{
				base.Attributes[48] = value;
			}
		}

		// Token: 0x0600F2C5 RID: 62149 RVA: 0x00293ECF File Offset: 0x002920CF
		public GalleryRegular()
		{
		}

		// Token: 0x0600F2C6 RID: 62150 RVA: 0x00293ED7 File Offset: 0x002920D7
		public GalleryRegular(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F2C7 RID: 62151 RVA: 0x00293EE0 File Offset: 0x002920E0
		public GalleryRegular(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F2C8 RID: 62152 RVA: 0x00293EE9 File Offset: 0x002920E9
		public GalleryRegular(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600F2C9 RID: 62153 RVA: 0x002D2281 File Offset: 0x002D0481
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (57 == namespaceId && "item" == name)
			{
				return new Item();
			}
			if (57 == namespaceId && "button" == name)
			{
				return new ButtonRegular();
			}
			return null;
		}

		// Token: 0x0600F2CA RID: 62154 RVA: 0x002D22B4 File Offset: 0x002D04B4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "description" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getDescription" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "invalidateContentOnDrop" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "columns" == name)
			{
				return new IntegerValue();
			}
			if (namespaceId == 0 && "rows" == name)
			{
				return new IntegerValue();
			}
			if (namespaceId == 0 && "itemWidth" == name)
			{
				return new IntegerValue();
			}
			if (namespaceId == 0 && "itemHeight" == name)
			{
				return new IntegerValue();
			}
			if (namespaceId == 0 && "getItemWidth" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getItemHeight" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "showItemLabel" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "showInRibbon" == name)
			{
				return new EnumValue<GalleryShowInRibbonValues>();
			}
			if (namespaceId == 0 && "onAction" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "enabled" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getEnabled" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "image" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "imageMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getImage" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "showItemImage" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getItemCount" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getItemLabel" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getItemScreentip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getItemSupertip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getItemImage" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getItemID" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "sizeString" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getSelectedItemID" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getSelectedItemIndex" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "idQ" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "tag" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "idMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "screentip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getScreentip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "supertip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getSupertip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "label" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getLabel" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertAfterMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertBeforeMso" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertAfterQ" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "insertBeforeQ" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "visible" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getVisible" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "keytip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getKeytip" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "showLabel" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getShowLabel" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "showImage" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "getShowImage" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600F2CB RID: 62155 RVA: 0x002D26FF File Offset: 0x002D08FF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GalleryRegular>(deep);
		}

		// Token: 0x0600F2CC RID: 62156 RVA: 0x002D2708 File Offset: 0x002D0908
		// Note: this type is marked as 'beforefieldinit'.
		static GalleryRegular()
		{
			byte[] array = new byte[49];
			GalleryRegular.attributeNamespaceIds = array;
		}

		// Token: 0x040070DE RID: 28894
		private const string tagName = "gallery";

		// Token: 0x040070DF RID: 28895
		private const byte tagNsId = 57;

		// Token: 0x040070E0 RID: 28896
		internal const int ElementTypeIdConst = 13038;

		// Token: 0x040070E1 RID: 28897
		private static string[] attributeTagNames = new string[]
		{
			"description", "getDescription", "invalidateContentOnDrop", "columns", "rows", "itemWidth", "itemHeight", "getItemWidth", "getItemHeight", "showItemLabel",
			"showInRibbon", "onAction", "enabled", "getEnabled", "image", "imageMso", "getImage", "showItemImage", "getItemCount", "getItemLabel",
			"getItemScreentip", "getItemSupertip", "getItemImage", "getItemID", "sizeString", "getSelectedItemID", "getSelectedItemIndex", "id", "idQ", "tag",
			"idMso", "screentip", "getScreentip", "supertip", "getSupertip", "label", "getLabel", "insertAfterMso", "insertBeforeMso", "insertAfterQ",
			"insertBeforeQ", "visible", "getVisible", "keytip", "getKeytip", "showLabel", "getShowLabel", "showImage", "getShowImage"
		};

		// Token: 0x040070E2 RID: 28898
		private static byte[] attributeNamespaceIds;
	}
}
