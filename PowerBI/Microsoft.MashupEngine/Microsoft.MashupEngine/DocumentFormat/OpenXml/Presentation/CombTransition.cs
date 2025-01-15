using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002ACF RID: 10959
	[GeneratedCode("DomGen", "2.0")]
	internal class CombTransition : OrientationTransitionType
	{
		// Token: 0x17007563 RID: 30051
		// (get) Token: 0x06016561 RID: 91489 RVA: 0x003291C1 File Offset: 0x003273C1
		public override string LocalName
		{
			get
			{
				return "comb";
			}
		}

		// Token: 0x17007564 RID: 30052
		// (get) Token: 0x06016562 RID: 91490 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007565 RID: 30053
		// (get) Token: 0x06016563 RID: 91491 RVA: 0x003291C8 File Offset: 0x003273C8
		internal override int ElementTypeId
		{
			get
			{
				return 12379;
			}
		}

		// Token: 0x06016564 RID: 91492 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016566 RID: 91494 RVA: 0x003291CF File Offset: 0x003273CF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CombTransition>(deep);
		}

		// Token: 0x04009747 RID: 38727
		private const string tagName = "comb";

		// Token: 0x04009748 RID: 38728
		private const byte tagNsId = 24;

		// Token: 0x04009749 RID: 38729
		internal const int ElementTypeIdConst = 12379;
	}
}
