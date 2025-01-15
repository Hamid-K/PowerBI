using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DE6 RID: 11750
	[GeneratedCode("DomGen", "2.0")]
	internal class NoColumnBalance : OnOffType
	{
		// Token: 0x1700884E RID: 34894
		// (get) Token: 0x06018F5C RID: 102236 RVA: 0x003454B7 File Offset: 0x003436B7
		public override string LocalName
		{
			get
			{
				return "noColumnBalance";
			}
		}

		// Token: 0x1700884F RID: 34895
		// (get) Token: 0x06018F5D RID: 102237 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008850 RID: 34896
		// (get) Token: 0x06018F5E RID: 102238 RVA: 0x003454BE File Offset: 0x003436BE
		internal override int ElementTypeId
		{
			get
			{
				return 12060;
			}
		}

		// Token: 0x06018F5F RID: 102239 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018F61 RID: 102241 RVA: 0x003454C5 File Offset: 0x003436C5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NoColumnBalance>(deep);
		}

		// Token: 0x0400A61D RID: 42525
		private const string tagName = "noColumnBalance";

		// Token: 0x0400A61E RID: 42526
		private const byte tagNsId = 23;

		// Token: 0x0400A61F RID: 42527
		internal const int ElementTypeIdConst = 12060;
	}
}
