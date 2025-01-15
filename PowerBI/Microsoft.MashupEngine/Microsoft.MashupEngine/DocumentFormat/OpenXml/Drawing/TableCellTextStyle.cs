using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002801 RID: 10241
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Fonts))]
	[ChildElementInfo(typeof(FontReference))]
	[ChildElementInfo(typeof(RgbColorModelPercentage))]
	[ChildElementInfo(typeof(RgbColorModelHex))]
	[ChildElementInfo(typeof(HslColor))]
	[ChildElementInfo(typeof(SystemColor))]
	[ChildElementInfo(typeof(SchemeColor))]
	[ChildElementInfo(typeof(PresetColor))]
	internal class TableCellTextStyle : OpenXmlCompositeElement
	{
		// Token: 0x17006528 RID: 25896
		// (get) Token: 0x0601404B RID: 81995 RVA: 0x0030E692 File Offset: 0x0030C892
		public override string LocalName
		{
			get
			{
				return "tcTxStyle";
			}
		}

		// Token: 0x17006529 RID: 25897
		// (get) Token: 0x0601404C RID: 81996 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700652A RID: 25898
		// (get) Token: 0x0601404D RID: 81997 RVA: 0x0030E699 File Offset: 0x0030C899
		internal override int ElementTypeId
		{
			get
			{
				return 10277;
			}
		}

		// Token: 0x0601404E RID: 81998 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700652B RID: 25899
		// (get) Token: 0x0601404F RID: 81999 RVA: 0x0030E6A0 File Offset: 0x0030C8A0
		internal override string[] AttributeTagNames
		{
			get
			{
				return TableCellTextStyle.attributeTagNames;
			}
		}

		// Token: 0x1700652C RID: 25900
		// (get) Token: 0x06014050 RID: 82000 RVA: 0x0030E6A7 File Offset: 0x0030C8A7
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TableCellTextStyle.attributeNamespaceIds;
			}
		}

		// Token: 0x1700652D RID: 25901
		// (get) Token: 0x06014051 RID: 82001 RVA: 0x0030E6AE File Offset: 0x0030C8AE
		// (set) Token: 0x06014052 RID: 82002 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "b")]
		public EnumValue<BooleanStyleValues> Bold
		{
			get
			{
				return (EnumValue<BooleanStyleValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x1700652E RID: 25902
		// (get) Token: 0x06014053 RID: 82003 RVA: 0x0030E6BD File Offset: 0x0030C8BD
		// (set) Token: 0x06014054 RID: 82004 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "i")]
		public EnumValue<BooleanStyleValues> Italic
		{
			get
			{
				return (EnumValue<BooleanStyleValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06014055 RID: 82005 RVA: 0x00293ECF File Offset: 0x002920CF
		public TableCellTextStyle()
		{
		}

		// Token: 0x06014056 RID: 82006 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TableCellTextStyle(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014057 RID: 82007 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TableCellTextStyle(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014058 RID: 82008 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TableCellTextStyle(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014059 RID: 82009 RVA: 0x0030E6CC File Offset: 0x0030C8CC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "font" == name)
			{
				return new Fonts();
			}
			if (10 == namespaceId && "fontRef" == name)
			{
				return new FontReference();
			}
			if (10 == namespaceId && "scrgbClr" == name)
			{
				return new RgbColorModelPercentage();
			}
			if (10 == namespaceId && "srgbClr" == name)
			{
				return new RgbColorModelHex();
			}
			if (10 == namespaceId && "hslClr" == name)
			{
				return new HslColor();
			}
			if (10 == namespaceId && "sysClr" == name)
			{
				return new SystemColor();
			}
			if (10 == namespaceId && "schemeClr" == name)
			{
				return new SchemeColor();
			}
			if (10 == namespaceId && "prstClr" == name)
			{
				return new PresetColor();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x0601405A RID: 82010 RVA: 0x0030E7B2 File Offset: 0x0030C9B2
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "b" == name)
			{
				return new EnumValue<BooleanStyleValues>();
			}
			if (namespaceId == 0 && "i" == name)
			{
				return new EnumValue<BooleanStyleValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601405B RID: 82011 RVA: 0x0030E7E8 File Offset: 0x0030C9E8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableCellTextStyle>(deep);
		}

		// Token: 0x0601405C RID: 82012 RVA: 0x0030E7F4 File Offset: 0x0030C9F4
		// Note: this type is marked as 'beforefieldinit'.
		static TableCellTextStyle()
		{
			byte[] array = new byte[2];
			TableCellTextStyle.attributeNamespaceIds = array;
		}

		// Token: 0x040088A9 RID: 34985
		private const string tagName = "tcTxStyle";

		// Token: 0x040088AA RID: 34986
		private const byte tagNsId = 10;

		// Token: 0x040088AB RID: 34987
		internal const int ElementTypeIdConst = 10277;

		// Token: 0x040088AC RID: 34988
		private static string[] attributeTagNames = new string[] { "b", "i" };

		// Token: 0x040088AD RID: 34989
		private static byte[] attributeNamespaceIds;
	}
}
