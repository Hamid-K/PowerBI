using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EFA RID: 12026
	[GeneratedCode("DomGen", "2.0")]
	internal class Personal : OnOffOnlyType
	{
		// Token: 0x17008DA2 RID: 36258
		// (get) Token: 0x06019A94 RID: 105108 RVA: 0x003539D1 File Offset: 0x00351BD1
		public override string LocalName
		{
			get
			{
				return "personal";
			}
		}

		// Token: 0x17008DA3 RID: 36259
		// (get) Token: 0x06019A95 RID: 105109 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008DA4 RID: 36260
		// (get) Token: 0x06019A96 RID: 105110 RVA: 0x003539D8 File Offset: 0x00351BD8
		internal override int ElementTypeId
		{
			get
			{
				return 11904;
			}
		}

		// Token: 0x06019A97 RID: 105111 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019A99 RID: 105113 RVA: 0x003539DF File Offset: 0x00351BDF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Personal>(deep);
		}

		// Token: 0x0400A9FB RID: 43515
		private const string tagName = "personal";

		// Token: 0x0400A9FC RID: 43516
		private const byte tagNsId = 23;

		// Token: 0x0400A9FD RID: 43517
		internal const int ElementTypeIdConst = 11904;
	}
}
