using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Drawing.ChartDrawing
{
	// Token: 0x02002338 RID: 9016
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class NonVisualInkProperties : NonVisualInkContentPartPropertiesType
	{
		// Token: 0x17004924 RID: 18724
		// (get) Token: 0x060101BE RID: 65982 RVA: 0x002DFE0F File Offset: 0x002DE00F
		public override string LocalName
		{
			get
			{
				return "cNvInkPr";
			}
		}

		// Token: 0x17004925 RID: 18725
		// (get) Token: 0x060101BF RID: 65983 RVA: 0x002DF9A4 File Offset: 0x002DDBA4
		internal override byte NamespaceId
		{
			get
			{
				return 47;
			}
		}

		// Token: 0x17004926 RID: 18726
		// (get) Token: 0x060101C0 RID: 65984 RVA: 0x002DFE16 File Offset: 0x002DE016
		internal override int ElementTypeId
		{
			get
			{
				return 12705;
			}
		}

		// Token: 0x060101C1 RID: 65985 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060101C2 RID: 65986 RVA: 0x002DFE1D File Offset: 0x002DE01D
		public NonVisualInkProperties()
		{
		}

		// Token: 0x060101C3 RID: 65987 RVA: 0x002DFE25 File Offset: 0x002DE025
		public NonVisualInkProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060101C4 RID: 65988 RVA: 0x002DFE2E File Offset: 0x002DE02E
		public NonVisualInkProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060101C5 RID: 65989 RVA: 0x002DFE37 File Offset: 0x002DE037
		public NonVisualInkProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060101C6 RID: 65990 RVA: 0x002DFE40 File Offset: 0x002DE040
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualInkProperties>(deep);
		}

		// Token: 0x0400731A RID: 29466
		private const string tagName = "cNvInkPr";

		// Token: 0x0400731B RID: 29467
		private const byte tagNsId = 47;

		// Token: 0x0400731C RID: 29468
		internal const int ElementTypeIdConst = 12705;
	}
}
