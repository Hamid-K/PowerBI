using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Ink
{
	// Token: 0x02002269 RID: 8809
	[GeneratedCode("DomGen", "2.0")]
	internal class SourceLink : ContextLinkType
	{
		// Token: 0x17003CD6 RID: 15574
		// (get) Token: 0x0600E79E RID: 59294 RVA: 0x002C8713 File Offset: 0x002C6913
		public override string LocalName
		{
			get
			{
				return "sourceLink";
			}
		}

		// Token: 0x17003CD7 RID: 15575
		// (get) Token: 0x0600E79F RID: 59295 RVA: 0x002C826A File Offset: 0x002C646A
		internal override byte NamespaceId
		{
			get
			{
				return 45;
			}
		}

		// Token: 0x17003CD8 RID: 15576
		// (get) Token: 0x0600E7A0 RID: 59296 RVA: 0x002C871A File Offset: 0x002C691A
		internal override int ElementTypeId
		{
			get
			{
				return 12689;
			}
		}

		// Token: 0x0600E7A1 RID: 59297 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600E7A3 RID: 59299 RVA: 0x002C8729 File Offset: 0x002C6929
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SourceLink>(deep);
		}

		// Token: 0x04006F57 RID: 28503
		private const string tagName = "sourceLink";

		// Token: 0x04006F58 RID: 28504
		private const byte tagNsId = 45;

		// Token: 0x04006F59 RID: 28505
		internal const int ElementTypeIdConst = 12689;
	}
}
