using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x02002355 RID: 9045
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class NonVisualInkContentPartProperties : NonVisualInkContentPartPropertiesType
	{
		// Token: 0x17004A09 RID: 18953
		// (get) Token: 0x060103B6 RID: 66486 RVA: 0x002DFE49 File Offset: 0x002DE049
		public override string LocalName
		{
			get
			{
				return "cNvContentPartPr";
			}
		}

		// Token: 0x17004A0A RID: 18954
		// (get) Token: 0x060103B7 RID: 66487 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x17004A0B RID: 18955
		// (get) Token: 0x060103B8 RID: 66488 RVA: 0x002E1432 File Offset: 0x002DF632
		internal override int ElementTypeId
		{
			get
			{
				return 12729;
			}
		}

		// Token: 0x060103B9 RID: 66489 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060103BA RID: 66490 RVA: 0x002E1406 File Offset: 0x002DF606
		public NonVisualInkContentPartProperties()
		{
		}

		// Token: 0x060103BB RID: 66491 RVA: 0x002E140E File Offset: 0x002DF60E
		public NonVisualInkContentPartProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060103BC RID: 66492 RVA: 0x002E1417 File Offset: 0x002DF617
		public NonVisualInkContentPartProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060103BD RID: 66493 RVA: 0x002E1420 File Offset: 0x002DF620
		public NonVisualInkContentPartProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060103BE RID: 66494 RVA: 0x002E1439 File Offset: 0x002DF639
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualInkContentPartProperties>(deep);
		}

		// Token: 0x040073A3 RID: 29603
		private const string tagName = "cNvContentPartPr";

		// Token: 0x040073A4 RID: 29604
		private const byte tagNsId = 48;

		// Token: 0x040073A5 RID: 29605
		internal const int ElementTypeIdConst = 12729;
	}
}
