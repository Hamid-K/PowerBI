using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F84 RID: 12164
	[GeneratedCode("DomGen", "2.0")]
	internal class DivsChild : DivsType
	{
		// Token: 0x1700919B RID: 37275
		// (get) Token: 0x0601A36C RID: 107372 RVA: 0x0035F2F9 File Offset: 0x0035D4F9
		public override string LocalName
		{
			get
			{
				return "divsChild";
			}
		}

		// Token: 0x1700919C RID: 37276
		// (get) Token: 0x0601A36D RID: 107373 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700919D RID: 37277
		// (get) Token: 0x0601A36E RID: 107374 RVA: 0x0035F300 File Offset: 0x0035D500
		internal override int ElementTypeId
		{
			get
			{
				return 11934;
			}
		}

		// Token: 0x0601A36F RID: 107375 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A370 RID: 107376 RVA: 0x0035F2CD File Offset: 0x0035D4CD
		public DivsChild()
		{
		}

		// Token: 0x0601A371 RID: 107377 RVA: 0x0035F2D5 File Offset: 0x0035D4D5
		public DivsChild(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A372 RID: 107378 RVA: 0x0035F2DE File Offset: 0x0035D4DE
		public DivsChild(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A373 RID: 107379 RVA: 0x0035F2E7 File Offset: 0x0035D4E7
		public DivsChild(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A374 RID: 107380 RVA: 0x0035F307 File Offset: 0x0035D507
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DivsChild>(deep);
		}

		// Token: 0x0400AC37 RID: 44087
		private const string tagName = "divsChild";

		// Token: 0x0400AC38 RID: 44088
		private const byte tagNsId = 23;

		// Token: 0x0400AC39 RID: 44089
		internal const int ElementTypeIdConst = 11934;
	}
}
