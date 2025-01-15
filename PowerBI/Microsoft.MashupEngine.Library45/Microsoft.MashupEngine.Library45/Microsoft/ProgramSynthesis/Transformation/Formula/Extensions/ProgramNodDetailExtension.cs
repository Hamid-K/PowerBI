using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Visitors;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Extensions
{
	// Token: 0x020019BC RID: 6588
	public static class ProgramNodDetailExtension
	{
		// Token: 0x0600D71B RID: 55067 RVA: 0x002DB134 File Offset: 0x002D9334
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Cast<T>(this ProgramNodeDetail nodeDetail) where T : struct, IProgramNodeBuilder
		{
			return nodeDetail.Cast(ProgramNodDetailExtension._build);
		}

		// Token: 0x0600D71C RID: 55068 RVA: 0x002DB141 File Offset: 0x002D9341
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Is<T>(this ProgramNodeDetail node, out T grammarNode) where T : struct, IProgramNodeBuilder
		{
			return node.Node.Is(ProgramNodDetailExtension._build, out grammarNode);
		}

		// Token: 0x0600D71D RID: 55069 RVA: 0x002DB154 File Offset: 0x002D9354
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Any<T>(this IEnumerable<ProgramNodeDetail> nodeDetails, Func<T, bool> predicate) where T : struct, IProgramNodeBuilder
		{
			return nodeDetails.Any(ProgramNodDetailExtension._build, predicate);
		}

		// Token: 0x0600D71E RID: 55070 RVA: 0x002DB162 File Offset: 0x002D9362
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<T> Cast<T>(this IEnumerable<ProgramNodeDetail> nodeDetails) where T : struct, IProgramNodeBuilder
		{
			return nodeDetails.Select((ProgramNodeDetail node) => node.Cast(ProgramNodDetailExtension._build));
		}

		// Token: 0x0600D71F RID: 55071 RVA: 0x002DB189 File Offset: 0x002D9389
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Count<T>(this IEnumerable<ProgramNodeDetail> nodeDetails, Func<T, bool> predicate) where T : struct, IProgramNodeBuilder
		{
			return nodeDetails.Count(predicate, ProgramNodDetailExtension._build);
		}

		// Token: 0x0600D720 RID: 55072 RVA: 0x002DB197 File Offset: 0x002D9397
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T First<T>(this IEnumerable<ProgramNodeDetail> nodeDetails) where T : struct, IProgramNodeBuilder
		{
			return nodeDetails.First(ProgramNodDetailExtension._build);
		}

		// Token: 0x0600D721 RID: 55073 RVA: 0x002DB1A4 File Offset: 0x002D93A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T First<T>(this IEnumerable<ProgramNodeDetail> nodeDetails, Func<T, bool> predicate) where T : struct, IProgramNodeBuilder
		{
			return nodeDetails.First(ProgramNodDetailExtension._build, predicate);
		}

		// Token: 0x0600D722 RID: 55074 RVA: 0x002DB1B2 File Offset: 0x002D93B2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T FirstOrDefault<T>(this IEnumerable<ProgramNodeDetail> nodeDetails) where T : struct, IProgramNodeBuilder
		{
			return nodeDetails.FirstOrDefault(ProgramNodDetailExtension._build);
		}

		// Token: 0x0600D723 RID: 55075 RVA: 0x002DB1BF File Offset: 0x002D93BF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T FirstOrDefault<T>(this IEnumerable<ProgramNodeDetail> nodeDetails, Func<T, bool> predicate) where T : struct, IProgramNodeBuilder
		{
			return nodeDetails.FirstOrDefault(ProgramNodDetailExtension._build, predicate);
		}

		// Token: 0x0600D724 RID: 55076 RVA: 0x002DB1CD File Offset: 0x002D93CD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TValue FirstOrDefaultValue<TNode, TValue>(this IEnumerable<ProgramNodeDetail> nodeDetails) where TNode : struct, IProgramNodeBuilder
		{
			return nodeDetails.FirstOrDefaultValue(ProgramNodDetailExtension._build);
		}

		// Token: 0x0600D725 RID: 55077 RVA: 0x002DB1DA File Offset: 0x002D93DA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TValue FirstOrDefaultValue<TNode, TValue>(this IEnumerable<ProgramNodeDetail> nodeDetails, Func<TNode, bool> predicate) where TNode : struct, IProgramNodeBuilder
		{
			return nodeDetails.FirstOrDefaultValue(ProgramNodDetailExtension._build, predicate);
		}

		// Token: 0x0600D726 RID: 55078 RVA: 0x002DB1E8 File Offset: 0x002D93E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Last<T>(this IEnumerable<ProgramNodeDetail> nodeDetails) where T : struct, IProgramNodeBuilder
		{
			return nodeDetails.Last(ProgramNodDetailExtension._build);
		}

		// Token: 0x0600D727 RID: 55079 RVA: 0x002DB1F5 File Offset: 0x002D93F5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Last<T>(this IEnumerable<ProgramNodeDetail> nodeDetails, Func<T, bool> predicate) where T : struct, IProgramNodeBuilder
		{
			return nodeDetails.Last(ProgramNodDetailExtension._build, predicate);
		}

		// Token: 0x0600D728 RID: 55080 RVA: 0x002DB203 File Offset: 0x002D9403
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T LastOrDefault<T>(this IEnumerable<ProgramNodeDetail> nodeDetails) where T : struct, IProgramNodeBuilder
		{
			return nodeDetails.LastOrDefault(ProgramNodDetailExtension._build);
		}

		// Token: 0x0600D729 RID: 55081 RVA: 0x002DB210 File Offset: 0x002D9410
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T LastOrDefault<T>(this IEnumerable<ProgramNodeDetail> nodeDetails, Func<T, bool> predicate) where T : struct, IProgramNodeBuilder
		{
			return nodeDetails.LastOrDefault(ProgramNodDetailExtension._build, predicate);
		}

		// Token: 0x0600D72A RID: 55082 RVA: 0x002DB21E File Offset: 0x002D941E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<T> OfGrammarType<T>(this IEnumerable<ProgramNodeDetail> nodeDetails) where T : struct, IProgramNodeBuilder
		{
			return nodeDetails.OfGrammarType(ProgramNodDetailExtension._build);
		}

		// Token: 0x0600D72B RID: 55083 RVA: 0x002DB22B File Offset: 0x002D942B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<TValue> SelectValues<TNode, TValue>(this IEnumerable<ProgramNodeDetail> nodeDetails) where TNode : struct, IProgramNodeBuilder
		{
			return nodeDetails.SelectValues(ProgramNodDetailExtension._build);
		}

		// Token: 0x0600D72C RID: 55084 RVA: 0x002DB238 File Offset: 0x002D9438
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryFirst<T>(this IEnumerable<ProgramNodeDetail> nodeDetails, out T grammarNode) where T : struct, IProgramNodeBuilder
		{
			return nodeDetails.TryFirst(ProgramNodDetailExtension._build, out grammarNode);
		}

		// Token: 0x0600D72D RID: 55085 RVA: 0x002DB246 File Offset: 0x002D9446
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryFirst<T>(this IEnumerable<ProgramNodeDetail> nodeDetails, Func<ProgramNodeDetail, bool> predicate, out T grammarNode) where T : struct, IProgramNodeBuilder
		{
			return nodeDetails.TryFirst(ProgramNodDetailExtension._build, predicate, out grammarNode);
		}

		// Token: 0x0600D72E RID: 55086 RVA: 0x002DB255 File Offset: 0x002D9455
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryFirst<T>(this IEnumerable<ProgramNodeDetail> nodeDetails, Func<T, bool> predicate, out T grammarNode) where T : struct, IProgramNodeBuilder
		{
			return nodeDetails.TryFirst(ProgramNodDetailExtension._build, predicate, out grammarNode);
		}

		// Token: 0x0600D72F RID: 55087 RVA: 0x002DB264 File Offset: 0x002D9464
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryLast<T>(this IEnumerable<ProgramNodeDetail> nodeDetails, out T grammarNode) where T : struct, IProgramNodeBuilder
		{
			return nodeDetails.TryLast(ProgramNodDetailExtension._build, out grammarNode);
		}

		// Token: 0x0600D730 RID: 55088 RVA: 0x002DB272 File Offset: 0x002D9472
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryLast<T>(this IEnumerable<ProgramNodeDetail> nodeDetails, Func<ProgramNodeDetail, bool> predicate, out T grammarNode) where T : struct, IProgramNodeBuilder
		{
			return nodeDetails.TryLast(ProgramNodDetailExtension._build, predicate, out grammarNode);
		}

		// Token: 0x0600D731 RID: 55089 RVA: 0x002DB281 File Offset: 0x002D9481
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryLast<T>(this IEnumerable<ProgramNodeDetail> nodeDetails, Func<T, bool> predicate, out T grammarNode) where T : struct, IProgramNodeBuilder
		{
			return nodeDetails.TryLast(ProgramNodDetailExtension._build, predicate, out grammarNode);
		}

		// Token: 0x0400529D RID: 21149
		private static readonly GrammarBuilders _build = Language.Build;
	}
}
