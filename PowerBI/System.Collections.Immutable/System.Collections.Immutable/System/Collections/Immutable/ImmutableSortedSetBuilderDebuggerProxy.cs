using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x0200003E RID: 62
	[NullableContext(1)]
	[Nullable(0)]
	internal class ImmutableSortedSetBuilderDebuggerProxy<[Nullable(2)] T>
	{
		// Token: 0x0600033F RID: 831 RVA: 0x00008DCF File Offset: 0x00006FCF
		public ImmutableSortedSetBuilderDebuggerProxy(ImmutableSortedSet<T>.Builder builder)
		{
			Requires.NotNull<ImmutableSortedSet<T>.Builder>(builder, "builder");
			this._set = builder;
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000340 RID: 832 RVA: 0x00008DE9 File Offset: 0x00006FE9
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Contents
		{
			get
			{
				return this._set.ToArray(this._set.Count);
			}
		}

		// Token: 0x04000039 RID: 57
		private readonly ImmutableSortedSet<T>.Builder _set;
	}
}
