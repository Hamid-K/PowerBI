using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.ChartDrawing
{
	// Token: 0x02002641 RID: 9793
	[GeneratedCode("DomGen", "2.0")]
	internal class ToAnchor : MarkerType
	{
		// Token: 0x17005ADA RID: 23258
		// (get) Token: 0x06012913 RID: 76051 RVA: 0x002FCA83 File Offset: 0x002FAC83
		public override string LocalName
		{
			get
			{
				return "to";
			}
		}

		// Token: 0x17005ADB RID: 23259
		// (get) Token: 0x06012914 RID: 76052 RVA: 0x001422C0 File Offset: 0x001404C0
		internal override byte NamespaceId
		{
			get
			{
				return 12;
			}
		}

		// Token: 0x17005ADC RID: 23260
		// (get) Token: 0x06012915 RID: 76053 RVA: 0x002FCA8A File Offset: 0x002FAC8A
		internal override int ElementTypeId
		{
			get
			{
				return 10611;
			}
		}

		// Token: 0x06012916 RID: 76054 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012917 RID: 76055 RVA: 0x002FCA57 File Offset: 0x002FAC57
		public ToAnchor()
		{
		}

		// Token: 0x06012918 RID: 76056 RVA: 0x002FCA5F File Offset: 0x002FAC5F
		public ToAnchor(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012919 RID: 76057 RVA: 0x002FCA68 File Offset: 0x002FAC68
		public ToAnchor(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601291A RID: 76058 RVA: 0x002FCA71 File Offset: 0x002FAC71
		public ToAnchor(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601291B RID: 76059 RVA: 0x002FCA91 File Offset: 0x002FAC91
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ToAnchor>(deep);
		}

		// Token: 0x040080B2 RID: 32946
		private const string tagName = "to";

		// Token: 0x040080B3 RID: 32947
		private const byte tagNsId = 12;

		// Token: 0x040080B4 RID: 32948
		internal const int ElementTypeIdConst = 10611;
	}
}
