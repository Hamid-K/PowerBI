using System;

namespace Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations
{
	// Token: 0x02000135 RID: 309
	public sealed class MParameterAnnotationProvider : IAnnotationProvider<MParameterAnnotation, IConceptualSchema>
	{
		// Token: 0x06000802 RID: 2050 RVA: 0x0001099E File Offset: 0x0000EB9E
		public MParameterAnnotationProvider(IConceptualSchema schema)
		{
			this._targetSchema = schema;
			this._annotation = new Lazy<MParameterAnnotation>(new Func<MParameterAnnotation>(this.BuildAnnotation));
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x000109C4 File Offset: 0x0000EBC4
		public MParameterAnnotationProvider(MParameterAnnotation annotation, IConceptualSchema schema)
		{
			this._targetSchema = schema;
			this._annotation = new Lazy<MParameterAnnotation>(new Func<MParameterAnnotation>(this.BuildAnnotation));
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x000109EA File Offset: 0x0000EBEA
		public bool TryGetAnnotation(IConceptualSchema target, out MParameterAnnotation annotation)
		{
			annotation = this._annotation.Value;
			return true;
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x000109FA File Offset: 0x0000EBFA
		private MParameterAnnotation BuildAnnotation()
		{
			return new MParameterAnnotation(this._targetSchema);
		}

		// Token: 0x040003B1 RID: 945
		private readonly Lazy<MParameterAnnotation> _annotation;

		// Token: 0x040003B2 RID: 946
		private readonly IConceptualSchema _targetSchema;
	}
}
