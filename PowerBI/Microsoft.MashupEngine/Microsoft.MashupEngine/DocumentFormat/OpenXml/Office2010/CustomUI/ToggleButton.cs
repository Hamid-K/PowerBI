using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022C9 RID: 8905
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class ToggleButton : OpenXmlLeafElement
	{
		// Token: 0x17004351 RID: 17233
		// (get) Token: 0x0600F543 RID: 62787 RVA: 0x002C99C0 File Offset: 0x002C7BC0
		public override string LocalName
		{
			get
			{
				return "toggleButton";
			}
		}

		// Token: 0x17004352 RID: 17234
		// (get) Token: 0x0600F544 RID: 62788 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x17004353 RID: 17235
		// (get) Token: 0x0600F545 RID: 62789 RVA: 0x002D4DBC File Offset: 0x002D2FBC
		internal override int ElementTypeId
		{
			get
			{
				return 13050;
			}
		}

		// Token: 0x0600F546 RID: 62790 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004354 RID: 17236
		// (get) Token: 0x0600F547 RID: 62791 RVA: 0x002D4DC3 File Offset: 0x002D2FC3
		internal override string[] AttributeTagNames
		{
			get
			{
				return ToggleButton.attributeTagNames;
			}
		}

		// Token: 0x17004355 RID: 17237
		// (get) Token: 0x0600F548 RID: 62792 RVA: 0x002D4DCA File Offset: 0x002D2FCA
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ToggleButton.attributeNamespaceIds;
			}
		}

		// Token: 0x17004356 RID: 17238
		// (get) Token: 0x0600F549 RID: 62793 RVA: 0x002D42D0 File Offset: 0x002D24D0
		// (set) Token: 0x0600F54A RID: 62794 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "size")]
		public EnumValue<SizeValues> Size
		{
			get
			{
				return (EnumValue<SizeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17004357 RID: 17239
		// (get) Token: 0x0600F54B RID: 62795 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600F54C RID: 62796 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "getSize")]
		public StringValue GetSize
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

		// Token: 0x17004358 RID: 17240
		// (get) Token: 0x0600F54D RID: 62797 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600F54E RID: 62798 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "getPressed")]
		public StringValue GetPressed
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

		// Token: 0x17004359 RID: 17241
		// (get) Token: 0x0600F54F RID: 62799 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600F550 RID: 62800 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "onAction")]
		public StringValue OnAction
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

		// Token: 0x1700435A RID: 17242
		// (get) Token: 0x0600F551 RID: 62801 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x0600F552 RID: 62802 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
		{
			get
			{
				return (BooleanValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x1700435B RID: 17243
		// (get) Token: 0x0600F553 RID: 62803 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600F554 RID: 62804 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x1700435C RID: 17244
		// (get) Token: 0x0600F555 RID: 62805 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600F556 RID: 62806 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "description")]
		public StringValue Description
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

		// Token: 0x1700435D RID: 17245
		// (get) Token: 0x0600F557 RID: 62807 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600F558 RID: 62808 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "getDescription")]
		public StringValue GetDescription
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

		// Token: 0x1700435E RID: 17246
		// (get) Token: 0x0600F559 RID: 62809 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600F55A RID: 62810 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "image")]
		public StringValue Image
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

		// Token: 0x1700435F RID: 17247
		// (get) Token: 0x0600F55B RID: 62811 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600F55C RID: 62812 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "imageMso")]
		public StringValue ImageMso
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

		// Token: 0x17004360 RID: 17248
		// (get) Token: 0x0600F55D RID: 62813 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600F55E RID: 62814 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "getImage")]
		public StringValue GetImage
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

		// Token: 0x17004361 RID: 17249
		// (get) Token: 0x0600F55F RID: 62815 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600F560 RID: 62816 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x17004362 RID: 17250
		// (get) Token: 0x0600F561 RID: 62817 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600F562 RID: 62818 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "idQ")]
		public StringValue QualifiedId
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

		// Token: 0x17004363 RID: 17251
		// (get) Token: 0x0600F563 RID: 62819 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600F564 RID: 62820 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x17004364 RID: 17252
		// (get) Token: 0x0600F565 RID: 62821 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600F566 RID: 62822 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x17004365 RID: 17253
		// (get) Token: 0x0600F567 RID: 62823 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600F568 RID: 62824 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "screentip")]
		public StringValue Screentip
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

		// Token: 0x17004366 RID: 17254
		// (get) Token: 0x0600F569 RID: 62825 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600F56A RID: 62826 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "getScreentip")]
		public StringValue GetScreentip
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

		// Token: 0x17004367 RID: 17255
		// (get) Token: 0x0600F56B RID: 62827 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600F56C RID: 62828 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "supertip")]
		public StringValue Supertip
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

		// Token: 0x17004368 RID: 17256
		// (get) Token: 0x0600F56D RID: 62829 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600F56E RID: 62830 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "getSupertip")]
		public StringValue GetSupertip
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

		// Token: 0x17004369 RID: 17257
		// (get) Token: 0x0600F56F RID: 62831 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600F570 RID: 62832 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "label")]
		public StringValue Label
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

		// Token: 0x1700436A RID: 17258
		// (get) Token: 0x0600F571 RID: 62833 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600F572 RID: 62834 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
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

		// Token: 0x1700436B RID: 17259
		// (get) Token: 0x0600F573 RID: 62835 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600F574 RID: 62836 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "insertAfterMso")]
		public StringValue InsertAfterMso
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

		// Token: 0x1700436C RID: 17260
		// (get) Token: 0x0600F575 RID: 62837 RVA: 0x002BE2F5 File Offset: 0x002BC4F5
		// (set) Token: 0x0600F576 RID: 62838 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "insertBeforeMso")]
		public StringValue InsertBeforeMso
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

		// Token: 0x1700436D RID: 17261
		// (get) Token: 0x0600F577 RID: 62839 RVA: 0x002BEFAF File Offset: 0x002BD1AF
		// (set) Token: 0x0600F578 RID: 62840 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQulifiedId
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

		// Token: 0x1700436E RID: 17262
		// (get) Token: 0x0600F579 RID: 62841 RVA: 0x002BE32D File Offset: 0x002BC52D
		// (set) Token: 0x0600F57A RID: 62842 RVA: 0x002BE33D File Offset: 0x002BC53D
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQulifiedId
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

		// Token: 0x1700436F RID: 17263
		// (get) Token: 0x0600F57B RID: 62843 RVA: 0x002CBE3C File Offset: 0x002CA03C
		// (set) Token: 0x0600F57C RID: 62844 RVA: 0x002BE359 File Offset: 0x002BC559
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
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

		// Token: 0x17004370 RID: 17264
		// (get) Token: 0x0600F57D RID: 62845 RVA: 0x002BE365 File Offset: 0x002BC565
		// (set) Token: 0x0600F57E RID: 62846 RVA: 0x002BE375 File Offset: 0x002BC575
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
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

		// Token: 0x17004371 RID: 17265
		// (get) Token: 0x0600F57F RID: 62847 RVA: 0x002C13E0 File Offset: 0x002BF5E0
		// (set) Token: 0x0600F580 RID: 62848 RVA: 0x002BE391 File Offset: 0x002BC591
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
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

		// Token: 0x17004372 RID: 17266
		// (get) Token: 0x0600F581 RID: 62849 RVA: 0x002BE39D File Offset: 0x002BC59D
		// (set) Token: 0x0600F582 RID: 62850 RVA: 0x002BE3AD File Offset: 0x002BC5AD
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
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

		// Token: 0x17004373 RID: 17267
		// (get) Token: 0x0600F583 RID: 62851 RVA: 0x002C99FC File Offset: 0x002C7BFC
		// (set) Token: 0x0600F584 RID: 62852 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
		[SchemaAttr(0, "showLabel")]
		public BooleanValue ShowLabel
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

		// Token: 0x17004374 RID: 17268
		// (get) Token: 0x0600F585 RID: 62853 RVA: 0x002C931A File Offset: 0x002C751A
		// (set) Token: 0x0600F586 RID: 62854 RVA: 0x002BE3E5 File Offset: 0x002BC5E5
		[SchemaAttr(0, "getShowLabel")]
		public StringValue GetShowLabel
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

		// Token: 0x17004375 RID: 17269
		// (get) Token: 0x0600F587 RID: 62855 RVA: 0x002CBE4C File Offset: 0x002CA04C
		// (set) Token: 0x0600F588 RID: 62856 RVA: 0x002C1410 File Offset: 0x002BF610
		[SchemaAttr(0, "showImage")]
		public BooleanValue ShowImage
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

		// Token: 0x17004376 RID: 17270
		// (get) Token: 0x0600F589 RID: 62857 RVA: 0x002C2A7B File Offset: 0x002C0C7B
		// (set) Token: 0x0600F58A RID: 62858 RVA: 0x002C142C File Offset: 0x002BF62C
		[SchemaAttr(0, "getShowImage")]
		public StringValue GetShowImage
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

		// Token: 0x0600F58C RID: 62860 RVA: 0x002D4DD4 File Offset: 0x002D2FD4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "size" == name)
			{
				return new EnumValue<SizeValues>();
			}
			if (namespaceId == 0 && "getSize" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getPressed" == name)
			{
				return new StringValue();
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
			if (namespaceId == 0 && "description" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getDescription" == name)
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

		// Token: 0x0600F58D RID: 62861 RVA: 0x002D50BF File Offset: 0x002D32BF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ToggleButton>(deep);
		}

		// Token: 0x0600F58E RID: 62862 RVA: 0x002D50C8 File Offset: 0x002D32C8
		// Note: this type is marked as 'beforefieldinit'.
		static ToggleButton()
		{
			byte[] array = new byte[33];
			ToggleButton.attributeNamespaceIds = array;
		}

		// Token: 0x0400711A RID: 28954
		private const string tagName = "toggleButton";

		// Token: 0x0400711B RID: 28955
		private const byte tagNsId = 57;

		// Token: 0x0400711C RID: 28956
		internal const int ElementTypeIdConst = 13050;

		// Token: 0x0400711D RID: 28957
		private static string[] attributeTagNames = new string[]
		{
			"size", "getSize", "getPressed", "onAction", "enabled", "getEnabled", "description", "getDescription", "image", "imageMso",
			"getImage", "id", "idQ", "tag", "idMso", "screentip", "getScreentip", "supertip", "getSupertip", "label",
			"getLabel", "insertAfterMso", "insertBeforeMso", "insertAfterQ", "insertBeforeQ", "visible", "getVisible", "keytip", "getKeytip", "showLabel",
			"getShowLabel", "showImage", "getShowImage"
		};

		// Token: 0x0400711E RID: 28958
		private static byte[] attributeNamespaceIds;
	}
}
