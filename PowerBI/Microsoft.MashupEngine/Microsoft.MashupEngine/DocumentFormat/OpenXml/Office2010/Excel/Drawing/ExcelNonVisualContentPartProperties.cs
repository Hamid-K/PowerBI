using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel.Drawing
{
	// Token: 0x0200238C RID: 9100
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ExcelNonVisualContentPartProperties : ContentPartNonVisualType
	{
		// Token: 0x17004B9B RID: 19355
		// (get) Token: 0x0601071F RID: 67359 RVA: 0x002DFF48 File Offset: 0x002DE148
		public override string LocalName
		{
			get
			{
				return "nvContentPr";
			}
		}

		// Token: 0x17004B9C RID: 19356
		// (get) Token: 0x06010720 RID: 67360 RVA: 0x002E35B9 File Offset: 0x002E17B9
		internal override byte NamespaceId
		{
			get
			{
				return 54;
			}
		}

		// Token: 0x17004B9D RID: 19357
		// (get) Token: 0x06010721 RID: 67361 RVA: 0x002E3A94 File Offset: 0x002E1C94
		internal override int ElementTypeId
		{
			get
			{
				return 13014;
			}
		}

		// Token: 0x06010722 RID: 67362 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010723 RID: 67363 RVA: 0x002E3A9B File Offset: 0x002E1C9B
		public ExcelNonVisualContentPartProperties()
		{
		}

		// Token: 0x06010724 RID: 67364 RVA: 0x002E3AA3 File Offset: 0x002E1CA3
		public ExcelNonVisualContentPartProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010725 RID: 67365 RVA: 0x002E3AAC File Offset: 0x002E1CAC
		public ExcelNonVisualContentPartProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010726 RID: 67366 RVA: 0x002E3AB5 File Offset: 0x002E1CB5
		public ExcelNonVisualContentPartProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010727 RID: 67367 RVA: 0x002E3ABE File Offset: 0x002E1CBE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ExcelNonVisualContentPartProperties>(deep);
		}

		// Token: 0x040074A0 RID: 29856
		private const string tagName = "nvContentPr";

		// Token: 0x040074A1 RID: 29857
		private const byte tagNsId = 54;

		// Token: 0x040074A2 RID: 29858
		internal const int ElementTypeIdConst = 13014;
	}
}
