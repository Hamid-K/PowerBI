using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E6C RID: 11884
	[GeneratedCode("DomGen", "2.0")]
	internal class TabChar : EmptyType
	{
		// Token: 0x17008A85 RID: 35461
		// (get) Token: 0x060193EB RID: 103403 RVA: 0x002D001B File Offset: 0x002CE21B
		public override string LocalName
		{
			get
			{
				return "tab";
			}
		}

		// Token: 0x17008A86 RID: 35462
		// (get) Token: 0x060193EC RID: 103404 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A87 RID: 35463
		// (get) Token: 0x060193ED RID: 103405 RVA: 0x00347B66 File Offset: 0x00345D66
		internal override int ElementTypeId
		{
			get
			{
				return 11564;
			}
		}

		// Token: 0x060193EE RID: 103406 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060193F0 RID: 103408 RVA: 0x00347B6D File Offset: 0x00345D6D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TabChar>(deep);
		}

		// Token: 0x0400A7DC RID: 42972
		private const string tagName = "tab";

		// Token: 0x0400A7DD RID: 42973
		private const byte tagNsId = 23;

		// Token: 0x0400A7DE RID: 42974
		internal const int ElementTypeIdConst = 11564;
	}
}
