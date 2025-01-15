using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x02002354 RID: 9044
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class NonVisualInkProperties : NonVisualInkContentPartPropertiesType
	{
		// Token: 0x17004A06 RID: 18950
		// (get) Token: 0x060103AD RID: 66477 RVA: 0x002DFE0F File Offset: 0x002DE00F
		public override string LocalName
		{
			get
			{
				return "cNvInkPr";
			}
		}

		// Token: 0x17004A07 RID: 18951
		// (get) Token: 0x060103AE RID: 66478 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x17004A08 RID: 18952
		// (get) Token: 0x060103AF RID: 66479 RVA: 0x002E13FF File Offset: 0x002DF5FF
		internal override int ElementTypeId
		{
			get
			{
				return 12728;
			}
		}

		// Token: 0x060103B0 RID: 66480 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060103B1 RID: 66481 RVA: 0x002E1406 File Offset: 0x002DF606
		public NonVisualInkProperties()
		{
		}

		// Token: 0x060103B2 RID: 66482 RVA: 0x002E140E File Offset: 0x002DF60E
		public NonVisualInkProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060103B3 RID: 66483 RVA: 0x002E1417 File Offset: 0x002DF617
		public NonVisualInkProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060103B4 RID: 66484 RVA: 0x002E1420 File Offset: 0x002DF620
		public NonVisualInkProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060103B5 RID: 66485 RVA: 0x002E1429 File Offset: 0x002DF629
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualInkProperties>(deep);
		}

		// Token: 0x040073A0 RID: 29600
		private const string tagName = "cNvInkPr";

		// Token: 0x040073A1 RID: 29601
		private const byte tagNsId = 48;

		// Token: 0x040073A2 RID: 29602
		internal const int ElementTypeIdConst = 12728;
	}
}
