using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Http.Internal;
using System.Web.Http.ModelBinding;
using System.Web.Http.Properties;

namespace System.Web.Http.Controllers
{
	// Token: 0x0200010A RID: 266
	public abstract class HttpParameterDescriptor
	{
		// Token: 0x060006F5 RID: 1781 RVA: 0x0001169C File Offset: 0x0000F89C
		protected HttpParameterDescriptor()
		{
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x000116AF File Offset: 0x0000F8AF
		protected HttpParameterDescriptor(HttpActionDescriptor actionDescriptor)
		{
			if (actionDescriptor == null)
			{
				throw Error.ArgumentNull("actionDescriptor");
			}
			this._actionDescriptor = actionDescriptor;
			this._configuration = this._actionDescriptor.Configuration;
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x000116E8 File Offset: 0x0000F8E8
		// (set) Token: 0x060006F8 RID: 1784 RVA: 0x000116F0 File Offset: 0x0000F8F0
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

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x00011702 File Offset: 0x0000F902
		// (set) Token: 0x060006FA RID: 1786 RVA: 0x0001170A File Offset: 0x0000F90A
		public HttpActionDescriptor ActionDescriptor
		{
			get
			{
				return this._actionDescriptor;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._actionDescriptor = value;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x0001171C File Offset: 0x0000F91C
		public ConcurrentDictionary<object, object> Properties
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x0000413B File Offset: 0x0000233B
		public virtual object DefaultValue
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060006FD RID: 1789
		public abstract string ParameterName { get; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060006FE RID: 1790
		public abstract Type ParameterType { get; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x00011724 File Offset: 0x0000F924
		public virtual string Prefix
		{
			get
			{
				ModelBinderAttribute modelBinderAttribute = this.ParameterBinderAttribute as ModelBinderAttribute;
				if (modelBinderAttribute == null)
				{
					return null;
				}
				return modelBinderAttribute.Name;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000700 RID: 1792 RVA: 0x00003B5D File Offset: 0x00001D5D
		public virtual bool IsOptional
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x00011748 File Offset: 0x0000F948
		// (set) Token: 0x06000702 RID: 1794 RVA: 0x00011773 File Offset: 0x0000F973
		public virtual ParameterBindingAttribute ParameterBinderAttribute
		{
			get
			{
				if (this._parameterBindingAttribute == null && !this._searchedModelBinderAttribute)
				{
					this._searchedModelBinderAttribute = true;
					this._parameterBindingAttribute = this.FindParameterBindingAttribute();
				}
				return this._parameterBindingAttribute;
			}
			set
			{
				this._parameterBindingAttribute = value;
			}
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0001177C File Offset: 0x0000F97C
		public virtual Collection<T> GetCustomAttributes<T>() where T : class
		{
			return new Collection<T>();
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00011783 File Offset: 0x0000F983
		private ParameterBindingAttribute FindParameterBindingAttribute()
		{
			return HttpParameterDescriptor.ChooseAttribute(this.GetCustomAttributes<ParameterBindingAttribute>()) ?? HttpParameterDescriptor.ChooseAttribute(this.ParameterType.GetCustomAttributes(false));
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x000117A5 File Offset: 0x0000F9A5
		private static ParameterBindingAttribute ChooseAttribute(IList<ParameterBindingAttribute> list)
		{
			if (list.Count == 0)
			{
				return null;
			}
			if (list.Count > 1)
			{
				return new HttpParameterDescriptor.AmbiguousParameterBindingAttribute();
			}
			return list[0];
		}

		// Token: 0x040001C5 RID: 453
		private readonly ConcurrentDictionary<object, object> _properties = new ConcurrentDictionary<object, object>();

		// Token: 0x040001C6 RID: 454
		private ParameterBindingAttribute _parameterBindingAttribute;

		// Token: 0x040001C7 RID: 455
		private bool _searchedModelBinderAttribute;

		// Token: 0x040001C8 RID: 456
		private HttpConfiguration _configuration;

		// Token: 0x040001C9 RID: 457
		private HttpActionDescriptor _actionDescriptor;

		// Token: 0x02000203 RID: 515
		private sealed class AmbiguousParameterBindingAttribute : ParameterBindingAttribute
		{
			// Token: 0x06000BEA RID: 3050 RVA: 0x0001F920 File Offset: 0x0001DB20
			public override HttpParameterBinding GetBinding(HttpParameterDescriptor parameter)
			{
				string text = Error.Format(SRResources.ParameterBindingConflictingAttributes, new object[] { parameter.ParameterName });
				return parameter.BindAsError(text);
			}
		}
	}
}
