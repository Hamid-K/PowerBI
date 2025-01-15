using System;
using System.Collections.Generic;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000013 RID: 19
	internal class PerRequestParameterBinding : HttpParameterBinding
	{
		// Token: 0x0600006A RID: 106 RVA: 0x0000329B File Offset: 0x0000149B
		public PerRequestParameterBinding(HttpParameterDescriptor descriptor, IEnumerable<MediaTypeFormatter> formatters)
			: base(descriptor)
		{
			if (formatters == null)
			{
				throw Error.ArgumentNull("formatters");
			}
			this._formatters = formatters;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000032B9 File Offset: 0x000014B9
		public override bool WillReadBody
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000032BC File Offset: 0x000014BC
		public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext, CancellationToken cancellationToken)
		{
			List<MediaTypeFormatter> list = new List<MediaTypeFormatter>();
			foreach (MediaTypeFormatter mediaTypeFormatter in this._formatters)
			{
				MediaTypeFormatter perRequestFormatterInstance = mediaTypeFormatter.GetPerRequestFormatterInstance(base.Descriptor.ParameterType, actionContext.Request, actionContext.Request.Content.Headers.ContentType);
				list.Add(perRequestFormatterInstance);
			}
			return this.CreateInnerBinding(list).ExecuteBindingAsync(metadataProvider, actionContext, cancellationToken);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x0000334C File Offset: 0x0000154C
		protected virtual HttpParameterBinding CreateInnerBinding(IEnumerable<MediaTypeFormatter> perRequestFormatters)
		{
			return ParameterBindingExtensions.BindWithFormatter(base.Descriptor, perRequestFormatters);
		}

		// Token: 0x04000017 RID: 23
		private IEnumerable<MediaTypeFormatter> _formatters;
	}
}
