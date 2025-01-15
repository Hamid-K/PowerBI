using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001AA RID: 426
	public class UnqualifiedODataUriResolver : ODataUriResolver
	{
		// Token: 0x0600111F RID: 4383 RVA: 0x0003036C File Offset: 0x0002E56C
		public override IEnumerable<IEdmOperation> ResolveUnboundOperations(IEdmModel model, string identifier)
		{
			if (identifier.Contains("."))
			{
				return base.ResolveUnboundOperations(model, identifier);
			}
			return Enumerable.Where<IEdmOperation>(Enumerable.OfType<IEdmOperation>(model.SchemaElements), (IEdmOperation operation) => string.Equals(identifier, operation.Name, this.EnableCaseInsensitive ? 5 : 4) && !operation.IsBound);
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x000303CC File Offset: 0x0002E5CC
		public override IEnumerable<IEdmOperation> ResolveBoundOperations(IEdmModel model, string identifier, IEdmType bindingType)
		{
			if (identifier.Contains("."))
			{
				return base.ResolveBoundOperations(model, identifier, bindingType);
			}
			return Enumerable.Where<IEdmOperation>(Enumerable.OfType<IEdmOperation>(model.SchemaElements), (IEdmOperation operation) => string.Equals(identifier, operation.Name, this.EnableCaseInsensitive ? 5 : 4) && operation.IsBound && Enumerable.Any<IEdmOperationParameter>(operation.Parameters) && operation.HasEquivalentBindingType(bindingType));
		}
	}
}
