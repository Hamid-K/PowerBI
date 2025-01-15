using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02002089 RID: 8329
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ImmutableArrayBuilderDebuggerProxy<[Nullable(2)] T>
	{
		// Token: 0x060115AF RID: 71087 RVA: 0x003B9C3D File Offset: 0x003B7E3D
		public ImmutableArrayBuilderDebuggerProxy(ImmutableArray<T>.Builder builder)
		{
			Requires.NotNull<ImmutableArray<T>.Builder>(builder, "builder");
			this._builder = builder;
		}

		// Token: 0x17002E5D RID: 11869
		// (get) Token: 0x060115B0 RID: 71088 RVA: 0x003B9C57 File Offset: 0x003B7E57
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] A
		{
			get
			{
				return this._builder.ToArray();
			}
		}

		// Token: 0x040068E1 RID: 26849
		private readonly ImmutableArray<T>.Builder _builder;
	}
}
