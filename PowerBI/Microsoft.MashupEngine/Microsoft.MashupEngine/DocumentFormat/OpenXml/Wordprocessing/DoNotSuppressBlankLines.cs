using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DA0 RID: 11680
	[GeneratedCode("DomGen", "2.0")]
	internal class DoNotSuppressBlankLines : OnOffType
	{
		// Token: 0x1700877C RID: 34684
		// (get) Token: 0x06018DB8 RID: 101816 RVA: 0x00344E6D File Offset: 0x0034306D
		public override string LocalName
		{
			get
			{
				return "doNotSuppressBlankLines";
			}
		}

		// Token: 0x1700877D RID: 34685
		// (get) Token: 0x06018DB9 RID: 101817 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700877E RID: 34686
		// (get) Token: 0x06018DBA RID: 101818 RVA: 0x00344E74 File Offset: 0x00343074
		internal override int ElementTypeId
		{
			get
			{
				return 11819;
			}
		}

		// Token: 0x06018DBB RID: 101819 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018DBD RID: 101821 RVA: 0x00344E7B File Offset: 0x0034307B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DoNotSuppressBlankLines>(deep);
		}

		// Token: 0x0400A54B RID: 42315
		private const string tagName = "doNotSuppressBlankLines";

		// Token: 0x0400A54C RID: 42316
		private const byte tagNsId = 23;

		// Token: 0x0400A54D RID: 42317
		internal const int ElementTypeIdConst = 11819;
	}
}
