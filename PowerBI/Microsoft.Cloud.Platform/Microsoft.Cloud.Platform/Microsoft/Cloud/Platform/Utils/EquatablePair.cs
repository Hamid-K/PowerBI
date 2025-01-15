using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000219 RID: 537
	public class EquatablePair<TFirst, TSecond> : Pair<TFirst, TSecond>, IEquatable<EquatablePair<TFirst, TSecond>> where TFirst : IEquatable<TFirst> where TSecond : IEquatable<TSecond>
	{
		// Token: 0x06000E25 RID: 3621 RVA: 0x00032158 File Offset: 0x00030358
		public EquatablePair(TFirst first, TSecond second)
			: base(first, second)
		{
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x00032164 File Offset: 0x00030364
		public bool Equals(EquatablePair<TFirst, TSecond> other)
		{
			if (other != null)
			{
				if (base.First != null)
				{
					TFirst first = base.First;
					if (first.Equals(other.First))
					{
						goto IL_0049;
					}
				}
				if (base.First != null || other.First != null)
				{
					return false;
				}
				IL_0049:
				if (base.Second != null)
				{
					TSecond second = base.Second;
					if (second.Equals(other.Second))
					{
						return true;
					}
				}
				return base.Second == null && other.Second == null;
			}
			return false;
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x00032206 File Offset: 0x00030406
		public override bool Equals(object obj)
		{
			return this.Equals(obj as EquatablePair<TFirst, TSecond>);
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x00032214 File Offset: 0x00030414
		public override int GetHashCode()
		{
			int num;
			if (base.First == null)
			{
				num = 0;
			}
			else
			{
				TFirst first = base.First;
				num = first.GetHashCode();
			}
			int num2;
			if (base.Second == null)
			{
				num2 = 0;
			}
			else
			{
				TSecond second = base.Second;
				num2 = second.GetHashCode();
			}
			return num ^ num2;
		}
	}
}
