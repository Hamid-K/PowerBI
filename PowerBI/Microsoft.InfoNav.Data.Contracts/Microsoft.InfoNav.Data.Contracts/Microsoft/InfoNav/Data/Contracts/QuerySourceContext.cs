using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Contracts
{
	// Token: 0x02000090 RID: 144
	[ImmutableObject(true)]
	internal sealed class QuerySourceContext
	{
		// Token: 0x06000346 RID: 838 RVA: 0x000095E0 File Offset: 0x000077E0
		internal QuerySourceContext(IReadOnlyDictionary<string, ResolvedQuerySource> sourceMap, IFederatedConceptualSchema federatedSchema)
		{
			this._sourceMap = sourceMap;
			this._federatedSchema = federatedSchema;
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000347 RID: 839 RVA: 0x000095F6 File Offset: 0x000077F6
		internal IReadOnlyDictionary<string, ResolvedQuerySource> SourceMap
		{
			get
			{
				return this._sourceMap;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000348 RID: 840 RVA: 0x000095FE File Offset: 0x000077FE
		internal IFederatedConceptualSchema FederatedSchema
		{
			get
			{
				return this._federatedSchema;
			}
		}

		// Token: 0x040001C7 RID: 455
		private readonly IReadOnlyDictionary<string, ResolvedQuerySource> _sourceMap;

		// Token: 0x040001C8 RID: 456
		private readonly IFederatedConceptualSchema _federatedSchema;
	}
}
