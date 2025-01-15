using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200012A RID: 298
	internal class DbccJoinOptionsHelper : OptionsHelper<DbccOptionKind>
	{
		// Token: 0x060014BE RID: 5310 RVA: 0x00090C72 File Offset: 0x0008EE72
		private DbccJoinOptionsHelper()
		{
			base.AddOptionMapping(DbccOptionKind.StatHeader, "STAT_HEADER");
			base.AddOptionMapping(DbccOptionKind.DensityVector, "DENSITY_VECTOR");
		}

		// Token: 0x04001146 RID: 4422
		internal static readonly DbccJoinOptionsHelper Instance = new DbccJoinOptionsHelper();
	}
}
