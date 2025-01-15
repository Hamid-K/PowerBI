using System;

namespace Microsoft.InfoNav
{
	// Token: 0x02000010 RID: 16
	public interface IAnnotationProvider<TAnnotation, TTarget>
	{
		// Token: 0x06000022 RID: 34
		bool TryGetAnnotation(TTarget target, out TAnnotation annotation);
	}
}
