using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Web.Http.ModelBinding;
using System.Web.Http.Properties;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x02000137 RID: 311
	internal class FormatterParameterBindingTracer : FormatterParameterBinding, IDecorator<FormatterParameterBinding>
	{
		// Token: 0x06000841 RID: 2113 RVA: 0x00014E3C File Offset: 0x0001303C
		public FormatterParameterBindingTracer(FormatterParameterBinding innerBinding, ITraceWriter traceWriter)
			: base(innerBinding.Descriptor, innerBinding.Formatters, innerBinding.BodyModelValidator)
		{
			this._innerBinding = innerBinding;
			this._traceWriter = traceWriter;
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000842 RID: 2114 RVA: 0x00014E64 File Offset: 0x00013064
		public FormatterParameterBinding Inner
		{
			get
			{
				return this._innerBinding;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x00014E6C File Offset: 0x0001306C
		public override string ErrorMessage
		{
			get
			{
				return this._innerBinding.ErrorMessage;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x00014E79 File Offset: 0x00013079
		public override bool WillReadBody
		{
			get
			{
				return this._innerBinding.WillReadBody;
			}
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00014E86 File Offset: 0x00013086
		public override Task<object> ReadContentAsync(HttpRequestMessage request, Type type, IEnumerable<MediaTypeFormatter> formatters, IFormatterLogger formatterLogger)
		{
			return this._innerBinding.ReadContentAsync(request, type, this.CreateFormatterTracers(request, formatters), formatterLogger);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x00014E9F File Offset: 0x0001309F
		public override Task<object> ReadContentAsync(HttpRequestMessage request, Type type, IEnumerable<MediaTypeFormatter> formatters, IFormatterLogger formatterLogger, CancellationToken cancellationToken)
		{
			return this._innerBinding.ReadContentAsync(request, type, formatters, formatterLogger, cancellationToken);
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00014EB4 File Offset: 0x000130B4
		public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext, CancellationToken cancellationToken)
		{
			return this._traceWriter.TraceBeginEndAsync(actionContext.Request, TraceCategories.ModelBindingCategory, TraceLevel.Info, this._innerBinding.GetType().Name, "ExecuteBindingAsync", delegate(TraceRecord tr)
			{
				tr.Message = Error.Format(SRResources.TraceBeginParameterBind, new object[] { this._innerBinding.Descriptor.ParameterName });
			}, () => this._innerBinding.ExecuteBindingAsync(metadataProvider, actionContext, cancellationToken), delegate(TraceRecord tr)
			{
				string parameterName = this._innerBinding.Descriptor.ParameterName;
				tr.Message = (actionContext.ActionArguments.ContainsKey(parameterName) ? Error.Format(SRResources.TraceEndParameterBind, new object[]
				{
					parameterName,
					FormattingUtilities.ValueToString(actionContext.ActionArguments[parameterName], CultureInfo.CurrentCulture)
				}) : Error.Format(SRResources.TraceEndParameterBindNoBind, new object[] { parameterName }));
			}, null);
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x00014F3C File Offset: 0x0001313C
		private IEnumerable<MediaTypeFormatter> CreateFormatterTracers(HttpRequestMessage request, IEnumerable<MediaTypeFormatter> formatters)
		{
			List<MediaTypeFormatter> list = new List<MediaTypeFormatter>();
			foreach (MediaTypeFormatter mediaTypeFormatter in formatters)
			{
				list.Add(MediaTypeFormatterTracer.CreateTracer(mediaTypeFormatter, this._traceWriter, request));
			}
			return list;
		}

		// Token: 0x04000240 RID: 576
		private const string ExecuteBindingAsyncMethodName = "ExecuteBindingAsync";

		// Token: 0x04000241 RID: 577
		private readonly FormatterParameterBinding _innerBinding;

		// Token: 0x04000242 RID: 578
		private readonly ITraceWriter _traceWriter;
	}
}
