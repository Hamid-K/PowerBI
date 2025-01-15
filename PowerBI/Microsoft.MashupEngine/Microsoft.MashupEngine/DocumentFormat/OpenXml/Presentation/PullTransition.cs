using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AD3 RID: 10963
	[GeneratedCode("DomGen", "2.0")]
	internal class PullTransition : EightDirectionTransitionType
	{
		// Token: 0x1700756F RID: 30063
		// (get) Token: 0x0601657A RID: 91514 RVA: 0x0032924E File Offset: 0x0032744E
		public override string LocalName
		{
			get
			{
				return "pull";
			}
		}

		// Token: 0x17007570 RID: 30064
		// (get) Token: 0x0601657B RID: 91515 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007571 RID: 30065
		// (get) Token: 0x0601657C RID: 91516 RVA: 0x00329255 File Offset: 0x00327455
		internal override int ElementTypeId
		{
			get
			{
				return 12386;
			}
		}

		// Token: 0x0601657D RID: 91517 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601657F RID: 91519 RVA: 0x0032925C File Offset: 0x0032745C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PullTransition>(deep);
		}

		// Token: 0x04009752 RID: 38738
		private const string tagName = "pull";

		// Token: 0x04009753 RID: 38739
		private const byte tagNsId = 24;

		// Token: 0x04009754 RID: 38740
		internal const int ElementTypeIdConst = 12386;
	}
}
