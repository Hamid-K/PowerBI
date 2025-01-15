using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Metadata;
using System.Web.Http.ValueProviders;
using System.Web.Http.ValueProviders.Providers;

namespace System.Web.Http.ModelBinding
{
	// Token: 0x02000056 RID: 86
	public class ModelBinderParameterBinding : HttpParameterBinding, IValueProviderParameterBinding
	{
		// Token: 0x06000253 RID: 595 RVA: 0x0000726B File Offset: 0x0000546B
		public ModelBinderParameterBinding(HttpParameterDescriptor descriptor, IModelBinder modelBinder, IEnumerable<ValueProviderFactory> valueProviderFactories)
			: base(descriptor)
		{
			if (modelBinder == null)
			{
				throw Error.ArgumentNull("modelBinder");
			}
			if (valueProviderFactories == null)
			{
				throw Error.ArgumentNull("valueProviderFactories");
			}
			this._binder = modelBinder;
			this._valueProviderFactories = valueProviderFactories.ToArray<ValueProviderFactory>();
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000254 RID: 596 RVA: 0x000072A3 File Offset: 0x000054A3
		public IEnumerable<ValueProviderFactory> ValueProviderFactories
		{
			get
			{
				return this._valueProviderFactories;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000255 RID: 597 RVA: 0x000072AB File Offset: 0x000054AB
		public IModelBinder Binder
		{
			get
			{
				return this._binder;
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x000072B4 File Offset: 0x000054B4
		public override Task ExecuteBindingAsync(ModelMetadataProvider metadataProvider, HttpActionContext actionContext, CancellationToken cancellationToken)
		{
			ModelBindingContext modelBindingContext = this.GetModelBindingContext(metadataProvider, actionContext);
			object obj = (this._binder.BindModel(actionContext, modelBindingContext) ? modelBindingContext.Model : base.Descriptor.DefaultValue);
			base.SetValue(actionContext, obj);
			return TaskHelpers.Completed();
		}

		// Token: 0x06000257 RID: 599 RVA: 0x000072FC File Offset: 0x000054FC
		private ModelBindingContext GetModelBindingContext(ModelMetadataProvider metadataProvider, HttpActionContext actionContext)
		{
			string parameterName = base.Descriptor.ParameterName;
			Type parameterType = base.Descriptor.ParameterType;
			string prefix = base.Descriptor.Prefix;
			IValueProvider valueProvider = CompositeValueProviderFactory.GetValueProvider(actionContext, this._valueProviderFactories);
			return new ModelBindingContext
			{
				ModelName = (prefix ?? parameterName),
				FallbackToEmptyPrefix = (prefix == null),
				ModelMetadata = metadataProvider.GetMetadataForType(null, parameterType),
				ModelState = actionContext.ModelState,
				ValueProvider = valueProvider
			};
		}

		// Token: 0x04000084 RID: 132
		private readonly ValueProviderFactory[] _valueProviderFactories;

		// Token: 0x04000085 RID: 133
		private readonly IModelBinder _binder;
	}
}
