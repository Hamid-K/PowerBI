using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002782 RID: 10114
	[GeneratedCode("DomGen", "2.0")]
	internal class Accent3Color : Color2Type
	{
		// Token: 0x170061BC RID: 25020
		// (get) Token: 0x0601389C RID: 80028 RVA: 0x003082B9 File Offset: 0x003064B9
		public override string LocalName
		{
			get
			{
				return "accent3";
			}
		}

		// Token: 0x170061BD RID: 25021
		// (get) Token: 0x0601389D RID: 80029 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170061BE RID: 25022
		// (get) Token: 0x0601389E RID: 80030 RVA: 0x003082C0 File Offset: 0x003064C0
		internal override int ElementTypeId
		{
			get
			{
				return 10153;
			}
		}

		// Token: 0x0601389F RID: 80031 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060138A0 RID: 80032 RVA: 0x0030821A File Offset: 0x0030641A
		public Accent3Color()
		{
		}

		// Token: 0x060138A1 RID: 80033 RVA: 0x00308222 File Offset: 0x00306422
		public Accent3Color(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060138A2 RID: 80034 RVA: 0x0030822B File Offset: 0x0030642B
		public Accent3Color(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060138A3 RID: 80035 RVA: 0x00308234 File Offset: 0x00306434
		public Accent3Color(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060138A4 RID: 80036 RVA: 0x003082C7 File Offset: 0x003064C7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Accent3Color>(deep);
		}

		// Token: 0x040086A3 RID: 34467
		private const string tagName = "accent3";

		// Token: 0x040086A4 RID: 34468
		private const byte tagNsId = 10;

		// Token: 0x040086A5 RID: 34469
		internal const int ElementTypeIdConst = 10153;
	}
}
