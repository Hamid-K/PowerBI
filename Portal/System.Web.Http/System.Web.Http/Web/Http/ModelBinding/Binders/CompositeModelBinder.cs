using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Validation;

namespace System.Web.Http.ModelBinding.Binders
{
	// Token: 0x02000068 RID: 104
	public class CompositeModelBinder : IModelBinder
	{
		// Token: 0x060002D8 RID: 728 RVA: 0x00008263 File Offset: 0x00006463
		public CompositeModelBinder(IEnumerable<IModelBinder> binders)
			: this(binders.ToArray<IModelBinder>())
		{
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00008271 File Offset: 0x00006471
		public CompositeModelBinder(params IModelBinder[] binders)
		{
			this.Binders = binders;
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002DA RID: 730 RVA: 0x00008280 File Offset: 0x00006480
		// (set) Token: 0x060002DB RID: 731 RVA: 0x00008288 File Offset: 0x00006488
		private IModelBinder[] Binders { get; set; }

		// Token: 0x060002DC RID: 732 RVA: 0x00008294 File Offset: 0x00006494
		public virtual bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
		{
			ModelBindingContext modelBindingContext = CompositeModelBinder.CreateNewBindingContext(bindingContext, bindingContext.ModelName);
			bool flag = this.TryBind(actionContext, modelBindingContext);
			if (!flag && !string.IsNullOrEmpty(bindingContext.ModelName) && bindingContext.FallbackToEmptyPrefix)
			{
				modelBindingContext = CompositeModelBinder.CreateNewBindingContext(bindingContext, string.Empty);
				flag = this.TryBind(actionContext, modelBindingContext);
			}
			if (!flag)
			{
				return false;
			}
			if (!modelBindingContext.ModelMetadata.IsComplexType && string.IsNullOrEmpty(modelBindingContext.ModelName))
			{
				modelBindingContext.ValidationNode = new ModelValidationNode(modelBindingContext.ModelMetadata, bindingContext.ModelName);
			}
			modelBindingContext.ValidationNode.Validate(actionContext, null);
			bindingContext.Model = modelBindingContext.Model;
			return true;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00008334 File Offset: 0x00006534
		private bool TryBind(HttpActionContext actionContext, ModelBindingContext bindingContext)
		{
			return actionContext.Bind(bindingContext, this.Binders);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00008344 File Offset: 0x00006544
		private static ModelBindingContext CreateNewBindingContext(ModelBindingContext oldBindingContext, string modelName)
		{
			ModelBindingContext modelBindingContext = new ModelBindingContext
			{
				ModelMetadata = oldBindingContext.ModelMetadata,
				ModelName = modelName,
				ModelState = oldBindingContext.ModelState,
				ValueProvider = oldBindingContext.ValueProvider
			};
			if (modelName == oldBindingContext.ModelName)
			{
				modelBindingContext.ValidationNode = oldBindingContext.ValidationNode;
			}
			return modelBindingContext;
		}
	}
}
