using System;
using System.Web.Http.Controllers;

namespace System.Web.Http.ModelBinding.Binders
{
	// Token: 0x02000077 RID: 119
	public sealed class SimpleModelBinderProvider : ModelBinderProvider
	{
		// Token: 0x0600031C RID: 796 RVA: 0x0000900C File Offset: 0x0000720C
		public SimpleModelBinderProvider(Type modelType, IModelBinder modelBinder)
		{
			if (modelType == null)
			{
				throw Error.ArgumentNull("modelType");
			}
			if (modelBinder == null)
			{
				throw Error.ArgumentNull("modelBinder");
			}
			this._modelType = modelType;
			this._modelBinderFactory = () => modelBinder;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000906C File Offset: 0x0000726C
		public SimpleModelBinderProvider(Type modelType, Func<IModelBinder> modelBinderFactory)
		{
			if (modelType == null)
			{
				throw Error.ArgumentNull("modelType");
			}
			if (modelBinderFactory == null)
			{
				throw Error.ArgumentNull("modelBinderFactory");
			}
			this._modelType = modelType;
			this._modelBinderFactory = modelBinderFactory;
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600031E RID: 798 RVA: 0x000090A4 File Offset: 0x000072A4
		public Type ModelType
		{
			get
			{
				return this._modelType;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600031F RID: 799 RVA: 0x000090AC File Offset: 0x000072AC
		// (set) Token: 0x06000320 RID: 800 RVA: 0x000090B4 File Offset: 0x000072B4
		public bool SuppressPrefixCheck { get; set; }

		// Token: 0x06000321 RID: 801 RVA: 0x000090BD File Offset: 0x000072BD
		public override IModelBinder GetBinder(HttpConfiguration configuration, Type modelType)
		{
			if (modelType == null)
			{
				throw Error.ArgumentNull("modelType");
			}
			if (!(modelType == this.ModelType))
			{
				return null;
			}
			if (this.SuppressPrefixCheck)
			{
				return this._modelBinderFactory();
			}
			return new SimpleModelBinderProvider.SimpleModelBinder(this);
		}

		// Token: 0x040000AA RID: 170
		private readonly Func<IModelBinder> _modelBinderFactory;

		// Token: 0x040000AB RID: 171
		private readonly Type _modelType;

		// Token: 0x020001B6 RID: 438
		private class SimpleModelBinder : IModelBinder
		{
			// Token: 0x06000AC2 RID: 2754 RVA: 0x0001BB5C File Offset: 0x00019D5C
			public SimpleModelBinder(SimpleModelBinderProvider parent)
			{
				this._parent = parent;
			}

			// Token: 0x06000AC3 RID: 2755 RVA: 0x0001BB6B File Offset: 0x00019D6B
			public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
			{
				return bindingContext.ValueProvider.ContainsPrefix(bindingContext.ModelName) && this._parent._modelBinderFactory().BindModel(actionContext, bindingContext);
			}

			// Token: 0x04000340 RID: 832
			private readonly SimpleModelBinderProvider _parent;
		}
	}
}
