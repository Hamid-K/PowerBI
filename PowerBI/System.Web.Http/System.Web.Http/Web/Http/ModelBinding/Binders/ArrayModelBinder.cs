using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Controllers;

namespace System.Web.Http.ModelBinding.Binders
{
	// Token: 0x0200006A RID: 106
	public class ArrayModelBinder<TElement> : CollectionModelBinder<TElement>
	{
		// Token: 0x060002E1 RID: 737 RVA: 0x000083ED File Offset: 0x000065ED
		public override bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
		{
			return !bindingContext.ModelMetadata.IsReadOnly && base.BindModel(actionContext, bindingContext);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00008406 File Offset: 0x00006606
		protected override bool CreateOrReplaceCollection(HttpActionContext actionContext, ModelBindingContext bindingContext, IList<TElement> newCollection)
		{
			bindingContext.Model = newCollection.ToArray<TElement>();
			return true;
		}
	}
}
