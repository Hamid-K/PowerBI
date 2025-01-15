using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Processing.Correlation;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;

namespace Microsoft.DataShaping.Processing.Pipeline
{
	// Token: 0x020000A0 RID: 160
	internal sealed class NestingManager
	{
		// Token: 0x0600042B RID: 1067 RVA: 0x0000D3EA File Offset: 0x0000B5EA
		internal NestingManager(ICorrelationGovernorFactory correlationGovernorFactory)
		{
			this._correlationGovernorFactory = correlationGovernorFactory;
			this._correlationCache = new Dictionary<DataShape, CorrelationGovernor>();
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x0000D404 File Offset: 0x0000B604
		internal void EnterDataShape(DataShape dataShape)
		{
			CellScopeToIntersectionRangeMapping cellScopeToIntersectionRangeMapping = dataShape.CellScopeToIntersectionRangeMapping;
			NestingManager.NestedLevel orCreateNestedLevel = this.GetOrCreateNestedLevel(dataShape, cellScopeToIntersectionRangeMapping);
			Util.PushToLazyStack<NestingManager.NestedLevel>(ref this._nestedLevels, orCreateNestedLevel);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000D42D File Offset: 0x0000B62D
		internal void ExitDataShape()
		{
			this.PopWithCheck<NestingManager.NestedLevel>(this._nestedLevels);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0000D43C File Offset: 0x0000B63C
		internal NestingManager.NestedLevel GetOrCreateNestedLevel(DataShape dataShape, CellScopeToIntersectionRangeMapping cellScopeRangeMapping)
		{
			CorrelationGovernor correlationGovernor;
			if (dataShape.HasReusableSecondary && this._correlationCache.TryGetValue(dataShape, out correlationGovernor))
			{
				correlationGovernor = correlationGovernor.ToReadOnly();
			}
			else
			{
				correlationGovernor = this._correlationGovernorFactory.CreateCorrelationGovernor(dataShape, cellScopeRangeMapping);
				if (dataShape.HasReusableSecondary)
				{
					this._correlationCache[dataShape] = correlationGovernor;
				}
			}
			return new NestingManager.NestedLevel
			{
				DataWindow = new DataWindowGovernor(),
				DataLimit = new DataLimitGovernor(),
				Correlation = correlationGovernor
			};
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x0000D4AF File Offset: 0x0000B6AF
		internal DataLimitGovernor DataLimitGovernor
		{
			get
			{
				if (this._nestedLevels.IsNullOrEmpty<NestingManager.NestedLevel>())
				{
					return null;
				}
				return this._nestedLevels.Peek().DataLimit;
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000D4D0 File Offset: 0x0000B6D0
		private void PopWithCheck<T>(Stack<T> stack)
		{
			stack.Pop();
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0000D4D9 File Offset: 0x0000B6D9
		// (set) Token: 0x06000432 RID: 1074 RVA: 0x0000D4FA File Offset: 0x0000B6FA
		internal DataWindowGovernor DataWindowGovernor
		{
			get
			{
				if (this._nestedLevels.IsNullOrEmpty<NestingManager.NestedLevel>())
				{
					return null;
				}
				return this._nestedLevels.Peek().DataWindow;
			}
			set
			{
				this._nestedLevels.Peek().DataWindow = value;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x0000D50D File Offset: 0x0000B70D
		internal CorrelationGovernor CorrelationGovernor
		{
			get
			{
				if (this._nestedLevels.IsNullOrEmpty<NestingManager.NestedLevel>())
				{
					return null;
				}
				return this._nestedLevels.Peek().Correlation;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x0000D52E File Offset: 0x0000B72E
		internal ProcessingDataShapeTelemetry DataShapeTelemetry
		{
			get
			{
				if (this._nestedLevels.IsNullOrEmpty<NestingManager.NestedLevel>())
				{
					return null;
				}
				return this._nestedLevels.Peek().Correlation.DataShapeTelemetry;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x0000D554 File Offset: 0x0000B754
		internal int Depth
		{
			get
			{
				Stack<NestingManager.NestedLevel> nestedLevels = this._nestedLevels;
				if (nestedLevels == null)
				{
					return 0;
				}
				return nestedLevels.Count;
			}
		}

		// Token: 0x0400022C RID: 556
		private readonly ICorrelationGovernorFactory _correlationGovernorFactory;

		// Token: 0x0400022D RID: 557
		private readonly Dictionary<DataShape, CorrelationGovernor> _correlationCache;

		// Token: 0x0400022E RID: 558
		private Stack<NestingManager.NestedLevel> _nestedLevels;

		// Token: 0x020000F6 RID: 246
		internal class NestedLevel
		{
			// Token: 0x04000384 RID: 900
			public DataLimitGovernor DataLimit;

			// Token: 0x04000385 RID: 901
			public DataWindowGovernor DataWindow;

			// Token: 0x04000386 RID: 902
			public CorrelationGovernor Correlation;
		}
	}
}
