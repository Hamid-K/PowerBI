using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E02 RID: 11778
	[GeneratedCode("DomGen", "2.0")]
	internal class ForgetLastTabAlignment : OnOffType
	{
		// Token: 0x170088A2 RID: 34978
		// (get) Token: 0x06019004 RID: 102404 RVA: 0x0034573B File Offset: 0x0034393B
		public override string LocalName
		{
			get
			{
				return "forgetLastTabAlignment";
			}
		}

		// Token: 0x170088A3 RID: 34979
		// (get) Token: 0x06019005 RID: 102405 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170088A4 RID: 34980
		// (get) Token: 0x06019006 RID: 102406 RVA: 0x00345742 File Offset: 0x00343942
		internal override int ElementTypeId
		{
			get
			{
				return 12088;
			}
		}

		// Token: 0x06019007 RID: 102407 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019009 RID: 102409 RVA: 0x00345749 File Offset: 0x00343949
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ForgetLastTabAlignment>(deep);
		}

		// Token: 0x0400A671 RID: 42609
		private const string tagName = "forgetLastTabAlignment";

		// Token: 0x0400A672 RID: 42610
		private const byte tagNsId = 23;

		// Token: 0x0400A673 RID: 42611
		internal const int ElementTypeIdConst = 12088;
	}
}
