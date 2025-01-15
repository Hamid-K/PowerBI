using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002731 RID: 10033
	[GeneratedCode("DomGen", "2.0")]
	internal class EffectReference : StyleMatrixReferenceType
	{
		// Token: 0x17006013 RID: 24595
		// (get) Token: 0x060134A6 RID: 79014 RVA: 0x00305D18 File Offset: 0x00303F18
		public override string LocalName
		{
			get
			{
				return "effectRef";
			}
		}

		// Token: 0x17006014 RID: 24596
		// (get) Token: 0x060134A7 RID: 79015 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006015 RID: 24597
		// (get) Token: 0x060134A8 RID: 79016 RVA: 0x00305D1F File Offset: 0x00303F1F
		internal override int ElementTypeId
		{
			get
			{
				return 10096;
			}
		}

		// Token: 0x060134A9 RID: 79017 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060134AA RID: 79018 RVA: 0x00305CEC File Offset: 0x00303EEC
		public EffectReference()
		{
		}

		// Token: 0x060134AB RID: 79019 RVA: 0x00305CF4 File Offset: 0x00303EF4
		public EffectReference(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060134AC RID: 79020 RVA: 0x00305CFD File Offset: 0x00303EFD
		public EffectReference(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060134AD RID: 79021 RVA: 0x00305D06 File Offset: 0x00303F06
		public EffectReference(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060134AE RID: 79022 RVA: 0x00305D26 File Offset: 0x00303F26
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EffectReference>(deep);
		}

		// Token: 0x0400857B RID: 34171
		private const string tagName = "effectRef";

		// Token: 0x0400857C RID: 34172
		private const byte tagNsId = 10;

		// Token: 0x0400857D RID: 34173
		internal const int ElementTypeIdConst = 10096;
	}
}
