using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200277F RID: 10111
	[GeneratedCode("DomGen", "2.0")]
	internal class Light2Color : Color2Type
	{
		// Token: 0x170061B3 RID: 25011
		// (get) Token: 0x06013881 RID: 80001 RVA: 0x00308274 File Offset: 0x00306474
		public override string LocalName
		{
			get
			{
				return "lt2";
			}
		}

		// Token: 0x170061B4 RID: 25012
		// (get) Token: 0x06013882 RID: 80002 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170061B5 RID: 25013
		// (get) Token: 0x06013883 RID: 80003 RVA: 0x0030827B File Offset: 0x0030647B
		internal override int ElementTypeId
		{
			get
			{
				return 10150;
			}
		}

		// Token: 0x06013884 RID: 80004 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013885 RID: 80005 RVA: 0x0030821A File Offset: 0x0030641A
		public Light2Color()
		{
		}

		// Token: 0x06013886 RID: 80006 RVA: 0x00308222 File Offset: 0x00306422
		public Light2Color(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013887 RID: 80007 RVA: 0x0030822B File Offset: 0x0030642B
		public Light2Color(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013888 RID: 80008 RVA: 0x00308234 File Offset: 0x00306434
		public Light2Color(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013889 RID: 80009 RVA: 0x00308282 File Offset: 0x00306482
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Light2Color>(deep);
		}

		// Token: 0x0400869A RID: 34458
		private const string tagName = "lt2";

		// Token: 0x0400869B RID: 34459
		private const byte tagNsId = 10;

		// Token: 0x0400869C RID: 34460
		internal const int ElementTypeIdConst = 10150;
	}
}
