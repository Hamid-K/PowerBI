using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002730 RID: 10032
	[GeneratedCode("DomGen", "2.0")]
	internal class FillReference : StyleMatrixReferenceType
	{
		// Token: 0x17006010 RID: 24592
		// (get) Token: 0x0601349D RID: 79005 RVA: 0x00305CDE File Offset: 0x00303EDE
		public override string LocalName
		{
			get
			{
				return "fillRef";
			}
		}

		// Token: 0x17006011 RID: 24593
		// (get) Token: 0x0601349E RID: 79006 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006012 RID: 24594
		// (get) Token: 0x0601349F RID: 79007 RVA: 0x00305CE5 File Offset: 0x00303EE5
		internal override int ElementTypeId
		{
			get
			{
				return 10094;
			}
		}

		// Token: 0x060134A0 RID: 79008 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060134A1 RID: 79009 RVA: 0x00305CEC File Offset: 0x00303EEC
		public FillReference()
		{
		}

		// Token: 0x060134A2 RID: 79010 RVA: 0x00305CF4 File Offset: 0x00303EF4
		public FillReference(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060134A3 RID: 79011 RVA: 0x00305CFD File Offset: 0x00303EFD
		public FillReference(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060134A4 RID: 79012 RVA: 0x00305D06 File Offset: 0x00303F06
		public FillReference(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060134A5 RID: 79013 RVA: 0x00305D0F File Offset: 0x00303F0F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FillReference>(deep);
		}

		// Token: 0x04008578 RID: 34168
		private const string tagName = "fillRef";

		// Token: 0x04008579 RID: 34169
		private const byte tagNsId = 10;

		// Token: 0x0400857A RID: 34170
		internal const int ElementTypeIdConst = 10094;
	}
}
