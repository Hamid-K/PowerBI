using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Visitors;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions
{
	// Token: 0x02001788 RID: 6024
	public static class IProgramNodeBuilderExtension
	{
		// Token: 0x0600C78F RID: 51087 RVA: 0x002ADBCB File Offset: 0x002ABDCB
		public static bool Is<T>(this IProgramNodeBuilder node) where T : struct, IProgramNodeBuilder
		{
			return node.Node.Is<T>();
		}

		// Token: 0x0600C790 RID: 51088 RVA: 0x002ADBD8 File Offset: 0x002ABDD8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Is<T>(this IProgramNodeBuilder node, GrammarBuilders build, out T grammarNode) where T : struct, IProgramNodeBuilder
		{
			return node.Node.Is(build, out grammarNode);
		}

		// Token: 0x0600C791 RID: 51089 RVA: 0x002ADBE7 File Offset: 0x002ABDE7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Is<T1, T2>(this IProgramNodeBuilder node) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder
		{
			return node.Node.Is<T1, T2>();
		}

		// Token: 0x0600C792 RID: 51090 RVA: 0x002ADBF4 File Offset: 0x002ABDF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Is<T1, T2, T3>(this IProgramNodeBuilder node) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder
		{
			return node.Node.Is<T1, T2, T3>();
		}

		// Token: 0x0600C793 RID: 51091 RVA: 0x002ADC01 File Offset: 0x002ABE01
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Is<T1, T2, T3, T4>(this IProgramNodeBuilder node) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder where T4 : struct, IProgramNodeBuilder
		{
			return node.Node.Is<T1, T2, T3, T4>();
		}

		// Token: 0x0600C794 RID: 51092 RVA: 0x002ADC0E File Offset: 0x002ABE0E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Is<T1, T2, T3, T4, T5>(this IProgramNodeBuilder node) where T1 : struct, IProgramNodeBuilder where T2 : struct, IProgramNodeBuilder where T3 : struct, IProgramNodeBuilder where T4 : struct, IProgramNodeBuilder where T5 : struct, IProgramNodeBuilder
		{
			return node.Node.Is<T1, T2, T3, T4, T5>();
		}

		// Token: 0x0600C795 RID: 51093 RVA: 0x002ADC1B File Offset: 0x002ABE1B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<ProgramNodeDetail> Ancestors(this IProgramNodeBuilder node, IEnumerable<ProgramNodeDetail> nodeDetails)
		{
			return node.Node.Ancestors(nodeDetails);
		}

		// Token: 0x0600C796 RID: 51094 RVA: 0x002ADC29 File Offset: 0x002ABE29
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static IEnumerable<ProgramNodeDetail> Descendents(this IProgramNodeBuilder node, IEnumerable<ProgramNodeDetail> nodeDetails)
		{
			return node.Node.Descendents(nodeDetails);
		}
	}
}
