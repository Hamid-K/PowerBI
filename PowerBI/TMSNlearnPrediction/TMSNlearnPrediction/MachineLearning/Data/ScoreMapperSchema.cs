using System;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200021F RID: 543
	public sealed class ScoreMapperSchema : ScoreMapperSchemaBase
	{
		// Token: 0x06000C2B RID: 3115 RVA: 0x000427AE File Offset: 0x000409AE
		public ScoreMapperSchema(ColumnType scoreType, string scoreColumnKind)
			: base(scoreType, scoreColumnKind)
		{
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000C2C RID: 3116 RVA: 0x000427B8 File Offset: 0x000409B8
		public override int ColumnCount
		{
			get
			{
				return 1;
			}
		}
	}
}
