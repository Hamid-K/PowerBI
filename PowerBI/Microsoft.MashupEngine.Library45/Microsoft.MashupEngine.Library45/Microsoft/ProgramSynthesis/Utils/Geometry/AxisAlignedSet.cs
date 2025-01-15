using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Utils.Geometry
{
	// Token: 0x0200059A RID: 1434
	public class AxisAlignedSet<T> : AxisAligned<HashSet<T>>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x06001F4D RID: 8013 RVA: 0x00059FF2 File Offset: 0x000581F2
		public AxisAlignedSet()
			: base((Axis axis) => new HashSet<T>())
		{
		}

		// Token: 0x06001F4E RID: 8014 RVA: 0x0005A01C File Offset: 0x0005821C
		public AxisAlignedSet(Func<Axis, IEnumerable<T>> generator)
			: base((Axis axis) => generator(axis).ConvertToHashSet<T>())
		{
		}

		// Token: 0x06001F4F RID: 8015 RVA: 0x0005A048 File Offset: 0x00058248
		public bool Any()
		{
			return base.Vertical.Any<T>() || base.Horizontal.Any<T>();
		}

		// Token: 0x06001F50 RID: 8016 RVA: 0x0005A064 File Offset: 0x00058264
		public bool IsEmpty()
		{
			return !this.Any();
		}

		// Token: 0x06001F51 RID: 8017 RVA: 0x0005A06F File Offset: 0x0005826F
		public void AddRange<TCollection>(AxisAligned<TCollection> other) where TCollection : IEnumerable<T>
		{
			base.Horizontal.AddRange(other.Horizontal);
			base.Vertical.AddRange(other.Vertical);
		}

		// Token: 0x06001F52 RID: 8018 RVA: 0x0005A09D File Offset: 0x0005829D
		public void Clear()
		{
			base.Horizontal.Clear();
			base.Vertical.Clear();
		}

		// Token: 0x06001F53 RID: 8019 RVA: 0x0005A0B5 File Offset: 0x000582B5
		public IEnumerator<T> GetEnumerator()
		{
			return base.Vertical.Concat(base.Horizontal).GetEnumerator();
		}

		// Token: 0x06001F54 RID: 8020 RVA: 0x0005A0CD File Offset: 0x000582CD
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}
	}
}
