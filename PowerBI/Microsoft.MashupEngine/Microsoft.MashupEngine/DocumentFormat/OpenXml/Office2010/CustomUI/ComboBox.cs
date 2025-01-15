using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022CB RID: 8907
	[ChildElementInfo(typeof(Item), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ComboBox : OpenXmlCompositeElement
	{
		// Token: 0x1700439B RID: 17307
		// (get) Token: 0x0600F5D7 RID: 62935 RVA: 0x002CC6B7 File Offset: 0x002CA8B7
		public override string LocalName
		{
			get
			{
				return "comboBox";
			}
		}

		// Token: 0x1700439C RID: 17308
		// (get) Token: 0x0600F5D8 RID: 62936 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x1700439D RID: 17309
		// (get) Token: 0x0600F5D9 RID: 62937 RVA: 0x002D5627 File Offset: 0x002D3827
		internal override int ElementTypeId
		{
			get
			{
				return 13052;
			}
		}

		// Token: 0x0600F5DA RID: 62938 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x1700439E RID: 17310
		// (get) Token: 0x0600F5DB RID: 62939 RVA: 0x002D562E File Offset: 0x002D382E
		internal override string[] AttributeTagNames
		{
			get
			{
				return ComboBox.attributeTagNames;
			}
		}

		// Token: 0x1700439F RID: 17311
		// (get) Token: 0x0600F5DC RID: 62940 RVA: 0x002D5635 File Offset: 0x002D3835
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ComboBox.attributeNamespaceIds;
			}
		}

		// Token: 0x170043A0 RID: 17312
		// (get) Token: 0x0600F5DD RID: 62941 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x0600F5DE RID: 62942 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "showItemImage")]
		public BooleanValue ShowItemImage
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

		// Token: 0x170043A1 RID: 17313
		// (get) Token: 0x0600F5DF RID: 62943 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600F5E0 RID: 62944 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "getItemCount")]
		public StringValue GetItemCount
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

		// Token: 0x170043A2 RID: 17314
		// (get) Token: 0x0600F5E1 RID: 62945 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600F5E2 RID: 62946 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "getItemLabel")]
		public StringValue GetItemLabel
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

		// Token: 0x170043A3 RID: 17315
		// (get) Token: 0x0600F5E3 RID: 62947 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600F5E4 RID: 62948 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "getItemScreentip")]
		public StringValue GetItemScreentip
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

		// Token: 0x170043A4 RID: 17316
		// (get) Token: 0x0600F5E5 RID: 62949 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600F5E6 RID: 62950 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "getItemSupertip")]
		public StringValue GetItemSupertip
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

		// Token: 0x170043A5 RID: 17317
		// (get) Token: 0x0600F5E7 RID: 62951 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600F5E8 RID: 62952 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "getItemImage")]
		public StringValue GetItemImage
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

		// Token: 0x170043A6 RID: 17318
		// (get) Token: 0x0600F5E9 RID: 62953 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600F5EA RID: 62954 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "getItemID")]
		public StringValue GetItemID
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

		// Token: 0x170043A7 RID: 17319
		// (get) Token: 0x0600F5EB RID: 62955 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600F5EC RID: 62956 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "sizeString")]
		public StringValue SizeString
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

		// Token: 0x170043A8 RID: 17320
		// (get) Token: 0x0600F5ED RID: 62957 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x0600F5EE RID: 62958 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "invalidateContentOnDrop")]
		public BooleanValue InvalidateContentOnDrop
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

		// Token: 0x170043A9 RID: 17321
		// (get) Token: 0x0600F5EF RID: 62959 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x0600F5F0 RID: 62960 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
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

		// Token: 0x170043AA RID: 17322
		// (get) Token: 0x0600F5F1 RID: 62961 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600F5F2 RID: 62962 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x170043AB RID: 17323
		// (get) Token: 0x0600F5F3 RID: 62963 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600F5F4 RID: 62964 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "image")]
		public StringValue Image
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

		// Token: 0x170043AC RID: 17324
		// (get) Token: 0x0600F5F5 RID: 62965 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600F5F6 RID: 62966 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "imageMso")]
		public StringValue ImageMso
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

		// Token: 0x170043AD RID: 17325
		// (get) Token: 0x0600F5F7 RID: 62967 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600F5F8 RID: 62968 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "getImage")]
		public StringValue GetImage
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

		// Token: 0x170043AE RID: 17326
		// (get) Token: 0x0600F5F9 RID: 62969 RVA: 0x002CC6D3 File Offset: 0x002CA8D3
		// (set) Token: 0x0600F5FA RID: 62970 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "maxLength")]
		public IntegerValue MaxLength
		{
			get
			{
				return (IntegerValue)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x170043AF RID: 17327
		// (get) Token: 0x0600F5FB RID: 62971 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600F5FC RID: 62972 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "getText")]
		public StringValue GetText
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

		// Token: 0x170043B0 RID: 17328
		// (get) Token: 0x0600F5FD RID: 62973 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600F5FE RID: 62974 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "onChange")]
		public StringValue OnChange
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

		// Token: 0x170043B1 RID: 17329
		// (get) Token: 0x0600F5FF RID: 62975 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600F600 RID: 62976 RVA: 0x002BE279 File Offset: 0x002BC479
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

		// Token: 0x170043B2 RID: 17330
		// (get) Token: 0x0600F601 RID: 62977 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600F602 RID: 62978 RVA: 0x002BE295 File Offset: 0x002BC495
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

		// Token: 0x170043B3 RID: 17331
		// (get) Token: 0x0600F603 RID: 62979 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600F604 RID: 62980 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
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

		// Token: 0x170043B4 RID: 17332
		// (get) Token: 0x0600F605 RID: 62981 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600F606 RID: 62982 RVA: 0x002BE2CD File Offset: 0x002BC4CD
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

		// Token: 0x170043B5 RID: 17333
		// (get) Token: 0x0600F607 RID: 62983 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600F608 RID: 62984 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
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

		// Token: 0x170043B6 RID: 17334
		// (get) Token: 0x0600F609 RID: 62985 RVA: 0x002BE2F5 File Offset: 0x002BC4F5
		// (set) Token: 0x0600F60A RID: 62986 RVA: 0x002BE305 File Offset: 0x002BC505
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

		// Token: 0x170043B7 RID: 17335
		// (get) Token: 0x0600F60B RID: 62987 RVA: 0x002BEFAF File Offset: 0x002BD1AF
		// (set) Token: 0x0600F60C RID: 62988 RVA: 0x002BE321 File Offset: 0x002BC521
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

		// Token: 0x170043B8 RID: 17336
		// (get) Token: 0x0600F60D RID: 62989 RVA: 0x002BE32D File Offset: 0x002BC52D
		// (set) Token: 0x0600F60E RID: 62990 RVA: 0x002BE33D File Offset: 0x002BC53D
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

		// Token: 0x170043B9 RID: 17337
		// (get) Token: 0x0600F60F RID: 62991 RVA: 0x002BE349 File Offset: 0x002BC549
		// (set) Token: 0x0600F610 RID: 62992 RVA: 0x002BE359 File Offset: 0x002BC559
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

		// Token: 0x170043BA RID: 17338
		// (get) Token: 0x0600F611 RID: 62993 RVA: 0x002BE365 File Offset: 0x002BC565
		// (set) Token: 0x0600F612 RID: 62994 RVA: 0x002BE375 File Offset: 0x002BC575
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

		// Token: 0x170043BB RID: 17339
		// (get) Token: 0x0600F613 RID: 62995 RVA: 0x002C13E0 File Offset: 0x002BF5E0
		// (set) Token: 0x0600F614 RID: 62996 RVA: 0x002BE391 File Offset: 0x002BC591
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

		// Token: 0x170043BC RID: 17340
		// (get) Token: 0x0600F615 RID: 62997 RVA: 0x002BE39D File Offset: 0x002BC59D
		// (set) Token: 0x0600F616 RID: 62998 RVA: 0x002BE3AD File Offset: 0x002BC5AD
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

		// Token: 0x170043BD RID: 17341
		// (get) Token: 0x0600F617 RID: 62999 RVA: 0x002BE3B9 File Offset: 0x002BC5B9
		// (set) Token: 0x0600F618 RID: 63000 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
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

		// Token: 0x170043BE RID: 17342
		// (get) Token: 0x0600F619 RID: 63001 RVA: 0x002C931A File Offset: 0x002C751A
		// (set) Token: 0x0600F61A RID: 63002 RVA: 0x002BE3E5 File Offset: 0x002BC5E5
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

		// Token: 0x170043BF RID: 17343
		// (get) Token: 0x0600F61B RID: 63003 RVA: 0x002CBE4C File Offset: 0x002CA04C
		// (set) Token: 0x0600F61C RID: 63004 RVA: 0x002C1410 File Offset: 0x002BF610
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

		// Token: 0x170043C0 RID: 17344
		// (get) Token: 0x0600F61D RID: 63005 RVA: 0x002C2A7B File Offset: 0x002C0C7B
		// (set) Token: 0x0600F61E RID: 63006 RVA: 0x002C142C File Offset: 0x002BF62C
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

		// Token: 0x170043C1 RID: 17345
		// (get) Token: 0x0600F61F RID: 63007 RVA: 0x002C932A File Offset: 0x002C752A
		// (set) Token: 0x0600F620 RID: 63008 RVA: 0x002C1448 File Offset: 0x002BF648
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

		// Token: 0x170043C2 RID: 17346
		// (get) Token: 0x0600F621 RID: 63009 RVA: 0x002C4785 File Offset: 0x002C2985
		// (set) Token: 0x0600F622 RID: 63010 RVA: 0x002C1464 File Offset: 0x002BF664
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

		// Token: 0x170043C3 RID: 17347
		// (get) Token: 0x0600F623 RID: 63011 RVA: 0x002CC6E3 File Offset: 0x002CA8E3
		// (set) Token: 0x0600F624 RID: 63012 RVA: 0x002C1480 File Offset: 0x002BF680
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

		// Token: 0x170043C4 RID: 17348
		// (get) Token: 0x0600F625 RID: 63013 RVA: 0x002C4795 File Offset: 0x002C2995
		// (set) Token: 0x0600F626 RID: 63014 RVA: 0x002C149C File Offset: 0x002BF69C
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

		// Token: 0x170043C5 RID: 17349
		// (get) Token: 0x0600F627 RID: 63015 RVA: 0x002CC6F3 File Offset: 0x002CA8F3
		// (set) Token: 0x0600F628 RID: 63016 RVA: 0x002C14B8 File Offset: 0x002BF6B8
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

		// Token: 0x170043C6 RID: 17350
		// (get) Token: 0x0600F629 RID: 63017 RVA: 0x002C14C4 File Offset: 0x002BF6C4
		// (set) Token: 0x0600F62A RID: 63018 RVA: 0x002C14D4 File Offset: 0x002BF6D4
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

		// Token: 0x0600F62B RID: 63019 RVA: 0x00293ECF File Offset: 0x002920CF
		public ComboBox()
		{
		}

		// Token: 0x0600F62C RID: 63020 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ComboBox(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F62D RID: 63021 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ComboBox(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F62E RID: 63022 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ComboBox(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600F62F RID: 63023 RVA: 0x002D563C File Offset: 0x002D383C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (57 == namespaceId && "item" == name)
			{
				return new Item();
			}
			return null;
		}

		// Token: 0x0600F630 RID: 63024 RVA: 0x002D5658 File Offset: 0x002D3858
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
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
			if (namespaceId == 0 && "invalidateContentOnDrop" == name)
			{
				return new BooleanValue();
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
			if (namespaceId == 0 && "maxLength" == name)
			{
				return new IntegerValue();
			}
			if (namespaceId == 0 && "getText" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "onChange" == name)
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

		// Token: 0x0600F631 RID: 63025 RVA: 0x002D59C7 File Offset: 0x002D3BC7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ComboBox>(deep);
		}

		// Token: 0x0600F632 RID: 63026 RVA: 0x002D59D0 File Offset: 0x002D3BD0
		// Note: this type is marked as 'beforefieldinit'.
		static ComboBox()
		{
			byte[] array = new byte[39];
			ComboBox.attributeNamespaceIds = array;
		}

		// Token: 0x04007124 RID: 28964
		private const string tagName = "comboBox";

		// Token: 0x04007125 RID: 28965
		private const byte tagNsId = 57;

		// Token: 0x04007126 RID: 28966
		internal const int ElementTypeIdConst = 13052;

		// Token: 0x04007127 RID: 28967
		private static string[] attributeTagNames = new string[]
		{
			"showItemImage", "getItemCount", "getItemLabel", "getItemScreentip", "getItemSupertip", "getItemImage", "getItemID", "sizeString", "invalidateContentOnDrop", "enabled",
			"getEnabled", "image", "imageMso", "getImage", "maxLength", "getText", "onChange", "id", "idQ", "tag",
			"idMso", "screentip", "getScreentip", "supertip", "getSupertip", "label", "getLabel", "insertAfterMso", "insertBeforeMso", "insertAfterQ",
			"insertBeforeQ", "visible", "getVisible", "keytip", "getKeytip", "showLabel", "getShowLabel", "showImage", "getShowImage"
		};

		// Token: 0x04007128 RID: 28968
		private static byte[] attributeNamespaceIds;
	}
}
