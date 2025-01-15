using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002732 RID: 10034
	[GeneratedCode("DomGen", "2.0")]
	internal class LineReference : StyleMatrixReferenceType
	{
		// Token: 0x17006016 RID: 24598
		// (get) Token: 0x060134AF RID: 79023 RVA: 0x00305D2F File Offset: 0x00303F2F
		public override string LocalName
		{
			get
			{
				return "lnRef";
			}
		}

		// Token: 0x17006017 RID: 24599
		// (get) Token: 0x060134B0 RID: 79024 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006018 RID: 24600
		// (get) Token: 0x060134B1 RID: 79025 RVA: 0x00305D36 File Offset: 0x00303F36
		internal override int ElementTypeId
		{
			get
			{
				return 10238;
			}
		}

		// Token: 0x060134B2 RID: 79026 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060134B3 RID: 79027 RVA: 0x00305CEC File Offset: 0x00303EEC
		public LineReference()
		{
		}

		// Token: 0x060134B4 RID: 79028 RVA: 0x00305CF4 File Offset: 0x00303EF4
		public LineReference(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060134B5 RID: 79029 RVA: 0x00305CFD File Offset: 0x00303EFD
		public LineReference(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060134B6 RID: 79030 RVA: 0x00305D06 File Offset: 0x00303F06
		public LineReference(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060134B7 RID: 79031 RVA: 0x00305D3D File Offset: 0x00303F3D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LineReference>(deep);
		}

		// Token: 0x0400857E RID: 34174
		private const string tagName = "lnRef";

		// Token: 0x0400857F RID: 34175
		private const byte tagNsId = 10;

		// Token: 0x04008580 RID: 34176
		internal const int ElementTypeIdConst = 10238;
	}
}
