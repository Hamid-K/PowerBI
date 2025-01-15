using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D58 RID: 11608
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class TableDescription : StringType
	{
		// Token: 0x170086A4 RID: 34468
		// (get) Token: 0x06018C07 RID: 101383 RVA: 0x00344815 File Offset: 0x00342A15
		public override string LocalName
		{
			get
			{
				return "tblDescription";
			}
		}

		// Token: 0x170086A5 RID: 34469
		// (get) Token: 0x06018C08 RID: 101384 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170086A6 RID: 34470
		// (get) Token: 0x06018C09 RID: 101385 RVA: 0x0034481C File Offset: 0x00342A1C
		internal override int ElementTypeId
		{
			get
			{
				return 11786;
			}
		}

		// Token: 0x06018C0A RID: 101386 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06018C0C RID: 101388 RVA: 0x00344823 File Offset: 0x00342A23
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableDescription>(deep);
		}

		// Token: 0x0400A474 RID: 42100
		private const string tagName = "tblDescription";

		// Token: 0x0400A475 RID: 42101
		private const byte tagNsId = 23;

		// Token: 0x0400A476 RID: 42102
		internal const int ElementTypeIdConst = 11786;
	}
}
