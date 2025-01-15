using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Properties;
using System.Web.Http.Services;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x02000126 RID: 294
	internal class HttpActionDescriptorTracer : HttpActionDescriptor, IDecorator<HttpActionDescriptor>
	{
		// Token: 0x060007C6 RID: 1990 RVA: 0x00013A0C File Offset: 0x00011C0C
		public HttpActionDescriptorTracer(HttpControllerContext controllerContext, HttpActionDescriptor innerDescriptor, ITraceWriter traceWriter)
			: base(controllerContext.ControllerDescriptor)
		{
			this._innerDescriptor = innerDescriptor;
			this._traceWriter = traceWriter;
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060007C7 RID: 1991 RVA: 0x00013A28 File Offset: 0x00011C28
		public HttpActionDescriptor Inner
		{
			get
			{
				return this._innerDescriptor;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x00013A30 File Offset: 0x00011C30
		public override ConcurrentDictionary<object, object> Properties
		{
			get
			{
				return this._innerDescriptor.Properties;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060007C9 RID: 1993 RVA: 0x00013A3D File Offset: 0x00011C3D
		// (set) Token: 0x060007CA RID: 1994 RVA: 0x00013A4A File Offset: 0x00011C4A
		public override HttpActionBinding ActionBinding
		{
			get
			{
				return this._innerDescriptor.ActionBinding;
			}
			set
			{
				this._innerDescriptor.ActionBinding = value;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060007CB RID: 1995 RVA: 0x00013A58 File Offset: 0x00011C58
		public override Collection<HttpMethod> SupportedHttpMethods
		{
			get
			{
				return this._innerDescriptor.SupportedHttpMethods;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x00013A65 File Offset: 0x00011C65
		public override string ActionName
		{
			get
			{
				return this._innerDescriptor.ActionName;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x00013A72 File Offset: 0x00011C72
		public override IActionResultConverter ResultConverter
		{
			get
			{
				return this._innerDescriptor.ResultConverter;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x00013A7F File Offset: 0x00011C7F
		public override Type ReturnType
		{
			get
			{
				return this._innerDescriptor.ReturnType;
			}
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x00013A8C File Offset: 0x00011C8C
		public override Task<object> ExecuteAsync(HttpControllerContext controllerContext, IDictionary<string, object> arguments, CancellationToken cancellationToken)
		{
			return this._traceWriter.TraceBeginEndAsync(controllerContext.Request, TraceCategories.ActionCategory, TraceLevel.Info, this._innerDescriptor.GetType().Name, "ExecuteAsync", delegate(TraceRecord tr)
			{
				tr.Message = Error.Format(SRResources.TraceInvokingAction, new object[] { FormattingUtilities.ActionInvokeToString(this.ActionName, arguments) });
			}, () => this._innerDescriptor.ExecuteAsync(controllerContext, arguments, cancellationToken), delegate(TraceRecord tr, object value)
			{
				tr.Message = Error.Format(SRResources.TraceActionReturnValue, new object[] { FormattingUtilities.ValueToString(value, CultureInfo.CurrentCulture) });
			}, null);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x00013B24 File Offset: 0x00011D24
		public override Collection<T> GetCustomAttributes<T>()
		{
			return this._innerDescriptor.GetCustomAttributes<T>();
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00013B31 File Offset: 0x00011D31
		public override Collection<T> GetCustomAttributes<T>(bool inherit)
		{
			return this._innerDescriptor.GetCustomAttributes<T>(inherit);
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00013B40 File Offset: 0x00011D40
		public override Collection<IFilter> GetFilters()
		{
			List<IFilter> list = new List<IFilter>(this._innerDescriptor.GetFilters());
			List<IFilter> list2 = new List<IFilter>(list.Count);
			for (int i = 0; i < list.Count; i++)
			{
				if (FilterTracer.IsFilterTracer(list[i]))
				{
					list2.Add(list[i]);
				}
				else
				{
					foreach (IFilter filter in FilterTracer.CreateFilterTracers(list[i], this._traceWriter))
					{
						list2.Add(filter);
					}
				}
			}
			return new Collection<IFilter>(list2);
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00013BEC File Offset: 0x00011DEC
		public override Collection<FilterInfo> GetFilterPipeline()
		{
			List<FilterInfo> list = new List<FilterInfo>(this._innerDescriptor.GetFilterPipeline());
			List<FilterInfo> list2 = new List<FilterInfo>(list.Count);
			for (int i = 0; i < list.Count; i++)
			{
				if (FilterTracer.IsFilterTracer(list[i].Instance))
				{
					list2.Add(list[i]);
				}
				else
				{
					foreach (FilterInfo filterInfo in FilterTracer.CreateFilterTracers(list[i], this._traceWriter))
					{
						list2.Add(filterInfo);
					}
				}
			}
			return new Collection<FilterInfo>(list2);
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00013CA0 File Offset: 0x00011EA0
		public override Collection<HttpParameterDescriptor> GetParameters()
		{
			return this._innerDescriptor.GetParameters();
		}

		// Token: 0x04000215 RID: 533
		private const string ExecuteMethodName = "ExecuteAsync";

		// Token: 0x04000216 RID: 534
		private readonly HttpActionDescriptor _innerDescriptor;

		// Token: 0x04000217 RID: 535
		private readonly ITraceWriter _traceWriter;
	}
}
