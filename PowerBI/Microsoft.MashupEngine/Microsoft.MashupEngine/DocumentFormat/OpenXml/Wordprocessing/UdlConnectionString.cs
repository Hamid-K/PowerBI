using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D5B RID: 11611
	[GeneratedCode("DomGen", "2.0")]
	internal class UdlConnectionString : StringType
	{
		// Token: 0x170086AD RID: 34477
		// (get) Token: 0x06018C19 RID: 101401 RVA: 0x00344853 File Offset: 0x00342A53
		public override string LocalName
		{
			get
			{
				return "udl";
			}
		}

		// Token: 0x170086AE RID: 34478
		// (get) Token: 0x06018C1A RID: 101402 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170086AF RID: 34479
		// (get) Token: 0x06018C1B RID: 101403 RVA: 0x0034485A File Offset: 0x00342A5A
		internal override int ElementTypeId
		{
			get
			{
				return 11804;
			}
		}

		// Token: 0x06018C1C RID: 101404 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018C1E RID: 101406 RVA: 0x00344861 File Offset: 0x00342A61
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<UdlConnectionString>(deep);
		}

		// Token: 0x0400A47D RID: 42109
		private const string tagName = "udl";

		// Token: 0x0400A47E RID: 42110
		private const byte tagNsId = 23;

		// Token: 0x0400A47F RID: 42111
		internal const int ElementTypeIdConst = 11804;
	}
}
