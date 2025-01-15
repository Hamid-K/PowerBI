using System;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000025 RID: 37
	public abstract class RowToRowTransformBase : TransformBase
	{
		// Token: 0x060000C1 RID: 193 RVA: 0x000070CC File Offset: 0x000052CC
		protected RowToRowTransformBase(IHostEnvironment env, string name, IDataView input)
			: base(env, name, input)
		{
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000070D7 File Offset: 0x000052D7
		protected RowToRowTransformBase(IHost host, IDataView input)
			: base(host, input)
		{
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000070E1 File Offset: 0x000052E1
		public sealed override long? GetRowCount(bool lazy = true)
		{
			return this._input.GetRowCount(lazy);
		}
	}
}
