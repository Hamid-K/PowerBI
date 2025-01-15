using System;
using System.ComponentModel;

namespace Microsoft.InfoNav
{
	// Token: 0x02000068 RID: 104
	[ImmutableObject(true)]
	public sealed class IntegerValue : NumberValue<long>
	{
		// Token: 0x06000215 RID: 533 RVA: 0x00006588 File Offset: 0x00004788
		public IntegerValue(long value)
			: base(value)
		{
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00006591 File Offset: 0x00004791
		internal override DataType Type
		{
			get
			{
				return DataType.Integer;
			}
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00006595 File Offset: 0x00004795
		public new static implicit operator IntegerValue(long value)
		{
			return new IntegerValue(value);
		}
	}
}
