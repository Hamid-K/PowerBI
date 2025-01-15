using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Metadata;

namespace Microsoft.OData
{
	// Token: 0x02000088 RID: 136
	public abstract class ODataResourceSetBase : ODataItem
	{
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x0000C23F File Offset: 0x0000A43F
		// (set) Token: 0x060004C0 RID: 1216 RVA: 0x0000C27A File Offset: 0x0000A47A
		public string TypeName
		{
			get
			{
				if (this.typeName == null && this.SerializationInfo != null && this.SerializationInfo.ExpectedTypeName != null)
				{
					this.typeName = EdmLibraryExtensions.GetCollectionTypeName(this.SerializationInfo.ExpectedTypeName);
				}
				return this.typeName;
			}
			set
			{
				this.typeName = value;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x0000C283 File Offset: 0x0000A483
		// (set) Token: 0x060004C2 RID: 1218 RVA: 0x0000C28B File Offset: 0x0000A48B
		public long? Count { get; set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x0000C294 File Offset: 0x0000A494
		// (set) Token: 0x060004C4 RID: 1220 RVA: 0x0000C29C File Offset: 0x0000A49C
		public Uri Id { get; set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0000C2A5 File Offset: 0x0000A4A5
		// (set) Token: 0x060004C6 RID: 1222 RVA: 0x0000C2AD File Offset: 0x0000A4AD
		public Uri NextPageLink
		{
			get
			{
				return this.nextPageLink;
			}
			set
			{
				if (this.DeltaLink != null && value != null)
				{
					throw new ODataException(Strings.ODataResourceSet_MustNotContainBothNextPageLinkAndDeltaLink);
				}
				this.nextPageLink = value;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x0000C2D8 File Offset: 0x0000A4D8
		// (set) Token: 0x060004C8 RID: 1224 RVA: 0x0000C2E0 File Offset: 0x0000A4E0
		public Uri DeltaLink
		{
			get
			{
				return this.deltaLink;
			}
			set
			{
				if (this.NextPageLink != null && value != null)
				{
					throw new ODataException(Strings.ODataResourceSet_MustNotContainBothNextPageLinkAndDeltaLink);
				}
				this.deltaLink = value;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x000035C0 File Offset: 0x000017C0
		// (set) Token: 0x060004CA RID: 1226 RVA: 0x000035C8 File Offset: 0x000017C8
		[SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly", Justification = "We want to allow the same instance annotation collection instance to be shared across ODataLib OM instances.")]
		public ICollection<ODataInstanceAnnotation> InstanceAnnotations
		{
			get
			{
				return base.GetInstanceAnnotations();
			}
			set
			{
				base.SetInstanceAnnotations(value);
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x0000C30B File Offset: 0x0000A50B
		// (set) Token: 0x060004CC RID: 1228 RVA: 0x0000C313 File Offset: 0x0000A513
		internal ODataResourceSerializationInfo SerializationInfo
		{
			get
			{
				return this.serializationInfo;
			}
			set
			{
				this.serializationInfo = ODataResourceSerializationInfo.Validate(value);
			}
		}

		// Token: 0x0400021A RID: 538
		private Uri nextPageLink;

		// Token: 0x0400021B RID: 539
		private Uri deltaLink;

		// Token: 0x0400021C RID: 540
		private ODataResourceSerializationInfo serializationInfo;

		// Token: 0x0400021D RID: 541
		private string typeName;
	}
}
