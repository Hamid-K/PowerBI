using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022FB RID: 8955
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(BackstageMenuGroup), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class BackstagePrimaryMenu : OpenXmlCompositeElement
	{
		// Token: 0x17004732 RID: 18226
		// (get) Token: 0x0600FD71 RID: 64881 RVA: 0x002CA224 File Offset: 0x002C8424
		public override string LocalName
		{
			get
			{
				return "menu";
			}
		}

		// Token: 0x17004733 RID: 18227
		// (get) Token: 0x0600FD72 RID: 64882 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x17004734 RID: 18228
		// (get) Token: 0x0600FD73 RID: 64883 RVA: 0x002DC4B0 File Offset: 0x002DA6B0
		internal override int ElementTypeId
		{
			get
			{
				return 13098;
			}
		}

		// Token: 0x0600FD74 RID: 64884 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004735 RID: 18229
		// (get) Token: 0x0600FD75 RID: 64885 RVA: 0x002DC4B7 File Offset: 0x002DA6B7
		internal override string[] AttributeTagNames
		{
			get
			{
				return BackstagePrimaryMenu.attributeTagNames;
			}
		}

		// Token: 0x17004736 RID: 18230
		// (get) Token: 0x0600FD76 RID: 64886 RVA: 0x002DC4BE File Offset: 0x002DA6BE
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BackstagePrimaryMenu.attributeNamespaceIds;
			}
		}

		// Token: 0x17004737 RID: 18231
		// (get) Token: 0x0600FD77 RID: 64887 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600FD78 RID: 64888 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "screentip")]
		public StringValue Screentip
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

		// Token: 0x17004738 RID: 18232
		// (get) Token: 0x0600FD79 RID: 64889 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600FD7A RID: 64890 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "getScreentip")]
		public StringValue GetScreentip
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

		// Token: 0x17004739 RID: 18233
		// (get) Token: 0x0600FD7B RID: 64891 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600FD7C RID: 64892 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "supertip")]
		public StringValue Supertip
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

		// Token: 0x1700473A RID: 18234
		// (get) Token: 0x0600FD7D RID: 64893 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600FD7E RID: 64894 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "getSupertip")]
		public StringValue GetSupertip
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

		// Token: 0x1700473B RID: 18235
		// (get) Token: 0x0600FD7F RID: 64895 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600FD80 RID: 64896 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x1700473C RID: 18236
		// (get) Token: 0x0600FD81 RID: 64897 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600FD82 RID: 64898 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "idQ")]
		public StringValue QualifiedId
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

		// Token: 0x1700473D RID: 18237
		// (get) Token: 0x0600FD83 RID: 64899 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600FD84 RID: 64900 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x1700473E RID: 18238
		// (get) Token: 0x0600FD85 RID: 64901 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x0600FD86 RID: 64902 RVA: 0x002BD516 File Offset: 0x002BB716
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

		// Token: 0x1700473F RID: 18239
		// (get) Token: 0x0600FD87 RID: 64903 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600FD88 RID: 64904 RVA: 0x002BD530 File Offset: 0x002BB730
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

		// Token: 0x17004740 RID: 18240
		// (get) Token: 0x0600FD89 RID: 64905 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600FD8A RID: 64906 RVA: 0x002BD54B File Offset: 0x002BB74B
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

		// Token: 0x17004741 RID: 18241
		// (get) Token: 0x0600FD8B RID: 64907 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600FD8C RID: 64908 RVA: 0x002BDB45 File Offset: 0x002BBD45
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

		// Token: 0x17004742 RID: 18242
		// (get) Token: 0x0600FD8D RID: 64909 RVA: 0x002C92FA File Offset: 0x002C74FA
		// (set) Token: 0x0600FD8E RID: 64910 RVA: 0x002BDB61 File Offset: 0x002BBD61
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

		// Token: 0x17004743 RID: 18243
		// (get) Token: 0x0600FD8F RID: 64911 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600FD90 RID: 64912 RVA: 0x002BDB7D File Offset: 0x002BBD7D
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

		// Token: 0x17004744 RID: 18244
		// (get) Token: 0x0600FD91 RID: 64913 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600FD92 RID: 64914 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "image")]
		public StringValue Image
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

		// Token: 0x17004745 RID: 18245
		// (get) Token: 0x0600FD93 RID: 64915 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600FD94 RID: 64916 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "imageMso")]
		public StringValue ImageMso
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

		// Token: 0x17004746 RID: 18246
		// (get) Token: 0x0600FD95 RID: 64917 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600FD96 RID: 64918 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "getImage")]
		public StringValue GetImage
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

		// Token: 0x17004747 RID: 18247
		// (get) Token: 0x0600FD97 RID: 64919 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600FD98 RID: 64920 RVA: 0x002BE25D File Offset: 0x002BC45D
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

		// Token: 0x17004748 RID: 18248
		// (get) Token: 0x0600FD99 RID: 64921 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600FD9A RID: 64922 RVA: 0x002BE279 File Offset: 0x002BC479
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

		// Token: 0x0600FD9B RID: 64923 RVA: 0x00293ECF File Offset: 0x002920CF
		public BackstagePrimaryMenu()
		{
		}

		// Token: 0x0600FD9C RID: 64924 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BackstagePrimaryMenu(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FD9D RID: 64925 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BackstagePrimaryMenu(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FD9E RID: 64926 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BackstagePrimaryMenu(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600FD9F RID: 64927 RVA: 0x002D7D7F File Offset: 0x002D5F7F
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (57 == namespaceId && "menuGroup" == name)
			{
				return new BackstageMenuGroup();
			}
			return null;
		}

		// Token: 0x0600FDA0 RID: 64928 RVA: 0x002DC4C8 File Offset: 0x002DA6C8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
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

		// Token: 0x0600FDA1 RID: 64929 RVA: 0x002DC669 File Offset: 0x002DA869
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BackstagePrimaryMenu>(deep);
		}

		// Token: 0x0600FDA2 RID: 64930 RVA: 0x002DC674 File Offset: 0x002DA874
		// Note: this type is marked as 'beforefieldinit'.
		static BackstagePrimaryMenu()
		{
			byte[] array = new byte[18];
			BackstagePrimaryMenu.attributeNamespaceIds = array;
		}

		// Token: 0x04007202 RID: 29186
		private const string tagName = "menu";

		// Token: 0x04007203 RID: 29187
		private const byte tagNsId = 57;

		// Token: 0x04007204 RID: 29188
		internal const int ElementTypeIdConst = 13098;

		// Token: 0x04007205 RID: 29189
		private static string[] attributeTagNames = new string[]
		{
			"screentip", "getScreentip", "supertip", "getSupertip", "id", "idQ", "tag", "enabled", "getEnabled", "label",
			"getLabel", "visible", "getVisible", "image", "imageMso", "getImage", "keytip", "getKeytip"
		};

		// Token: 0x04007206 RID: 29190
		private static byte[] attributeNamespaceIds;
	}
}
