using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.PowerBI.Lucia;

namespace Microsoft.PowerBI.ExploreHost.Lucia.NLToDax
{
	// Token: 0x02000078 RID: 120
	internal sealed class SchemaDataSetProvider : ISchemaDataSetProvider
	{
		// Token: 0x0600034B RID: 843 RVA: 0x0000A91E File Offset: 0x00008B1E
		internal SchemaDataSetProvider(Func<string, IReadOnlyDictionary<string, object>, DataSet> getSchemaDataSet)
		{
			this.m_getSchemaDataSet = getSchemaDataSet;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000A92D File Offset: 0x00008B2D
		public Task<DataSet> GetSchemaDataSetAsync(string schemaName, IReadOnlyDictionary<string, object> restrictions, CancellationToken cancellationToken)
		{
			return AsyncUtils.AsCancellable<DataSet>(() => Task.FromResult<DataSet>(this.m_getSchemaDataSet(schemaName, restrictions)), cancellationToken);
		}

		// Token: 0x04000179 RID: 377
		private readonly Func<string, IReadOnlyDictionary<string, object>, DataSet> m_getSchemaDataSet;
	}
}
