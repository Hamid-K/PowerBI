using System;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Results;

namespace Microsoft.AspNet.OData
{
	// Token: 0x0200000F RID: 15
	[ODataFormatting]
	[ODataRouting]
	[ApiExplorerSettings(IgnoreApi = true)]
	public abstract class ODataController : ApiController
	{
		// Token: 0x0600005F RID: 95 RVA: 0x0000311A File Offset: 0x0000131A
		protected override void Dispose(bool disposing)
		{
			if (disposing && base.Request != null)
			{
				base.Request.DeleteRequestContainer(true);
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000313A File Offset: 0x0000133A
		protected virtual CreatedODataResult<TEntity> Created<TEntity>(TEntity entity)
		{
			if (entity == null)
			{
				throw Error.ArgumentNull("entity");
			}
			return new CreatedODataResult<TEntity>(entity, this);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003156 File Offset: 0x00001356
		protected virtual UpdatedODataResult<TEntity> Updated<TEntity>(TEntity entity)
		{
			if (entity == null)
			{
				throw Error.ArgumentNull("entity");
			}
			return new UpdatedODataResult<TEntity>(entity, this);
		}
	}
}
