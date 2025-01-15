using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022D6 RID: 8918
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class BackstageMenuToggleButton : OpenXmlLeafElement
	{
		// Token: 0x170044EA RID: 17642
		// (get) Token: 0x0600F895 RID: 63637 RVA: 0x002C99C0 File Offset: 0x002C7BC0
		public override string LocalName
		{
			get
			{
				return "toggleButton";
			}
		}

		// Token: 0x170044EB RID: 17643
		// (get) Token: 0x0600F896 RID: 63638 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x170044EC RID: 17644
		// (get) Token: 0x0600F897 RID: 63639 RVA: 0x002D7FCC File Offset: 0x002D61CC
		internal override int ElementTypeId
		{
			get
			{
				return 13063;
			}
		}

		// Token: 0x0600F898 RID: 63640 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170044ED RID: 17645
		// (get) Token: 0x0600F899 RID: 63641 RVA: 0x002D7FD3 File Offset: 0x002D61D3
		internal override string[] AttributeTagNames
		{
			get
			{
				return BackstageMenuToggleButton.attributeTagNames;
			}
		}

		// Token: 0x170044EE RID: 17646
		// (get) Token: 0x0600F89A RID: 63642 RVA: 0x002D7FDA File Offset: 0x002D61DA
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BackstageMenuToggleButton.attributeNamespaceIds;
			}
		}

		// Token: 0x170044EF RID: 17647
		// (get) Token: 0x0600F89B RID: 63643 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600F89C RID: 63644 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "image")]
		public StringValue Image
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

		// Token: 0x170044F0 RID: 17648
		// (get) Token: 0x0600F89D RID: 63645 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600F89E RID: 63646 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "imageMso")]
		public StringValue ImageMso
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

		// Token: 0x170044F1 RID: 17649
		// (get) Token: 0x0600F89F RID: 63647 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600F8A0 RID: 63648 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "getImage")]
		public StringValue GetImage
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

		// Token: 0x170044F2 RID: 17650
		// (get) Token: 0x0600F8A1 RID: 63649 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600F8A2 RID: 63650 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "description")]
		public StringValue Description
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

		// Token: 0x170044F3 RID: 17651
		// (get) Token: 0x0600F8A3 RID: 63651 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600F8A4 RID: 63652 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "getDescription")]
		public StringValue GetDescription
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

		// Token: 0x170044F4 RID: 17652
		// (get) Token: 0x0600F8A5 RID: 63653 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600F8A6 RID: 63654 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x170044F5 RID: 17653
		// (get) Token: 0x0600F8A7 RID: 63655 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600F8A8 RID: 63656 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "idQ")]
		public StringValue QualifiedId
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

		// Token: 0x170044F6 RID: 17654
		// (get) Token: 0x0600F8A9 RID: 63657 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600F8AA RID: 63658 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x170044F7 RID: 17655
		// (get) Token: 0x0600F8AB RID: 63659 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600F8AC RID: 63660 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "onAction")]
		public StringValue OnAction
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

		// Token: 0x170044F8 RID: 17656
		// (get) Token: 0x0600F8AD RID: 63661 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600F8AE RID: 63662 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "getPressed")]
		public StringValue GetPressed
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

		// Token: 0x170044F9 RID: 17657
		// (get) Token: 0x0600F8AF RID: 63663 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x0600F8B0 RID: 63664 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
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

		// Token: 0x170044FA RID: 17658
		// (get) Token: 0x0600F8B1 RID: 63665 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600F8B2 RID: 63666 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x170044FB RID: 17659
		// (get) Token: 0x0600F8B3 RID: 63667 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600F8B4 RID: 63668 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "label")]
		public StringValue Label
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

		// Token: 0x170044FC RID: 17660
		// (get) Token: 0x0600F8B5 RID: 63669 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600F8B6 RID: 63670 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
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

		// Token: 0x170044FD RID: 17661
		// (get) Token: 0x0600F8B7 RID: 63671 RVA: 0x002C9F8A File Offset: 0x002C818A
		// (set) Token: 0x0600F8B8 RID: 63672 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
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

		// Token: 0x170044FE RID: 17662
		// (get) Token: 0x0600F8B9 RID: 63673 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600F8BA RID: 63674 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
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

		// Token: 0x170044FF RID: 17663
		// (get) Token: 0x0600F8BB RID: 63675 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600F8BC RID: 63676 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
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

		// Token: 0x17004500 RID: 17664
		// (get) Token: 0x0600F8BD RID: 63677 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600F8BE RID: 63678 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
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

		// Token: 0x0600F8C0 RID: 63680 RVA: 0x002D7FE4 File Offset: 0x002D61E4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
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

		// Token: 0x0600F8C1 RID: 63681 RVA: 0x002D8185 File Offset: 0x002D6385
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BackstageMenuToggleButton>(deep);
		}

		// Token: 0x0600F8C2 RID: 63682 RVA: 0x002D8190 File Offset: 0x002D6390
		// Note: this type is marked as 'beforefieldinit'.
		static BackstageMenuToggleButton()
		{
			byte[] array = new byte[18];
			BackstageMenuToggleButton.attributeNamespaceIds = array;
		}

		// Token: 0x0400715B RID: 29019
		private const string tagName = "toggleButton";

		// Token: 0x0400715C RID: 29020
		private const byte tagNsId = 57;

		// Token: 0x0400715D RID: 29021
		internal const int ElementTypeIdConst = 13063;

		// Token: 0x0400715E RID: 29022
		private static string[] attributeTagNames = new string[]
		{
			"image", "imageMso", "getImage", "description", "getDescription", "id", "idQ", "tag", "onAction", "getPressed",
			"enabled", "getEnabled", "label", "getLabel", "visible", "getVisible", "keytip", "getKeytip"
		};

		// Token: 0x0400715F RID: 29023
		private static byte[] attributeNamespaceIds;
	}
}
