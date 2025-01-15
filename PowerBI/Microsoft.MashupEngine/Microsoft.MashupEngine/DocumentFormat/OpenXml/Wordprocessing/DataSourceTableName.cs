using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D5C RID: 11612
	[GeneratedCode("DomGen", "2.0")]
	internal class DataSourceTableName : StringType
	{
		// Token: 0x170086B0 RID: 34480
		// (get) Token: 0x06018C1F RID: 101407 RVA: 0x00049581 File Offset: 0x00047781
		public override string LocalName
		{
			get
			{
				return "table";
			}
		}

		// Token: 0x170086B1 RID: 34481
		// (get) Token: 0x06018C20 RID: 101408 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170086B2 RID: 34482
		// (get) Token: 0x06018C21 RID: 101409 RVA: 0x0034486A File Offset: 0x00342A6A
		internal override int ElementTypeId
		{
			get
			{
				return 11805;
			}
		}

		// Token: 0x06018C22 RID: 101410 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018C24 RID: 101412 RVA: 0x00344871 File Offset: 0x00342A71
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DataSourceTableName>(deep);
		}

		// Token: 0x0400A480 RID: 42112
		private const string tagName = "table";

		// Token: 0x0400A481 RID: 42113
		private const byte tagNsId = 23;

		// Token: 0x0400A482 RID: 42114
		internal const int ElementTypeIdConst = 11805;
	}
}
