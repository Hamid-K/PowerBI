using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200251B RID: 9499
	[GeneratedCode("DomGen", "2.0")]
	internal class Bubble3D : BooleanType
	{
		// Token: 0x1700546A RID: 21610
		// (get) Token: 0x06011ACF RID: 72399 RVA: 0x002F1396 File Offset: 0x002EF596
		public override string LocalName
		{
			get
			{
				return "bubble3D";
			}
		}

		// Token: 0x1700546B RID: 21611
		// (get) Token: 0x06011AD0 RID: 72400 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700546C RID: 21612
		// (get) Token: 0x06011AD1 RID: 72401 RVA: 0x002F139D File Offset: 0x002EF59D
		internal override int ElementTypeId
		{
			get
			{
				return 10433;
			}
		}

		// Token: 0x06011AD2 RID: 72402 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011AD4 RID: 72404 RVA: 0x002F13A4 File Offset: 0x002EF5A4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Bubble3D>(deep);
		}

		// Token: 0x04007BE5 RID: 31717
		private const string tagName = "bubble3D";

		// Token: 0x04007BE6 RID: 31718
		private const byte tagNsId = 11;

		// Token: 0x04007BE7 RID: 31719
		internal const int ElementTypeIdConst = 10433;
	}
}
