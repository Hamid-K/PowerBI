using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022D3 RID: 8915
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class BackstageMenuButton : OpenXmlLeafElement
	{
		// Token: 0x170044AA RID: 17578
		// (get) Token: 0x0600F811 RID: 63505 RVA: 0x002C8B1A File Offset: 0x002C6D1A
		public override string LocalName
		{
			get
			{
				return "button";
			}
		}

		// Token: 0x170044AB RID: 17579
		// (get) Token: 0x0600F812 RID: 63506 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x170044AC RID: 17580
		// (get) Token: 0x0600F813 RID: 63507 RVA: 0x002D78B1 File Offset: 0x002D5AB1
		internal override int ElementTypeId
		{
			get
			{
				return 13060;
			}
		}

		// Token: 0x0600F814 RID: 63508 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170044AD RID: 17581
		// (get) Token: 0x0600F815 RID: 63509 RVA: 0x002D78B8 File Offset: 0x002D5AB8
		internal override string[] AttributeTagNames
		{
			get
			{
				return BackstageMenuButton.attributeTagNames;
			}
		}

		// Token: 0x170044AE RID: 17582
		// (get) Token: 0x0600F816 RID: 63510 RVA: 0x002D78BF File Offset: 0x002D5ABF
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BackstageMenuButton.attributeNamespaceIds;
			}
		}

		// Token: 0x170044AF RID: 17583
		// (get) Token: 0x0600F817 RID: 63511 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600F818 RID: 63512 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170044B0 RID: 17584
		// (get) Token: 0x0600F819 RID: 63513 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600F81A RID: 63514 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x170044B1 RID: 17585
		// (get) Token: 0x0600F81B RID: 63515 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600F81C RID: 63516 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "id")]
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

		// Token: 0x170044B2 RID: 17586
		// (get) Token: 0x0600F81D RID: 63517 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600F81E RID: 63518 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "idQ")]
		public StringValue QualifiedId
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

		// Token: 0x170044B3 RID: 17587
		// (get) Token: 0x0600F81F RID: 63519 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600F820 RID: 63520 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x170044B4 RID: 17588
		// (get) Token: 0x0600F821 RID: 63521 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600F822 RID: 63522 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "onAction")]
		public StringValue OnAction
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

		// Token: 0x170044B5 RID: 17589
		// (get) Token: 0x0600F823 RID: 63523 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x0600F824 RID: 63524 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "isDefinitive")]
		public BooleanValue IsDefinitive
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

		// Token: 0x170044B6 RID: 17590
		// (get) Token: 0x0600F825 RID: 63525 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x0600F826 RID: 63526 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
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

		// Token: 0x170044B7 RID: 17591
		// (get) Token: 0x0600F827 RID: 63527 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600F828 RID: 63528 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x170044B8 RID: 17592
		// (get) Token: 0x0600F829 RID: 63529 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600F82A RID: 63530 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "label")]
		public StringValue Label
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

		// Token: 0x170044B9 RID: 17593
		// (get) Token: 0x0600F82B RID: 63531 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600F82C RID: 63532 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
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

		// Token: 0x170044BA RID: 17594
		// (get) Token: 0x0600F82D RID: 63533 RVA: 0x002C92FA File Offset: 0x002C74FA
		// (set) Token: 0x0600F82E RID: 63534 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
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

		// Token: 0x170044BB RID: 17595
		// (get) Token: 0x0600F82F RID: 63535 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600F830 RID: 63536 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
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

		// Token: 0x170044BC RID: 17596
		// (get) Token: 0x0600F831 RID: 63537 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600F832 RID: 63538 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
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

		// Token: 0x170044BD RID: 17597
		// (get) Token: 0x0600F833 RID: 63539 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600F834 RID: 63540 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
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

		// Token: 0x170044BE RID: 17598
		// (get) Token: 0x0600F835 RID: 63541 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600F836 RID: 63542 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "image")]
		public StringValue Image
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

		// Token: 0x170044BF RID: 17599
		// (get) Token: 0x0600F837 RID: 63543 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600F838 RID: 63544 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "imageMso")]
		public StringValue ImageMso
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

		// Token: 0x170044C0 RID: 17600
		// (get) Token: 0x0600F839 RID: 63545 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600F83A RID: 63546 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "getImage")]
		public StringValue GetImage
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

		// Token: 0x0600F83C RID: 63548 RVA: 0x002D78C8 File Offset: 0x002D5AC8
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

		// Token: 0x0600F83D RID: 63549 RVA: 0x002D7A69 File Offset: 0x002D5C69
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BackstageMenuButton>(deep);
		}

		// Token: 0x0600F83E RID: 63550 RVA: 0x002D7A74 File Offset: 0x002D5C74
		// Note: this type is marked as 'beforefieldinit'.
		static BackstageMenuButton()
		{
			byte[] array = new byte[18];
			BackstageMenuButton.attributeNamespaceIds = array;
		}

		// Token: 0x0400714C RID: 29004
		private const string tagName = "button";

		// Token: 0x0400714D RID: 29005
		private const byte tagNsId = 57;

		// Token: 0x0400714E RID: 29006
		internal const int ElementTypeIdConst = 13060;

		// Token: 0x0400714F RID: 29007
		private static string[] attributeTagNames = new string[]
		{
			"description", "getDescription", "id", "idQ", "tag", "onAction", "isDefinitive", "enabled", "getEnabled", "label",
			"getLabel", "visible", "getVisible", "keytip", "getKeytip", "image", "imageMso", "getImage"
		};

		// Token: 0x04007150 RID: 29008
		private static byte[] attributeNamespaceIds;
	}
}
