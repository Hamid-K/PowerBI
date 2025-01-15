using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FDF RID: 12255
	[GeneratedCode("DomGen", "2.0")]
	internal class StylePaneSortMethods : OpenXmlLeafElement
	{
		// Token: 0x170094C2 RID: 38082
		// (get) Token: 0x0601AA1F RID: 109087 RVA: 0x00365311 File Offset: 0x00363511
		public override string LocalName
		{
			get
			{
				return "stylePaneSortMethod";
			}
		}

		// Token: 0x170094C3 RID: 38083
		// (get) Token: 0x0601AA20 RID: 109088 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170094C4 RID: 38084
		// (get) Token: 0x0601AA21 RID: 109089 RVA: 0x00365318 File Offset: 0x00363518
		internal override int ElementTypeId
		{
			get
			{
				return 11985;
			}
		}

		// Token: 0x0601AA22 RID: 109090 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170094C5 RID: 38085
		// (get) Token: 0x0601AA23 RID: 109091 RVA: 0x0036531F File Offset: 0x0036351F
		internal override string[] AttributeTagNames
		{
			get
			{
				return StylePaneSortMethods.attributeTagNames;
			}
		}

		// Token: 0x170094C6 RID: 38086
		// (get) Token: 0x0601AA24 RID: 109092 RVA: 0x00365326 File Offset: 0x00363526
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return StylePaneSortMethods.attributeNamespaceIds;
			}
		}

		// Token: 0x170094C7 RID: 38087
		// (get) Token: 0x0601AA25 RID: 109093 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601AA26 RID: 109094 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x0601AA28 RID: 109096 RVA: 0x00344715 File Offset: 0x00342915
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601AA29 RID: 109097 RVA: 0x0036532D File Offset: 0x0036352D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StylePaneSortMethods>(deep);
		}

		// Token: 0x0400ADCA RID: 44490
		private const string tagName = "stylePaneSortMethod";

		// Token: 0x0400ADCB RID: 44491
		private const byte tagNsId = 23;

		// Token: 0x0400ADCC RID: 44492
		internal const int ElementTypeIdConst = 11985;

		// Token: 0x0400ADCD RID: 44493
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400ADCE RID: 44494
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
