using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x020001F5 RID: 501
	internal static class EdmFieldRelationshipPathSegmentExtensions
	{
		// Token: 0x060017E7 RID: 6119 RVA: 0x00041FE8 File Offset: 0x000401E8
		internal static IEnumerable<IEdmFieldInstance> GetFieldInstances(this IEnumerable<EdmFieldRelationshipPathSegment> pathSegments)
		{
			return pathSegments.SelectMany(delegate(EdmFieldRelationshipPathSegment pathSegment)
			{
				EdmFieldRelationshipPathSegment edmFieldRelationshipPathSegment = pathSegment;
				return edmFieldRelationshipPathSegment.Highest.GetLowerRelationshipPath();
			}, (EdmFieldRelationshipPathSegment pathSegment, IEdmFieldInstance pathFieldInstance) => pathFieldInstance);
		}
	}
}
