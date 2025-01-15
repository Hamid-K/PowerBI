using System;
using System.Collections.Generic;
using Microsoft.InfoNav;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x020001E9 RID: 489
	internal sealed class EdmConceptualPropertyInitContext
	{
		// Token: 0x0600174D RID: 5965 RVA: 0x0003FC00 File Offset: 0x0003DE00
		internal EdmConceptualPropertyInitContext(EdmConceptualEntity entity, EntitySet edmEntitySet, Dictionary<string, Dictionary<string, List<ConceptualMParameter>>> mappedMParameters)
		{
			this._entity = entity;
			this._edmEntitySet = edmEntitySet;
			this._mappedMParameters = mappedMParameters;
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x0600174E RID: 5966 RVA: 0x0003FC1D File Offset: 0x0003DE1D
		internal EdmConceptualEntity Entity
		{
			get
			{
				return this._entity;
			}
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x0600174F RID: 5967 RVA: 0x0003FC25 File Offset: 0x0003DE25
		internal EntitySet EdmEntitySet
		{
			get
			{
				return this._edmEntitySet;
			}
		}

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06001750 RID: 5968 RVA: 0x0003FC2D File Offset: 0x0003DE2D
		internal IReadOnlyDictionary<string, Dictionary<string, List<ConceptualMParameter>>> MappedMParameters
		{
			get
			{
				return this._mappedMParameters;
			}
		}

		// Token: 0x06001751 RID: 5969 RVA: 0x0003FC38 File Offset: 0x0003DE38
		internal T GetProperty<T>(EdmProperty edmProperty) where T : class, IConceptualProperty
		{
			IConceptualProperty conceptualProperty;
			this._entity.TryGetPropertyByEdmName(edmProperty.Name, out conceptualProperty);
			return conceptualProperty as T;
		}

		// Token: 0x04000C67 RID: 3175
		private readonly EdmConceptualEntity _entity;

		// Token: 0x04000C68 RID: 3176
		private readonly EntitySet _edmEntitySet;

		// Token: 0x04000C69 RID: 3177
		private readonly Dictionary<string, Dictionary<string, List<ConceptualMParameter>>> _mappedMParameters;
	}
}
