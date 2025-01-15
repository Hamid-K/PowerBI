using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E9C RID: 11932
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class BorderType : OpenXmlLeafElement
	{
		// Token: 0x17008B67 RID: 35687
		// (get) Token: 0x060195BF RID: 103871 RVA: 0x00348E6C File Offset: 0x0034706C
		internal override string[] AttributeTagNames
		{
			get
			{
				return BorderType.attributeTagNames;
			}
		}

		// Token: 0x17008B68 RID: 35688
		// (get) Token: 0x060195C0 RID: 103872 RVA: 0x00348E73 File Offset: 0x00347073
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BorderType.attributeNamespaceIds;
			}
		}

		// Token: 0x17008B69 RID: 35689
		// (get) Token: 0x060195C1 RID: 103873 RVA: 0x00348E7A File Offset: 0x0034707A
		// (set) Token: 0x060195C2 RID: 103874 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<BorderValues> Val
		{
			get
			{
				return (EnumValue<BorderValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17008B6A RID: 35690
		// (get) Token: 0x060195C3 RID: 103875 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x060195C4 RID: 103876 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17008B6B RID: 35691
		// (get) Token: 0x060195C5 RID: 103877 RVA: 0x00346013 File Offset: 0x00344213
		// (set) Token: 0x060195C6 RID: 103878 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17008B6C RID: 35692
		// (get) Token: 0x060195C7 RID: 103879 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x060195C8 RID: 103880 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x17008B6D RID: 35693
		// (get) Token: 0x060195C9 RID: 103881 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x060195CA RID: 103882 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x17008B6E RID: 35694
		// (get) Token: 0x060195CB RID: 103883 RVA: 0x002E6EEB File Offset: 0x002E50EB
		// (set) Token: 0x060195CC RID: 103884 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(23, "sz")]
		public UInt32Value Size
		{
			get
			{
				return (UInt32Value)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17008B6F RID: 35695
		// (get) Token: 0x060195CD RID: 103885 RVA: 0x002E6C60 File Offset: 0x002E4E60
		// (set) Token: 0x060195CE RID: 103886 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(23, "space")]
		public UInt32Value Space
		{
			get
			{
				return (UInt32Value)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17008B70 RID: 35696
		// (get) Token: 0x060195CF RID: 103887 RVA: 0x00348E89 File Offset: 0x00347089
		// (set) Token: 0x060195D0 RID: 103888 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(23, "shadow")]
		public OnOffValue Shadow
		{
			get
			{
				return (OnOffValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17008B71 RID: 35697
		// (get) Token: 0x060195D1 RID: 103889 RVA: 0x00348E98 File Offset: 0x00347098
		// (set) Token: 0x060195D2 RID: 103890 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(23, "frame")]
		public OnOffValue Frame
		{
			get
			{
				return (OnOffValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x060195D3 RID: 103891 RVA: 0x00348EA8 File Offset: 0x003470A8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<BorderValues>();
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
			if (23 == namespaceId && "sz" == name)
			{
				return new UInt32Value();
			}
			if (23 == namespaceId && "space" == name)
			{
				return new UInt32Value();
			}
			if (23 == namespaceId && "shadow" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "frame" == name)
			{
				return new OnOffValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0400A88B RID: 43147
		private static string[] attributeTagNames = new string[] { "val", "color", "themeColor", "themeTint", "themeShade", "sz", "space", "shadow", "frame" };

		// Token: 0x0400A88C RID: 43148
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23, 23, 23, 23, 23, 23, 23 };
	}
}
