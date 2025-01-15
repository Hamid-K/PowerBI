using System;
using System.ComponentModel;

namespace Microsoft.InfoNav
{
	// Token: 0x02000066 RID: 102
	[ImmutableObject(true)]
	internal sealed class DecadeValue : DataValue<int>, IDatePartValue
	{
		// Token: 0x06000212 RID: 530 RVA: 0x00006570 File Offset: 0x00004770
		internal DecadeValue(int decade)
			: base(decade)
		{
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000213 RID: 531 RVA: 0x00006579 File Offset: 0x00004779
		internal override DataType Type
		{
			get
			{
				return DataType.Decade;
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00006580 File Offset: 0x00004780
		public static implicit operator DecadeValue(int value)
		{
			return new DecadeValue(value);
		}
	}
}
