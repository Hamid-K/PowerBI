using System;
using System.Linq;
using Microsoft.AspNet.OData.Formatter.Serialization;

namespace Microsoft.AspNet.OData.Query
{
	// Token: 0x020000A9 RID: 169
	public abstract class SkipTokenHandler
	{
		// Token: 0x060005DC RID: 1500
		public abstract IQueryable<T> ApplyTo<T>(IQueryable<T> query, SkipTokenQueryOption skipTokenQueryOption);

		// Token: 0x060005DD RID: 1501
		public abstract IQueryable ApplyTo(IQueryable query, SkipTokenQueryOption skipTokenQueryOption);

		// Token: 0x060005DE RID: 1502
		public abstract Uri GenerateNextPageLink(Uri baseUri, int pageSize, object instance, ODataSerializerContext context);
	}
}
