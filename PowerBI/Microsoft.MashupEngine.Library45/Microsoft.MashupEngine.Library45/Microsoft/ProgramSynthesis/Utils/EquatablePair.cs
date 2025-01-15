using System;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x02000487 RID: 1159
	public struct EquatablePair<T1, T2> : IEquatable<EquatablePair<T1, T2>> where T1 : IEquatable<T1> where T2 : IEquatable<T2>
	{
		// Token: 0x06001A24 RID: 6692 RVA: 0x0004F12C File Offset: 0x0004D32C
		public override int GetHashCode()
		{
			int? hashCode = this._hashCode;
			if (hashCode == null)
			{
				T1 item = this.Item1;
				int num = item.GetHashCode() * 7409299;
				T2 item2 = this.Item2;
				int? num2 = (this._hashCode = new int?(num ^ item2.GetHashCode()));
				return num2.Value;
			}
			return hashCode.GetValueOrDefault();
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06001A25 RID: 6693 RVA: 0x0004F196 File Offset: 0x0004D396
		public readonly T1 Item1 { get; }

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06001A26 RID: 6694 RVA: 0x0004F19E File Offset: 0x0004D39E
		public readonly T2 Item2 { get; }

		// Token: 0x06001A27 RID: 6695 RVA: 0x0004F1A8 File Offset: 0x0004D3A8
		public bool Equals(EquatablePair<T1, T2> other)
		{
			T1 item = this.Item1;
			if (item.Equals(other.Item1))
			{
				T2 item2 = this.Item2;
				return item2.Equals(other.Item2);
			}
			return false;
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x0004F1EF File Offset: 0x0004D3EF
		public override bool Equals(object obj)
		{
			return obj != null && obj is EquatablePair<T1, T2> && this.Equals((EquatablePair<T1, T2>)obj);
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x0004F20C File Offset: 0x0004D40C
		public static bool operator ==(EquatablePair<T1, T2> left, EquatablePair<T1, T2> right)
		{
			return left.Equals(right);
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x0004F216 File Offset: 0x0004D416
		public static bool operator !=(EquatablePair<T1, T2> left, EquatablePair<T1, T2> right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x0004F223 File Offset: 0x0004D423
		public EquatablePair(T1 item1, T2 item2)
		{
			this.Item1 = item1;
			this.Item2 = item2;
			this._hashCode = null;
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x0004F23F File Offset: 0x0004D43F
		public EquatablePair<T2, T1> Reverse()
		{
			return new EquatablePair<T2, T1>(this.Item2, this.Item1);
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x0004F252 File Offset: 0x0004D452
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("{{{0} , {1}}}", new object[] { this.Item1, this.Item2 }));
		}

		// Token: 0x04000CE7 RID: 3303
		private int? _hashCode;
	}
}
