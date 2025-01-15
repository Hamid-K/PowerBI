using System;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core
{
	// Token: 0x02000091 RID: 145
	internal sealed class FeedWithoutExpectedTypeValidator
	{
		// Token: 0x060005B3 RID: 1459 RVA: 0x00014C39 File Offset: 0x00012E39
		internal FeedWithoutExpectedTypeValidator()
		{
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x00014C44 File Offset: 0x00012E44
		internal void ValidateEntry(IEdmEntityType entityType)
		{
			if (this.itemType == null)
			{
				this.itemType = entityType;
			}
			if (this.itemType.IsEquivalentTo(entityType))
			{
				return;
			}
			IEdmType commonBaseType = this.itemType.GetCommonBaseType(entityType);
			if (commonBaseType == null)
			{
				throw new ODataException(Strings.FeedWithoutExpectedTypeValidator_IncompatibleTypes(entityType.FullTypeName(), this.itemType.FullTypeName()));
			}
			this.itemType = (IEdmEntityType)commonBaseType;
		}

		// Token: 0x0400026C RID: 620
		private IEdmEntityType itemType;
	}
}
