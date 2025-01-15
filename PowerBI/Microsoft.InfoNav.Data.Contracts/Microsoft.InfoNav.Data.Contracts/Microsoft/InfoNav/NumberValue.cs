using System;
using System.ComponentModel;

namespace Microsoft.InfoNav
{
	// Token: 0x0200006B RID: 107
	[ImmutableObject(true)]
	public class NumberValue<T> : DataValue<T> where T : struct
	{
		// Token: 0x0600021F RID: 543 RVA: 0x000065E8 File Offset: 0x000047E8
		public NumberValue(T value)
			: base(value)
		{
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000220 RID: 544 RVA: 0x000065F1 File Offset: 0x000047F1
		internal override DataType Type
		{
			get
			{
				return DataType.Number;
			}
		}
	}
}
