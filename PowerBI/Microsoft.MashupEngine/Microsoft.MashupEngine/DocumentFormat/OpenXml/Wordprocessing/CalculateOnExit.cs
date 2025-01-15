using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D95 RID: 11669
	[GeneratedCode("DomGen", "2.0")]
	internal class CalculateOnExit : OnOffType
	{
		// Token: 0x1700875B RID: 34651
		// (get) Token: 0x06018D76 RID: 101750 RVA: 0x00344D7E File Offset: 0x00342F7E
		public override string LocalName
		{
			get
			{
				return "calcOnExit";
			}
		}

		// Token: 0x1700875C RID: 34652
		// (get) Token: 0x06018D77 RID: 101751 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700875D RID: 34653
		// (get) Token: 0x06018D78 RID: 101752 RVA: 0x00344D85 File Offset: 0x00342F85
		internal override int ElementTypeId
		{
			get
			{
				return 11728;
			}
		}

		// Token: 0x06018D79 RID: 101753 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018D7B RID: 101755 RVA: 0x00344D8C File Offset: 0x00342F8C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CalculateOnExit>(deep);
		}

		// Token: 0x0400A52A RID: 42282
		private const string tagName = "calcOnExit";

		// Token: 0x0400A52B RID: 42283
		private const byte tagNsId = 23;

		// Token: 0x0400A52C RID: 42284
		internal const int ElementTypeIdConst = 11728;
	}
}
