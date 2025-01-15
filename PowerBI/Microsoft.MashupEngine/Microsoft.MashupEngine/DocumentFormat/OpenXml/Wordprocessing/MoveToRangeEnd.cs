using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D46 RID: 11590
	[GeneratedCode("DomGen", "2.0")]
	internal class MoveToRangeEnd : MarkupRangeType
	{
		// Token: 0x17008668 RID: 34408
		// (get) Token: 0x06018B8C RID: 101260 RVA: 0x003444B5 File Offset: 0x003426B5
		public override string LocalName
		{
			get
			{
				return "moveToRangeEnd";
			}
		}

		// Token: 0x17008669 RID: 34409
		// (get) Token: 0x06018B8D RID: 101261 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700866A RID: 34410
		// (get) Token: 0x06018B8E RID: 101262 RVA: 0x003444BC File Offset: 0x003426BC
		internal override int ElementTypeId
		{
			get
			{
				return 11483;
			}
		}

		// Token: 0x06018B8F RID: 101263 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018B91 RID: 101265 RVA: 0x003444C3 File Offset: 0x003426C3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MoveToRangeEnd>(deep);
		}

		// Token: 0x0400A441 RID: 42049
		private const string tagName = "moveToRangeEnd";

		// Token: 0x0400A442 RID: 42050
		private const byte tagNsId = 23;

		// Token: 0x0400A443 RID: 42051
		internal const int ElementTypeIdConst = 11483;
	}
}
