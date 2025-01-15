using System;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Evaluation;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData
{
	// Token: 0x02000117 RID: 279
	internal class ODataFeedAndEntryTypeContext : IODataFeedAndEntryTypeContext
	{
		// Token: 0x0600075A RID: 1882 RVA: 0x00019170 File Offset: 0x00017370
		private ODataFeedAndEntryTypeContext(bool throwIfMissingTypeInfo)
		{
			this.throwIfMissingTypeInfo = throwIfMissingTypeInfo;
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x0001917F File Offset: 0x0001737F
		public virtual string EntitySetName
		{
			get
			{
				return this.ValidateAndReturn<string>(null);
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600075C RID: 1884 RVA: 0x00019188 File Offset: 0x00017388
		public virtual string EntitySetElementTypeName
		{
			get
			{
				return this.ValidateAndReturn<string>(null);
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x00019191 File Offset: 0x00017391
		public virtual string ExpectedEntityTypeName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x0600075E RID: 1886 RVA: 0x00019194 File Offset: 0x00017394
		public virtual bool IsMediaLinkEntry
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x00019197 File Offset: 0x00017397
		public virtual UrlConvention UrlConvention
		{
			get
			{
				return ODataFeedAndEntryTypeContext.DefaultUrlConvention;
			}
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0001919E File Offset: 0x0001739E
		internal static ODataFeedAndEntryTypeContext Create(ODataFeedAndEntrySerializationInfo serializationInfo, IEdmEntitySet entitySet, IEdmEntityType entitySetElementType, IEdmEntityType expectedEntityType, IEdmModel model, bool throwIfMissingTypeInfo)
		{
			if (serializationInfo != null)
			{
				return new ODataFeedAndEntryTypeContext.ODataFeedAndEntryTypeContextWithoutModel(serializationInfo);
			}
			if (entitySet != null && model.IsUserModel())
			{
				return new ODataFeedAndEntryTypeContext.ODataFeedAndEntryTypeContextWithModel(entitySet, entitySetElementType, expectedEntityType, model);
			}
			return new ODataFeedAndEntryTypeContext(throwIfMissingTypeInfo);
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x000191C8 File Offset: 0x000173C8
		private T ValidateAndReturn<T>(T value) where T : class
		{
			if (this.throwIfMissingTypeInfo && value == null)
			{
				throw new ODataException(Strings.ODataFeedAndEntryTypeContext_MetadataOrSerializationInfoMissing);
			}
			return value;
		}

		// Token: 0x040002CE RID: 718
		private static readonly UrlConvention DefaultUrlConvention = UrlConvention.CreateWithExplicitValue(false);

		// Token: 0x040002CF RID: 719
		private readonly bool throwIfMissingTypeInfo;

		// Token: 0x02000118 RID: 280
		private sealed class ODataFeedAndEntryTypeContextWithoutModel : ODataFeedAndEntryTypeContext
		{
			// Token: 0x06000763 RID: 1891 RVA: 0x000191F3 File Offset: 0x000173F3
			internal ODataFeedAndEntryTypeContextWithoutModel(ODataFeedAndEntrySerializationInfo serializationInfo)
				: base(false)
			{
				this.serializationInfo = serializationInfo;
			}

			// Token: 0x170001DD RID: 477
			// (get) Token: 0x06000764 RID: 1892 RVA: 0x00019203 File Offset: 0x00017403
			public override string EntitySetName
			{
				get
				{
					return this.serializationInfo.EntitySetName;
				}
			}

			// Token: 0x170001DE RID: 478
			// (get) Token: 0x06000765 RID: 1893 RVA: 0x00019210 File Offset: 0x00017410
			public override string EntitySetElementTypeName
			{
				get
				{
					return this.serializationInfo.EntitySetElementTypeName;
				}
			}

			// Token: 0x170001DF RID: 479
			// (get) Token: 0x06000766 RID: 1894 RVA: 0x0001921D File Offset: 0x0001741D
			public override string ExpectedEntityTypeName
			{
				get
				{
					return this.serializationInfo.ExpectedTypeName;
				}
			}

			// Token: 0x170001E0 RID: 480
			// (get) Token: 0x06000767 RID: 1895 RVA: 0x0001922A File Offset: 0x0001742A
			public override bool IsMediaLinkEntry
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170001E1 RID: 481
			// (get) Token: 0x06000768 RID: 1896 RVA: 0x0001922D File Offset: 0x0001742D
			public override UrlConvention UrlConvention
			{
				get
				{
					return ODataFeedAndEntryTypeContext.DefaultUrlConvention;
				}
			}

			// Token: 0x040002D0 RID: 720
			private readonly ODataFeedAndEntrySerializationInfo serializationInfo;
		}

		// Token: 0x02000119 RID: 281
		private sealed class ODataFeedAndEntryTypeContextWithModel : ODataFeedAndEntryTypeContext
		{
			// Token: 0x06000769 RID: 1897 RVA: 0x000192B8 File Offset: 0x000174B8
			internal ODataFeedAndEntryTypeContextWithModel(IEdmEntitySet entitySet, IEdmEntityType entitySetElementType, IEdmEntityType expectedEntityType, IEdmModel model)
				: base(false)
			{
				this.entitySet = entitySet;
				this.entitySetElementType = entitySetElementType;
				this.expectedEntityType = expectedEntityType;
				this.model = model;
				this.lazyEntitySetName = new SimpleLazy<string>(delegate
				{
					if (!this.model.IsDefaultEntityContainer(this.entitySet.Container))
					{
						return this.entitySet.Container.FullName() + "." + this.entitySet.Name;
					}
					return this.entitySet.Name;
				});
				this.lazyIsMediaLinkEntry = new SimpleLazy<bool>(() => this.model.HasDefaultStream(this.expectedEntityType));
				this.lazyUrlConvention = new SimpleLazy<UrlConvention>(() => UrlConvention.ForEntityContainer(this.model, this.entitySet.Container));
			}

			// Token: 0x170001E2 RID: 482
			// (get) Token: 0x0600076A RID: 1898 RVA: 0x00019343 File Offset: 0x00017543
			public override string EntitySetName
			{
				get
				{
					return this.lazyEntitySetName.Value;
				}
			}

			// Token: 0x170001E3 RID: 483
			// (get) Token: 0x0600076B RID: 1899 RVA: 0x00019350 File Offset: 0x00017550
			public override string EntitySetElementTypeName
			{
				get
				{
					return this.entitySetElementType.FullName();
				}
			}

			// Token: 0x170001E4 RID: 484
			// (get) Token: 0x0600076C RID: 1900 RVA: 0x0001935D File Offset: 0x0001755D
			public override string ExpectedEntityTypeName
			{
				get
				{
					return this.expectedEntityType.FullName();
				}
			}

			// Token: 0x170001E5 RID: 485
			// (get) Token: 0x0600076D RID: 1901 RVA: 0x0001936A File Offset: 0x0001756A
			public override bool IsMediaLinkEntry
			{
				get
				{
					return this.lazyIsMediaLinkEntry.Value;
				}
			}

			// Token: 0x170001E6 RID: 486
			// (get) Token: 0x0600076E RID: 1902 RVA: 0x00019377 File Offset: 0x00017577
			public override UrlConvention UrlConvention
			{
				get
				{
					return this.lazyUrlConvention.Value;
				}
			}

			// Token: 0x040002D1 RID: 721
			private readonly IEdmModel model;

			// Token: 0x040002D2 RID: 722
			private readonly IEdmEntitySet entitySet;

			// Token: 0x040002D3 RID: 723
			private readonly IEdmEntityType entitySetElementType;

			// Token: 0x040002D4 RID: 724
			private readonly IEdmEntityType expectedEntityType;

			// Token: 0x040002D5 RID: 725
			private readonly SimpleLazy<string> lazyEntitySetName;

			// Token: 0x040002D6 RID: 726
			private readonly SimpleLazy<bool> lazyIsMediaLinkEntry;

			// Token: 0x040002D7 RID: 727
			private readonly SimpleLazy<UrlConvention> lazyUrlConvention;
		}
	}
}
