using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E27 RID: 11815
	[GeneratedCode("DomGen", "2.0")]
	internal class Shading : OpenXmlLeafElement
	{
		// Token: 0x17008932 RID: 35122
		// (get) Token: 0x06019128 RID: 102696 RVA: 0x00345FE8 File Offset: 0x003441E8
		public override string LocalName
		{
			get
			{
				return "shd";
			}
		}

		// Token: 0x17008933 RID: 35123
		// (get) Token: 0x06019129 RID: 102697 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008934 RID: 35124
		// (get) Token: 0x0601912A RID: 102698 RVA: 0x00345FEF File Offset: 0x003441EF
		internal override int ElementTypeId
		{
			get
			{
				return 11501;
			}
		}

		// Token: 0x0601912B RID: 102699 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008935 RID: 35125
		// (get) Token: 0x0601912C RID: 102700 RVA: 0x00345FF6 File Offset: 0x003441F6
		internal override string[] AttributeTagNames
		{
			get
			{
				return Shading.attributeTagNames;
			}
		}

		// Token: 0x17008936 RID: 35126
		// (get) Token: 0x0601912D RID: 102701 RVA: 0x00345FFD File Offset: 0x003441FD
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Shading.attributeNamespaceIds;
			}
		}

		// Token: 0x17008937 RID: 35127
		// (get) Token: 0x0601912E RID: 102702 RVA: 0x00346004 File Offset: 0x00344204
		// (set) Token: 0x0601912F RID: 102703 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<ShadingPatternValues> Val
		{
			get
			{
				return (EnumValue<ShadingPatternValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17008938 RID: 35128
		// (get) Token: 0x06019130 RID: 102704 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06019131 RID: 102705 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "color")]
		public StringValue Color
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

		// Token: 0x17008939 RID: 35129
		// (get) Token: 0x06019132 RID: 102706 RVA: 0x00346013 File Offset: 0x00344213
		// (set) Token: 0x06019133 RID: 102707 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "themeColor")]
		public EnumValue<ThemeColorValues> ThemeColor
		{
			get
			{
				return (EnumValue<ThemeColorValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x1700893A RID: 35130
		// (get) Token: 0x06019134 RID: 102708 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x06019135 RID: 102709 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(23, "themeTint")]
		public StringValue ThemeTint
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

		// Token: 0x1700893B RID: 35131
		// (get) Token: 0x06019136 RID: 102710 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x06019137 RID: 102711 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(23, "themeShade")]
		public StringValue ThemeShade
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

		// Token: 0x1700893C RID: 35132
		// (get) Token: 0x06019138 RID: 102712 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x06019139 RID: 102713 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(23, "fill")]
		public StringValue Fill
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

		// Token: 0x1700893D RID: 35133
		// (get) Token: 0x0601913A RID: 102714 RVA: 0x00346022 File Offset: 0x00344222
		// (set) Token: 0x0601913B RID: 102715 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(23, "themeFill")]
		public EnumValue<ThemeColorValues> ThemeFill
		{
			get
			{
				return (EnumValue<ThemeColorValues>)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x1700893E RID: 35134
		// (get) Token: 0x0601913C RID: 102716 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0601913D RID: 102717 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(23, "themeFillTint")]
		public StringValue ThemeFillTint
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

		// Token: 0x1700893F RID: 35135
		// (get) Token: 0x0601913E RID: 102718 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0601913F RID: 102719 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(23, "themeFillShade")]
		public StringValue ThemeFillShade
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

		// Token: 0x06019141 RID: 102721 RVA: 0x00346034 File Offset: 0x00344234
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<ShadingPatternValues>();
			}
			if (23 == namespaceId && "color" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "themeColor" == name)
			{
				return new EnumValue<ThemeColorValues>();
			}
			if (23 == namespaceId && "themeTint" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "themeShade" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "fill" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "themeFill" == name)
			{
				return new EnumValue<ThemeColorValues>();
			}
			if (23 == namespaceId && "themeFillTint" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "themeFillShade" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019142 RID: 102722 RVA: 0x00346121 File Offset: 0x00344321
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Shading>(deep);
		}

		// Token: 0x0400A6E6 RID: 42726
		private const string tagName = "shd";

		// Token: 0x0400A6E7 RID: 42727
		private const byte tagNsId = 23;

		// Token: 0x0400A6E8 RID: 42728
		internal const int ElementTypeIdConst = 11501;

		// Token: 0x0400A6E9 RID: 42729
		private static string[] attributeTagNames = new string[] { "val", "color", "themeColor", "themeTint", "themeShade", "fill", "themeFill", "themeFillTint", "themeFillShade" };

		// Token: 0x0400A6EA RID: 42730
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23, 23, 23, 23, 23, 23, 23 };
	}
}
