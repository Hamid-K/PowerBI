using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022D7 RID: 8919
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class BackstageGroupButton : OpenXmlLeafElement
	{
		// Token: 0x17004501 RID: 17665
		// (get) Token: 0x0600F8C3 RID: 63683 RVA: 0x002C8B1A File Offset: 0x002C6D1A
		public override string LocalName
		{
			get
			{
				return "button";
			}
		}

		// Token: 0x17004502 RID: 17666
		// (get) Token: 0x0600F8C4 RID: 63684 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x17004503 RID: 17667
		// (get) Token: 0x0600F8C5 RID: 63685 RVA: 0x002D8252 File Offset: 0x002D6452
		internal override int ElementTypeId
		{
			get
			{
				return 13064;
			}
		}

		// Token: 0x0600F8C6 RID: 63686 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004504 RID: 17668
		// (get) Token: 0x0600F8C7 RID: 63687 RVA: 0x002D8259 File Offset: 0x002D6459
		internal override string[] AttributeTagNames
		{
			get
			{
				return BackstageGroupButton.attributeTagNames;
			}
		}

		// Token: 0x17004505 RID: 17669
		// (get) Token: 0x0600F8C8 RID: 63688 RVA: 0x002D8260 File Offset: 0x002D6460
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BackstageGroupButton.attributeNamespaceIds;
			}
		}

		// Token: 0x17004506 RID: 17670
		// (get) Token: 0x0600F8C9 RID: 63689 RVA: 0x002D8267 File Offset: 0x002D6467
		// (set) Token: 0x0600F8CA RID: 63690 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "expand")]
		public EnumValue<ExpandValues> Expand
		{
			get
			{
				return (EnumValue<ExpandValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17004507 RID: 17671
		// (get) Token: 0x0600F8CB RID: 63691 RVA: 0x002D8276 File Offset: 0x002D6476
		// (set) Token: 0x0600F8CC RID: 63692 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "style")]
		public EnumValue<Style2Values> Style
		{
			get
			{
				return (EnumValue<Style2Values>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17004508 RID: 17672
		// (get) Token: 0x0600F8CD RID: 63693 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600F8CE RID: 63694 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "screentip")]
		public StringValue Screentip
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

		// Token: 0x17004509 RID: 17673
		// (get) Token: 0x0600F8CF RID: 63695 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600F8D0 RID: 63696 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "getScreentip")]
		public StringValue GetScreentip
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

		// Token: 0x1700450A RID: 17674
		// (get) Token: 0x0600F8D1 RID: 63697 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600F8D2 RID: 63698 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "supertip")]
		public StringValue Supertip
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

		// Token: 0x1700450B RID: 17675
		// (get) Token: 0x0600F8D3 RID: 63699 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600F8D4 RID: 63700 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "getSupertip")]
		public StringValue GetSupertip
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

		// Token: 0x1700450C RID: 17676
		// (get) Token: 0x0600F8D5 RID: 63701 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600F8D6 RID: 63702 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x1700450D RID: 17677
		// (get) Token: 0x0600F8D7 RID: 63703 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600F8D8 RID: 63704 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "idQ")]
		public StringValue QualifiedId
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

		// Token: 0x1700450E RID: 17678
		// (get) Token: 0x0600F8D9 RID: 63705 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600F8DA RID: 63706 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x1700450F RID: 17679
		// (get) Token: 0x0600F8DB RID: 63707 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600F8DC RID: 63708 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "onAction")]
		public StringValue OnAction
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

		// Token: 0x17004510 RID: 17680
		// (get) Token: 0x0600F8DD RID: 63709 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x0600F8DE RID: 63710 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "isDefinitive")]
		public BooleanValue IsDefinitive
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

		// Token: 0x17004511 RID: 17681
		// (get) Token: 0x0600F8DF RID: 63711 RVA: 0x002C92FA File Offset: 0x002C74FA
		// (set) Token: 0x0600F8E0 RID: 63712 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
		{
			get
			{
				return (BooleanValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x17004512 RID: 17682
		// (get) Token: 0x0600F8E1 RID: 63713 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600F8E2 RID: 63714 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x17004513 RID: 17683
		// (get) Token: 0x0600F8E3 RID: 63715 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600F8E4 RID: 63716 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "label")]
		public StringValue Label
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

		// Token: 0x17004514 RID: 17684
		// (get) Token: 0x0600F8E5 RID: 63717 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600F8E6 RID: 63718 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
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

		// Token: 0x17004515 RID: 17685
		// (get) Token: 0x0600F8E7 RID: 63719 RVA: 0x002CA745 File Offset: 0x002C8945
		// (set) Token: 0x0600F8E8 RID: 63720 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
		{
			get
			{
				return (BooleanValue)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x17004516 RID: 17686
		// (get) Token: 0x0600F8E9 RID: 63721 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600F8EA RID: 63722 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
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

		// Token: 0x17004517 RID: 17687
		// (get) Token: 0x0600F8EB RID: 63723 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600F8EC RID: 63724 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
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

		// Token: 0x17004518 RID: 17688
		// (get) Token: 0x0600F8ED RID: 63725 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600F8EE RID: 63726 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
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

		// Token: 0x17004519 RID: 17689
		// (get) Token: 0x0600F8EF RID: 63727 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600F8F0 RID: 63728 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "image")]
		public StringValue Image
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

		// Token: 0x1700451A RID: 17690
		// (get) Token: 0x0600F8F1 RID: 63729 RVA: 0x002C8782 File Offset: 0x002C6982
		// (set) Token: 0x0600F8F2 RID: 63730 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "imageMso")]
		public StringValue ImageMso
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

		// Token: 0x1700451B RID: 17691
		// (get) Token: 0x0600F8F3 RID: 63731 RVA: 0x002BE2D9 File Offset: 0x002BC4D9
		// (set) Token: 0x0600F8F4 RID: 63732 RVA: 0x002BE2E9 File Offset: 0x002BC4E9
		[SchemaAttr(0, "getImage")]
		public StringValue GetImage
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

		// Token: 0x0600F8F6 RID: 63734 RVA: 0x002D8288 File Offset: 0x002D6488
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "expand" == name)
			{
				return new EnumValue<ExpandValues>();
			}
			if (namespaceId == 0 && "style" == name)
			{
				return new EnumValue<Style2Values>();
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
			if (namespaceId == 0 && "onAction" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "isDefinitive" == name)
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
			if (namespaceId == 0 && "label" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getLabel" == name)
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
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600F8F7 RID: 63735 RVA: 0x002D8481 File Offset: 0x002D6681
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BackstageGroupButton>(deep);
		}

		// Token: 0x0600F8F8 RID: 63736 RVA: 0x002D848C File Offset: 0x002D668C
		// Note: this type is marked as 'beforefieldinit'.
		static BackstageGroupButton()
		{
			byte[] array = new byte[22];
			BackstageGroupButton.attributeNamespaceIds = array;
		}

		// Token: 0x04007160 RID: 29024
		private const string tagName = "button";

		// Token: 0x04007161 RID: 29025
		private const byte tagNsId = 57;

		// Token: 0x04007162 RID: 29026
		internal const int ElementTypeIdConst = 13064;

		// Token: 0x04007163 RID: 29027
		private static string[] attributeTagNames = new string[]
		{
			"expand", "style", "screentip", "getScreentip", "supertip", "getSupertip", "id", "idQ", "tag", "onAction",
			"isDefinitive", "enabled", "getEnabled", "label", "getLabel", "visible", "getVisible", "keytip", "getKeytip", "image",
			"imageMso", "getImage"
		};

		// Token: 0x04007164 RID: 29028
		private static byte[] attributeNamespaceIds;
	}
}
