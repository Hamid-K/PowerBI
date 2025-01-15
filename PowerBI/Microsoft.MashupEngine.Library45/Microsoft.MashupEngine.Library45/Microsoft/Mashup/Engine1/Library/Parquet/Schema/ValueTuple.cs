using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Parquet.Schema
{
	// Token: 0x02001FE6 RID: 8166
	internal struct ValueTuple<T1, T2> : IEquatable<ValueTuple<T1, T2>>
	{
		// Token: 0x060110C4 RID: 69828 RVA: 0x003ACF2E File Offset: 0x003AB12E
		public ValueTuple(T1 item1, T2 item2)
		{
			this.item1 = item1;
			this.item2 = item2;
		}

		// Token: 0x17002CFB RID: 11515
		// (get) Token: 0x060110C5 RID: 69829 RVA: 0x003ACF3E File Offset: 0x003AB13E
		public T1 Item1
		{
			get
			{
				return this.item1;
			}
		}

		// Token: 0x17002CFC RID: 11516
		// (get) Token: 0x060110C6 RID: 69830 RVA: 0x003ACF46 File Offset: 0x003AB146
		public T2 Item2
		{
			get
			{
				return this.item2;
			}
		}

		// Token: 0x060110C7 RID: 69831 RVA: 0x003ACF50 File Offset: 0x003AB150
		public override bool Equals(object obj)
		{
			ValueTuple<T1, T2>? valueTuple = obj as ValueTuple<T1, T2>?;
			return valueTuple != null && this.Equals(valueTuple.Value);
		}

		// Token: 0x060110C8 RID: 69832 RVA: 0x003ACF81 File Offset: 0x003AB181
		public bool Equals(ValueTuple<T1, T2> other)
		{
			return ValueTuple<T1, T2>.t1Comparer.Equals(this.Item1, other.Item1) && ValueTuple<T1, T2>.t2Comparer.Equals(this.Item2, other.Item2);
		}

		// Token: 0x060110C9 RID: 69833 RVA: 0x003ACFB5 File Offset: 0x003AB1B5
		public override int GetHashCode()
		{
			return ValueTuple<T1, T2>.t1Comparer.GetHashCode(this.Item1) * 23 + ValueTuple<T1, T2>.t2Comparer.GetHashCode(this.Item2);
		}

		// Token: 0x0400671B RID: 26395
		private static readonly EqualityComparer<T1> t1Comparer = EqualityComparer<T1>.Default;

		// Token: 0x0400671C RID: 26396
		private static readonly EqualityComparer<T2> t2Comparer = EqualityComparer<T2>.Default;

		// Token: 0x0400671D RID: 26397
		private readonly T1 item1;

		// Token: 0x0400671E RID: 26398
		private readonly T2 item2;
	}
}
