using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E9A RID: 11930
	[GeneratedCode("DomGen", "2.0")]
	internal class Underline : OpenXmlLeafElement
	{
		// Token: 0x17008B57 RID: 35671
		// (get) Token: 0x0601959F RID: 103839 RVA: 0x00333427 File Offset: 0x00331627
		public override string LocalName
		{
			get
			{
				return "u";
			}
		}

		// Token: 0x17008B58 RID: 35672
		// (get) Token: 0x060195A0 RID: 103840 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008B59 RID: 35673
		// (get) Token: 0x060195A1 RID: 103841 RVA: 0x00348CD4 File Offset: 0x00346ED4
		internal override int ElementTypeId
		{
			get
			{
				return 11600;
			}
		}

		// Token: 0x060195A2 RID: 103842 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008B5A RID: 35674
		// (get) Token: 0x060195A3 RID: 103843 RVA: 0x00348CDB File Offset: 0x00346EDB
		internal override string[] AttributeTagNames
		{
			get
			{
				return Underline.attributeTagNames;
			}
		}

		// Token: 0x17008B5B RID: 35675
		// (get) Token: 0x060195A4 RID: 103844 RVA: 0x00348CE2 File Offset: 0x00346EE2
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Underline.attributeNamespaceIds;
			}
		}

		// Token: 0x17008B5C RID: 35676
		// (get) Token: 0x060195A5 RID: 103845 RVA: 0x00348CE9 File Offset: 0x00346EE9
		// (set) Token: 0x060195A6 RID: 103846 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<UnderlineValues> Val
		{
			get
			{
				return (EnumValue<UnderlineValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17008B5D RID: 35677
		// (get) Token: 0x060195A7 RID: 103847 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x060195A8 RID: 103848 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17008B5E RID: 35678
		// (get) Token: 0x060195A9 RID: 103849 RVA: 0x00346013 File Offset: 0x00344213
		// (set) Token: 0x060195AA RID: 103850 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17008B5F RID: 35679
		// (get) Token: 0x060195AB RID: 103851 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x060195AC RID: 103852 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x17008B60 RID: 35680
		// (get) Token: 0x060195AD RID: 103853 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x060195AE RID: 103854 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x060195B0 RID: 103856 RVA: 0x00348CF8 File Offset: 0x00346EF8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<UnderlineValues>();
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
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060195B1 RID: 103857 RVA: 0x00348D85 File Offset: 0x00346F85
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Underline>(deep);
		}

		// Token: 0x0400A881 RID: 43137
		private const string tagName = "u";

		// Token: 0x0400A882 RID: 43138
		private const byte tagNsId = 23;

		// Token: 0x0400A883 RID: 43139
		internal const int ElementTypeIdConst = 11600;

		// Token: 0x0400A884 RID: 43140
		private static string[] attributeTagNames = new string[] { "val", "color", "themeColor", "themeTint", "themeShade" };

		// Token: 0x0400A885 RID: 43141
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23, 23, 23 };
	}
}
