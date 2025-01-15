using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D5E RID: 11614
	[GeneratedCode("DomGen", "2.0")]
	internal class Query : StringType
	{
		// Token: 0x170086B6 RID: 34486
		// (get) Token: 0x06018C2B RID: 101419 RVA: 0x003303A7 File Offset: 0x0032E5A7
		public override string LocalName
		{
			get
			{
				return "query";
			}
		}

		// Token: 0x170086B7 RID: 34487
		// (get) Token: 0x06018C2C RID: 101420 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170086B8 RID: 34488
		// (get) Token: 0x06018C2D RID: 101421 RVA: 0x00344891 File Offset: 0x00342A91
		internal override int ElementTypeId
		{
			get
			{
				return 11816;
			}
		}

		// Token: 0x06018C2E RID: 101422 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018C30 RID: 101424 RVA: 0x00344898 File Offset: 0x00342A98
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Query>(deep);
		}

		// Token: 0x0400A486 RID: 42118
		private const string tagName = "query";

		// Token: 0x0400A487 RID: 42119
		private const byte tagNsId = 23;

		// Token: 0x0400A488 RID: 42120
		internal const int ElementTypeIdConst = 11816;
	}
}
