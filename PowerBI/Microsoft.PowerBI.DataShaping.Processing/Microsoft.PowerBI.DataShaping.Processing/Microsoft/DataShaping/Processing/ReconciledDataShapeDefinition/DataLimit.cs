using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x02000026 RID: 38
	internal sealed class DataLimit
	{
		// Token: 0x06000127 RID: 295 RVA: 0x00004FDE File Offset: 0x000031DE
		internal DataLimit(string id, DataLimitOperator limitOperator, IList<string> targetScopeIds, string withinScopeId, IList<string> appliesToScopeIds, int? telemetryId)
		{
			this.Id = id;
			this.LimitOperator = limitOperator;
			this.TargetScopeIds = targetScopeIds;
			this.AppliesToScopeIds = appliesToScopeIds;
			this.WithinScopeId = withinScopeId;
			this._telemetryId = telemetryId;
			this._role = LimitTelemetryRole.None;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000128 RID: 296 RVA: 0x0000501A File Offset: 0x0000321A
		internal string Id { get; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00005022 File Offset: 0x00003222
		internal DataLimitOperator LimitOperator { get; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600012A RID: 298 RVA: 0x0000502A File Offset: 0x0000322A
		internal IList<string> TargetScopeIds { get; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00005032 File Offset: 0x00003232
		internal string WithinScopeId { get; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600012C RID: 300 RVA: 0x0000503A File Offset: 0x0000323A
		// (set) Token: 0x0600012D RID: 301 RVA: 0x00005042 File Offset: 0x00003242
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

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600012E RID: 302 RVA: 0x0000504B File Offset: 0x0000324B
		// (set) Token: 0x0600012F RID: 303 RVA: 0x00005053 File Offset: 0x00003253
		internal Scope WithinScope
		{
			get
			{
				return this._withinScope;
			}
			set
			{
				this._withinScope = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000130 RID: 304 RVA: 0x0000505C File Offset: 0x0000325C
		// (set) Token: 0x06000131 RID: 305 RVA: 0x00005064 File Offset: 0x00003264
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

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000132 RID: 306 RVA: 0x0000506D File Offset: 0x0000326D
		// (set) Token: 0x06000133 RID: 307 RVA: 0x00005075 File Offset: 0x00003275
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

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000134 RID: 308 RVA: 0x0000507E File Offset: 0x0000327E
		// (set) Token: 0x06000135 RID: 309 RVA: 0x00005086 File Offset: 0x00003286
		internal LimitTelemetryRole Role
		{
			get
			{
				return this._role;
			}
			set
			{
				this._role = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000136 RID: 310 RVA: 0x0000508F File Offset: 0x0000328F
		internal int? TelemetryId
		{
			get
			{
				return this._telemetryId;
			}
		}

		// Token: 0x040000B2 RID: 178
		private IList<Scope> _targetScopes;

		// Token: 0x040000B3 RID: 179
		private IList<string> _appliesToScopeIds;

		// Token: 0x040000B4 RID: 180
		private IList<Scope> _appliesToScopes;

		// Token: 0x040000B5 RID: 181
		private Scope _withinScope;

		// Token: 0x040000B6 RID: 182
		private LimitTelemetryRole _role;

		// Token: 0x040000B7 RID: 183
		private int? _telemetryId;
	}
}
