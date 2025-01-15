using System;
using Microsoft.InfoNav;

namespace Microsoft.DataShaping.InternalContracts.Model
{
	// Token: 0x02000023 RID: 35
	public sealed class ComparerAnnotationProvider : IAnnotationProvider<ComparerAnnotation, IConceptualSchema>
	{
		// Token: 0x060000CC RID: 204 RVA: 0x000036E4 File Offset: 0x000018E4
		public ComparerAnnotationProvider(ComparerAnnotation annotation, IConceptualSchema schema)
		{
			this._schema = schema;
			this._annotation = new Lazy<ComparerAnnotation>(() => annotation);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003722 File Offset: 0x00001922
		public ComparerAnnotationProvider(Func<ComparerAnnotation> buildAnnotation, IConceptualSchema schema)
		{
			this._schema = schema;
			this._annotation = new Lazy<ComparerAnnotation>(buildAnnotation);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000373D File Offset: 0x0000193D
		public bool TryGetAnnotation(IConceptualSchema target, out ComparerAnnotation annotation)
		{
			annotation = this._annotation.Value;
			return true;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000374D File Offset: 0x0000194D
		public static ComparerAnnotation BuildComparerAnnotation(IConceptualSchema schema)
		{
			return new ComparerAnnotation(schema);
		}

		// Token: 0x04000066 RID: 102
		private Lazy<ComparerAnnotation> _annotation;

		// Token: 0x04000067 RID: 103
		private IConceptualSchema _schema;
	}
}
