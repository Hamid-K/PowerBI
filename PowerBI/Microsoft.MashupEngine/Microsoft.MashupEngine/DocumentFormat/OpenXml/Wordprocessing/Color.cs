using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E8E RID: 11918
	[GeneratedCode("DomGen", "2.0")]
	internal class Color : OpenXmlLeafElement
	{
		// Token: 0x17008B1E RID: 35614
		// (get) Token: 0x0601952C RID: 103724 RVA: 0x002E847F File Offset: 0x002E667F
		public override string LocalName
		{
			get
			{
				return "color";
			}
		}

		// Token: 0x17008B1F RID: 35615
		// (get) Token: 0x0601952D RID: 103725 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008B20 RID: 35616
		// (get) Token: 0x0601952E RID: 103726 RVA: 0x0034890A File Offset: 0x00346B0A
		internal override int ElementTypeId
		{
			get
			{
				return 11592;
			}
		}

		// Token: 0x0601952F RID: 103727 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008B21 RID: 35617
		// (get) Token: 0x06019530 RID: 103728 RVA: 0x00348911 File Offset: 0x00346B11
		internal override string[] AttributeTagNames
		{
			get
			{
				return Color.attributeTagNames;
			}
		}

		// Token: 0x17008B22 RID: 35618
		// (get) Token: 0x06019531 RID: 103729 RVA: 0x00348918 File Offset: 0x00346B18
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Color.attributeNamespaceIds;
			}
		}

		// Token: 0x17008B23 RID: 35619
		// (get) Token: 0x06019532 RID: 103730 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06019533 RID: 103731 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public StringValue Val
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

		// Token: 0x17008B24 RID: 35620
		// (get) Token: 0x06019534 RID: 103732 RVA: 0x0034891F File Offset: 0x00346B1F
		// (set) Token: 0x06019535 RID: 103733 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "themeColor")]
		public EnumValue<ThemeColorValues> ThemeColor
		{
			get
			{
				return (EnumValue<ThemeColorValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17008B25 RID: 35621
		// (get) Token: 0x06019536 RID: 103734 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06019537 RID: 103735 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "themeTint")]
		public StringValue ThemeTint
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

		// Token: 0x17008B26 RID: 35622
		// (get) Token: 0x06019538 RID: 103736 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x06019539 RID: 103737 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(23, "themeShade")]
		public StringValue ThemeShade
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

		// Token: 0x0601953B RID: 103739 RVA: 0x00348930 File Offset: 0x00346B30
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
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

		// Token: 0x0601953C RID: 103740 RVA: 0x003489A5 File Offset: 0x00346BA5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Color>(deep);
		}

		// Token: 0x0400A852 RID: 43090
		private const string tagName = "color";

		// Token: 0x0400A853 RID: 43091
		private const byte tagNsId = 23;

		// Token: 0x0400A854 RID: 43092
		internal const int ElementTypeIdConst = 11592;

		// Token: 0x0400A855 RID: 43093
		private static string[] attributeTagNames = new string[] { "val", "themeColor", "themeTint", "themeShade" };

		// Token: 0x0400A856 RID: 43094
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23, 23 };
	}
}
