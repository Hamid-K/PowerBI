using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build
{
	// Token: 0x02001512 RID: 5394
	public static class Cluster
	{
		// Token: 0x0600AE52 RID: 44626 RVA: 0x000AB0AF File Offset: 0x000A92AF
		private static Optional<T> CastValue<T>(object obj)
		{
			if (!(obj is Bottom))
			{
				return ((T)((object)obj)).Some<T>();
			}
			return Optional<T>.Nothing;
		}

		// Token: 0x0600AE53 RID: 44627 RVA: 0x00269B97 File Offset: 0x00267D97
		public static IEnumerable<KeyValuePair<Optional<object>, ProgramSetBuilder<result>>> ClusterOnInput(this ProgramSetBuilder<result> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<object>, ProgramSetBuilder<result>>(Cluster.CastValue<object>(kvp.Key), ProgramSetBuilder<result>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE54 RID: 44628 RVA: 0x00269BC9 File Offset: 0x00267DC9
		public static IEnumerable<KeyValuePair<Optional<object>, ProgramSetBuilder<output>>> ClusterOnInput(this ProgramSetBuilder<output> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<object>, ProgramSetBuilder<output>>(Cluster.CastValue<object>(kvp.Key), ProgramSetBuilder<output>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE55 RID: 44629 RVA: 0x00269BFB File Offset: 0x00267DFB
		public static IEnumerable<KeyValuePair<Optional<decimal>, ProgramSetBuilder<outNumber>>> ClusterOnInput(this ProgramSetBuilder<outNumber> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<decimal>, ProgramSetBuilder<outNumber>>(Cluster.CastValue<decimal>(kvp.Key), ProgramSetBuilder<outNumber>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE56 RID: 44630 RVA: 0x00269C2D File Offset: 0x00267E2D
		public static IEnumerable<KeyValuePair<Optional<DateTime>, ProgramSetBuilder<outDate>>> ClusterOnInput(this ProgramSetBuilder<outDate> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<DateTime>, ProgramSetBuilder<outDate>>(Cluster.CastValue<DateTime>(kvp.Key), ProgramSetBuilder<outDate>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE57 RID: 44631 RVA: 0x00269C5F File Offset: 0x00267E5F
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<outStr>>> ClusterOnInput(this ProgramSetBuilder<outStr> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<outStr>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<outStr>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE58 RID: 44632 RVA: 0x00269C91 File Offset: 0x00267E91
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<outStr1>>> ClusterOnInput(this ProgramSetBuilder<outStr1> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<outStr1>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<outStr1>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE59 RID: 44633 RVA: 0x00269CC3 File Offset: 0x00267EC3
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<segmentCase>>> ClusterOnInput(this ProgramSetBuilder<segmentCase> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<segmentCase>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<segmentCase>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE5A RID: 44634 RVA: 0x00269CF5 File Offset: 0x00267EF5
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<segment>>> ClusterOnInput(this ProgramSetBuilder<segment> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<segment>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<segment>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE5B RID: 44635 RVA: 0x00269D27 File Offset: 0x00267F27
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<formatted>>> ClusterOnInput(this ProgramSetBuilder<formatted> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<formatted>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<formatted>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE5C RID: 44636 RVA: 0x00269D59 File Offset: 0x00267F59
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<concatEntry>>> ClusterOnInput(this ProgramSetBuilder<concatEntry> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<concatEntry>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<concatEntry>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE5D RID: 44637 RVA: 0x00269D8B File Offset: 0x00267F8B
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<concatCase>>> ClusterOnInput(this ProgramSetBuilder<concatCase> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<concatCase>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<concatCase>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE5E RID: 44638 RVA: 0x00269DBD File Offset: 0x00267FBD
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<concat>>> ClusterOnInput(this ProgramSetBuilder<concat> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<concat>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<concat>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE5F RID: 44639 RVA: 0x00269DEF File Offset: 0x00267FEF
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<concatPrefix>>> ClusterOnInput(this ProgramSetBuilder<concatPrefix> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<concatPrefix>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<concatPrefix>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE60 RID: 44640 RVA: 0x00269E21 File Offset: 0x00268021
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<concatSegment>>> ClusterOnInput(this ProgramSetBuilder<concatSegment> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<concatSegment>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<concatSegment>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE61 RID: 44641 RVA: 0x00269E53 File Offset: 0x00268053
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<concatSuffix>>> ClusterOnInput(this ProgramSetBuilder<concatSuffix> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<concatSuffix>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<concatSuffix>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE62 RID: 44642 RVA: 0x00269E85 File Offset: 0x00268085
		public static IEnumerable<KeyValuePair<Optional<bool>, ProgramSetBuilder<condition>>> ClusterOnInput(this ProgramSetBuilder<condition> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<bool>, ProgramSetBuilder<condition>>(Cluster.CastValue<bool>(kvp.Key), ProgramSetBuilder<condition>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE63 RID: 44643 RVA: 0x00269EB7 File Offset: 0x002680B7
		public static IEnumerable<KeyValuePair<Optional<bool>, ProgramSetBuilder<or>>> ClusterOnInput(this ProgramSetBuilder<or> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<bool>, ProgramSetBuilder<or>>(Cluster.CastValue<bool>(kvp.Key), ProgramSetBuilder<or>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE64 RID: 44644 RVA: 0x00269EE9 File Offset: 0x002680E9
		public static IEnumerable<KeyValuePair<Optional<object>, ProgramSetBuilder<inull>>> ClusterOnInput(this ProgramSetBuilder<inull> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<object>, ProgramSetBuilder<inull>>(Cluster.CastValue<object>(kvp.Key), ProgramSetBuilder<inull>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE65 RID: 44645 RVA: 0x00269F1B File Offset: 0x0026811B
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<equalsText>>> ClusterOnInput(this ProgramSetBuilder<equalsText> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<equalsText>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<equalsText>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE66 RID: 44646 RVA: 0x00269F4D File Offset: 0x0026814D
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<containsFindText>>> ClusterOnInput(this ProgramSetBuilder<containsFindText> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<containsFindText>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<containsFindText>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE67 RID: 44647 RVA: 0x00269F7F File Offset: 0x0026817F
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<startsWithFindText>>> ClusterOnInput(this ProgramSetBuilder<startsWithFindText> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<startsWithFindText>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<startsWithFindText>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE68 RID: 44648 RVA: 0x00269FB1 File Offset: 0x002681B1
		public static IEnumerable<KeyValuePair<Optional<Regex>, ProgramSetBuilder<isMatchRegex>>> ClusterOnInput(this ProgramSetBuilder<isMatchRegex> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Regex>, ProgramSetBuilder<isMatchRegex>>(Cluster.CastValue<Regex>(kvp.Key), ProgramSetBuilder<isMatchRegex>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE69 RID: 44649 RVA: 0x00269FE3 File Offset: 0x002681E3
		public static IEnumerable<KeyValuePair<Optional<Regex>, ProgramSetBuilder<containsMatchRegex>>> ClusterOnInput(this ProgramSetBuilder<containsMatchRegex> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Regex>, ProgramSetBuilder<containsMatchRegex>>(Cluster.CastValue<Regex>(kvp.Key), ProgramSetBuilder<containsMatchRegex>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE6A RID: 44650 RVA: 0x0026A015 File Offset: 0x00268215
		public static IEnumerable<KeyValuePair<Optional<int>, ProgramSetBuilder<containsCount>>> ClusterOnInput(this ProgramSetBuilder<containsCount> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<int>, ProgramSetBuilder<containsCount>>(Cluster.CastValue<int>(kvp.Key), ProgramSetBuilder<containsCount>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE6B RID: 44651 RVA: 0x0026A047 File Offset: 0x00268247
		public static IEnumerable<KeyValuePair<Optional<int>, ProgramSetBuilder<matchCount>>> ClusterOnInput(this ProgramSetBuilder<matchCount> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<int>, ProgramSetBuilder<matchCount>>(Cluster.CastValue<int>(kvp.Key), ProgramSetBuilder<matchCount>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE6C RID: 44652 RVA: 0x0026A079 File Offset: 0x00268279
		public static IEnumerable<KeyValuePair<Optional<decimal>, ProgramSetBuilder<numberEqualsValue>>> ClusterOnInput(this ProgramSetBuilder<numberEqualsValue> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<decimal>, ProgramSetBuilder<numberEqualsValue>>(Cluster.CastValue<decimal>(kvp.Key), ProgramSetBuilder<numberEqualsValue>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE6D RID: 44653 RVA: 0x0026A0AB File Offset: 0x002682AB
		public static IEnumerable<KeyValuePair<Optional<decimal>, ProgramSetBuilder<numberGreaterThanValue>>> ClusterOnInput(this ProgramSetBuilder<numberGreaterThanValue> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<decimal>, ProgramSetBuilder<numberGreaterThanValue>>(Cluster.CastValue<decimal>(kvp.Key), ProgramSetBuilder<numberGreaterThanValue>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE6E RID: 44654 RVA: 0x0026A0DD File Offset: 0x002682DD
		public static IEnumerable<KeyValuePair<Optional<decimal>, ProgramSetBuilder<numberLessThanValue>>> ClusterOnInput(this ProgramSetBuilder<numberLessThanValue> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<decimal>, ProgramSetBuilder<numberLessThanValue>>(Cluster.CastValue<decimal>(kvp.Key), ProgramSetBuilder<numberLessThanValue>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE6F RID: 44655 RVA: 0x0026A10F File Offset: 0x0026830F
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<formatNumber>>> ClusterOnInput(this ProgramSetBuilder<formatNumber> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<formatNumber>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<formatNumber>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE70 RID: 44656 RVA: 0x0026A141 File Offset: 0x00268341
		public static IEnumerable<KeyValuePair<Optional<decimal>, ProgramSetBuilder<number>>> ClusterOnInput(this ProgramSetBuilder<number> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<decimal>, ProgramSetBuilder<number>>(Cluster.CastValue<decimal>(kvp.Key), ProgramSetBuilder<number>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE71 RID: 44657 RVA: 0x0026A173 File Offset: 0x00268373
		public static IEnumerable<KeyValuePair<Optional<decimal>, ProgramSetBuilder<number1>>> ClusterOnInput(this ProgramSetBuilder<number1> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<decimal>, ProgramSetBuilder<number1>>(Cluster.CastValue<decimal>(kvp.Key), ProgramSetBuilder<number1>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE72 RID: 44658 RVA: 0x0026A1A5 File Offset: 0x002683A5
		public static IEnumerable<KeyValuePair<Optional<decimal>, ProgramSetBuilder<arithmetic>>> ClusterOnInput(this ProgramSetBuilder<arithmetic> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<decimal>, ProgramSetBuilder<arithmetic>>(Cluster.CastValue<decimal>(kvp.Key), ProgramSetBuilder<arithmetic>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE73 RID: 44659 RVA: 0x0026A1D7 File Offset: 0x002683D7
		public static IEnumerable<KeyValuePair<Optional<decimal>, ProgramSetBuilder<arithmeticLeft>>> ClusterOnInput(this ProgramSetBuilder<arithmeticLeft> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<decimal>, ProgramSetBuilder<arithmeticLeft>>(Cluster.CastValue<decimal>(kvp.Key), ProgramSetBuilder<arithmeticLeft>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE74 RID: 44660 RVA: 0x0026A209 File Offset: 0x00268409
		public static IEnumerable<KeyValuePair<Optional<decimal>, ProgramSetBuilder<addRight>>> ClusterOnInput(this ProgramSetBuilder<addRight> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<decimal>, ProgramSetBuilder<addRight>>(Cluster.CastValue<decimal>(kvp.Key), ProgramSetBuilder<addRight>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE75 RID: 44661 RVA: 0x0026A23B File Offset: 0x0026843B
		public static IEnumerable<KeyValuePair<Optional<decimal>, ProgramSetBuilder<subtractRight>>> ClusterOnInput(this ProgramSetBuilder<subtractRight> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<decimal>, ProgramSetBuilder<subtractRight>>(Cluster.CastValue<decimal>(kvp.Key), ProgramSetBuilder<subtractRight>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE76 RID: 44662 RVA: 0x0026A26D File Offset: 0x0026846D
		public static IEnumerable<KeyValuePair<Optional<decimal>, ProgramSetBuilder<multiplyRight>>> ClusterOnInput(this ProgramSetBuilder<multiplyRight> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<decimal>, ProgramSetBuilder<multiplyRight>>(Cluster.CastValue<decimal>(kvp.Key), ProgramSetBuilder<multiplyRight>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE77 RID: 44663 RVA: 0x0026A29F File Offset: 0x0026849F
		public static IEnumerable<KeyValuePair<Optional<decimal>, ProgramSetBuilder<divideRight>>> ClusterOnInput(this ProgramSetBuilder<divideRight> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<decimal>, ProgramSetBuilder<divideRight>>(Cluster.CastValue<decimal>(kvp.Key), ProgramSetBuilder<divideRight>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE78 RID: 44664 RVA: 0x0026A2D1 File Offset: 0x002684D1
		public static IEnumerable<KeyValuePair<Optional<decimal>, ProgramSetBuilder<inumber>>> ClusterOnInput(this ProgramSetBuilder<inumber> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<decimal>, ProgramSetBuilder<inumber>>(Cluster.CastValue<decimal>(kvp.Key), ProgramSetBuilder<inumber>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE79 RID: 44665 RVA: 0x0026A303 File Offset: 0x00268503
		public static IEnumerable<KeyValuePair<Optional<decimal>, ProgramSetBuilder<rowNumberTransform>>> ClusterOnInput(this ProgramSetBuilder<rowNumberTransform> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<decimal>, ProgramSetBuilder<rowNumberTransform>>(Cluster.CastValue<decimal>(kvp.Key), ProgramSetBuilder<rowNumberTransform>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE7A RID: 44666 RVA: 0x0026A335 File Offset: 0x00268535
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<formatDateTime>>> ClusterOnInput(this ProgramSetBuilder<formatDateTime> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<formatDateTime>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<formatDateTime>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE7B RID: 44667 RVA: 0x0026A367 File Offset: 0x00268567
		public static IEnumerable<KeyValuePair<Optional<DateTime>, ProgramSetBuilder<date>>> ClusterOnInput(this ProgramSetBuilder<date> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<DateTime>, ProgramSetBuilder<date>>(Cluster.CastValue<DateTime>(kvp.Key), ProgramSetBuilder<date>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE7C RID: 44668 RVA: 0x0026A399 File Offset: 0x00268599
		public static IEnumerable<KeyValuePair<Optional<DateTime>, ProgramSetBuilder<idate>>> ClusterOnInput(this ProgramSetBuilder<idate> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<DateTime>, ProgramSetBuilder<idate>>(Cluster.CastValue<DateTime>(kvp.Key), ProgramSetBuilder<idate>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE7D RID: 44669 RVA: 0x0026A3CB File Offset: 0x002685CB
		public static IEnumerable<KeyValuePair<Optional<Time>, ProgramSetBuilder<itime>>> ClusterOnInput(this ProgramSetBuilder<itime> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Time>, ProgramSetBuilder<itime>>(Cluster.CastValue<Time>(kvp.Key), ProgramSetBuilder<itime>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE7E RID: 44670 RVA: 0x0026A3FD File Offset: 0x002685FD
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<parseSubject>>> ClusterOnInput(this ProgramSetBuilder<parseSubject> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<parseSubject>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<parseSubject>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE7F RID: 44671 RVA: 0x0026A42F File Offset: 0x0026862F
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<letSubstring>>> ClusterOnInput(this ProgramSetBuilder<letSubstring> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<letSubstring>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<letSubstring>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE80 RID: 44672 RVA: 0x0026A461 File Offset: 0x00268661
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<substring>>> ClusterOnInput(this ProgramSetBuilder<substring> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<substring>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<substring>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE81 RID: 44673 RVA: 0x0026A493 File Offset: 0x00268693
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<splitTrim>>> ClusterOnInput(this ProgramSetBuilder<splitTrim> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<splitTrim>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<splitTrim>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE82 RID: 44674 RVA: 0x0026A4C5 File Offset: 0x002686C5
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<split>>> ClusterOnInput(this ProgramSetBuilder<split> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<split>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<split>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE83 RID: 44675 RVA: 0x0026A4F7 File Offset: 0x002686F7
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<sliceTrim>>> ClusterOnInput(this ProgramSetBuilder<sliceTrim> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<sliceTrim>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<sliceTrim>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE84 RID: 44676 RVA: 0x0026A529 File Offset: 0x00268729
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<slice>>> ClusterOnInput(this ProgramSetBuilder<slice> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<slice>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<slice>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE85 RID: 44677 RVA: 0x0026A55B File Offset: 0x0026875B
		public static IEnumerable<KeyValuePair<Optional<int>, ProgramSetBuilder<pos>>> ClusterOnInput(this ProgramSetBuilder<pos> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<int>, ProgramSetBuilder<pos>>(Cluster.CastValue<int>(kvp.Key), ProgramSetBuilder<pos>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE86 RID: 44678 RVA: 0x0026A58D File Offset: 0x0026878D
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<fromStrTrim>>> ClusterOnInput(this ProgramSetBuilder<fromStrTrim> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<fromStrTrim>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<fromStrTrim>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE87 RID: 44679 RVA: 0x0026A5BF File Offset: 0x002687BF
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<fromStr>>> ClusterOnInput(this ProgramSetBuilder<fromStr> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<fromStr>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<fromStr>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE88 RID: 44680 RVA: 0x0026A5F1 File Offset: 0x002687F1
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<fromNumberStr>>> ClusterOnInput(this ProgramSetBuilder<fromNumberStr> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<fromNumberStr>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<fromNumberStr>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE89 RID: 44681 RVA: 0x0026A623 File Offset: 0x00268823
		public static IEnumerable<KeyValuePair<Optional<decimal>, ProgramSetBuilder<fromNumber>>> ClusterOnInput(this ProgramSetBuilder<fromNumber> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<decimal>, ProgramSetBuilder<fromNumber>>(Cluster.CastValue<decimal>(kvp.Key), ProgramSetBuilder<fromNumber>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE8A RID: 44682 RVA: 0x0026A655 File Offset: 0x00268855
		public static IEnumerable<KeyValuePair<Optional<decimal>, ProgramSetBuilder<fromNumberCoalesced>>> ClusterOnInput(this ProgramSetBuilder<fromNumberCoalesced> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<decimal>, ProgramSetBuilder<fromNumberCoalesced>>(Cluster.CastValue<decimal>(kvp.Key), ProgramSetBuilder<fromNumberCoalesced>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE8B RID: 44683 RVA: 0x0026A687 File Offset: 0x00268887
		public static IEnumerable<KeyValuePair<Optional<int>, ProgramSetBuilder<fromRowNumber>>> ClusterOnInput(this ProgramSetBuilder<fromRowNumber> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<int>, ProgramSetBuilder<fromRowNumber>>(Cluster.CastValue<int>(kvp.Key), ProgramSetBuilder<fromRowNumber>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE8C RID: 44684 RVA: 0x0026A6B9 File Offset: 0x002688B9
		public static IEnumerable<KeyValuePair<Optional<decimal[]>, ProgramSetBuilder<fromNumbers>>> ClusterOnInput(this ProgramSetBuilder<fromNumbers> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<decimal[]>, ProgramSetBuilder<fromNumbers>>(Cluster.CastValue<decimal[]>(kvp.Key), ProgramSetBuilder<fromNumbers>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE8D RID: 44685 RVA: 0x0026A6EB File Offset: 0x002688EB
		public static IEnumerable<KeyValuePair<Optional<DateTime>, ProgramSetBuilder<fromDateTime>>> ClusterOnInput(this ProgramSetBuilder<fromDateTime> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<DateTime>, ProgramSetBuilder<fromDateTime>>(Cluster.CastValue<DateTime>(kvp.Key), ProgramSetBuilder<fromDateTime>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE8E RID: 44686 RVA: 0x0026A71D File Offset: 0x0026891D
		public static IEnumerable<KeyValuePair<Optional<DateTime>, ProgramSetBuilder<fromDateTimePart>>> ClusterOnInput(this ProgramSetBuilder<fromDateTimePart> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<DateTime>, ProgramSetBuilder<fromDateTimePart>>(Cluster.CastValue<DateTime>(kvp.Key), ProgramSetBuilder<fromDateTimePart>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE8F RID: 44687 RVA: 0x0026A74F File Offset: 0x0026894F
		public static IEnumerable<KeyValuePair<Optional<Time>, ProgramSetBuilder<fromTime>>> ClusterOnInput(this ProgramSetBuilder<fromTime> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<Time>, ProgramSetBuilder<fromTime>>(Cluster.CastValue<Time>(kvp.Key), ProgramSetBuilder<fromTime>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE90 RID: 44688 RVA: 0x0026A781 File Offset: 0x00268981
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<constString>>> ClusterOnInput(this ProgramSetBuilder<constString> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<constString>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<constString>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE91 RID: 44689 RVA: 0x0026A7B3 File Offset: 0x002689B3
		public static IEnumerable<KeyValuePair<Optional<decimal>, ProgramSetBuilder<constNumber>>> ClusterOnInput(this ProgramSetBuilder<constNumber> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<decimal>, ProgramSetBuilder<constNumber>>(Cluster.CastValue<decimal>(kvp.Key), ProgramSetBuilder<constNumber>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE92 RID: 44690 RVA: 0x0026A7E5 File Offset: 0x002689E5
		public static IEnumerable<KeyValuePair<Optional<DateTime>, ProgramSetBuilder<constDate>>> ClusterOnInput(this ProgramSetBuilder<constDate> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<DateTime>, ProgramSetBuilder<constDate>>(Cluster.CastValue<DateTime>(kvp.Key), ProgramSetBuilder<constDate>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE93 RID: 44691 RVA: 0x0026A817 File Offset: 0x00268A17
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<columnName>>> ClusterOnInput(this ProgramSetBuilder<columnName> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<columnName>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<columnName>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE94 RID: 44692 RVA: 0x0026A849 File Offset: 0x00268A49
		public static IEnumerable<KeyValuePair<Optional<string[]>, ProgramSetBuilder<columnNames>>> ClusterOnInput(this ProgramSetBuilder<columnNames> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string[]>, ProgramSetBuilder<columnNames>>(Cluster.CastValue<string[]>(kvp.Key), ProgramSetBuilder<columnNames>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE95 RID: 44693 RVA: 0x0026A87B File Offset: 0x00268A7B
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<constStr>>> ClusterOnInput(this ProgramSetBuilder<constStr> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<constStr>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<constStr>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE96 RID: 44694 RVA: 0x0026A8AD File Offset: 0x00268AAD
		public static IEnumerable<KeyValuePair<Optional<decimal>, ProgramSetBuilder<constNum>>> ClusterOnInput(this ProgramSetBuilder<constNum> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<decimal>, ProgramSetBuilder<constNum>>(Cluster.CastValue<decimal>(kvp.Key), ProgramSetBuilder<constNum>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE97 RID: 44695 RVA: 0x0026A8DF File Offset: 0x00268ADF
		public static IEnumerable<KeyValuePair<Optional<DateTime>, ProgramSetBuilder<constDt>>> ClusterOnInput(this ProgramSetBuilder<constDt> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<DateTime>, ProgramSetBuilder<constDt>>(Cluster.CastValue<DateTime>(kvp.Key), ProgramSetBuilder<constDt>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE98 RID: 44696 RVA: 0x0026A911 File Offset: 0x00268B11
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<locale>>> ClusterOnInput(this ProgramSetBuilder<locale> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<locale>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<locale>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE99 RID: 44697 RVA: 0x0026A943 File Offset: 0x00268B43
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<replaceFindText>>> ClusterOnInput(this ProgramSetBuilder<replaceFindText> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<replaceFindText>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<replaceFindText>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE9A RID: 44698 RVA: 0x0026A975 File Offset: 0x00268B75
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<replaceText>>> ClusterOnInput(this ProgramSetBuilder<replaceText> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<replaceText>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<replaceText>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE9B RID: 44699 RVA: 0x0026A9A7 File Offset: 0x00268BA7
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<sliceBetweenStartText>>> ClusterOnInput(this ProgramSetBuilder<sliceBetweenStartText> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<sliceBetweenStartText>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<sliceBetweenStartText>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE9C RID: 44700 RVA: 0x0026A9D9 File Offset: 0x00268BD9
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<sliceBetweenEndText>>> ClusterOnInput(this ProgramSetBuilder<sliceBetweenEndText> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<sliceBetweenEndText>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<sliceBetweenEndText>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE9D RID: 44701 RVA: 0x0026AA0B File Offset: 0x00268C0B
		public static IEnumerable<KeyValuePair<Optional<FormatNumberDescriptor>, ProgramSetBuilder<numberFormatDesc>>> ClusterOnInput(this ProgramSetBuilder<numberFormatDesc> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<FormatNumberDescriptor>, ProgramSetBuilder<numberFormatDesc>>(Cluster.CastValue<FormatNumberDescriptor>(kvp.Key), ProgramSetBuilder<numberFormatDesc>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE9E RID: 44702 RVA: 0x0026AA3D File Offset: 0x00268C3D
		public static IEnumerable<KeyValuePair<Optional<RoundNumberDescriptor>, ProgramSetBuilder<numberRoundDesc>>> ClusterOnInput(this ProgramSetBuilder<numberRoundDesc> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<RoundNumberDescriptor>, ProgramSetBuilder<numberRoundDesc>>(Cluster.CastValue<RoundNumberDescriptor>(kvp.Key), ProgramSetBuilder<numberRoundDesc>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AE9F RID: 44703 RVA: 0x0026AA6F File Offset: 0x00268C6F
		public static IEnumerable<KeyValuePair<Optional<RoundDateTimeDescriptor>, ProgramSetBuilder<dateTimeRoundDesc>>> ClusterOnInput(this ProgramSetBuilder<dateTimeRoundDesc> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<RoundDateTimeDescriptor>, ProgramSetBuilder<dateTimeRoundDesc>>(Cluster.CastValue<RoundDateTimeDescriptor>(kvp.Key), ProgramSetBuilder<dateTimeRoundDesc>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AEA0 RID: 44704 RVA: 0x0026AAA1 File Offset: 0x00268CA1
		public static IEnumerable<KeyValuePair<Optional<DateTimeDescriptor>, ProgramSetBuilder<dateTimeFormatDesc>>> ClusterOnInput(this ProgramSetBuilder<dateTimeFormatDesc> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<DateTimeDescriptor>, ProgramSetBuilder<dateTimeFormatDesc>>(Cluster.CastValue<DateTimeDescriptor>(kvp.Key), ProgramSetBuilder<dateTimeFormatDesc>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AEA1 RID: 44705 RVA: 0x0026AAD3 File Offset: 0x00268CD3
		public static IEnumerable<KeyValuePair<Optional<DateTimeDescriptor>, ProgramSetBuilder<dateTimeParseDesc>>> ClusterOnInput(this ProgramSetBuilder<dateTimeParseDesc> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<DateTimeDescriptor>, ProgramSetBuilder<dateTimeParseDesc>>(Cluster.CastValue<DateTimeDescriptor>(kvp.Key), ProgramSetBuilder<dateTimeParseDesc>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AEA2 RID: 44706 RVA: 0x0026AB05 File Offset: 0x00268D05
		public static IEnumerable<KeyValuePair<Optional<DateTimePartKind>, ProgramSetBuilder<dateTimePartKind>>> ClusterOnInput(this ProgramSetBuilder<dateTimePartKind> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<DateTimePartKind>, ProgramSetBuilder<dateTimePartKind>>(Cluster.CastValue<DateTimePartKind>(kvp.Key), ProgramSetBuilder<dateTimePartKind>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AEA3 RID: 44707 RVA: 0x0026AB37 File Offset: 0x00268D37
		public static IEnumerable<KeyValuePair<Optional<DateTimePartKind>, ProgramSetBuilder<fromDateTimePartKind>>> ClusterOnInput(this ProgramSetBuilder<fromDateTimePartKind> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<DateTimePartKind>, ProgramSetBuilder<fromDateTimePartKind>>(Cluster.CastValue<DateTimePartKind>(kvp.Key), ProgramSetBuilder<fromDateTimePartKind>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AEA4 RID: 44708 RVA: 0x0026AB69 File Offset: 0x00268D69
		public static IEnumerable<KeyValuePair<Optional<TimePartKind>, ProgramSetBuilder<timePartKind>>> ClusterOnInput(this ProgramSetBuilder<timePartKind> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<TimePartKind>, ProgramSetBuilder<timePartKind>>(Cluster.CastValue<TimePartKind>(kvp.Key), ProgramSetBuilder<timePartKind>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AEA5 RID: 44709 RVA: 0x0026AB9B File Offset: 0x00268D9B
		public static IEnumerable<KeyValuePair<Optional<RowNumberLinearTransformDescriptor>, ProgramSetBuilder<rowNumberLinearTransformDesc>>> ClusterOnInput(this ProgramSetBuilder<rowNumberLinearTransformDesc> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<RowNumberLinearTransformDescriptor>, ProgramSetBuilder<rowNumberLinearTransformDesc>>(Cluster.CastValue<RowNumberLinearTransformDescriptor>(kvp.Key), ProgramSetBuilder<rowNumberLinearTransformDesc>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AEA6 RID: 44710 RVA: 0x0026ABCD File Offset: 0x00268DCD
		public static IEnumerable<KeyValuePair<Optional<MatchDescriptor>, ProgramSetBuilder<matchDesc>>> ClusterOnInput(this ProgramSetBuilder<matchDesc> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<MatchDescriptor>, ProgramSetBuilder<matchDesc>>(Cluster.CastValue<MatchDescriptor>(kvp.Key), ProgramSetBuilder<matchDesc>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AEA7 RID: 44711 RVA: 0x0026ABFF File Offset: 0x00268DFF
		public static IEnumerable<KeyValuePair<Optional<int>, ProgramSetBuilder<matchInstance>>> ClusterOnInput(this ProgramSetBuilder<matchInstance> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<int>, ProgramSetBuilder<matchInstance>>(Cluster.CastValue<int>(kvp.Key), ProgramSetBuilder<matchInstance>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AEA8 RID: 44712 RVA: 0x0026AC31 File Offset: 0x00268E31
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<splitDelimiter>>> ClusterOnInput(this ProgramSetBuilder<splitDelimiter> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<splitDelimiter>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<splitDelimiter>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AEA9 RID: 44713 RVA: 0x0026AC63 File Offset: 0x00268E63
		public static IEnumerable<KeyValuePair<Optional<int>, ProgramSetBuilder<splitInstance>>> ClusterOnInput(this ProgramSetBuilder<splitInstance> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<int>, ProgramSetBuilder<splitInstance>>(Cluster.CastValue<int>(kvp.Key), ProgramSetBuilder<splitInstance>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AEAA RID: 44714 RVA: 0x0026AC95 File Offset: 0x00268E95
		public static IEnumerable<KeyValuePair<Optional<string>, ProgramSetBuilder<findDelimiter>>> ClusterOnInput(this ProgramSetBuilder<findDelimiter> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<string>, ProgramSetBuilder<findDelimiter>>(Cluster.CastValue<string>(kvp.Key), ProgramSetBuilder<findDelimiter>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AEAB RID: 44715 RVA: 0x0026ACC7 File Offset: 0x00268EC7
		public static IEnumerable<KeyValuePair<Optional<int>, ProgramSetBuilder<findInstance>>> ClusterOnInput(this ProgramSetBuilder<findInstance> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<int>, ProgramSetBuilder<findInstance>>(Cluster.CastValue<int>(kvp.Key), ProgramSetBuilder<findInstance>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AEAC RID: 44716 RVA: 0x0026ACF9 File Offset: 0x00268EF9
		public static IEnumerable<KeyValuePair<Optional<int>, ProgramSetBuilder<findOffset>>> ClusterOnInput(this ProgramSetBuilder<findOffset> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<int>, ProgramSetBuilder<findOffset>>(Cluster.CastValue<int>(kvp.Key), ProgramSetBuilder<findOffset>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AEAD RID: 44717 RVA: 0x0026AD2B File Offset: 0x00268F2B
		public static IEnumerable<KeyValuePair<Optional<int>, ProgramSetBuilder<slicePrefixAbsPos>>> ClusterOnInput(this ProgramSetBuilder<slicePrefixAbsPos> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<int>, ProgramSetBuilder<slicePrefixAbsPos>>(Cluster.CastValue<int>(kvp.Key), ProgramSetBuilder<slicePrefixAbsPos>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AEAE RID: 44718 RVA: 0x0026AD5D File Offset: 0x00268F5D
		public static IEnumerable<KeyValuePair<Optional<int>, ProgramSetBuilder<scaleNumberFactor>>> ClusterOnInput(this ProgramSetBuilder<scaleNumberFactor> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<int>, ProgramSetBuilder<scaleNumberFactor>>(Cluster.CastValue<int>(kvp.Key), ProgramSetBuilder<scaleNumberFactor>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AEAF RID: 44719 RVA: 0x0026AD8F File Offset: 0x00268F8F
		public static IEnumerable<KeyValuePair<Optional<int>, ProgramSetBuilder<absPos>>> ClusterOnInput(this ProgramSetBuilder<absPos> set, State state)
		{
			return from kvp in set.Set.ClusterOnInput(state)
				select new KeyValuePair<Optional<int>, ProgramSetBuilder<absPos>>(Cluster.CastValue<int>(kvp.Key), ProgramSetBuilder<absPos>.CreateUnsafe(kvp.Value));
		}

		// Token: 0x0600AEB0 RID: 44720 RVA: 0x0026ADC1 File Offset: 0x00268FC1
		public static IEnumerable<KeyValuePair<Optional<object>[], ProgramSetBuilder<result>>> ClusterOnInputTuple(this ProgramSetBuilder<result> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<object>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<object>>(Cluster.CastValue<object>));
				}
				return new KeyValuePair<Optional<object>[], ProgramSetBuilder<result>>(key.Select(func).ToArray<Optional<object>>(), ProgramSetBuilder<result>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEB1 RID: 44721 RVA: 0x0026ADF3 File Offset: 0x00268FF3
		public static IEnumerable<KeyValuePair<Optional<object>[], ProgramSetBuilder<output>>> ClusterOnInputTuple(this ProgramSetBuilder<output> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<object>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<object>>(Cluster.CastValue<object>));
				}
				return new KeyValuePair<Optional<object>[], ProgramSetBuilder<output>>(key.Select(func).ToArray<Optional<object>>(), ProgramSetBuilder<output>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEB2 RID: 44722 RVA: 0x0026AE25 File Offset: 0x00269025
		public static IEnumerable<KeyValuePair<Optional<decimal>[], ProgramSetBuilder<outNumber>>> ClusterOnInputTuple(this ProgramSetBuilder<outNumber> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<decimal>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<decimal>>(Cluster.CastValue<decimal>));
				}
				return new KeyValuePair<Optional<decimal>[], ProgramSetBuilder<outNumber>>(key.Select(func).ToArray<Optional<decimal>>(), ProgramSetBuilder<outNumber>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEB3 RID: 44723 RVA: 0x0026AE57 File Offset: 0x00269057
		public static IEnumerable<KeyValuePair<Optional<DateTime>[], ProgramSetBuilder<outDate>>> ClusterOnInputTuple(this ProgramSetBuilder<outDate> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<DateTime>> func;
				if ((func = Cluster.<>O.<2>__CastValue) == null)
				{
					func = (Cluster.<>O.<2>__CastValue = new Func<object, Optional<DateTime>>(Cluster.CastValue<DateTime>));
				}
				return new KeyValuePair<Optional<DateTime>[], ProgramSetBuilder<outDate>>(key.Select(func).ToArray<Optional<DateTime>>(), ProgramSetBuilder<outDate>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEB4 RID: 44724 RVA: 0x0026AE89 File Offset: 0x00269089
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<outStr>>> ClusterOnInputTuple(this ProgramSetBuilder<outStr> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<outStr>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<outStr>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEB5 RID: 44725 RVA: 0x0026AEBB File Offset: 0x002690BB
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<outStr1>>> ClusterOnInputTuple(this ProgramSetBuilder<outStr1> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<outStr1>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<outStr1>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEB6 RID: 44726 RVA: 0x0026AEED File Offset: 0x002690ED
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<segmentCase>>> ClusterOnInputTuple(this ProgramSetBuilder<segmentCase> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<segmentCase>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<segmentCase>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEB7 RID: 44727 RVA: 0x0026AF1F File Offset: 0x0026911F
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<segment>>> ClusterOnInputTuple(this ProgramSetBuilder<segment> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<segment>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<segment>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEB8 RID: 44728 RVA: 0x0026AF51 File Offset: 0x00269151
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<formatted>>> ClusterOnInputTuple(this ProgramSetBuilder<formatted> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<formatted>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<formatted>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEB9 RID: 44729 RVA: 0x0026AF83 File Offset: 0x00269183
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<concatEntry>>> ClusterOnInputTuple(this ProgramSetBuilder<concatEntry> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<concatEntry>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<concatEntry>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEBA RID: 44730 RVA: 0x0026AFB5 File Offset: 0x002691B5
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<concatCase>>> ClusterOnInputTuple(this ProgramSetBuilder<concatCase> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<concatCase>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<concatCase>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEBB RID: 44731 RVA: 0x0026AFE7 File Offset: 0x002691E7
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<concat>>> ClusterOnInputTuple(this ProgramSetBuilder<concat> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<concat>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<concat>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEBC RID: 44732 RVA: 0x0026B019 File Offset: 0x00269219
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<concatPrefix>>> ClusterOnInputTuple(this ProgramSetBuilder<concatPrefix> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<concatPrefix>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<concatPrefix>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEBD RID: 44733 RVA: 0x0026B04B File Offset: 0x0026924B
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<concatSegment>>> ClusterOnInputTuple(this ProgramSetBuilder<concatSegment> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<concatSegment>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<concatSegment>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEBE RID: 44734 RVA: 0x0026B07D File Offset: 0x0026927D
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<concatSuffix>>> ClusterOnInputTuple(this ProgramSetBuilder<concatSuffix> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<concatSuffix>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<concatSuffix>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEBF RID: 44735 RVA: 0x0026B0AF File Offset: 0x002692AF
		public static IEnumerable<KeyValuePair<Optional<bool>[], ProgramSetBuilder<condition>>> ClusterOnInputTuple(this ProgramSetBuilder<condition> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<bool>> func;
				if ((func = Cluster.<>O.<4>__CastValue) == null)
				{
					func = (Cluster.<>O.<4>__CastValue = new Func<object, Optional<bool>>(Cluster.CastValue<bool>));
				}
				return new KeyValuePair<Optional<bool>[], ProgramSetBuilder<condition>>(key.Select(func).ToArray<Optional<bool>>(), ProgramSetBuilder<condition>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEC0 RID: 44736 RVA: 0x0026B0E1 File Offset: 0x002692E1
		public static IEnumerable<KeyValuePair<Optional<bool>[], ProgramSetBuilder<or>>> ClusterOnInputTuple(this ProgramSetBuilder<or> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<bool>> func;
				if ((func = Cluster.<>O.<4>__CastValue) == null)
				{
					func = (Cluster.<>O.<4>__CastValue = new Func<object, Optional<bool>>(Cluster.CastValue<bool>));
				}
				return new KeyValuePair<Optional<bool>[], ProgramSetBuilder<or>>(key.Select(func).ToArray<Optional<bool>>(), ProgramSetBuilder<or>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEC1 RID: 44737 RVA: 0x0026B113 File Offset: 0x00269313
		public static IEnumerable<KeyValuePair<Optional<object>[], ProgramSetBuilder<inull>>> ClusterOnInputTuple(this ProgramSetBuilder<inull> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<object>> func;
				if ((func = Cluster.<>O.<0>__CastValue) == null)
				{
					func = (Cluster.<>O.<0>__CastValue = new Func<object, Optional<object>>(Cluster.CastValue<object>));
				}
				return new KeyValuePair<Optional<object>[], ProgramSetBuilder<inull>>(key.Select(func).ToArray<Optional<object>>(), ProgramSetBuilder<inull>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEC2 RID: 44738 RVA: 0x0026B145 File Offset: 0x00269345
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<equalsText>>> ClusterOnInputTuple(this ProgramSetBuilder<equalsText> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<equalsText>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<equalsText>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEC3 RID: 44739 RVA: 0x0026B177 File Offset: 0x00269377
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<containsFindText>>> ClusterOnInputTuple(this ProgramSetBuilder<containsFindText> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<containsFindText>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<containsFindText>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEC4 RID: 44740 RVA: 0x0026B1A9 File Offset: 0x002693A9
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<startsWithFindText>>> ClusterOnInputTuple(this ProgramSetBuilder<startsWithFindText> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<startsWithFindText>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<startsWithFindText>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEC5 RID: 44741 RVA: 0x0026B1DB File Offset: 0x002693DB
		public static IEnumerable<KeyValuePair<Optional<Regex>[], ProgramSetBuilder<isMatchRegex>>> ClusterOnInputTuple(this ProgramSetBuilder<isMatchRegex> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Regex>> func;
				if ((func = Cluster.<>O.<5>__CastValue) == null)
				{
					func = (Cluster.<>O.<5>__CastValue = new Func<object, Optional<Regex>>(Cluster.CastValue<Regex>));
				}
				return new KeyValuePair<Optional<Regex>[], ProgramSetBuilder<isMatchRegex>>(key.Select(func).ToArray<Optional<Regex>>(), ProgramSetBuilder<isMatchRegex>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEC6 RID: 44742 RVA: 0x0026B20D File Offset: 0x0026940D
		public static IEnumerable<KeyValuePair<Optional<Regex>[], ProgramSetBuilder<containsMatchRegex>>> ClusterOnInputTuple(this ProgramSetBuilder<containsMatchRegex> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Regex>> func;
				if ((func = Cluster.<>O.<5>__CastValue) == null)
				{
					func = (Cluster.<>O.<5>__CastValue = new Func<object, Optional<Regex>>(Cluster.CastValue<Regex>));
				}
				return new KeyValuePair<Optional<Regex>[], ProgramSetBuilder<containsMatchRegex>>(key.Select(func).ToArray<Optional<Regex>>(), ProgramSetBuilder<containsMatchRegex>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEC7 RID: 44743 RVA: 0x0026B23F File Offset: 0x0026943F
		public static IEnumerable<KeyValuePair<Optional<int>[], ProgramSetBuilder<containsCount>>> ClusterOnInputTuple(this ProgramSetBuilder<containsCount> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<int>> func;
				if ((func = Cluster.<>O.<6>__CastValue) == null)
				{
					func = (Cluster.<>O.<6>__CastValue = new Func<object, Optional<int>>(Cluster.CastValue<int>));
				}
				return new KeyValuePair<Optional<int>[], ProgramSetBuilder<containsCount>>(key.Select(func).ToArray<Optional<int>>(), ProgramSetBuilder<containsCount>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEC8 RID: 44744 RVA: 0x0026B271 File Offset: 0x00269471
		public static IEnumerable<KeyValuePair<Optional<int>[], ProgramSetBuilder<matchCount>>> ClusterOnInputTuple(this ProgramSetBuilder<matchCount> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<int>> func;
				if ((func = Cluster.<>O.<6>__CastValue) == null)
				{
					func = (Cluster.<>O.<6>__CastValue = new Func<object, Optional<int>>(Cluster.CastValue<int>));
				}
				return new KeyValuePair<Optional<int>[], ProgramSetBuilder<matchCount>>(key.Select(func).ToArray<Optional<int>>(), ProgramSetBuilder<matchCount>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEC9 RID: 44745 RVA: 0x0026B2A3 File Offset: 0x002694A3
		public static IEnumerable<KeyValuePair<Optional<decimal>[], ProgramSetBuilder<numberEqualsValue>>> ClusterOnInputTuple(this ProgramSetBuilder<numberEqualsValue> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<decimal>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<decimal>>(Cluster.CastValue<decimal>));
				}
				return new KeyValuePair<Optional<decimal>[], ProgramSetBuilder<numberEqualsValue>>(key.Select(func).ToArray<Optional<decimal>>(), ProgramSetBuilder<numberEqualsValue>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AECA RID: 44746 RVA: 0x0026B2D5 File Offset: 0x002694D5
		public static IEnumerable<KeyValuePair<Optional<decimal>[], ProgramSetBuilder<numberGreaterThanValue>>> ClusterOnInputTuple(this ProgramSetBuilder<numberGreaterThanValue> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<decimal>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<decimal>>(Cluster.CastValue<decimal>));
				}
				return new KeyValuePair<Optional<decimal>[], ProgramSetBuilder<numberGreaterThanValue>>(key.Select(func).ToArray<Optional<decimal>>(), ProgramSetBuilder<numberGreaterThanValue>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AECB RID: 44747 RVA: 0x0026B307 File Offset: 0x00269507
		public static IEnumerable<KeyValuePair<Optional<decimal>[], ProgramSetBuilder<numberLessThanValue>>> ClusterOnInputTuple(this ProgramSetBuilder<numberLessThanValue> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<decimal>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<decimal>>(Cluster.CastValue<decimal>));
				}
				return new KeyValuePair<Optional<decimal>[], ProgramSetBuilder<numberLessThanValue>>(key.Select(func).ToArray<Optional<decimal>>(), ProgramSetBuilder<numberLessThanValue>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AECC RID: 44748 RVA: 0x0026B339 File Offset: 0x00269539
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<formatNumber>>> ClusterOnInputTuple(this ProgramSetBuilder<formatNumber> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<formatNumber>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<formatNumber>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AECD RID: 44749 RVA: 0x0026B36B File Offset: 0x0026956B
		public static IEnumerable<KeyValuePair<Optional<decimal>[], ProgramSetBuilder<number>>> ClusterOnInputTuple(this ProgramSetBuilder<number> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<decimal>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<decimal>>(Cluster.CastValue<decimal>));
				}
				return new KeyValuePair<Optional<decimal>[], ProgramSetBuilder<number>>(key.Select(func).ToArray<Optional<decimal>>(), ProgramSetBuilder<number>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AECE RID: 44750 RVA: 0x0026B39D File Offset: 0x0026959D
		public static IEnumerable<KeyValuePair<Optional<decimal>[], ProgramSetBuilder<number1>>> ClusterOnInputTuple(this ProgramSetBuilder<number1> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<decimal>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<decimal>>(Cluster.CastValue<decimal>));
				}
				return new KeyValuePair<Optional<decimal>[], ProgramSetBuilder<number1>>(key.Select(func).ToArray<Optional<decimal>>(), ProgramSetBuilder<number1>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AECF RID: 44751 RVA: 0x0026B3CF File Offset: 0x002695CF
		public static IEnumerable<KeyValuePair<Optional<decimal>[], ProgramSetBuilder<arithmetic>>> ClusterOnInputTuple(this ProgramSetBuilder<arithmetic> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<decimal>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<decimal>>(Cluster.CastValue<decimal>));
				}
				return new KeyValuePair<Optional<decimal>[], ProgramSetBuilder<arithmetic>>(key.Select(func).ToArray<Optional<decimal>>(), ProgramSetBuilder<arithmetic>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AED0 RID: 44752 RVA: 0x0026B401 File Offset: 0x00269601
		public static IEnumerable<KeyValuePair<Optional<decimal>[], ProgramSetBuilder<arithmeticLeft>>> ClusterOnInputTuple(this ProgramSetBuilder<arithmeticLeft> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<decimal>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<decimal>>(Cluster.CastValue<decimal>));
				}
				return new KeyValuePair<Optional<decimal>[], ProgramSetBuilder<arithmeticLeft>>(key.Select(func).ToArray<Optional<decimal>>(), ProgramSetBuilder<arithmeticLeft>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AED1 RID: 44753 RVA: 0x0026B433 File Offset: 0x00269633
		public static IEnumerable<KeyValuePair<Optional<decimal>[], ProgramSetBuilder<addRight>>> ClusterOnInputTuple(this ProgramSetBuilder<addRight> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<decimal>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<decimal>>(Cluster.CastValue<decimal>));
				}
				return new KeyValuePair<Optional<decimal>[], ProgramSetBuilder<addRight>>(key.Select(func).ToArray<Optional<decimal>>(), ProgramSetBuilder<addRight>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AED2 RID: 44754 RVA: 0x0026B465 File Offset: 0x00269665
		public static IEnumerable<KeyValuePair<Optional<decimal>[], ProgramSetBuilder<subtractRight>>> ClusterOnInputTuple(this ProgramSetBuilder<subtractRight> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<decimal>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<decimal>>(Cluster.CastValue<decimal>));
				}
				return new KeyValuePair<Optional<decimal>[], ProgramSetBuilder<subtractRight>>(key.Select(func).ToArray<Optional<decimal>>(), ProgramSetBuilder<subtractRight>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AED3 RID: 44755 RVA: 0x0026B497 File Offset: 0x00269697
		public static IEnumerable<KeyValuePair<Optional<decimal>[], ProgramSetBuilder<multiplyRight>>> ClusterOnInputTuple(this ProgramSetBuilder<multiplyRight> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<decimal>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<decimal>>(Cluster.CastValue<decimal>));
				}
				return new KeyValuePair<Optional<decimal>[], ProgramSetBuilder<multiplyRight>>(key.Select(func).ToArray<Optional<decimal>>(), ProgramSetBuilder<multiplyRight>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AED4 RID: 44756 RVA: 0x0026B4C9 File Offset: 0x002696C9
		public static IEnumerable<KeyValuePair<Optional<decimal>[], ProgramSetBuilder<divideRight>>> ClusterOnInputTuple(this ProgramSetBuilder<divideRight> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<decimal>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<decimal>>(Cluster.CastValue<decimal>));
				}
				return new KeyValuePair<Optional<decimal>[], ProgramSetBuilder<divideRight>>(key.Select(func).ToArray<Optional<decimal>>(), ProgramSetBuilder<divideRight>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AED5 RID: 44757 RVA: 0x0026B4FB File Offset: 0x002696FB
		public static IEnumerable<KeyValuePair<Optional<decimal>[], ProgramSetBuilder<inumber>>> ClusterOnInputTuple(this ProgramSetBuilder<inumber> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<decimal>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<decimal>>(Cluster.CastValue<decimal>));
				}
				return new KeyValuePair<Optional<decimal>[], ProgramSetBuilder<inumber>>(key.Select(func).ToArray<Optional<decimal>>(), ProgramSetBuilder<inumber>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AED6 RID: 44758 RVA: 0x0026B52D File Offset: 0x0026972D
		public static IEnumerable<KeyValuePair<Optional<decimal>[], ProgramSetBuilder<rowNumberTransform>>> ClusterOnInputTuple(this ProgramSetBuilder<rowNumberTransform> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<decimal>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<decimal>>(Cluster.CastValue<decimal>));
				}
				return new KeyValuePair<Optional<decimal>[], ProgramSetBuilder<rowNumberTransform>>(key.Select(func).ToArray<Optional<decimal>>(), ProgramSetBuilder<rowNumberTransform>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AED7 RID: 44759 RVA: 0x0026B55F File Offset: 0x0026975F
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<formatDateTime>>> ClusterOnInputTuple(this ProgramSetBuilder<formatDateTime> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<formatDateTime>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<formatDateTime>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AED8 RID: 44760 RVA: 0x0026B591 File Offset: 0x00269791
		public static IEnumerable<KeyValuePair<Optional<DateTime>[], ProgramSetBuilder<date>>> ClusterOnInputTuple(this ProgramSetBuilder<date> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<DateTime>> func;
				if ((func = Cluster.<>O.<2>__CastValue) == null)
				{
					func = (Cluster.<>O.<2>__CastValue = new Func<object, Optional<DateTime>>(Cluster.CastValue<DateTime>));
				}
				return new KeyValuePair<Optional<DateTime>[], ProgramSetBuilder<date>>(key.Select(func).ToArray<Optional<DateTime>>(), ProgramSetBuilder<date>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AED9 RID: 44761 RVA: 0x0026B5C3 File Offset: 0x002697C3
		public static IEnumerable<KeyValuePair<Optional<DateTime>[], ProgramSetBuilder<idate>>> ClusterOnInputTuple(this ProgramSetBuilder<idate> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<DateTime>> func;
				if ((func = Cluster.<>O.<2>__CastValue) == null)
				{
					func = (Cluster.<>O.<2>__CastValue = new Func<object, Optional<DateTime>>(Cluster.CastValue<DateTime>));
				}
				return new KeyValuePair<Optional<DateTime>[], ProgramSetBuilder<idate>>(key.Select(func).ToArray<Optional<DateTime>>(), ProgramSetBuilder<idate>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEDA RID: 44762 RVA: 0x0026B5F5 File Offset: 0x002697F5
		public static IEnumerable<KeyValuePair<Optional<Time>[], ProgramSetBuilder<itime>>> ClusterOnInputTuple(this ProgramSetBuilder<itime> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Time>> func;
				if ((func = Cluster.<>O.<7>__CastValue) == null)
				{
					func = (Cluster.<>O.<7>__CastValue = new Func<object, Optional<Time>>(Cluster.CastValue<Time>));
				}
				return new KeyValuePair<Optional<Time>[], ProgramSetBuilder<itime>>(key.Select(func).ToArray<Optional<Time>>(), ProgramSetBuilder<itime>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEDB RID: 44763 RVA: 0x0026B627 File Offset: 0x00269827
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<parseSubject>>> ClusterOnInputTuple(this ProgramSetBuilder<parseSubject> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<parseSubject>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<parseSubject>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEDC RID: 44764 RVA: 0x0026B659 File Offset: 0x00269859
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<letSubstring>>> ClusterOnInputTuple(this ProgramSetBuilder<letSubstring> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<letSubstring>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<letSubstring>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEDD RID: 44765 RVA: 0x0026B68B File Offset: 0x0026988B
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<substring>>> ClusterOnInputTuple(this ProgramSetBuilder<substring> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<substring>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<substring>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEDE RID: 44766 RVA: 0x0026B6BD File Offset: 0x002698BD
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<splitTrim>>> ClusterOnInputTuple(this ProgramSetBuilder<splitTrim> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<splitTrim>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<splitTrim>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEDF RID: 44767 RVA: 0x0026B6EF File Offset: 0x002698EF
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<split>>> ClusterOnInputTuple(this ProgramSetBuilder<split> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<split>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<split>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEE0 RID: 44768 RVA: 0x0026B721 File Offset: 0x00269921
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<sliceTrim>>> ClusterOnInputTuple(this ProgramSetBuilder<sliceTrim> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<sliceTrim>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<sliceTrim>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEE1 RID: 44769 RVA: 0x0026B753 File Offset: 0x00269953
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<slice>>> ClusterOnInputTuple(this ProgramSetBuilder<slice> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<slice>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<slice>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEE2 RID: 44770 RVA: 0x0026B785 File Offset: 0x00269985
		public static IEnumerable<KeyValuePair<Optional<int>[], ProgramSetBuilder<pos>>> ClusterOnInputTuple(this ProgramSetBuilder<pos> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<int>> func;
				if ((func = Cluster.<>O.<6>__CastValue) == null)
				{
					func = (Cluster.<>O.<6>__CastValue = new Func<object, Optional<int>>(Cluster.CastValue<int>));
				}
				return new KeyValuePair<Optional<int>[], ProgramSetBuilder<pos>>(key.Select(func).ToArray<Optional<int>>(), ProgramSetBuilder<pos>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEE3 RID: 44771 RVA: 0x0026B7B7 File Offset: 0x002699B7
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<fromStrTrim>>> ClusterOnInputTuple(this ProgramSetBuilder<fromStrTrim> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<fromStrTrim>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<fromStrTrim>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEE4 RID: 44772 RVA: 0x0026B7E9 File Offset: 0x002699E9
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<fromStr>>> ClusterOnInputTuple(this ProgramSetBuilder<fromStr> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<fromStr>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<fromStr>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEE5 RID: 44773 RVA: 0x0026B81B File Offset: 0x00269A1B
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<fromNumberStr>>> ClusterOnInputTuple(this ProgramSetBuilder<fromNumberStr> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<fromNumberStr>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<fromNumberStr>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEE6 RID: 44774 RVA: 0x0026B84D File Offset: 0x00269A4D
		public static IEnumerable<KeyValuePair<Optional<decimal>[], ProgramSetBuilder<fromNumber>>> ClusterOnInputTuple(this ProgramSetBuilder<fromNumber> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<decimal>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<decimal>>(Cluster.CastValue<decimal>));
				}
				return new KeyValuePair<Optional<decimal>[], ProgramSetBuilder<fromNumber>>(key.Select(func).ToArray<Optional<decimal>>(), ProgramSetBuilder<fromNumber>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEE7 RID: 44775 RVA: 0x0026B87F File Offset: 0x00269A7F
		public static IEnumerable<KeyValuePair<Optional<decimal>[], ProgramSetBuilder<fromNumberCoalesced>>> ClusterOnInputTuple(this ProgramSetBuilder<fromNumberCoalesced> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<decimal>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<decimal>>(Cluster.CastValue<decimal>));
				}
				return new KeyValuePair<Optional<decimal>[], ProgramSetBuilder<fromNumberCoalesced>>(key.Select(func).ToArray<Optional<decimal>>(), ProgramSetBuilder<fromNumberCoalesced>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEE8 RID: 44776 RVA: 0x0026B8B1 File Offset: 0x00269AB1
		public static IEnumerable<KeyValuePair<Optional<int>[], ProgramSetBuilder<fromRowNumber>>> ClusterOnInputTuple(this ProgramSetBuilder<fromRowNumber> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<int>> func;
				if ((func = Cluster.<>O.<6>__CastValue) == null)
				{
					func = (Cluster.<>O.<6>__CastValue = new Func<object, Optional<int>>(Cluster.CastValue<int>));
				}
				return new KeyValuePair<Optional<int>[], ProgramSetBuilder<fromRowNumber>>(key.Select(func).ToArray<Optional<int>>(), ProgramSetBuilder<fromRowNumber>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEE9 RID: 44777 RVA: 0x0026B8E3 File Offset: 0x00269AE3
		public static IEnumerable<KeyValuePair<Optional<decimal[]>[], ProgramSetBuilder<fromNumbers>>> ClusterOnInputTuple(this ProgramSetBuilder<fromNumbers> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<decimal[]>> func;
				if ((func = Cluster.<>O.<8>__CastValue) == null)
				{
					func = (Cluster.<>O.<8>__CastValue = new Func<object, Optional<decimal[]>>(Cluster.CastValue<decimal[]>));
				}
				return new KeyValuePair<Optional<decimal[]>[], ProgramSetBuilder<fromNumbers>>(key.Select(func).ToArray<Optional<decimal[]>>(), ProgramSetBuilder<fromNumbers>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEEA RID: 44778 RVA: 0x0026B915 File Offset: 0x00269B15
		public static IEnumerable<KeyValuePair<Optional<DateTime>[], ProgramSetBuilder<fromDateTime>>> ClusterOnInputTuple(this ProgramSetBuilder<fromDateTime> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<DateTime>> func;
				if ((func = Cluster.<>O.<2>__CastValue) == null)
				{
					func = (Cluster.<>O.<2>__CastValue = new Func<object, Optional<DateTime>>(Cluster.CastValue<DateTime>));
				}
				return new KeyValuePair<Optional<DateTime>[], ProgramSetBuilder<fromDateTime>>(key.Select(func).ToArray<Optional<DateTime>>(), ProgramSetBuilder<fromDateTime>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEEB RID: 44779 RVA: 0x0026B947 File Offset: 0x00269B47
		public static IEnumerable<KeyValuePair<Optional<DateTime>[], ProgramSetBuilder<fromDateTimePart>>> ClusterOnInputTuple(this ProgramSetBuilder<fromDateTimePart> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<DateTime>> func;
				if ((func = Cluster.<>O.<2>__CastValue) == null)
				{
					func = (Cluster.<>O.<2>__CastValue = new Func<object, Optional<DateTime>>(Cluster.CastValue<DateTime>));
				}
				return new KeyValuePair<Optional<DateTime>[], ProgramSetBuilder<fromDateTimePart>>(key.Select(func).ToArray<Optional<DateTime>>(), ProgramSetBuilder<fromDateTimePart>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEEC RID: 44780 RVA: 0x0026B979 File Offset: 0x00269B79
		public static IEnumerable<KeyValuePair<Optional<Time>[], ProgramSetBuilder<fromTime>>> ClusterOnInputTuple(this ProgramSetBuilder<fromTime> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<Time>> func;
				if ((func = Cluster.<>O.<7>__CastValue) == null)
				{
					func = (Cluster.<>O.<7>__CastValue = new Func<object, Optional<Time>>(Cluster.CastValue<Time>));
				}
				return new KeyValuePair<Optional<Time>[], ProgramSetBuilder<fromTime>>(key.Select(func).ToArray<Optional<Time>>(), ProgramSetBuilder<fromTime>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEED RID: 44781 RVA: 0x0026B9AB File Offset: 0x00269BAB
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<constString>>> ClusterOnInputTuple(this ProgramSetBuilder<constString> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<constString>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<constString>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEEE RID: 44782 RVA: 0x0026B9DD File Offset: 0x00269BDD
		public static IEnumerable<KeyValuePair<Optional<decimal>[], ProgramSetBuilder<constNumber>>> ClusterOnInputTuple(this ProgramSetBuilder<constNumber> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<decimal>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<decimal>>(Cluster.CastValue<decimal>));
				}
				return new KeyValuePair<Optional<decimal>[], ProgramSetBuilder<constNumber>>(key.Select(func).ToArray<Optional<decimal>>(), ProgramSetBuilder<constNumber>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEEF RID: 44783 RVA: 0x0026BA0F File Offset: 0x00269C0F
		public static IEnumerable<KeyValuePair<Optional<DateTime>[], ProgramSetBuilder<constDate>>> ClusterOnInputTuple(this ProgramSetBuilder<constDate> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<DateTime>> func;
				if ((func = Cluster.<>O.<2>__CastValue) == null)
				{
					func = (Cluster.<>O.<2>__CastValue = new Func<object, Optional<DateTime>>(Cluster.CastValue<DateTime>));
				}
				return new KeyValuePair<Optional<DateTime>[], ProgramSetBuilder<constDate>>(key.Select(func).ToArray<Optional<DateTime>>(), ProgramSetBuilder<constDate>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEF0 RID: 44784 RVA: 0x0026BA41 File Offset: 0x00269C41
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<columnName>>> ClusterOnInputTuple(this ProgramSetBuilder<columnName> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<columnName>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<columnName>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEF1 RID: 44785 RVA: 0x0026BA73 File Offset: 0x00269C73
		public static IEnumerable<KeyValuePair<Optional<string[]>[], ProgramSetBuilder<columnNames>>> ClusterOnInputTuple(this ProgramSetBuilder<columnNames> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string[]>> func;
				if ((func = Cluster.<>O.<9>__CastValue) == null)
				{
					func = (Cluster.<>O.<9>__CastValue = new Func<object, Optional<string[]>>(Cluster.CastValue<string[]>));
				}
				return new KeyValuePair<Optional<string[]>[], ProgramSetBuilder<columnNames>>(key.Select(func).ToArray<Optional<string[]>>(), ProgramSetBuilder<columnNames>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEF2 RID: 44786 RVA: 0x0026BAA5 File Offset: 0x00269CA5
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<constStr>>> ClusterOnInputTuple(this ProgramSetBuilder<constStr> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<constStr>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<constStr>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEF3 RID: 44787 RVA: 0x0026BAD7 File Offset: 0x00269CD7
		public static IEnumerable<KeyValuePair<Optional<decimal>[], ProgramSetBuilder<constNum>>> ClusterOnInputTuple(this ProgramSetBuilder<constNum> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<decimal>> func;
				if ((func = Cluster.<>O.<1>__CastValue) == null)
				{
					func = (Cluster.<>O.<1>__CastValue = new Func<object, Optional<decimal>>(Cluster.CastValue<decimal>));
				}
				return new KeyValuePair<Optional<decimal>[], ProgramSetBuilder<constNum>>(key.Select(func).ToArray<Optional<decimal>>(), ProgramSetBuilder<constNum>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEF4 RID: 44788 RVA: 0x0026BB09 File Offset: 0x00269D09
		public static IEnumerable<KeyValuePair<Optional<DateTime>[], ProgramSetBuilder<constDt>>> ClusterOnInputTuple(this ProgramSetBuilder<constDt> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<DateTime>> func;
				if ((func = Cluster.<>O.<2>__CastValue) == null)
				{
					func = (Cluster.<>O.<2>__CastValue = new Func<object, Optional<DateTime>>(Cluster.CastValue<DateTime>));
				}
				return new KeyValuePair<Optional<DateTime>[], ProgramSetBuilder<constDt>>(key.Select(func).ToArray<Optional<DateTime>>(), ProgramSetBuilder<constDt>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEF5 RID: 44789 RVA: 0x0026BB3B File Offset: 0x00269D3B
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<locale>>> ClusterOnInputTuple(this ProgramSetBuilder<locale> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<locale>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<locale>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEF6 RID: 44790 RVA: 0x0026BB6D File Offset: 0x00269D6D
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<replaceFindText>>> ClusterOnInputTuple(this ProgramSetBuilder<replaceFindText> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<replaceFindText>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<replaceFindText>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEF7 RID: 44791 RVA: 0x0026BB9F File Offset: 0x00269D9F
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<replaceText>>> ClusterOnInputTuple(this ProgramSetBuilder<replaceText> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<replaceText>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<replaceText>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEF8 RID: 44792 RVA: 0x0026BBD1 File Offset: 0x00269DD1
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<sliceBetweenStartText>>> ClusterOnInputTuple(this ProgramSetBuilder<sliceBetweenStartText> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<sliceBetweenStartText>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<sliceBetweenStartText>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEF9 RID: 44793 RVA: 0x0026BC03 File Offset: 0x00269E03
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<sliceBetweenEndText>>> ClusterOnInputTuple(this ProgramSetBuilder<sliceBetweenEndText> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<sliceBetweenEndText>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<sliceBetweenEndText>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEFA RID: 44794 RVA: 0x0026BC35 File Offset: 0x00269E35
		public static IEnumerable<KeyValuePair<Optional<FormatNumberDescriptor>[], ProgramSetBuilder<numberFormatDesc>>> ClusterOnInputTuple(this ProgramSetBuilder<numberFormatDesc> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<FormatNumberDescriptor>> func;
				if ((func = Cluster.<>O.<10>__CastValue) == null)
				{
					func = (Cluster.<>O.<10>__CastValue = new Func<object, Optional<FormatNumberDescriptor>>(Cluster.CastValue<FormatNumberDescriptor>));
				}
				return new KeyValuePair<Optional<FormatNumberDescriptor>[], ProgramSetBuilder<numberFormatDesc>>(key.Select(func).ToArray<Optional<FormatNumberDescriptor>>(), ProgramSetBuilder<numberFormatDesc>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEFB RID: 44795 RVA: 0x0026BC67 File Offset: 0x00269E67
		public static IEnumerable<KeyValuePair<Optional<RoundNumberDescriptor>[], ProgramSetBuilder<numberRoundDesc>>> ClusterOnInputTuple(this ProgramSetBuilder<numberRoundDesc> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<RoundNumberDescriptor>> func;
				if ((func = Cluster.<>O.<11>__CastValue) == null)
				{
					func = (Cluster.<>O.<11>__CastValue = new Func<object, Optional<RoundNumberDescriptor>>(Cluster.CastValue<RoundNumberDescriptor>));
				}
				return new KeyValuePair<Optional<RoundNumberDescriptor>[], ProgramSetBuilder<numberRoundDesc>>(key.Select(func).ToArray<Optional<RoundNumberDescriptor>>(), ProgramSetBuilder<numberRoundDesc>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEFC RID: 44796 RVA: 0x0026BC99 File Offset: 0x00269E99
		public static IEnumerable<KeyValuePair<Optional<RoundDateTimeDescriptor>[], ProgramSetBuilder<dateTimeRoundDesc>>> ClusterOnInputTuple(this ProgramSetBuilder<dateTimeRoundDesc> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<RoundDateTimeDescriptor>> func;
				if ((func = Cluster.<>O.<12>__CastValue) == null)
				{
					func = (Cluster.<>O.<12>__CastValue = new Func<object, Optional<RoundDateTimeDescriptor>>(Cluster.CastValue<RoundDateTimeDescriptor>));
				}
				return new KeyValuePair<Optional<RoundDateTimeDescriptor>[], ProgramSetBuilder<dateTimeRoundDesc>>(key.Select(func).ToArray<Optional<RoundDateTimeDescriptor>>(), ProgramSetBuilder<dateTimeRoundDesc>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEFD RID: 44797 RVA: 0x0026BCCB File Offset: 0x00269ECB
		public static IEnumerable<KeyValuePair<Optional<DateTimeDescriptor>[], ProgramSetBuilder<dateTimeFormatDesc>>> ClusterOnInputTuple(this ProgramSetBuilder<dateTimeFormatDesc> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<DateTimeDescriptor>> func;
				if ((func = Cluster.<>O.<13>__CastValue) == null)
				{
					func = (Cluster.<>O.<13>__CastValue = new Func<object, Optional<DateTimeDescriptor>>(Cluster.CastValue<DateTimeDescriptor>));
				}
				return new KeyValuePair<Optional<DateTimeDescriptor>[], ProgramSetBuilder<dateTimeFormatDesc>>(key.Select(func).ToArray<Optional<DateTimeDescriptor>>(), ProgramSetBuilder<dateTimeFormatDesc>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEFE RID: 44798 RVA: 0x0026BCFD File Offset: 0x00269EFD
		public static IEnumerable<KeyValuePair<Optional<DateTimeDescriptor>[], ProgramSetBuilder<dateTimeParseDesc>>> ClusterOnInputTuple(this ProgramSetBuilder<dateTimeParseDesc> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<DateTimeDescriptor>> func;
				if ((func = Cluster.<>O.<13>__CastValue) == null)
				{
					func = (Cluster.<>O.<13>__CastValue = new Func<object, Optional<DateTimeDescriptor>>(Cluster.CastValue<DateTimeDescriptor>));
				}
				return new KeyValuePair<Optional<DateTimeDescriptor>[], ProgramSetBuilder<dateTimeParseDesc>>(key.Select(func).ToArray<Optional<DateTimeDescriptor>>(), ProgramSetBuilder<dateTimeParseDesc>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AEFF RID: 44799 RVA: 0x0026BD2F File Offset: 0x00269F2F
		public static IEnumerable<KeyValuePair<Optional<DateTimePartKind>[], ProgramSetBuilder<dateTimePartKind>>> ClusterOnInputTuple(this ProgramSetBuilder<dateTimePartKind> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<DateTimePartKind>> func;
				if ((func = Cluster.<>O.<14>__CastValue) == null)
				{
					func = (Cluster.<>O.<14>__CastValue = new Func<object, Optional<DateTimePartKind>>(Cluster.CastValue<DateTimePartKind>));
				}
				return new KeyValuePair<Optional<DateTimePartKind>[], ProgramSetBuilder<dateTimePartKind>>(key.Select(func).ToArray<Optional<DateTimePartKind>>(), ProgramSetBuilder<dateTimePartKind>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AF00 RID: 44800 RVA: 0x0026BD61 File Offset: 0x00269F61
		public static IEnumerable<KeyValuePair<Optional<DateTimePartKind>[], ProgramSetBuilder<fromDateTimePartKind>>> ClusterOnInputTuple(this ProgramSetBuilder<fromDateTimePartKind> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<DateTimePartKind>> func;
				if ((func = Cluster.<>O.<14>__CastValue) == null)
				{
					func = (Cluster.<>O.<14>__CastValue = new Func<object, Optional<DateTimePartKind>>(Cluster.CastValue<DateTimePartKind>));
				}
				return new KeyValuePair<Optional<DateTimePartKind>[], ProgramSetBuilder<fromDateTimePartKind>>(key.Select(func).ToArray<Optional<DateTimePartKind>>(), ProgramSetBuilder<fromDateTimePartKind>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AF01 RID: 44801 RVA: 0x0026BD93 File Offset: 0x00269F93
		public static IEnumerable<KeyValuePair<Optional<TimePartKind>[], ProgramSetBuilder<timePartKind>>> ClusterOnInputTuple(this ProgramSetBuilder<timePartKind> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<TimePartKind>> func;
				if ((func = Cluster.<>O.<15>__CastValue) == null)
				{
					func = (Cluster.<>O.<15>__CastValue = new Func<object, Optional<TimePartKind>>(Cluster.CastValue<TimePartKind>));
				}
				return new KeyValuePair<Optional<TimePartKind>[], ProgramSetBuilder<timePartKind>>(key.Select(func).ToArray<Optional<TimePartKind>>(), ProgramSetBuilder<timePartKind>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AF02 RID: 44802 RVA: 0x0026BDC5 File Offset: 0x00269FC5
		public static IEnumerable<KeyValuePair<Optional<RowNumberLinearTransformDescriptor>[], ProgramSetBuilder<rowNumberLinearTransformDesc>>> ClusterOnInputTuple(this ProgramSetBuilder<rowNumberLinearTransformDesc> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<RowNumberLinearTransformDescriptor>> func;
				if ((func = Cluster.<>O.<16>__CastValue) == null)
				{
					func = (Cluster.<>O.<16>__CastValue = new Func<object, Optional<RowNumberLinearTransformDescriptor>>(Cluster.CastValue<RowNumberLinearTransformDescriptor>));
				}
				return new KeyValuePair<Optional<RowNumberLinearTransformDescriptor>[], ProgramSetBuilder<rowNumberLinearTransformDesc>>(key.Select(func).ToArray<Optional<RowNumberLinearTransformDescriptor>>(), ProgramSetBuilder<rowNumberLinearTransformDesc>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AF03 RID: 44803 RVA: 0x0026BDF7 File Offset: 0x00269FF7
		public static IEnumerable<KeyValuePair<Optional<MatchDescriptor>[], ProgramSetBuilder<matchDesc>>> ClusterOnInputTuple(this ProgramSetBuilder<matchDesc> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<MatchDescriptor>> func;
				if ((func = Cluster.<>O.<17>__CastValue) == null)
				{
					func = (Cluster.<>O.<17>__CastValue = new Func<object, Optional<MatchDescriptor>>(Cluster.CastValue<MatchDescriptor>));
				}
				return new KeyValuePair<Optional<MatchDescriptor>[], ProgramSetBuilder<matchDesc>>(key.Select(func).ToArray<Optional<MatchDescriptor>>(), ProgramSetBuilder<matchDesc>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AF04 RID: 44804 RVA: 0x0026BE29 File Offset: 0x0026A029
		public static IEnumerable<KeyValuePair<Optional<int>[], ProgramSetBuilder<matchInstance>>> ClusterOnInputTuple(this ProgramSetBuilder<matchInstance> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<int>> func;
				if ((func = Cluster.<>O.<6>__CastValue) == null)
				{
					func = (Cluster.<>O.<6>__CastValue = new Func<object, Optional<int>>(Cluster.CastValue<int>));
				}
				return new KeyValuePair<Optional<int>[], ProgramSetBuilder<matchInstance>>(key.Select(func).ToArray<Optional<int>>(), ProgramSetBuilder<matchInstance>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AF05 RID: 44805 RVA: 0x0026BE5B File Offset: 0x0026A05B
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<splitDelimiter>>> ClusterOnInputTuple(this ProgramSetBuilder<splitDelimiter> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<splitDelimiter>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<splitDelimiter>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AF06 RID: 44806 RVA: 0x0026BE8D File Offset: 0x0026A08D
		public static IEnumerable<KeyValuePair<Optional<int>[], ProgramSetBuilder<splitInstance>>> ClusterOnInputTuple(this ProgramSetBuilder<splitInstance> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<int>> func;
				if ((func = Cluster.<>O.<6>__CastValue) == null)
				{
					func = (Cluster.<>O.<6>__CastValue = new Func<object, Optional<int>>(Cluster.CastValue<int>));
				}
				return new KeyValuePair<Optional<int>[], ProgramSetBuilder<splitInstance>>(key.Select(func).ToArray<Optional<int>>(), ProgramSetBuilder<splitInstance>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AF07 RID: 44807 RVA: 0x0026BEBF File Offset: 0x0026A0BF
		public static IEnumerable<KeyValuePair<Optional<string>[], ProgramSetBuilder<findDelimiter>>> ClusterOnInputTuple(this ProgramSetBuilder<findDelimiter> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<string>> func;
				if ((func = Cluster.<>O.<3>__CastValue) == null)
				{
					func = (Cluster.<>O.<3>__CastValue = new Func<object, Optional<string>>(Cluster.CastValue<string>));
				}
				return new KeyValuePair<Optional<string>[], ProgramSetBuilder<findDelimiter>>(key.Select(func).ToArray<Optional<string>>(), ProgramSetBuilder<findDelimiter>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AF08 RID: 44808 RVA: 0x0026BEF1 File Offset: 0x0026A0F1
		public static IEnumerable<KeyValuePair<Optional<int>[], ProgramSetBuilder<findInstance>>> ClusterOnInputTuple(this ProgramSetBuilder<findInstance> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<int>> func;
				if ((func = Cluster.<>O.<6>__CastValue) == null)
				{
					func = (Cluster.<>O.<6>__CastValue = new Func<object, Optional<int>>(Cluster.CastValue<int>));
				}
				return new KeyValuePair<Optional<int>[], ProgramSetBuilder<findInstance>>(key.Select(func).ToArray<Optional<int>>(), ProgramSetBuilder<findInstance>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AF09 RID: 44809 RVA: 0x0026BF23 File Offset: 0x0026A123
		public static IEnumerable<KeyValuePair<Optional<int>[], ProgramSetBuilder<findOffset>>> ClusterOnInputTuple(this ProgramSetBuilder<findOffset> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<int>> func;
				if ((func = Cluster.<>O.<6>__CastValue) == null)
				{
					func = (Cluster.<>O.<6>__CastValue = new Func<object, Optional<int>>(Cluster.CastValue<int>));
				}
				return new KeyValuePair<Optional<int>[], ProgramSetBuilder<findOffset>>(key.Select(func).ToArray<Optional<int>>(), ProgramSetBuilder<findOffset>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AF0A RID: 44810 RVA: 0x0026BF55 File Offset: 0x0026A155
		public static IEnumerable<KeyValuePair<Optional<int>[], ProgramSetBuilder<slicePrefixAbsPos>>> ClusterOnInputTuple(this ProgramSetBuilder<slicePrefixAbsPos> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<int>> func;
				if ((func = Cluster.<>O.<6>__CastValue) == null)
				{
					func = (Cluster.<>O.<6>__CastValue = new Func<object, Optional<int>>(Cluster.CastValue<int>));
				}
				return new KeyValuePair<Optional<int>[], ProgramSetBuilder<slicePrefixAbsPos>>(key.Select(func).ToArray<Optional<int>>(), ProgramSetBuilder<slicePrefixAbsPos>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AF0B RID: 44811 RVA: 0x0026BF87 File Offset: 0x0026A187
		public static IEnumerable<KeyValuePair<Optional<int>[], ProgramSetBuilder<scaleNumberFactor>>> ClusterOnInputTuple(this ProgramSetBuilder<scaleNumberFactor> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<int>> func;
				if ((func = Cluster.<>O.<6>__CastValue) == null)
				{
					func = (Cluster.<>O.<6>__CastValue = new Func<object, Optional<int>>(Cluster.CastValue<int>));
				}
				return new KeyValuePair<Optional<int>[], ProgramSetBuilder<scaleNumberFactor>>(key.Select(func).ToArray<Optional<int>>(), ProgramSetBuilder<scaleNumberFactor>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x0600AF0C RID: 44812 RVA: 0x0026BFB9 File Offset: 0x0026A1B9
		public static IEnumerable<KeyValuePair<Optional<int>[], ProgramSetBuilder<absPos>>> ClusterOnInputTuple(this ProgramSetBuilder<absPos> set, IEnumerable<State> states)
		{
			return set.Set.ClusterOnInputTuple(states).Select(delegate(KeyValuePair<object[], ProgramSet> kvp)
			{
				IEnumerable<object> key = kvp.Key;
				Func<object, Optional<int>> func;
				if ((func = Cluster.<>O.<6>__CastValue) == null)
				{
					func = (Cluster.<>O.<6>__CastValue = new Func<object, Optional<int>>(Cluster.CastValue<int>));
				}
				return new KeyValuePair<Optional<int>[], ProgramSetBuilder<absPos>>(key.Select(func).ToArray<Optional<int>>(), ProgramSetBuilder<absPos>.CreateUnsafe(kvp.Value));
			});
		}

		// Token: 0x02001513 RID: 5395
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040044F6 RID: 17654
			public static Func<object, Optional<object>> <0>__CastValue;

			// Token: 0x040044F7 RID: 17655
			public static Func<object, Optional<decimal>> <1>__CastValue;

			// Token: 0x040044F8 RID: 17656
			public static Func<object, Optional<DateTime>> <2>__CastValue;

			// Token: 0x040044F9 RID: 17657
			public static Func<object, Optional<string>> <3>__CastValue;

			// Token: 0x040044FA RID: 17658
			public static Func<object, Optional<bool>> <4>__CastValue;

			// Token: 0x040044FB RID: 17659
			public static Func<object, Optional<Regex>> <5>__CastValue;

			// Token: 0x040044FC RID: 17660
			public static Func<object, Optional<int>> <6>__CastValue;

			// Token: 0x040044FD RID: 17661
			public static Func<object, Optional<Time>> <7>__CastValue;

			// Token: 0x040044FE RID: 17662
			public static Func<object, Optional<decimal[]>> <8>__CastValue;

			// Token: 0x040044FF RID: 17663
			public static Func<object, Optional<string[]>> <9>__CastValue;

			// Token: 0x04004500 RID: 17664
			public static Func<object, Optional<FormatNumberDescriptor>> <10>__CastValue;

			// Token: 0x04004501 RID: 17665
			public static Func<object, Optional<RoundNumberDescriptor>> <11>__CastValue;

			// Token: 0x04004502 RID: 17666
			public static Func<object, Optional<RoundDateTimeDescriptor>> <12>__CastValue;

			// Token: 0x04004503 RID: 17667
			public static Func<object, Optional<DateTimeDescriptor>> <13>__CastValue;

			// Token: 0x04004504 RID: 17668
			public static Func<object, Optional<DateTimePartKind>> <14>__CastValue;

			// Token: 0x04004505 RID: 17669
			public static Func<object, Optional<TimePartKind>> <15>__CastValue;

			// Token: 0x04004506 RID: 17670
			public static Func<object, Optional<RowNumberLinearTransformDescriptor>> <16>__CastValue;

			// Token: 0x04004507 RID: 17671
			public static Func<object, Optional<MatchDescriptor>> <17>__CastValue;
		}
	}
}
