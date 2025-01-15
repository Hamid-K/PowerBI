using System;
using System.Text;

namespace NLog.Internal
{
	// Token: 0x02000109 RID: 265
	internal struct AppendBuilderCreator : IDisposable
	{
		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000E62 RID: 3682 RVA: 0x00023B40 File Offset: 0x00021D40
		public StringBuilder Builder
		{
			get
			{
				return this._builder.Item;
			}
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x00023B4D File Offset: 0x00021D4D
		public AppendBuilderCreator(StringBuilder appendTarget, bool mustBeEmpty)
		{
			this._appendTarget = appendTarget;
			if (this._appendTarget.Length > 0 && mustBeEmpty)
			{
				this._builder = AppendBuilderCreator._builderPool.Acquire();
				return;
			}
			this._builder = new StringBuilderPool.ItemHolder(this._appendTarget, null, 0);
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x00023B8C File Offset: 0x00021D8C
		public void Dispose()
		{
			if (this._builder.Item != this._appendTarget)
			{
				this._builder.Item.CopyTo(this._appendTarget);
				this._builder.Dispose();
			}
		}

		// Token: 0x040003DC RID: 988
		private static readonly StringBuilderPool _builderPool = new StringBuilderPool(Environment.ProcessorCount * 2, 1024, 524288);

		// Token: 0x040003DD RID: 989
		private readonly StringBuilder _appendTarget;

		// Token: 0x040003DE RID: 990
		private readonly StringBuilderPool.ItemHolder _builder;
	}
}
