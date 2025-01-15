using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000FE RID: 254
	[CompatibilityRequirement("1563")]
	public class InferredPartitionSource : PartitionSource
	{
		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06001065 RID: 4197 RVA: 0x00078EE6 File Offset: 0x000770E6
		internal override PartitionSourceType Type
		{
			get
			{
				return PartitionSourceType.Inferred;
			}
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x00078EE9 File Offset: 0x000770E9
		internal override void MoveDataToPartition(Partition partition)
		{
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x00078EEB File Offset: 0x000770EB
		internal override void LoadDataFromPartition(Partition partition, bool canResolveLinks, bool resetPartitionBodyProperties)
		{
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x00078EED File Offset: 0x000770ED
		internal override IEnumerable<CustomizedPropertyName> GetCustomizedPropertyNames()
		{
			return Enumerable.Empty<CustomizedPropertyName>();
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x00078EF4 File Offset: 0x000770F4
		private protected override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			return jsonProp.Name == "type";
		}
	}
}
