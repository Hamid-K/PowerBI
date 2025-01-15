using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D9A RID: 11674
	[GeneratedCode("DomGen", "2.0")]
	internal class Dirty : OnOffType
	{
		// Token: 0x1700876A RID: 34666
		// (get) Token: 0x06018D94 RID: 101780 RVA: 0x00344DEA File Offset: 0x00342FEA
		public override string LocalName
		{
			get
			{
				return "dirty";
			}
		}

		// Token: 0x1700876B RID: 34667
		// (get) Token: 0x06018D95 RID: 101781 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700876C RID: 34668
		// (get) Token: 0x06018D96 RID: 101782 RVA: 0x00344DF1 File Offset: 0x00342FF1
		internal override int ElementTypeId
		{
			get
			{
				return 11757;
			}
		}

		// Token: 0x06018D97 RID: 101783 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018D99 RID: 101785 RVA: 0x00344DF8 File Offset: 0x00342FF8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Dirty>(deep);
		}

		// Token: 0x0400A539 RID: 42297
		private const string tagName = "dirty";

		// Token: 0x0400A53A RID: 42298
		private const byte tagNsId = 23;

		// Token: 0x0400A53B RID: 42299
		internal const int ElementTypeIdConst = 11757;
	}
}
