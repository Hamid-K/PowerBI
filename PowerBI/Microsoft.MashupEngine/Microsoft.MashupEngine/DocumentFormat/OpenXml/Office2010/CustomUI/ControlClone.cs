using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022C6 RID: 8902
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ControlClone : OpenXmlLeafElement
	{
		// Token: 0x170042F0 RID: 17136
		// (get) Token: 0x0600F481 RID: 62593 RVA: 0x002AD773 File Offset: 0x002AB973
		public override string LocalName
		{
			get
			{
				return "control";
			}
		}

		// Token: 0x170042F1 RID: 17137
		// (get) Token: 0x0600F482 RID: 62594 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x170042F2 RID: 17138
		// (get) Token: 0x0600F483 RID: 62595 RVA: 0x002D42BB File Offset: 0x002D24BB
		internal override int ElementTypeId
		{
			get
			{
				return 13047;
			}
		}

		// Token: 0x0600F484 RID: 62596 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170042F3 RID: 17139
		// (get) Token: 0x0600F485 RID: 62597 RVA: 0x002D42C2 File Offset: 0x002D24C2
		internal override string[] AttributeTagNames
		{
			get
			{
				return ControlClone.attributeTagNames;
			}
		}

		// Token: 0x170042F4 RID: 17140
		// (get) Token: 0x0600F486 RID: 62598 RVA: 0x002D42C9 File Offset: 0x002D24C9
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ControlClone.attributeNamespaceIds;
			}
		}

		// Token: 0x170042F5 RID: 17141
		// (get) Token: 0x0600F487 RID: 62599 RVA: 0x002D42D0 File Offset: 0x002D24D0
		// (set) Token: 0x0600F488 RID: 62600 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170042F6 RID: 17142
		// (get) Token: 0x0600F489 RID: 62601 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600F48A RID: 62602 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x170042F7 RID: 17143
		// (get) Token: 0x0600F48B RID: 62603 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x0600F48C RID: 62604 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
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

		// Token: 0x170042F8 RID: 17144
		// (get) Token: 0x0600F48D RID: 62605 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600F48E RID: 62606 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x170042F9 RID: 17145
		// (get) Token: 0x0600F48F RID: 62607 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600F490 RID: 62608 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "description")]
		public StringValue Description
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

		// Token: 0x170042FA RID: 17146
		// (get) Token: 0x0600F491 RID: 62609 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600F492 RID: 62610 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "getDescription")]
		public StringValue GetDescription
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

		// Token: 0x170042FB RID: 17147
		// (get) Token: 0x0600F493 RID: 62611 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600F494 RID: 62612 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "image")]
		public StringValue Image
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

		// Token: 0x170042FC RID: 17148
		// (get) Token: 0x0600F495 RID: 62613 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600F496 RID: 62614 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "imageMso")]
		public StringValue ImageMso
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

		// Token: 0x170042FD RID: 17149
		// (get) Token: 0x0600F497 RID: 62615 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600F498 RID: 62616 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "getImage")]
		public StringValue GetImage
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

		// Token: 0x170042FE RID: 17150
		// (get) Token: 0x0600F499 RID: 62617 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600F49A RID: 62618 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "idQ")]
		public StringValue QualifiedId
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

		// Token: 0x170042FF RID: 17151
		// (get) Token: 0x0600F49B RID: 62619 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600F49C RID: 62620 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x17004300 RID: 17152
		// (get) Token: 0x0600F49D RID: 62621 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600F49E RID: 62622 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x17004301 RID: 17153
		// (get) Token: 0x0600F49F RID: 62623 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600F4A0 RID: 62624 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "screentip")]
		public StringValue Screentip
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

		// Token: 0x17004302 RID: 17154
		// (get) Token: 0x0600F4A1 RID: 62625 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600F4A2 RID: 62626 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "getScreentip")]
		public StringValue GetScreentip
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

		// Token: 0x17004303 RID: 17155
		// (get) Token: 0x0600F4A3 RID: 62627 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600F4A4 RID: 62628 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "supertip")]
		public StringValue Supertip
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

		// Token: 0x17004304 RID: 17156
		// (get) Token: 0x0600F4A5 RID: 62629 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600F4A6 RID: 62630 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "getSupertip")]
		public StringValue GetSupertip
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

		// Token: 0x17004305 RID: 17157
		// (get) Token: 0x0600F4A7 RID: 62631 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600F4A8 RID: 62632 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "label")]
		public StringValue Label
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

		// Token: 0x17004306 RID: 17158
		// (get) Token: 0x0600F4A9 RID: 62633 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600F4AA RID: 62634 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
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

		// Token: 0x17004307 RID: 17159
		// (get) Token: 0x0600F4AB RID: 62635 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600F4AC RID: 62636 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "insertAfterMso")]
		public StringValue InsertAfterMso
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

		// Token: 0x17004308 RID: 17160
		// (get) Token: 0x0600F4AD RID: 62637 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600F4AE RID: 62638 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "insertBeforeMso")]
		public StringValue InsertBeforeMso
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

		// Token: 0x17004309 RID: 17161
		// (get) Token: 0x0600F4AF RID: 62639 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600F4B0 RID: 62640 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQulifiedId
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

		// Token: 0x1700430A RID: 17162
		// (get) Token: 0x0600F4B1 RID: 62641 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600F4B2 RID: 62642 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQulifiedId
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

		// Token: 0x1700430B RID: 17163
		// (get) Token: 0x0600F4B3 RID: 62643 RVA: 0x002C8792 File Offset: 0x002C6992
		// (set) Token: 0x0600F4B4 RID: 62644 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
		{
			get
			{
				return (BooleanValue)base.Attributes[22];
			}
			set
			{
				base.Attributes[22] = value;
			}
		}

		// Token: 0x1700430C RID: 17164
		// (get) Token: 0x0600F4B5 RID: 62645 RVA: 0x002BEFAF File Offset: 0x002BD1AF
		// (set) Token: 0x0600F4B6 RID: 62646 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
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

		// Token: 0x1700430D RID: 17165
		// (get) Token: 0x0600F4B7 RID: 62647 RVA: 0x002BE32D File Offset: 0x002BC52D
		// (set) Token: 0x0600F4B8 RID: 62648 RVA: 0x002BE33D File Offset: 0x002BC53D
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
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

		// Token: 0x1700430E RID: 17166
		// (get) Token: 0x0600F4B9 RID: 62649 RVA: 0x002BE349 File Offset: 0x002BC549
		// (set) Token: 0x0600F4BA RID: 62650 RVA: 0x002BE359 File Offset: 0x002BC559
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
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

		// Token: 0x1700430F RID: 17167
		// (get) Token: 0x0600F4BB RID: 62651 RVA: 0x002C8B45 File Offset: 0x002C6D45
		// (set) Token: 0x0600F4BC RID: 62652 RVA: 0x002BE375 File Offset: 0x002BC575
		[SchemaAttr(0, "showLabel")]
		public BooleanValue ShowLabel
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

		// Token: 0x17004310 RID: 17168
		// (get) Token: 0x0600F4BD RID: 62653 RVA: 0x002C13E0 File Offset: 0x002BF5E0
		// (set) Token: 0x0600F4BE RID: 62654 RVA: 0x002BE391 File Offset: 0x002BC591
		[SchemaAttr(0, "getShowLabel")]
		public StringValue GetShowLabel
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

		// Token: 0x17004311 RID: 17169
		// (get) Token: 0x0600F4BF RID: 62655 RVA: 0x002C8B55 File Offset: 0x002C6D55
		// (set) Token: 0x0600F4C0 RID: 62656 RVA: 0x002BE3AD File Offset: 0x002BC5AD
		[SchemaAttr(0, "showImage")]
		public BooleanValue ShowImage
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

		// Token: 0x17004312 RID: 17170
		// (get) Token: 0x0600F4C1 RID: 62657 RVA: 0x002BE3B9 File Offset: 0x002BC5B9
		// (set) Token: 0x0600F4C2 RID: 62658 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
		[SchemaAttr(0, "getShowImage")]
		public StringValue GetShowImage
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

		// Token: 0x0600F4C4 RID: 62660 RVA: 0x002D42E0 File Offset: 0x002D24E0
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

		// Token: 0x0600F4C5 RID: 62661 RVA: 0x002D4589 File Offset: 0x002D2789
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ControlClone>(deep);
		}

		// Token: 0x0600F4C6 RID: 62662 RVA: 0x002D4594 File Offset: 0x002D2794
		// Note: this type is marked as 'beforefieldinit'.
		static ControlClone()
		{
			byte[] array = new byte[30];
			ControlClone.attributeNamespaceIds = array;
		}

		// Token: 0x0400710B RID: 28939
		private const string tagName = "control";

		// Token: 0x0400710C RID: 28940
		private const byte tagNsId = 57;

		// Token: 0x0400710D RID: 28941
		internal const int ElementTypeIdConst = 13047;

		// Token: 0x0400710E RID: 28942
		private static string[] attributeTagNames = new string[]
		{
			"size", "getSize", "enabled", "getEnabled", "description", "getDescription", "image", "imageMso", "getImage", "idQ",
			"tag", "idMso", "screentip", "getScreentip", "supertip", "getSupertip", "label", "getLabel", "insertAfterMso", "insertBeforeMso",
			"insertAfterQ", "insertBeforeQ", "visible", "getVisible", "keytip", "getKeytip", "showLabel", "getShowLabel", "showImage", "getShowImage"
		};

		// Token: 0x0400710F RID: 28943
		private static byte[] attributeNamespaceIds;
	}
}
