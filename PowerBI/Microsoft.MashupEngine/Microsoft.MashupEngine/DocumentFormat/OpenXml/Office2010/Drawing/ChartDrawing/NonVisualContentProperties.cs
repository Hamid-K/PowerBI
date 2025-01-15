using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Drawing.ChartDrawing
{
	// Token: 0x0200233B RID: 9019
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class NonVisualContentProperties : ContentPartNonVisualType
	{
		// Token: 0x17004930 RID: 18736
		// (get) Token: 0x060101DF RID: 66015 RVA: 0x002DFF48 File Offset: 0x002DE148
		public override string LocalName
		{
			get
			{
				return "nvContentPr";
			}
		}

		// Token: 0x17004931 RID: 18737
		// (get) Token: 0x060101E0 RID: 66016 RVA: 0x002DF9A4 File Offset: 0x002DDBA4
		internal override byte NamespaceId
		{
			get
			{
				return 47;
			}
		}

		// Token: 0x17004932 RID: 18738
		// (get) Token: 0x060101E1 RID: 66017 RVA: 0x002DFF4F File Offset: 0x002DE14F
		internal override int ElementTypeId
		{
			get
			{
				return 12707;
			}
		}

		// Token: 0x060101E2 RID: 66018 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060101E3 RID: 66019 RVA: 0x002DFF56 File Offset: 0x002DE156
		public NonVisualContentProperties()
		{
		}

		// Token: 0x060101E4 RID: 66020 RVA: 0x002DFF5E File Offset: 0x002DE15E
		public NonVisualContentProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060101E5 RID: 66021 RVA: 0x002DFF67 File Offset: 0x002DE167
		public NonVisualContentProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060101E6 RID: 66022 RVA: 0x002DFF70 File Offset: 0x002DE170
		public NonVisualContentProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060101E7 RID: 66023 RVA: 0x002DFF79 File Offset: 0x002DE179
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualContentProperties>(deep);
		}

		// Token: 0x04007322 RID: 29474
		private const string tagName = "nvContentPr";

		// Token: 0x04007323 RID: 29475
		private const byte tagNsId = 47;

		// Token: 0x04007324 RID: 29476
		internal const int ElementTypeIdConst = 12707;
	}
}
