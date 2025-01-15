using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EF1 RID: 12017
	[GeneratedCode("DomGen", "2.0")]
	internal class LinkedToFile : OnOffOnlyType
	{
		// Token: 0x17008D87 RID: 36231
		// (get) Token: 0x06019A5E RID: 105054 RVA: 0x00353909 File Offset: 0x00351B09
		public override string LocalName
		{
			get
			{
				return "linkedToFile";
			}
		}

		// Token: 0x17008D88 RID: 36232
		// (get) Token: 0x06019A5F RID: 105055 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008D89 RID: 36233
		// (get) Token: 0x06019A60 RID: 105056 RVA: 0x00353910 File Offset: 0x00351B10
		internal override int ElementTypeId
		{
			get
			{
				return 11855;
			}
		}

		// Token: 0x06019A61 RID: 105057 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019A63 RID: 105059 RVA: 0x00353917 File Offset: 0x00351B17
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LinkedToFile>(deep);
		}

		// Token: 0x0400A9E0 RID: 43488
		private const string tagName = "linkedToFile";

		// Token: 0x0400A9E1 RID: 43489
		private const byte tagNsId = 23;

		// Token: 0x0400A9E2 RID: 43490
		internal const int ElementTypeIdConst = 11855;
	}
}
