using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EC4 RID: 11972
	[GeneratedCode("DomGen", "2.0")]
	internal class DeletedRun : RunTrackChangeType
	{
		// Token: 0x17008C78 RID: 35960
		// (get) Token: 0x06019811 RID: 104465 RVA: 0x00344131 File Offset: 0x00342331
		public override string LocalName
		{
			get
			{
				return "del";
			}
		}

		// Token: 0x17008C79 RID: 35961
		// (get) Token: 0x06019812 RID: 104466 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008C7A RID: 35962
		// (get) Token: 0x06019813 RID: 104467 RVA: 0x0034CDB7 File Offset: 0x0034AFB7
		internal override int ElementTypeId
		{
			get
			{
				return 11627;
			}
		}

		// Token: 0x06019814 RID: 104468 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019815 RID: 104469 RVA: 0x0034CD8B File Offset: 0x0034AF8B
		public DeletedRun()
		{
		}

		// Token: 0x06019816 RID: 104470 RVA: 0x0034CD93 File Offset: 0x0034AF93
		public DeletedRun(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019817 RID: 104471 RVA: 0x0034CD9C File Offset: 0x0034AF9C
		public DeletedRun(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019818 RID: 104472 RVA: 0x0034CDA5 File Offset: 0x0034AFA5
		public DeletedRun(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019819 RID: 104473 RVA: 0x0034CDBE File Offset: 0x0034AFBE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DeletedRun>(deep);
		}

		// Token: 0x0400A921 RID: 43297
		private const string tagName = "del";

		// Token: 0x0400A922 RID: 43298
		private const byte tagNsId = 23;

		// Token: 0x0400A923 RID: 43299
		internal const int ElementTypeIdConst = 11627;
	}
}
