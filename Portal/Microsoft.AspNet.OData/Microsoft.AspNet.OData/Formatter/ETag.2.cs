using System;
using System.Linq;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Formatter
{
	// Token: 0x02000190 RID: 400
	public class ETag<TEntity> : ETag
	{
		// Token: 0x06000D15 RID: 3349 RVA: 0x00034140 File Offset: 0x00032340
		public ETag()
		{
			base.EntityType = typeof(TEntity);
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x00034158 File Offset: 0x00032358
		public override IQueryable ApplyTo(IQueryable query)
		{
			ETag<TEntity>.ValidateQuery(query);
			return base.ApplyTo(query);
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x00034167 File Offset: 0x00032367
		public IQueryable<TEntity> ApplyTo(IQueryable<TEntity> query)
		{
			if (query == null)
			{
				throw Error.ArgumentNull("query");
			}
			return (IQueryable<TEntity>)base.ApplyTo(query);
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x00034184 File Offset: 0x00032384
		private static void ValidateQuery(IQueryable query)
		{
			if (query == null)
			{
				throw Error.ArgumentNull("query");
			}
			if (!TypeHelper.IsTypeAssignableFrom(typeof(TEntity), query.ElementType))
			{
				throw Error.Argument("query", SRResources.CannotApplyETagOfT, new object[]
				{
					typeof(ETag).Name,
					typeof(TEntity).FullName,
					typeof(IQueryable).Name,
					query.ElementType.FullName
				});
			}
		}
	}
}
