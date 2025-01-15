using System;

namespace Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations
{
	// Token: 0x02000131 RID: 305
	public static class ErrorStateAnnotationExtensions
	{
		// Token: 0x060007F4 RID: 2036 RVA: 0x00010754 File Offset: 0x0000E954
		public static bool IsInErrorState(this IConceptualProperty property)
		{
			ErrorStateAnnotation errorStateAnnotation;
			return property.TryGetAnnotation(out errorStateAnnotation) && errorStateAnnotation.IsError;
		}
	}
}
