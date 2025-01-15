using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022D5 RID: 8917
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(BackstageMenuGroup), FileFormatVersions.Office2010)]
	internal class BackstageSubMenu : OpenXmlCompositeElement
	{
		// Token: 0x170044D5 RID: 17621
		// (get) Token: 0x0600F867 RID: 63591 RVA: 0x002CA224 File Offset: 0x002C8424
		public override string LocalName
		{
			get
			{
				return "menu";
			}
		}

		// Token: 0x170044D6 RID: 17622
		// (get) Token: 0x0600F868 RID: 63592 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x170044D7 RID: 17623
		// (get) Token: 0x0600F869 RID: 63593 RVA: 0x002D7D5B File Offset: 0x002D5F5B
		internal override int ElementTypeId
		{
			get
			{
				return 13062;
			}
		}

		// Token: 0x0600F86A RID: 63594 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170044D8 RID: 17624
		// (get) Token: 0x0600F86B RID: 63595 RVA: 0x002D7D62 File Offset: 0x002D5F62
		internal override string[] AttributeTagNames
		{
			get
			{
				return BackstageSubMenu.attributeTagNames;
			}
		}

		// Token: 0x170044D9 RID: 17625
		// (get) Token: 0x0600F86C RID: 63596 RVA: 0x002D7D69 File Offset: 0x002D5F69
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BackstageSubMenu.attributeNamespaceIds;
			}
		}

		// Token: 0x170044DA RID: 17626
		// (get) Token: 0x0600F86D RID: 63597 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600F86E RID: 63598 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170044DB RID: 17627
		// (get) Token: 0x0600F86F RID: 63599 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600F870 RID: 63600 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x170044DC RID: 17628
		// (get) Token: 0x0600F871 RID: 63601 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600F872 RID: 63602 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x170044DD RID: 17629
		// (get) Token: 0x0600F873 RID: 63603 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600F874 RID: 63604 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x170044DE RID: 17630
		// (get) Token: 0x0600F875 RID: 63605 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600F876 RID: 63606 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x170044DF RID: 17631
		// (get) Token: 0x0600F877 RID: 63607 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x0600F878 RID: 63608 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "enabled")]
		public BooleanValue Enabled
		{
			get
			{
				return (BooleanValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x170044E0 RID: 17632
		// (get) Token: 0x0600F879 RID: 63609 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600F87A RID: 63610 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "getEnabled")]
		public StringValue GetEnabled
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

		// Token: 0x170044E1 RID: 17633
		// (get) Token: 0x0600F87B RID: 63611 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600F87C RID: 63612 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "label")]
		public StringValue Label
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

		// Token: 0x170044E2 RID: 17634
		// (get) Token: 0x0600F87D RID: 63613 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600F87E RID: 63614 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
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

		// Token: 0x170044E3 RID: 17635
		// (get) Token: 0x0600F87F RID: 63615 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x0600F880 RID: 63616 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
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

		// Token: 0x170044E4 RID: 17636
		// (get) Token: 0x0600F881 RID: 63617 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600F882 RID: 63618 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
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

		// Token: 0x170044E5 RID: 17637
		// (get) Token: 0x0600F883 RID: 63619 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600F884 RID: 63620 RVA: 0x002BDB61 File Offset: 0x002BBD61
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

		// Token: 0x170044E6 RID: 17638
		// (get) Token: 0x0600F885 RID: 63621 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600F886 RID: 63622 RVA: 0x002BDB7D File Offset: 0x002BBD7D
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

		// Token: 0x170044E7 RID: 17639
		// (get) Token: 0x0600F887 RID: 63623 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600F888 RID: 63624 RVA: 0x002BE209 File Offset: 0x002BC409
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

		// Token: 0x170044E8 RID: 17640
		// (get) Token: 0x0600F889 RID: 63625 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600F88A RID: 63626 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
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

		// Token: 0x170044E9 RID: 17641
		// (get) Token: 0x0600F88B RID: 63627 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600F88C RID: 63628 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
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

		// Token: 0x0600F88D RID: 63629 RVA: 0x00293ECF File Offset: 0x002920CF
		public BackstageSubMenu()
		{
		}

		// Token: 0x0600F88E RID: 63630 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BackstageSubMenu(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F88F RID: 63631 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BackstageSubMenu(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600F890 RID: 63632 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BackstageSubMenu(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600F891 RID: 63633 RVA: 0x002D7D7F File Offset: 0x002D5F7F
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (57 == namespaceId && "menuGroup" == name)
			{
				return new BackstageMenuGroup();
			}
			return null;
		}

		// Token: 0x0600F892 RID: 63634 RVA: 0x002D7D9C File Offset: 0x002D5F9C
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

		// Token: 0x0600F893 RID: 63635 RVA: 0x002D7F11 File Offset: 0x002D6111
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BackstageSubMenu>(deep);
		}

		// Token: 0x0600F894 RID: 63636 RVA: 0x002D7F1C File Offset: 0x002D611C
		// Note: this type is marked as 'beforefieldinit'.
		static BackstageSubMenu()
		{
			byte[] array = new byte[16];
			BackstageSubMenu.attributeNamespaceIds = array;
		}

		// Token: 0x04007156 RID: 29014
		private const string tagName = "menu";

		// Token: 0x04007157 RID: 29015
		private const byte tagNsId = 57;

		// Token: 0x04007158 RID: 29016
		internal const int ElementTypeIdConst = 13062;

		// Token: 0x04007159 RID: 29017
		private static string[] attributeTagNames = new string[]
		{
			"description", "getDescription", "id", "idQ", "tag", "enabled", "getEnabled", "label", "getLabel", "visible",
			"getVisible", "image", "imageMso", "getImage", "keytip", "getKeytip"
		};

		// Token: 0x0400715A RID: 29018
		private static byte[] attributeNamespaceIds;
	}
}
