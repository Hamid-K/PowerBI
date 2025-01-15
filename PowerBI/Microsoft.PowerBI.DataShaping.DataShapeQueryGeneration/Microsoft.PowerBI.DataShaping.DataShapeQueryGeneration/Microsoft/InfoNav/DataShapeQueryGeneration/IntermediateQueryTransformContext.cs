using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000D6 RID: 214
	internal sealed class IntermediateQueryTransformContext
	{
		// Token: 0x06000782 RID: 1922 RVA: 0x0001C491 File Offset: 0x0001A691
		internal IntermediateQueryTransformContext(IReadOnlyList<IntermediateQueryTransform> transforms, IIntermediateQueryTransformResolver resolver)
		{
			this._transforms = transforms;
			this._resolver = resolver;
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000783 RID: 1923 RVA: 0x0001C4A7 File Offset: 0x0001A6A7
		internal IReadOnlyList<IntermediateQueryTransform> Transforms
		{
			get
			{
				return this._transforms;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x0001C4AF File Offset: 0x0001A6AF
		internal IIntermediateQueryTransformResolver Resolver
		{
			get
			{
				return this._resolver;
			}
		}

		// Token: 0x040003E7 RID: 999
		internal static readonly IntermediateQueryTransformContext Empty = new IntermediateQueryTransformContext(Util.EmptyReadOnlyCollection<IntermediateQueryTransform>(), new IntermediateQueryTransformResolver());

		// Token: 0x040003E8 RID: 1000
		private readonly IReadOnlyList<IntermediateQueryTransform> _transforms;

		// Token: 0x040003E9 RID: 1001
		private readonly IIntermediateQueryTransformResolver _resolver;
	}
}
