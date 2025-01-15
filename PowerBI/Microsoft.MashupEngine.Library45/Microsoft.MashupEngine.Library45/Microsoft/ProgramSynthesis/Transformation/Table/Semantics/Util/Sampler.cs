using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Util
{
	// Token: 0x02001AEA RID: 6890
	internal static class Sampler
	{
		// Token: 0x0600E364 RID: 58212 RVA: 0x003046F0 File Offset: 0x003028F0
		private static IReadOnlyList<T> RandomlySampleListFromEnumerable<T>(this IEnumerable<T> xs, Random random, int length, int nonRandomPrefixLength = 0)
		{
			if (nonRandomPrefixLength > length)
			{
				throw new ArgumentException("Non-random prefix may not be longer than the entire output.", "nonRandomPrefixLength");
			}
			List<T> list = new List<T>(length);
			foreach (Record<int, T> record in xs.Enumerate<T>())
			{
				int num;
				T t;
				record.Deconstruct(out num, out t);
				int num2 = num;
				T t2 = t;
				if (num2 < length)
				{
					list.Add(t2);
				}
				else
				{
					int num3 = random.Next(nonRandomPrefixLength, num2 + 1);
					if (num3 < length)
					{
						list[num3] = t2;
					}
				}
			}
			return list;
		}

		// Token: 0x0600E365 RID: 58213 RVA: 0x00304788 File Offset: 0x00302988
		public static Table<object> SampleFromITable(ITable<object> table, int? numRowsToConsider = null, int? maxRowsToSample = null, bool prefix = true, bool randomSample = false, int seed = 0)
		{
			if ((numRowsToConsider == null && maxRowsToSample == null && randomSample) || !table.Rows.Any<IEnumerable<object>>())
			{
				return new Table<object>(table.ColumnNames, table.Rows, null);
			}
			if (!prefix && !randomSample)
			{
				return null;
			}
			if (maxRowsToSample == null && randomSample)
			{
				return null;
			}
			if (maxRowsToSample != null)
			{
				int valueOrDefault = maxRowsToSample.GetValueOrDefault();
				IEnumerable<IEnumerable<object>> enumerable;
				if (numRowsToConsider != null)
				{
					int valueOrDefault2 = numRowsToConsider.GetValueOrDefault();
					enumerable = table.Rows.Take(valueOrDefault2);
				}
				else
				{
					enumerable = table.Rows;
				}
				IEnumerable<IReadOnlyList<object>> enumerable2 = enumerable.Select((IEnumerable<object> row) => row.ToList<object>());
				int num = (randomSample ? (prefix ? (valueOrDefault / 2) : 0) : valueOrDefault);
				IReadOnlyList<IReadOnlyList<object>> readOnlyList2;
				if (!randomSample)
				{
					IReadOnlyList<IReadOnlyList<object>> readOnlyList = enumerable2.Take(valueOrDefault).ToList<IReadOnlyList<object>>();
					readOnlyList2 = readOnlyList;
				}
				else
				{
					readOnlyList2 = enumerable2.RandomlySampleListFromEnumerable(new Random(seed), valueOrDefault, num);
				}
				IReadOnlyList<IReadOnlyList<object>> readOnlyList3 = readOnlyList2;
				num = Math.Min(num, readOnlyList3.Count);
				int longestRow = readOnlyList3.Max((IReadOnlyList<object> row) => row.Count);
				return new Table<object>(table.ColumnNames.ExtendToLength(longestRow, (int j) => string.Format("Column{0}", j + 1)), readOnlyList3.Select((IReadOnlyList<object> row) => row.ExtendToLength(longestRow, string.Empty).ToList<object>()).ToList<List<object>>(), table.Metadata.Where((ITableMetadata m) => !(m is NumPrefixRowsMetadata)).AppendItem(new NumPrefixRowsMetadata
				{
					NumPrefixRows = num
				}).ToList<ITableMetadata>());
			}
			return new Table<object>(table.ColumnNames, table.Rows, table.Metadata);
		}
	}
}
