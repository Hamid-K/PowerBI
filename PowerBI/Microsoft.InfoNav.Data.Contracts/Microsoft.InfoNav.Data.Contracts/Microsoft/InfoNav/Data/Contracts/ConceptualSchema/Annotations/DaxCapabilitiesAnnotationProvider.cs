using System;

namespace Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations
{
	// Token: 0x0200012B RID: 299
	public sealed class DaxCapabilitiesAnnotationProvider : IAnnotationProvider<DaxCapabilitiesAnnotation, IConceptualSchema>
	{
		// Token: 0x060007C6 RID: 1990 RVA: 0x0001025B File Offset: 0x0000E45B
		public DaxCapabilitiesAnnotationProvider(DaxCapabilitiesAnnotation annotation)
		{
			this._annotation = annotation;
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0001026A File Offset: 0x0000E46A
		public bool TryGetAnnotation(IConceptualSchema target, out DaxCapabilitiesAnnotation annotation)
		{
			annotation = this._annotation;
			return true;
		}

		// Token: 0x04000386 RID: 902
		private DaxCapabilitiesAnnotation _annotation;
	}
}
