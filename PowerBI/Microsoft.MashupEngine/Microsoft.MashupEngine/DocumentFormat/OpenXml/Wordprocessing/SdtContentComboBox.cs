using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02003007 RID: 12295
	[ChildElementInfo(typeof(ListItem))]
	[GeneratedCode("DomGen", "2.0")]
	internal class SdtContentComboBox : OpenXmlCompositeElement
	{
		// Token: 0x1700963F RID: 38463
		// (get) Token: 0x0601AD44 RID: 109892 RVA: 0x002CC6B7 File Offset: 0x002CA8B7
		public override string LocalName
		{
			get
			{
				return "comboBox";
			}
		}

		// Token: 0x17009640 RID: 38464
		// (get) Token: 0x0601AD45 RID: 109893 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009641 RID: 38465
		// (get) Token: 0x0601AD46 RID: 109894 RVA: 0x00368334 File Offset: 0x00366534
		internal override int ElementTypeId
		{
			get
			{
				return 12148;
			}
		}

		// Token: 0x0601AD47 RID: 109895 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009642 RID: 38466
		// (get) Token: 0x0601AD48 RID: 109896 RVA: 0x0036833B File Offset: 0x0036653B
		internal override string[] AttributeTagNames
		{
			get
			{
				return SdtContentComboBox.attributeTagNames;
			}
		}

		// Token: 0x17009643 RID: 38467
		// (get) Token: 0x0601AD49 RID: 109897 RVA: 0x00368342 File Offset: 0x00366542
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SdtContentComboBox.attributeNamespaceIds;
			}
		}

		// Token: 0x17009644 RID: 38468
		// (get) Token: 0x0601AD4A RID: 109898 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601AD4B RID: 109899 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x0601AD4C RID: 109900 RVA: 0x00293ECF File Offset: 0x002920CF
		public SdtContentComboBox()
		{
		}

		// Token: 0x0601AD4D RID: 109901 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SdtContentComboBox(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AD4E RID: 109902 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SdtContentComboBox(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AD4F RID: 109903 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SdtContentComboBox(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601AD50 RID: 109904 RVA: 0x00368349 File Offset: 0x00366549
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "listItem" == name)
			{
				return new ListItem();
			}
			return null;
		}

		// Token: 0x0601AD51 RID: 109905 RVA: 0x00368364 File Offset: 0x00366564
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "lastValue" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601AD52 RID: 109906 RVA: 0x00368386 File Offset: 0x00366586
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SdtContentComboBox>(deep);
		}

		// Token: 0x0400AE70 RID: 44656
		private const string tagName = "comboBox";

		// Token: 0x0400AE71 RID: 44657
		private const byte tagNsId = 23;

		// Token: 0x0400AE72 RID: 44658
		internal const int ElementTypeIdConst = 12148;

		// Token: 0x0400AE73 RID: 44659
		private static string[] attributeTagNames = new string[] { "lastValue" };

		// Token: 0x0400AE74 RID: 44660
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
