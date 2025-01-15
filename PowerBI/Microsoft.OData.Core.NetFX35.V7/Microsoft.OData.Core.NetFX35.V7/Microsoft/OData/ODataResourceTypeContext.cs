using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x02000063 RID: 99
	internal class ODataResourceTypeContext : IODataResourceTypeContext
	{
		// Token: 0x06000327 RID: 807 RVA: 0x0000A228 File Offset: 0x00008428
		private ODataResourceTypeContext(bool throwIfMissingTypeInfo)
		{
			this.throwIfMissingTypeInfo = throwIfMissingTypeInfo;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000A237 File Offset: 0x00008437
		private ODataResourceTypeContext(IEdmStructuredType expectedResourceType, bool throwIfMissingTypeInfo)
		{
			this.expectedResourceType = expectedResourceType;
			this.throwIfMissingTypeInfo = throwIfMissingTypeInfo;
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000329 RID: 809 RVA: 0x0000A24D File Offset: 0x0000844D
		public virtual string NavigationSourceName
		{
			get
			{
				return this.ValidateAndReturn<string>(null);
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000A24D File Offset: 0x0000844D
		public virtual string NavigationSourceEntityTypeName
		{
			get
			{
				return this.ValidateAndReturn<string>(null);
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600032B RID: 811 RVA: 0x0000A24D File Offset: 0x0000844D
		public virtual string NavigationSourceFullTypeName
		{
			get
			{
				return this.ValidateAndReturn<string>(null);
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600032C RID: 812 RVA: 0x00002500 File Offset: 0x00000700
		public virtual EdmNavigationSourceKind NavigationSourceKind
		{
			get
			{
				return EdmNavigationSourceKind.None;
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000A256 File Offset: 0x00008456
		public virtual string ExpectedResourceTypeName
		{
			get
			{
				if (this.expectedResourceTypeName == null)
				{
					this.expectedResourceTypeName = ((this.expectedResourceType == null) ? null : this.expectedResourceType.FullTypeName());
				}
				return this.expectedResourceTypeName;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000A282 File Offset: 0x00008482
		public virtual IEdmStructuredType ExpectedResourceType
		{
			get
			{
				return this.expectedResourceType;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600032F RID: 815 RVA: 0x00002500 File Offset: 0x00000700
		public virtual bool IsFromCollection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000330 RID: 816 RVA: 0x00002500 File Offset: 0x00000700
		public virtual bool IsMediaLinkEntry
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000A28A File Offset: 0x0000848A
		internal static ODataResourceTypeContext Create(ODataResourceSerializationInfo serializationInfo, IEdmNavigationSource navigationSource, IEdmEntityType navigationSourceEntityType, IEdmStructuredType expectedResourceType, bool throwIfMissingTypeInfo)
		{
			if (serializationInfo != null)
			{
				return new ODataResourceTypeContext.ODataResourceTypeContextWithoutModel(serializationInfo);
			}
			if (expectedResourceType != null && expectedResourceType.IsODataComplexTypeKind())
			{
				return new ODataResourceTypeContext.ODataResourceTypeContextWithModel(null, null, expectedResourceType);
			}
			if (navigationSource != null && expectedResourceType != null)
			{
				return new ODataResourceTypeContext.ODataResourceTypeContextWithModel(navigationSource, navigationSourceEntityType, expectedResourceType);
			}
			return new ODataResourceTypeContext(expectedResourceType, throwIfMissingTypeInfo);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000A2C1 File Offset: 0x000084C1
		private T ValidateAndReturn<T>(T value) where T : class
		{
			if (this.throwIfMissingTypeInfo && value == null)
			{
				throw new ODataException(Strings.ODataResourceTypeContext_MetadataOrSerializationInfoMissing);
			}
			return value;
		}

		// Token: 0x040001B8 RID: 440
		protected IEdmStructuredType expectedResourceType;

		// Token: 0x040001B9 RID: 441
		protected string expectedResourceTypeName;

		// Token: 0x040001BA RID: 442
		private readonly bool throwIfMissingTypeInfo;

		// Token: 0x0200025F RID: 607
		internal sealed class ODataResourceTypeContextWithoutModel : ODataResourceTypeContext
		{
			// Token: 0x0600177D RID: 6013 RVA: 0x0004758F File Offset: 0x0004578F
			internal ODataResourceTypeContextWithoutModel(ODataResourceSerializationInfo serializationInfo)
				: base(false)
			{
				this.serializationInfo = serializationInfo;
			}

			// Token: 0x17000544 RID: 1348
			// (get) Token: 0x0600177E RID: 6014 RVA: 0x0004759F File Offset: 0x0004579F
			public override string NavigationSourceName
			{
				get
				{
					return this.serializationInfo.NavigationSourceName;
				}
			}

			// Token: 0x17000545 RID: 1349
			// (get) Token: 0x0600177F RID: 6015 RVA: 0x000475AC File Offset: 0x000457AC
			public override string NavigationSourceEntityTypeName
			{
				get
				{
					return this.serializationInfo.NavigationSourceEntityTypeName;
				}
			}

			// Token: 0x17000546 RID: 1350
			// (get) Token: 0x06001780 RID: 6016 RVA: 0x000475B9 File Offset: 0x000457B9
			public override string NavigationSourceFullTypeName
			{
				get
				{
					if (this.IsFromCollection)
					{
						return EdmLibraryExtensions.GetCollectionTypeName(this.serializationInfo.NavigationSourceEntityTypeName);
					}
					return this.serializationInfo.NavigationSourceEntityTypeName;
				}
			}

			// Token: 0x17000547 RID: 1351
			// (get) Token: 0x06001781 RID: 6017 RVA: 0x000475DF File Offset: 0x000457DF
			public override EdmNavigationSourceKind NavigationSourceKind
			{
				get
				{
					return this.serializationInfo.NavigationSourceKind;
				}
			}

			// Token: 0x17000548 RID: 1352
			// (get) Token: 0x06001782 RID: 6018 RVA: 0x000475EC File Offset: 0x000457EC
			public override string ExpectedResourceTypeName
			{
				get
				{
					return this.serializationInfo.ExpectedTypeName;
				}
			}

			// Token: 0x17000549 RID: 1353
			// (get) Token: 0x06001783 RID: 6019 RVA: 0x0000B41B File Offset: 0x0000961B
			public override IEdmStructuredType ExpectedResourceType
			{
				get
				{
					return null;
				}
			}

			// Token: 0x1700054A RID: 1354
			// (get) Token: 0x06001784 RID: 6020 RVA: 0x00002500 File Offset: 0x00000700
			public override bool IsMediaLinkEntry
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700054B RID: 1355
			// (get) Token: 0x06001785 RID: 6021 RVA: 0x000475F9 File Offset: 0x000457F9
			public override bool IsFromCollection
			{
				get
				{
					return this.serializationInfo.IsFromCollection;
				}
			}

			// Token: 0x04000AFB RID: 2811
			private readonly ODataResourceSerializationInfo serializationInfo;
		}

		// Token: 0x02000260 RID: 608
		internal sealed class ODataResourceTypeContextWithModel : ODataResourceTypeContext
		{
			// Token: 0x06001786 RID: 6022 RVA: 0x00047608 File Offset: 0x00045808
			internal ODataResourceTypeContextWithModel(IEdmNavigationSource navigationSource, IEdmEntityType navigationSourceEntityType, IEdmStructuredType expectedResourceType)
				: base(expectedResourceType, false)
			{
				this.navigationSource = navigationSource;
				this.navigationSourceEntityType = navigationSourceEntityType;
				IEdmContainedEntitySet edmContainedEntitySet = navigationSource as IEdmContainedEntitySet;
				if (edmContainedEntitySet != null && edmContainedEntitySet.NavigationProperty.Type.TypeKind() == EdmTypeKind.Collection)
				{
					this.isFromCollection = true;
				}
				IEdmUnknownEntitySet edmUnknownEntitySet = navigationSource as IEdmUnknownEntitySet;
				if (edmUnknownEntitySet != null && edmUnknownEntitySet.Type.TypeKind == EdmTypeKind.Collection)
				{
					this.isFromCollection = true;
				}
				this.navigationSourceName = ((this.navigationSource == null) ? null : this.navigationSource.Name);
				IEdmEntityType edmEntityType = this.expectedResourceType as IEdmEntityType;
				this.isMediaLinkEntry = edmEntityType != null && edmEntityType.HasStream;
			}

			// Token: 0x1700054C RID: 1356
			// (get) Token: 0x06001787 RID: 6023 RVA: 0x000476A8 File Offset: 0x000458A8
			public override string NavigationSourceName
			{
				get
				{
					return this.navigationSourceName;
				}
			}

			// Token: 0x1700054D RID: 1357
			// (get) Token: 0x06001788 RID: 6024 RVA: 0x000476B0 File Offset: 0x000458B0
			public override string NavigationSourceEntityTypeName
			{
				get
				{
					if (this.navigationSourceEntityType != null)
					{
						this.navigationSourceEntityTypeName = this.navigationSourceEntityType.FullName();
					}
					return this.navigationSourceEntityTypeName;
				}
			}

			// Token: 0x1700054E RID: 1358
			// (get) Token: 0x06001789 RID: 6025 RVA: 0x000476D1 File Offset: 0x000458D1
			public override string NavigationSourceFullTypeName
			{
				get
				{
					if (this.navigationSource != null)
					{
						this.navigationSourceFullTypeName = this.navigationSource.Type.FullTypeName();
					}
					return this.navigationSourceFullTypeName;
				}
			}

			// Token: 0x1700054F RID: 1359
			// (get) Token: 0x0600178A RID: 6026 RVA: 0x000476F7 File Offset: 0x000458F7
			public override EdmNavigationSourceKind NavigationSourceKind
			{
				get
				{
					return this.navigationSource.NavigationSourceKind();
				}
			}

			// Token: 0x17000550 RID: 1360
			// (get) Token: 0x0600178B RID: 6027 RVA: 0x00047704 File Offset: 0x00045904
			public override string ExpectedResourceTypeName
			{
				get
				{
					return this.expectedResourceType.FullTypeName();
				}
			}

			// Token: 0x17000551 RID: 1361
			// (get) Token: 0x0600178C RID: 6028 RVA: 0x0000A282 File Offset: 0x00008482
			public override IEdmStructuredType ExpectedResourceType
			{
				get
				{
					return this.expectedResourceType;
				}
			}

			// Token: 0x17000552 RID: 1362
			// (get) Token: 0x0600178D RID: 6029 RVA: 0x00047711 File Offset: 0x00045911
			public override bool IsMediaLinkEntry
			{
				get
				{
					return this.isMediaLinkEntry;
				}
			}

			// Token: 0x17000553 RID: 1363
			// (get) Token: 0x0600178E RID: 6030 RVA: 0x00047719 File Offset: 0x00045919
			public override bool IsFromCollection
			{
				get
				{
					return this.isFromCollection;
				}
			}

			// Token: 0x17000554 RID: 1364
			// (get) Token: 0x0600178F RID: 6031 RVA: 0x00047721 File Offset: 0x00045921
			internal IEdmEntityType NavigationSourceEntityType
			{
				get
				{
					return this.navigationSourceEntityType;
				}
			}

			// Token: 0x04000AFC RID: 2812
			private readonly IEdmNavigationSource navigationSource;

			// Token: 0x04000AFD RID: 2813
			private readonly IEdmEntityType navigationSourceEntityType;

			// Token: 0x04000AFE RID: 2814
			private readonly string navigationSourceName;

			// Token: 0x04000AFF RID: 2815
			private readonly bool isMediaLinkEntry;

			// Token: 0x04000B00 RID: 2816
			private readonly bool isFromCollection;

			// Token: 0x04000B01 RID: 2817
			private string navigationSourceFullTypeName;

			// Token: 0x04000B02 RID: 2818
			private string navigationSourceEntityTypeName;
		}
	}
}
