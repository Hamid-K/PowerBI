using System;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200003F RID: 63
	internal sealed class DataShapeBindingLimitUpgrader
	{
		// Token: 0x0600022A RID: 554 RVA: 0x0000A082 File Offset: 0x00008282
		private DataShapeBindingLimitUpgrader(DataShapeGenerationErrorContext errorContext, QueryProjections projections)
		{
			this._errorContext = errorContext;
			this._projections = projections;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000A098 File Offset: 0x00008298
		internal static bool TryUpgrade(DataShapeGenerationErrorContext errorContext, QueryProjections projections, DataShapeBinding binding, int? maxRowCount, out IntermediateDataReduction reduction)
		{
			return new DataShapeBindingLimitUpgrader(errorContext, projections).TryUpgrade(binding, maxRowCount, out reduction);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000A0AC File Offset: 0x000082AC
		private bool TryUpgrade(DataShapeBinding binding, int? maxRowCount, out IntermediateDataReduction reduction)
		{
			if (maxRowCount == null)
			{
				maxRowCount = new int?(100);
			}
			reduction = new IntermediateDataReduction();
			if (binding != null && binding.Limits != null)
			{
				for (int i = 0; i < binding.Limits.Count; i++)
				{
					DataShapeBindingLimit dataShapeBindingLimit = binding.Limits[i];
					if (dataShapeBindingLimit.Target.Primary != null)
					{
						IntermediateSimpleLimit intermediateSimpleLimit;
						if (!this.TryUpgradeLimit(dataShapeBindingLimit, out intermediateSimpleLimit))
						{
							return false;
						}
						int? count = intermediateSimpleLimit.Count;
						int value = maxRowCount.Value;
						if ((count.GetValueOrDefault() > value) & (count != null))
						{
							intermediateSimpleLimit.Count = new int?(maxRowCount.Value);
						}
						reduction.Primary = intermediateSimpleLimit;
					}
					if (dataShapeBindingLimit.Target.Secondary != null)
					{
						IntermediateSimpleLimit intermediateSimpleLimit2;
						if (!this.TryUpgradeLimit(dataShapeBindingLimit, out intermediateSimpleLimit2))
						{
							return false;
						}
						reduction.Secondary = intermediateSimpleLimit2;
					}
				}
			}
			if (this._projections.PrimaryMembers.Count > 0 && reduction.Primary == null)
			{
				reduction.Primary = new IntermediateDataWindow
				{
					IncludeRestartToken = false,
					Count = new int?(maxRowCount.Value)
				};
			}
			return true;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000A1E0 File Offset: 0x000083E0
		private bool TryUpgradeLimit(DataShapeBindingLimit limit, out IntermediateSimpleLimit intermediateLimit)
		{
			switch (limit.Type)
			{
			case DataShapeBindingLimitType.Top:
				intermediateLimit = new IntermediateSimpleLimit
				{
					Count = new int?(limit.Count),
					Kind = IntermediateSimpleLimitKind.Top
				};
				return true;
			case DataShapeBindingLimitType.First:
				intermediateLimit = new IntermediateSimpleLimit
				{
					Count = new int?(1),
					Kind = IntermediateSimpleLimitKind.First
				};
				return true;
			case DataShapeBindingLimitType.Last:
				intermediateLimit = new IntermediateSimpleLimit
				{
					Count = new int?(1),
					Kind = IntermediateSimpleLimitKind.Last
				};
				return true;
			case DataShapeBindingLimitType.Sample:
				intermediateLimit = new IntermediateSimpleLimit
				{
					Count = new int?(limit.Count),
					Kind = IntermediateSimpleLimitKind.Sample
				};
				return true;
			case DataShapeBindingLimitType.Bottom:
				intermediateLimit = new IntermediateSimpleLimit
				{
					Count = new int?(limit.Count),
					Kind = IntermediateSimpleLimitKind.Bottom
				};
				return true;
			default:
				this._errorContext.Register(DataShapeGenerationMessages.UnexpectedLimitType(EngineMessageSeverity.Error, limit.Type));
				intermediateLimit = null;
				return false;
			}
		}

		// Token: 0x04000104 RID: 260
		private const int DefaultMaxRowCount = 100;

		// Token: 0x04000105 RID: 261
		private readonly DataShapeGenerationErrorContext _errorContext;

		// Token: 0x04000106 RID: 262
		private readonly QueryProjections _projections;
	}
}
