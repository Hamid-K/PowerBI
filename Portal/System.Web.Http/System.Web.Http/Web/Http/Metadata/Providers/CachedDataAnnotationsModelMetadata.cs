using System;
using System.Collections.Generic;

namespace System.Web.Http.Metadata.Providers
{
	// Token: 0x0200004C RID: 76
	public class CachedDataAnnotationsModelMetadata : CachedModelMetadata<CachedDataAnnotationsMetadataAttributes>
	{
		// Token: 0x0600021D RID: 541 RVA: 0x0000688C File Offset: 0x00004A8C
		public CachedDataAnnotationsModelMetadata(CachedDataAnnotationsModelMetadata prototype, Func<object> modelAccessor)
			: base(prototype, modelAccessor)
		{
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00006896 File Offset: 0x00004A96
		public CachedDataAnnotationsModelMetadata(DataAnnotationsModelMetadataProvider provider, Type containerType, Type modelType, string propertyName, IEnumerable<Attribute> attributes)
			: base(provider, containerType, modelType, propertyName, new CachedDataAnnotationsMetadataAttributes(attributes))
		{
		}

		// Token: 0x0600021F RID: 543 RVA: 0x000068AA File Offset: 0x00004AAA
		protected override bool ComputeConvertEmptyStringToNull()
		{
			if (base.PrototypeCache.DisplayFormat == null)
			{
				return base.ComputeConvertEmptyStringToNull();
			}
			return base.PrototypeCache.DisplayFormat.ConvertEmptyStringToNull;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x000068D0 File Offset: 0x00004AD0
		protected override string ComputeDescription()
		{
			if (base.PrototypeCache.Display == null)
			{
				return base.ComputeDescription();
			}
			return base.PrototypeCache.Display.GetDescription();
		}

		// Token: 0x06000221 RID: 545 RVA: 0x000068F8 File Offset: 0x00004AF8
		protected override bool ComputeIsReadOnly()
		{
			if (base.PrototypeCache.Editable != null)
			{
				return !base.PrototypeCache.Editable.AllowEdit;
			}
			if (base.PrototypeCache.ReadOnly != null)
			{
				return base.PrototypeCache.ReadOnly.IsReadOnly;
			}
			return base.ComputeIsReadOnly();
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000694C File Offset: 0x00004B4C
		public override string GetDisplayName()
		{
			if (base.PrototypeCache.Display != null)
			{
				string name = base.PrototypeCache.Display.GetName();
				if (name != null)
				{
					return name;
				}
			}
			if (base.PrototypeCache.DisplayName != null)
			{
				string displayName = base.PrototypeCache.DisplayName.DisplayName;
				if (displayName != null)
				{
					return displayName;
				}
			}
			return base.GetDisplayName();
		}
	}
}
