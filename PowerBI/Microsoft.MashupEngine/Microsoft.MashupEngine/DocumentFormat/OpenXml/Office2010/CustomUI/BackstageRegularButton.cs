using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022FA RID: 8954
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class BackstageRegularButton : OpenXmlLeafElement
	{
		// Token: 0x17004719 RID: 18201
		// (get) Token: 0x0600FD3F RID: 64831 RVA: 0x002C8B1A File Offset: 0x002C6D1A
		public override string LocalName
		{
			get
			{
				return "button";
			}
		}

		// Token: 0x1700471A RID: 18202
		// (get) Token: 0x0600FD40 RID: 64832 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x1700471B RID: 18203
		// (get) Token: 0x0600FD41 RID: 64833 RVA: 0x002DC1EE File Offset: 0x002DA3EE
		internal override int ElementTypeId
		{
			get
			{
				return 13097;
			}
		}

		// Token: 0x0600FD42 RID: 64834 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x1700471C RID: 18204
		// (get) Token: 0x0600FD43 RID: 64835 RVA: 0x002DC1F5 File Offset: 0x002DA3F5
		internal override string[] AttributeTagNames
		{
			get
			{
				return BackstageRegularButton.attributeTagNames;
			}
		}

		// Token: 0x1700471D RID: 18205
		// (get) Token: 0x0600FD44 RID: 64836 RVA: 0x002DC1FC File Offset: 0x002DA3FC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BackstageRegularButton.attributeNamespaceIds;
			}
		}

		// Token: 0x1700471E RID: 18206
		// (get) Token: 0x0600FD45 RID: 64837 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600FD46 RID: 64838 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x1700471F RID: 18207
		// (get) Token: 0x0600FD47 RID: 64839 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600FD48 RID: 64840 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17004720 RID: 18208
		// (get) Token: 0x0600FD49 RID: 64841 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600FD4A RID: 64842 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17004721 RID: 18209
		// (get) Token: 0x0600FD4B RID: 64843 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600FD4C RID: 64844 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x17004722 RID: 18210
		// (get) Token: 0x0600FD4D RID: 64845 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600FD4E RID: 64846 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x17004723 RID: 18211
		// (get) Token: 0x0600FD4F RID: 64847 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600FD50 RID: 64848 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
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

		// Token: 0x17004724 RID: 18212
		// (get) Token: 0x0600FD51 RID: 64849 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600FD52 RID: 64850 RVA: 0x002BD4FC File Offset: 0x002BB6FC
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

		// Token: 0x17004725 RID: 18213
		// (get) Token: 0x0600FD53 RID: 64851 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600FD54 RID: 64852 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "onAction")]
		public StringValue OnAction
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

		// Token: 0x17004726 RID: 18214
		// (get) Token: 0x0600FD55 RID: 64853 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x0600FD56 RID: 64854 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "isDefinitive")]
		public BooleanValue IsDefinitive
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

		// Token: 0x17004727 RID: 18215
		// (get) Token: 0x0600FD57 RID: 64855 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x0600FD58 RID: 64856 RVA: 0x002BD54B File Offset: 0x002BB74B
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

		// Token: 0x17004728 RID: 18216
		// (get) Token: 0x0600FD59 RID: 64857 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600FD5A RID: 64858 RVA: 0x002BDB45 File Offset: 0x002BBD45
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

		// Token: 0x17004729 RID: 18217
		// (get) Token: 0x0600FD5B RID: 64859 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600FD5C RID: 64860 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "label")]
		public StringValue Label
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

		// Token: 0x1700472A RID: 18218
		// (get) Token: 0x0600FD5D RID: 64861 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600FD5E RID: 64862 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "getLabel")]
		public StringValue GetLabel
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

		// Token: 0x1700472B RID: 18219
		// (get) Token: 0x0600FD5F RID: 64863 RVA: 0x002CD15F File Offset: 0x002CB35F
		// (set) Token: 0x0600FD60 RID: 64864 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
		{
			get
			{
				return (BooleanValue)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x1700472C RID: 18220
		// (get) Token: 0x0600FD61 RID: 64865 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600FD62 RID: 64866 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
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

		// Token: 0x1700472D RID: 18221
		// (get) Token: 0x0600FD63 RID: 64867 RVA: 0x002BE231 File Offset: 0x002BC431
		// (set) Token: 0x0600FD64 RID: 64868 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "keytip")]
		public StringValue Keytip
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

		// Token: 0x1700472E RID: 18222
		// (get) Token: 0x0600FD65 RID: 64869 RVA: 0x002BE24D File Offset: 0x002BC44D
		// (set) Token: 0x0600FD66 RID: 64870 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "getKeytip")]
		public StringValue GetKeytip
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

		// Token: 0x1700472F RID: 18223
		// (get) Token: 0x0600FD67 RID: 64871 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600FD68 RID: 64872 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "image")]
		public StringValue Image
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

		// Token: 0x17004730 RID: 18224
		// (get) Token: 0x0600FD69 RID: 64873 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600FD6A RID: 64874 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "imageMso")]
		public StringValue ImageMso
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

		// Token: 0x17004731 RID: 18225
		// (get) Token: 0x0600FD6B RID: 64875 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600FD6C RID: 64876 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "getImage")]
		public StringValue GetImage
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

		// Token: 0x0600FD6E RID: 64878 RVA: 0x002DC204 File Offset: 0x002DA404
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

		// Token: 0x0600FD6F RID: 64879 RVA: 0x002DC3D1 File Offset: 0x002DA5D1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BackstageRegularButton>(deep);
		}

		// Token: 0x0600FD70 RID: 64880 RVA: 0x002DC3DC File Offset: 0x002DA5DC
		// Note: this type is marked as 'beforefieldinit'.
		static BackstageRegularButton()
		{
			byte[] array = new byte[20];
			BackstageRegularButton.attributeNamespaceIds = array;
		}

		// Token: 0x040071FD RID: 29181
		private const string tagName = "button";

		// Token: 0x040071FE RID: 29182
		private const byte tagNsId = 57;

		// Token: 0x040071FF RID: 29183
		internal const int ElementTypeIdConst = 13097;

		// Token: 0x04007200 RID: 29184
		private static string[] attributeTagNames = new string[]
		{
			"screentip", "getScreentip", "supertip", "getSupertip", "id", "idQ", "tag", "onAction", "isDefinitive", "enabled",
			"getEnabled", "label", "getLabel", "visible", "getVisible", "keytip", "getKeytip", "image", "imageMso", "getImage"
		};

		// Token: 0x04007201 RID: 29185
		private static byte[] attributeNamespaceIds;
	}
}
