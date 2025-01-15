using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200005E RID: 94
	internal struct GeneratedFilterCondition
	{
		// Token: 0x06000457 RID: 1111 RVA: 0x00010A06 File Offset: 0x0000EC06
		internal static GeneratedFilterCondition CreateSucceeded(FilterCondition condition, DsqFilterType? filterType)
		{
			return new GeneratedFilterCondition(condition, FilterConversionStatus.Succeeded, filterType);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x00010A10 File Offset: 0x0000EC10
		internal GeneratedFilterCondition(FilterCondition condition, FilterConversionStatus conversionStatus, DsqFilterType? filterType)
		{
			this.Condition = condition;
			this.FilterType = filterType;
			this.ConversionStatus = conversionStatus;
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x00010A27 File Offset: 0x0000EC27
		// (set) Token: 0x0600045A RID: 1114 RVA: 0x00010A2F File Offset: 0x0000EC2F
		internal FilterCondition Condition { readonly get; private set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x00010A38 File Offset: 0x0000EC38
		// (set) Token: 0x0600045C RID: 1116 RVA: 0x00010A40 File Offset: 0x0000EC40
		internal FilterConversionStatus ConversionStatus { readonly get; private set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x00010A49 File Offset: 0x0000EC49
		// (set) Token: 0x0600045E RID: 1118 RVA: 0x00010A51 File Offset: 0x0000EC51
		internal DsqFilterType? FilterType { readonly get; private set; }

		// Token: 0x04000259 RID: 601
		internal static readonly GeneratedFilterCondition Empty = new GeneratedFilterCondition(null, FilterConversionStatus.Failed, null);

		// Token: 0x0400025A RID: 602
		internal static readonly GeneratedFilterCondition Ignored = new GeneratedFilterCondition(null, FilterConversionStatus.Ignored, null);
	}
}
