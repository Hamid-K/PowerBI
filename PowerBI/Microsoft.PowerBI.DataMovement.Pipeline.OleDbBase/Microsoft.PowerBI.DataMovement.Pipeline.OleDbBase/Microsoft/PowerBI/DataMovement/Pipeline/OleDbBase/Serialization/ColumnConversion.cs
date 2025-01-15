using System;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization
{
	// Token: 0x020000CF RID: 207
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	public sealed class ColumnConversion
	{
		// Token: 0x060003B2 RID: 946 RVA: 0x0000B260 File Offset: 0x00009460
		public ColumnConversion(Type resultType, Action<object, Column> addValue)
		{
			this.resultType = resultType;
			this.addValue = addValue;
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0000B276 File Offset: 0x00009476
		public Type ResultType
		{
			get
			{
				return this.resultType;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0000B27E File Offset: 0x0000947E
		public Action<object, Column> AddValue
		{
			get
			{
				return this.addValue;
			}
		}

		// Token: 0x040003A0 RID: 928
		private readonly Type resultType;

		// Token: 0x040003A1 RID: 929
		private readonly Action<object, Column> addValue;
	}
}
