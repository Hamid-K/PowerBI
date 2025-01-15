using System;
using System.Collections.Generic;
using Microsoft.Internal;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x02001837 RID: 6199
	public class TableDistinct
	{
		// Token: 0x06009D12 RID: 40210 RVA: 0x002074CD File Offset: 0x002056CD
		public TableDistinct(Distinct[] distincts)
		{
			this.distincts = distincts;
		}

		// Token: 0x17002882 RID: 10370
		// (get) Token: 0x06009D13 RID: 40211 RVA: 0x002074DC File Offset: 0x002056DC
		public Distinct[] Distincts
		{
			get
			{
				return this.distincts;
			}
		}

		// Token: 0x06009D14 RID: 40212 RVA: 0x002074E4 File Offset: 0x002056E4
		public bool TryGetColumns(RecordTypeValue rowType, out int[] distinctColumns)
		{
			HashSet<int> hashSet = new HashSet<int>();
			for (int i = 0; i < this.distincts.Length; i++)
			{
				Distinct distinct = this.distincts[i];
				if (distinct.Selector == null)
				{
					distinctColumns = null;
					return false;
				}
				int num;
				if (!QueryExpressionBuilder.ToQueryExpression(rowType, distinct.Selector).TryGetColumnAccess(out num) || distinct.Comparer != null)
				{
					distinctColumns = null;
					return false;
				}
				hashSet.Add(num);
			}
			distinctColumns = hashSet.ToArray<int>();
			return true;
		}

		// Token: 0x06009D15 RID: 40213 RVA: 0x0020755C File Offset: 0x0020575C
		public bool AllColumns(RecordTypeValue rowType)
		{
			int[] array;
			return this.distincts.Length == rowType.FieldKeys.Length && this.TryGetColumns(rowType, out array) && array.Length == rowType.FieldKeys.Length;
		}

		// Token: 0x06009D16 RID: 40214 RVA: 0x0020759C File Offset: 0x0020579C
		public IEqualityComparer<Value> ToComparer(RecordTypeValue rowType, out Func<Value, Value> keyTransformer)
		{
			int[] array;
			if (this.TryGetColumns(rowType, out array) && array.Length < rowType.FieldKeys.Length / 2)
			{
				Distinct[] array2 = new Distinct[array.Length];
				ColumnSelectionBuilder columnSelectionBuilder = default(ColumnSelectionBuilder);
				foreach (int num in array)
				{
					columnSelectionBuilder.Add(rowType.FieldKeys[num], num);
				}
				ColumnSelection selection = columnSelectionBuilder.ToColumnSelection();
				for (int j = 0; j < array.Length; j++)
				{
					array2[j] = new Distinct(QueryExpressionAssembler.Assemble(selection.Keys, new ColumnAccessQueryExpression(j)), null);
				}
				keyTransformer = (Value v) => ProjectColumnsQuery.Project(v.AsRecord, selection);
				return TableDistinct.ToComparer(array2);
			}
			keyTransformer = (Value v) => v;
			return this.ToComparer();
		}

		// Token: 0x06009D17 RID: 40215 RVA: 0x00207696 File Offset: 0x00205896
		public IEqualityComparer<Value> ToComparer()
		{
			return TableDistinct.ToComparer(this.distincts);
		}

		// Token: 0x06009D18 RID: 40216 RVA: 0x002076A4 File Offset: 0x002058A4
		private static IEqualityComparer<Value> ToComparer(Distinct[] distincts)
		{
			if (distincts.Length == 1)
			{
				return TableDistinct.GetComparer(distincts[0]);
			}
			IEqualityComparer<Value>[] array = new IEqualityComparer<Value>[distincts.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = TableDistinct.GetComparer(distincts[i]);
			}
			return new TableDistinct.ComparersComparer(array);
		}

		// Token: 0x06009D19 RID: 40217 RVA: 0x002076F0 File Offset: 0x002058F0
		private static IEqualityComparer<Value> GetComparer(Distinct distinct)
		{
			return TableDistinct.GetComparer(distinct.Selector, distinct.Comparer);
		}

		// Token: 0x06009D1A RID: 40218 RVA: 0x00207705 File Offset: 0x00205905
		private static IEqualityComparer<Value> GetComparer(FunctionValue selector, IEqualityComparer<Value> comparer)
		{
			if (selector == null)
			{
				return comparer;
			}
			if (comparer == null)
			{
				return new TableDistinct.SelectorComparer(selector);
			}
			return new TableDistinct.SelectorComparerComparer(selector, comparer);
		}

		// Token: 0x0400529A RID: 21146
		private Distinct[] distincts;

		// Token: 0x02001838 RID: 6200
		private class SelectorComparer : IEqualityComparer<Value>
		{
			// Token: 0x06009D1B RID: 40219 RVA: 0x0020771D File Offset: 0x0020591D
			public SelectorComparer(FunctionValue selector)
			{
				this.selector = selector;
			}

			// Token: 0x06009D1C RID: 40220 RVA: 0x0020772C File Offset: 0x0020592C
			public bool Equals(Value x, Value y)
			{
				return this.selector.Invoke(x).Equals(this.selector.Invoke(y));
			}

			// Token: 0x06009D1D RID: 40221 RVA: 0x0020774B File Offset: 0x0020594B
			public int GetHashCode(Value obj)
			{
				return this.selector.Invoke(obj).GetHashCode();
			}

			// Token: 0x0400529B RID: 21147
			private FunctionValue selector;
		}

		// Token: 0x02001839 RID: 6201
		private class SelectorComparerComparer : IEqualityComparer<Value>
		{
			// Token: 0x06009D1E RID: 40222 RVA: 0x0020775E File Offset: 0x0020595E
			public SelectorComparerComparer(FunctionValue selector, IEqualityComparer<Value> comparer)
			{
				this.selector = selector;
				this.comparer = comparer;
			}

			// Token: 0x06009D1F RID: 40223 RVA: 0x00207774 File Offset: 0x00205974
			public bool Equals(Value x, Value y)
			{
				return this.comparer.Equals(this.selector.Invoke(x), this.selector.Invoke(y));
			}

			// Token: 0x06009D20 RID: 40224 RVA: 0x00207799 File Offset: 0x00205999
			public int GetHashCode(Value obj)
			{
				return this.comparer.GetHashCode(this.selector.Invoke(obj));
			}

			// Token: 0x0400529C RID: 21148
			private FunctionValue selector;

			// Token: 0x0400529D RID: 21149
			private IEqualityComparer<Value> comparer;
		}

		// Token: 0x0200183A RID: 6202
		private class ComparersComparer : IEqualityComparer<Value>
		{
			// Token: 0x06009D21 RID: 40225 RVA: 0x002077B2 File Offset: 0x002059B2
			public ComparersComparer(IEqualityComparer<Value>[] comparers)
			{
				this.comparers = comparers;
			}

			// Token: 0x06009D22 RID: 40226 RVA: 0x002077C4 File Offset: 0x002059C4
			public bool Equals(Value x, Value y)
			{
				for (int i = 0; i < this.comparers.Length; i++)
				{
					if (!this.comparers[i].Equals(x, y))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06009D23 RID: 40227 RVA: 0x002077F8 File Offset: 0x002059F8
			public int GetHashCode(Value obj)
			{
				int num = 0;
				for (int i = 0; i < this.comparers.Length; i++)
				{
					num += this.comparers[i].GetHashCode(obj);
					num += num << 10;
					num ^= num >> 6;
				}
				return num;
			}

			// Token: 0x0400529E RID: 21150
			private IEqualityComparer<Value>[] comparers;
		}
	}
}
