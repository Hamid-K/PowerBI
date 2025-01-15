using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.DataShaping.ServiceContracts.QueryTranslation
{
	// Token: 0x0200001D RID: 29
	public sealed class ClusteringTranslationResult
	{
		// Token: 0x060000A3 RID: 163 RVA: 0x00002A61 File Offset: 0x00000C61
		public ClusteringTranslationResult(string mappingTableName, string mappingTableExpression, string groupingColumnExpression, string partitionIdColumnName, IReadOnlyList<IReadOnlyList<PartitionTableIdentityMapping>> itemIdMappings)
		{
			this.MappingTableName = mappingTableName;
			this.MappingTableExpression = mappingTableExpression;
			this.GroupingColumnExpression = groupingColumnExpression;
			this.PartitionIdColumnName = partitionIdColumnName;
			this.ItemIdMappings = itemIdMappings;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00002A8E File Offset: 0x00000C8E
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x00002A96 File Offset: 0x00000C96
		public string MappingTableName { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00002A9F File Offset: 0x00000C9F
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00002AA7 File Offset: 0x00000CA7
		public string MappingTableExpression { get; private set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00002AB0 File Offset: 0x00000CB0
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x00002AB8 File Offset: 0x00000CB8
		public string GroupingColumnExpression { get; private set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00002AC1 File Offset: 0x00000CC1
		// (set) Token: 0x060000AB RID: 171 RVA: 0x00002AC9 File Offset: 0x00000CC9
		public string PartitionIdColumnName { get; private set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000AC RID: 172 RVA: 0x00002AD2 File Offset: 0x00000CD2
		// (set) Token: 0x060000AD RID: 173 RVA: 0x00002ADA File Offset: 0x00000CDA
		public IReadOnlyList<IReadOnlyList<PartitionTableIdentityMapping>> ItemIdMappings { get; private set; }
	}
}
