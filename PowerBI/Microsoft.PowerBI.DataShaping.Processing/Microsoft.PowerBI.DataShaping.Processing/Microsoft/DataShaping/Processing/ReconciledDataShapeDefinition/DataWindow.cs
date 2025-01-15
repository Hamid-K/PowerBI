using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeDefinition;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x0200003E RID: 62
	internal sealed class DataWindow
	{
		// Token: 0x060001BA RID: 442 RVA: 0x00005B65 File Offset: 0x00003D65
		internal DataWindow(ExpressionNode count, IList<IList<ExpressionNode>> restartDefinitions, int? telemetryId)
		{
			this.Count = count;
			this.RestartDefinitions = restartDefinitions;
			this.ExceededDetection = ExceededDetectionKind.InstancesVsCount;
			this._telemetryId = telemetryId;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00005B8C File Offset: 0x00003D8C
		internal DataWindow(string id, ExpressionNode count, ExpressionNode isExceededDbCount, ExpressionNode dbCount, IList<IList<ExpressionNode>> restartDefinitions, IList<int> segmentationTableIds, IList<string> targetScopeIds, IList<string> appliesToScopeIds, ExceededDetectionKind exceededDetection, int? telemetryId)
		{
			this.Id = id;
			this.IsExceededDbCount = isExceededDbCount;
			this.Count = count;
			this.DbCount = dbCount;
			this.RestartDefinitions = restartDefinitions;
			this.SegmentationTableIds = segmentationTableIds;
			this.TargetScopeIds = targetScopeIds;
			this.AppliesToScopeIds = appliesToScopeIds;
			this.ExceededDetection = exceededDetection;
			this._telemetryId = telemetryId;
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00005BEC File Offset: 0x00003DEC
		internal string Id { get; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00005BF4 File Offset: 0x00003DF4
		internal ExpressionNode Count { get; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00005BFC File Offset: 0x00003DFC
		internal ExpressionNode DbCount { get; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00005C04 File Offset: 0x00003E04
		internal ExpressionNode IsExceededDbCount { get; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00005C0C File Offset: 0x00003E0C
		internal IList<IList<ExpressionNode>> RestartDefinitions { get; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00005C14 File Offset: 0x00003E14
		internal IList<int> SegmentationTableIds { get; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00005C1C File Offset: 0x00003E1C
		internal ExceededDetectionKind ExceededDetection { get; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00005C24 File Offset: 0x00003E24
		// (set) Token: 0x060001C4 RID: 452 RVA: 0x00005C2C File Offset: 0x00003E2C
		internal IList<string> TargetScopeIds
		{
			get
			{
				return this._targetScopeIds;
			}
			set
			{
				this._targetScopeIds = value;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00005C35 File Offset: 0x00003E35
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x00005C3D File Offset: 0x00003E3D
		internal IList<Scope> TargetScopes
		{
			get
			{
				return this._targetScopes;
			}
			set
			{
				this._targetScopes = value;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00005C46 File Offset: 0x00003E46
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x00005C4E File Offset: 0x00003E4E
		internal IList<string> AppliesToScopeIds
		{
			get
			{
				return this._appliesToScopeIds;
			}
			set
			{
				this._appliesToScopeIds = value;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00005C57 File Offset: 0x00003E57
		// (set) Token: 0x060001CA RID: 458 RVA: 0x00005C5F File Offset: 0x00003E5F
		internal IList<Scope> AppliesToScopes
		{
			get
			{
				return this._appliesToScopes;
			}
			set
			{
				this._appliesToScopes = value;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00005C68 File Offset: 0x00003E68
		internal int? TelemetryId
		{
			get
			{
				return this._telemetryId;
			}
		}

		// Token: 0x04000110 RID: 272
		private IList<Scope> _targetScopes;

		// Token: 0x04000111 RID: 273
		private IList<string> _targetScopeIds;

		// Token: 0x04000112 RID: 274
		private IList<Scope> _appliesToScopes;

		// Token: 0x04000113 RID: 275
		private IList<string> _appliesToScopeIds;

		// Token: 0x04000114 RID: 276
		private int? _telemetryId;
	}
}
