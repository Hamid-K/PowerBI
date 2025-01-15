using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Parquet.Schema
{
	// Token: 0x02001FE7 RID: 8167
	internal struct ValueTuple<T1, T2, T3> : IEquatable<ValueTuple<T1, T2, T3>>
	{
		// Token: 0x060110CB RID: 69835 RVA: 0x003ACFF1 File Offset: 0x003AB1F1
		public ValueTuple(T1 item1, T2 item2, T3 item3)
		{
			this.item1 = item1;
			this.item2 = item2;
			this.item3 = item3;
		}

		// Token: 0x17002CFD RID: 11517
		// (get) Token: 0x060110CC RID: 69836 RVA: 0x003AD008 File Offset: 0x003AB208
		public T1 Item1
		{
			get
			{
				return this.item1;
			}
		}

		// Token: 0x17002CFE RID: 11518
		// (get) Token: 0x060110CD RID: 69837 RVA: 0x003AD010 File Offset: 0x003AB210
		public T2 Item2
		{
			get
			{
				return this.item2;
			}
		}

		// Token: 0x17002CFF RID: 11519
		// (get) Token: 0x060110CE RID: 69838 RVA: 0x003AD018 File Offset: 0x003AB218
		public T3 Item3
		{
			get
			{
				return this.item3;
			}
		}

		// Token: 0x060110CF RID: 69839 RVA: 0x003AD020 File Offset: 0x003AB220
		public override bool Equals(object obj)
		{
			ValueTuple<T1, T2, T3>? valueTuple = obj as ValueTuple<T1, T2, T3>?;
			return valueTuple != null && this.Equals(valueTuple.Value);
		}

		// Token: 0x060110D0 RID: 69840 RVA: 0x003AD054 File Offset: 0x003AB254
		public bool Equals(ValueTuple<T1, T2, T3> other)
		{
			return ValueTuple<T1, T2, T3>.t1Comparer.Equals(this.Item1, other.Item1) && ValueTuple<T1, T2, T3>.t2Comparer.Equals(this.Item2, other.Item2) && ValueTuple<T1, T2, T3>.t3Comparer.Equals(this.Item3, other.Item3);
		}

		// Token: 0x060110D1 RID: 69841 RVA: 0x003AD0AC File Offset: 0x003AB2AC
		public override int GetHashCode()
		{
			return (ValueTuple<T1, T2, T3>.t1Comparer.GetHashCode(this.Item1) * 23 + ValueTuple<T1, T2, T3>.t2Comparer.GetHashCode(this.Item2)) * 23 + ValueTuple<T1, T2, T3>.t3Comparer.GetHashCode(this.Item3);
		}

		// Token: 0x0400671F RID: 26399
		private static readonly EqualityComparer<T1> t1Comparer = EqualityComparer<T1>.Default;

		// Token: 0x04006720 RID: 26400
		private static readonly EqualityComparer<T2> t2Comparer = EqualityComparer<T2>.Default;

		// Token: 0x04006721 RID: 26401
		private static readonly EqualityComparer<T3> t3Comparer = EqualityComparer<T3>.Default;

		// Token: 0x04006722 RID: 26402
		private readonly T1 item1;

		// Token: 0x04006723 RID: 26403
		private readonly T2 item2;

		// Token: 0x04006724 RID: 26404
		private readonly T3 item3;
	}
}
