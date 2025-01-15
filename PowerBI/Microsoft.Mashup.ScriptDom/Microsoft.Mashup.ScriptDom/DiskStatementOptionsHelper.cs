using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000068 RID: 104
	internal class DiskStatementOptionsHelper : OptionsHelper<DiskStatementOptionKind>
	{
		// Token: 0x06000239 RID: 569 RVA: 0x00006824 File Offset: 0x00004A24
		private DiskStatementOptionsHelper()
		{
			base.AddOptionMapping(DiskStatementOptionKind.Name, "NAME");
			base.AddOptionMapping(DiskStatementOptionKind.PhysName, "PHYSNAME");
			base.AddOptionMapping(DiskStatementOptionKind.VDevNo, "VDEVNO");
			base.AddOptionMapping(DiskStatementOptionKind.Size, "SIZE");
			base.AddOptionMapping(DiskStatementOptionKind.VStart, "VSTART");
		}

		// Token: 0x04000182 RID: 386
		internal static readonly DiskStatementOptionsHelper Instance = new DiskStatementOptionsHelper();
	}
}
