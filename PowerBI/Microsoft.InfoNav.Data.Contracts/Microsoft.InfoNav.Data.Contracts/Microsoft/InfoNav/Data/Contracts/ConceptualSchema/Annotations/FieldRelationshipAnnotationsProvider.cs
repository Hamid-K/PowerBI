using System;

namespace Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations
{
	// Token: 0x02000133 RID: 307
	public sealed class FieldRelationshipAnnotationsProvider : IAnnotationProvider<FieldRelationshipAnnotations, IConceptualSchema>
	{
		// Token: 0x060007FC RID: 2044 RVA: 0x000108A8 File Offset: 0x0000EAA8
		public FieldRelationshipAnnotationsProvider(FieldRelationshipAnnotations annotations, IConceptualSchema schema)
		{
			this._schema = schema;
			this._annotations = new Lazy<FieldRelationshipAnnotations>(() => annotations);
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x000108E6 File Offset: 0x0000EAE6
		public FieldRelationshipAnnotationsProvider(Func<FieldRelationshipAnnotations> buildAnnotations, IConceptualSchema schema)
		{
			this._schema = schema;
			this._annotations = new Lazy<FieldRelationshipAnnotations>(buildAnnotations);
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00010901 File Offset: 0x0000EB01
		public bool TryGetAnnotation(IConceptualSchema target, out FieldRelationshipAnnotations annotation)
		{
			annotation = this._annotations.Value;
			return true;
		}

		// Token: 0x040003AE RID: 942
		private Lazy<FieldRelationshipAnnotations> _annotations;

		// Token: 0x040003AF RID: 943
		private IConceptualSchema _schema;
	}
}
