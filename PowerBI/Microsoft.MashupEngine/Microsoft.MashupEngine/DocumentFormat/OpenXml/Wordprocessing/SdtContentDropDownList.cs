using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x0200300C RID: 12300
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ListItem))]
	internal class SdtContentDropDownList : OpenXmlCompositeElement
	{
		// Token: 0x1700965E RID: 38494
		// (get) Token: 0x0601AD90 RID: 109968 RVA: 0x00368681 File Offset: 0x00366881
		public override string LocalName
		{
			get
			{
				return "dropDownList";
			}
		}

		// Token: 0x1700965F RID: 38495
		// (get) Token: 0x0601AD91 RID: 109969 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009660 RID: 38496
		// (get) Token: 0x0601AD92 RID: 109970 RVA: 0x00368688 File Offset: 0x00366888
		internal override int ElementTypeId
		{
			get
			{
				return 12152;
			}
		}

		// Token: 0x0601AD93 RID: 109971 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009661 RID: 38497
		// (get) Token: 0x0601AD94 RID: 109972 RVA: 0x0036868F File Offset: 0x0036688F
		internal override string[] AttributeTagNames
		{
			get
			{
				return SdtContentDropDownList.attributeTagNames;
			}
		}

		// Token: 0x17009662 RID: 38498
		// (get) Token: 0x0601AD95 RID: 109973 RVA: 0x00368696 File Offset: 0x00366896
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SdtContentDropDownList.attributeNamespaceIds;
			}
		}

		// Token: 0x17009663 RID: 38499
		// (get) Token: 0x0601AD96 RID: 109974 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601AD97 RID: 109975 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "lastValue")]
		public StringValue LastValue
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

		// Token: 0x0601AD98 RID: 109976 RVA: 0x00293ECF File Offset: 0x002920CF
		public SdtContentDropDownList()
		{
		}

		// Token: 0x0601AD99 RID: 109977 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SdtContentDropDownList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AD9A RID: 109978 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SdtContentDropDownList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AD9B RID: 109979 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SdtContentDropDownList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601AD9C RID: 109980 RVA: 0x00368349 File Offset: 0x00366549
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "listItem" == name)
			{
				return new ListItem();
			}
			return null;
		}

		// Token: 0x0601AD9D RID: 109981 RVA: 0x00368364 File Offset: 0x00366564
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "lastValue" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601AD9E RID: 109982 RVA: 0x0036869D File Offset: 0x0036689D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SdtContentDropDownList>(deep);
		}

		// Token: 0x0400AE84 RID: 44676
		private const string tagName = "dropDownList";

		// Token: 0x0400AE85 RID: 44677
		private const byte tagNsId = 23;

		// Token: 0x0400AE86 RID: 44678
		internal const int ElementTypeIdConst = 12152;

		// Token: 0x0400AE87 RID: 44679
		private static string[] attributeTagNames = new string[] { "lastValue" };

		// Token: 0x0400AE88 RID: 44680
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
