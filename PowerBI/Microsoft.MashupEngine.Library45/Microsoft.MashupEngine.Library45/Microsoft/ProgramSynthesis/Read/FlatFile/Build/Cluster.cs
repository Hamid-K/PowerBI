using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Read.FlatFile.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Build
{
	// Token: 0x02001276 RID: 4726
	public static class Cluster
	{
		// Token: 0x06008E93 RID: 36499 RVA: 0x000AB0AF File Offset: 0x000A92AF
		private static Optional<T> CastValue<T>(object obj)
		{
			if (!(obj is Bottom))
			{
				return ((T)((object)obj)).Some<T>();
			}
			return Optional<T>.Nothing;
		}

		// Token: 0x06008E94 RID: 36500 RVA: 0x001E0E5B File Offset: 0x001DF05B
		public static IEnumerable<KeyValuePair<Optional<IReadOnlyList<string>>, ProgramSetBuilder<columnNames>>> ClusterOnInput(this ProgramSetBuilder<columnNames> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IReadOnlyList<string>>, ProgramSetBuilder<columnNames>>(Cluster.CastValue<IReadOnlyList<string>>(kvp.Key), ProgramSetBuilder<columnNames>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06008E95 RID: 36501 RVA: 0x001E0E8D File Offset: 0x001DF08D
		public static IEnumerable<KeyValuePair<Optional<int>, ProgramSetBuilder<skip>>> ClusterOnInput(this ProgramSetBuilder<skip> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<int>, ProgramSetBuilder<skip>>(Cluster.CastValue<int>(kvp.Key), ProgramSetBuilder<skip>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06008E96 RID: 36502 RVA: 0x001E0EBF File Offset: 0x001DF0BF
		public static IEnumerable<KeyValuePair<Optional<int>, ProgramSetBuilder<skipFooter>>> ClusterOnInput(this ProgramSetBuilder<skipFooter> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<int>, ProgramSetBuilder<skipFooter>>(Cluster.CastValue<int>(kvp.Key), ProgramSetBuilder<skipFooter>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06008E97 RID: 36503 RVA: 0x001E0EF1 File Offset: 0x001DF0F1
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<delimiter>>> ClusterOnInput(this ProgramSetBuilder<delimiter> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<delimiter>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<delimiter>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06008E98 RID: 36504 RVA: 0x001E0F23 File Offset: 0x001DF123
		public static IEnumerable<KeyValuePair<Optional<IReadOnlyList<Record<int, int?>>>, ProgramSetBuilder<fieldPositions>>> ClusterOnInput(this ProgramSetBuilder<fieldPositions> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IReadOnlyList<Record<int, int?>>>, ProgramSetBuilder<fieldPositions>>(Cluster.CastValue<IReadOnlyList<Record<int, int?>>>(kvp.Key), ProgramSetBuilder<fieldPositions>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06008E99 RID: 36505 RVA: 0x001E0F55 File Offset: 0x001DF155
		public static IEnumerable<KeyValuePair<Optional<bool>, ProgramSetBuilder<filterEmptyLines>>> ClusterOnInput(this ProgramSetBuilder<filterEmptyLines> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<bool>, ProgramSetBuilder<filterEmptyLines>>(Cluster.CastValue<bool>(kvp.Key), ProgramSetBuilder<filterEmptyLines>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06008E9A RID: 36506 RVA: 0x001E0F87 File Offset: 0x001DF187
		public static IEnumerable<KeyValuePair<Optional<Optional<string>>, ProgramSetBuilder<commentStr>>> ClusterOnInput(this ProgramSetBuilder<commentStr> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Optional<string>>, ProgramSetBuilder<commentStr>>(Cluster.CastValue<Optional<string>>(kvp.Key), ProgramSetBuilder<commentStr>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06008E9B RID: 36507 RVA: 0x001E0FB9 File Offset: 0x001DF1B9
		public static IEnumerable<KeyValuePair<Optional<Optional<char>>, ProgramSetBuilder<quoteChar>>> ClusterOnInput(this ProgramSetBuilder<quoteChar> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Optional<char>>, ProgramSetBuilder<quoteChar>>(Cluster.CastValue<Optional<char>>(kvp.Key), ProgramSetBuilder<quoteChar>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06008E9C RID: 36508 RVA: 0x001E0FEB File Offset: 0x001DF1EB
		public static IEnumerable<KeyValuePair<Optional<Optional<char>>, ProgramSetBuilder<escapeChar>>> ClusterOnInput(this ProgramSetBuilder<escapeChar> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Optional<char>>, ProgramSetBuilder<escapeChar>>(Cluster.CastValue<Optional<char>>(kvp.Key), ProgramSetBuilder<escapeChar>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06008E9D RID: 36509 RVA: 0x001E101D File Offset: 0x001DF21D
		public static IEnumerable<KeyValuePair<Optional<bool>, ProgramSetBuilder<doubleQuote>>> ClusterOnInput(this ProgramSetBuilder<doubleQuote> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<bool>, ProgramSetBuilder<doubleQuote>>(Cluster.CastValue<bool>(kvp.Key), ProgramSetBuilder<doubleQuote>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06008E9E RID: 36510 RVA: 0x001E104F File Offset: 0x001DF24F
		public static IEnumerable<KeyValuePair<Optional<ITable<string>>, ProgramSetBuilder<readFlatFile>>> ClusterOnInput(this ProgramSetBuilder<readFlatFile> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ITable<string>>, ProgramSetBuilder<readFlatFile>>(Cluster.CastValue<ITable<string>>(kvp.Key), ProgramSetBuilder<readFlatFile>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06008E9F RID: 36511 RVA: 0x001E1081 File Offset: 0x001DF281
		public static IEnumerable<KeyValuePair<Optional<ITable<StringRegion>>, ProgramSetBuilder<eText>>> ClusterOnInput(this ProgramSetBuilder<eText> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ITable<StringRegion>>, ProgramSetBuilder<eText>>(Cluster.CastValue<ITable<StringRegion>>(kvp.Key), ProgramSetBuilder<eText>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06008EA0 RID: 36512 RVA: 0x001E10B3 File Offset: 0x001DF2B3
		public static IEnumerable<KeyValuePair<Optional<StringRegion>, ProgramSetBuilder<_LetB0>>> ClusterOnInput(this ProgramSetBuilder<_LetB0> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<StringRegion>, ProgramSetBuilder<_LetB0>>(Cluster.CastValue<StringRegion>(kvp.Key), ProgramSetBuilder<_LetB0>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06008EA1 RID: 36513 RVA: 0x001E10E5 File Offset: 0x001DF2E5
		public static IEnumerable<KeyValuePair<Optional<ITable<StringRegion>>, ProgramSetBuilder<_LetB1>>> ClusterOnInput(this ProgramSetBuilder<_LetB1> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ITable<StringRegion>>, ProgramSetBuilder<_LetB1>>(Cluster.CastValue<ITable<StringRegion>>(kvp.Key), ProgramSetBuilder<_LetB1>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06008EA2 RID: 36514 RVA: 0x001E1117 File Offset: 0x001DF317
		public static IEnumerable<KeyValuePair<Optional<IReadOnlyList<string>>[], ProgramSetBuilder<columnNames>>> ClusterOnInputTuple(this ProgramSetBuilder<columnNames> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IReadOnlyList<string>>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<IReadOnlyList<string>>>(Cluster.CastValue<IReadOnlyList<string>>));
				}
				return new KeyValuePair<Optional<IReadOnlyList<string>>[], ProgramSetBuilder<columnNames>>(key.Select(func).ToArray<Optional<IReadOnlyList<string>>>(), ProgramSetBuilder<columnNames>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06008EA3 RID: 36515 RVA: 0x001E1149 File Offset: 0x001DF349
		public static IEnumerable<KeyValuePair<Optional<int>[], ProgramSetBuilder<skip>>> ClusterOnInputTuple(this ProgramSetBuilder<skip> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<int>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<int>>(Cluster.CastValue<int>));
				}
				return new KeyValuePair<Optional<int>[], ProgramSetBuilder<skip>>(key.Select(func).ToArray<Optional<int>>(), ProgramSetBuilder<skip>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06008EA4 RID: 36516 RVA: 0x001E117B File Offset: 0x001DF37B
		public static IEnumerable<KeyValuePair<Optional<int>[], ProgramSetBuilder<skipFooter>>> ClusterOnInputTuple(this ProgramSetBuilder<skipFooter> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<int>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<int>>(Cluster.CastValue<int>));
				}
				return new KeyValuePair<Optional<int>[], ProgramSetBuilder<skipFooter>>(key.Select(func).ToArray<Optional<int>>(), ProgramSetBuilder<skipFooter>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06008EA5 RID: 36517 RVA: 0x001E11AD File Offset: 0x001DF3AD
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<delimiter>>> ClusterOnInputTuple(this ProgramSetBuilder<delimiter> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<2>__CastValue) == null)
				{
					func = (Cluster.<>O.<2>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<delimiter>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<delimiter>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06008EA6 RID: 36518 RVA: 0x001E11DF File Offset: 0x001DF3DF
		public static IEnumerable<KeyValuePair<Optional<IReadOnlyList<Record<int, int?>>>[], ProgramSetBuilder<fieldPositions>>> ClusterOnInputTuple(this ProgramSetBuilder<fieldPositions> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IReadOnlyList<Record<int, int?>>>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<IReadOnlyList<Record<int, int?>>>>(Cluster.CastValue<IReadOnlyList<Record<int, int?>>>));
				}
				return new KeyValuePair<Optional<IReadOnlyList<Record<int, int?>>>[], ProgramSetBuilder<fieldPositions>>(key.Select(func).ToArray<Optional<IReadOnlyList<Record<int, int?>>>>(), ProgramSetBuilder<fieldPositions>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06008EA7 RID: 36519 RVA: 0x001E1211 File Offset: 0x001DF411
		public static IEnumerable<KeyValuePair<Optional<bool>[], ProgramSetBuilder<filterEmptyLines>>> ClusterOnInputTuple(this ProgramSetBuilder<filterEmptyLines> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<bool>> func;
				if ((func = Cluster.<>O.<4>__CastValue) == null)
				{
					func = (Cluster.<>O.<4>__CastValue = new Func<object, Optional<bool>>(Cluster.CastValue<bool>));
				}
				return new KeyValuePair<Optional<bool>[], ProgramSetBuilder<filterEmptyLines>>(key.Select(func).ToArray<Optional<bool>>(), ProgramSetBuilder<filterEmptyLines>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06008EA8 RID: 36520 RVA: 0x001E1243 File Offset: 0x001DF443
		public static IEnumerable<KeyValuePair<Optional<Optional<string>>[], ProgramSetBuilder<commentStr>>> ClusterOnInputTuple(this ProgramSetBuilder<commentStr> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Optional<string>>> func;
				if ((func = Cluster.<>O.<5>__CastValue) == null)
				{
					func = (Cluster.<>O.<5>__CastValue = new Func<object, Optional<Optional<string>>>(Cluster.CastValue<Optional<string>>));
				}
				return new KeyValuePair<Optional<Optional<string>>[], ProgramSetBuilder<commentStr>>(key.Select(func).ToArray<Optional<Optional<string>>>(), ProgramSetBuilder<commentStr>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06008EA9 RID: 36521 RVA: 0x001E1275 File Offset: 0x001DF475
		public static IEnumerable<KeyValuePair<Optional<Optional<char>>[], ProgramSetBuilder<quoteChar>>> ClusterOnInputTuple(this ProgramSetBuilder<quoteChar> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Optional<char>>> func;
				if ((func = Cluster.<>O.<6>__CastValue) == null)
				{
					func = (Cluster.<>O.<6>__CastValue = new Func<object, Optional<Optional<char>>>(Cluster.CastValue<Optional<char>>));
				}
				return new KeyValuePair<Optional<Optional<char>>[], ProgramSetBuilder<quoteChar>>(key.Select(func).ToArray<Optional<Optional<char>>>(), ProgramSetBuilder<quoteChar>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06008EAA RID: 36522 RVA: 0x001E12A7 File Offset: 0x001DF4A7
		public static IEnumerable<KeyValuePair<Optional<Optional<char>>[], ProgramSetBuilder<escapeChar>>> ClusterOnInputTuple(this ProgramSetBuilder<escapeChar> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Optional<char>>> func;
				if ((func = Cluster.<>O.<6>__CastValue) == null)
				{
					func = (Cluster.<>O.<6>__CastValue = new Func<object, Optional<Optional<char>>>(Cluster.CastValue<Optional<char>>));
				}
				return new KeyValuePair<Optional<Optional<char>>[], ProgramSetBuilder<escapeChar>>(key.Select(func).ToArray<Optional<Optional<char>>>(), ProgramSetBuilder<escapeChar>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06008EAB RID: 36523 RVA: 0x001E12D9 File Offset: 0x001DF4D9
		public static IEnumerable<KeyValuePair<Optional<bool>[], ProgramSetBuilder<doubleQuote>>> ClusterOnInputTuple(this ProgramSetBuilder<doubleQuote> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<bool>> func;
				if ((func = Cluster.<>O.<4>__CastValue) == null)
				{
					func = (Cluster.<>O.<4>__CastValue = new Func<object, Optional<bool>>(Cluster.CastValue<bool>));
				}
				return new KeyValuePair<Optional<bool>[], ProgramSetBuilder<doubleQuote>>(key.Select(func).ToArray<Optional<bool>>(), ProgramSetBuilder<doubleQuote>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06008EAC RID: 36524 RVA: 0x001E130B File Offset: 0x001DF50B
		public static IEnumerable<KeyValuePair<Optional<ITable<string>>[], ProgramSetBuilder<readFlatFile>>> ClusterOnInputTuple(this ProgramSetBuilder<readFlatFile> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ITable<string>>> func;
				if ((func = Cluster.<>O.<7>__CastValue) == null)
				{
					func = (Cluster.<>O.<7>__CastValue = new Func<object, Optional<ITable<string>>>(Cluster.CastValue<ITable<string>>));
				}
				return new KeyValuePair<Optional<ITable<string>>[], ProgramSetBuilder<readFlatFile>>(key.Select(func).ToArray<Optional<ITable<string>>>(), ProgramSetBuilder<readFlatFile>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06008EAD RID: 36525 RVA: 0x001E133D File Offset: 0x001DF53D
		public static IEnumerable<KeyValuePair<Optional<ITable<StringRegion>>[], ProgramSetBuilder<eText>>> ClusterOnInputTuple(this ProgramSetBuilder<eText> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ITable<StringRegion>>> func;
				if ((func = Cluster.<>O.<8>__CastValue) == null)
				{
					func = (Cluster.<>O.<8>__CastValue = new Func<object, Optional<ITable<StringRegion>>>(Cluster.CastValue<ITable<StringRegion>>));
				}
				return new KeyValuePair<Optional<ITable<StringRegion>>[], ProgramSetBuilder<eText>>(key.Select(func).ToArray<Optional<ITable<StringRegion>>>(), ProgramSetBuilder<eText>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06008EAE RID: 36526 RVA: 0x001E136F File Offset: 0x001DF56F
		public static IEnumerable<KeyValuePair<Optional<StringRegion>[], ProgramSetBuilder<_LetB0>>> ClusterOnInputTuple(this ProgramSetBuilder<_LetB0> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<StringRegion>> func;
				if ((func = Cluster.<>O.<9>__CastValue) == null)
				{
					func = (Cluster.<>O.<9>__CastValue = new Func<object, Optional<StringRegion>>(Cluster.CastValue<StringRegion>));
				}
				return new KeyValuePair<Optional<StringRegion>[], ProgramSetBuilder<_LetB0>>(key.Select(func).ToArray<Optional<StringRegion>>(), ProgramSetBuilder<_LetB0>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06008EAF RID: 36527 RVA: 0x001E13A1 File Offset: 0x001DF5A1
		public static IEnumerable<KeyValuePair<Optional<ITable<StringRegion>>[], ProgramSetBuilder<_LetB1>>> ClusterOnInputTuple(this ProgramSetBuilder<_LetB1> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ITable<StringRegion>>> func;
				if ((func = Cluster.<>O.<8>__CastValue) == null)
				{
					func = (Cluster.<>O.<8>__CastValue = new Func<object, Optional<ITable<StringRegion>>>(Cluster.CastValue<ITable<StringRegion>>));
				}
				return new KeyValuePair<Optional<ITable<StringRegion>>[], ProgramSetBuilder<_LetB1>>(key.Select(func).ToArray<Optional<ITable<StringRegion>>>(), ProgramSetBuilder<_LetB1>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x02001277 RID: 4727
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04003A43 RID: 14915
			public static Func<object, Optional<IReadOnlyList<string>>> <0>__CastValue;

			// Token: 0x04003A44 RID: 14916
			public static Func<object, Optional<int>> <1>__CastValue;

			// Token: 0x04003A45 RID: 14917
			public static Func<object, Optional<string>> <2>__CastValue;

			// Token: 0x04003A46 RID: 14918
			public static Func<object, Optional<IReadOnlyList<Record<int, int?>>>> <3>__CastValue;

			// Token: 0x04003A47 RID: 14919
			public static Func<object, Optional<bool>> <4>__CastValue;

			// Token: 0x04003A48 RID: 14920
			public static Func<object, Optional<Optional<string>>> <5>__CastValue;

			// Token: 0x04003A49 RID: 14921
			public static Func<object, Optional<Optional<char>>> <6>__CastValue;

			// Token: 0x04003A4A RID: 14922
			public static Func<object, Optional<ITable<string>>> <7>__CastValue;

			// Token: 0x04003A4B RID: 14923
			public static Func<object, Optional<ITable<StringRegion>>> <8>__CastValue;

			// Token: 0x04003A4C RID: 14924
			public static Func<object, Optional<StringRegion>> <9>__CastValue;
		}
	}
}
