using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations;

namespace Microsoft.InfoNav.Data.Edm
{
	// Token: 0x0200000B RID: 11
	internal sealed class WritableFieldRelationshipAnnotation : FieldRelationshipAnnotation
	{
		// Token: 0x06000022 RID: 34 RVA: 0x000029C8 File Offset: 0x00000BC8
		internal WritableFieldRelationshipAnnotation()
		{
			this.LeafMemberships = new HashSet<IConceptualColumn>();
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000029DB File Offset: 0x00000BDB
		public HashSet<IConceptualColumn> LeafMemberships { get; }

		// Token: 0x06000024 RID: 36 RVA: 0x000029E3 File Offset: 0x00000BE3
		public void SetRelatedToFields(IReadOnlyList<IConceptualColumn> relatedToFields)
		{
			this._relatedToFields = relatedToFields;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000029EC File Offset: 0x00000BEC
		public void SetRelatedToSource(IConceptualColumn relatedToSource)
		{
			this._relatedToSource = relatedToSource;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000029F5 File Offset: 0x00000BF5
		public void SetAllFieldsOnPath(IReadOnlyList<IConceptualColumn> allFieldsOnPath)
		{
			this._allFieldsOnPath = allFieldsOnPath;
		}
	}
}
