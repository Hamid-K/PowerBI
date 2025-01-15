using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000148 RID: 328
	public class UnqualifiedODataUriResolver : ODataUriResolver
	{
		// Token: 0x060010EA RID: 4330 RVA: 0x0002FA2C File Offset: 0x0002DC2C
		public override IEnumerable<IEdmOperation> ResolveUnboundOperations(IEdmModel model, string identifier)
		{
			if (identifier.Contains("."))
			{
				return base.ResolveUnboundOperations(model, identifier);
			}
			return from operation in UnqualifiedODataUriResolver.FindAcrossModels<IEdmOperation>(model, identifier, this.EnableCaseInsensitive)
				where !operation.IsBound
				select operation;
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x0002FA80 File Offset: 0x0002DC80
		public override IEnumerable<IEdmOperation> ResolveBoundOperations(IEdmModel model, string identifier, IEdmType bindingType)
		{
			if (identifier.Contains("."))
			{
				return base.ResolveBoundOperations(model, identifier, bindingType);
			}
			return from operation in UnqualifiedODataUriResolver.FindAcrossModels<IEdmOperation>(model, identifier, this.EnableCaseInsensitive)
				where operation.IsBound && operation.Parameters.Any<IEdmOperationParameter>() && operation.HasEquivalentBindingType(bindingType)
				select operation;
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x0002FAD4 File Offset: 0x0002DCD4
		private static IEnumerable<T> FindAcrossModels<T>(IEdmModel model, string qualifiedName, bool caseInsensitive) where T : IEdmSchemaElement
		{
			Func<T, bool> <>9__1;
			Func<IEdmModel, IEnumerable<T>> func = delegate(IEdmModel refModel)
			{
				IEnumerable<T> enumerable2 = refModel.SchemaElements.OfType<T>();
				Func<T, bool> func2;
				if ((func2 = <>9__1) == null)
				{
					func2 = (<>9__1 = (T e) => string.Equals(qualifiedName, e.Name, caseInsensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal));
				}
				return enumerable2.Where(func2);
			};
			IEnumerable<T> enumerable = func(model);
			foreach (IEdmModel edmModel in model.ReferencedModels)
			{
				enumerable.Concat(func(edmModel));
			}
			return enumerable;
		}
	}
}
