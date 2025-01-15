using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000116 RID: 278
	internal class AlterIndexTypeHelper : OptionsHelper<AlterIndexType>
	{
		// Token: 0x060014A2 RID: 5282 RVA: 0x00090825 File Offset: 0x0008EA25
		private AlterIndexTypeHelper()
		{
			base.AddOptionMapping(AlterIndexType.Disable, "DISABLE");
			base.AddOptionMapping(AlterIndexType.Rebuild, "REBUILD");
			base.AddOptionMapping(AlterIndexType.Reorganize, "REORGANIZE");
			base.AddOptionMapping(AlterIndexType.Set, TSqlTokenType.Set);
		}

		// Token: 0x04001110 RID: 4368
		internal static readonly AlterIndexTypeHelper Instance = new AlterIndexTypeHelper();
	}
}
