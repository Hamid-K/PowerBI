using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002785 RID: 10117
	[GeneratedCode("DomGen", "2.0")]
	internal class Accent6Color : Color2Type
	{
		// Token: 0x170061C5 RID: 25029
		// (get) Token: 0x060138B7 RID: 80055 RVA: 0x003082FE File Offset: 0x003064FE
		public override string LocalName
		{
			get
			{
				return "accent6";
			}
		}

		// Token: 0x170061C6 RID: 25030
		// (get) Token: 0x060138B8 RID: 80056 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170061C7 RID: 25031
		// (get) Token: 0x060138B9 RID: 80057 RVA: 0x00308305 File Offset: 0x00306505
		internal override int ElementTypeId
		{
			get
			{
				return 10156;
			}
		}

		// Token: 0x060138BA RID: 80058 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060138BB RID: 80059 RVA: 0x0030821A File Offset: 0x0030641A
		public Accent6Color()
		{
		}

		// Token: 0x060138BC RID: 80060 RVA: 0x00308222 File Offset: 0x00306422
		public Accent6Color(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060138BD RID: 80061 RVA: 0x0030822B File Offset: 0x0030642B
		public Accent6Color(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060138BE RID: 80062 RVA: 0x00308234 File Offset: 0x00306434
		public Accent6Color(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060138BF RID: 80063 RVA: 0x0030830C File Offset: 0x0030650C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Accent6Color>(deep);
		}

		// Token: 0x040086AC RID: 34476
		private const string tagName = "accent6";

		// Token: 0x040086AD RID: 34477
		private const byte tagNsId = 10;

		// Token: 0x040086AE RID: 34478
		internal const int ElementTypeIdConst = 10156;
	}
}
