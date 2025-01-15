using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EF9 RID: 12025
	[GeneratedCode("DomGen", "2.0")]
	internal class Locked : OnOffOnlyType
	{
		// Token: 0x17008D9F RID: 36255
		// (get) Token: 0x06019A8E RID: 105102 RVA: 0x003539BA File Offset: 0x00351BBA
		public override string LocalName
		{
			get
			{
				return "locked";
			}
		}

		// Token: 0x17008DA0 RID: 36256
		// (get) Token: 0x06019A8F RID: 105103 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008DA1 RID: 36257
		// (get) Token: 0x06019A90 RID: 105104 RVA: 0x003539C1 File Offset: 0x00351BC1
		internal override int ElementTypeId
		{
			get
			{
				return 11903;
			}
		}

		// Token: 0x06019A91 RID: 105105 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019A93 RID: 105107 RVA: 0x003539C8 File Offset: 0x00351BC8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Locked>(deep);
		}

		// Token: 0x0400A9F8 RID: 43512
		private const string tagName = "locked";

		// Token: 0x0400A9F9 RID: 43513
		private const byte tagNsId = 23;

		// Token: 0x0400A9FA RID: 43514
		internal const int ElementTypeIdConst = 11903;
	}
}
