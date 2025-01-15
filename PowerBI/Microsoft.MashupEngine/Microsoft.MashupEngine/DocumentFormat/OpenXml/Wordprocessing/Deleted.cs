using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D3C RID: 11580
	[GeneratedCode("DomGen", "2.0")]
	internal class Deleted : TrackChangeType
	{
		// Token: 0x1700863B RID: 34363
		// (get) Token: 0x06018B31 RID: 101169 RVA: 0x00344131 File Offset: 0x00342331
		public override string LocalName
		{
			get
			{
				return "del";
			}
		}

		// Token: 0x1700863C RID: 34364
		// (get) Token: 0x06018B32 RID: 101170 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700863D RID: 34365
		// (get) Token: 0x06018B33 RID: 101171 RVA: 0x00344138 File Offset: 0x00342338
		internal override int ElementTypeId
		{
			get
			{
				return 11687;
			}
		}

		// Token: 0x06018B34 RID: 101172 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018B36 RID: 101174 RVA: 0x0034413F File Offset: 0x0034233F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Deleted>(deep);
		}

		// Token: 0x0400A420 RID: 42016
		private const string tagName = "del";

		// Token: 0x0400A421 RID: 42017
		private const byte tagNsId = 23;

		// Token: 0x0400A422 RID: 42018
		internal const int ElementTypeIdConst = 11687;
	}
}
