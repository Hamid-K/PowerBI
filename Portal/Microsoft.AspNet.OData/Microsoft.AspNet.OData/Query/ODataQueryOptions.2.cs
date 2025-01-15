using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter;

namespace Microsoft.AspNet.OData.Query
{
	// Token: 0x020000A5 RID: 165
	[ODataQueryParameterBinding]
	public class ODataQueryOptions<TEntity> : ODataQueryOptions
	{
		// Token: 0x060005C3 RID: 1475 RVA: 0x000147C4 File Offset: 0x000129C4
		public ODataQueryOptions(ODataQueryContext context, HttpRequestMessage request)
			: base(context, request)
		{
			if (base.Context.ElementClrType == null)
			{
				throw Error.Argument("context", SRResources.ElementClrTypeNull, new object[] { typeof(ODataQueryContext).Name });
			}
			if (context.ElementClrType != typeof(TEntity))
			{
				throw Error.Argument("context", SRResources.EntityTypeMismatch, new object[]
				{
					context.ElementClrType.FullName,
					typeof(TEntity).FullName
				});
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x00014861 File Offset: 0x00012A61
		public new ETag<TEntity> IfMatch
		{
			get
			{
				return base.IfMatch as ETag<TEntity>;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x0001486E File Offset: 0x00012A6E
		public new ETag<TEntity> IfNoneMatch
		{
			get
			{
				return base.IfNoneMatch as ETag<TEntity>;
			}
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x0001487B File Offset: 0x00012A7B
		internal override ETag GetETag(EntityTagHeaderValue etagHeaderValue)
		{
			return base.InternalRequest.GetETag<TEntity>(etagHeaderValue);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00014889 File Offset: 0x00012A89
		public override IQueryable ApplyTo(IQueryable query)
		{
			ODataQueryOptions<TEntity>.ValidateQuery(query);
			return base.ApplyTo(query);
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00014898 File Offset: 0x00012A98
		public override IQueryable ApplyTo(IQueryable query, ODataQuerySettings querySettings)
		{
			ODataQueryOptions<TEntity>.ValidateQuery(query);
			return base.ApplyTo(query, querySettings);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x000148A8 File Offset: 0x00012AA8
		private static void ValidateQuery(IQueryable query)
		{
			if (query == null)
			{
				throw Error.ArgumentNull("query");
			}
			if (!TypeHelper.IsTypeAssignableFrom(typeof(TEntity), query.ElementType))
			{
				throw Error.Argument("query", SRResources.CannotApplyODataQueryOptionsOfT, new object[]
				{
					typeof(ODataQueryOptions).Name,
					typeof(TEntity).FullName,
					typeof(IQueryable).Name,
					query.ElementType.FullName
				});
			}
		}
	}
}
