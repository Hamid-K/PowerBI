using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AAB RID: 10923
	[ChildElementInfo(typeof(SystemColor))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RgbColorModelHex))]
	[ChildElementInfo(typeof(RgbColorModelPercentage))]
	[ChildElementInfo(typeof(HslColor))]
	[ChildElementInfo(typeof(SchemeColor))]
	[ChildElementInfo(typeof(PresetColor))]
	internal class ColorMostRecentlyUsed : OpenXmlCompositeElement
	{
		// Token: 0x170074A2 RID: 29858
		// (get) Token: 0x060163A2 RID: 91042 RVA: 0x00327FFB File Offset: 0x003261FB
		public override string LocalName
		{
			get
			{
				return "clrMru";
			}
		}

		// Token: 0x170074A3 RID: 29859
		// (get) Token: 0x060163A3 RID: 91043 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170074A4 RID: 29860
		// (get) Token: 0x060163A4 RID: 91044 RVA: 0x00328002 File Offset: 0x00326202
		internal override int ElementTypeId
		{
			get
			{
				return 12337;
			}
		}

		// Token: 0x060163A5 RID: 91045 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060163A6 RID: 91046 RVA: 0x00293ECF File Offset: 0x002920CF
		public ColorMostRecentlyUsed()
		{
		}

		// Token: 0x060163A7 RID: 91047 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ColorMostRecentlyUsed(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060163A8 RID: 91048 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ColorMostRecentlyUsed(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060163A9 RID: 91049 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ColorMostRecentlyUsed(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060163AA RID: 91050 RVA: 0x0032800C File Offset: 0x0032620C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
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
			return null;
		}

		// Token: 0x060163AB RID: 91051 RVA: 0x003280AA File Offset: 0x003262AA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColorMostRecentlyUsed>(deep);
		}

		// Token: 0x040096C1 RID: 38593
		private const string tagName = "clrMru";

		// Token: 0x040096C2 RID: 38594
		private const byte tagNsId = 24;

		// Token: 0x040096C3 RID: 38595
		internal const int ElementTypeIdConst = 12337;
	}
}
