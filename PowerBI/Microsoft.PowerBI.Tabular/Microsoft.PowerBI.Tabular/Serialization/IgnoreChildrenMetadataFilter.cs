using System;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x0200016F RID: 367
	internal sealed class IgnoreChildrenMetadataFilter : IMetadataFilter
	{
		// Token: 0x06001765 RID: 5989 RVA: 0x000A2942 File Offset: 0x000A0B42
		public IgnoreChildrenMetadataFilter(IMetadataFilter baseFilter)
		{
			this.baseFilter = baseFilter;
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x000A2954 File Offset: 0x000A0B54
		public bool IgnoreProperty(ObjectType owner, string propertyName, MetadataPropertyNature propertyNature)
		{
			MetadataPropertyNature metadataPropertyNature = propertyNature & MetadataPropertyNature.PropertyCategoryMask;
			if (metadataPropertyNature != MetadataPropertyNature.ChildProperty)
			{
				if (metadataPropertyNature - MetadataPropertyNature.ChildCollection <= 1)
				{
					return true;
				}
			}
			else if ((propertyNature & MetadataPropertyNature.Translation) == MetadataPropertyNature.None && (propertyNature & MetadataPropertyNature.JsonString) == MetadataPropertyNature.None && string.Compare(propertyName, "source", StringComparison.InvariantCulture) != 0)
			{
				return true;
			}
			return this.baseFilter != null && this.baseFilter.IgnoreProperty(owner, propertyName, propertyNature);
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x000A29B1 File Offset: 0x000A0BB1
		public bool IgnoreChild(ObjectType owner, string propertyName, MetadataPropertyNature propertyNature, MetadataObject @object)
		{
			return true;
		}

		// Token: 0x0400044D RID: 1101
		private IMetadataFilter baseFilter;
	}
}
