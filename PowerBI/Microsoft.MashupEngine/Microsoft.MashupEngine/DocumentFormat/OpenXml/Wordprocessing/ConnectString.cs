using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D5D RID: 11613
	[GeneratedCode("DomGen", "2.0")]
	internal class ConnectString : StringType
	{
		// Token: 0x170086B3 RID: 34483
		// (get) Token: 0x06018C25 RID: 101413 RVA: 0x0034487A File Offset: 0x00342A7A
		public override string LocalName
		{
			get
			{
				return "connectString";
			}
		}

		// Token: 0x170086B4 RID: 34484
		// (get) Token: 0x06018C26 RID: 101414 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170086B5 RID: 34485
		// (get) Token: 0x06018C27 RID: 101415 RVA: 0x00344881 File Offset: 0x00342A81
		internal override int ElementTypeId
		{
			get
			{
				return 11815;
			}
		}

		// Token: 0x06018C28 RID: 101416 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018C2A RID: 101418 RVA: 0x00344888 File Offset: 0x00342A88
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ConnectString>(deep);
		}

		// Token: 0x0400A483 RID: 42115
		private const string tagName = "connectString";

		// Token: 0x0400A484 RID: 42116
		private const byte tagNsId = 23;

		// Token: 0x0400A485 RID: 42117
		internal const int ElementTypeIdConst = 11815;
	}
}
