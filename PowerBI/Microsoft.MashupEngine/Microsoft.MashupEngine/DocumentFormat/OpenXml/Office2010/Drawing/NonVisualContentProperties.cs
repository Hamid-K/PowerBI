using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x02002357 RID: 9047
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class NonVisualContentProperties : GvmlContentPartNonVisualType
	{
		// Token: 0x17004A12 RID: 18962
		// (get) Token: 0x060103CE RID: 66510 RVA: 0x002DFF48 File Offset: 0x002DE148
		public override string LocalName
		{
			get
			{
				return "nvContentPr";
			}
		}

		// Token: 0x17004A13 RID: 18963
		// (get) Token: 0x060103CF RID: 66511 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x17004A14 RID: 18964
		// (get) Token: 0x060103D0 RID: 66512 RVA: 0x002E152C File Offset: 0x002DF72C
		internal override int ElementTypeId
		{
			get
			{
				return 12730;
			}
		}

		// Token: 0x060103D1 RID: 66513 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060103D2 RID: 66514 RVA: 0x002E1533 File Offset: 0x002DF733
		public NonVisualContentProperties()
		{
		}

		// Token: 0x060103D3 RID: 66515 RVA: 0x002E153B File Offset: 0x002DF73B
		public NonVisualContentProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060103D4 RID: 66516 RVA: 0x002E1544 File Offset: 0x002DF744
		public NonVisualContentProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060103D5 RID: 66517 RVA: 0x002E154D File Offset: 0x002DF74D
		public NonVisualContentProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060103D6 RID: 66518 RVA: 0x002E1556 File Offset: 0x002DF756
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualContentProperties>(deep);
		}

		// Token: 0x040073A8 RID: 29608
		private const string tagName = "nvContentPr";

		// Token: 0x040073A9 RID: 29609
		private const byte tagNsId = 48;

		// Token: 0x040073AA RID: 29610
		internal const int ElementTypeIdConst = 12730;
	}
}
