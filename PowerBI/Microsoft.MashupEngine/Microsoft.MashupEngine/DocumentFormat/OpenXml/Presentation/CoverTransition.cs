using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AD2 RID: 10962
	[GeneratedCode("DomGen", "2.0")]
	internal class CoverTransition : EightDirectionTransitionType
	{
		// Token: 0x1700756C RID: 30060
		// (get) Token: 0x06016574 RID: 91508 RVA: 0x0032922F File Offset: 0x0032742F
		public override string LocalName
		{
			get
			{
				return "cover";
			}
		}

		// Token: 0x1700756D RID: 30061
		// (get) Token: 0x06016575 RID: 91509 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700756E RID: 30062
		// (get) Token: 0x06016576 RID: 91510 RVA: 0x00329236 File Offset: 0x00327436
		internal override int ElementTypeId
		{
			get
			{
				return 12380;
			}
		}

		// Token: 0x06016577 RID: 91511 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016579 RID: 91513 RVA: 0x00329245 File Offset: 0x00327445
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CoverTransition>(deep);
		}

		// Token: 0x0400974F RID: 38735
		private const string tagName = "cover";

		// Token: 0x04009750 RID: 38736
		private const byte tagNsId = 24;

		// Token: 0x04009751 RID: 38737
		internal const int ElementTypeIdConst = 12380;
	}
}
