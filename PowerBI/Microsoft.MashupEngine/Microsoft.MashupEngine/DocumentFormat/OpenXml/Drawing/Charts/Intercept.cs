using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002562 RID: 9570
	[GeneratedCode("DomGen", "2.0")]
	internal class Intercept : DoubleType
	{
		// Token: 0x170055A2 RID: 21922
		// (get) Token: 0x06011D79 RID: 73081 RVA: 0x002F2F71 File Offset: 0x002F1171
		public override string LocalName
		{
			get
			{
				return "intercept";
			}
		}

		// Token: 0x170055A3 RID: 21923
		// (get) Token: 0x06011D7A RID: 73082 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170055A4 RID: 21924
		// (get) Token: 0x06011D7B RID: 73083 RVA: 0x002F2F78 File Offset: 0x002F1178
		internal override int ElementTypeId
		{
			get
			{
				return 10442;
			}
		}

		// Token: 0x06011D7C RID: 73084 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011D7E RID: 73086 RVA: 0x002F2F7F File Offset: 0x002F117F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Intercept>(deep);
		}

		// Token: 0x04007CD6 RID: 31958
		private const string tagName = "intercept";

		// Token: 0x04007CD7 RID: 31959
		private const byte tagNsId = 11;

		// Token: 0x04007CD8 RID: 31960
		internal const int ElementTypeIdConst = 10442;
	}
}
