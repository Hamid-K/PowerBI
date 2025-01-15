using System;
using Microsoft.OData.Core.Evaluation;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core
{
	// Token: 0x02000175 RID: 373
	internal class ODataFeedAndEntryTypeContext : IODataFeedAndEntryTypeContext
	{
		// Token: 0x06000DB4 RID: 3508 RVA: 0x00031722 File Offset: 0x0002F922
		private ODataFeedAndEntryTypeContext(bool throwIfMissingTypeInfo)
		{
			this.throwIfMissingTypeInfo = throwIfMissingTypeInfo;
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000DB5 RID: 3509 RVA: 0x00031731 File Offset: 0x0002F931
		public virtual string NavigationSourceName
		{
			get
			{
				return this.ValidateAndReturn<string>(null);
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000DB6 RID: 3510 RVA: 0x0003173A File Offset: 0x0002F93A
		public virtual string NavigationSourceEntityTypeName
		{
			get
			{
				return this.ValidateAndReturn<string>(null);
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x00031743 File Offset: 0x0002F943
		public virtual string NavigationSourceFullTypeName
		{
			get
			{
				return this.ValidateAndReturn<string>(null);
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000DB8 RID: 3512 RVA: 0x0003174C File Offset: 0x0002F94C
		public virtual EdmNavigationSourceKind NavigationSourceKind
		{
			get
			{
				return EdmNavigationSourceKind.None;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000DB9 RID: 3513 RVA: 0x0003174F File Offset: 0x0002F94F
		public virtual string ExpectedEntityTypeName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000DBA RID: 3514 RVA: 0x00031752 File Offset: 0x0002F952
		public virtual bool IsFromCollection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000DBB RID: 3515 RVA: 0x00031755 File Offset: 0x0002F955
		public virtual bool IsMediaLinkEntry
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000DBC RID: 3516 RVA: 0x00031758 File Offset: 0x0002F958
		public virtual UrlConvention UrlConvention
		{
			get
			{
				return ODataFeedAndEntryTypeContext.DefaultUrlConvention;
			}
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0003175F File Offset: 0x0002F95F
		internal static ODataFeedAndEntryTypeContext Create(ODataFeedAndEntrySerializationInfo serializationInfo, IEdmNavigationSource navigationSource, IEdmEntityType navigationSourceEntityType, IEdmEntityType expectedEntityType, IEdmModel model, bool throwIfMissingTypeInfo)
		{
			if (serializationInfo != null)
			{
				return new ODataFeedAndEntryTypeContext.ODataFeedAndEntryTypeContextWithoutModel(serializationInfo);
			}
			if (navigationSource != null && model.IsUserModel())
			{
				return new ODataFeedAndEntryTypeContext.ODataFeedAndEntryTypeContextWithModel(navigationSource, navigationSourceEntityType, expectedEntityType, model);
			}
			return new ODataFeedAndEntryTypeContext(throwIfMissingTypeInfo);
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x00031789 File Offset: 0x0002F989
		private T ValidateAndReturn<T>(T value) where T : class
		{
			if (this.throwIfMissingTypeInfo && value == null)
			{
				throw new ODataException(Strings.ODataFeedAndEntryTypeContext_MetadataOrSerializationInfoMissing);
			}
			return value;
		}

		// Token: 0x040005F6 RID: 1526
		private static readonly UrlConvention DefaultUrlConvention = UrlConvention.CreateWithExplicitValue(false);

		// Token: 0x040005F7 RID: 1527
		private readonly bool throwIfMissingTypeInfo;

		// Token: 0x02000176 RID: 374
		internal sealed class ODataFeedAndEntryTypeContextWithoutModel : ODataFeedAndEntryTypeContext
		{
			// Token: 0x06000DC0 RID: 3520 RVA: 0x000317B4 File Offset: 0x0002F9B4
			internal ODataFeedAndEntryTypeContextWithoutModel(ODataFeedAndEntrySerializationInfo serializationInfo)
				: base(false)
			{
				this.serializationInfo = serializationInfo;
			}

			// Token: 0x170002E5 RID: 741
			// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x000317C4 File Offset: 0x0002F9C4
			public override string NavigationSourceName
			{
				get
				{
					return this.serializationInfo.NavigationSourceName;
				}
			}

			// Token: 0x170002E6 RID: 742
			// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x000317D1 File Offset: 0x0002F9D1
			public override string NavigationSourceEntityTypeName
			{
				get
				{
					return this.serializationInfo.NavigationSourceEntityTypeName;
				}
			}

			// Token: 0x170002E7 RID: 743
			// (get) Token: 0x06000DC3 RID: 3523 RVA: 0x000317DE File Offset: 0x0002F9DE
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

			// Token: 0x170002E8 RID: 744
			// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x00031804 File Offset: 0x0002FA04
			public override EdmNavigationSourceKind NavigationSourceKind
			{
				get
				{
					return this.serializationInfo.NavigationSourceKind;
				}
			}

			// Token: 0x170002E9 RID: 745
			// (get) Token: 0x06000DC5 RID: 3525 RVA: 0x00031811 File Offset: 0x0002FA11
			public override string ExpectedEntityTypeName
			{
				get
				{
					return this.serializationInfo.ExpectedTypeName;
				}
			}

			// Token: 0x170002EA RID: 746
			// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x0003181E File Offset: 0x0002FA1E
			public override bool IsMediaLinkEntry
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170002EB RID: 747
			// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x00031821 File Offset: 0x0002FA21
			public override UrlConvention UrlConvention
			{
				get
				{
					return ODataFeedAndEntryTypeContext.DefaultUrlConvention;
				}
			}

			// Token: 0x170002EC RID: 748
			// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x00031828 File Offset: 0x0002FA28
			public override bool IsFromCollection
			{
				get
				{
					return this.serializationInfo.IsFromCollection;
				}
			}

			// Token: 0x040005F8 RID: 1528
			private readonly ODataFeedAndEntrySerializationInfo serializationInfo;
		}

		// Token: 0x02000177 RID: 375
		internal sealed class ODataFeedAndEntryTypeContextWithModel : ODataFeedAndEntryTypeContext
		{
			// Token: 0x06000DC9 RID: 3529 RVA: 0x00031844 File Offset: 0x0002FA44
			internal ODataFeedAndEntryTypeContextWithModel(IEdmNavigationSource navigationSource, IEdmEntityType navigationSourceEntityType, IEdmEntityType expectedEntityType, IEdmModel model)
				: base(false)
			{
				this.navigationSource = navigationSource;
				this.navigationSourceEntityType = navigationSourceEntityType;
				this.expectedEntityType = expectedEntityType;
				this.model = model;
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
				this.navigationSourceName = this.navigationSource.Name;
				this.isMediaLinkEntry = this.expectedEntityType.HasStream;
				this.lazyUrlConvention = new SimpleLazy<UrlConvention>(() => UrlConvention.ForModel(this.model));
			}

			// Token: 0x170002ED RID: 749
			// (get) Token: 0x06000DCA RID: 3530 RVA: 0x000318F8 File Offset: 0x0002FAF8
			public override string NavigationSourceName
			{
				get
				{
					return this.navigationSourceName;
				}
			}

			// Token: 0x170002EE RID: 750
			// (get) Token: 0x06000DCB RID: 3531 RVA: 0x00031900 File Offset: 0x0002FB00
			public override string NavigationSourceEntityTypeName
			{
				get
				{
					return this.navigationSourceEntityType.FullName();
				}
			}

			// Token: 0x170002EF RID: 751
			// (get) Token: 0x06000DCC RID: 3532 RVA: 0x0003190D File Offset: 0x0002FB0D
			public override string NavigationSourceFullTypeName
			{
				get
				{
					return this.navigationSource.Type.FullTypeName();
				}
			}

			// Token: 0x170002F0 RID: 752
			// (get) Token: 0x06000DCD RID: 3533 RVA: 0x0003191F File Offset: 0x0002FB1F
			public override EdmNavigationSourceKind NavigationSourceKind
			{
				get
				{
					return this.navigationSource.NavigationSourceKind();
				}
			}

			// Token: 0x170002F1 RID: 753
			// (get) Token: 0x06000DCE RID: 3534 RVA: 0x0003192C File Offset: 0x0002FB2C
			public override string ExpectedEntityTypeName
			{
				get
				{
					return this.expectedEntityType.FullName();
				}
			}

			// Token: 0x170002F2 RID: 754
			// (get) Token: 0x06000DCF RID: 3535 RVA: 0x00031939 File Offset: 0x0002FB39
			public override bool IsMediaLinkEntry
			{
				get
				{
					return this.isMediaLinkEntry;
				}
			}

			// Token: 0x170002F3 RID: 755
			// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x00031941 File Offset: 0x0002FB41
			public override UrlConvention UrlConvention
			{
				get
				{
					return this.lazyUrlConvention.Value;
				}
			}

			// Token: 0x170002F4 RID: 756
			// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x0003194E File Offset: 0x0002FB4E
			public override bool IsFromCollection
			{
				get
				{
					return this.isFromCollection;
				}
			}

			// Token: 0x170002F5 RID: 757
			// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x00031956 File Offset: 0x0002FB56
			internal IEdmEntityType NavigationSourceEntityType
			{
				get
				{
					return this.navigationSourceEntityType;
				}
			}

			// Token: 0x040005F9 RID: 1529
			private readonly IEdmModel model;

			// Token: 0x040005FA RID: 1530
			private readonly IEdmNavigationSource navigationSource;

			// Token: 0x040005FB RID: 1531
			private readonly IEdmEntityType navigationSourceEntityType;

			// Token: 0x040005FC RID: 1532
			private readonly IEdmEntityType expectedEntityType;

			// Token: 0x040005FD RID: 1533
			private readonly string navigationSourceName;

			// Token: 0x040005FE RID: 1534
			private readonly bool isMediaLinkEntry;

			// Token: 0x040005FF RID: 1535
			private readonly SimpleLazy<UrlConvention> lazyUrlConvention;

			// Token: 0x04000600 RID: 1536
			private readonly bool isFromCollection;
		}
	}
}
