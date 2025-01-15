using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025BF RID: 9663
	[GeneratedCode("DomGen", "2.0")]
	internal class MinorUnit : AxisUnitType
	{
		// Token: 0x17005779 RID: 22393
		// (get) Token: 0x060121A1 RID: 74145 RVA: 0x002F5882 File Offset: 0x002F3A82
		public override string LocalName
		{
			get
			{
				return "minorUnit";
			}
		}

		// Token: 0x1700577A RID: 22394
		// (get) Token: 0x060121A2 RID: 74146 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700577B RID: 22395
		// (get) Token: 0x060121A3 RID: 74147 RVA: 0x002F5889 File Offset: 0x002F3A89
		internal override int ElementTypeId
		{
			get
			{
				return 10489;
			}
		}

		// Token: 0x060121A4 RID: 74148 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060121A6 RID: 74150 RVA: 0x002F5890 File Offset: 0x002F3A90
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MinorUnit>(deep);
		}

		// Token: 0x04007E3C RID: 32316
		private const string tagName = "minorUnit";

		// Token: 0x04007E3D RID: 32317
		private const byte tagNsId = 11;

		// Token: 0x04007E3E RID: 32318
		internal const int ElementTypeIdConst = 10489;
	}
}
