using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;

namespace Microsoft.DataShaping.Processing.Pipeline
{
	// Token: 0x02000095 RID: 149
	internal abstract class DataPipelineLimit
	{
		// Token: 0x060003EA RID: 1002 RVA: 0x0000CBE0 File Offset: 0x0000ADE0
		protected DataPipelineLimit(string id, int capacity, IList<Scope> targetScopes, Scope withinScope, bool skipInstancesWhenExceeded, int? warningCount)
		{
			this._id = id;
			this._instanceCount = 0;
			this._targetScopes = targetScopes;
			this._withinScope = withinScope;
			this._capacity = capacity;
			this._skipInstancesWhenExceeded = skipInstancesWhenExceeded;
			this._warningCount = warningCount;
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060003EB RID: 1003
		internal abstract bool HasCapacity { get; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060003EC RID: 1004
		internal abstract bool IsExceededByAnyInstance { get; }

		// Token: 0x060003ED RID: 1005
		internal abstract void SetExceeded();

		// Token: 0x060003EE RID: 1006 RVA: 0x0000CC1C File Offset: 0x0000AE1C
		internal virtual void ExitInstance(Scope scope)
		{
			if (this._targetScopes.Contains(scope))
			{
				this._instanceCount++;
				return;
			}
			if (scope == this._withinScope)
			{
				this._instanceCount = 0;
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000CC4B File Offset: 0x0000AE4B
		internal string Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0000CC53 File Offset: 0x0000AE53
		internal virtual int InstanceCount
		{
			get
			{
				return this._instanceCount;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0000CC5B File Offset: 0x0000AE5B
		internal int Capacity
		{
			get
			{
				return this._capacity;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0000CC63 File Offset: 0x0000AE63
		public bool SkipInstancesWhenExceeded
		{
			get
			{
				return this._skipInstancesWhenExceeded;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0000CC6B File Offset: 0x0000AE6B
		public int? WarningCount
		{
			get
			{
				return this._warningCount;
			}
		}

		// Token: 0x04000213 RID: 531
		private readonly string _id;

		// Token: 0x04000214 RID: 532
		private readonly IList<Scope> _targetScopes;

		// Token: 0x04000215 RID: 533
		private readonly Scope _withinScope;

		// Token: 0x04000216 RID: 534
		private readonly int _capacity;

		// Token: 0x04000217 RID: 535
		private readonly bool _skipInstancesWhenExceeded;

		// Token: 0x04000218 RID: 536
		private int _instanceCount;

		// Token: 0x04000219 RID: 537
		private int? _warningCount;
	}
}
