using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel.Drawing
{
	// Token: 0x0200238D RID: 9101
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ExcelNonVisualContentPartShapeProperties : ContentPartNonVisualType
	{
		// Token: 0x17004B9E RID: 19358
		// (get) Token: 0x06010728 RID: 67368 RVA: 0x002DFF82 File Offset: 0x002DE182
		public override string LocalName
		{
			get
			{
				return "nvContentPartPr";
			}
		}

		// Token: 0x17004B9F RID: 19359
		// (get) Token: 0x06010729 RID: 67369 RVA: 0x002E35B9 File Offset: 0x002E17B9
		internal override byte NamespaceId
		{
			get
			{
				return 54;
			}
		}

		// Token: 0x17004BA0 RID: 19360
		// (get) Token: 0x0601072A RID: 67370 RVA: 0x002E3AC7 File Offset: 0x002E1CC7
		internal override int ElementTypeId
		{
			get
			{
				return 13015;
			}
		}

		// Token: 0x0601072B RID: 67371 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0601072C RID: 67372 RVA: 0x002E3A9B File Offset: 0x002E1C9B
		public ExcelNonVisualContentPartShapeProperties()
		{
		}

		// Token: 0x0601072D RID: 67373 RVA: 0x002E3AA3 File Offset: 0x002E1CA3
		public ExcelNonVisualContentPartShapeProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601072E RID: 67374 RVA: 0x002E3AAC File Offset: 0x002E1CAC
		public ExcelNonVisualContentPartShapeProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601072F RID: 67375 RVA: 0x002E3AB5 File Offset: 0x002E1CB5
		public ExcelNonVisualContentPartShapeProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010730 RID: 67376 RVA: 0x002E3ACE File Offset: 0x002E1CCE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ExcelNonVisualContentPartShapeProperties>(deep);
		}

		// Token: 0x040074A3 RID: 29859
		private const string tagName = "nvContentPartPr";

		// Token: 0x040074A4 RID: 29860
		private const byte tagNsId = 54;

		// Token: 0x040074A5 RID: 29861
		internal const int ElementTypeIdConst = 13015;
	}
}
