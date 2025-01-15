using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x02000103 RID: 259
	internal abstract class BuilderBase<TType, TParent> : BuilderBase<TType>
	{
		// Token: 0x06000708 RID: 1800 RVA: 0x0000F3AB File Offset: 0x0000D5AB
		protected BuilderBase(TParent parent, TType activeObject)
			: base(activeObject)
		{
			this._parent = parent;
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x0000F3BB File Offset: 0x0000D5BB
		public TParent Parent()
		{
			return this._parent;
		}

		// Token: 0x040002C2 RID: 706
		private readonly TParent _parent;
	}
}
