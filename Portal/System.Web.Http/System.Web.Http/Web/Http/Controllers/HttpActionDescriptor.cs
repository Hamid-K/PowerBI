using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using System.Web.Http.Internal;
using System.Web.Http.Properties;

namespace System.Web.Http.Controllers
{
	// Token: 0x020000FE RID: 254
	public abstract class HttpActionDescriptor
	{
		// Token: 0x0600068A RID: 1674 RVA: 0x000106C1 File Offset: 0x0000E8C1
		protected HttpActionDescriptor()
		{
			this._filterPipeline = new Lazy<Collection<FilterInfo>>(new Func<Collection<FilterInfo>>(this.InitializeFilterPipeline));
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x000106F6 File Offset: 0x0000E8F6
		protected HttpActionDescriptor(HttpControllerDescriptor controllerDescriptor)
			: this()
		{
			if (controllerDescriptor == null)
			{
				throw Error.ArgumentNull("controllerDescriptor");
			}
			this._controllerDescriptor = controllerDescriptor;
			this._configuration = this._controllerDescriptor.Configuration;
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x0600068C RID: 1676
		public abstract string ActionName { get; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x00010724 File Offset: 0x0000E924
		// (set) Token: 0x0600068E RID: 1678 RVA: 0x0001072C File Offset: 0x0000E92C
		public HttpConfiguration Configuration
		{
			get
			{
				return this._configuration;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._configuration = value;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x00010740 File Offset: 0x0000E940
		// (set) Token: 0x06000690 RID: 1680 RVA: 0x0001077E File Offset: 0x0000E97E
		public virtual HttpActionBinding ActionBinding
		{
			get
			{
				if (this._actionBinding == null)
				{
					HttpActionBinding binding = this._controllerDescriptor.Configuration.Services.GetActionValueBinder().GetBinding(this);
					this._actionBinding = binding;
				}
				return this._actionBinding;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._actionBinding = value;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x00010790 File Offset: 0x0000E990
		// (set) Token: 0x06000692 RID: 1682 RVA: 0x00010798 File Offset: 0x0000E998
		public HttpControllerDescriptor ControllerDescriptor
		{
			get
			{
				return this._controllerDescriptor;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._controllerDescriptor = value;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000693 RID: 1683
		public abstract Type ReturnType { get; }

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x000107AA File Offset: 0x0000E9AA
		public virtual IActionResultConverter ResultConverter
		{
			get
			{
				if (this._converter == null)
				{
					this._converter = HttpActionDescriptor.GetResultConverter(this.ReturnType);
				}
				return this._converter;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x000107CB File Offset: 0x0000E9CB
		public virtual Collection<HttpMethod> SupportedHttpMethods
		{
			get
			{
				return this._supportedHttpMethods;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x000107D3 File Offset: 0x0000E9D3
		public virtual ConcurrentDictionary<object, object> Properties
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x000107DB File Offset: 0x0000E9DB
		public virtual Collection<T> GetCustomAttributes<T>() where T : class
		{
			return this.GetCustomAttributes<T>(true);
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x000107E4 File Offset: 0x0000E9E4
		public virtual Collection<T> GetCustomAttributes<T>(bool inherit) where T : class
		{
			return new Collection<T>();
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x000107EB File Offset: 0x0000E9EB
		public virtual Collection<IFilter> GetFilters()
		{
			return new Collection<IFilter>();
		}

		// Token: 0x0600069A RID: 1690
		public abstract Collection<HttpParameterDescriptor> GetParameters();

		// Token: 0x0600069B RID: 1691 RVA: 0x000107F4 File Offset: 0x0000E9F4
		internal static IActionResultConverter GetResultConverter(Type type)
		{
			if (type != null && type.IsGenericParameter)
			{
				throw Error.InvalidOperation(SRResources.HttpActionDescriptor_NoConverterForGenericParamterTypeExists, new object[] { type });
			}
			if (type == null)
			{
				return HttpActionDescriptor._voidResultConverter;
			}
			if (typeof(HttpResponseMessage).IsAssignableFrom(type))
			{
				return HttpActionDescriptor._responseMessageResultConverter;
			}
			if (typeof(IHttpActionResult).IsAssignableFrom(type))
			{
				return null;
			}
			return TypeActivator.Create<IActionResultConverter>(typeof(ValueResultConverter<>).MakeGenericType(new Type[] { type }))();
		}

		// Token: 0x0600069C RID: 1692
		public abstract Task<object> ExecuteAsync(HttpControllerContext controllerContext, IDictionary<string, object> arguments, CancellationToken cancellationToken);

		// Token: 0x0600069D RID: 1693 RVA: 0x00010885 File Offset: 0x0000EA85
		public virtual Collection<FilterInfo> GetFilterPipeline()
		{
			return this._filterPipeline.Value;
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x00010894 File Offset: 0x0000EA94
		internal FilterGrouping GetFilterGrouping()
		{
			Collection<FilterInfo> filterPipeline = this.GetFilterPipeline();
			if (this._filterGrouping == null || this._filterPipelineForGrouping != filterPipeline)
			{
				this._filterGrouping = new FilterGrouping(filterPipeline);
				this._filterPipelineForGrouping = filterPipeline;
			}
			return this._filterGrouping;
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x000108D4 File Offset: 0x0000EAD4
		private Collection<FilterInfo> InitializeFilterPipeline()
		{
			return new Collection<FilterInfo>(HttpActionDescriptor.RemoveDuplicates(this._configuration.Services.GetFilterProviders().SelectMany((IFilterProvider fp) => fp.GetFilters(this._configuration, this)).OrderBy((FilterInfo f) => f, FilterInfoComparer.Instance)
				.Reverse<FilterInfo>()).Reverse<FilterInfo>().ToList<FilterInfo>());
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x00010944 File Offset: 0x0000EB44
		private static IEnumerable<FilterInfo> RemoveDuplicates(IEnumerable<FilterInfo> filters)
		{
			HashSet<Type> visitedTypes = new HashSet<Type>();
			foreach (FilterInfo filterInfo in filters)
			{
				object instance = filterInfo.Instance;
				Type filterInstanceType = instance.GetType();
				if (!visitedTypes.Contains(filterInstanceType) || HttpActionDescriptor.AllowMultiple(instance))
				{
					yield return filterInfo;
					visitedTypes.Add(filterInstanceType);
				}
				filterInstanceType = null;
			}
			IEnumerator<FilterInfo> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x00010954 File Offset: 0x0000EB54
		private static bool AllowMultiple(object filterInstance)
		{
			IFilter filter = filterInstance as IFilter;
			return filter == null || filter.AllowMultiple;
		}

		// Token: 0x040001A5 RID: 421
		private readonly ConcurrentDictionary<object, object> _properties = new ConcurrentDictionary<object, object>();

		// Token: 0x040001A6 RID: 422
		private IActionResultConverter _converter;

		// Token: 0x040001A7 RID: 423
		private readonly Lazy<Collection<FilterInfo>> _filterPipeline;

		// Token: 0x040001A8 RID: 424
		private FilterGrouping _filterGrouping;

		// Token: 0x040001A9 RID: 425
		private Collection<FilterInfo> _filterPipelineForGrouping;

		// Token: 0x040001AA RID: 426
		private HttpConfiguration _configuration;

		// Token: 0x040001AB RID: 427
		private HttpControllerDescriptor _controllerDescriptor;

		// Token: 0x040001AC RID: 428
		private readonly Collection<HttpMethod> _supportedHttpMethods = new Collection<HttpMethod>();

		// Token: 0x040001AD RID: 429
		private HttpActionBinding _actionBinding;

		// Token: 0x040001AE RID: 430
		private static readonly ResponseMessageResultConverter _responseMessageResultConverter = new ResponseMessageResultConverter();

		// Token: 0x040001AF RID: 431
		private static readonly VoidResultConverter _voidResultConverter = new VoidResultConverter();
	}
}
