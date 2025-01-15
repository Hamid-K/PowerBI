using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Metadata
{
	// Token: 0x020001EC RID: 492
	public class UnqualifiedODataUriResolver : ODataUriResolver
	{
		// Token: 0x060011E8 RID: 4584 RVA: 0x00040E54 File Offset: 0x0003F054
		public override IEnumerable<IEdmOperation> ResolveUnboundOperations(IEdmModel model, string identifier)
		{
			if (identifier.Contains("."))
			{
				return base.ResolveUnboundOperations(model, identifier);
			}
			return Enumerable.Where<IEdmOperation>(Enumerable.OfType<IEdmOperation>(model.SchemaElements), (IEdmOperation operation) => string.Equals(identifier, operation.Name, this.EnableCaseInsensitive ? 5 : 4) && !operation.IsBound);
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x00040F10 File Offset: 0x0003F110
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
