using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D3D RID: 11581
	[GeneratedCode("DomGen", "2.0")]
	internal class MoveFrom : TrackChangeType
	{
		// Token: 0x1700863E RID: 34366
		// (get) Token: 0x06018B37 RID: 101175 RVA: 0x00344148 File Offset: 0x00342348
		public override string LocalName
		{
			get
			{
				return "moveFrom";
			}
		}

		// Token: 0x1700863F RID: 34367
		// (get) Token: 0x06018B38 RID: 101176 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008640 RID: 34368
		// (get) Token: 0x06018B39 RID: 101177 RVA: 0x0034414F File Offset: 0x0034234F
		internal override int ElementTypeId
		{
			get
			{
				return 11688;
			}
		}

		// Token: 0x06018B3A RID: 101178 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018B3C RID: 101180 RVA: 0x00344156 File Offset: 0x00342356
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MoveFrom>(deep);
		}

		// Token: 0x0400A423 RID: 42019
		private const string tagName = "moveFrom";

		// Token: 0x0400A424 RID: 42020
		private const byte tagNsId = 23;

		// Token: 0x0400A425 RID: 42021
		internal const int ElementTypeIdConst = 11688;
	}
}
