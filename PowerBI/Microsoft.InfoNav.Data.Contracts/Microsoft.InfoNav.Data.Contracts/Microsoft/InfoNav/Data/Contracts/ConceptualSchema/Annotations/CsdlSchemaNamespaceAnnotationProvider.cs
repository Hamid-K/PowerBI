using System;

namespace Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations
{
	// Token: 0x02000129 RID: 297
	public sealed class CsdlSchemaNamespaceAnnotationProvider : IAnnotationProvider<CsdlSchemaNamespaceAnnotation, IConceptualSchema>
	{
		// Token: 0x060007C2 RID: 1986 RVA: 0x0001022A File Offset: 0x0000E42A
		public CsdlSchemaNamespaceAnnotationProvider(CsdlSchemaNamespaceAnnotation annotation)
		{
			this._annotation = annotation;
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00010239 File Offset: 0x0000E439
		public bool TryGetAnnotation(IConceptualSchema target, out CsdlSchemaNamespaceAnnotation annotation)
		{
			annotation = this._annotation;
			return true;
		}

		// Token: 0x04000384 RID: 900
		private CsdlSchemaNamespaceAnnotation _annotation;
	}
}
