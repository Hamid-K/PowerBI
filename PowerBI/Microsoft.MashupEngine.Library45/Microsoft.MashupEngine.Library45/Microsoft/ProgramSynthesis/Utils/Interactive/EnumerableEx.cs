using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace Microsoft.ProgramSynthesis.Utils.Interactive
{
	// Token: 0x02000682 RID: 1666
	public static class EnumerableEx
	{
		// Token: 0x060023B2 RID: 9138 RVA: 0x000647EA File Offset: 0x000629EA
		public static IEnumerable<IList<TSource>> Buffer<TSource>(this IEnumerable<TSource> source, int count)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (count <= 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			return source.Buffer_(count, count);
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x00064811 File Offset: 0x00062A11
		public static IEnumerable<IList<TSource>> Buffer<TSource>(this IEnumerable<TSource> source, int count, int skip)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (count <= 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (skip <= 0)
			{
				throw new ArgumentOutOfRangeException("skip");
			}
			return source.Buffer_(count, skip);
		}

		// Token: 0x060023B4 RID: 9140 RVA: 0x00064847 File Offset: 0x00062A47
		private static IEnumerable<IList<TSource>> Buffer_<TSource>(this IEnumerable<TSource> source, int count, int skip)
		{
			Queue<IList<TSource>> buffers = new Queue<IList<TSource>>();
			int i = 0;
			foreach (TSource tsource in source)
			{
				if (i % skip == 0)
				{
					buffers.Enqueue(new List<TSource>(count));
				}
				foreach (IList<TSource> list in buffers)
				{
					list.Add(tsource);
				}
				if (buffers.Count > 0 && buffers.Peek().Count == count)
				{
					yield return buffers.Dequeue();
				}
				int num = i;
				i = num + 1;
			}
			IEnumerator<TSource> enumerator = null;
			while (buffers.Count > 0)
			{
				yield return buffers.Dequeue();
			}
			yield break;
			yield break;
		}

		// Token: 0x060023B5 RID: 9141 RVA: 0x00064865 File Offset: 0x00062A65
		public static IEnumerable<TResult> Create<TResult>(Func<IEnumerator<TResult>> getEnumerator)
		{
			if (getEnumerator == null)
			{
				throw new ArgumentNullException("getEnumerator");
			}
			return new EnumerableEx.AnonymousEnumerable<TResult>(getEnumerator);
		}

		// Token: 0x060023B6 RID: 9142 RVA: 0x0006487B File Offset: 0x00062A7B
		public static IEnumerable<T> Create<T>(Action<IYielder<T>> create)
		{
			if (create == null)
			{
				throw new ArgumentNullException("create");
			}
			foreach (T t in new Yielder<T>(create))
			{
				yield return t;
			}
			Yielder<T> yielder = null;
			yield break;
			yield break;
		}

		// Token: 0x060023B7 RID: 9143 RVA: 0x0006488B File Offset: 0x00062A8B
		public static IEnumerable<TSource> Distinct<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			return source.Distinct_(keySelector, EqualityComparer<TKey>.Default);
		}

		// Token: 0x060023B8 RID: 9144 RVA: 0x000648B5 File Offset: 0x00062AB5
		public static IEnumerable<TSource> Distinct<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			return source.Distinct_(keySelector, comparer);
		}

		// Token: 0x060023B9 RID: 9145 RVA: 0x000648E9 File Offset: 0x00062AE9
		private static IEnumerable<TSource> Distinct_<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			HashSet<TKey> set = new HashSet<TKey>(comparer);
			foreach (TSource tsource in source)
			{
				TKey tkey = keySelector(tsource);
				if (set.Add(tkey))
				{
					yield return tsource;
				}
			}
			IEnumerator<TSource> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060023BA RID: 9146 RVA: 0x00064907 File Offset: 0x00062B07
		public static IEnumerable<TSource> DistinctUntilChanged<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return source.DistinctUntilChanged_((TSource x) => x, EqualityComparer<TSource>.Default);
		}

		// Token: 0x060023BB RID: 9147 RVA: 0x00064944 File Offset: 0x00062B44
		public static IEnumerable<TSource> DistinctUntilChanged<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			return source.DistinctUntilChanged_((TSource x) => x, comparer);
		}

		// Token: 0x060023BC RID: 9148 RVA: 0x00064993 File Offset: 0x00062B93
		public static IEnumerable<TSource> DistinctUntilChanged<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			return source.DistinctUntilChanged_(keySelector, EqualityComparer<TKey>.Default);
		}

		// Token: 0x060023BD RID: 9149 RVA: 0x000649BD File Offset: 0x00062BBD
		public static IEnumerable<TSource> DistinctUntilChanged<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			return source.DistinctUntilChanged_(keySelector, comparer);
		}

		// Token: 0x060023BE RID: 9150 RVA: 0x000649F1 File Offset: 0x00062BF1
		private static IEnumerable<TSource> DistinctUntilChanged_<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			TKey currentKey = default(TKey);
			bool hasCurrentKey = false;
			foreach (TSource tsource in source)
			{
				TKey tkey = keySelector(tsource);
				bool flag = false;
				if (hasCurrentKey)
				{
					flag = comparer.Equals(currentKey, tkey);
				}
				if (!hasCurrentKey || !flag)
				{
					hasCurrentKey = true;
					currentKey = tkey;
					yield return tsource;
				}
			}
			IEnumerator<TSource> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060023BF RID: 9151 RVA: 0x00064A0F File Offset: 0x00062C0F
		public static bool IsEmpty<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return !source.Any<TSource>();
		}

		// Token: 0x060023C0 RID: 9152 RVA: 0x00064A28 File Offset: 0x00062C28
		public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> onNext)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (onNext == null)
			{
				throw new ArgumentNullException("onNext");
			}
			foreach (TSource tsource in source)
			{
				onNext(tsource);
			}
		}

		// Token: 0x060023C1 RID: 9153 RVA: 0x00064A8C File Offset: 0x00062C8C
		public static void ForEach<TSource>(this TSource[] source, Action<TSource> onNext)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (onNext == null)
			{
				throw new ArgumentNullException("onNext");
			}
			int num = source.Length;
			for (int i = 0; i < num; i++)
			{
				onNext(source[i]);
			}
		}

		// Token: 0x060023C2 RID: 9154 RVA: 0x00064AD4 File Offset: 0x00062CD4
		public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource, int> onNext)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (onNext == null)
			{
				throw new ArgumentNullException("onNext");
			}
			int num = 0;
			foreach (TSource tsource in source)
			{
				onNext(tsource, checked(num++));
			}
		}

		// Token: 0x060023C3 RID: 9155 RVA: 0x00064B40 File Offset: 0x00062D40
		public static TSource Max<TSource>(this IEnumerable<TSource> source, IComparer<TSource> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			return source.MaxBy((TSource x) => x, comparer).First<TSource>();
		}

		// Token: 0x060023C4 RID: 9156 RVA: 0x00064B94 File Offset: 0x00062D94
		public static IList<TSource> MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			return source.MaxBy(keySelector, Comparer<TKey>.Default);
		}

		// Token: 0x060023C5 RID: 9157 RVA: 0x00064BC0 File Offset: 0x00062DC0
		public static IList<TSource> MaxBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			return EnumerableEx.ExtremaBy<TSource, TKey>(source, keySelector, (TKey key, TKey minValue) => comparer.Compare(key, minValue));
		}

		// Token: 0x060023C6 RID: 9158 RVA: 0x00064C1C File Offset: 0x00062E1C
		private static IList<TSource> ExtremaBy<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TKey, TKey, int> compare)
		{
			List<TSource> list = new List<TSource>();
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw new InvalidOperationException("Source sequence doesn't contain any elements.");
				}
				TSource tsource = enumerator.Current;
				TKey tkey = keySelector(tsource);
				list.Add(tsource);
				while (enumerator.MoveNext())
				{
					TSource tsource2 = enumerator.Current;
					TKey tkey2 = keySelector(tsource2);
					int num = compare(tkey2, tkey);
					if (num == 0)
					{
						list.Add(tsource2);
					}
					else if (num > 0)
					{
						list = new List<TSource> { tsource2 };
						tkey = tkey2;
					}
				}
			}
			return list;
		}

		// Token: 0x060023C7 RID: 9159 RVA: 0x00064CC8 File Offset: 0x00062EC8
		public static IBuffer<TSource> Memoize<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new EnumerableEx.MemoizedBuffer<TSource>(source.GetEnumerator());
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x00064CE4 File Offset: 0x00062EE4
		public static IEnumerable<TResult> Memoize<TSource, TResult>(this IEnumerable<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> selector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}
			return EnumerableEx.Create<TResult>(() => selector(source.Memoize<TSource>()).GetEnumerator());
		}

		// Token: 0x060023C9 RID: 9161 RVA: 0x00064D3A File Offset: 0x00062F3A
		public static IBuffer<TSource> Memoize<TSource>(this IEnumerable<TSource> source, int readerCount)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (readerCount <= 0)
			{
				throw new ArgumentOutOfRangeException("readerCount");
			}
			return new EnumerableEx.MemoizedBuffer<TSource>(source.GetEnumerator(), readerCount);
		}

		// Token: 0x060023CA RID: 9162 RVA: 0x00064D68 File Offset: 0x00062F68
		public static IEnumerable<TResult> Memoize<TSource, TResult>(this IEnumerable<TSource> source, int readerCount, Func<IEnumerable<TSource>, IEnumerable<TResult>> selector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (readerCount <= 0)
			{
				throw new ArgumentOutOfRangeException("readerCount");
			}
			if (selector == null)
			{
				throw new ArgumentNullException("selector");
			}
			return EnumerableEx.Create<TResult>(() => selector(source.Memoize(readerCount)).GetEnumerator());
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x00064DDC File Offset: 0x00062FDC
		public static TSource Min<TSource>(this IEnumerable<TSource> source, IComparer<TSource> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			return source.MinBy((TSource x) => x, comparer).First<TSource>();
		}

		// Token: 0x060023CC RID: 9164 RVA: 0x00064E30 File Offset: 0x00063030
		public static IList<TSource> MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			return source.MinBy(keySelector, Comparer<TKey>.Default);
		}

		// Token: 0x060023CD RID: 9165 RVA: 0x00064E5C File Offset: 0x0006305C
		public static IList<TSource> MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			if (comparer == null)
			{
				throw new ArgumentNullException("comparer");
			}
			return EnumerableEx.ExtremaBy<TSource, TKey>(source, keySelector, (TKey key, TKey minValue) => -comparer.Compare(key, minValue));
		}

		// Token: 0x060023CE RID: 9166 RVA: 0x00064EB8 File Offset: 0x000630B8
		public static IEnumerable<TResult> Repeat<TResult>(TResult value)
		{
			for (;;)
			{
				yield return value;
			}
			yield break;
		}

		// Token: 0x060023CF RID: 9167 RVA: 0x00064EC8 File Offset: 0x000630C8
		public static IEnumerable<TResult> Repeat<TResult>(TResult element, int count)
		{
			return Enumerable.Repeat<TResult>(element, count);
		}

		// Token: 0x060023D0 RID: 9168 RVA: 0x00064ED1 File Offset: 0x000630D1
		public static IEnumerable<TSource> Repeat<TSource>(this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return EnumerableEx.Repeat_<TSource>(source);
		}

		// Token: 0x060023D1 RID: 9169 RVA: 0x00064EE7 File Offset: 0x000630E7
		public static IEnumerable<TSource> Repeat<TSource>(this IEnumerable<TSource> source, int count)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			return EnumerableEx.Repeat_<TSource>(source, count);
		}

		// Token: 0x060023D2 RID: 9170 RVA: 0x00064F0D File Offset: 0x0006310D
		private static IEnumerable<TSource> Repeat_<TSource>(IEnumerable<TSource> source)
		{
			for (;;)
			{
				foreach (TSource tsource in source)
				{
					yield return tsource;
				}
				IEnumerator<TSource> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x060023D3 RID: 9171 RVA: 0x00064F1D File Offset: 0x0006311D
		private static IEnumerable<TSource> Repeat_<TSource>(IEnumerable<TSource> source, int count)
		{
			int num;
			for (int i = 0; i < count; i = num + 1)
			{
				foreach (TSource tsource in source)
				{
					yield return tsource;
				}
				IEnumerator<TSource> enumerator = null;
				num = i;
			}
			yield break;
			yield break;
		}

		// Token: 0x060023D4 RID: 9172 RVA: 0x00064F34 File Offset: 0x00063134
		public static IEnumerable<TSource> SkipLast<TSource>(this IEnumerable<TSource> source, int count)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			return source.SkipLast_(count);
		}

		// Token: 0x060023D5 RID: 9173 RVA: 0x00064F5A File Offset: 0x0006315A
		private static IEnumerable<TSource> SkipLast_<TSource>(this IEnumerable<TSource> source, int count)
		{
			Queue<TSource> q = new Queue<TSource>();
			foreach (TSource tsource in source)
			{
				q.Enqueue(tsource);
				if (q.Count > count)
				{
					yield return q.Dequeue();
				}
			}
			IEnumerator<TSource> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060023D6 RID: 9174 RVA: 0x00064F71 File Offset: 0x00063171
		public static IEnumerable<TSource> TakeLast<TSource>(this IEnumerable<TSource> source, int count)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			return source.TakeLast_(count);
		}

		// Token: 0x060023D7 RID: 9175 RVA: 0x00064F97 File Offset: 0x00063197
		private static IEnumerable<TSource> TakeLast_<TSource>(this IEnumerable<TSource> source, int count)
		{
			if (count == 0)
			{
				yield break;
			}
			Queue<TSource> q = new Queue<TSource>(count);
			using (IEnumerator<TSource> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TSource tsource = enumerator.Current;
					if (q.Count >= count)
					{
						q.Dequeue();
					}
					q.Enqueue(tsource);
				}
				goto IL_00AA;
			}
			IL_0089:
			yield return q.Dequeue();
			IL_00AA:
			if (q.Count <= 0)
			{
				yield break;
			}
			goto IL_0089;
		}

		// Token: 0x02000683 RID: 1667
		private class AnonymousEnumerable<TResult> : IEnumerable<TResult>, IEnumerable
		{
			// Token: 0x060023D8 RID: 9176 RVA: 0x00064FAE File Offset: 0x000631AE
			public AnonymousEnumerable(Func<IEnumerator<TResult>> getEnumerator)
			{
				this._getEnumerator = getEnumerator;
			}

			// Token: 0x060023D9 RID: 9177 RVA: 0x00064FBD File Offset: 0x000631BD
			public IEnumerator<TResult> GetEnumerator()
			{
				return this._getEnumerator();
			}

			// Token: 0x060023DA RID: 9178 RVA: 0x00064FCA File Offset: 0x000631CA
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x040010F8 RID: 4344
			private readonly Func<IEnumerator<TResult>> _getEnumerator;
		}

		// Token: 0x02000684 RID: 1668
		private class MemoizedBuffer<T> : IBuffer<T>, IEnumerable<T>, IEnumerable, IDisposable
		{
			// Token: 0x060023DB RID: 9179 RVA: 0x00064FD2 File Offset: 0x000631D2
			public MemoizedBuffer(IEnumerator<T> source)
				: this(source, new MaxRefCountList<T>())
			{
			}

			// Token: 0x060023DC RID: 9180 RVA: 0x00064FE0 File Offset: 0x000631E0
			public MemoizedBuffer(IEnumerator<T> source, int readerCount)
				: this(source, new RefCountList<T>(readerCount))
			{
			}

			// Token: 0x060023DD RID: 9181 RVA: 0x00064FEF File Offset: 0x000631EF
			private MemoizedBuffer(IEnumerator<T> source, IRefCountList<T> buffer)
			{
				this._source = source;
				this._buffer = buffer;
			}

			// Token: 0x060023DE RID: 9182 RVA: 0x00065005 File Offset: 0x00063205
			public IEnumerator<T> GetEnumerator()
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("");
				}
				return this.GetEnumerator_();
			}

			// Token: 0x060023DF RID: 9183 RVA: 0x00065020 File Offset: 0x00063220
			IEnumerator IEnumerable.GetEnumerator()
			{
				if (this._disposed)
				{
					throw new ObjectDisposedException("");
				}
				return this.GetEnumerator();
			}

			// Token: 0x060023E0 RID: 9184 RVA: 0x0006503C File Offset: 0x0006323C
			public void Dispose()
			{
				IEnumerator<T> source = this._source;
				lock (source)
				{
					if (!this._disposed)
					{
						this._source.Dispose();
						this._source = null;
						this._buffer.Clear();
						this._buffer = null;
					}
					this._disposed = true;
				}
			}

			// Token: 0x060023E1 RID: 9185 RVA: 0x000650AC File Offset: 0x000632AC
			private IEnumerator<T> GetEnumerator_()
			{
				int i = 0;
				try
				{
					while (!this._disposed)
					{
						bool flag = false;
						T t = default(T);
						IEnumerator<T> source = this._source;
						lock (source)
						{
							if (i >= this._buffer.Count)
							{
								if (!this._stopped)
								{
									try
									{
										flag = this._source.MoveNext();
										if (flag)
										{
											t = this._source.Current;
										}
									}
									catch (Exception ex)
									{
										this._stopped = true;
										this._error = ExceptionDispatchInfo.Capture(ex);
										this._source.Dispose();
									}
								}
								if (this._stopped)
								{
									if (this._error == null)
									{
										goto IL_0147;
									}
									this._error.Throw();
								}
								if (flag)
								{
									this._buffer.Add(t);
								}
							}
							else
							{
								flag = true;
							}
						}
						if (flag)
						{
							yield return this._buffer[i];
							int num = i;
							i = num + 1;
							continue;
						}
						IL_0147:
						goto JumpOutOfTryFinally-3;
					}
					throw new ObjectDisposedException("");
				}
				finally
				{
					if (this._buffer != null)
					{
						this._buffer.Done(i + 1);
					}
				}
				JumpOutOfTryFinally-3:
				yield break;
				yield break;
			}

			// Token: 0x040010F9 RID: 4345
			private IRefCountList<T> _buffer;

			// Token: 0x040010FA RID: 4346
			private bool _disposed;

			// Token: 0x040010FB RID: 4347
			private ExceptionDispatchInfo _error;

			// Token: 0x040010FC RID: 4348
			private IEnumerator<T> _source;

			// Token: 0x040010FD RID: 4349
			private bool _stopped;
		}
	}
}
