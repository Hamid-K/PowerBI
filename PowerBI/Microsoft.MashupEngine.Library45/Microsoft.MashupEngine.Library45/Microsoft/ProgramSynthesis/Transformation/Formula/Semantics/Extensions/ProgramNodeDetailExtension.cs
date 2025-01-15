using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Visitors;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions
{
	// Token: 0x0200179B RID: 6043
	public static class ProgramNodeDetailExtension
	{
		// Token: 0x0600C81E RID: 51230 RVA: 0x002B07E6 File Offset: 0x002AE9E6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static string CanonicalName(this ProgramNode node)
		{
			GrammarRule grammarRule = node.GrammarRule;
			return ((grammarRule != null) ? grammarRule.Id : null) ?? node.Symbol.Name;
		}

		// Token: 0x0600C81F RID: 51231 RVA: 0x002B0809 File Offset: 0x002AEA09
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Cast<T>(this ProgramNodeDetail nodeDetail, GrammarBuilders build) where T : struct, IProgramNodeBuilder
		{
			return nodeDetail.Node.Cast(build);
		}

		// Token: 0x0600C820 RID: 51232 RVA: 0x002B0817 File Offset: 0x002AEA17
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Is<T>(this ProgramNodeDetail nodeDetail) where T : struct, IProgramNodeBuilder
		{
			return nodeDetail != null && nodeDetail.Node.Is<T>();
		}

		// Token: 0x0600C821 RID: 51233 RVA: 0x002B082F File Offset: 0x002AEA2F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Is<T1, T2>(this ProgramNodeDetail nodeDetail) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder
		{
			return nodeDetail.Node.Is<T1, T2>();
		}

		// Token: 0x0600C822 RID: 51234 RVA: 0x002B083C File Offset: 0x002AEA3C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Is<T1, T2, T3>(this ProgramNodeDetail nodeDetail) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder
		{
			return nodeDetail.Node.Is<T1, T2, T3>();
		}

		// Token: 0x0600C823 RID: 51235 RVA: 0x002B0849 File Offset: 0x002AEA49
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Is<T1, T2, T3, T4>(this ProgramNodeDetail nodeDetail) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder where T4 : struct, IProgramNodeBuilder
		{
			return nodeDetail.Node.Is<T1, T2, T3, T4>();
		}

		// Token: 0x0600C824 RID: 51236 RVA: 0x002B0856 File Offset: 0x002AEA56
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Is<T1, T2, T3, T4, T5>(this ProgramNodeDetail nodeDetail) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder where T4 : struct, IProgramNodeBuilder where T5 : struct, IProgramNodeBuilder
		{
			return nodeDetail.Node.Is<T1, T2, T3, T4, T5>();
		}

		// Token: 0x0600C825 RID: 51237 RVA: 0x002B0863 File Offset: 0x002AEA63
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Is<T1, T2, T3, T4, T5, T6>(this ProgramNodeDetail nodeDetail) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder where T4 : struct, IProgramNodeBuilder where T5 : struct, IProgramNodeBuilder where T6 : struct, IProgramNodeBuilder
		{
			return nodeDetail.Node.Is<T1, T2, T3, T4, T5, T6>();
		}

		// Token: 0x0600C826 RID: 51238 RVA: 0x002B0870 File Offset: 0x002AEA70
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Is<T1, T2, T3, T4, T5, T6, T7>(this ProgramNodeDetail nodeDetail) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder where T4 : struct, IProgramNodeBuilder where T5 : struct, IProgramNodeBuilder where T6 : struct, IProgramNodeBuilder where T7 : struct, IProgramNodeBuilder
		{
			return nodeDetail.Node.Is<T1, T2, T3, T4, T5, T6, T7>();
		}

		// Token: 0x0600C827 RID: 51239 RVA: 0x002B087D File Offset: 0x002AEA7D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Is<T>(this ProgramNodeDetail node, GrammarBuilders build, out T grammarNode) where T : struct, IProgramNodeBuilder
		{
			return node.Node.Is(build, out grammarNode);
		}

		// Token: 0x0600C828 RID: 51240 RVA: 0x002B088C File Offset: 0x002AEA8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<T> OfGrammarType<T>(this IEnumerable<ProgramNodeDetail> nodeDetails, GrammarBuilders build) where T : struct, IProgramNodeBuilder
		{
			return from nodeDetail in nodeDetails
				where nodeDetail.Is<T>()
				select nodeDetail.Cast(build);
		}

		// Token: 0x0600C829 RID: 51241 RVA: 0x002B08DC File Offset: 0x002AEADC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<ProgramNodeDetail> OfType<T1, T2>(this IEnumerable<ProgramNodeDetail> nodeDetails) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder
		{
			return nodeDetails.Where((ProgramNodeDetail nodeDetail) => nodeDetail.Is<T1, T2>());
		}

		// Token: 0x0600C82A RID: 51242 RVA: 0x002B0903 File Offset: 0x002AEB03
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<ProgramNodeDetail> OfType<T1, T2, T3>(this IEnumerable<ProgramNodeDetail> nodeDetails) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder
		{
			return nodeDetails.Where((ProgramNodeDetail nodeDetail) => nodeDetail.Is<T1, T2, T3>());
		}

		// Token: 0x0600C82B RID: 51243 RVA: 0x002B092A File Offset: 0x002AEB2A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<ProgramNodeDetail> OfType<T1, T2, T3, T4>(this IEnumerable<ProgramNodeDetail> nodeDetails) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder where T4 : struct, IProgramNodeBuilder
		{
			return nodeDetails.Where((ProgramNodeDetail nodeDetail) => nodeDetail.Is<T1, T2, T3, T4>());
		}

		// Token: 0x0600C82C RID: 51244 RVA: 0x002B0951 File Offset: 0x002AEB51
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<ProgramNodeDetail> OfType<T>(this IEnumerable<ProgramNodeDetail> nodeDetails) where T : struct, IProgramNodeBuilder
		{
			return nodeDetails.Where((ProgramNodeDetail nodeDetail) => nodeDetail.Is<T>());
		}

		// Token: 0x0600C82D RID: 51245 RVA: 0x002B0978 File Offset: 0x002AEB78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsLowerCase(this ProgramNodeDetail node)
		{
			return node.Node.IsLowerCase();
		}

		// Token: 0x0600C82E RID: 51246 RVA: 0x002B0985 File Offset: 0x002AEB85
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsProperCase(this ProgramNodeDetail node)
		{
			return node.Node.IsProperCase();
		}

		// Token: 0x0600C82F RID: 51247 RVA: 0x002B0992 File Offset: 0x002AEB92
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsUpperCase(this ProgramNodeDetail node)
		{
			return node.Node.IsUpperCase();
		}

		// Token: 0x0600C830 RID: 51248 RVA: 0x002B099F File Offset: 0x002AEB9F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool All<T>(this IEnumerable<ProgramNodeDetail> nodeDetails) where T : struct, IProgramNodeBuilder
		{
			return nodeDetails.All((ProgramNodeDetail nodeDetail) => nodeDetail.Is<T>());
		}

		// Token: 0x0600C831 RID: 51249 RVA: 0x002B09C6 File Offset: 0x002AEBC6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool All<T>(this IEnumerable<ProgramNodeDetail> nodeDetails, GrammarBuilders build, Func<T, bool> predicate) where T : struct, IProgramNodeBuilder
		{
			return nodeDetails.OfGrammarType(build).All(predicate);
		}

		// Token: 0x0600C832 RID: 51250 RVA: 0x002B09D5 File Offset: 0x002AEBD5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool All<T1, T2>(this IEnumerable<ProgramNodeDetail> nodeDetails) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder
		{
			return nodeDetails.All((ProgramNodeDetail nodeDetail) => nodeDetail.Is<T1>() || nodeDetail.Is<T2>());
		}

		// Token: 0x0600C833 RID: 51251 RVA: 0x002B09FC File Offset: 0x002AEBFC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool All<T1, T2, T3>(this IEnumerable<ProgramNodeDetail> nodeDetails) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder
		{
			return nodeDetails.All((ProgramNodeDetail nodeDetail) => nodeDetail.Is<T1>() || nodeDetail.Is<T2>() || nodeDetail.Is<T3>());
		}

		// Token: 0x0600C834 RID: 51252 RVA: 0x002B0A23 File Offset: 0x002AEC23
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool All<T1, T2, T3, T4>(this IEnumerable<ProgramNodeDetail> nodeDetails) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder where T4 : struct, IProgramNodeBuilder
		{
			return nodeDetails.All((ProgramNodeDetail nodeDetail) => nodeDetail.Is<T1>() || nodeDetail.Is<T2>() || nodeDetail.Is<T3>() || nodeDetail.Is<T4>());
		}

		// Token: 0x0600C835 RID: 51253 RVA: 0x002B0A4A File Offset: 0x002AEC4A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool All<T1, T2, T3, T4, T5>(this IEnumerable<ProgramNodeDetail> nodeDetails) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder where T4 : struct, IProgramNodeBuilder where T5 : struct, IProgramNodeBuilder
		{
			return nodeDetails.All((ProgramNodeDetail nodeDetail) => nodeDetail.Is<T1>() || nodeDetail.Is<T2>() || nodeDetail.Is<T3>() || nodeDetail.Is<T4>() || nodeDetail.Is<T5>());
		}

		// Token: 0x0600C836 RID: 51254 RVA: 0x002B0A71 File Offset: 0x002AEC71
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool All<T1, T2, T3, T4, T5, T6>(this IEnumerable<ProgramNodeDetail> nodeDetails) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder where T4 : struct, IProgramNodeBuilder where T5 : struct, IProgramNodeBuilder where T6 : struct, IProgramNodeBuilder
		{
			return nodeDetails.All((ProgramNodeDetail nodeDetail) => nodeDetail.Is<T1>() || nodeDetail.Is<T2>() || nodeDetail.Is<T3>() || nodeDetail.Is<T4>() || nodeDetail.Is<T5>() || nodeDetail.Is<T6>());
		}

		// Token: 0x0600C837 RID: 51255 RVA: 0x002B0A98 File Offset: 0x002AEC98
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool All<T1, T2, T3, T4, T5, T6, T7>(this IEnumerable<ProgramNodeDetail> nodeDetails) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder where T4 : struct, IProgramNodeBuilder where T5 : struct, IProgramNodeBuilder where T6 : struct, IProgramNodeBuilder where T7 : struct, IProgramNodeBuilder
		{
			return nodeDetails.All((ProgramNodeDetail nodeDetail) => nodeDetail.Is<T1>() || nodeDetail.Is<T2>() || nodeDetail.Is<T3>() || nodeDetail.Is<T4>() || nodeDetail.Is<T5>() || nodeDetail.Is<T6>() || nodeDetail.Is<T7>());
		}

		// Token: 0x0600C838 RID: 51256 RVA: 0x002B0ABF File Offset: 0x002AECBF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool All<T1, T2, T3, T4, T5, T6, T7, T8>(this IEnumerable<ProgramNodeDetail> nodeDetails) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder where T4 : struct, IProgramNodeBuilder where T5 : struct, IProgramNodeBuilder where T6 : struct, IProgramNodeBuilder where T7 : struct, IProgramNodeBuilder where T8 : struct, IProgramNodeBuilder
		{
			return nodeDetails.All((ProgramNodeDetail nodeDetail) => nodeDetail.Is<T1>() || nodeDetail.Is<T2>() || nodeDetail.Is<T3>() || nodeDetail.Is<T4>() || nodeDetail.Is<T5>() || nodeDetail.Is<T6>() || nodeDetail.Is<T7>() || nodeDetail.Is<T8>());
		}

		// Token: 0x0600C839 RID: 51257 RVA: 0x002B0AE6 File Offset: 0x002AECE6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Any<T>(this IEnumerable<ProgramNodeDetail> nodeDetails) where T : struct, IProgramNodeBuilder
		{
			return nodeDetails.Any((ProgramNodeDetail nodeDetail) => nodeDetail.Is<T>());
		}

		// Token: 0x0600C83A RID: 51258 RVA: 0x002B0B0D File Offset: 0x002AED0D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Any<T>(this IEnumerable<ProgramNodeDetail> nodeDetails, GrammarBuilders build, Func<T, bool> predicate) where T : struct, IProgramNodeBuilder
		{
			return nodeDetails.OfGrammarType(build).Any(predicate);
		}

		// Token: 0x0600C83B RID: 51259 RVA: 0x002B0B1C File Offset: 0x002AED1C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Any<T1, T2>(this IEnumerable<ProgramNodeDetail> nodeDetails) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder
		{
			return nodeDetails.Any((ProgramNodeDetail nodeDetail) => nodeDetail.Is<T1>() || nodeDetail.Is<T2>());
		}

		// Token: 0x0600C83C RID: 51260 RVA: 0x002B0B43 File Offset: 0x002AED43
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Any<T1, T2, T3>(this IEnumerable<ProgramNodeDetail> nodeDetails) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder
		{
			return nodeDetails.Any((ProgramNodeDetail nodeDetail) => nodeDetail.Is<T1>() || nodeDetail.Is<T2>() || nodeDetail.Is<T3>());
		}

		// Token: 0x0600C83D RID: 51261 RVA: 0x002B0B6A File Offset: 0x002AED6A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Any<T1, T2, T3, T4>(this IEnumerable<ProgramNodeDetail> nodeDetails) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder where T4 : struct, IProgramNodeBuilder
		{
			return nodeDetails.Any((ProgramNodeDetail nodeDetail) => nodeDetail.Is<T1>() || nodeDetail.Is<T2>() || nodeDetail.Is<T3>() || nodeDetail.Is<T4>());
		}

		// Token: 0x0600C83E RID: 51262 RVA: 0x002B0B91 File Offset: 0x002AED91
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Any<T1, T2, T3, T4, T5>(this IEnumerable<ProgramNodeDetail> nodeDetails) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder where T4 : struct, IProgramNodeBuilder where T5 : struct, IProgramNodeBuilder
		{
			return nodeDetails.Any((ProgramNodeDetail nodeDetail) => nodeDetail.Is<T1>() || nodeDetail.Is<T2>() || nodeDetail.Is<T3>() || nodeDetail.Is<T4>() || nodeDetail.Is<T5>());
		}

		// Token: 0x0600C83F RID: 51263 RVA: 0x002B0BB8 File Offset: 0x002AEDB8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Any<T1, T2, T3, T4, T5, T6>(this IEnumerable<ProgramNodeDetail> nodeDetails) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder where T4 : struct, IProgramNodeBuilder where T5 : struct, IProgramNodeBuilder where T6 : struct, IProgramNodeBuilder
		{
			return nodeDetails.Any((ProgramNodeDetail nodeDetail) => nodeDetail.Is<T1>() || nodeDetail.Is<T2>() || nodeDetail.Is<T3>() || nodeDetail.Is<T4>() || nodeDetail.Is<T5>() || nodeDetail.Is<T6>());
		}

		// Token: 0x0600C840 RID: 51264 RVA: 0x002B0BDF File Offset: 0x002AEDDF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Any<T1, T2, T3, T4, T5, T6, T7>(this IEnumerable<ProgramNodeDetail> nodeDetails) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder where T4 : struct, IProgramNodeBuilder where T5 : struct, IProgramNodeBuilder where T6 : struct, IProgramNodeBuilder where T7 : struct, IProgramNodeBuilder
		{
			return nodeDetails.Any((ProgramNodeDetail nodeDetail) => nodeDetail.Is<T1>() || nodeDetail.Is<T2>() || nodeDetail.Is<T3>() || nodeDetail.Is<T4>() || nodeDetail.Is<T5>() || nodeDetail.Is<T6>() || nodeDetail.Is<T7>());
		}

		// Token: 0x0600C841 RID: 51265 RVA: 0x002B0C06 File Offset: 0x002AEE06
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Any<T1, T2, T3, T4, T5, T6, T7, T8>(this IEnumerable<ProgramNodeDetail> nodeDetails) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder where T4 : struct, IProgramNodeBuilder where T5 : struct, IProgramNodeBuilder where T6 : struct, IProgramNodeBuilder where T7 : struct, IProgramNodeBuilder where T8 : struct, IProgramNodeBuilder
		{
			return nodeDetails.Any((ProgramNodeDetail nodeDetail) => nodeDetail.Is<T1>() || nodeDetail.Is<T2>() || nodeDetail.Is<T3>() || nodeDetail.Is<T4>() || nodeDetail.Is<T5>() || nodeDetail.Is<T6>() || nodeDetail.Is<T7>() || nodeDetail.Is<T8>());
		}

		// Token: 0x0600C842 RID: 51266 RVA: 0x002B0C30 File Offset: 0x002AEE30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<T> Cast<T>(this IEnumerable<ProgramNodeDetail> nodeDetails, GrammarBuilders build) where T : struct, IProgramNodeBuilder
		{
			return nodeDetails.Select((ProgramNodeDetail nodeDetail) => nodeDetail.Cast(build));
		}

		// Token: 0x0600C843 RID: 51267 RVA: 0x002B0C5C File Offset: 0x002AEE5C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Count<T>(this IEnumerable<ProgramNodeDetail> nodeDetails) where T : struct, IProgramNodeBuilder
		{
			return nodeDetails.Count((ProgramNodeDetail node) => node.Is<T>());
		}

		// Token: 0x0600C844 RID: 51268 RVA: 0x002B0C83 File Offset: 0x002AEE83
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Count<T>(this IEnumerable<ProgramNodeDetail> nodeDetails, Func<T, bool> predicate, GrammarBuilders build) where T : struct, IProgramNodeBuilder
		{
			return nodeDetails.OfGrammarType(build).Count(predicate);
		}

		// Token: 0x0600C845 RID: 51269 RVA: 0x002B0C92 File Offset: 0x002AEE92
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Count<T1, T2>(this IEnumerable<ProgramNodeDetail> nodeDetails) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder
		{
			return nodeDetails.Count((ProgramNodeDetail nodeDetail) => nodeDetail.Is<T1>() || nodeDetail.Is<T2>());
		}

		// Token: 0x0600C846 RID: 51270 RVA: 0x002B0CB9 File Offset: 0x002AEEB9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Count<T1, T2, T3>(this IEnumerable<ProgramNodeDetail> nodeDetails) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder
		{
			return nodeDetails.Count((ProgramNodeDetail nodeDetail) => nodeDetail.Is<T1>() || nodeDetail.Is<T2>() || nodeDetail.Is<T3>());
		}

		// Token: 0x0600C847 RID: 51271 RVA: 0x002B0CE0 File Offset: 0x002AEEE0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Count<T1, T2, T3, T4>(this IEnumerable<ProgramNodeDetail> nodeDetails) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder where T4 : struct, IProgramNodeBuilder
		{
			return nodeDetails.Count((ProgramNodeDetail nodeDetail) => nodeDetail.Is<T1>() || nodeDetail.Is<T2>() || nodeDetail.Is<T3>() || nodeDetail.Is<T4>());
		}

		// Token: 0x0600C848 RID: 51272 RVA: 0x002B0D07 File Offset: 0x002AEF07
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T First<T>(this IEnumerable<ProgramNodeDetail> nodeDetails, GrammarBuilders build) where T : struct, IProgramNodeBuilder
		{
			return nodeDetails.First((ProgramNodeDetail nodeDetail) => nodeDetail.Is<T>()).Cast(build);
		}

		// Token: 0x0600C849 RID: 51273 RVA: 0x002B0D34 File Offset: 0x002AEF34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T First<T>(this IEnumerable<ProgramNodeDetail> nodeDetails, GrammarBuilders build, Func<T, bool> predicate) where T : struct, IProgramNodeBuilder
		{
			return nodeDetails.OfGrammarType(build).First(predicate);
		}

		// Token: 0x0600C84A RID: 51274 RVA: 0x002B0D44 File Offset: 0x002AEF44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T FirstOrDefault<T>(this IEnumerable<ProgramNodeDetail> nodeDetails, GrammarBuilders build) where T : struct, IProgramNodeBuilder
		{
			ProgramNodeDetail programNodeDetail = nodeDetails.FirstOrDefault((ProgramNodeDetail nodeDetail) => nodeDetail.Is<T>());
			if (programNodeDetail == null)
			{
				return default(T);
			}
			return programNodeDetail.Cast(build);
		}

		// Token: 0x0600C84B RID: 51275 RVA: 0x002B0D8A File Offset: 0x002AEF8A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ProgramNodeDetail FirstOrDefault<T1, T2>(this IEnumerable<ProgramNodeDetail> nodeDetails) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder
		{
			return nodeDetails.FirstOrDefault((ProgramNodeDetail nodeDetail) => nodeDetail.Is<T1>() || nodeDetail.Is<T2>());
		}

		// Token: 0x0600C84C RID: 51276 RVA: 0x002B0DB1 File Offset: 0x002AEFB1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ProgramNodeDetail FirstOrDefault<T1, T2, T3>(this IEnumerable<ProgramNodeDetail> nodeDetails) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder
		{
			return nodeDetails.FirstOrDefault((ProgramNodeDetail nodeDetail) => nodeDetail.Is<T1>() || nodeDetail.Is<T2>() || nodeDetail.Is<T3>());
		}

		// Token: 0x0600C84D RID: 51277 RVA: 0x002B0DD8 File Offset: 0x002AEFD8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ProgramNodeDetail FirstOrDefault<T1, T2, T3, T4>(this IEnumerable<ProgramNodeDetail> nodeDetails) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder where T4 : struct, IProgramNodeBuilder
		{
			return nodeDetails.FirstOrDefault((ProgramNodeDetail nodeDetail) => nodeDetail.Is<T1>() || nodeDetail.Is<T2>() || nodeDetail.Is<T3>() || nodeDetail.Is<T4>());
		}

		// Token: 0x0600C84E RID: 51278 RVA: 0x002B0E00 File Offset: 0x002AF000
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T FirstOrDefault<T>(this IEnumerable<ProgramNodeDetail> nodeDetails, GrammarBuilders build, Func<T, bool> predicate) where T : struct, IProgramNodeBuilder
		{
			ProgramNodeDetail programNodeDetail = nodeDetails.FirstOrDefault((ProgramNodeDetail node) => node.Is<T>() && predicate(node.Cast(build)));
			if (programNodeDetail == null)
			{
				return default(T);
			}
			return programNodeDetail.Cast(build);
		}

		// Token: 0x0600C84F RID: 51279 RVA: 0x002B0E4C File Offset: 0x002AF04C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TValue FirstOrDefaultValue<TNode, TValue>(this IEnumerable<ProgramNodeDetail> nodeDetails, GrammarBuilders build) where TNode : struct, IProgramNodeBuilder
		{
			TNode tnode = nodeDetails.FirstOrDefault(build);
			if (tnode.IsDefault<TNode>())
			{
				return default(TValue);
			}
			return (TValue)((object)((LiteralNode)tnode.Node).Value);
		}

		// Token: 0x0600C850 RID: 51280 RVA: 0x002B0E90 File Offset: 0x002AF090
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TValue FirstOrDefaultValue<TNode, TValue>(this IEnumerable<ProgramNodeDetail> nodeDetails, GrammarBuilders build, Func<TNode, bool> predicate) where TNode : struct, IProgramNodeBuilder
		{
			TNode tnode = nodeDetails.FirstOrDefault(build, predicate);
			if (tnode.IsDefault<TNode>())
			{
				return default(TValue);
			}
			return (TValue)((object)((LiteralNode)tnode.Node).Value);
		}

		// Token: 0x0600C851 RID: 51281 RVA: 0x002B0ED4 File Offset: 0x002AF0D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Last<T>(this IEnumerable<ProgramNodeDetail> nodeDetails, GrammarBuilders build) where T : struct, IProgramNodeBuilder
		{
			return nodeDetails.Last((ProgramNodeDetail nodeDetail) => nodeDetail.Is<T>()).Cast(build);
		}

		// Token: 0x0600C852 RID: 51282 RVA: 0x002B0F01 File Offset: 0x002AF101
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Last<T>(this IEnumerable<ProgramNodeDetail> nodeDetails, GrammarBuilders build, Func<T, bool> predicate) where T : struct, IProgramNodeBuilder
		{
			return nodeDetails.OfGrammarType(build).Last(predicate);
		}

		// Token: 0x0600C853 RID: 51283 RVA: 0x002B0F10 File Offset: 0x002AF110
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T LastOrDefault<T>(this IEnumerable<ProgramNodeDetail> nodeDetails, GrammarBuilders build) where T : struct, IProgramNodeBuilder
		{
			ProgramNodeDetail programNodeDetail = nodeDetails.LastOrDefault((ProgramNodeDetail nodeDetail) => nodeDetail.Is<T>());
			if (programNodeDetail == null)
			{
				return default(T);
			}
			return programNodeDetail.Cast(build);
		}

		// Token: 0x0600C854 RID: 51284 RVA: 0x002B0F56 File Offset: 0x002AF156
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ProgramNodeDetail LastOrDefault<T1, T2>(this IEnumerable<ProgramNodeDetail> nodeDetails) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder
		{
			return nodeDetails.LastOrDefault((ProgramNodeDetail nodeDetail) => nodeDetail.Is<T1>() || nodeDetail.Is<T2>());
		}

		// Token: 0x0600C855 RID: 51285 RVA: 0x002B0F7D File Offset: 0x002AF17D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ProgramNodeDetail LastOrDefault<T1, T2, T3>(this IEnumerable<ProgramNodeDetail> nodeDetails) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder
		{
			return nodeDetails.LastOrDefault((ProgramNodeDetail nodeDetail) => nodeDetail.Is<T1>() || nodeDetail.Is<T2>() || nodeDetail.Is<T3>());
		}

		// Token: 0x0600C856 RID: 51286 RVA: 0x002B0FA4 File Offset: 0x002AF1A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ProgramNodeDetail LastOrDefault<T1, T2, T3, T4>(this IEnumerable<ProgramNodeDetail> nodeDetails) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder where T4 : struct, IProgramNodeBuilder
		{
			return nodeDetails.LastOrDefault((ProgramNodeDetail nodeDetail) => nodeDetail.Is<T1>() || nodeDetail.Is<T2>() || nodeDetail.Is<T3>() || nodeDetail.Is<T4>());
		}

		// Token: 0x0600C857 RID: 51287 RVA: 0x002B0FCC File Offset: 0x002AF1CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T LastOrDefault<T>(this IEnumerable<ProgramNodeDetail> nodeDetails, GrammarBuilders build, Func<T, bool> predicate) where T : struct, IProgramNodeBuilder
		{
			ProgramNodeDetail programNodeDetail = nodeDetails.LastOrDefault((ProgramNodeDetail node) => node.Is<T>() && predicate(node.Cast(build)));
			if (programNodeDetail == null)
			{
				return default(T);
			}
			return programNodeDetail.Cast(build);
		}

		// Token: 0x0600C858 RID: 51288 RVA: 0x002B1018 File Offset: 0x002AF218
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool None<T>(this IEnumerable<ProgramNodeDetail> nodes) where T : struct, IProgramNodeBuilder
		{
			return !nodes.Any((ProgramNodeDetail node) => node.Is<T>());
		}

		// Token: 0x0600C859 RID: 51289 RVA: 0x002B1042 File Offset: 0x002AF242
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool None<T>(this IEnumerable<ProgramNodeDetail> nodes, GrammarBuilders build, Func<T, bool> predicate) where T : struct, IProgramNodeBuilder
		{
			return !nodes.Any(build, predicate);
		}

		// Token: 0x0600C85A RID: 51290 RVA: 0x002B104F File Offset: 0x002AF24F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool None<T1, T2>(this IEnumerable<ProgramNodeDetail> nodes) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder
		{
			return !nodes.Any<T1, T2>();
		}

		// Token: 0x0600C85B RID: 51291 RVA: 0x002B105A File Offset: 0x002AF25A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool None<T1, T2, T3>(this IEnumerable<ProgramNodeDetail> nodes) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder
		{
			return !nodes.Any<T1, T2, T3>();
		}

		// Token: 0x0600C85C RID: 51292 RVA: 0x002B1065 File Offset: 0x002AF265
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool None<T1, T2, T3, T4>(this IEnumerable<ProgramNodeDetail> nodes) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder where T4 : struct, IProgramNodeBuilder
		{
			return !nodes.Any<T1, T2, T3, T4>();
		}

		// Token: 0x0600C85D RID: 51293 RVA: 0x002B1070 File Offset: 0x002AF270
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryFirst<T>(this IEnumerable<ProgramNodeDetail> nodeDetails, out ProgramNodeDetail nodeDetail) where T : struct, IProgramNodeBuilder
		{
			nodeDetail = nodeDetails.FirstOrDefault((ProgramNodeDetail nodeDetail) => nodeDetail.Is<T>());
			return nodeDetail != null;
		}

		// Token: 0x0600C85E RID: 51294 RVA: 0x002B10A4 File Offset: 0x002AF2A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryFirst<T>(this IEnumerable<ProgramNodeDetail> nodeDetails, GrammarBuilders build, out T grammarNode) where T : struct, IProgramNodeBuilder
		{
			ProgramNodeDetail programNodeDetail = nodeDetails.FirstOrDefault((ProgramNodeDetail nodeDetail) => nodeDetail.Is<T>());
			T? t = ((programNodeDetail != null) ? new T?(programNodeDetail.Cast(build)) : null);
			grammarNode = t.GetValueOrDefault();
			return t != null;
		}

		// Token: 0x0600C85F RID: 51295 RVA: 0x002B1105 File Offset: 0x002AF305
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryFirst<T>(this IEnumerable<ProgramNodeDetail> nodeDetails, GrammarBuilders build, Func<ProgramNodeDetail, bool> predicate, out T grammarNode) where T : struct, IProgramNodeBuilder
		{
			grammarNode = nodeDetails.FirstOrDefault(build);
			return !grammarNode.IsDefault<T>();
		}

		// Token: 0x0600C860 RID: 51296 RVA: 0x002B1122 File Offset: 0x002AF322
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryFirst<T>(this IEnumerable<ProgramNodeDetail> nodeDetails, GrammarBuilders build, Func<T, bool> predicate, out T grammarNode) where T : struct, IProgramNodeBuilder
		{
			grammarNode = nodeDetails.FirstOrDefault(build, predicate);
			return !grammarNode.IsDefault<T>();
		}

		// Token: 0x0600C861 RID: 51297 RVA: 0x002B1140 File Offset: 0x002AF340
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryLast<T>(this IEnumerable<ProgramNodeDetail> nodeDetails, GrammarBuilders build, out T grammarNode) where T : struct, IProgramNodeBuilder
		{
			ProgramNodeDetail programNodeDetail = nodeDetails.LastOrDefault((ProgramNodeDetail nodeDetail) => nodeDetail.Is<T>());
			T? t = ((programNodeDetail != null) ? new T?(programNodeDetail.Cast(build)) : null);
			grammarNode = t.GetValueOrDefault();
			return t != null;
		}

		// Token: 0x0600C862 RID: 51298 RVA: 0x002B11A1 File Offset: 0x002AF3A1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryLast<T>(this IEnumerable<ProgramNodeDetail> nodeDetails, out ProgramNodeDetail nodeDetail) where T : struct, IProgramNodeBuilder
		{
			nodeDetail = nodeDetails.LastOrDefault((ProgramNodeDetail n) => n.Is<T>());
			return nodeDetail != null;
		}

		// Token: 0x0600C863 RID: 51299 RVA: 0x002B11D2 File Offset: 0x002AF3D2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryLast<T>(this IEnumerable<ProgramNodeDetail> nodeDetails, GrammarBuilders build, Func<ProgramNodeDetail, bool> predicate, out T grammarNode) where T : struct, IProgramNodeBuilder
		{
			grammarNode = nodeDetails.LastOrDefault(build);
			return !grammarNode.IsDefault<T>();
		}

		// Token: 0x0600C864 RID: 51300 RVA: 0x002B11EF File Offset: 0x002AF3EF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryLast<T>(this IEnumerable<ProgramNodeDetail> nodeDetails, GrammarBuilders build, Func<T, bool> predicate, out T grammarNode) where T : struct, IProgramNodeBuilder
		{
			grammarNode = nodeDetails.LastOrDefault(build, predicate);
			return !grammarNode.IsDefault<T>();
		}

		// Token: 0x0600C865 RID: 51301 RVA: 0x002B120D File Offset: 0x002AF40D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AnyLowerCase(this IEnumerable<ProgramNodeDetail> nodeDetails)
		{
			return nodeDetails.Any((ProgramNodeDetail nodeDetail) => nodeDetail.Node.IsLowerCase());
		}

		// Token: 0x0600C866 RID: 51302 RVA: 0x002B1234 File Offset: 0x002AF434
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AnyMatch(this IEnumerable<ProgramNodeDetail> nodeDetails)
		{
			return nodeDetails.Any((ProgramNodeDetail nodeDetail) => nodeDetail.Node.IsMatch());
		}

		// Token: 0x0600C867 RID: 51303 RVA: 0x002B125B File Offset: 0x002AF45B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AnyProperCase(this IEnumerable<ProgramNodeDetail> nodeDetails)
		{
			return nodeDetails.Any((ProgramNodeDetail nodeDetail) => nodeDetail.Node.IsProperCase());
		}

		// Token: 0x0600C868 RID: 51304 RVA: 0x002B1282 File Offset: 0x002AF482
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AnySlice(this IEnumerable<ProgramNodeDetail> nodeDetails)
		{
			return nodeDetails.Any((ProgramNodeDetail nodeDetail) => nodeDetail.Node.IsSlice());
		}

		// Token: 0x0600C869 RID: 51305 RVA: 0x002B12A9 File Offset: 0x002AF4A9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AnyTrim(this IEnumerable<ProgramNodeDetail> nodeDetails)
		{
			return nodeDetails.Any((ProgramNodeDetail nodeDetail) => nodeDetail.Node.IsTrim());
		}

		// Token: 0x0600C86A RID: 51306 RVA: 0x002B12D0 File Offset: 0x002AF4D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AnyUpperCase(this IEnumerable<ProgramNodeDetail> nodeDetails)
		{
			return nodeDetails.Any((ProgramNodeDetail nodeDetail) => nodeDetail.Node.IsUpperCase());
		}

		// Token: 0x0600C86B RID: 51307 RVA: 0x002B12F7 File Offset: 0x002AF4F7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsOutputCast(this IEnumerable<ProgramNodeDetail> nodeDetails)
		{
			return nodeDetails.Any((ProgramNodeDetail nodeDetail) => nodeDetail.Node.IsOutputCast());
		}

		// Token: 0x0600C86C RID: 51308 RVA: 0x002B131E File Offset: 0x002AF51E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<TValue> SelectValues<TNode, TValue>(this IEnumerable<ProgramNodeDetail> nodes, GrammarBuilders build) where TNode : struct, IProgramNodeBuilder
		{
			return from node in nodes.OfGrammarType(build)
				select (TValue)((object)((LiteralNode)node.Node).Value);
		}
	}
}
