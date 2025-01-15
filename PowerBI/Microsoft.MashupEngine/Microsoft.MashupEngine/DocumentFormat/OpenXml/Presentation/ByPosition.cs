using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002ABC RID: 10940
	[GeneratedCode("DomGen", "2.0")]
	internal class ByPosition : TimeListType
	{
		// Token: 0x1700750B RID: 29963
		// (get) Token: 0x0601649A RID: 91290 RVA: 0x0032321F File Offset: 0x0032141F
		public override string LocalName
		{
			get
			{
				return "by";
			}
		}

		// Token: 0x1700750C RID: 29964
		// (get) Token: 0x0601649B RID: 91291 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700750D RID: 29965
		// (get) Token: 0x0601649C RID: 91292 RVA: 0x003289CB File Offset: 0x00326BCB
		internal override int ElementTypeId
		{
			get
			{
				return 12355;
			}
		}

		// Token: 0x0601649D RID: 91293 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601649F RID: 91295 RVA: 0x003289DA File Offset: 0x00326BDA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ByPosition>(deep);
		}

		// Token: 0x04009705 RID: 38661
		private const string tagName = "by";

		// Token: 0x04009706 RID: 38662
		private const byte tagNsId = 24;

		// Token: 0x04009707 RID: 38663
		internal const int ElementTypeIdConst = 12355;
	}
}
