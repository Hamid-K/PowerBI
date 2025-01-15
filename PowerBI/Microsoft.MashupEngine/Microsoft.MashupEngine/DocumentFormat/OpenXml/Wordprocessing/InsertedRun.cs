using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EC3 RID: 11971
	[GeneratedCode("DomGen", "2.0")]
	internal class InsertedRun : RunTrackChangeType
	{
		// Token: 0x17008C75 RID: 35957
		// (get) Token: 0x06019808 RID: 104456 RVA: 0x0034411A File Offset: 0x0034231A
		public override string LocalName
		{
			get
			{
				return "ins";
			}
		}

		// Token: 0x17008C76 RID: 35958
		// (get) Token: 0x06019809 RID: 104457 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008C77 RID: 35959
		// (get) Token: 0x0601980A RID: 104458 RVA: 0x0034CD84 File Offset: 0x0034AF84
		internal override int ElementTypeId
		{
			get
			{
				return 11626;
			}
		}

		// Token: 0x0601980B RID: 104459 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601980C RID: 104460 RVA: 0x0034CD8B File Offset: 0x0034AF8B
		public InsertedRun()
		{
		}

		// Token: 0x0601980D RID: 104461 RVA: 0x0034CD93 File Offset: 0x0034AF93
		public InsertedRun(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601980E RID: 104462 RVA: 0x0034CD9C File Offset: 0x0034AF9C
		public InsertedRun(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601980F RID: 104463 RVA: 0x0034CDA5 File Offset: 0x0034AFA5
		public InsertedRun(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019810 RID: 104464 RVA: 0x0034CDAE File Offset: 0x0034AFAE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<InsertedRun>(deep);
		}

		// Token: 0x0400A91E RID: 43294
		private const string tagName = "ins";

		// Token: 0x0400A91F RID: 43295
		private const byte tagNsId = 23;

		// Token: 0x0400A920 RID: 43296
		internal const int ElementTypeIdConst = 11626;
	}
}
