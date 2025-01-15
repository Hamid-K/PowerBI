using System;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.DataShaping.SemanticQueryTranslation.Clustering
{
	// Token: 0x02000027 RID: 39
	internal struct ClusteringLookupTuple
	{
		// Token: 0x06000127 RID: 295 RVA: 0x00006AFC File Offset: 0x00004CFC
		internal ClusteringLookupTuple(string daxColumnName, ResolvedQueryExpression columnExpression)
		{
			this._daxColumnName = daxColumnName;
			this._columnExpression = columnExpression;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00006B0C File Offset: 0x00004D0C
		public string DaxColumnName
		{
			get
			{
				return this._daxColumnName;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00006B14 File Offset: 0x00004D14
		internal ResolvedQueryExpression ColumnExpression
		{
			get
			{
				return this._columnExpression;
			}
		}

		// Token: 0x0400007F RID: 127
		private readonly string _daxColumnName;

		// Token: 0x04000080 RID: 128
		private readonly ResolvedQueryExpression _columnExpression;
	}
}
