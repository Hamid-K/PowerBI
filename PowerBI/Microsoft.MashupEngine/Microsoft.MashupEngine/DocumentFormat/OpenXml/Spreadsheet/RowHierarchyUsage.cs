using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B7E RID: 11134
	[GeneratedCode("DomGen", "2.0")]
	internal class RowHierarchyUsage : HierarchyUsageType
	{
		// Token: 0x17007A2F RID: 31279
		// (get) Token: 0x0601702E RID: 94254 RVA: 0x00331B07 File Offset: 0x0032FD07
		public override string LocalName
		{
			get
			{
				return "rowHierarchyUsage";
			}
		}

		// Token: 0x17007A30 RID: 31280
		// (get) Token: 0x0601702F RID: 94255 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007A31 RID: 31281
		// (get) Token: 0x06017030 RID: 94256 RVA: 0x00331B0E File Offset: 0x0032FD0E
		internal override int ElementTypeId
		{
			get
			{
				return 11113;
			}
		}

		// Token: 0x06017031 RID: 94257 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06017033 RID: 94259 RVA: 0x00331B1D File Offset: 0x0032FD1D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RowHierarchyUsage>(deep);
		}

		// Token: 0x04009ABB RID: 39611
		private const string tagName = "rowHierarchyUsage";

		// Token: 0x04009ABC RID: 39612
		private const byte tagNsId = 22;

		// Token: 0x04009ABD RID: 39613
		internal const int ElementTypeIdConst = 11113;
	}
}
