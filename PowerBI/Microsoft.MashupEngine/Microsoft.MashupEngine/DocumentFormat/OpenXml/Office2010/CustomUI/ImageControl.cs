using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022E1 RID: 8929
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class ImageControl : OpenXmlLeafElement
	{
		// Token: 0x170045D9 RID: 17881
		// (get) Token: 0x0600FA87 RID: 64135 RVA: 0x002D9CA3 File Offset: 0x002D7EA3
		public override string LocalName
		{
			get
			{
				return "imageControl";
			}
		}

		// Token: 0x170045DA RID: 17882
		// (get) Token: 0x0600FA88 RID: 64136 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x170045DB RID: 17883
		// (get) Token: 0x0600FA89 RID: 64137 RVA: 0x002D9CAA File Offset: 0x002D7EAA
		internal override int ElementTypeId
		{
			get
			{
				return 13074;
			}
		}

		// Token: 0x0600FA8A RID: 64138 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170045DC RID: 17884
		// (get) Token: 0x0600FA8B RID: 64139 RVA: 0x002D9CB1 File Offset: 0x002D7EB1
		internal override string[] AttributeTagNames
		{
			get
			{
				return ImageControl.attributeTagNames;
			}
		}

		// Token: 0x170045DD RID: 17885
		// (get) Token: 0x0600FA8C RID: 64140 RVA: 0x002D9CB8 File Offset: 0x002D7EB8
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ImageControl.attributeNamespaceIds;
			}
		}

		// Token: 0x170045DE RID: 17886
		// (get) Token: 0x0600FA8D RID: 64141 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600FA8E RID: 64142 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public StringValue Id
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

		// Token: 0x170045DF RID: 17887
		// (get) Token: 0x0600FA8F RID: 64143 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600FA90 RID: 64144 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "idQ")]
		public StringValue QualifiedId
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

		// Token: 0x170045E0 RID: 17888
		// (get) Token: 0x0600FA91 RID: 64145 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600FA92 RID: 64146 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "tag")]
		public StringValue Tag
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

		// Token: 0x170045E1 RID: 17889
		// (get) Token: 0x0600FA93 RID: 64147 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x0600FA94 RID: 64148 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x170045E2 RID: 17890
		// (get) Token: 0x0600FA95 RID: 64149 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0600FA96 RID: 64150 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x170045E3 RID: 17891
		// (get) Token: 0x0600FA97 RID: 64151 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x0600FA98 RID: 64152 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "visible")]
		public BooleanValue Visible
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

		// Token: 0x170045E4 RID: 17892
		// (get) Token: 0x0600FA99 RID: 64153 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600FA9A RID: 64154 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "getVisible")]
		public StringValue GetVisible
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

		// Token: 0x170045E5 RID: 17893
		// (get) Token: 0x0600FA9B RID: 64155 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600FA9C RID: 64156 RVA: 0x002BD516 File Offset: 0x002BB716
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

		// Token: 0x170045E6 RID: 17894
		// (get) Token: 0x0600FA9D RID: 64157 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600FA9E RID: 64158 RVA: 0x002BD530 File Offset: 0x002BB730
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

		// Token: 0x170045E7 RID: 17895
		// (get) Token: 0x0600FA9F RID: 64159 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600FAA0 RID: 64160 RVA: 0x002BD54B File Offset: 0x002BB74B
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

		// Token: 0x170045E8 RID: 17896
		// (get) Token: 0x0600FAA1 RID: 64161 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600FAA2 RID: 64162 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "altText")]
		public StringValue AltText
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

		// Token: 0x170045E9 RID: 17897
		// (get) Token: 0x0600FAA3 RID: 64163 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600FAA4 RID: 64164 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "getAltText")]
		public StringValue GetAltText
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

		// Token: 0x0600FAA6 RID: 64166 RVA: 0x002D9CC0 File Offset: 0x002D7EC0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
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
			if (namespaceId == 0 && "altText" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "getAltText" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600FAA7 RID: 64167 RVA: 0x002D9DDD File Offset: 0x002D7FDD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ImageControl>(deep);
		}

		// Token: 0x0600FAA8 RID: 64168 RVA: 0x002D9DE8 File Offset: 0x002D7FE8
		// Note: this type is marked as 'beforefieldinit'.
		static ImageControl()
		{
			byte[] array = new byte[12];
			ImageControl.attributeNamespaceIds = array;
		}

		// Token: 0x04007192 RID: 29074
		private const string tagName = "imageControl";

		// Token: 0x04007193 RID: 29075
		private const byte tagNsId = 57;

		// Token: 0x04007194 RID: 29076
		internal const int ElementTypeIdConst = 13074;

		// Token: 0x04007195 RID: 29077
		private static string[] attributeTagNames = new string[]
		{
			"id", "idQ", "tag", "enabled", "getEnabled", "visible", "getVisible", "image", "imageMso", "getImage",
			"altText", "getAltText"
		};

		// Token: 0x04007196 RID: 29078
		private static byte[] attributeNamespaceIds;
	}
}
