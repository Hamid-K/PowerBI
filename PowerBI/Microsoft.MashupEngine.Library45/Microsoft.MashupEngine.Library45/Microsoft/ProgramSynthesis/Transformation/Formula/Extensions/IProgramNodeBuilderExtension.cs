using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Visitors;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Extensions
{
	// Token: 0x020019B8 RID: 6584
	public static class IProgramNodeBuilderExtension
	{
		// Token: 0x0600D6E8 RID: 55016 RVA: 0x002ADBCB File Offset: 0x002ABDCB
		public static bool Is<T>(this IProgramNodeBuilder node) where T : struct, IProgramNodeBuilder
		{
			return node.Node.Is<T>();
		}

		// Token: 0x0600D6E9 RID: 55017 RVA: 0x002DAD31 File Offset: 0x002D8F31
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Is<T>(this IProgramNodeBuilder node, out T grammarNode) where T : struct, IProgramNodeBuilder
		{
			return node.Node.Is(IProgramNodeBuilderExtension._build, out grammarNode);
		}

		// Token: 0x0600D6EA RID: 55018 RVA: 0x002ADBE7 File Offset: 0x002ABDE7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Is<T1, T2>(this IProgramNodeBuilder node) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder
		{
			return node.Node.Is<T1, T2>();
		}

		// Token: 0x0600D6EB RID: 55019 RVA: 0x002ADBF4 File Offset: 0x002ABDF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Is<T1, T2, T3>(this IProgramNodeBuilder node) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder
		{
			return node.Node.Is<T1, T2, T3>();
		}

		// Token: 0x0600D6EC RID: 55020 RVA: 0x002ADC01 File Offset: 0x002ABE01
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Is<T1, T2, T3, T4>(this IProgramNodeBuilder node) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder where T4 : struct, IProgramNodeBuilder
		{
			return node.Node.Is<T1, T2, T3, T4>();
		}

		// Token: 0x0600D6ED RID: 55021 RVA: 0x002ADC0E File Offset: 0x002ABE0E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Is<T1, T2, T3, T4, T5>(this IProgramNodeBuilder node) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder where T4 : struct, IProgramNodeBuilder where T5 : struct, IProgramNodeBuilder
		{
			return node.Node.Is<T1, T2, T3, T4, T5>();
		}

		// Token: 0x0600D6EE RID: 55022 RVA: 0x002DAD44 File Offset: 0x002D8F44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<ProgramNodeDetail> Ancestors(this IProgramNodeBuilder node, Program program)
		{
			return node.Node.Ancestors(program.Meta.Nodes);
		}

		// Token: 0x0600D6EF RID: 55023 RVA: 0x002DAD5C File Offset: 0x002D8F5C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<T> Ancestors<T>(this IProgramNodeBuilder node, Program program) where T : struct, IProgramNodeBuilder
		{
			return node.Node.Ancestors(program.Meta.Nodes, IProgramNodeBuilderExtension._build);
		}

		// Token: 0x0600D6F0 RID: 55024 RVA: 0x002DAD79 File Offset: 0x002D8F79
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<ProgramNodeDetail> Descendents(this IProgramNodeBuilder node, Program program)
		{
			return node.Node.Descendents(program.Meta.Nodes);
		}

		// Token: 0x0600D6F1 RID: 55025 RVA: 0x002DAD91 File Offset: 0x002D8F91
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<T> Descendents<T>(this IProgramNodeBuilder node, Program program) where T : struct, IProgramNodeBuilder
		{
			return node.Node.Descendents(program.Meta.Nodes).OfGrammarType<T>();
		}

		// Token: 0x0600D6F2 RID: 55026 RVA: 0x002DADAE File Offset: 0x002D8FAE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<T> Ancestors<T>(this IProgramNodeBuilder node, IEnumerable<ProgramNodeDetail> nodeDetails) where T : struct, IProgramNodeBuilder
		{
			return node.Node.Ancestors(nodeDetails, IProgramNodeBuilderExtension._build);
		}

		// Token: 0x0600D6F3 RID: 55027 RVA: 0x002DADC1 File Offset: 0x002D8FC1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<T> Descendents<T>(this IProgramNodeBuilder node, IEnumerable<ProgramNodeDetail> nodeDetails) where T : struct, IProgramNodeBuilder
		{
			return node.Node.Descendents(nodeDetails, IProgramNodeBuilderExtension._build);
		}

		// Token: 0x04005299 RID: 21145
		private static readonly GrammarBuilders _build = Language.Build;
	}
}
