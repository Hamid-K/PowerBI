using System;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;

namespace Microsoft.DataShaping.Processing.Pipeline
{
	// Token: 0x02000097 RID: 151
	internal abstract class DataPipelineWindow
	{
		// Token: 0x060003F6 RID: 1014 RVA: 0x0000CCF5 File Offset: 0x0000AEF5
		internal DataPipelineWindow(string id, int size)
		{
			this._id = id;
			this._capacity = size;
			this._constraintMode = WindowConstraintMode.Strict;
		}

		// Token: 0x060003F7 RID: 1015
		internal abstract void ExitInstance(Scope scope);

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000CD12 File Offset: 0x0000AF12
		internal virtual void SetHasExceededCapacity()
		{
			this._hasExplicitlyExceededCapacity = true;
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060003F9 RID: 1017
		internal abstract bool HasCapacity { get; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060003FA RID: 1018
		internal abstract bool IsComplete { get; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0000CD1B File Offset: 0x0000AF1B
		internal bool HasExplicitlyExceededCapacity
		{
			get
			{
				return this._hasExplicitlyExceededCapacity;
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000CD23 File Offset: 0x0000AF23
		internal bool SatisfiesWindowConstraints()
		{
			if (this._constraintMode == WindowConstraintMode.Relaxed)
			{
				return !this.HasExplicitlyExceededCapacity;
			}
			return this.HasCapacity;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000CD3E File Offset: 0x0000AF3E
		internal WindowConstraintMode SetConstraintMode(WindowConstraintMode mode)
		{
			WindowConstraintMode constraintMode = this._constraintMode;
			this._constraintMode = mode;
			return constraintMode;
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x0000CD4D File Offset: 0x0000AF4D
		internal string Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0000CD55 File Offset: 0x0000AF55
		internal WindowConstraintMode ConstraintMode
		{
			get
			{
				return this._constraintMode;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000400 RID: 1024
		internal abstract int DiagnosticInstanceCount { get; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x0000CD5D File Offset: 0x0000AF5D
		internal int Capacity
		{
			get
			{
				return this._capacity;
			}
		}

		// Token: 0x0400021A RID: 538
		private readonly string _id;

		// Token: 0x0400021B RID: 539
		private int _capacity;

		// Token: 0x0400021C RID: 540
		private bool _hasExplicitlyExceededCapacity;

		// Token: 0x0400021D RID: 541
		private WindowConstraintMode _constraintMode;
	}
}
