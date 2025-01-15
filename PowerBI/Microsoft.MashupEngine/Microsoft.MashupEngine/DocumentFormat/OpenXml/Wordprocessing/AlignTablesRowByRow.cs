using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E01 RID: 11777
	[GeneratedCode("DomGen", "2.0")]
	internal class AlignTablesRowByRow : OnOffType
	{
		// Token: 0x1700889F RID: 34975
		// (get) Token: 0x06018FFE RID: 102398 RVA: 0x00345724 File Offset: 0x00343924
		public override string LocalName
		{
			get
			{
				return "alignTablesRowByRow";
			}
		}

		// Token: 0x170088A0 RID: 34976
		// (get) Token: 0x06018FFF RID: 102399 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170088A1 RID: 34977
		// (get) Token: 0x06019000 RID: 102400 RVA: 0x0034572B File Offset: 0x0034392B
		internal override int ElementTypeId
		{
			get
			{
				return 12087;
			}
		}

		// Token: 0x06019001 RID: 102401 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019003 RID: 102403 RVA: 0x00345732 File Offset: 0x00343932
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AlignTablesRowByRow>(deep);
		}

		// Token: 0x0400A66E RID: 42606
		private const string tagName = "alignTablesRowByRow";

		// Token: 0x0400A66F RID: 42607
		private const byte tagNsId = 23;

		// Token: 0x0400A670 RID: 42608
		internal const int ElementTypeIdConst = 12087;
	}
}
