using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Web.Http.ModelBinding;
using System.Web.Http.Properties;
using System.Web.Http.Services;
using System.Web.Http.ValueProviders;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x02000139 RID: 313
	internal class HttpParameterBindingTracer : HttpParameterBinding, IValueProviderParameterBinding, IDecorator<HttpParameterBinding>
	{
		// Token: 0x06000857 RID: 2135 RVA: 0x0001507F File Offset: 0x0001327F
		public HttpParameterBindingTracer(HttpParameterBinding innerBinding, ITraceWriter traceWriter)
			: base(innerBinding.Descriptor)
		{
			this.InnerBinding = innerBinding;
			this.TraceWriter = traceWriter;
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x0001509B File Offset: 0x0001329B
		public HttpParameterBinding Inner
		{
			get
			{
				return this.InnerBinding;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x000150A3 File Offset: 0x000132A3
		// (set) Token: 0x0600085A RID: 2138 RVA: 0x000150AB File Offset: 0x000132AB
		private protected HttpParameterBinding InnerBinding { protected get; private set; }

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x000150B4 File Offset: 0x000132B4
		// (set) Token: 0x0600085C RID: 2140 RVA: 0x000150BC File Offset: 0x000132BC
		private protected ITraceWriter TraceWriter { protected get; private set; }

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x000150C5 File Offset: 0x000132C5
		public override string ErrorMessage
		{
			get
			{
				return this.InnerBinding.ErrorMessage;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x000150D2 File Offset: 0x000132D2
		public override bool WillReadBody
		{
			get
			{
				return this.InnerBinding.WillReadBody;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x000150E0 File Offset: 0x000132E0
		public IEnumerable<ValueProviderFactory> ValueProviderFactories
		{
			get
			{
				IValueProviderParameterBinding valueProviderParameterBinding = this.InnerBinding as IValueProviderParameterBinding;
				if (valueProviderParameterBinding == null)
				{
					return Enumerable.Empty<ValueProviderFactory>();
				}
				return valueProviderParameterBinding.ValueProviderFactories;
			}
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x00015108 File Offset: 0x00013308
		public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext, CancellationToken cancellationToken)
		{
			return this.TraceWriter.TraceBeginEndAsync(actionContext.Request, TraceCategories.ModelBindingCategory, TraceLevel.Info, this.InnerBinding.GetType().Name, "ExecuteBindingAsync", delegate(TraceRecord tr)
			{
				tr.Message = Error.Format(SRResources.TraceBeginParameterBind, new object[] { this.InnerBinding.Descriptor.ParameterName });
			}, () => this.InnerBinding.ExecuteBindingAsync(metadataProvider, actionContext, cancellationToken), delegate(TraceRecord tr)
			{
				string parameterName = this.InnerBinding.Descriptor.ParameterName;
				if (!actionContext.ModelState.IsValid && actionContext.ModelState.ContainsKey(parameterName))
				{
					tr.Message = Error.Format(SRResources.TraceModelStateInvalidMessage, new object[] { FormattingUtilities.ModelStateToString(actionContext.ModelState) });
					return;
				}
				tr.Message = (actionContext.ActionArguments.ContainsKey(parameterName) ? Error.Format(SRResources.TraceEndParameterBind, new object[]
				{
					parameterName,
					FormattingUtilities.ValueToString(actionContext.ActionArguments[parameterName], CultureInfo.CurrentCulture)
				}) : Error.Format(SRResources.TraceEndParameterBindNoBind, new object[] { parameterName }));
			}, null);
		}

		// Token: 0x04000245 RID: 581
		private const string ExecuteBindingAsyncMethodName = "ExecuteBindingAsync";
	}
}
