using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200002B RID: 43
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class ImmutableArrayBuilderDebuggerProxy<[Nullable(2)] T>
	{
		// Token: 0x0600017D RID: 381 RVA: 0x000054B4 File Offset: 0x000036B4
		public ImmutableArrayBuilderDebuggerProxy(ImmutableArray<T>.Builder builder)
		{
			Requires.NotNull<ImmutableArray<T>.Builder>(builder, "builder");
			this._builder = builder;
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600017E RID: 382 RVA: 0x000054CE File Offset: 0x000036CE
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] A
		{
			get
			{
				return this._builder.ToArray();
			}
		}

		// Token: 0x0400001C RID: 28
		private readonly ImmutableArray<T>.Builder _builder;
	}
}
