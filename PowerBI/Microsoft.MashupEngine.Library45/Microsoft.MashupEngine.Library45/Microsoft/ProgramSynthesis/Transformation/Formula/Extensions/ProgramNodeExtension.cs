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
	// Token: 0x020019BA RID: 6586
	public static class ProgramNodeExtension
	{
		// Token: 0x0600D6F6 RID: 55030 RVA: 0x002DAEA8 File Offset: 0x002D90A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Any(this Program program, Func<ProgramNodeDetail, bool> predicate)
		{
			return program.Meta.Nodes.Any(predicate);
		}

		// Token: 0x0600D6F7 RID: 55031 RVA: 0x002DAEBB File Offset: 0x002D90BB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Any<T>(this Program program) where T : struct, IProgramNodeBuilder
		{
			return program.Meta.Nodes.Any<T>();
		}

		// Token: 0x0600D6F8 RID: 55032 RVA: 0x002DAECD File Offset: 0x002D90CD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Any<T>(this Program program, Func<T, bool> predicate) where T : struct, IProgramNodeBuilder
		{
			return program.Meta.Nodes.Any(predicate);
		}

		// Token: 0x0600D6F9 RID: 55033 RVA: 0x002DAEE0 File Offset: 0x002D90E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Any<T1, T2>(this Program program) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder
		{
			return program.Meta.Nodes.Any<T1, T2>();
		}

		// Token: 0x0600D6FA RID: 55034 RVA: 0x002DAEF2 File Offset: 0x002D90F2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Any<T1, T2, T3>(this Program program) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder
		{
			return program.Meta.Nodes.Any<T1, T2, T3>();
		}

		// Token: 0x0600D6FB RID: 55035 RVA: 0x002DAF04 File Offset: 0x002D9104
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Any<T1, T2, T3, T4>(this Program program) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder where T4 : struct, IProgramNodeBuilder
		{
			return program.Meta.Nodes.Any<T1, T2, T3, T4>();
		}

		// Token: 0x0600D6FC RID: 55036 RVA: 0x002DAF16 File Offset: 0x002D9116
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Any<T1, T2, T3, T4, T5>(this Program program) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder where T4 : struct, IProgramNodeBuilder where T5 : struct, IProgramNodeBuilder
		{
			return program.Meta.Nodes.Any<T1, T2, T3, T4, T5>();
		}

		// Token: 0x0600D6FD RID: 55037 RVA: 0x002DAF28 File Offset: 0x002D9128
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Any<T1, T2, T3, T4, T5, T6>(this Program program) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder where T4 : struct, IProgramNodeBuilder where T5 : struct, IProgramNodeBuilder where T6 : struct, IProgramNodeBuilder
		{
			return program.Meta.Nodes.Any<T1, T2, T3, T4, T5, T6>();
		}

		// Token: 0x0600D6FE RID: 55038 RVA: 0x002DAF3A File Offset: 0x002D913A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Count(this Program program, Func<ProgramNodeDetail, bool> predicate)
		{
			return program.Meta.Nodes.Count(predicate);
		}

		// Token: 0x0600D6FF RID: 55039 RVA: 0x002DAF4D File Offset: 0x002D914D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Count<T>(this Program program) where T : struct, IProgramNodeBuilder
		{
			return program.Meta.Nodes.Count<T>();
		}

		// Token: 0x0600D700 RID: 55040 RVA: 0x002DAF5F File Offset: 0x002D915F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Count<T>(this Program program, Func<T, bool> predicate) where T : struct, IProgramNodeBuilder
		{
			return program.Meta.Nodes.Count(predicate);
		}

		// Token: 0x0600D701 RID: 55041 RVA: 0x002DAF72 File Offset: 0x002D9172
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Count<T1, T2>(this Program program) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder
		{
			return program.Meta.Nodes.Count<T1, T2>();
		}

		// Token: 0x0600D702 RID: 55042 RVA: 0x002DAF84 File Offset: 0x002D9184
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Count<T1, T2, T3>(this Program program) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder
		{
			return program.Meta.Nodes.Count<T1, T2, T3>();
		}

		// Token: 0x0600D703 RID: 55043 RVA: 0x002DAF96 File Offset: 0x002D9196
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Count<T1, T2, T3, T4>(this Program program) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder where T4 : struct, IProgramNodeBuilder
		{
			return program.Meta.Nodes.Count<T1, T2, T3, T4>();
		}

		// Token: 0x0600D704 RID: 55044 RVA: 0x002DAFA8 File Offset: 0x002D91A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ProgramNodeDetail FirstOrDefault(this Program program, Func<ProgramNodeDetail, bool> predicate)
		{
			return program.Meta.Nodes.FirstOrDefault(predicate);
		}

		// Token: 0x0600D705 RID: 55045 RVA: 0x002DAFBB File Offset: 0x002D91BB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T FirstOrDefault<T>(this Program program) where T : struct, IProgramNodeBuilder
		{
			return program.Meta.Nodes.FirstOrDefault<T>();
		}

		// Token: 0x0600D706 RID: 55046 RVA: 0x002DAFCD File Offset: 0x002D91CD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T FirstOrDefault<T>(this Program program, Func<T, bool> predicate) where T : struct, IProgramNodeBuilder
		{
			return program.Meta.Nodes.FirstOrDefault(predicate);
		}

		// Token: 0x0600D707 RID: 55047 RVA: 0x002DAFE0 File Offset: 0x002D91E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TValue FirstOrDefaultValue<TNode, TValue>(this Program program) where TNode : struct, IProgramNodeBuilder
		{
			return program.Meta.Nodes.FirstOrDefaultValue(ProgramNodeExtension._build);
		}

		// Token: 0x0600D708 RID: 55048 RVA: 0x002DAFF7 File Offset: 0x002D91F7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static TValue FirstOrDefaultValue<TNode, TValue>(this Program program, Func<TNode, bool> predicate) where TNode : struct, IProgramNodeBuilder
		{
			return program.Meta.Nodes.FirstOrDefaultValue(ProgramNodeExtension._build, predicate);
		}

		// Token: 0x0600D709 RID: 55049 RVA: 0x002DB00F File Offset: 0x002D920F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool None(this Program program, Func<ProgramNodeDetail, bool> predicate)
		{
			return program.Meta.Nodes.None(predicate);
		}

		// Token: 0x0600D70A RID: 55050 RVA: 0x002DB022 File Offset: 0x002D9222
		public static bool None<T>(this Program program) where T : struct, IProgramNodeBuilder
		{
			return program.Meta.Nodes.None<T>();
		}

		// Token: 0x0600D70B RID: 55051 RVA: 0x002DB034 File Offset: 0x002D9234
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool None<T>(this Program program, Func<T, bool> predicate) where T : struct, IProgramNodeBuilder
		{
			return program.Meta.Nodes.None(ProgramNodeExtension._build, predicate);
		}

		// Token: 0x0600D70C RID: 55052 RVA: 0x002DB04C File Offset: 0x002D924C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool None<T1, T2>(this Program program) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder
		{
			return program.Meta.Nodes.None<T1, T2>();
		}

		// Token: 0x0600D70D RID: 55053 RVA: 0x002DB05E File Offset: 0x002D925E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool None<T1, T2, T3>(this Program program) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder
		{
			return program.Meta.Nodes.None<T1, T2, T3>();
		}

		// Token: 0x0600D70E RID: 55054 RVA: 0x002DB070 File Offset: 0x002D9270
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool None<T1, T2, T3, T4>(this Program program) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder where T4 : struct, IProgramNodeBuilder
		{
			return program.Meta.Nodes.None<T1, T2, T3, T4>();
		}

		// Token: 0x0600D70F RID: 55055 RVA: 0x002DB082 File Offset: 0x002D9282
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<T> OfType<T>(this Program program) where T : struct, IProgramNodeBuilder
		{
			return program.Meta.Nodes.OfGrammarType<T>();
		}

		// Token: 0x0600D710 RID: 55056 RVA: 0x002DB094 File Offset: 0x002D9294
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<TValue> SelectValues<TNode, TValue>(this Program program) where TNode : struct, IProgramNodeBuilder
		{
			return program.Meta.Nodes.SelectValues(ProgramNodeExtension._build);
		}

		// Token: 0x0600D711 RID: 55057 RVA: 0x002DB0AB File Offset: 0x002D92AB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<ProgramNodeDetail> Where(this Program program, Func<ProgramNodeDetail, bool> predicate)
		{
			return program.Meta.Nodes.Where(predicate);
		}

		// Token: 0x0600D712 RID: 55058 RVA: 0x002DB0BE File Offset: 0x002D92BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<ProgramNode> ProgramNodes(this IEnumerable<ProgramNodeDetail> nodeDetails)
		{
			return nodeDetails.Select((ProgramNodeDetail nodeDetail) => nodeDetail.Node);
		}

		// Token: 0x0600D713 RID: 55059 RVA: 0x002DB0E5 File Offset: 0x002D92E5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Cast<T>(this ProgramNode node) where T : struct, IProgramNodeBuilder
		{
			return node.Cast(ProgramNodeExtension._build);
		}

		// Token: 0x0600D714 RID: 55060 RVA: 0x002DB0F2 File Offset: 0x002D92F2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Is<T>(this ProgramNode node, out T grammarNode) where T : struct, IProgramNodeBuilder
		{
			return node.Is(ProgramNodeExtension._build, out grammarNode);
		}

		// Token: 0x0600D715 RID: 55061 RVA: 0x002DB100 File Offset: 0x002D9300
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsArithmeticRightNumber(this ProgramNode node, out decimal value)
		{
			return node.IsArithmeticRightNumber(ProgramNodeExtension._build, out value);
		}

		// Token: 0x0600D716 RID: 55062 RVA: 0x002DB10E File Offset: 0x002D930E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsConstantNumber(this ProgramNode node, out decimal value)
		{
			return node.IsConstantNumber(ProgramNodeExtension._build, out value);
		}

		// Token: 0x0400529A RID: 21146
		private static readonly GrammarBuilders _build = Language.Build;
	}
}
