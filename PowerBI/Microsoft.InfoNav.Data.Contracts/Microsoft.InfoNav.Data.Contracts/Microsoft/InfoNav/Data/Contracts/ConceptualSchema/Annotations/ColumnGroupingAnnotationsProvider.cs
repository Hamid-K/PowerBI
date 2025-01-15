using System;

namespace Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations
{
	// Token: 0x02000125 RID: 293
	public sealed class ColumnGroupingAnnotationsProvider : IAnnotationProvider<ColumnGroupingAnnotations, IConceptualSchema>
	{
		// Token: 0x060007A6 RID: 1958 RVA: 0x0000FE90 File Offset: 0x0000E090
		public ColumnGroupingAnnotationsProvider(ColumnGroupingAnnotations annotations, IConceptualSchema schema)
		{
			this._schema = schema;
			this._annotations = new Lazy<ColumnGroupingAnnotations>(() => annotations);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0000FECE File Offset: 0x0000E0CE
		public ColumnGroupingAnnotationsProvider(Func<ColumnGroupingAnnotations> buildAnnotations, IConceptualSchema schema)
		{
			this._schema = schema;
			this._annotations = new Lazy<ColumnGroupingAnnotations>(buildAnnotations);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0000FEE9 File Offset: 0x0000E0E9
		public bool TryGetAnnotation(IConceptualSchema target, out ColumnGroupingAnnotations annotation)
		{
			annotation = this._annotations.Value;
			return true;
		}

		// Token: 0x0400037E RID: 894
		private Lazy<ColumnGroupingAnnotations> _annotations;

		// Token: 0x0400037F RID: 895
		private IConceptualSchema _schema;
	}
}
