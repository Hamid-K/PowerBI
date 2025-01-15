using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Azure
{
	// Token: 0x0200002A RID: 42
	[NullableContext(1)]
	[Nullable(new byte[] { 0, 1, 1 })]
	public abstract class PageableOperation<T> : Operation<AsyncPageable<T>>
	{
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000BA RID: 186 RVA: 0x000031B0 File Offset: 0x000013B0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override AsyncPageable<T> Value
		{
			get
			{
				return this.GetValuesAsync(default(CancellationToken));
			}
		}

		// Token: 0x060000BB RID: 187
		public abstract AsyncPageable<T> GetValuesAsync(CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x060000BC RID: 188
		public abstract Pageable<T> GetValues(CancellationToken cancellationToken = default(CancellationToken));
	}
}
