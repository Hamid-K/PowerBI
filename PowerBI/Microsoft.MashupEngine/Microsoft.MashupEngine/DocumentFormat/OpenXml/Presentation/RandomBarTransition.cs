using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AD0 RID: 10960
	[GeneratedCode("DomGen", "2.0")]
	internal class RandomBarTransition : OrientationTransitionType
	{
		// Token: 0x17007566 RID: 30054
		// (get) Token: 0x06016567 RID: 91495 RVA: 0x003291D8 File Offset: 0x003273D8
		public override string LocalName
		{
			get
			{
				return "randomBar";
			}
		}

		// Token: 0x17007567 RID: 30055
		// (get) Token: 0x06016568 RID: 91496 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007568 RID: 30056
		// (get) Token: 0x06016569 RID: 91497 RVA: 0x003291DF File Offset: 0x003273DF
		internal override int ElementTypeId
		{
			get
			{
				return 12389;
			}
		}

		// Token: 0x0601656A RID: 91498 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601656C RID: 91500 RVA: 0x003291E6 File Offset: 0x003273E6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RandomBarTransition>(deep);
		}

		// Token: 0x0400974A RID: 38730
		private const string tagName = "randomBar";

		// Token: 0x0400974B RID: 38731
		private const byte tagNsId = 24;

		// Token: 0x0400974C RID: 38732
		internal const int ElementTypeIdConst = 12389;
	}
}
