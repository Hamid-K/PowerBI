using System;

namespace Microsoft.InfoNav
{
	// Token: 0x02000011 RID: 17
	public interface IAnnotatableRoot
	{
		// Token: 0x06000023 RID: 35
		bool RegisterAnnotationProvider<TAnnotation, TTarget>(IAnnotationProvider<TAnnotation, TTarget> annotationProvider);

		// Token: 0x06000024 RID: 36
		IAnnotationProvider<TAnnotation, TTarget> RegisterAnnotationProvider<TAnnotation, TTarget>(Func<IAnnotationProvider<TAnnotation, TTarget>> annotationProviderCreator);

		// Token: 0x06000025 RID: 37
		bool TryGetAnnotationProvider<TAnnotation, TTarget>(out IAnnotationProvider<TAnnotation, TTarget> annotationProvider);
	}
}
