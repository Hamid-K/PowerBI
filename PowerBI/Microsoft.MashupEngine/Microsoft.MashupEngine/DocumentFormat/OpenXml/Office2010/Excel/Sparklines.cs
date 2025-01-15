using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x0200240B RID: 9227
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Sparkline), FileFormatVersions.Office2010)]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class Sparklines : OpenXmlCompositeElement
	{
		// Token: 0x17004EC9 RID: 20169
		// (get) Token: 0x06010E1B RID: 69147 RVA: 0x002E8509 File Offset: 0x002E6709
		public override string LocalName
		{
			get
			{
				return "sparklines";
			}
		}

		// Token: 0x17004ECA RID: 20170
		// (get) Token: 0x06010E1C RID: 69148 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004ECB RID: 20171
		// (get) Token: 0x06010E1D RID: 69149 RVA: 0x002E8510 File Offset: 0x002E6710
		internal override int ElementTypeId
		{
			get
			{
				return 12945;
			}
		}

		// Token: 0x06010E1E RID: 69150 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010E1F RID: 69151 RVA: 0x00293ECF File Offset: 0x002920CF
		public Sparklines()
		{
		}

		// Token: 0x06010E20 RID: 69152 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Sparklines(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010E21 RID: 69153 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Sparklines(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010E22 RID: 69154 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Sparklines(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010E23 RID: 69155 RVA: 0x002E8517 File Offset: 0x002E6717
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "sparkline" == name)
			{
				return new Sparkline();
			}
			return null;
		}

		// Token: 0x06010E24 RID: 69156 RVA: 0x002E8532 File Offset: 0x002E6732
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Sparklines>(deep);
		}

		// Token: 0x040076B0 RID: 30384
		private const string tagName = "sparklines";

		// Token: 0x040076B1 RID: 30385
		private const byte tagNsId = 53;

		// Token: 0x040076B2 RID: 30386
		internal const int ElementTypeIdConst = 12945;
	}
}
