using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022CC RID: 8908
	[ChildElementInfo(typeof(Item), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ButtonRegular), FileFormatVersions.Office2010)]
	internal class DropDownRegular : OpenXmlCompositeElement
	{
		// Token: 0x170043C7 RID: 17351
		// (get) Token: 0x0600F633 RID: 63027 RVA: 0x002CCC17 File Offset: 0x002CAE17
		public override string LocalName
		{
			get
			{
				return "dropDown";
			}
		}

		// Token: 0x170043C8 RID: 17352
		// (get) Token: 0x0600F634 RID: 63028 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x170043C9 RID: 17353
		// (get) Token: 0x0600F635 RID: 63029 RVA: 0x002D5B4F File Offset: 0x002D3D4F
		internal override int ElementTypeId
		{
			get
			{
				return 13053;
			}
		}

		// Token: 0x0600F636 RID: 63030 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170043CA RID: 17354
		// (get) Token: 0x0600F637 RID: 63031 RVA: 0x002D5B56 File Offset: 0x002D3D56
		internal override string[] AttributeTagNames
		{
			get
			{
				return DropDownRegular.attributeTagNames;
			}
		}

		// Token: 0x170043CB RID: 17355
		// (get) Token: 0x0600F638 RID: 63032 RVA: 0x002D5B5D File Offset: 0x002D3D5D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DropDownRegular.attributeNamespaceIds;
			}
		}

		// Token: 0x170043CC RID: 17356
		// (get) Token: 0x0600F639 RID: 63033 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600F63A RID: 63034 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "onAction")]
		public StringValue OnAction
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

		// Token: 0x170043CD RID: 17357
		// (get) Token: 0x0600F63B RID: 63035 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x0600F63C RID: 63036 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170043CE RID: 17358
		// (get) Token: 0x0600F63D RID: 63037 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600F63E RID: 63038 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x170043CF RID: 17359
		// (get) Token: 0x0600F63F RID: 63039 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600F640 RID: 63040 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "image")]
		public StringValue Image
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

		// Token: 0x170043D0 RID: 17360
		// (get) Token: 0x0600F641 RID: 63041 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600F642 RID: 63042 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "imageMso")]
		public StringValue ImageMso
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

		// Token: 0x170043D1 RID: 17361
		// (get) Token: 0x0600F643 RID: 63043 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600F644 RID: 63044 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "getImage")]
		public StringValue GetImage
		{
			get
			{
				return (StringValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x170043D2 RID: 17362
		// (get) Token: 0x0600F645 RID: 63045 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x0600F646 RID: 63046 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "showItemImage")]
		public BooleanValue ShowItemImage
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

		// Token: 0x170043D3 RID: 17363
		// (get) Token: 0x0600F647 RID: 63047 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600F648 RID: 63048 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "getItemCount")]
		public StringValue GetItemCount
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

		// Token: 0x170043D4 RID: 17364
		// (get) Token: 0x0600F649 RID: 63049 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600F64A RID: 63050 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "getItemLabel")]
		public StringValue GetItemLabel
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

		// Token: 0x170043D5 RID: 17365
		// (get) Token: 0x0600F64B RID: 63051 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600F64C RID: 63052 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "getItemScreentip")]
		public StringValue GetItemScreentip
		{
			get
			{
				return (StringValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x170043D6 RID: 17366
		// (get) Token: 0x0600F64D RID: 63053 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600F64E RID: 63054 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "getItemSupertip")]
		public StringValue GetItemSupertip
		{
			get
			{
				return (StringValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x170043D7 RID: 17367
		// (get) Token: 0x0600F64F RID: 63055 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600F650 RID: 63056 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "getItemImage")]
		public StringValue GetItemImage
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

		// Token: 0x170043D8 RID: 17368
		// (get) Token: 0x0600F651 RID: 63057 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600F652 RID: 63058 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "getItemID")]
		public StringValue GetItemID
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

		// Token: 0x170043D9 RID: 17369
		// (get) Token: 0x0600F653 RID: 63059 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600F654 RID: 63060 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "sizeString")]
		public StringValue SizeString
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

		// Token: 0x170043DA RID: 17370
		// (get) Token: 0x0600F655 RID: 63061 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600F656 RID: 63062 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "getSelectedItemID")]
		public StringValue GetSelectedItemID
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

		// Token: 0x170043DB RID: 17371
		// (get) Token: 0x0600F657 RID: 63063 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600F658 RID: 63064 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "getSelectedItemIndex")]
		public StringValue GetSelectedItemIndex
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

		// Token: 0x170043DC RID: 17372
		// (get) Token: 0x0600F659 RID: 63065 RVA: 0x002C930A File Offset: 0x002C750A
		// (set) Token: 0x0600F65A RID: 63066 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "showItemLabel")]
		public BooleanValue ShowItemLabel
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

		// Token: 0x170043DD RID: 17373
		// (get) Token: 0x0600F65B RID: 63067 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600F65C RID: 63068 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x170043DE RID: 17374
		// (get) Token: 0x0600F65D RID: 63069 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600F65E RID: 63070 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "idQ")]
		public StringValue QualifiedId
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

		// Token: 0x170043DF RID: 17375
		// (get) Token: 0x0600F65F RID: 63071 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600F660 RID: 63072 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x170043E0 RID: 17376
		// (get) Token: 0x0600F661 RID: 63073 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600F662 RID: 63074 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x170043E1 RID: 17377
		// (get) Token: 0x0600F663 RID: 63075 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600F664 RID: 63076 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "screentip")]
		public StringValue Screentip
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

		// Token: 0x170043E2 RID: 17378
		// (get) Token: 0x0600F665 RID: 63077 RVA: 0x002BE2F5 File Offset: 0x002BC4F5
		// (set) Token: 0x0600F666 RID: 63078 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "getScreentip")]
		public StringValue GetScreentip
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

		// Token: 0x170043E3 RID: 17379
		// (get) Token: 0x0600F667 RID: 63079 RVA: 0x002BEFAF File Offset: 0x002BD1AF
		// (set) Token: 0x0600F668 RID: 63080 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(0, "supertip")]
		public StringValue Supertip
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

		// Token: 0x170043E4 RID: 17380
		// (get) Token: 0x0600F669 RID: 63081 RVA: 0x002BE32D File Offset: 0x002BC52D
		// (set) Token: 0x0600F66A RID: 63082 RVA: 0x002BE33D File Offset: 0x002BC53D
		[SchemaAttr(0, "getSupertip")]
		public StringValue GetSupertip
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

		// Token: 0x170043E5 RID: 17381
		// (get) Token: 0x0600F66B RID: 63083 RVA: 0x002BE349 File Offset: 0x002BC549
		// (set) Token: 0x0600F66C RID: 63084 RVA: 0x002BE359 File Offset: 0x002BC559
		[SchemaAttr(0, "label")]
		public StringValue Label
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

		// Token: 0x170043E6 RID: 17382
		// (get) Token: 0x0600F66D RID: 63085 RVA: 0x002BE365 File Offset: 0x002BC565
		// (set) Token: 0x0600F66E RID: 63086 RVA: 0x002BE375 File Offset: 0x002BC575
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
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

		// Token: 0x170043E7 RID: 17383
		// (get) Token: 0x0600F66F RID: 63087 RVA: 0x002C13E0 File Offset: 0x002BF5E0
		// (set) Token: 0x0600F670 RID: 63088 RVA: 0x002BE391 File Offset: 0x002BC591
		[SchemaAttr(0, "insertAfterMso")]
		public StringValue InsertAfterMso
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

		// Token: 0x170043E8 RID: 17384
		// (get) Token: 0x0600F671 RID: 63089 RVA: 0x002BE39D File Offset: 0x002BC59D
		// (set) Token: 0x0600F672 RID: 63090 RVA: 0x002BE3AD File Offset: 0x002BC5AD
		[SchemaAttr(0, "insertBeforeMso")]
		public StringValue InsertBeforeMso
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

		// Token: 0x170043E9 RID: 17385
		// (get) Token: 0x0600F673 RID: 63091 RVA: 0x002BE3B9 File Offset: 0x002BC5B9
		// (set) Token: 0x0600F674 RID: 63092 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQulifiedId
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

		// Token: 0x170043EA RID: 17386
		// (get) Token: 0x0600F675 RID: 63093 RVA: 0x002C931A File Offset: 0x002C751A
		// (set) Token: 0x0600F676 RID: 63094 RVA: 0x002BE3E5 File Offset: 0x002BC5E5
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQulifiedId
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

		// Token: 0x170043EB RID: 17387
		// (get) Token: 0x0600F677 RID: 63095 RVA: 0x002CBE4C File Offset: 0x002CA04C
		// (set) Token: 0x0600F678 RID: 63096 RVA: 0x002C1410 File Offset: 0x002BF610
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
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

		// Token: 0x170043EC RID: 17388
		// (get) Token: 0x0600F679 RID: 63097 RVA: 0x002C2A7B File Offset: 0x002C0C7B
		// (set) Token: 0x0600F67A RID: 63098 RVA: 0x002C142C File Offset: 0x002BF62C
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
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

		// Token: 0x170043ED RID: 17389
		// (get) Token: 0x0600F67B RID: 63099 RVA: 0x002C932A File Offset: 0x002C752A
		// (set) Token: 0x0600F67C RID: 63100 RVA: 0x002C1448 File Offset: 0x002BF648
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
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

		// Token: 0x170043EE RID: 17390
		// (get) Token: 0x0600F67D RID: 63101 RVA: 0x002C4785 File Offset: 0x002C2985
		// (set) Token: 0x0600F67E RID: 63102 RVA: 0x002C1464 File Offset: 0x002BF664
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
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

		// Token: 0x170043EF RID: 17391
		// (get) Token: 0x0600F67F RID: 63103 RVA: 0x002CC6E3 File Offset: 0x002CA8E3
		// (set) Token: 0x0600F680 RID: 63104 RVA: 0x002C1480 File Offset: 0x002BF680
		[SchemaAttr(0, "showLabel")]
		public BooleanValue ShowLabel
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

		// Token: 0x170043F0 RID: 17392
		// (get) Token: 0x0600F681 RID: 63105 RVA: 0x002C4795 File Offset: 0x002C2995
		// (set) Token: 0x0600F682 RID: 63106 RVA: 0x002C149C File Offset: 0x002BF69C
		[SchemaAttr(0, "getShowLabel")]
		public StringValue GetShowLabel
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

		// Token: 0x170043F1 RID: 17393
		// (get) Token: 0x0600F683 RID: 63107 RVA: 0x002CC6F3 File Offset: 0x002CA8F3
		// (set) Token: 0x0600F684 RID: 63108 RVA: 0x002C14B8 File Offset: 0x002BF6B8
		[SchemaAttr(0, "showImage")]
		public BooleanValue ShowImage
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

		// Token: 0x170043F2 RID: 17394
		// (get) Token: 0x0600F685 RID: 63109 RVA: 0x002C14C4 File Offset: 0x002BF6C4
		// (set) Token: 0x0600F686 RID: 63110 RVA: 0x002C14D4 File Offset: 0x002BF6D4
		[SchemaAttr(0, "getShowImage")]
		public StringValue GetShowImage
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

		// Token: 0x0600F687 RID: 63111 RVA: 0x00293ECF File Offset: 0x002920CF
		public DropDownRegular()
		{
		}

		// Token: 0x0600F688 RID: 63112 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DropDownRegular(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F689 RID: 63113 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DropDownRegular(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F68A RID: 63114 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DropDownRegular(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600F68B RID: 63115 RVA: 0x002D2281 File Offset: 0x002D0481
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

		// Token: 0x0600F68C RID: 63116 RVA: 0x002D5B64 File Offset: 0x002D3D64
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
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
			if (namespaceId == 0 && "showItemLabel" == name)
			{
				return new BooleanValue();
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

		// Token: 0x0600F68D RID: 63117 RVA: 0x002D5ED3 File Offset: 0x002D40D3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DropDownRegular>(deep);
		}

		// Token: 0x0600F68E RID: 63118 RVA: 0x002D5EDC File Offset: 0x002D40DC
		// Note: this type is marked as 'beforefieldinit'.
		static DropDownRegular()
		{
			byte[] array = new byte[39];
			DropDownRegular.attributeNamespaceIds = array;
		}

		// Token: 0x04007129 RID: 28969
		private const string tagName = "dropDown";

		// Token: 0x0400712A RID: 28970
		private const byte tagNsId = 57;

		// Token: 0x0400712B RID: 28971
		internal const int ElementTypeIdConst = 13053;

		// Token: 0x0400712C RID: 28972
		private static string[] attributeTagNames = new string[]
		{
			"onAction", "enabled", "getEnabled", "image", "imageMso", "getImage", "showItemImage", "getItemCount", "getItemLabel", "getItemScreentip",
			"getItemSupertip", "getItemImage", "getItemID", "sizeString", "getSelectedItemID", "getSelectedItemIndex", "showItemLabel", "id", "idQ", "tag",
			"idMso", "screentip", "getScreentip", "supertip", "getSupertip", "label", "getLabel", "insertAfterMso", "insertBeforeMso", "insertAfterQ",
			"insertBeforeQ", "visible", "getVisible", "keytip", "getKeytip", "showLabel", "getShowLabel", "showImage", "getShowImage"
		};

		// Token: 0x0400712D RID: 28973
		private static byte[] attributeNamespaceIds;
	}
}
