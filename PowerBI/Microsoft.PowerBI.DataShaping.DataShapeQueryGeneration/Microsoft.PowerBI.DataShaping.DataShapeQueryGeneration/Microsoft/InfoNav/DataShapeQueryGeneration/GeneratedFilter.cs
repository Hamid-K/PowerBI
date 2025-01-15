using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000062 RID: 98
	internal struct GeneratedFilter
	{
		// Token: 0x0600046C RID: 1132 RVA: 0x00010CD5 File Offset: 0x0000EED5
		internal GeneratedFilter(FilterCondition condition, DsqFilterType? filterType, Identifier targetScope)
		{
			this = new GeneratedFilter(condition, FilterConversionStatus.Succeeded, filterType, targetScope);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x00010CE1 File Offset: 0x0000EEE1
		internal GeneratedFilter(FilterCondition condition, FilterConversionStatus conversionStatus, DsqFilterType? filterType, Identifier targetScope = null)
		{
			this = default(GeneratedFilter);
			this.Condition = condition;
			this.TargetScope = targetScope;
			this.FilterType = filterType;
			this.ConversionStatus = conversionStatus;
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x00010D07 File Offset: 0x0000EF07
		internal readonly FilterCondition Condition { get; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x00010D0F File Offset: 0x0000EF0F
		internal readonly FilterConversionStatus ConversionStatus { get; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x00010D17 File Offset: 0x0000EF17
		internal readonly DsqFilterType? FilterType { get; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x00010D1F File Offset: 0x0000EF1F
		internal readonly Identifier TargetScope { get; }

		// Token: 0x04000270 RID: 624
		internal static readonly GeneratedFilter Empty = new GeneratedFilter(null, FilterConversionStatus.Failed, null, null);

		// Token: 0x04000271 RID: 625
		internal static readonly GeneratedFilter Ignored = new GeneratedFilter(null, FilterConversionStatus.Ignored, null, null);
	}
}
