using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000059 RID: 89
	internal sealed class DataShapeFilterResolutionResult
	{
		// Token: 0x060003FA RID: 1018 RVA: 0x0000E160 File Offset: 0x0000C360
		internal DataShapeFilterResolutionResult(Identifier targetScope, DataShape contextDataShape)
		{
			this._targetScope = targetScope;
			this._contextDataShape = contextDataShape;
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0000E176 File Offset: 0x0000C376
		internal Identifier TargetScope
		{
			get
			{
				return this._targetScope;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x0000E17E File Offset: 0x0000C37E
		internal DataShape ContextDataShape
		{
			get
			{
				return this._contextDataShape;
			}
		}

		// Token: 0x0400022D RID: 557
		private readonly Identifier _targetScope;

		// Token: 0x0400022E RID: 558
		private readonly DataShape _contextDataShape;
	}
}
