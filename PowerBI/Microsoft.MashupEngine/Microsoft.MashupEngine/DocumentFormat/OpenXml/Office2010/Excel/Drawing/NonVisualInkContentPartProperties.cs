using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel.Drawing
{
	// Token: 0x0200238A RID: 9098
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class NonVisualInkContentPartProperties : NonVisualInkContentPartPropertiesType
	{
		// Token: 0x17004B92 RID: 19346
		// (get) Token: 0x06010707 RID: 67335 RVA: 0x002DFE49 File Offset: 0x002DE049
		public override string LocalName
		{
			get
			{
				return "cNvContentPartPr";
			}
		}

		// Token: 0x17004B93 RID: 19347
		// (get) Token: 0x06010708 RID: 67336 RVA: 0x002E35B9 File Offset: 0x002E17B9
		internal override byte NamespaceId
		{
			get
			{
				return 54;
			}
		}

		// Token: 0x17004B94 RID: 19348
		// (get) Token: 0x06010709 RID: 67337 RVA: 0x002E399A File Offset: 0x002E1B9A
		internal override int ElementTypeId
		{
			get
			{
				return 13013;
			}
		}

		// Token: 0x0601070A RID: 67338 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0601070B RID: 67339 RVA: 0x002E396E File Offset: 0x002E1B6E
		public NonVisualInkContentPartProperties()
		{
		}

		// Token: 0x0601070C RID: 67340 RVA: 0x002E3976 File Offset: 0x002E1B76
		public NonVisualInkContentPartProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601070D RID: 67341 RVA: 0x002E397F File Offset: 0x002E1B7F
		public NonVisualInkContentPartProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601070E RID: 67342 RVA: 0x002E3988 File Offset: 0x002E1B88
		public NonVisualInkContentPartProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601070F RID: 67343 RVA: 0x002E39A1 File Offset: 0x002E1BA1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualInkContentPartProperties>(deep);
		}

		// Token: 0x0400749B RID: 29851
		private const string tagName = "cNvContentPartPr";

		// Token: 0x0400749C RID: 29852
		private const byte tagNsId = 54;

		// Token: 0x0400749D RID: 29853
		internal const int ElementTypeIdConst = 13013;
	}
}
