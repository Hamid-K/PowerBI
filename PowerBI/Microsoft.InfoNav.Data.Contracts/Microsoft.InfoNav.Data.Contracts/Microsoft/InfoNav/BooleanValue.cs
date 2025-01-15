using System;
using System.ComponentModel;

namespace Microsoft.InfoNav
{
	// Token: 0x0200005F RID: 95
	[ImmutableObject(true)]
	public sealed class BooleanValue : DataValue<bool>
	{
		// Token: 0x06000182 RID: 386 RVA: 0x000032E4 File Offset: 0x000014E4
		private BooleanValue(bool value)
			: base(value)
		{
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000183 RID: 387 RVA: 0x000032ED File Offset: 0x000014ED
		internal override DataType Type
		{
			get
			{
				return DataType.Boolean;
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000032F0 File Offset: 0x000014F0
		public new static implicit operator BooleanValue(bool value)
		{
			if (!value)
			{
				return BooleanValue.False;
			}
			return BooleanValue.True;
		}

		// Token: 0x04000113 RID: 275
		public static readonly BooleanValue True = new BooleanValue(true);

		// Token: 0x04000114 RID: 276
		public static readonly BooleanValue False = new BooleanValue(false);
	}
}
