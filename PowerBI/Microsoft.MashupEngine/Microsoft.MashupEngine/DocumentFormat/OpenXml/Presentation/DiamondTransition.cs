using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x020029E9 RID: 10729
	[GeneratedCode("DomGen", "2.0")]
	internal class DiamondTransition : EmptyType
	{
		// Token: 0x17006E54 RID: 28244
		// (get) Token: 0x06015597 RID: 87447 RVA: 0x0031E205 File Offset: 0x0031C405
		public override string LocalName
		{
			get
			{
				return "diamond";
			}
		}

		// Token: 0x17006E55 RID: 28245
		// (get) Token: 0x06015598 RID: 87448 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006E56 RID: 28246
		// (get) Token: 0x06015599 RID: 87449 RVA: 0x0031E20C File Offset: 0x0031C40C
		internal override int ElementTypeId
		{
			get
			{
				return 12382;
			}
		}

		// Token: 0x0601559A RID: 87450 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601559C RID: 87452 RVA: 0x0031E213 File Offset: 0x0031C413
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DiamondTransition>(deep);
		}

		// Token: 0x04009315 RID: 37653
		private const string tagName = "diamond";

		// Token: 0x04009316 RID: 37654
		private const byte tagNsId = 24;

		// Token: 0x04009317 RID: 37655
		internal const int ElementTypeIdConst = 12382;
	}
}
