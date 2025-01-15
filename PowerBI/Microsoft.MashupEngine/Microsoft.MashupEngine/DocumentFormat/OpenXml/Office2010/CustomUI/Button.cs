using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022C8 RID: 8904
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class Button : OpenXmlLeafElement
	{
		// Token: 0x1700432C RID: 17196
		// (get) Token: 0x0600F4F9 RID: 62713 RVA: 0x002C8B1A File Offset: 0x002C6D1A
		public override string LocalName
		{
			get
			{
				return "button";
			}
		}

		// Token: 0x1700432D RID: 17197
		// (get) Token: 0x0600F4FA RID: 62714 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x1700432E RID: 17198
		// (get) Token: 0x0600F4FB RID: 62715 RVA: 0x002D4984 File Offset: 0x002D2B84
		internal override int ElementTypeId
		{
			get
			{
				return 13049;
			}
		}

		// Token: 0x0600F4FC RID: 62716 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x1700432F RID: 17199
		// (get) Token: 0x0600F4FD RID: 62717 RVA: 0x002D498B File Offset: 0x002D2B8B
		internal override string[] AttributeTagNames
		{
			get
			{
				return Button.attributeTagNames;
			}
		}

		// Token: 0x17004330 RID: 17200
		// (get) Token: 0x0600F4FE RID: 62718 RVA: 0x002D4992 File Offset: 0x002D2B92
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Button.attributeNamespaceIds;
			}
		}

		// Token: 0x17004331 RID: 17201
		// (get) Token: 0x0600F4FF RID: 62719 RVA: 0x002D42D0 File Offset: 0x002D24D0
		// (set) Token: 0x0600F500 RID: 62720 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004332 RID: 17202
		// (get) Token: 0x0600F501 RID: 62721 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600F502 RID: 62722 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17004333 RID: 17203
		// (get) Token: 0x0600F503 RID: 62723 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600F504 RID: 62724 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "onAction")]
		public StringValue OnAction
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

		// Token: 0x17004334 RID: 17204
		// (get) Token: 0x0600F505 RID: 62725 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x0600F506 RID: 62726 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
		{
			get
			{
				return (BooleanValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17004335 RID: 17205
		// (get) Token: 0x0600F507 RID: 62727 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600F508 RID: 62728 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x17004336 RID: 17206
		// (get) Token: 0x0600F509 RID: 62729 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600F50A RID: 62730 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "description")]
		public StringValue Description
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

		// Token: 0x17004337 RID: 17207
		// (get) Token: 0x0600F50B RID: 62731 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600F50C RID: 62732 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "getDescription")]
		public StringValue GetDescription
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

		// Token: 0x17004338 RID: 17208
		// (get) Token: 0x0600F50D RID: 62733 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600F50E RID: 62734 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "image")]
		public StringValue Image
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

		// Token: 0x17004339 RID: 17209
		// (get) Token: 0x0600F50F RID: 62735 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600F510 RID: 62736 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "imageMso")]
		public StringValue ImageMso
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

		// Token: 0x1700433A RID: 17210
		// (get) Token: 0x0600F511 RID: 62737 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600F512 RID: 62738 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "getImage")]
		public StringValue GetImage
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

		// Token: 0x1700433B RID: 17211
		// (get) Token: 0x0600F513 RID: 62739 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600F514 RID: 62740 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x1700433C RID: 17212
		// (get) Token: 0x0600F515 RID: 62741 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600F516 RID: 62742 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "idQ")]
		public StringValue QualifiedId
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

		// Token: 0x1700433D RID: 17213
		// (get) Token: 0x0600F517 RID: 62743 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600F518 RID: 62744 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x1700433E RID: 17214
		// (get) Token: 0x0600F519 RID: 62745 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600F51A RID: 62746 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "idMso")]
		public StringValue IdMso
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

		// Token: 0x1700433F RID: 17215
		// (get) Token: 0x0600F51B RID: 62747 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600F51C RID: 62748 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "screentip")]
		public StringValue Screentip
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

		// Token: 0x17004340 RID: 17216
		// (get) Token: 0x0600F51D RID: 62749 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600F51E RID: 62750 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "getScreentip")]
		public StringValue GetScreentip
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

		// Token: 0x17004341 RID: 17217
		// (get) Token: 0x0600F51F RID: 62751 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600F520 RID: 62752 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "supertip")]
		public StringValue Supertip
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

		// Token: 0x17004342 RID: 17218
		// (get) Token: 0x0600F521 RID: 62753 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600F522 RID: 62754 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "getSupertip")]
		public StringValue GetSupertip
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

		// Token: 0x17004343 RID: 17219
		// (get) Token: 0x0600F523 RID: 62755 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600F524 RID: 62756 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "label")]
		public StringValue Label
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

		// Token: 0x17004344 RID: 17220
		// (get) Token: 0x0600F525 RID: 62757 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600F526 RID: 62758 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
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

		// Token: 0x17004345 RID: 17221
		// (get) Token: 0x0600F527 RID: 62759 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600F528 RID: 62760 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "insertAfterMso")]
		public StringValue InsertAfterMso
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

		// Token: 0x17004346 RID: 17222
		// (get) Token: 0x0600F529 RID: 62761 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600F52A RID: 62762 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "insertBeforeMso")]
		public StringValue InsertBeforeMso
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

		// Token: 0x17004347 RID: 17223
		// (get) Token: 0x0600F52B RID: 62763 RVA: 0x002BE2F5 File Offset: 0x002BC4F5
		// (set) Token: 0x0600F52C RID: 62764 RVA: 0x002BE305 File Offset: 0x002BC505
		[SchemaAttr(0, "insertAfterQ")]
		public StringValue InsertAfterQulifiedId
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

		// Token: 0x17004348 RID: 17224
		// (get) Token: 0x0600F52D RID: 62765 RVA: 0x002BEFAF File Offset: 0x002BD1AF
		// (set) Token: 0x0600F52E RID: 62766 RVA: 0x002BE321 File Offset: 0x002BC521
		[SchemaAttr(0, "insertBeforeQ")]
		public StringValue InsertBeforeQulifiedId
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

		// Token: 0x17004349 RID: 17225
		// (get) Token: 0x0600F52F RID: 62767 RVA: 0x002C87A2 File Offset: 0x002C69A2
		// (set) Token: 0x0600F530 RID: 62768 RVA: 0x002BE33D File Offset: 0x002BC53D
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
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

		// Token: 0x1700434A RID: 17226
		// (get) Token: 0x0600F531 RID: 62769 RVA: 0x002BE349 File Offset: 0x002BC549
		// (set) Token: 0x0600F532 RID: 62770 RVA: 0x002BE359 File Offset: 0x002BC559
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
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

		// Token: 0x1700434B RID: 17227
		// (get) Token: 0x0600F533 RID: 62771 RVA: 0x002BE365 File Offset: 0x002BC565
		// (set) Token: 0x0600F534 RID: 62772 RVA: 0x002BE375 File Offset: 0x002BC575
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
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

		// Token: 0x1700434C RID: 17228
		// (get) Token: 0x0600F535 RID: 62773 RVA: 0x002C13E0 File Offset: 0x002BF5E0
		// (set) Token: 0x0600F536 RID: 62774 RVA: 0x002BE391 File Offset: 0x002BC591
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
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

		// Token: 0x1700434D RID: 17229
		// (get) Token: 0x0600F537 RID: 62775 RVA: 0x002C8B55 File Offset: 0x002C6D55
		// (set) Token: 0x0600F538 RID: 62776 RVA: 0x002BE3AD File Offset: 0x002BC5AD
		[SchemaAttr(0, "showLabel")]
		public BooleanValue ShowLabel
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

		// Token: 0x1700434E RID: 17230
		// (get) Token: 0x0600F539 RID: 62777 RVA: 0x002BE3B9 File Offset: 0x002BC5B9
		// (set) Token: 0x0600F53A RID: 62778 RVA: 0x002BE3C9 File Offset: 0x002BC5C9
		[SchemaAttr(0, "getShowLabel")]
		public StringValue GetShowLabel
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

		// Token: 0x1700434F RID: 17231
		// (get) Token: 0x0600F53B RID: 62779 RVA: 0x002CB9E8 File Offset: 0x002C9BE8
		// (set) Token: 0x0600F53C RID: 62780 RVA: 0x002BE3E5 File Offset: 0x002BC5E5
		[SchemaAttr(0, "showImage")]
		public BooleanValue ShowImage
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

		// Token: 0x17004350 RID: 17232
		// (get) Token: 0x0600F53D RID: 62781 RVA: 0x002C2A6B File Offset: 0x002C0C6B
		// (set) Token: 0x0600F53E RID: 62782 RVA: 0x002C1410 File Offset: 0x002BF610
		[SchemaAttr(0, "getShowImage")]
		public StringValue GetShowImage
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

		// Token: 0x0600F540 RID: 62784 RVA: 0x002D499C File Offset: 0x002D2B9C
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

		// Token: 0x0600F541 RID: 62785 RVA: 0x002D4C71 File Offset: 0x002D2E71
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Button>(deep);
		}

		// Token: 0x0600F542 RID: 62786 RVA: 0x002D4C7C File Offset: 0x002D2E7C
		// Note: this type is marked as 'beforefieldinit'.
		static Button()
		{
			byte[] array = new byte[32];
			Button.attributeNamespaceIds = array;
		}

		// Token: 0x04007115 RID: 28949
		private const string tagName = "button";

		// Token: 0x04007116 RID: 28950
		private const byte tagNsId = 57;

		// Token: 0x04007117 RID: 28951
		internal const int ElementTypeIdConst = 13049;

		// Token: 0x04007118 RID: 28952
		private static string[] attributeTagNames = new string[]
		{
			"size", "getSize", "onAction", "enabled", "getEnabled", "description", "getDescription", "image", "imageMso", "getImage",
			"id", "idQ", "tag", "idMso", "screentip", "getScreentip", "supertip", "getSupertip", "label", "getLabel",
			"insertAfterMso", "insertBeforeMso", "insertAfterQ", "insertBeforeQ", "visible", "getVisible", "keytip", "getKeytip", "showLabel", "getShowLabel",
			"showImage", "getShowImage"
		};

		// Token: 0x04007119 RID: 28953
		private static byte[] attributeNamespaceIds;
	}
}
