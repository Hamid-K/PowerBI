using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Detection.RichDataTypes;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Extraction.Json;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Learning;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Build
{
	// Token: 0x02001AA4 RID: 6820
	public static class Cluster
	{
		// Token: 0x0600E10E RID: 57614 RVA: 0x000AB0AF File Offset: 0x000A92AF
		private static Optional<T> CastValue<T>(object obj)
		{
			if (!(obj is Bottom))
			{
				return ((T)((object)obj)).Some<T>();
			}
			return Optional<T>.Nothing;
		}

		// Token: 0x0600E10F RID: 57615 RVA: 0x002FEE6B File Offset: 0x002FD06B
		public static IEnumerable<KeyValuePair<Optional<ITable<object>>, ProgramSetBuilder<@out>>> ClusterOnInput(this ProgramSetBuilder<@out> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ITable<object>>, ProgramSetBuilder<@out>>(Cluster.CastValue<ITable<object>>(kvp.Key), ProgramSetBuilder<@out>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600E110 RID: 57616 RVA: 0x002FEE9D File Offset: 0x002FD09D
		public static IEnumerable<KeyValuePair<Optional<ITable<object>>, ProgramSetBuilder<table>>> ClusterOnInput(this ProgramSetBuilder<table> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ITable<object>>, ProgramSetBuilder<table>>(Cluster.CastValue<ITable<object>>(kvp.Key), ProgramSetBuilder<table>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600E111 RID: 57617 RVA: 0x002FEECF File Offset: 0x002FD0CF
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IEnumerable<object>>>, ProgramSetBuilder<newColumns>>> ClusterOnInput(this ProgramSetBuilder<newColumns> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<IEnumerable<object>>>, ProgramSetBuilder<newColumns>>(Cluster.CastValue<IEnumerable<IEnumerable<object>>>(kvp.Key), ProgramSetBuilder<newColumns>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600E112 RID: 57618 RVA: 0x002FEF01 File Offset: 0x002FD101
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<StringRegion>>, ProgramSetBuilder<columnToSplit>>> ClusterOnInput(this ProgramSetBuilder<columnToSplit> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<StringRegion>>, ProgramSetBuilder<columnToSplit>>(Cluster.CastValue<IEnumerable<StringRegion>>(kvp.Key), ProgramSetBuilder<columnToSplit>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600E113 RID: 57619 RVA: 0x002FEF33 File Offset: 0x002FD133
		public static IEnumerable<KeyValuePair<Optional<SplitCell[]>, ProgramSetBuilder<splitCell>>> ClusterOnInput(this ProgramSetBuilder<splitCell> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<SplitCell[]>, ProgramSetBuilder<splitCell>>(Cluster.CastValue<SplitCell[]>(kvp.Key), ProgramSetBuilder<splitCell>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600E114 RID: 57620 RVA: 0x002FEF65 File Offset: 0x002FD165
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<sourceColumnName>>> ClusterOnInput(this ProgramSetBuilder<sourceColumnName> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<sourceColumnName>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<sourceColumnName>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600E115 RID: 57621 RVA: 0x002FEF97 File Offset: 0x002FD197
		public static IEnumerable<KeyValuePair<Optional<IRichDataType>, ProgramSetBuilder<richDataType>>> ClusterOnInput(this ProgramSetBuilder<richDataType> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IRichDataType>, ProgramSetBuilder<richDataType>>(Cluster.CastValue<IRichDataType>(kvp.Key), ProgramSetBuilder<richDataType>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600E116 RID: 57622 RVA: 0x002FEFC9 File Offset: 0x002FD1C9
		public static IEnumerable<KeyValuePair<Optional<FillMethod>, ProgramSetBuilder<fillMethod>>> ClusterOnInput(this ProgramSetBuilder<fillMethod> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<FillMethod>, ProgramSetBuilder<fillMethod>>(Cluster.CastValue<FillMethod>(kvp.Key), ProgramSetBuilder<fillMethod>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600E117 RID: 57623 RVA: 0x002FEFFB File Offset: 0x002FD1FB
		public static IEnumerable<KeyValuePair<Optional<DropCondition>, ProgramSetBuilder<dropCondition>>> ClusterOnInput(this ProgramSetBuilder<dropCondition> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<DropCondition>, ProgramSetBuilder<dropCondition>>(Cluster.CastValue<DropCondition>(kvp.Key), ProgramSetBuilder<dropCondition>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600E118 RID: 57624 RVA: 0x002FF02D File Offset: 0x002FD22D
		public static IEnumerable<KeyValuePair<Optional<object>, ProgramSetBuilder<fillValue>>> ClusterOnInput(this ProgramSetBuilder<fillValue> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<object>, ProgramSetBuilder<fillValue>>(Cluster.CastValue<object>(kvp.Key), ProgramSetBuilder<fillValue>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600E119 RID: 57625 RVA: 0x002FF05F File Offset: 0x002FD25F
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<object>>, ProgramSetBuilder<missingValueMarkers>>> ClusterOnInput(this ProgramSetBuilder<missingValueMarkers> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<IEnumerable<object>>, ProgramSetBuilder<missingValueMarkers>>(Cluster.CastValue<IEnumerable<object>>(kvp.Key), ProgramSetBuilder<missingValueMarkers>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600E11A RID: 57626 RVA: 0x002FF091 File Offset: 0x002FD291
		public static IEnumerable<KeyValuePair<Optional<bool>, ProgramSetBuilder<isMixedColumn>>> ClusterOnInput(this ProgramSetBuilder<isMixedColumn> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<bool>, ProgramSetBuilder<isMixedColumn>>(Cluster.CastValue<bool>(kvp.Key), ProgramSetBuilder<isMixedColumn>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600E11B RID: 57627 RVA: 0x002FF0C3 File Offset: 0x002FD2C3
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<delimiter>>> ClusterOnInput(this ProgramSetBuilder<delimiter> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<delimiter>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<delimiter>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600E11C RID: 57628 RVA: 0x002FF0F5 File Offset: 0x002FD2F5
		public static IEnumerable<KeyValuePair<Optional<Program>, ProgramSetBuilder<ejsonProgram>>> ClusterOnInput(this ProgramSetBuilder<ejsonProgram> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Program>, ProgramSetBuilder<ejsonProgram>>(Cluster.CastValue<Program>(kvp.Key), ProgramSetBuilder<ejsonProgram>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600E11D RID: 57629 RVA: 0x002FF127 File Offset: 0x002FD327
		public static IEnumerable<KeyValuePair<Optional<ITable<object>>[], ProgramSetBuilder<@out>>> ClusterOnInputTuple(this ProgramSetBuilder<@out> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ITable<object>>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<ITable<object>>>(Cluster.CastValue<ITable<object>>));
				}
				return new KeyValuePair<Optional<ITable<object>>[], ProgramSetBuilder<@out>>(key.Select(func).ToArray<Optional<ITable<object>>>(), ProgramSetBuilder<@out>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600E11E RID: 57630 RVA: 0x002FF159 File Offset: 0x002FD359
		public static IEnumerable<KeyValuePair<Optional<ITable<object>>[], ProgramSetBuilder<table>>> ClusterOnInputTuple(this ProgramSetBuilder<table> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ITable<object>>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<ITable<object>>>(Cluster.CastValue<ITable<object>>));
				}
				return new KeyValuePair<Optional<ITable<object>>[], ProgramSetBuilder<table>>(key.Select(func).ToArray<Optional<ITable<object>>>(), ProgramSetBuilder<table>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600E11F RID: 57631 RVA: 0x002FF18B File Offset: 0x002FD38B
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<IEnumerable<object>>>[], ProgramSetBuilder<newColumns>>> ClusterOnInputTuple(this ProgramSetBuilder<newColumns> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<IEnumerable<object>>>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<IEnumerable<IEnumerable<object>>>>(Cluster.CastValue<IEnumerable<IEnumerable<object>>>));
				}
				return new KeyValuePair<Optional<IEnumerable<IEnumerable<object>>>[], ProgramSetBuilder<newColumns>>(key.Select(func).ToArray<Optional<IEnumerable<IEnumerable<object>>>>(), ProgramSetBuilder<newColumns>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600E120 RID: 57632 RVA: 0x002FF1BD File Offset: 0x002FD3BD
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<StringRegion>>[], ProgramSetBuilder<columnToSplit>>> ClusterOnInputTuple(this ProgramSetBuilder<columnToSplit> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<StringRegion>>> func;
				if ((func = Cluster.<>O.<2>__CastValue) == null)
				{
					func = (Cluster.<>O.<2>__CastValue = new Func<object, Optional<IEnumerable<StringRegion>>>(Cluster.CastValue<IEnumerable<StringRegion>>));
				}
				return new KeyValuePair<Optional<IEnumerable<StringRegion>>[], ProgramSetBuilder<columnToSplit>>(key.Select(func).ToArray<Optional<IEnumerable<StringRegion>>>(), ProgramSetBuilder<columnToSplit>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600E121 RID: 57633 RVA: 0x002FF1EF File Offset: 0x002FD3EF
		public static IEnumerable<KeyValuePair<Optional<SplitCell[]>[], ProgramSetBuilder<splitCell>>> ClusterOnInputTuple(this ProgramSetBuilder<splitCell> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<SplitCell[]>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<SplitCell[]>>(Cluster.CastValue<SplitCell[]>));
				}
				return new KeyValuePair<Optional<SplitCell[]>[], ProgramSetBuilder<splitCell>>(key.Select(func).ToArray<Optional<SplitCell[]>>(), ProgramSetBuilder<splitCell>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600E122 RID: 57634 RVA: 0x002FF221 File Offset: 0x002FD421
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<sourceColumnName>>> ClusterOnInputTuple(this ProgramSetBuilder<sourceColumnName> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<4>__CastValue) == null)
				{
					func = (Cluster.<>O.<4>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<sourceColumnName>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<sourceColumnName>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600E123 RID: 57635 RVA: 0x002FF253 File Offset: 0x002FD453
		public static IEnumerable<KeyValuePair<Optional<IRichDataType>[], ProgramSetBuilder<richDataType>>> ClusterOnInputTuple(this ProgramSetBuilder<richDataType> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IRichDataType>> func;
				if ((func = Cluster.<>O.<5>__CastValue) == null)
				{
					func = (Cluster.<>O.<5>__CastValue = new Func<object, Optional<IRichDataType>>(Cluster.CastValue<IRichDataType>));
				}
				return new KeyValuePair<Optional<IRichDataType>[], ProgramSetBuilder<richDataType>>(key.Select(func).ToArray<Optional<IRichDataType>>(), ProgramSetBuilder<richDataType>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600E124 RID: 57636 RVA: 0x002FF285 File Offset: 0x002FD485
		public static IEnumerable<KeyValuePair<Optional<FillMethod>[], ProgramSetBuilder<fillMethod>>> ClusterOnInputTuple(this ProgramSetBuilder<fillMethod> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<FillMethod>> func;
				if ((func = Cluster.<>O.<6>__CastValue) == null)
				{
					func = (Cluster.<>O.<6>__CastValue = new Func<object, Optional<FillMethod>>(Cluster.CastValue<FillMethod>));
				}
				return new KeyValuePair<Optional<FillMethod>[], ProgramSetBuilder<fillMethod>>(key.Select(func).ToArray<Optional<FillMethod>>(), ProgramSetBuilder<fillMethod>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600E125 RID: 57637 RVA: 0x002FF2B7 File Offset: 0x002FD4B7
		public static IEnumerable<KeyValuePair<Optional<DropCondition>[], ProgramSetBuilder<dropCondition>>> ClusterOnInputTuple(this ProgramSetBuilder<dropCondition> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<DropCondition>> func;
				if ((func = Cluster.<>O.<7>__CastValue) == null)
				{
					func = (Cluster.<>O.<7>__CastValue = new Func<object, Optional<DropCondition>>(Cluster.CastValue<DropCondition>));
				}
				return new KeyValuePair<Optional<DropCondition>[], ProgramSetBuilder<dropCondition>>(key.Select(func).ToArray<Optional<DropCondition>>(), ProgramSetBuilder<dropCondition>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600E126 RID: 57638 RVA: 0x002FF2E9 File Offset: 0x002FD4E9
		public static IEnumerable<KeyValuePair<Optional<object>[], ProgramSetBuilder<fillValue>>> ClusterOnInputTuple(this ProgramSetBuilder<fillValue> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<object>> func;
				if ((func = Cluster.<>O.<8>__CastValue) == null)
				{
					func = (Cluster.<>O.<8>__CastValue = new Func<object, Optional<object>>(Cluster.CastValue<object>));
				}
				return new KeyValuePair<Optional<object>[], ProgramSetBuilder<fillValue>>(key.Select(func).ToArray<Optional<object>>(), ProgramSetBuilder<fillValue>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600E127 RID: 57639 RVA: 0x002FF31B File Offset: 0x002FD51B
		public static IEnumerable<KeyValuePair<Optional<IEnumerable<object>>[], ProgramSetBuilder<missingValueMarkers>>> ClusterOnInputTuple(this ProgramSetBuilder<missingValueMarkers> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<IEnumerable<object>>> func;
				if ((func = Cluster.<>O.<9>__CastValue) == null)
				{
					func = (Cluster.<>O.<9>__CastValue = new Func<object, Optional<IEnumerable<object>>>(Cluster.CastValue<IEnumerable<object>>));
				}
				return new KeyValuePair<Optional<IEnumerable<object>>[], ProgramSetBuilder<missingValueMarkers>>(key.Select(func).ToArray<Optional<IEnumerable<object>>>(), ProgramSetBuilder<missingValueMarkers>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600E128 RID: 57640 RVA: 0x002FF34D File Offset: 0x002FD54D
		public static IEnumerable<KeyValuePair<Optional<bool>[], ProgramSetBuilder<isMixedColumn>>> ClusterOnInputTuple(this ProgramSetBuilder<isMixedColumn> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<bool>> func;
				if ((func = Cluster.<>O.<10>__CastValue) == null)
				{
					func = (Cluster.<>O.<10>__CastValue = new Func<object, Optional<bool>>(Cluster.CastValue<bool>));
				}
				return new KeyValuePair<Optional<bool>[], ProgramSetBuilder<isMixedColumn>>(key.Select(func).ToArray<Optional<bool>>(), ProgramSetBuilder<isMixedColumn>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600E129 RID: 57641 RVA: 0x002FF37F File Offset: 0x002FD57F
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<delimiter>>> ClusterOnInputTuple(this ProgramSetBuilder<delimiter> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<4>__CastValue) == null)
				{
					func = (Cluster.<>O.<4>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<delimiter>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<delimiter>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600E12A RID: 57642 RVA: 0x002FF3B1 File Offset: 0x002FD5B1
		public static IEnumerable<KeyValuePair<Optional<Program>[], ProgramSetBuilder<ejsonProgram>>> ClusterOnInputTuple(this ProgramSetBuilder<ejsonProgram> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Program>> func;
				if ((func = Cluster.<>O.<11>__CastValue) == null)
				{
					func = (Cluster.<>O.<11>__CastValue = new Func<object, Optional<Program>>(Cluster.CastValue<Program>));
				}
				return new KeyValuePair<Optional<Program>[], ProgramSetBuilder<ejsonProgram>>(key.Select(func).ToArray<Optional<Program>>(), ProgramSetBuilder<ejsonProgram>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x02001AA5 RID: 6821
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400553D RID: 21821
			public static Func<object, Optional<ITable<object>>> <0>__CastValue;

			// Token: 0x0400553E RID: 21822
			public static Func<object, Optional<IEnumerable<IEnumerable<object>>>> <1>__CastValue;

			// Token: 0x0400553F RID: 21823
			public static Func<object, Optional<IEnumerable<StringRegion>>> <2>__CastValue;

			// Token: 0x04005540 RID: 21824
			public static Func<object, Optional<SplitCell[]>> <3>__CastValue;

			// Token: 0x04005541 RID: 21825
			public static Func<object, Optional<string>> <4>__CastValue;

			// Token: 0x04005542 RID: 21826
			public static Func<object, Optional<IRichDataType>> <5>__CastValue;

			// Token: 0x04005543 RID: 21827
			public static Func<object, Optional<FillMethod>> <6>__CastValue;

			// Token: 0x04005544 RID: 21828
			public static Func<object, Optional<DropCondition>> <7>__CastValue;

			// Token: 0x04005545 RID: 21829
			public static Func<object, Optional<object>> <8>__CastValue;

			// Token: 0x04005546 RID: 21830
			public static Func<object, Optional<IEnumerable<object>>> <9>__CastValue;

			// Token: 0x04005547 RID: 21831
			public static Func<object, Optional<bool>> <10>__CastValue;

			// Token: 0x04005548 RID: 21832
			public static Func<object, Optional<Program>> <11>__CastValue;
		}
	}
}
