using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022D4 RID: 8916
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class BackstageMenuCheckBox : OpenXmlLeafElement
	{
		// Token: 0x170044C1 RID: 17601
		// (get) Token: 0x0600F83F RID: 63551 RVA: 0x002C8F4A File Offset: 0x002C714A
		public override string LocalName
		{
			get
			{
				return "checkBox";
			}
		}

		// Token: 0x170044C2 RID: 17602
		// (get) Token: 0x0600F840 RID: 63552 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x170044C3 RID: 17603
		// (get) Token: 0x0600F841 RID: 63553 RVA: 0x002D7B36 File Offset: 0x002D5D36
		internal override int ElementTypeId
		{
			get
			{
				return 13061;
			}
		}

		// Token: 0x0600F842 RID: 63554 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170044C4 RID: 17604
		// (get) Token: 0x0600F843 RID: 63555 RVA: 0x002D7B3D File Offset: 0x002D5D3D
		internal override string[] AttributeTagNames
		{
			get
			{
				return BackstageMenuCheckBox.attributeTagNames;
			}
		}

		// Token: 0x170044C5 RID: 17605
		// (get) Token: 0x0600F844 RID: 63556 RVA: 0x002D7B44 File Offset: 0x002D5D44
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BackstageMenuCheckBox.attributeNamespaceIds;
			}
		}

		// Token: 0x170044C6 RID: 17606
		// (get) Token: 0x0600F845 RID: 63557 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600F846 RID: 63558 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170044C7 RID: 17607
		// (get) Token: 0x0600F847 RID: 63559 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600F848 RID: 63560 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x170044C8 RID: 17608
		// (get) Token: 0x0600F849 RID: 63561 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600F84A RID: 63562 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x170044C9 RID: 17609
		// (get) Token: 0x0600F84B RID: 63563 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600F84C RID: 63564 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x170044CA RID: 17610
		// (get) Token: 0x0600F84D RID: 63565 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600F84E RID: 63566 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x170044CB RID: 17611
		// (get) Token: 0x0600F84F RID: 63567 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600F850 RID: 63568 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x170044CC RID: 17612
		// (get) Token: 0x0600F851 RID: 63569 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600F852 RID: 63570 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "getPressed")]
		public StringValue GetPressed
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

		// Token: 0x170044CD RID: 17613
		// (get) Token: 0x0600F853 RID: 63571 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x0600F854 RID: 63572 RVA: 0x002BD516 File Offset: 0x002BB716
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

		// Token: 0x170044CE RID: 17614
		// (get) Token: 0x0600F855 RID: 63573 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600F856 RID: 63574 RVA: 0x002BD530 File Offset: 0x002BB730
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

		// Token: 0x170044CF RID: 17615
		// (get) Token: 0x0600F857 RID: 63575 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600F858 RID: 63576 RVA: 0x002BD54B File Offset: 0x002BB74B
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

		// Token: 0x170044D0 RID: 17616
		// (get) Token: 0x0600F859 RID: 63577 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600F85A RID: 63578 RVA: 0x002BDB45 File Offset: 0x002BBD45
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

		// Token: 0x170044D1 RID: 17617
		// (get) Token: 0x0600F85B RID: 63579 RVA: 0x002C92FA File Offset: 0x002C74FA
		// (set) Token: 0x0600F85C RID: 63580 RVA: 0x002BDB61 File Offset: 0x002BBD61
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

		// Token: 0x170044D2 RID: 17618
		// (get) Token: 0x0600F85D RID: 63581 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600F85E RID: 63582 RVA: 0x002BDB7D File Offset: 0x002BBD7D
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

		// Token: 0x170044D3 RID: 17619
		// (get) Token: 0x0600F85F RID: 63583 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600F860 RID: 63584 RVA: 0x002BE209 File Offset: 0x002BC409
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

		// Token: 0x170044D4 RID: 17620
		// (get) Token: 0x0600F861 RID: 63585 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600F862 RID: 63586 RVA: 0x002BE225 File Offset: 0x002BC425
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

		// Token: 0x0600F864 RID: 63588 RVA: 0x002D7B4C File Offset: 0x002D5D4C
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
			if (namespaceId == 0 && "getPressed" == name)
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
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600F865 RID: 63589 RVA: 0x002D7CAB File Offset: 0x002D5EAB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BackstageMenuCheckBox>(deep);
		}

		// Token: 0x0600F866 RID: 63590 RVA: 0x002D7CB4 File Offset: 0x002D5EB4
		// Note: this type is marked as 'beforefieldinit'.
		static BackstageMenuCheckBox()
		{
			byte[] array = new byte[15];
			BackstageMenuCheckBox.attributeNamespaceIds = array;
		}

		// Token: 0x04007151 RID: 29009
		private const string tagName = "checkBox";

		// Token: 0x04007152 RID: 29010
		private const byte tagNsId = 57;

		// Token: 0x04007153 RID: 29011
		internal const int ElementTypeIdConst = 13061;

		// Token: 0x04007154 RID: 29012
		private static string[] attributeTagNames = new string[]
		{
			"description", "getDescription", "id", "idQ", "tag", "onAction", "getPressed", "enabled", "getEnabled", "label",
			"getLabel", "visible", "getVisible", "keytip", "getKeytip"
		};

		// Token: 0x04007155 RID: 29013
		private static byte[] attributeNamespaceIds;
	}
}
