using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x020020BE RID: 8382
	[NullableContext(1)]
	[Nullable(0)]
	internal class ImmutableSortedSetBuilderDebuggerProxy<[Nullable(2)] T>
	{
		// Token: 0x0601198C RID: 72076 RVA: 0x003C3024 File Offset: 0x003C1224
		public ImmutableSortedSetBuilderDebuggerProxy(ImmutableSortedSet<T>.Builder builder)
		{
			Requires.NotNull<ImmutableSortedSet<T>.Builder>(builder, "builder");
			this._set = builder;
		}

		// Token: 0x17002F35 RID: 12085
		// (get) Token: 0x0601198D RID: 72077 RVA: 0x003C303E File Offset: 0x003C123E
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Contents
		{
			get
			{
				return this._set.ToArray(this._set.Count);
			}
		}

		// Token: 0x0400698E RID: 27022
		private readonly ImmutableSortedSet<T>.Builder _set;
	}
}
