using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000252 RID: 594
	internal abstract class SystemResourcePackageContentRequiredValidator : ISystemResourcePackageContentValidator
	{
		// Token: 0x060015BD RID: 5565 RVA: 0x000563DC File Offset: 0x000545DC
		internal SystemResourcePackageContentRequiredValidator(IEnumerable<SystemResourcePackageContentValidationItem> requiredContents)
		{
			this._requiredContents = requiredContents;
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x000563EC File Offset: 0x000545EC
		public bool Validate(IEnumerable<SystemResourceContentItem> contentItems)
		{
			using (IEnumerator<SystemResourcePackageContentValidationItem> enumerator = this._requiredContents.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					SystemResourcePackageContentValidationItem item = enumerator.Current;
					SystemResourceContentItem systemResourceContentItem = contentItems.SingleOrDefault((SystemResourceContentItem x) => string.Equals(x.Key, item.Key, StringComparison.InvariantCultureIgnoreCase));
					if (systemResourceContentItem == null)
					{
						throw new SystemResourcePackageRequiredItemMissingException(item.Key);
					}
					if (!string.IsNullOrEmpty(systemResourceContentItem.ContentType) && !string.Equals(systemResourceContentItem.ContentType, item.ContentType, StringComparison.InvariantCultureIgnoreCase))
					{
						throw new SystemResourcePackageItemContentTypeMismatchException(item.Key, item.ContentType);
					}
					if (!string.Equals(Path.GetExtension(systemResourceContentItem.Path), item.Extension, StringComparison.InvariantCultureIgnoreCase))
					{
						throw new SystemResourcePackageItemExtensionMismatchException(item.Key, item.Extension);
					}
				}
			}
			return true;
		}

		// Token: 0x040007F6 RID: 2038
		private readonly IEnumerable<SystemResourcePackageContentValidationItem> _requiredContents;
	}
}
