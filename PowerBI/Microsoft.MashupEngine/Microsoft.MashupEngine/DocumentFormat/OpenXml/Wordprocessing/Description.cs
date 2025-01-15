using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D63 RID: 11619
	[GeneratedCode("DomGen", "2.0")]
	internal class Description : StringType
	{
		// Token: 0x170086C5 RID: 34501
		// (get) Token: 0x06018C49 RID: 101449 RVA: 0x003448EF File Offset: 0x00342AEF
		public override string LocalName
		{
			get
			{
				return "description";
			}
		}

		// Token: 0x170086C6 RID: 34502
		// (get) Token: 0x06018C4A RID: 101450 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170086C7 RID: 34503
		// (get) Token: 0x06018C4B RID: 101451 RVA: 0x003448F6 File Offset: 0x00342AF6
		internal override int ElementTypeId
		{
			get
			{
				return 11953;
			}
		}

		// Token: 0x06018C4C RID: 101452 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018C4E RID: 101454 RVA: 0x003448FD File Offset: 0x00342AFD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Description>(deep);
		}

		// Token: 0x0400A495 RID: 42133
		private const string tagName = "description";

		// Token: 0x0400A496 RID: 42134
		private const byte tagNsId = 23;

		// Token: 0x0400A497 RID: 42135
		internal const int ElementTypeIdConst = 11953;
	}
}
