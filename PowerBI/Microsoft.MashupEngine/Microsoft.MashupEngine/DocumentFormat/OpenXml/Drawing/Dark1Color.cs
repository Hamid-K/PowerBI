using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200277C RID: 10108
	[GeneratedCode("DomGen", "2.0")]
	internal class Dark1Color : Color2Type
	{
		// Token: 0x170061AA RID: 25002
		// (get) Token: 0x06013866 RID: 79974 RVA: 0x0030820C File Offset: 0x0030640C
		public override string LocalName
		{
			get
			{
				return "dk1";
			}
		}

		// Token: 0x170061AB RID: 25003
		// (get) Token: 0x06013867 RID: 79975 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170061AC RID: 25004
		// (get) Token: 0x06013868 RID: 79976 RVA: 0x00308213 File Offset: 0x00306413
		internal override int ElementTypeId
		{
			get
			{
				return 10147;
			}
		}

		// Token: 0x06013869 RID: 79977 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601386A RID: 79978 RVA: 0x0030821A File Offset: 0x0030641A
		public Dark1Color()
		{
		}

		// Token: 0x0601386B RID: 79979 RVA: 0x00308222 File Offset: 0x00306422
		public Dark1Color(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601386C RID: 79980 RVA: 0x0030822B File Offset: 0x0030642B
		public Dark1Color(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601386D RID: 79981 RVA: 0x00308234 File Offset: 0x00306434
		public Dark1Color(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601386E RID: 79982 RVA: 0x0030823D File Offset: 0x0030643D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Dark1Color>(deep);
		}

		// Token: 0x04008691 RID: 34449
		private const string tagName = "dk1";

		// Token: 0x04008692 RID: 34450
		private const byte tagNsId = 10;

		// Token: 0x04008693 RID: 34451
		internal const int ElementTypeIdConst = 10147;
	}
}
