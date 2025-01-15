using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B7F RID: 11135
	[GeneratedCode("DomGen", "2.0")]
	internal class ColumnHierarchyUsage : HierarchyUsageType
	{
		// Token: 0x17007A32 RID: 31282
		// (get) Token: 0x06017034 RID: 94260 RVA: 0x00331B26 File Offset: 0x0032FD26
		public override string LocalName
		{
			get
			{
				return "colHierarchyUsage";
			}
		}

		// Token: 0x17007A33 RID: 31283
		// (get) Token: 0x06017035 RID: 94261 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007A34 RID: 31284
		// (get) Token: 0x06017036 RID: 94262 RVA: 0x00331B2D File Offset: 0x0032FD2D
		internal override int ElementTypeId
		{
			get
			{
				return 11126;
			}
		}

		// Token: 0x06017037 RID: 94263 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06017039 RID: 94265 RVA: 0x00331B34 File Offset: 0x0032FD34
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColumnHierarchyUsage>(deep);
		}

		// Token: 0x04009ABE RID: 39614
		private const string tagName = "colHierarchyUsage";

		// Token: 0x04009ABF RID: 39615
		private const byte tagNsId = 22;

		// Token: 0x04009AC0 RID: 39616
		internal const int ElementTypeIdConst = 11126;
	}
}
