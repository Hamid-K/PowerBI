using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Drawing.ChartDrawing
{
	// Token: 0x02002339 RID: 9017
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class NonVisualInkContentPartProperties : NonVisualInkContentPartPropertiesType
	{
		// Token: 0x17004927 RID: 18727
		// (get) Token: 0x060101C7 RID: 65991 RVA: 0x002DFE49 File Offset: 0x002DE049
		public override string LocalName
		{
			get
			{
				return "cNvContentPartPr";
			}
		}

		// Token: 0x17004928 RID: 18728
		// (get) Token: 0x060101C8 RID: 65992 RVA: 0x002DF9A4 File Offset: 0x002DDBA4
		internal override byte NamespaceId
		{
			get
			{
				return 47;
			}
		}

		// Token: 0x17004929 RID: 18729
		// (get) Token: 0x060101C9 RID: 65993 RVA: 0x002DFE50 File Offset: 0x002DE050
		internal override int ElementTypeId
		{
			get
			{
				return 12706;
			}
		}

		// Token: 0x060101CA RID: 65994 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060101CB RID: 65995 RVA: 0x002DFE1D File Offset: 0x002DE01D
		public NonVisualInkContentPartProperties()
		{
		}

		// Token: 0x060101CC RID: 65996 RVA: 0x002DFE25 File Offset: 0x002DE025
		public NonVisualInkContentPartProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060101CD RID: 65997 RVA: 0x002DFE2E File Offset: 0x002DE02E
		public NonVisualInkContentPartProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060101CE RID: 65998 RVA: 0x002DFE37 File Offset: 0x002DE037
		public NonVisualInkContentPartProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060101CF RID: 65999 RVA: 0x002DFE57 File Offset: 0x002DE057
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualInkContentPartProperties>(deep);
		}

		// Token: 0x0400731D RID: 29469
		private const string tagName = "cNvContentPartPr";

		// Token: 0x0400731E RID: 29470
		private const byte tagNsId = 47;

		// Token: 0x0400731F RID: 29471
		internal const int ElementTypeIdConst = 12706;
	}
}
