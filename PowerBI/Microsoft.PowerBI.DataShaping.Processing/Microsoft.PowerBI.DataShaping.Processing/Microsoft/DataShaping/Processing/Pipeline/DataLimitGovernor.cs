using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;

namespace Microsoft.DataShaping.Processing.Pipeline
{
	// Token: 0x0200008E RID: 142
	internal sealed class DataLimitGovernor
	{
		// Token: 0x0600039C RID: 924 RVA: 0x0000BF46 File Offset: 0x0000A146
		internal void AddCapacityLimit(string id, int count, IList<Scope> targetScopes, Scope withinScope, bool skipInstancesWhenExceeded, int? warningCount)
		{
			Util.AddToLazyList<DataPipelineLimit>(ref this._limits, new DataPipelineCapacityLimit(id, count, targetScopes, withinScope, skipInstancesWhenExceeded, warningCount));
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000BF61 File Offset: 0x0000A161
		internal void AddDbCountLimit(string id, int count, int isExceededDbCount, IList<Scope> targetScopes, Scope withinScope, bool skipInstancesWhenExceeded, int? warningCount)
		{
			Util.AddToLazyList<DataPipelineLimit>(ref this._limits, new DataPipelineDbCountLimit(id, count, isExceededDbCount, targetScopes, withinScope, skipInstancesWhenExceeded, warningCount));
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000BF7E File Offset: 0x0000A17E
		internal void AddDbCountVsCountLimit(string id, int count, int isExceededDbCount, IList<Scope> targetScopes, Scope withinScope, bool skipInstancesWhenExceeded, int? warningCount)
		{
			Util.AddToLazyList<DataPipelineLimit>(ref this._limits, new DataPipelineDbCountVsCountLimit(id, count, isExceededDbCount, targetScopes, withinScope, skipInstancesWhenExceeded, warningCount));
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000BF9C File Offset: 0x0000A19C
		internal void AddLimit(Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataLimit limit, int count, int? isExceededDbCount, int? warningCount)
		{
			Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataLimitOperator limitOperator = limit.LimitOperator;
			switch (limitOperator.Kind)
			{
			case ExceededDetectionKind.InstancesVsCount:
				this.AddCapacityLimit(limit.Id, count, limit.TargetScopes, limit.WithinScope, limitOperator.SkipInstancesWhenExceeded, warningCount);
				return;
			case ExceededDetectionKind.DbCountVsInstances:
				this.AddDbCountLimit(limit.Id, count, isExceededDbCount.Value, limit.TargetScopes, limit.WithinScope, limitOperator.SkipInstancesWhenExceeded, warningCount);
				return;
			case ExceededDetectionKind.DbCountVsCount:
				this.AddDbCountVsCountLimit(limit.Id, count, isExceededDbCount.Value, limit.TargetScopes, limit.WithinScope, limitOperator.SkipInstancesWhenExceeded, warningCount);
				return;
			default:
				Contract.RetailFail("Unknown DataLimitOperatorKind '{0}'", limitOperator.Kind);
				return;
			}
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000C054 File Offset: 0x0000A254
		internal DataPipelineLimit GetLimit(string id)
		{
			if (this._limits != null)
			{
				for (int i = 0; i < this._limits.Count; i++)
				{
					DataPipelineLimit dataPipelineLimit = this._limits[i];
					if (dataPipelineLimit.Id == id)
					{
						return dataPipelineLimit;
					}
				}
			}
			return null;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0000C0A0 File Offset: 0x0000A2A0
		internal void ExitInstance(Scope scope)
		{
			if (this.ActiveMemberHasWithinLimits)
			{
				foreach (int num in this._activeMember.WithinLimits)
				{
					this._limits[num].ExitInstance(scope);
				}
			}
			if (this.ActiveMemberHasApplicableLimits)
			{
				foreach (int num2 in this._activeMember.ApplicableLimits)
				{
					this._limits[num2].ExitInstance(scope);
				}
			}
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000C15C File Offset: 0x0000A35C
		internal void ExitDataIntersectionInstance(Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataIntersection dataIntersection)
		{
			IList<int> applicableLimits = dataIntersection.ApplicableLimits;
			if (applicableLimits != null)
			{
				int num = applicableLimits[0];
				DataPipelineLimit dataPipelineLimit = this._limits[num];
				dataPipelineLimit.ExitInstance(dataIntersection);
				if (!dataPipelineLimit.HasCapacity)
				{
					dataPipelineLimit.SetExceeded();
				}
			}
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000C1A0 File Offset: 0x0000A3A0
		internal bool IsConstrainedByLimitWithoutCapacity(Scope scope)
		{
			if (scope.ApplicableLimits.IsNullOrEmpty<int>())
			{
				return false;
			}
			foreach (int num in scope.ApplicableLimits)
			{
				DataPipelineLimit dataPipelineLimit = this._limits[num];
				if (!dataPipelineLimit.HasCapacity && dataPipelineLimit.SkipInstancesWhenExceeded)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x0000C21C File Offset: 0x0000A41C
		internal bool HasCapacity
		{
			get
			{
				if (!this.ActiveMemberHasApplicableLimits)
				{
					return true;
				}
				foreach (int num in this._activeMember.ApplicableLimits)
				{
					if (!this._limits[num].HasCapacity)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000C28C File Offset: 0x0000A48C
		internal void SetLimitsExceeded()
		{
			if (!this.ActiveMemberHasApplicableLimits)
			{
				return;
			}
			foreach (int num in this._activeMember.ApplicableLimits)
			{
				DataPipelineLimit dataPipelineLimit = this._limits[num];
				if (!dataPipelineLimit.HasCapacity)
				{
					dataPipelineLimit.SetExceeded();
					break;
				}
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x0000C300 File Offset: 0x0000A500
		internal bool SkipInstancesWhenExceeded
		{
			get
			{
				if (this._activeMember == null || this._activeMember.ApplicableLimits == null)
				{
					return false;
				}
				foreach (int num in this._activeMember.ApplicableLimits)
				{
					DataPipelineLimit dataPipelineLimit = this._limits[num];
					if (!dataPipelineLimit.HasCapacity && dataPipelineLimit.SkipInstancesWhenExceeded)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0000C388 File Offset: 0x0000A588
		// (set) Token: 0x060003A8 RID: 936 RVA: 0x0000C390 File Offset: 0x0000A590
		internal Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataMember ActiveMember
		{
			get
			{
				return this._activeMember;
			}
			set
			{
				this._activeMember = value;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0000C399 File Offset: 0x0000A599
		internal List<DataPipelineLimit> Limits
		{
			get
			{
				return this._limits;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060003AA RID: 938 RVA: 0x0000C3A1 File Offset: 0x0000A5A1
		private bool ActiveMemberHasApplicableLimits
		{
			get
			{
				return this._activeMember != null && this._activeMember.ApplicableLimits != null;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0000C3BB File Offset: 0x0000A5BB
		private bool ActiveMemberHasWithinLimits
		{
			get
			{
				return this._activeMember != null && this._activeMember.WithinLimits != null;
			}
		}

		// Token: 0x04000205 RID: 517
		private List<DataPipelineLimit> _limits;

		// Token: 0x04000206 RID: 518
		private Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataMember _activeMember;
	}
}
