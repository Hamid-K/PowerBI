using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200277D RID: 10109
	[GeneratedCode("DomGen", "2.0")]
	internal class Light1Color : Color2Type
	{
		// Token: 0x170061AD RID: 25005
		// (get) Token: 0x0601386F RID: 79983 RVA: 0x00308246 File Offset: 0x00306446
		public override string LocalName
		{
			get
			{
				return "lt1";
			}
		}

		// Token: 0x170061AE RID: 25006
		// (get) Token: 0x06013870 RID: 79984 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170061AF RID: 25007
		// (get) Token: 0x06013871 RID: 79985 RVA: 0x0030824D File Offset: 0x0030644D
		internal override int ElementTypeId
		{
			get
			{
				return 10148;
			}
		}

		// Token: 0x06013872 RID: 79986 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013873 RID: 79987 RVA: 0x0030821A File Offset: 0x0030641A
		public Light1Color()
		{
		}

		// Token: 0x06013874 RID: 79988 RVA: 0x00308222 File Offset: 0x00306422
		public Light1Color(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013875 RID: 79989 RVA: 0x0030822B File Offset: 0x0030642B
		public Light1Color(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013876 RID: 79990 RVA: 0x00308234 File Offset: 0x00306434
		public Light1Color(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013877 RID: 79991 RVA: 0x00308254 File Offset: 0x00306454
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Light1Color>(deep);
		}

		// Token: 0x04008694 RID: 34452
		private const string tagName = "lt1";

		// Token: 0x04008695 RID: 34453
		private const byte tagNsId = 10;

		// Token: 0x04008696 RID: 34454
		internal const int ElementTypeIdConst = 10148;
	}
}
