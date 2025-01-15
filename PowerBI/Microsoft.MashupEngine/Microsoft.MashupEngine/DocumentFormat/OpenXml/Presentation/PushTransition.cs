using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AD8 RID: 10968
	[GeneratedCode("DomGen", "2.0")]
	internal class PushTransition : SideDirectionTransitionType
	{
		// Token: 0x1700757E RID: 30078
		// (get) Token: 0x0601659A RID: 91546 RVA: 0x0032932F File Offset: 0x0032752F
		public override string LocalName
		{
			get
			{
				return "push";
			}
		}

		// Token: 0x1700757F RID: 30079
		// (get) Token: 0x0601659B RID: 91547 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007580 RID: 30080
		// (get) Token: 0x0601659C RID: 91548 RVA: 0x00329336 File Offset: 0x00327536
		internal override int ElementTypeId
		{
			get
			{
				return 12387;
			}
		}

		// Token: 0x0601659D RID: 91549 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601659F RID: 91551 RVA: 0x00329345 File Offset: 0x00327545
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PushTransition>(deep);
		}

		// Token: 0x0400975F RID: 38751
		private const string tagName = "push";

		// Token: 0x04009760 RID: 38752
		private const byte tagNsId = 24;

		// Token: 0x04009761 RID: 38753
		internal const int ElementTypeIdConst = 12387;
	}
}
