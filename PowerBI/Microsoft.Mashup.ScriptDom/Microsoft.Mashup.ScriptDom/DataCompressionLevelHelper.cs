using System;

namespace Microsoft.Mashup.ScriptDom
{
	// Token: 0x02000096 RID: 150
	internal class DataCompressionLevelHelper : OptionsHelper<DataCompressionLevel>
	{
		// Token: 0x060002AE RID: 686 RVA: 0x0000B8BB File Offset: 0x00009ABB
		private DataCompressionLevelHelper()
		{
			base.AddOptionMapping(DataCompressionLevel.None, "NONE");
			base.AddOptionMapping(DataCompressionLevel.Page, "PAGE");
			base.AddOptionMapping(DataCompressionLevel.Row, "ROW");
		}

		// Token: 0x040003A7 RID: 935
		public static readonly DataCompressionLevelHelper Instance = new DataCompressionLevelHelper();
	}
}
