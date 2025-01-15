using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EC6 RID: 11974
	[GeneratedCode("DomGen", "2.0")]
	internal class MoveToRun : RunTrackChangeType
	{
		// Token: 0x17008C7E RID: 35966
		// (get) Token: 0x06019823 RID: 104483 RVA: 0x0030BDF9 File Offset: 0x00309FF9
		public override string LocalName
		{
			get
			{
				return "moveTo";
			}
		}

		// Token: 0x17008C7F RID: 35967
		// (get) Token: 0x06019824 RID: 104484 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008C80 RID: 35968
		// (get) Token: 0x06019825 RID: 104485 RVA: 0x0034CDD7 File Offset: 0x0034AFD7
		internal override int ElementTypeId
		{
			get
			{
				return 11629;
			}
		}

		// Token: 0x06019826 RID: 104486 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019827 RID: 104487 RVA: 0x0034CD8B File Offset: 0x0034AF8B
		public MoveToRun()
		{
		}

		// Token: 0x06019828 RID: 104488 RVA: 0x0034CD93 File Offset: 0x0034AF93
		public MoveToRun(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019829 RID: 104489 RVA: 0x0034CD9C File Offset: 0x0034AF9C
		public MoveToRun(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601982A RID: 104490 RVA: 0x0034CDA5 File Offset: 0x0034AFA5
		public MoveToRun(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601982B RID: 104491 RVA: 0x0034CDDE File Offset: 0x0034AFDE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MoveToRun>(deep);
		}

		// Token: 0x0400A927 RID: 43303
		private const string tagName = "moveTo";

		// Token: 0x0400A928 RID: 43304
		private const byte tagNsId = 23;

		// Token: 0x0400A929 RID: 43305
		internal const int ElementTypeIdConst = 11629;
	}
}
