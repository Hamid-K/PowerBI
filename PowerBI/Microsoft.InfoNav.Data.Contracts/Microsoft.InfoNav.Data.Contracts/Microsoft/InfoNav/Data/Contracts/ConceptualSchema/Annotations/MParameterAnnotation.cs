using System;

namespace Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations
{
	// Token: 0x02000136 RID: 310
	public sealed class MParameterAnnotation
	{
		// Token: 0x06000806 RID: 2054 RVA: 0x00010A07 File Offset: 0x0000EC07
		public MParameterAnnotation(IConceptualSchema schema)
		{
			this.HasMappedParameters = this.ComputeHasMappedParameters(schema);
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000807 RID: 2055 RVA: 0x00010A1C File Offset: 0x0000EC1C
		public bool HasMappedParameters { get; }

		// Token: 0x06000808 RID: 2056 RVA: 0x00010A24 File Offset: 0x0000EC24
		private bool ComputeHasMappedParameters(IConceptualSchema schema)
		{
			foreach (IConceptualEntity conceptualEntity in schema.Entities)
			{
				foreach (IConceptualProperty conceptualProperty in conceptualEntity.Properties)
				{
					IConceptualColumn conceptualColumn = conceptualProperty as IConceptualColumn;
					if (conceptualColumn != null && conceptualColumn.HasMappedParameter())
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
