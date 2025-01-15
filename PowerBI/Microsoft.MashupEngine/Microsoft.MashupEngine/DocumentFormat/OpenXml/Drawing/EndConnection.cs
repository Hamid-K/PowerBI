using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002795 RID: 10133
	[GeneratedCode("DomGen", "2.0")]
	internal class EndConnection : ConnectionType
	{
		// Token: 0x1700621A RID: 25114
		// (get) Token: 0x06013970 RID: 80240 RVA: 0x00308966 File Offset: 0x00306B66
		public override string LocalName
		{
			get
			{
				return "endCxn";
			}
		}

		// Token: 0x1700621B RID: 25115
		// (get) Token: 0x06013971 RID: 80241 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700621C RID: 25116
		// (get) Token: 0x06013972 RID: 80242 RVA: 0x0030896D File Offset: 0x00306B6D
		internal override int ElementTypeId
		{
			get
			{
				return 10169;
			}
		}

		// Token: 0x06013973 RID: 80243 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013975 RID: 80245 RVA: 0x00308974 File Offset: 0x00306B74
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EndConnection>(deep);
		}

		// Token: 0x040086E0 RID: 34528
		private const string tagName = "endCxn";

		// Token: 0x040086E1 RID: 34529
		private const byte tagNsId = 10;

		// Token: 0x040086E2 RID: 34530
		internal const int ElementTypeIdConst = 10169;
	}
}
