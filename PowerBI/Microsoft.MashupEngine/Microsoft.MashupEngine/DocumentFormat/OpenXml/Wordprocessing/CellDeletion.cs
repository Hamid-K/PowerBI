using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D36 RID: 11574
	[GeneratedCode("DomGen", "2.0")]
	internal class CellDeletion : TrackChangeType
	{
		// Token: 0x17008629 RID: 34345
		// (get) Token: 0x06018B0D RID: 101133 RVA: 0x003440A7 File Offset: 0x003422A7
		public override string LocalName
		{
			get
			{
				return "cellDel";
			}
		}

		// Token: 0x1700862A RID: 34346
		// (get) Token: 0x06018B0E RID: 101134 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700862B RID: 34347
		// (get) Token: 0x06018B0F RID: 101135 RVA: 0x003440AE File Offset: 0x003422AE
		internal override int ElementTypeId
		{
			get
			{
				return 11474;
			}
		}

		// Token: 0x06018B10 RID: 101136 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018B12 RID: 101138 RVA: 0x003440B5 File Offset: 0x003422B5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CellDeletion>(deep);
		}

		// Token: 0x0400A40E RID: 41998
		private const string tagName = "cellDel";

		// Token: 0x0400A40F RID: 41999
		private const byte tagNsId = 23;

		// Token: 0x0400A410 RID: 42000
		internal const int ElementTypeIdConst = 11474;
	}
}
