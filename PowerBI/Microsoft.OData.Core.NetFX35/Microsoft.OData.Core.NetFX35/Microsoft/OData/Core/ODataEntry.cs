using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Core.Evaluation;

namespace Microsoft.OData.Core
{
	// Token: 0x0200016E RID: 366
	[DebuggerDisplay("Id: {Id} TypeName: {TypeName}")]
	public sealed class ODataEntry : ODataItem
	{
		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000D5C RID: 3420 RVA: 0x00031237 File Offset: 0x0002F437
		// (set) Token: 0x06000D5D RID: 3421 RVA: 0x00031244 File Offset: 0x0002F444
		public string ETag
		{
			get
			{
				return this.MetadataBuilder.GetETag();
			}
			set
			{
				this.etag = value;
				this.hasNonComputedETag = true;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000D5E RID: 3422 RVA: 0x00031254 File Offset: 0x0002F454
		// (set) Token: 0x06000D5F RID: 3423 RVA: 0x00031261 File Offset: 0x0002F461
		public Uri Id
		{
			get
			{
				return this.MetadataBuilder.GetId();
			}
			set
			{
				this.id = value;
				this.hasNonComputedId = true;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000D60 RID: 3424 RVA: 0x00031271 File Offset: 0x0002F471
		// (set) Token: 0x06000D61 RID: 3425 RVA: 0x0003127E File Offset: 0x0002F47E
		public Uri EditLink
		{
			get
			{
				return this.MetadataBuilder.GetEditLink();
			}
			set
			{
				this.editLink = value;
				this.hasNonComputedEditLink = true;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000D62 RID: 3426 RVA: 0x0003128E File Offset: 0x0002F48E
		// (set) Token: 0x06000D63 RID: 3427 RVA: 0x00031296 File Offset: 0x0002F496
		public bool IsTransient { get; set; }

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000D64 RID: 3428 RVA: 0x0003129F File Offset: 0x0002F49F
		// (set) Token: 0x06000D65 RID: 3429 RVA: 0x000312AC File Offset: 0x0002F4AC
		public Uri ReadLink
		{
			get
			{
				return this.MetadataBuilder.GetReadLink();
			}
			set
			{
				this.readLink = value;
				this.hasNonComputedReadLink = true;
			}
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000D66 RID: 3430 RVA: 0x000312BC File Offset: 0x0002F4BC
		// (set) Token: 0x06000D67 RID: 3431 RVA: 0x000312C9 File Offset: 0x0002F4C9
		public ODataStreamReferenceValue MediaResource
		{
			get
			{
				return this.MetadataBuilder.GetMediaResource();
			}
			set
			{
				this.mediaResource = value;
			}
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000D68 RID: 3432 RVA: 0x000312D2 File Offset: 0x0002F4D2
		public IEnumerable<ODataAction> Actions
		{
			get
			{
				return this.MetadataBuilder.GetActions();
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000D69 RID: 3433 RVA: 0x000312DF File Offset: 0x0002F4DF
		public IEnumerable<ODataFunction> Functions
		{
			get
			{
				return this.MetadataBuilder.GetFunctions();
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000D6A RID: 3434 RVA: 0x000312EC File Offset: 0x0002F4EC
		// (set) Token: 0x06000D6B RID: 3435 RVA: 0x000312FF File Offset: 0x0002F4FF
		public IEnumerable<ODataProperty> Properties
		{
			get
			{
				return this.MetadataBuilder.GetProperties(this.properties);
			}
			set
			{
				this.properties = value;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000D6C RID: 3436 RVA: 0x00031308 File Offset: 0x0002F508
		// (set) Token: 0x06000D6D RID: 3437 RVA: 0x00031310 File Offset: 0x0002F510
		public string TypeName { get; set; }

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000D6E RID: 3438 RVA: 0x00031319 File Offset: 0x0002F519
		// (set) Token: 0x06000D6F RID: 3439 RVA: 0x00031321 File Offset: 0x0002F521
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

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000D70 RID: 3440 RVA: 0x0003132A File Offset: 0x0002F52A
		// (set) Token: 0x06000D71 RID: 3441 RVA: 0x00031346 File Offset: 0x0002F546
		internal ODataEntityMetadataBuilder MetadataBuilder
		{
			get
			{
				if (this.metadataBuilder == null)
				{
					this.metadataBuilder = new NoOpEntityMetadataBuilder(this);
				}
				return this.metadataBuilder;
			}
			set
			{
				this.metadataBuilder = value;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000D72 RID: 3442 RVA: 0x0003134F File Offset: 0x0002F54F
		internal Uri NonComputedId
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000D73 RID: 3443 RVA: 0x00031357 File Offset: 0x0002F557
		internal bool HasNonComputedId
		{
			get
			{
				return this.hasNonComputedId;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x0003135F File Offset: 0x0002F55F
		internal Uri NonComputedEditLink
		{
			get
			{
				return this.editLink;
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000D75 RID: 3445 RVA: 0x00031367 File Offset: 0x0002F567
		internal bool HasNonComputedEditLink
		{
			get
			{
				return this.hasNonComputedEditLink;
			}
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000D76 RID: 3446 RVA: 0x0003136F File Offset: 0x0002F56F
		internal Uri NonComputedReadLink
		{
			get
			{
				return this.readLink;
			}
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000D77 RID: 3447 RVA: 0x00031377 File Offset: 0x0002F577
		internal bool HasNonComputedReadLink
		{
			get
			{
				return this.hasNonComputedReadLink;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000D78 RID: 3448 RVA: 0x0003137F File Offset: 0x0002F57F
		internal string NonComputedETag
		{
			get
			{
				return this.etag;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000D79 RID: 3449 RVA: 0x00031387 File Offset: 0x0002F587
		internal bool HasNonComputedETag
		{
			get
			{
				return this.hasNonComputedETag;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000D7A RID: 3450 RVA: 0x0003138F File Offset: 0x0002F58F
		internal ODataStreamReferenceValue NonComputedMediaResource
		{
			get
			{
				return this.mediaResource;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000D7B RID: 3451 RVA: 0x00031397 File Offset: 0x0002F597
		internal IEnumerable<ODataProperty> NonComputedProperties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x0003139F File Offset: 0x0002F59F
		internal IEnumerable<ODataAction> NonComputedActions
		{
			get
			{
				if (this.actions != null)
				{
					return new ReadOnlyEnumerable<ODataAction>(this.actions);
				}
				return null;
			}
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000D7D RID: 3453 RVA: 0x000313B6 File Offset: 0x0002F5B6
		internal IEnumerable<ODataFunction> NonComputedFunctions
		{
			get
			{
				if (this.functions != null)
				{
					return new ReadOnlyEnumerable<ODataFunction>(this.functions);
				}
				return null;
			}
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000D7E RID: 3454 RVA: 0x000313CD File Offset: 0x0002F5CD
		// (set) Token: 0x06000D7F RID: 3455 RVA: 0x000313D5 File Offset: 0x0002F5D5
		internal ODataFeedAndEntrySerializationInfo SerializationInfo
		{
			get
			{
				return this.serializationInfo;
			}
			set
			{
				this.serializationInfo = ODataFeedAndEntrySerializationInfo.Validate(value);
			}
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x000313E3 File Offset: 0x0002F5E3
		public void AddAction(ODataAction action)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataAction>(action, "action");
			if (!this.actions.Contains(action))
			{
				this.actions.Add(action);
			}
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x0003140A File Offset: 0x0002F60A
		public void AddFunction(ODataFunction function)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataFunction>(function, "function");
			if (!this.functions.Contains(function))
			{
				this.functions.Add(function);
			}
		}

		// Token: 0x040005D5 RID: 1493
		private ODataEntityMetadataBuilder metadataBuilder;

		// Token: 0x040005D6 RID: 1494
		private string etag;

		// Token: 0x040005D7 RID: 1495
		private bool hasNonComputedETag;

		// Token: 0x040005D8 RID: 1496
		private Uri id;

		// Token: 0x040005D9 RID: 1497
		private bool hasNonComputedId;

		// Token: 0x040005DA RID: 1498
		private Uri editLink;

		// Token: 0x040005DB RID: 1499
		private bool hasNonComputedEditLink;

		// Token: 0x040005DC RID: 1500
		private Uri readLink;

		// Token: 0x040005DD RID: 1501
		private bool hasNonComputedReadLink;

		// Token: 0x040005DE RID: 1502
		private ODataStreamReferenceValue mediaResource;

		// Token: 0x040005DF RID: 1503
		private IEnumerable<ODataProperty> properties;

		// Token: 0x040005E0 RID: 1504
		private List<ODataAction> actions = new List<ODataAction>();

		// Token: 0x040005E1 RID: 1505
		private List<ODataFunction> functions = new List<ODataFunction>();

		// Token: 0x040005E2 RID: 1506
		private ODataFeedAndEntrySerializationInfo serializationInfo;
	}
}
