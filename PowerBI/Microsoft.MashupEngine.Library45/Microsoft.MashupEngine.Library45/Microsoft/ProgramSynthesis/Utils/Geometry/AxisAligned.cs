using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Utils.Geometry
{
	// Token: 0x02000594 RID: 1428
	public class AxisAligned<T> : IEquatable<AxisAligned<T>>
	{
		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001F2D RID: 7981 RVA: 0x00059CBD File Offset: 0x00057EBD
		public T Horizontal { get; }

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001F2E RID: 7982 RVA: 0x00059CC5 File Offset: 0x00057EC5
		public T Vertical { get; }

		// Token: 0x06001F2F RID: 7983 RVA: 0x00059CCD File Offset: 0x00057ECD
		public AxisAligned(Func<Axis, T> generator)
		{
			this.Horizontal = generator(Axis.Horizontal);
			this.Vertical = generator(Axis.Vertical);
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x00059CEF File Offset: 0x00057EEF
		public AxisAligned(T horizontal, T vertical)
		{
			this.Horizontal = horizontal;
			this.Vertical = vertical;
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x00059D05 File Offset: 0x00057F05
		public AxisAligned(T both)
		{
			this.Horizontal = both;
			this.Vertical = both;
		}

		// Token: 0x1700057D RID: 1405
		public T this[Axis a]
		{
			get
			{
				if (a == Axis.Horizontal)
				{
					return this.Horizontal;
				}
				if (a != Axis.Vertical)
				{
					throw new ArgumentException("a", string.Format("Invalid axis: {0}", a));
				}
				return this.Vertical;
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001F33 RID: 7987 RVA: 0x00059D4E File Offset: 0x00057F4E
		public IEnumerable<T> AsEnumerable
		{
			get
			{
				yield return this.Horizontal;
				yield return this.Vertical;
				yield break;
			}
		}

		// Token: 0x06001F34 RID: 7988 RVA: 0x00059D5E File Offset: 0x00057F5E
		public AxisAlignedList<TElement> ToAxisAlignedList<TElement>(Func<T, List<TElement>> elementMap)
		{
			return new AxisAlignedList<TElement>((Axis axis) => elementMap(this[axis]));
		}

		// Token: 0x06001F35 RID: 7989 RVA: 0x00059D84 File Offset: 0x00057F84
		public override int GetHashCode()
		{
			T t = this.Horizontal;
			int hashCode = t.GetHashCode();
			int num = 31;
			t = this.Vertical;
			return hashCode ^ (num * t.GetHashCode());
		}

		// Token: 0x06001F36 RID: 7990 RVA: 0x00059DBD File Offset: 0x00057FBD
		public override bool Equals(object obj)
		{
			return this.Equals(obj as AxisAligned<T>);
		}

		// Token: 0x06001F37 RID: 7991 RVA: 0x00059DCC File Offset: 0x00057FCC
		public bool Equals(AxisAligned<T> other)
		{
			return other != null && (this == other || (object.Equals(this.Horizontal, other.Horizontal) && object.Equals(this.Vertical, other.Vertical)));
		}
	}
}
