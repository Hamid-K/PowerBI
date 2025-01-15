using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils.Caching;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Util
{
	// Token: 0x02001ADA RID: 6874
	internal class DataQualityComputer
	{
		// Token: 0x0600E329 RID: 58153 RVA: 0x003035A0 File Offset: 0x003017A0
		public DataQualityComputer()
		{
			this._tableCache.Clear();
			this._dataCountCache.Clear();
			this._missingnessCache.Clear();
			this._stringnessCache.Clear();
			this._spreadCache.Clear();
			this._dataTypeIsDoubleCache.Clear();
			this._dataTypeIsIntCache.Clear();
			this._correlationsCache.Clear();
			this._indexnessCache.Clear();
			this._splitnessCache.Clear();
			this._dtypesVariabilityCache.Clear();
		}

		// Token: 0x0600E32A RID: 58154 RVA: 0x00303700 File Offset: 0x00301900
		public DataQuality GetDataQuality(Table<object> table)
		{
			DataQualityComputer.<>c__DisplayClass12_0 CS$<>8__locals1 = new DataQualityComputer.<>c__DisplayClass12_0();
			CS$<>8__locals1.table = table;
			CS$<>8__locals1.<>4__this = this;
			return this._tableCache.GetOrAdd(CS$<>8__locals1.table.GetHashCode(), delegate(int _)
			{
				DataQualityComputer.<>c__DisplayClass12_1 CS$<>8__locals2 = new DataQualityComputer.<>c__DisplayClass12_1();
				CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
				DataQuality dataQuality = new DataQuality();
				int num = CS$<>8__locals1.table.Rows.Count<IEnumerable<object>>() * CS$<>8__locals1.table.ColumnNames.Count<string>();
				if (num == 0)
				{
					return dataQuality;
				}
				DataQuality dataQuality2 = dataQuality;
				IEnumerable<string> columnNames = CS$<>8__locals1.table.ColumnNames;
				Func<string, double> func;
				if ((func = CS$<>8__locals1.<>9__1) == null)
				{
					func = (CS$<>8__locals1.<>9__1 = (string columnName) => CS$<>8__locals1.<>4__this._missingnessCache.GetOrAdd(CS$<>8__locals1.table.HashedColumn(columnName), (int _) => (double)CS$<>8__locals1.table.Column(columnName).Count((object d) => Utilities.IsMissing(d))));
				}
				dataQuality2.Missingness = columnNames.Sum(func) / (double)num;
				DataQuality dataQuality3 = dataQuality;
				IEnumerable<string> columnNames2 = CS$<>8__locals1.table.ColumnNames;
				Func<string, double> func2;
				if ((func2 = CS$<>8__locals1.<>9__2) == null)
				{
					func2 = (CS$<>8__locals1.<>9__2 = (string columnName) => CS$<>8__locals1.<>4__this._stringnessCache.GetOrAdd(CS$<>8__locals1.table.HashedColumn(columnName), (int _) => (double)CS$<>8__locals1.table.Column(columnName).Count((object d) => d is string)));
				}
				dataQuality3.Stringness = columnNames2.Sum(func2) / (double)num;
				DataQuality dataQuality4 = dataQuality;
				double num2 = 1.0;
				IEnumerable<string> columnNames3 = CS$<>8__locals1.table.ColumnNames;
				Func<string, double> func3;
				if ((func3 = CS$<>8__locals1.<>9__3) == null)
				{
					func3 = (CS$<>8__locals1.<>9__3 = delegate(string columnName)
					{
						Func<int, bool> <>9__13;
						Func<int, bool> <>9__14;
						return CS$<>8__locals1.<>4__this._spreadCache.GetOrAdd(CS$<>8__locals1.table.HashedColumn(columnName), delegate(int _)
						{
							IEnumerable<object> enumerable = from d in CS$<>8__locals1.table.Column(columnName)
								where d != null
								select d;
							if (!enumerable.Any<object>())
							{
								return 0.0;
							}
							CloneableCache<int, bool, LruCache<int, bool>> dataTypeIsIntCache = CS$<>8__locals1.<>4__this._dataTypeIsIntCache;
							int num5 = CS$<>8__locals1.table.HashedColumn(columnName);
							Func<int, bool> func6;
							if ((func6 = <>9__13) == null)
							{
								func6 = (<>9__13 = (int _) => Utilities.IsInteger(CS$<>8__locals1.table.Column(columnName), true));
							}
							if (dataTypeIsIntCache.GetOrAdd(num5, func6))
							{
								IEnumerable<long> enumerable2 = enumerable.Select(delegate(object d)
								{
									if (d is int)
									{
										int num7 = (int)d;
										return (long)num7;
									}
									return Convert.ToInt64(d);
								});
								return (double)enumerable2.Distinct<long>().Count<long>() / (double)enumerable2.Count<long>();
							}
							CloneableCache<int, bool, LruCache<int, bool>> dataTypeIsDoubleCache = CS$<>8__locals1.<>4__this._dataTypeIsDoubleCache;
							int num6 = CS$<>8__locals1.table.HashedColumn(columnName);
							Func<int, bool> func7;
							if ((func7 = <>9__14) == null)
							{
								func7 = (<>9__14 = (int _) => Utilities.IsDouble(CS$<>8__locals1.table.Column(columnName), true));
							}
							if (!dataTypeIsDoubleCache.GetOrAdd(num6, func7))
							{
								return (double)enumerable.Distinct<object>().Count<object>() / (double)enumerable.Count<object>();
							}
							IEnumerable<double> enumerable3 = enumerable.Select(delegate(object d)
							{
								if (d is double)
								{
									return (double)d;
								}
								return Convert.ToDouble(d);
							});
							if (Math.Abs(enumerable3.Max() - enumerable3.Min()) < 1E-06)
							{
								return 0.0;
							}
							double avg = enumerable3.Average();
							return Math.Min(1.0, Math.Sqrt(enumerable3.Select((double d) => (d - avg) * (d - avg)).Average()) / (enumerable3.Max() - enumerable3.Min()));
						});
					});
				}
				dataQuality4.Concentration = num2 - columnNames3.Select(func3).Average();
				List<double> list = new List<double>();
				foreach (int num3 in Enumerable.Range(0, CS$<>8__locals1.table.ColumnNames.Count<string>() - 1))
				{
					foreach (int num4 in Enumerable.Range(num3 + 1, CS$<>8__locals1.table.ColumnNames.Count<string>() - num3 - 2))
					{
						string column1Name = CS$<>8__locals1.table.ColumnNames.ElementAt(num3);
						string column2Name = CS$<>8__locals1.table.ColumnNames.ElementAt(num3);
						Func<int, bool> <>9__19;
						Func<int, bool> <>9__20;
						Func<int, int> <>9__27;
						double orAdd = CS$<>8__locals1.<>4__this._correlationsCache.GetOrAdd(new Tuple<int, int>(CS$<>8__locals1.table.HashedColumn(column1Name), CS$<>8__locals1.table.HashedColumn(column2Name)), delegate(Tuple<int, int> _)
						{
							CloneableCache<int, bool, LruCache<int, bool>> dataTypeIsDoubleCache2 = CS$<>8__locals2.CS$<>8__locals1.<>4__this._dataTypeIsDoubleCache;
							int num8 = CS$<>8__locals2.CS$<>8__locals1.table.HashedColumn(column1Name);
							Func<int, bool> func8;
							if ((func8 = <>9__19) == null)
							{
								func8 = (<>9__19 = (int _) => Utilities.IsDouble(CS$<>8__locals2.CS$<>8__locals1.table.Column(column1Name), true));
							}
							bool orAdd2 = dataTypeIsDoubleCache2.GetOrAdd(num8, func8);
							CloneableCache<int, bool, LruCache<int, bool>> dataTypeIsDoubleCache3 = CS$<>8__locals2.CS$<>8__locals1.<>4__this._dataTypeIsDoubleCache;
							int num9 = CS$<>8__locals2.CS$<>8__locals1.table.HashedColumn(column2Name);
							Func<int, bool> func9;
							if ((func9 = <>9__20) == null)
							{
								func9 = (<>9__20 = (int _) => Utilities.IsDouble(CS$<>8__locals2.CS$<>8__locals1.table.Column(column2Name), true));
							}
							bool orAdd3 = dataTypeIsDoubleCache3.GetOrAdd(num9, func9);
							if (orAdd2 && orAdd3)
							{
								IEnumerable<double> enumerable4 = CS$<>8__locals2.CS$<>8__locals1.table.Column(column1Name).Select(delegate(object d)
								{
									if (d == null)
									{
										return 0.0;
									}
									if (d is double)
									{
										return (double)d;
									}
									return Convert.ToDouble(d);
								});
								IEnumerable<double> enumerable5 = CS$<>8__locals2.CS$<>8__locals1.table.Column(column2Name).Select(delegate(object d)
								{
									if (d == null)
									{
										return 0.0;
									}
									if (d is double)
									{
										return (double)d;
									}
									return Convert.ToDouble(d);
								});
								double avg1 = enumerable4.Average();
								double avg2 = enumerable5.Average();
								double num10 = enumerable4.Zip(enumerable5, (double x, double y) => (x - avg1) * (y - avg2)).Sum();
								double num11 = enumerable4.Sum((double x) => (x - avg1) * (x - avg1));
								double num12 = enumerable5.Sum((double y) => (y - avg2) * (y - avg2));
								double num13 = 0.0;
								if (num11 <= 1E-09 && num12 <= 1E-09)
								{
									num13 = 1.0;
								}
								else if (num11 > 1E-09 && num12 > 1E-09)
								{
									num13 = Math.Abs(num10 / Math.Sqrt(num11 * num12));
								}
								return num13;
							}
							if (!orAdd2 && !orAdd3)
							{
								double num14 = (double)CS$<>8__locals2.CS$<>8__locals1.table.Column(column1Name).Zip(CS$<>8__locals2.CS$<>8__locals1.table.Column(column2Name), delegate(object first, object second)
								{
									if (first != null && second != null && first.ToString().Equals(second.ToString()))
									{
										return 1;
									}
									if (first != null || second != null)
									{
										return 0;
									}
									return 1;
								}).Sum();
								CloneableCache<int, int, LruCache<int, int>> dataCountCache = CS$<>8__locals2.CS$<>8__locals1.<>4__this._dataCountCache;
								int num15 = CS$<>8__locals2.CS$<>8__locals1.table.HashedColumn(column1Name);
								Func<int, int> func10;
								if ((func10 = <>9__27) == null)
								{
									func10 = (<>9__27 = (int _) => CS$<>8__locals2.CS$<>8__locals1.table.Column(column1Name).Count<object>());
								}
								return num14 / (double)dataCountCache.GetOrAdd(num15, func10);
							}
							return 0.0;
						});
						list.Add(orAdd);
					}
				}
				dataQuality.Redundancy = (list.Any<double>() ? list.Average() : 0.0);
				DataQualityComputer.<>c__DisplayClass12_1 CS$<>8__locals4 = CS$<>8__locals2;
				NumPrefixRowsMetadata numPrefixRowsMetadata = CS$<>8__locals1.table.Metadata.OfType<NumPrefixRowsMetadata>().FirstOrDefault<NumPrefixRowsMetadata>();
				CS$<>8__locals4.prefix = ((numPrefixRowsMetadata != null) ? numPrefixRowsMetadata.NumPrefixRows : 0);
				dataQuality.Indexness = CS$<>8__locals1.table.ColumnNames.Select((string columnName) => CS$<>8__locals2.CS$<>8__locals1.<>4__this._indexnessCache.GetOrAdd(new Tuple<int, int>(CS$<>8__locals2.CS$<>8__locals1.table.HashedColumn(columnName), (CS$<>8__locals2.prefix > 0) ? CS$<>8__locals2.prefix : CS$<>8__locals2.CS$<>8__locals1.<>4__this._dataCountCache.GetOrAdd(CS$<>8__locals2.CS$<>8__locals1.table.HashedColumn(columnName), (int _) => CS$<>8__locals2.CS$<>8__locals1.table.Column(columnName).Count<object>())), (Tuple<int, int> _) => DataQualityComputer.GetIndexness(CS$<>8__locals2.CS$<>8__locals1.table.Column(columnName), CS$<>8__locals2.prefix))).Average();
				DataQuality dataQuality5 = dataQuality;
				IEnumerable<string> columnNames4 = CS$<>8__locals1.table.ColumnNames;
				Func<string, double> func4;
				if ((func4 = CS$<>8__locals1.<>9__5) == null)
				{
					func4 = (CS$<>8__locals1.<>9__5 = (string columnName) => CS$<>8__locals1.<>4__this._splitnessCache.GetOrAdd(CS$<>8__locals1.table.HashedColumn(columnName), (int _) => DataQualityComputer.GetSplitness(CS$<>8__locals1.table.Column(columnName))));
				}
				dataQuality5.Splitness = columnNames4.Select(func4).Average();
				DataQuality dataQuality6 = dataQuality;
				IEnumerable<string> columnNames5 = CS$<>8__locals1.table.ColumnNames;
				Func<string, double> func5;
				if ((func5 = CS$<>8__locals1.<>9__6) == null)
				{
					func5 = (CS$<>8__locals1.<>9__6 = delegate(string columnName)
					{
						Func<int, int> <>9__33;
						return CS$<>8__locals1.<>4__this._dtypesVariabilityCache.GetOrAdd(CS$<>8__locals1.table.HashedColumn(columnName), delegate(int _)
						{
							double num16 = (double)CS$<>8__locals1.table.Column(columnName).Count((object d) => d != null && Utilities.IsNumeric(d));
							CloneableCache<int, int, LruCache<int, int>> dataCountCache2 = CS$<>8__locals1.<>4__this._dataCountCache;
							int num17 = CS$<>8__locals1.table.HashedColumn(columnName);
							Func<int, int> func11;
							if ((func11 = <>9__33) == null)
							{
								func11 = (<>9__33 = (int _) => CS$<>8__locals1.table.Column(columnName).Count<object>());
							}
							double num18 = num16 / (double)dataCountCache2.GetOrAdd(num17, func11);
							return Math.Min(num18, 1.0 - num18) / 0.5;
						});
					});
				}
				dataQuality6.DtypesVariability = columnNames5.Select(func5).Average();
				return dataQuality;
			});
		}

		// Token: 0x0600E32B RID: 58155 RVA: 0x00303744 File Offset: 0x00301944
		internal static double GetIndexness(IEnumerable<object> data, int numPrefixRows)
		{
			data = ((numPrefixRows > 0) ? data.Take(numPrefixRows) : data);
			if (data.Skip(1).Any<object>() && Utilities.IsInteger(data, false) && data.Distinct<object>().Count<object>() == data.Count<object>())
			{
				IEnumerable<object> enumerable = data;
				Func<object, long> func;
				if ((func = DataQualityComputer.<>O.<0>__ToInt64) == null)
				{
					func = (DataQualityComputer.<>O.<0>__ToInt64 = new Func<object, long>(Convert.ToInt64));
				}
				if (!(from s in enumerable.Select(func)
					orderby s
					select s).Select((long v, int i) => v - (long)i).Distinct<long>().Skip(1)
					.Any<long>())
				{
					return 1.0;
				}
			}
			return 0.0;
		}

		// Token: 0x0600E32C RID: 58156 RVA: 0x00303824 File Offset: 0x00301A24
		internal static double GetSplitness(IEnumerable<object> data)
		{
			if (!Utilities.IsDouble(data, true))
			{
				data = data.Where((object d) => d != null && d is string);
				if (data.Any<object>() && DataQualityComputer._delimitersList.Any((char delim) => data.All((object d) => d.ToString().Contains(delim.ToString()))))
				{
					return 1.0;
				}
			}
			return 0.0;
		}

		// Token: 0x040055C9 RID: 21961
		public readonly ConcurrentLruCache<int, DataQuality> _tableCache = new ConcurrentLruCache<int, DataQuality>(4096, null, null, null);

		// Token: 0x040055CA RID: 21962
		public readonly ConcurrentLruCache<int, int> _dataCountCache = new ConcurrentLruCache<int, int>(4096, null, null, null);

		// Token: 0x040055CB RID: 21963
		public readonly ConcurrentLruCache<int, double> _missingnessCache = new ConcurrentLruCache<int, double>(4096, null, null, null);

		// Token: 0x040055CC RID: 21964
		public readonly ConcurrentLruCache<int, double> _stringnessCache = new ConcurrentLruCache<int, double>(4096, null, null, null);

		// Token: 0x040055CD RID: 21965
		public readonly ConcurrentLruCache<int, double> _spreadCache = new ConcurrentLruCache<int, double>(4096, null, null, null);

		// Token: 0x040055CE RID: 21966
		public readonly ConcurrentLruCache<int, bool> _dataTypeIsDoubleCache = new ConcurrentLruCache<int, bool>(4096, null, null, null);

		// Token: 0x040055CF RID: 21967
		public readonly ConcurrentLruCache<int, bool> _dataTypeIsIntCache = new ConcurrentLruCache<int, bool>(4096, null, null, null);

		// Token: 0x040055D0 RID: 21968
		public readonly ConcurrentLruCache<Tuple<int, int>, double> _correlationsCache = new ConcurrentLruCache<Tuple<int, int>, double>(4096, null, null, null);

		// Token: 0x040055D1 RID: 21969
		public readonly ConcurrentLruCache<Tuple<int, int>, double> _indexnessCache = new ConcurrentLruCache<Tuple<int, int>, double>(4096, null, null, null);

		// Token: 0x040055D2 RID: 21970
		public readonly ConcurrentLruCache<int, double> _splitnessCache = new ConcurrentLruCache<int, double>(4096, null, null, null);

		// Token: 0x040055D3 RID: 21971
		public readonly ConcurrentLruCache<int, double> _dtypesVariabilityCache = new ConcurrentLruCache<int, double>(4096, null, null, null);

		// Token: 0x040055D4 RID: 21972
		private static string _delimitersList = " _;-,:!@#$%&*().,";

		// Token: 0x02001ADB RID: 6875
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040055D5 RID: 21973
			public static Func<object, long> <0>__ToInt64;
		}
	}
}
