using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.VersionSpace
{
	// Token: 0x0200028D RID: 653
	public abstract class ProgramSetBuilder
	{
		// Token: 0x06000E38 RID: 3640 RVA: 0x000299A7 File Offset: 0x00027BA7
		protected ProgramSetBuilder(ProgramSet set)
		{
			this.Set = set;
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x000299B6 File Offset: 0x00027BB6
		public override int GetHashCode()
		{
			return this.Set.GetHashCode();
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x000299C4 File Offset: 0x00027BC4
		public override bool Equals(object other)
		{
			ProgramSetBuilder programSetBuilder = other as ProgramSetBuilder;
			return programSetBuilder != null && this.Set.Equals(programSetBuilder.Set);
		}

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000E3B RID: 3643 RVA: 0x000299EE File Offset: 0x00027BEE
		public ProgramSet Set { get; }

		// Token: 0x06000E3C RID: 3644 RVA: 0x000299F8 File Offset: 0x00027BF8
		public static ProgramSetBuilder<T> Empty<T>(Symbol symbol) where T : IProgramNodeBuilder
		{
			if (typeof(T).Name != symbol.Name)
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Symbol name {0} and node name {1} do not match", new object[]
				{
					symbol.Name,
					typeof(T).Name
				})), "symbol");
			}
			return ProgramSetBuilder<T>.CreateUnsafe(ProgramSet.Empty(symbol));
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x00029A67 File Offset: 0x00027C67
		public static ProgramSetBuilder<T> List<T>(Symbol symbol, params T[] nodes) where T : IProgramNodeBuilder
		{
			return ProgramSetBuilder.List<T>(symbol, nodes);
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x00029A70 File Offset: 0x00027C70
		public static ProgramSetBuilder<T> List<T>(Symbol symbol, IEnumerable<T> nodes) where T : IProgramNodeBuilder
		{
			if (typeof(T).Name != symbol.Name)
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Symbol name {0} and node name {1} do not match", new object[]
				{
					symbol.Name,
					typeof(T).Name
				})), "symbol");
			}
			return ProgramSetBuilder<T>.CreateUnsafe(ProgramSet.List(symbol, nodes.Select((T n) => n.Node)));
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x00029B04 File Offset: 0x00027D04
		public static ProgramSetBuilder<T> List<T>(params T[] nodes) where T : IProgramNodeBuilder
		{
			return ProgramSetBuilder<T>.CreateUnsafe(ProgramSet.List(nodes[0].Node.Symbol, nodes.Select((T n) => n.Node)));
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x00029B59 File Offset: 0x00027D59
		public static ProgramSetBuilder<T> NormalizedUnion<T>(params ProgramSetBuilder<T>[] builders) where T : IProgramNodeBuilder
		{
			return builders.NormalizedUnion<T>();
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x00029B61 File Offset: 0x00027D61
		public static ProgramSetBuilder<T> Unsafe<T>(ProgramSet set) where T : IProgramNodeBuilder
		{
			return ProgramSetBuilder<T>.CreateUnsafe(set);
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x00029B69 File Offset: 0x00027D69
		public static bool IsNullOrEmpty<T>(ProgramSetBuilder<T> set) where T : IProgramNodeBuilder
		{
			return ProgramSet.IsNullOrEmpty((set != null) ? set.Set : null);
		}
	}
}
