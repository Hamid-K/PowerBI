using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData
{
	// Token: 0x020001BF RID: 447
	internal sealed class FeedWithoutExpectedTypeValidator
	{
		// Token: 0x06000D2D RID: 3373 RVA: 0x0002E8C8 File Offset: 0x0002CAC8
		internal FeedWithoutExpectedTypeValidator()
		{
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x0002E8D0 File Offset: 0x0002CAD0
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
				throw new ODataException(Strings.FeedWithoutExpectedTypeValidator_IncompatibleTypes(entityType.ODataFullName(), this.itemType.ODataFullName()));
			}
			this.itemType = (IEdmEntityType)commonBaseType;
		}

		// Token: 0x040004AB RID: 1195
		private IEdmEntityType itemType;
	}
}
