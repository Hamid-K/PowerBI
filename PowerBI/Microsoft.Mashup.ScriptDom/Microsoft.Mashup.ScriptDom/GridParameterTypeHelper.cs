using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x0200009A RID: 154
	internal class GridParameterTypeHelper : OptionsHelper<GridParameterType>
	{
		// Token: 0x060002B2 RID: 690 RVA: 0x0000B91F File Offset: 0x00009B1F
		private GridParameterTypeHelper()
		{
			base.AddOptionMapping(GridParameterType.Level1, "LEVEL_1");
			base.AddOptionMapping(GridParameterType.Level2, "LEVEL_2");
			base.AddOptionMapping(GridParameterType.Level3, "LEVEL_3");
			base.AddOptionMapping(GridParameterType.Level4, "LEVEL_4");
		}

		// Token: 0x040003B3 RID: 947
		internal static readonly GridParameterTypeHelper Instance = new GridParameterTypeHelper();
	}
}
