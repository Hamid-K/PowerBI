using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Web.Http.Properties;
using System.Web.Http.Validation;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x0200004F RID: 79
	public class FormatterParameterBinding : HttpParameterBinding
	{
		// Token: 0x06000229 RID: 553 RVA: 0x000069F4 File Offset: 0x00004BF4
		public FormatterParameterBinding(HttpParameterDescriptor descriptor, IEnumerable<MediaTypeFormatter> formatters, IBodyModelValidator bodyModelValidator)
			: base(descriptor)
		{
			if (descriptor.IsOptional)
			{
				this._errorMessage = Error.Format(SRResources.OptionalBodyParameterNotSupported, new object[]
				{
					descriptor.Prefix ?? descriptor.ParameterName,
					base.GetType().Name
				});
			}
			this.Formatters = formatters;
			this.BodyModelValidator = bodyModelValidator;
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00006A55 File Offset: 0x00004C55
		public override bool WillReadBody
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00006A58 File Offset: 0x00004C58
		public override string ErrorMessage
		{
			get
			{
				return this._errorMessage;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00006A60 File Offset: 0x00004C60
		// (set) Token: 0x0600022D RID: 557 RVA: 0x00006A68 File Offset: 0x00004C68
		public IEnumerable<MediaTypeFormatter> Formatters
		{
			get
			{
				return this._formatters;
			}
			set
			{
				if (value == null)
				{
					throw Error.ArgumentNull("formatters");
				}
				this._formatters = value;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00006A7F File Offset: 0x00004C7F
		// (set) Token: 0x0600022F RID: 559 RVA: 0x00006A87 File Offset: 0x00004C87
		public IBodyModelValidator BodyModelValidator { get; set; }

		// Token: 0x06000230 RID: 560 RVA: 0x00006A90 File Offset: 0x00004C90
		public virtual Task<object> ReadContentAsync(HttpRequestMessage request, Type type, IEnumerable<MediaTypeFormatter> formatters, IFormatterLogger formatterLogger)
		{
			object obj;
			if (!request.Properties.TryGetValue("MS_FormatterParameterBinding_CancellationToken", out obj))
			{
				obj = CancellationToken.None;
			}
			return this.ReadContentAsync(request, type, formatters, formatterLogger, (CancellationToken)obj);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00006AD0 File Offset: 0x00004CD0
		public virtual Task<object> ReadContentAsync(HttpRequestMessage request, Type type, IEnumerable<MediaTypeFormatter> formatters, IFormatterLogger formatterLogger, CancellationToken cancellationToken)
		{
			HttpContent content = request.Content;
			if (content != null)
			{
				Task<object> task;
				try
				{
					task = content.ReadAsAsync(type, formatters, formatterLogger, cancellationToken);
				}
				catch (UnsupportedMediaTypeException ex)
				{
					string text = ((content.Headers.ContentType == null) ? SRResources.UnsupportedMediaTypeNoContentType : SRResources.UnsupportedMediaType);
					throw new HttpResponseException(request.CreateErrorResponse(HttpStatusCode.UnsupportedMediaType, Error.Format(text, new object[] { ex.MediaType.MediaType }), ex));
				}
				return task;
			}
			object defaultValueForType = MediaTypeFormatter.GetDefaultValueForType(type);
			if (defaultValueForType == null)
			{
				return TaskHelpers.NullResult();
			}
			return Task.FromResult<object>(defaultValueForType);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00006B68 File Offset: 0x00004D68
		public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext, CancellationToken cancellationToken)
		{
			HttpParameterDescriptor descriptor = base.Descriptor;
			Type parameterType = descriptor.ParameterType;
			HttpRequestMessage request = actionContext.ControllerContext.Request;
			IFormatterLogger formatterLogger = new ModelStateFormatterLogger(actionContext.ModelState, descriptor.ParameterName);
			return this.ExecuteBindingAsyncCore(metadataProvider, actionContext, descriptor, parameterType, request, formatterLogger, cancellationToken);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00006BB0 File Offset: 0x00004DB0
		private async Task ExecuteBindingAsyncCore(ModelMetadataProvider metadataProvider, HttpActionContext actionContext, HttpParameterDescriptor paramFromBody, Type type, HttpRequestMessage request, IFormatterLogger formatterLogger, CancellationToken cancellationToken)
		{
			request.Properties["MS_FormatterParameterBinding_CancellationToken"] = cancellationToken;
			object obj = await this.ReadContentAsync(request, type, this._formatters, formatterLogger);
			base.SetValue(actionContext, obj);
			if (this.BodyModelValidator != null)
			{
				this.BodyModelValidator.Validate(obj, type, metadataProvider, actionContext, paramFromBody.ParameterName);
			}
		}

		// Token: 0x0400007E RID: 126
		private const string CancellationTokenKey = "MS_FormatterParameterBinding_CancellationToken";

		// Token: 0x0400007F RID: 127
		private IEnumerable<MediaTypeFormatter> _formatters;

		// Token: 0x04000080 RID: 128
		private string _errorMessage;
	}
}
