using System;
using System.Collections.Concurrent;

namespace Microsoft.InfoNav.Data.Library
{
	// Token: 0x02000070 RID: 112
	public sealed class ConceptualAnnotationManager : IAnnotatableRoot
	{
		// Token: 0x06000243 RID: 579 RVA: 0x00006F07 File Offset: 0x00005107
		public ConceptualAnnotationManager()
			: this(new ConcurrentDictionary<Type, object>())
		{
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00006F14 File Offset: 0x00005114
		private ConceptualAnnotationManager(ConcurrentDictionary<Type, object> annotationProviders)
		{
			this._annotationProviders = annotationProviders;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00006F23 File Offset: 0x00005123
		public bool RegisterAnnotationProvider<TAnnotation, TTarget>(IAnnotationProvider<TAnnotation, TTarget> annotationProvider)
		{
			return this._annotationProviders.TryAdd(typeof(IAnnotationProvider<TAnnotation, TTarget>), annotationProvider);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00006F3C File Offset: 0x0000513C
		public IAnnotationProvider<TAnnotation, TTarget> RegisterAnnotationProvider<TAnnotation, TTarget>(Func<IAnnotationProvider<TAnnotation, TTarget>> annotationProviderCreator)
		{
			return this._annotationProviders.GetOrAdd(typeof(IAnnotationProvider<TAnnotation, TTarget>), (Type t) => annotationProviderCreator()) as IAnnotationProvider<TAnnotation, TTarget>;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00006F7C File Offset: 0x0000517C
		public bool TryGetAnnotationProvider<TAnnotation, TTarget>(out IAnnotationProvider<TAnnotation, TTarget> annotationProvider)
		{
			object obj;
			if (this._annotationProviders.TryGetValue(typeof(IAnnotationProvider<TAnnotation, TTarget>), out obj))
			{
				annotationProvider = (IAnnotationProvider<TAnnotation, TTarget>)obj;
				return true;
			}
			annotationProvider = null;
			return false;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00006FB0 File Offset: 0x000051B0
		public ConceptualAnnotationManager Clone()
		{
			return new ConceptualAnnotationManager(new ConcurrentDictionary<Type, object>(this._annotationProviders));
		}

		// Token: 0x0400017A RID: 378
		private readonly ConcurrentDictionary<Type, object> _annotationProviders;
	}
}
