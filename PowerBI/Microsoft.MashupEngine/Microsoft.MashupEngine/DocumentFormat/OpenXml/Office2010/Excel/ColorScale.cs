using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x0200241A RID: 9242
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(Color), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ConditionalFormattingValueObject), FileFormatVersions.Office2010)]
	internal class ColorScale : OpenXmlCompositeElement
	{
		// Token: 0x17004F25 RID: 20261
		// (get) Token: 0x06010EF1 RID: 69361 RVA: 0x002E8C0F File Offset: 0x002E6E0F
		public override string LocalName
		{
			get
			{
				return "colorScale";
			}
		}

		// Token: 0x17004F26 RID: 20262
		// (get) Token: 0x06010EF2 RID: 69362 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004F27 RID: 20263
		// (get) Token: 0x06010EF3 RID: 69363 RVA: 0x002E8C16 File Offset: 0x002E6E16
		internal override int ElementTypeId
		{
			get
			{
				return 12960;
			}
		}

		// Token: 0x06010EF4 RID: 69364 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010EF5 RID: 69365 RVA: 0x00293ECF File Offset: 0x002920CF
		public ColorScale()
		{
		}

		// Token: 0x06010EF6 RID: 69366 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ColorScale(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010EF7 RID: 69367 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ColorScale(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010EF8 RID: 69368 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ColorScale(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010EF9 RID: 69369 RVA: 0x002E8C1D File Offset: 0x002E6E1D
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "cfvo" == name)
			{
				return new ConditionalFormattingValueObject();
			}
			if (53 == namespaceId && "color" == name)
			{
				return new Color();
			}
			return null;
		}

		// Token: 0x06010EFA RID: 69370 RVA: 0x002E8C50 File Offset: 0x002E6E50
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColorScale>(deep);
		}

		// Token: 0x040076F7 RID: 30455
		private const string tagName = "colorScale";

		// Token: 0x040076F8 RID: 30456
		private const byte tagNsId = 53;

		// Token: 0x040076F9 RID: 30457
		internal const int ElementTypeIdConst = 12960;
	}
}
