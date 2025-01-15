using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DA3 RID: 11683
	[GeneratedCode("DomGen", "2.0")]
	internal class RelyOnVML : OnOffType
	{
		// Token: 0x17008785 RID: 34693
		// (get) Token: 0x06018DCA RID: 101834 RVA: 0x00344EB2 File Offset: 0x003430B2
		public override string LocalName
		{
			get
			{
				return "relyOnVML";
			}
		}

		// Token: 0x17008786 RID: 34694
		// (get) Token: 0x06018DCB RID: 101835 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008787 RID: 34695
		// (get) Token: 0x06018DCC RID: 101836 RVA: 0x00344EB9 File Offset: 0x003430B9
		internal override int ElementTypeId
		{
			get
			{
				return 11839;
			}
		}

		// Token: 0x06018DCD RID: 101837 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018DCF RID: 101839 RVA: 0x00344EC0 File Offset: 0x003430C0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RelyOnVML>(deep);
		}

		// Token: 0x0400A554 RID: 42324
		private const string tagName = "relyOnVML";

		// Token: 0x0400A555 RID: 42325
		private const byte tagNsId = 23;

		// Token: 0x0400A556 RID: 42326
		internal const int ElementTypeIdConst = 11839;
	}
}
