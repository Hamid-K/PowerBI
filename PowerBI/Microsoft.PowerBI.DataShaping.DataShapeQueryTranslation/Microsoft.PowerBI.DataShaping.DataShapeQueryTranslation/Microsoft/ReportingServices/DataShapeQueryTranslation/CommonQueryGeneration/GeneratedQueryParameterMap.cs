using System;
using System.Collections.Generic;
using Microsoft.InfoNav;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.CommonQueryGeneration
{
	// Token: 0x02000115 RID: 277
	internal class GeneratedQueryParameterMap
	{
		// Token: 0x06000A85 RID: 2693 RVA: 0x00028C1F File Offset: 0x00026E1F
		internal GeneratedQueryParameterMap()
		{
			this._parametersByDsqName = new Dictionary<string, QueryParameterReferenceExpression>(ConceptualNameComparer.Instance);
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x00028C37 File Offset: 0x00026E37
		internal int Count
		{
			get
			{
				return this._parametersByDsqName.Count;
			}
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x00028C44 File Offset: 0x00026E44
		public bool TryGetParameterReference(string dsqName, out QueryParameterReferenceExpression parameterRef)
		{
			return this._parametersByDsqName.TryGetValue(dsqName, out parameterRef);
		}

		// Token: 0x04000540 RID: 1344
		internal static readonly GeneratedQueryParameterMap Empty = new GeneratedQueryParameterMap();

		// Token: 0x04000541 RID: 1345
		protected readonly Dictionary<string, QueryParameterReferenceExpression> _parametersByDsqName;
	}
}
