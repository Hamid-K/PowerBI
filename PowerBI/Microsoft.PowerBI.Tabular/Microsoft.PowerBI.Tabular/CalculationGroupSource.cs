using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000EB RID: 235
	[CompatibilityRequirement("1470")]
	public class CalculationGroupSource : PartitionSource
	{
		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x06000F95 RID: 3989 RVA: 0x00077176 File Offset: 0x00075376
		internal override PartitionSourceType Type
		{
			get
			{
				return PartitionSourceType.CalculationGroup;
			}
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x00077179 File Offset: 0x00075379
		internal override void MoveDataToPartition(Partition partition)
		{
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x0007717B File Offset: 0x0007537B
		internal override void LoadDataFromPartition(Partition partition, bool canResolveLinks, bool resetPartitionBodyProperties)
		{
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x0007717D File Offset: 0x0007537D
		internal override IEnumerable<CustomizedPropertyName> GetCustomizedPropertyNames()
		{
			return Enumerable.Empty<CustomizedPropertyName>();
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x00077184 File Offset: 0x00075384
		private protected override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			return jsonProp.Name == "type";
		}
	}
}
