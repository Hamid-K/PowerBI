using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Build
{
	// Token: 0x02000E05 RID: 3589
	public static class Cluster
	{
		// Token: 0x06005F01 RID: 24321 RVA: 0x000AB0AF File Offset: 0x000A92AF
		private static Optional<T> CastValue<T>(object obj)
		{
			if (!(obj is Bottom))
			{
				return ((T)((object)obj)).Some<T>();
			}
			return Optional<T>.Nothing;
		}

		// Token: 0x06005F02 RID: 24322 RVA: 0x0013AC7F File Offset: 0x00138E7F
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<output>>> ClusterOnInput(this ProgramSetBuilder<output> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<output>>(Cluster.CastValue<SpreadsheetArea>(kvp.Key), ProgramSetBuilder<output>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06005F03 RID: 24323 RVA: 0x0013ACB1 File Offset: 0x00138EB1
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<trim>>> ClusterOnInput(this ProgramSetBuilder<trim> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<trim>>(Cluster.CastValue<SpreadsheetArea>(kvp.Key), ProgramSetBuilder<trim>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06005F04 RID: 24324 RVA: 0x0013ACE3 File Offset: 0x00138EE3
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<area>>> ClusterOnInput(this ProgramSetBuilder<area> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<area>>(Cluster.CastValue<SpreadsheetArea>(kvp.Key), ProgramSetBuilder<area>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06005F05 RID: 24325 RVA: 0x0013AD15 File Offset: 0x00138F15
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<trimLeft>>> ClusterOnInput(this ProgramSetBuilder<trimLeft> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<trimLeft>>(Cluster.CastValue<SpreadsheetArea>(kvp.Key), ProgramSetBuilder<trimLeft>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06005F06 RID: 24326 RVA: 0x0013AD47 File Offset: 0x00138F47
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<trimBottom>>> ClusterOnInput(this ProgramSetBuilder<trimBottom> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<trimBottom>>(Cluster.CastValue<SpreadsheetArea>(kvp.Key), ProgramSetBuilder<trimBottom>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06005F07 RID: 24327 RVA: 0x0013AD79 File Offset: 0x00138F79
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<trimTop>>> ClusterOnInput(this ProgramSetBuilder<trimTop> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<trimTop>>(Cluster.CastValue<SpreadsheetArea>(kvp.Key), ProgramSetBuilder<trimTop>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06005F08 RID: 24328 RVA: 0x0013ADAB File Offset: 0x00138FAB
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<sheetSection>>> ClusterOnInput(this ProgramSetBuilder<sheetSection> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<sheetSection>>(Cluster.CastValue<SpreadsheetArea>(kvp.Key), ProgramSetBuilder<sheetSection>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06005F09 RID: 24329 RVA: 0x0013ADDD File Offset: 0x00138FDD
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<horizontalSheetSection>>> ClusterOnInput(this ProgramSetBuilder<horizontalSheetSection> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<horizontalSheetSection>>(Cluster.CastValue<SpreadsheetArea>(kvp.Key), ProgramSetBuilder<horizontalSheetSection>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06005F0A RID: 24330 RVA: 0x0013AE0F File Offset: 0x0013900F
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<verticalSheetSection>>> ClusterOnInput(this ProgramSetBuilder<verticalSheetSection> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<verticalSheetSection>>(Cluster.CastValue<SpreadsheetArea>(kvp.Key), ProgramSetBuilder<verticalSheetSection>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06005F0B RID: 24331 RVA: 0x0013AE41 File Offset: 0x00139041
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<uncleanedSheetSection>>> ClusterOnInput(this ProgramSetBuilder<uncleanedSheetSection> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<uncleanedSheetSection>>(Cluster.CastValue<SpreadsheetArea>(kvp.Key), ProgramSetBuilder<uncleanedSheetSection>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06005F0C RID: 24332 RVA: 0x0013AE73 File Offset: 0x00139073
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<wholeSheet>>> ClusterOnInput(this ProgramSetBuilder<wholeSheet> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<wholeSheet>>(Cluster.CastValue<SpreadsheetArea>(kvp.Key), ProgramSetBuilder<wholeSheet>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06005F0D RID: 24333 RVA: 0x0013AEA5 File Offset: 0x001390A5
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<wholeSheetFull>>> ClusterOnInput(this ProgramSetBuilder<wholeSheetFull> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<wholeSheetFull>>(Cluster.CastValue<SpreadsheetArea>(kvp.Key), ProgramSetBuilder<wholeSheetFull>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06005F0E RID: 24334 RVA: 0x0013AED7 File Offset: 0x001390D7
		public static IEnumerable<KeyValuePair<Optional<ISpreadsheet>, ProgramSetBuilder<sheet>>> ClusterOnInput(this ProgramSetBuilder<sheet> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ISpreadsheet>, ProgramSetBuilder<sheet>>(Cluster.CastValue<ISpreadsheet>(kvp.Key), ProgramSetBuilder<sheet>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06005F0F RID: 24335 RVA: 0x0013AF09 File Offset: 0x00139109
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea[]>, ProgramSetBuilder<horizontalSheetSplits>>> ClusterOnInput(this ProgramSetBuilder<horizontalSheetSplits> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<SpreadsheetArea[]>, ProgramSetBuilder<horizontalSheetSplits>>(Cluster.CastValue<SpreadsheetArea[]>(kvp.Key), ProgramSetBuilder<horizontalSheetSplits>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06005F10 RID: 24336 RVA: 0x0013AF3B File Offset: 0x0013913B
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea[]>, ProgramSetBuilder<verticalSheetSplits>>> ClusterOnInput(this ProgramSetBuilder<verticalSheetSplits> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<SpreadsheetArea[]>, ProgramSetBuilder<verticalSheetSplits>>(Cluster.CastValue<SpreadsheetArea[]>(kvp.Key), ProgramSetBuilder<verticalSheetSplits>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06005F11 RID: 24337 RVA: 0x0013AF6D File Offset: 0x0013916D
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea[]>, ProgramSetBuilder<sheetSplits>>> ClusterOnInput(this ProgramSetBuilder<sheetSplits> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<SpreadsheetArea[]>, ProgramSetBuilder<sheetSplits>>(Cluster.CastValue<SpreadsheetArea[]>(kvp.Key), ProgramSetBuilder<sheetSplits>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06005F12 RID: 24338 RVA: 0x0013AF9F File Offset: 0x0013919F
		public static IEnumerable<KeyValuePair<Optional<int>, ProgramSetBuilder<index>>> ClusterOnInput(this ProgramSetBuilder<index> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<int>, ProgramSetBuilder<index>>(Cluster.CastValue<int>(kvp.Key), ProgramSetBuilder<index>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06005F13 RID: 24339 RVA: 0x0013AFD1 File Offset: 0x001391D1
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<rangeName>>> ClusterOnInput(this ProgramSetBuilder<rangeName> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<rangeName>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<rangeName>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06005F14 RID: 24340 RVA: 0x0013B003 File Offset: 0x00139203
		public static IEnumerable<KeyValuePair<Optional<int>, ProgramSetBuilder<k>>> ClusterOnInput(this ProgramSetBuilder<k> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<int>, ProgramSetBuilder<k>>(Cluster.CastValue<int>(kvp.Key), ProgramSetBuilder<k>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06005F15 RID: 24341 RVA: 0x0013B035 File Offset: 0x00139235
		public static IEnumerable<KeyValuePair<Optional<SplitMode>, ProgramSetBuilder<splitMode>>> ClusterOnInput(this ProgramSetBuilder<splitMode> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<SplitMode>, ProgramSetBuilder<splitMode>>(Cluster.CastValue<SplitMode>(kvp.Key), ProgramSetBuilder<splitMode>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06005F16 RID: 24342 RVA: 0x0013B067 File Offset: 0x00139267
		public static IEnumerable<KeyValuePair<Optional<StyleFilter>, ProgramSetBuilder<styleFilter>>> ClusterOnInput(this ProgramSetBuilder<styleFilter> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<StyleFilter>, ProgramSetBuilder<styleFilter>>(Cluster.CastValue<StyleFilter>(kvp.Key), ProgramSetBuilder<styleFilter>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06005F17 RID: 24343 RVA: 0x0013B099 File Offset: 0x00139299
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<mProgram>>> ClusterOnInput(this ProgramSetBuilder<mProgram> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<mProgram>>(Cluster.CastValue<SpreadsheetArea>(kvp.Key), ProgramSetBuilder<mProgram>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06005F18 RID: 24344 RVA: 0x0013B0CB File Offset: 0x001392CB
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<mTable>>> ClusterOnInput(this ProgramSetBuilder<mTable> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<mTable>>(Cluster.CastValue<SpreadsheetArea>(kvp.Key), ProgramSetBuilder<mTable>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06005F19 RID: 24345 RVA: 0x0013B0FD File Offset: 0x001392FD
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea[]>, ProgramSetBuilder<mSection>>> ClusterOnInput(this ProgramSetBuilder<mSection> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<SpreadsheetArea[]>, ProgramSetBuilder<mSection>>(Cluster.CastValue<SpreadsheetArea[]>(kvp.Key), ProgramSetBuilder<mSection>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06005F1A RID: 24346 RVA: 0x0013B12F File Offset: 0x0013932F
		public static IEnumerable<KeyValuePair<Optional<ISpreadsheet>, ProgramSetBuilder<withoutFormatting>>> ClusterOnInput(this ProgramSetBuilder<withoutFormatting> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<ISpreadsheet>, ProgramSetBuilder<withoutFormatting>>(Cluster.CastValue<ISpreadsheet>(kvp.Key), ProgramSetBuilder<withoutFormatting>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06005F1B RID: 24347 RVA: 0x0013B161 File Offset: 0x00139361
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<startTitle>>> ClusterOnInput(this ProgramSetBuilder<startTitle> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<startTitle>>(Cluster.CastValue<SpreadsheetArea>(kvp.Key), ProgramSetBuilder<startTitle>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06005F1C RID: 24348 RVA: 0x0013B193 File Offset: 0x00139393
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<title>>> ClusterOnInput(this ProgramSetBuilder<title> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<title>>(Cluster.CastValue<SpreadsheetArea>(kvp.Key), ProgramSetBuilder<title>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06005F1D RID: 24349 RVA: 0x0013B1C5 File Offset: 0x001393C5
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<aboveOrLeftmost>>> ClusterOnInput(this ProgramSetBuilder<aboveOrLeftmost> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<aboveOrLeftmost>>(Cluster.CastValue<SpreadsheetArea>(kvp.Key), ProgramSetBuilder<aboveOrLeftmost>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06005F1E RID: 24350 RVA: 0x0013B1F7 File Offset: 0x001393F7
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<aboveOrOutput>>> ClusterOnInput(this ProgramSetBuilder<aboveOrOutput> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<aboveOrOutput>>(Cluster.CastValue<SpreadsheetArea>(kvp.Key), ProgramSetBuilder<aboveOrOutput>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06005F1F RID: 24351 RVA: 0x0013B229 File Offset: 0x00139429
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<aboveOrHeader>>> ClusterOnInput(this ProgramSetBuilder<aboveOrHeader> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<aboveOrHeader>>(Cluster.CastValue<SpreadsheetArea>(kvp.Key), ProgramSetBuilder<aboveOrHeader>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06005F20 RID: 24352 RVA: 0x0013B25B File Offset: 0x0013945B
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<headerSection>>> ClusterOnInput(this ProgramSetBuilder<headerSection> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<headerSection>>(Cluster.CastValue<SpreadsheetArea>(kvp.Key), ProgramSetBuilder<headerSection>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06005F21 RID: 24353 RVA: 0x0013B28D File Offset: 0x0013948D
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea[]>, ProgramSetBuilder<splitForTitle>>> ClusterOnInput(this ProgramSetBuilder<splitForTitle> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<SpreadsheetArea[]>, ProgramSetBuilder<splitForTitle>>(Cluster.CastValue<SpreadsheetArea[]>(kvp.Key), ProgramSetBuilder<splitForTitle>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06005F22 RID: 24354 RVA: 0x0013B2BF File Offset: 0x001394BF
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<above>>> ClusterOnInput(this ProgramSetBuilder<above> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<above>>(Cluster.CastValue<SpreadsheetArea>(kvp.Key), ProgramSetBuilder<above>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06005F23 RID: 24355 RVA: 0x0013B2F1 File Offset: 0x001394F1
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<titleOf>>> ClusterOnInput(this ProgramSetBuilder<titleOf> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<SpreadsheetArea>, ProgramSetBuilder<titleOf>>(Cluster.CastValue<SpreadsheetArea>(kvp.Key), ProgramSetBuilder<titleOf>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06005F24 RID: 24356 RVA: 0x0013B323 File Offset: 0x00139523
		public static IEnumerable<KeyValuePair<Optional<TitleAboveMode>, ProgramSetBuilder<titleAboveMode>>> ClusterOnInput(this ProgramSetBuilder<titleAboveMode> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<TitleAboveMode>, ProgramSetBuilder<titleAboveMode>>(Cluster.CastValue<TitleAboveMode>(kvp.Key), ProgramSetBuilder<titleAboveMode>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x06005F25 RID: 24357 RVA: 0x0013B355 File Offset: 0x00139555
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<output>>> ClusterOnInputTuple(this ProgramSetBuilder<output> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<SpreadsheetArea>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<SpreadsheetArea>>(Cluster.CastValue<SpreadsheetArea>));
				}
				return new KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<output>>(key.Select(func).ToArray<Optional<SpreadsheetArea>>(), ProgramSetBuilder<output>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06005F26 RID: 24358 RVA: 0x0013B387 File Offset: 0x00139587
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<trim>>> ClusterOnInputTuple(this ProgramSetBuilder<trim> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<SpreadsheetArea>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<SpreadsheetArea>>(Cluster.CastValue<SpreadsheetArea>));
				}
				return new KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<trim>>(key.Select(func).ToArray<Optional<SpreadsheetArea>>(), ProgramSetBuilder<trim>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06005F27 RID: 24359 RVA: 0x0013B3B9 File Offset: 0x001395B9
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<area>>> ClusterOnInputTuple(this ProgramSetBuilder<area> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<SpreadsheetArea>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<SpreadsheetArea>>(Cluster.CastValue<SpreadsheetArea>));
				}
				return new KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<area>>(key.Select(func).ToArray<Optional<SpreadsheetArea>>(), ProgramSetBuilder<area>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06005F28 RID: 24360 RVA: 0x0013B3EB File Offset: 0x001395EB
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<trimLeft>>> ClusterOnInputTuple(this ProgramSetBuilder<trimLeft> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<SpreadsheetArea>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<SpreadsheetArea>>(Cluster.CastValue<SpreadsheetArea>));
				}
				return new KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<trimLeft>>(key.Select(func).ToArray<Optional<SpreadsheetArea>>(), ProgramSetBuilder<trimLeft>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06005F29 RID: 24361 RVA: 0x0013B41D File Offset: 0x0013961D
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<trimBottom>>> ClusterOnInputTuple(this ProgramSetBuilder<trimBottom> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<SpreadsheetArea>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<SpreadsheetArea>>(Cluster.CastValue<SpreadsheetArea>));
				}
				return new KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<trimBottom>>(key.Select(func).ToArray<Optional<SpreadsheetArea>>(), ProgramSetBuilder<trimBottom>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06005F2A RID: 24362 RVA: 0x0013B44F File Offset: 0x0013964F
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<trimTop>>> ClusterOnInputTuple(this ProgramSetBuilder<trimTop> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<SpreadsheetArea>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<SpreadsheetArea>>(Cluster.CastValue<SpreadsheetArea>));
				}
				return new KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<trimTop>>(key.Select(func).ToArray<Optional<SpreadsheetArea>>(), ProgramSetBuilder<trimTop>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06005F2B RID: 24363 RVA: 0x0013B481 File Offset: 0x00139681
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<sheetSection>>> ClusterOnInputTuple(this ProgramSetBuilder<sheetSection> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<SpreadsheetArea>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<SpreadsheetArea>>(Cluster.CastValue<SpreadsheetArea>));
				}
				return new KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<sheetSection>>(key.Select(func).ToArray<Optional<SpreadsheetArea>>(), ProgramSetBuilder<sheetSection>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06005F2C RID: 24364 RVA: 0x0013B4B3 File Offset: 0x001396B3
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<horizontalSheetSection>>> ClusterOnInputTuple(this ProgramSetBuilder<horizontalSheetSection> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<SpreadsheetArea>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<SpreadsheetArea>>(Cluster.CastValue<SpreadsheetArea>));
				}
				return new KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<horizontalSheetSection>>(key.Select(func).ToArray<Optional<SpreadsheetArea>>(), ProgramSetBuilder<horizontalSheetSection>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06005F2D RID: 24365 RVA: 0x0013B4E5 File Offset: 0x001396E5
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<verticalSheetSection>>> ClusterOnInputTuple(this ProgramSetBuilder<verticalSheetSection> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<SpreadsheetArea>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<SpreadsheetArea>>(Cluster.CastValue<SpreadsheetArea>));
				}
				return new KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<verticalSheetSection>>(key.Select(func).ToArray<Optional<SpreadsheetArea>>(), ProgramSetBuilder<verticalSheetSection>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06005F2E RID: 24366 RVA: 0x0013B517 File Offset: 0x00139717
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<uncleanedSheetSection>>> ClusterOnInputTuple(this ProgramSetBuilder<uncleanedSheetSection> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<SpreadsheetArea>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<SpreadsheetArea>>(Cluster.CastValue<SpreadsheetArea>));
				}
				return new KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<uncleanedSheetSection>>(key.Select(func).ToArray<Optional<SpreadsheetArea>>(), ProgramSetBuilder<uncleanedSheetSection>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06005F2F RID: 24367 RVA: 0x0013B549 File Offset: 0x00139749
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<wholeSheet>>> ClusterOnInputTuple(this ProgramSetBuilder<wholeSheet> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<SpreadsheetArea>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<SpreadsheetArea>>(Cluster.CastValue<SpreadsheetArea>));
				}
				return new KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<wholeSheet>>(key.Select(func).ToArray<Optional<SpreadsheetArea>>(), ProgramSetBuilder<wholeSheet>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06005F30 RID: 24368 RVA: 0x0013B57B File Offset: 0x0013977B
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<wholeSheetFull>>> ClusterOnInputTuple(this ProgramSetBuilder<wholeSheetFull> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<SpreadsheetArea>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<SpreadsheetArea>>(Cluster.CastValue<SpreadsheetArea>));
				}
				return new KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<wholeSheetFull>>(key.Select(func).ToArray<Optional<SpreadsheetArea>>(), ProgramSetBuilder<wholeSheetFull>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06005F31 RID: 24369 RVA: 0x0013B5AD File Offset: 0x001397AD
		public static IEnumerable<KeyValuePair<Optional<ISpreadsheet>[], ProgramSetBuilder<sheet>>> ClusterOnInputTuple(this ProgramSetBuilder<sheet> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ISpreadsheet>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<ISpreadsheet>>(Cluster.CastValue<ISpreadsheet>));
				}
				return new KeyValuePair<Optional<ISpreadsheet>[], ProgramSetBuilder<sheet>>(key.Select(func).ToArray<Optional<ISpreadsheet>>(), ProgramSetBuilder<sheet>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06005F32 RID: 24370 RVA: 0x0013B5DF File Offset: 0x001397DF
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea[]>[], ProgramSetBuilder<horizontalSheetSplits>>> ClusterOnInputTuple(this ProgramSetBuilder<horizontalSheetSplits> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<SpreadsheetArea[]>> func;
				if ((func = Cluster.<>O.<2>__CastValue) == null)
				{
					func = (Cluster.<>O.<2>__CastValue = new Func<object, Optional<SpreadsheetArea[]>>(Cluster.CastValue<SpreadsheetArea[]>));
				}
				return new KeyValuePair<Optional<SpreadsheetArea[]>[], ProgramSetBuilder<horizontalSheetSplits>>(key.Select(func).ToArray<Optional<SpreadsheetArea[]>>(), ProgramSetBuilder<horizontalSheetSplits>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06005F33 RID: 24371 RVA: 0x0013B611 File Offset: 0x00139811
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea[]>[], ProgramSetBuilder<verticalSheetSplits>>> ClusterOnInputTuple(this ProgramSetBuilder<verticalSheetSplits> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<SpreadsheetArea[]>> func;
				if ((func = Cluster.<>O.<2>__CastValue) == null)
				{
					func = (Cluster.<>O.<2>__CastValue = new Func<object, Optional<SpreadsheetArea[]>>(Cluster.CastValue<SpreadsheetArea[]>));
				}
				return new KeyValuePair<Optional<SpreadsheetArea[]>[], ProgramSetBuilder<verticalSheetSplits>>(key.Select(func).ToArray<Optional<SpreadsheetArea[]>>(), ProgramSetBuilder<verticalSheetSplits>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06005F34 RID: 24372 RVA: 0x0013B643 File Offset: 0x00139843
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea[]>[], ProgramSetBuilder<sheetSplits>>> ClusterOnInputTuple(this ProgramSetBuilder<sheetSplits> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<SpreadsheetArea[]>> func;
				if ((func = Cluster.<>O.<2>__CastValue) == null)
				{
					func = (Cluster.<>O.<2>__CastValue = new Func<object, Optional<SpreadsheetArea[]>>(Cluster.CastValue<SpreadsheetArea[]>));
				}
				return new KeyValuePair<Optional<SpreadsheetArea[]>[], ProgramSetBuilder<sheetSplits>>(key.Select(func).ToArray<Optional<SpreadsheetArea[]>>(), ProgramSetBuilder<sheetSplits>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06005F35 RID: 24373 RVA: 0x0013B675 File Offset: 0x00139875
		public static IEnumerable<KeyValuePair<Optional<int>[], ProgramSetBuilder<index>>> ClusterOnInputTuple(this ProgramSetBuilder<index> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<int>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<int>>(Cluster.CastValue<int>));
				}
				return new KeyValuePair<Optional<int>[], ProgramSetBuilder<index>>(key.Select(func).ToArray<Optional<int>>(), ProgramSetBuilder<index>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06005F36 RID: 24374 RVA: 0x0013B6A7 File Offset: 0x001398A7
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<rangeName>>> ClusterOnInputTuple(this ProgramSetBuilder<rangeName> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<4>__CastValue) == null)
				{
					func = (Cluster.<>O.<4>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<rangeName>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<rangeName>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06005F37 RID: 24375 RVA: 0x0013B6D9 File Offset: 0x001398D9
		public static IEnumerable<KeyValuePair<Optional<int>[], ProgramSetBuilder<k>>> ClusterOnInputTuple(this ProgramSetBuilder<k> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<int>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<int>>(Cluster.CastValue<int>));
				}
				return new KeyValuePair<Optional<int>[], ProgramSetBuilder<k>>(key.Select(func).ToArray<Optional<int>>(), ProgramSetBuilder<k>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06005F38 RID: 24376 RVA: 0x0013B70B File Offset: 0x0013990B
		public static IEnumerable<KeyValuePair<Optional<SplitMode>[], ProgramSetBuilder<splitMode>>> ClusterOnInputTuple(this ProgramSetBuilder<splitMode> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<SplitMode>> func;
				if ((func = Cluster.<>O.<5>__CastValue) == null)
				{
					func = (Cluster.<>O.<5>__CastValue = new Func<object, Optional<SplitMode>>(Cluster.CastValue<SplitMode>));
				}
				return new KeyValuePair<Optional<SplitMode>[], ProgramSetBuilder<splitMode>>(key.Select(func).ToArray<Optional<SplitMode>>(), ProgramSetBuilder<splitMode>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06005F39 RID: 24377 RVA: 0x0013B73D File Offset: 0x0013993D
		public static IEnumerable<KeyValuePair<Optional<StyleFilter>[], ProgramSetBuilder<styleFilter>>> ClusterOnInputTuple(this ProgramSetBuilder<styleFilter> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<StyleFilter>> func;
				if ((func = Cluster.<>O.<6>__CastValue) == null)
				{
					func = (Cluster.<>O.<6>__CastValue = new Func<object, Optional<StyleFilter>>(Cluster.CastValue<StyleFilter>));
				}
				return new KeyValuePair<Optional<StyleFilter>[], ProgramSetBuilder<styleFilter>>(key.Select(func).ToArray<Optional<StyleFilter>>(), ProgramSetBuilder<styleFilter>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06005F3A RID: 24378 RVA: 0x0013B76F File Offset: 0x0013996F
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<mProgram>>> ClusterOnInputTuple(this ProgramSetBuilder<mProgram> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<SpreadsheetArea>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<SpreadsheetArea>>(Cluster.CastValue<SpreadsheetArea>));
				}
				return new KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<mProgram>>(key.Select(func).ToArray<Optional<SpreadsheetArea>>(), ProgramSetBuilder<mProgram>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06005F3B RID: 24379 RVA: 0x0013B7A1 File Offset: 0x001399A1
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<mTable>>> ClusterOnInputTuple(this ProgramSetBuilder<mTable> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<SpreadsheetArea>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<SpreadsheetArea>>(Cluster.CastValue<SpreadsheetArea>));
				}
				return new KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<mTable>>(key.Select(func).ToArray<Optional<SpreadsheetArea>>(), ProgramSetBuilder<mTable>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06005F3C RID: 24380 RVA: 0x0013B7D3 File Offset: 0x001399D3
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea[]>[], ProgramSetBuilder<mSection>>> ClusterOnInputTuple(this ProgramSetBuilder<mSection> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<SpreadsheetArea[]>> func;
				if ((func = Cluster.<>O.<2>__CastValue) == null)
				{
					func = (Cluster.<>O.<2>__CastValue = new Func<object, Optional<SpreadsheetArea[]>>(Cluster.CastValue<SpreadsheetArea[]>));
				}
				return new KeyValuePair<Optional<SpreadsheetArea[]>[], ProgramSetBuilder<mSection>>(key.Select(func).ToArray<Optional<SpreadsheetArea[]>>(), ProgramSetBuilder<mSection>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06005F3D RID: 24381 RVA: 0x0013B805 File Offset: 0x00139A05
		public static IEnumerable<KeyValuePair<Optional<ISpreadsheet>[], ProgramSetBuilder<withoutFormatting>>> ClusterOnInputTuple(this ProgramSetBuilder<withoutFormatting> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<ISpreadsheet>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<ISpreadsheet>>(Cluster.CastValue<ISpreadsheet>));
				}
				return new KeyValuePair<Optional<ISpreadsheet>[], ProgramSetBuilder<withoutFormatting>>(key.Select(func).ToArray<Optional<ISpreadsheet>>(), ProgramSetBuilder<withoutFormatting>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06005F3E RID: 24382 RVA: 0x0013B837 File Offset: 0x00139A37
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<startTitle>>> ClusterOnInputTuple(this ProgramSetBuilder<startTitle> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<SpreadsheetArea>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<SpreadsheetArea>>(Cluster.CastValue<SpreadsheetArea>));
				}
				return new KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<startTitle>>(key.Select(func).ToArray<Optional<SpreadsheetArea>>(), ProgramSetBuilder<startTitle>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06005F3F RID: 24383 RVA: 0x0013B869 File Offset: 0x00139A69
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<title>>> ClusterOnInputTuple(this ProgramSetBuilder<title> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<SpreadsheetArea>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<SpreadsheetArea>>(Cluster.CastValue<SpreadsheetArea>));
				}
				return new KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<title>>(key.Select(func).ToArray<Optional<SpreadsheetArea>>(), ProgramSetBuilder<title>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06005F40 RID: 24384 RVA: 0x0013B89B File Offset: 0x00139A9B
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<aboveOrLeftmost>>> ClusterOnInputTuple(this ProgramSetBuilder<aboveOrLeftmost> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<SpreadsheetArea>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<SpreadsheetArea>>(Cluster.CastValue<SpreadsheetArea>));
				}
				return new KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<aboveOrLeftmost>>(key.Select(func).ToArray<Optional<SpreadsheetArea>>(), ProgramSetBuilder<aboveOrLeftmost>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06005F41 RID: 24385 RVA: 0x0013B8CD File Offset: 0x00139ACD
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<aboveOrOutput>>> ClusterOnInputTuple(this ProgramSetBuilder<aboveOrOutput> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<SpreadsheetArea>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<SpreadsheetArea>>(Cluster.CastValue<SpreadsheetArea>));
				}
				return new KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<aboveOrOutput>>(key.Select(func).ToArray<Optional<SpreadsheetArea>>(), ProgramSetBuilder<aboveOrOutput>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06005F42 RID: 24386 RVA: 0x0013B8FF File Offset: 0x00139AFF
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<aboveOrHeader>>> ClusterOnInputTuple(this ProgramSetBuilder<aboveOrHeader> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<SpreadsheetArea>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<SpreadsheetArea>>(Cluster.CastValue<SpreadsheetArea>));
				}
				return new KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<aboveOrHeader>>(key.Select(func).ToArray<Optional<SpreadsheetArea>>(), ProgramSetBuilder<aboveOrHeader>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06005F43 RID: 24387 RVA: 0x0013B931 File Offset: 0x00139B31
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<headerSection>>> ClusterOnInputTuple(this ProgramSetBuilder<headerSection> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<SpreadsheetArea>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<SpreadsheetArea>>(Cluster.CastValue<SpreadsheetArea>));
				}
				return new KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<headerSection>>(key.Select(func).ToArray<Optional<SpreadsheetArea>>(), ProgramSetBuilder<headerSection>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06005F44 RID: 24388 RVA: 0x0013B963 File Offset: 0x00139B63
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea[]>[], ProgramSetBuilder<splitForTitle>>> ClusterOnInputTuple(this ProgramSetBuilder<splitForTitle> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<SpreadsheetArea[]>> func;
				if ((func = Cluster.<>O.<2>__CastValue) == null)
				{
					func = (Cluster.<>O.<2>__CastValue = new Func<object, Optional<SpreadsheetArea[]>>(Cluster.CastValue<SpreadsheetArea[]>));
				}
				return new KeyValuePair<Optional<SpreadsheetArea[]>[], ProgramSetBuilder<splitForTitle>>(key.Select(func).ToArray<Optional<SpreadsheetArea[]>>(), ProgramSetBuilder<splitForTitle>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06005F45 RID: 24389 RVA: 0x0013B995 File Offset: 0x00139B95
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<above>>> ClusterOnInputTuple(this ProgramSetBuilder<above> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<SpreadsheetArea>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<SpreadsheetArea>>(Cluster.CastValue<SpreadsheetArea>));
				}
				return new KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<above>>(key.Select(func).ToArray<Optional<SpreadsheetArea>>(), ProgramSetBuilder<above>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06005F46 RID: 24390 RVA: 0x0013B9C7 File Offset: 0x00139BC7
		public static IEnumerable<KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<titleOf>>> ClusterOnInputTuple(this ProgramSetBuilder<titleOf> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<SpreadsheetArea>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<SpreadsheetArea>>(Cluster.CastValue<SpreadsheetArea>));
				}
				return new KeyValuePair<Optional<SpreadsheetArea>[], ProgramSetBuilder<titleOf>>(key.Select(func).ToArray<Optional<SpreadsheetArea>>(), ProgramSetBuilder<titleOf>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x06005F47 RID: 24391 RVA: 0x0013B9F9 File Offset: 0x00139BF9
		public static IEnumerable<KeyValuePair<Optional<TitleAboveMode>[], ProgramSetBuilder<titleAboveMode>>> ClusterOnInputTuple(this ProgramSetBuilder<titleAboveMode> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<TitleAboveMode>> func;
				if ((func = Cluster.<>O.<7>__CastValue) == null)
				{
					func = (Cluster.<>O.<7>__CastValue = new Func<object, Optional<TitleAboveMode>>(Cluster.CastValue<TitleAboveMode>));
				}
				return new KeyValuePair<Optional<TitleAboveMode>[], ProgramSetBuilder<titleAboveMode>>(key.Select(func).ToArray<Optional<TitleAboveMode>>(), ProgramSetBuilder<titleAboveMode>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x02000E06 RID: 3590
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04002B63 RID: 11107
			public static Func<object, Optional<SpreadsheetArea>> <0>__CastValue;

			// Token: 0x04002B64 RID: 11108
			public static Func<object, Optional<ISpreadsheet>> <1>__CastValue;

			// Token: 0x04002B65 RID: 11109
			public static Func<object, Optional<SpreadsheetArea[]>> <2>__CastValue;

			// Token: 0x04002B66 RID: 11110
			public static Func<object, Optional<int>> <3>__CastValue;

			// Token: 0x04002B67 RID: 11111
			public static Func<object, Optional<string>> <4>__CastValue;

			// Token: 0x04002B68 RID: 11112
			public static Func<object, Optional<SplitMode>> <5>__CastValue;

			// Token: 0x04002B69 RID: 11113
			public static Func<object, Optional<StyleFilter>> <6>__CastValue;

			// Token: 0x04002B6A RID: 11114
			public static Func<object, Optional<TitleAboveMode>> <7>__CastValue;
		}
	}
}
