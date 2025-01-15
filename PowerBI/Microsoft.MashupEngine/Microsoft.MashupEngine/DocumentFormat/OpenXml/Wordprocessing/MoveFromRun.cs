using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EC5 RID: 11973
	[GeneratedCode("DomGen", "2.0")]
	internal class MoveFromRun : RunTrackChangeType
	{
		// Token: 0x17008C7B RID: 35963
		// (get) Token: 0x0601981A RID: 104474 RVA: 0x00344148 File Offset: 0x00342348
		public override string LocalName
		{
			get
			{
				return "moveFrom";
			}
		}

		// Token: 0x17008C7C RID: 35964
		// (get) Token: 0x0601981B RID: 104475 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008C7D RID: 35965
		// (get) Token: 0x0601981C RID: 104476 RVA: 0x0034CDC7 File Offset: 0x0034AFC7
		internal override int ElementTypeId
		{
			get
			{
				return 11628;
			}
		}

		// Token: 0x0601981D RID: 104477 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601981E RID: 104478 RVA: 0x0034CD8B File Offset: 0x0034AF8B
		public MoveFromRun()
		{
		}

		// Token: 0x0601981F RID: 104479 RVA: 0x0034CD93 File Offset: 0x0034AF93
		public MoveFromRun(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019820 RID: 104480 RVA: 0x0034CD9C File Offset: 0x0034AF9C
		public MoveFromRun(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019821 RID: 104481 RVA: 0x0034CDA5 File Offset: 0x0034AFA5
		public MoveFromRun(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019822 RID: 104482 RVA: 0x0034CDCE File Offset: 0x0034AFCE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MoveFromRun>(deep);
		}

		// Token: 0x0400A924 RID: 43300
		private const string tagName = "moveFrom";

		// Token: 0x0400A925 RID: 43301
		private const byte tagNsId = 23;

		// Token: 0x0400A926 RID: 43302
		internal const int ElementTypeIdConst = 11628;
	}
}
