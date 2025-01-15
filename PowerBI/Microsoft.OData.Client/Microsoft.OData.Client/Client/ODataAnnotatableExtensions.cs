using System;
using System.Runtime.CompilerServices;

namespace Microsoft.OData.Client
{
	// Token: 0x02000032 RID: 50
	internal static class ODataAnnotatableExtensions
	{
		// Token: 0x06000191 RID: 401 RVA: 0x00008143 File Offset: 0x00006343
		public static void SetAnnotation<T>(this ODataAnnotatable annotatable, T annotation) where T : class
		{
			ODataAnnotatableExtensions.InternalDictionary<T>.SetAnnotation(annotatable, annotation);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000814C File Offset: 0x0000634C
		public static T GetAnnotation<T>(this ODataAnnotatable annotatable) where T : class
		{
			return ODataAnnotatableExtensions.InternalDictionary<T>.GetAnnotation(annotatable);
		}

		// Token: 0x02000161 RID: 353
		private static class InternalDictionary<T> where T : class
		{
			// Token: 0x06000D4F RID: 3407 RVA: 0x0002E8D2 File Offset: 0x0002CAD2
			public static void SetAnnotation(ODataAnnotatable annotatable, T annotation)
			{
				ODataAnnotatableExtensions.InternalDictionary<T>.Dictionary.Add(annotatable, annotation);
			}

			// Token: 0x06000D50 RID: 3408 RVA: 0x0002E8E0 File Offset: 0x0002CAE0
			public static T GetAnnotation(ODataAnnotatable annotatable)
			{
				T t;
				if (ODataAnnotatableExtensions.InternalDictionary<T>.Dictionary.TryGetValue(annotatable, out t))
				{
					return t;
				}
				return default(T);
			}

			// Token: 0x04000705 RID: 1797
			private static readonly ConditionalWeakTable<ODataAnnotatable, T> Dictionary = new ConditionalWeakTable<ODataAnnotatable, T>();
		}
	}
}
