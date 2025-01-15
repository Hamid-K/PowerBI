using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002ABE RID: 10942
	[GeneratedCode("DomGen", "2.0")]
	internal class ToPosition : TimeListType
	{
		// Token: 0x17007511 RID: 29969
		// (get) Token: 0x060164A6 RID: 91302 RVA: 0x002FCA83 File Offset: 0x002FAC83
		public override string LocalName
		{
			get
			{
				return "to";
			}
		}

		// Token: 0x17007512 RID: 29970
		// (get) Token: 0x060164A7 RID: 91303 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007513 RID: 29971
		// (get) Token: 0x060164A8 RID: 91304 RVA: 0x003289F3 File Offset: 0x00326BF3
		internal override int ElementTypeId
		{
			get
			{
				return 12357;
			}
		}

		// Token: 0x060164A9 RID: 91305 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060164AB RID: 91307 RVA: 0x003289FA File Offset: 0x00326BFA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ToPosition>(deep);
		}

		// Token: 0x0400970B RID: 38667
		private const string tagName = "to";

		// Token: 0x0400970C RID: 38668
		private const byte tagNsId = 24;

		// Token: 0x0400970D RID: 38669
		internal const int ElementTypeIdConst = 12357;
	}
}
