using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Utils.Geometry
{
	// Token: 0x02000597 RID: 1431
	public class AxisAlignedList<T> : AxisAligned<IReadOnlyList<T>>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x06001F42 RID: 8002 RVA: 0x00059F24 File Offset: 0x00058124
		public AxisAlignedList(Func<Axis, IEnumerable<T>> generator)
			: base(delegate(Axis axis)
			{
				IEnumerable<T> enumerable = generator(axis);
				return (enumerable as IReadOnlyList<T>) ?? enumerable.ToList<T>();
			})
		{
		}

		// Token: 0x06001F43 RID: 8003 RVA: 0x00059F50 File Offset: 0x00058150
		public bool Any()
		{
			return base.Vertical.Any<T>() || base.Horizontal.Any<T>();
		}

		// Token: 0x06001F44 RID: 8004 RVA: 0x00059F6C File Offset: 0x0005816C
		public bool IsEmpty()
		{
			return !this.Any();
		}

		// Token: 0x06001F45 RID: 8005 RVA: 0x00059F77 File Offset: 0x00058177
		public IEnumerator<T> GetEnumerator()
		{
			return base.Vertical.Concat(base.Horizontal).GetEnumerator();
		}

		// Token: 0x06001F46 RID: 8006 RVA: 0x00059F8F File Offset: 0x0005818F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000F0E RID: 3854
		public static readonly AxisAlignedList<T> Empty = new AxisAlignedList<T>((Axis _) => Enumerable.Empty<T>());
	}
}
