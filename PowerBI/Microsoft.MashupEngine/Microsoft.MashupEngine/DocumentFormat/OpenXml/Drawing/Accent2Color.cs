using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002781 RID: 10113
	[GeneratedCode("DomGen", "2.0")]
	internal class Accent2Color : Color2Type
	{
		// Token: 0x170061B9 RID: 25017
		// (get) Token: 0x06013893 RID: 80019 RVA: 0x003082A2 File Offset: 0x003064A2
		public override string LocalName
		{
			get
			{
				return "accent2";
			}
		}

		// Token: 0x170061BA RID: 25018
		// (get) Token: 0x06013894 RID: 80020 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170061BB RID: 25019
		// (get) Token: 0x06013895 RID: 80021 RVA: 0x003082A9 File Offset: 0x003064A9
		internal override int ElementTypeId
		{
			get
			{
				return 10152;
			}
		}

		// Token: 0x06013896 RID: 80022 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013897 RID: 80023 RVA: 0x0030821A File Offset: 0x0030641A
		public Accent2Color()
		{
		}

		// Token: 0x06013898 RID: 80024 RVA: 0x00308222 File Offset: 0x00306422
		public Accent2Color(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013899 RID: 80025 RVA: 0x0030822B File Offset: 0x0030642B
		public Accent2Color(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601389A RID: 80026 RVA: 0x00308234 File Offset: 0x00306434
		public Accent2Color(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601389B RID: 80027 RVA: 0x003082B0 File Offset: 0x003064B0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Accent2Color>(deep);
		}

		// Token: 0x040086A0 RID: 34464
		private const string tagName = "accent2";

		// Token: 0x040086A1 RID: 34465
		private const byte tagNsId = 10;

		// Token: 0x040086A2 RID: 34466
		internal const int ElementTypeIdConst = 10152;
	}
}
