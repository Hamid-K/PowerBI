using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002716 RID: 10006
	[ChildElementInfo(typeof(RgbColorModelPercentage))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RgbColorModelHex))]
	[ChildElementInfo(typeof(HslColor))]
	[ChildElementInfo(typeof(SystemColor))]
	[ChildElementInfo(typeof(SchemeColor))]
	[ChildElementInfo(typeof(PresetColor))]
	internal class Duotone : OpenXmlCompositeElement
	{
		// Token: 0x17005F0B RID: 24331
		// (get) Token: 0x0601327A RID: 78458 RVA: 0x003043C4 File Offset: 0x003025C4
		public override string LocalName
		{
			get
			{
				return "duotone";
			}
		}

		// Token: 0x17005F0C RID: 24332
		// (get) Token: 0x0601327B RID: 78459 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005F0D RID: 24333
		// (get) Token: 0x0601327C RID: 78460 RVA: 0x003043CB File Offset: 0x003025CB
		internal override int ElementTypeId
		{
			get
			{
				return 10068;
			}
		}

		// Token: 0x0601327D RID: 78461 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601327E RID: 78462 RVA: 0x00293ECF File Offset: 0x002920CF
		public Duotone()
		{
		}

		// Token: 0x0601327F RID: 78463 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Duotone(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013280 RID: 78464 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Duotone(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013281 RID: 78465 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Duotone(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013282 RID: 78466 RVA: 0x003043D4 File Offset: 0x003025D4
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

		// Token: 0x06013283 RID: 78467 RVA: 0x00304472 File Offset: 0x00302672
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Duotone>(deep);
		}

		// Token: 0x040084F3 RID: 34035
		private const string tagName = "duotone";

		// Token: 0x040084F4 RID: 34036
		private const byte tagNsId = 10;

		// Token: 0x040084F5 RID: 34037
		internal const int ElementTypeIdConst = 10068;
	}
}
