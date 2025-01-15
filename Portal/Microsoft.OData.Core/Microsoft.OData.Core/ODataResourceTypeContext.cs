using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x02000087 RID: 135
	internal class ODataResourceTypeContext : IODataResourceTypeContext
	{
		// Token: 0x060004B3 RID: 1203 RVA: 0x0000C188 File Offset: 0x0000A388
		private ODataResourceTypeContext(bool throwIfMissingTypeInfo)
		{
			this.throwIfMissingTypeInfo = throwIfMissingTypeInfo;
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0000C197 File Offset: 0x0000A397
		private ODataResourceTypeContext(IEdmStructuredType expectedResourceType, bool throwIfMissingTypeInfo)
		{
			this.expectedResourceType = expectedResourceType;
			this.throwIfMissingTypeInfo = throwIfMissingTypeInfo;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x0000C1AD File Offset: 0x0000A3AD
		public virtual string NavigationSourceName
		{
			get
			{
				return this.ValidateAndReturn<string>(null);
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x0000C1AD File Offset: 0x0000A3AD
		public virtual string NavigationSourceEntityTypeName
		{
			get
			{
				return this.ValidateAndReturn<string>(null);
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x0000C1AD File Offset: 0x0000A3AD
		public virtual string NavigationSourceFullTypeName
		{
			get
			{
				return this.ValidateAndReturn<string>(null);
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x00002390 File Offset: 0x00000590
		public virtual EdmNavigationSourceKind NavigationSourceKind
		{
			get
			{
				return EdmNavigationSourceKind.None;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x0000C1B6 File Offset: 0x0000A3B6
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

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x0000C1E2 File Offset: 0x0000A3E2
		public virtual IEdmStructuredType ExpectedResourceType
		{
			get
			{
				return this.expectedResourceType;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x00002390 File Offset: 0x00000590
		public virtual bool IsFromCollection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x00002390 File Offset: 0x00000590
		public virtual bool IsMediaLinkEntry
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0000C1EA File Offset: 0x0000A3EA
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

		// Token: 0x060004BE RID: 1214 RVA: 0x0000C221 File Offset: 0x0000A421
		private T ValidateAndReturn<T>(T value) where T : class
		{
			if (this.throwIfMissingTypeInfo && value == null)
			{
				throw new ODataException(Strings.ODataResourceTypeContext_MetadataOrSerializationInfoMissing);
			}
			return value;
		}

		// Token: 0x04000217 RID: 535
		protected IEdmStructuredType expectedResourceType;

		// Token: 0x04000218 RID: 536
		protected string expectedResourceTypeName;

		// Token: 0x04000219 RID: 537
		private readonly bool throwIfMissingTypeInfo;

		// Token: 0x020002B1 RID: 689
		internal sealed class ODataResourceTypeContextWithoutModel : ODataResourceTypeContext
		{
			// Token: 0x06001CD9 RID: 7385 RVA: 0x00057192 File Offset: 0x00055392
			internal ODataResourceTypeContextWithoutModel(ODataResourceSerializationInfo serializationInfo)
				: base(false)
			{
				this.serializationInfo = serializationInfo;
			}

			// Token: 0x170005E2 RID: 1506
			// (get) Token: 0x06001CDA RID: 7386 RVA: 0x000571A2 File Offset: 0x000553A2
			public override string NavigationSourceName
			{
				get
				{
					return this.serializationInfo.NavigationSourceName;
				}
			}

			// Token: 0x170005E3 RID: 1507
			// (get) Token: 0x06001CDB RID: 7387 RVA: 0x000571AF File Offset: 0x000553AF
			public override string NavigationSourceEntityTypeName
			{
				get
				{
					return this.serializationInfo.NavigationSourceEntityTypeName;
				}
			}

			// Token: 0x170005E4 RID: 1508
			// (get) Token: 0x06001CDC RID: 7388 RVA: 0x000571BC File Offset: 0x000553BC
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

			// Token: 0x170005E5 RID: 1509
			// (get) Token: 0x06001CDD RID: 7389 RVA: 0x000571E2 File Offset: 0x000553E2
			public override EdmNavigationSourceKind NavigationSourceKind
			{
				get
				{
					return this.serializationInfo.NavigationSourceKind;
				}
			}

			// Token: 0x170005E6 RID: 1510
			// (get) Token: 0x06001CDE RID: 7390 RVA: 0x000571EF File Offset: 0x000553EF
			public override string ExpectedResourceTypeName
			{
				get
				{
					return this.serializationInfo.ExpectedTypeName;
				}
			}

			// Token: 0x170005E7 RID: 1511
			// (get) Token: 0x06001CDF RID: 7391 RVA: 0x0000360D File Offset: 0x0000180D
			public override IEdmStructuredType ExpectedResourceType
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170005E8 RID: 1512
			// (get) Token: 0x06001CE0 RID: 7392 RVA: 0x00002390 File Offset: 0x00000590
			public override bool IsMediaLinkEntry
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170005E9 RID: 1513
			// (get) Token: 0x06001CE1 RID: 7393 RVA: 0x000571FC File Offset: 0x000553FC
			public override bool IsFromCollection
			{
				get
				{
					return this.serializationInfo.IsFromCollection;
				}
			}

			// Token: 0x04000C6F RID: 3183
			private readonly ODataResourceSerializationInfo serializationInfo;
		}

		// Token: 0x020002B2 RID: 690
		internal sealed class ODataResourceTypeContextWithModel : ODataResourceTypeContext
		{
			// Token: 0x06001CE2 RID: 7394 RVA: 0x0005720C File Offset: 0x0005540C
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

			// Token: 0x170005EA RID: 1514
			// (get) Token: 0x06001CE3 RID: 7395 RVA: 0x000572AC File Offset: 0x000554AC
			public override string NavigationSourceName
			{
				get
				{
					return this.navigationSourceName;
				}
			}

			// Token: 0x170005EB RID: 1515
			// (get) Token: 0x06001CE4 RID: 7396 RVA: 0x000572B4 File Offset: 0x000554B4
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

			// Token: 0x170005EC RID: 1516
			// (get) Token: 0x06001CE5 RID: 7397 RVA: 0x000572D5 File Offset: 0x000554D5
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

			// Token: 0x170005ED RID: 1517
			// (get) Token: 0x06001CE6 RID: 7398 RVA: 0x000572FB File Offset: 0x000554FB
			public override EdmNavigationSourceKind NavigationSourceKind
			{
				get
				{
					return this.navigationSource.NavigationSourceKind();
				}
			}

			// Token: 0x170005EE RID: 1518
			// (get) Token: 0x06001CE7 RID: 7399 RVA: 0x00057308 File Offset: 0x00055508
			public override string ExpectedResourceTypeName
			{
				get
				{
					return this.expectedResourceType.FullTypeName();
				}
			}

			// Token: 0x170005EF RID: 1519
			// (get) Token: 0x06001CE8 RID: 7400 RVA: 0x0000C1E2 File Offset: 0x0000A3E2
			public override IEdmStructuredType ExpectedResourceType
			{
				get
				{
					return this.expectedResourceType;
				}
			}

			// Token: 0x170005F0 RID: 1520
			// (get) Token: 0x06001CE9 RID: 7401 RVA: 0x00057315 File Offset: 0x00055515
			public override bool IsMediaLinkEntry
			{
				get
				{
					return this.isMediaLinkEntry;
				}
			}

			// Token: 0x170005F1 RID: 1521
			// (get) Token: 0x06001CEA RID: 7402 RVA: 0x0005731D File Offset: 0x0005551D
			public override bool IsFromCollection
			{
				get
				{
					return this.isFromCollection;
				}
			}

			// Token: 0x170005F2 RID: 1522
			// (get) Token: 0x06001CEB RID: 7403 RVA: 0x00057325 File Offset: 0x00055525
			internal IEdmEntityType NavigationSourceEntityType
			{
				get
				{
					return this.navigationSourceEntityType;
				}
			}

			// Token: 0x04000C70 RID: 3184
			private readonly IEdmNavigationSource navigationSource;

			// Token: 0x04000C71 RID: 3185
			private readonly IEdmEntityType navigationSourceEntityType;

			// Token: 0x04000C72 RID: 3186
			private readonly string navigationSourceName;

			// Token: 0x04000C73 RID: 3187
			private readonly bool isMediaLinkEntry;

			// Token: 0x04000C74 RID: 3188
			private readonly bool isFromCollection;

			// Token: 0x04000C75 RID: 3189
			private string navigationSourceFullTypeName;

			// Token: 0x04000C76 RID: 3190
			private string navigationSourceEntityTypeName;
		}
	}
}
