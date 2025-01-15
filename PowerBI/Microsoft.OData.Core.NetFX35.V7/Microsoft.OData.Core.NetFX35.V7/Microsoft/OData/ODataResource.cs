using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Evaluation;

namespace Microsoft.OData
{
	// Token: 0x0200005C RID: 92
	[DebuggerDisplay("Id: {Id} TypeName: {TypeName}")]
	public sealed class ODataResource : ODataItem
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002CB RID: 715 RVA: 0x00009D01 File Offset: 0x00007F01
		// (set) Token: 0x060002CC RID: 716 RVA: 0x00009D0E File Offset: 0x00007F0E
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

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002CD RID: 717 RVA: 0x00009D1E File Offset: 0x00007F1E
		// (set) Token: 0x060002CE RID: 718 RVA: 0x00009D2B File Offset: 0x00007F2B
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

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002CF RID: 719 RVA: 0x00009D3B File Offset: 0x00007F3B
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x00009D48 File Offset: 0x00007F48
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

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x00009D58 File Offset: 0x00007F58
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x00009D60 File Offset: 0x00007F60
		public bool IsTransient { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x00009D69 File Offset: 0x00007F69
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x00009D76 File Offset: 0x00007F76
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

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x00009D86 File Offset: 0x00007F86
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x00009D93 File Offset: 0x00007F93
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

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x00009D9C File Offset: 0x00007F9C
		public IEnumerable<ODataAction> Actions
		{
			get
			{
				return this.MetadataBuilder.GetActions();
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x00009DA9 File Offset: 0x00007FA9
		public IEnumerable<ODataFunction> Functions
		{
			get
			{
				return this.MetadataBuilder.GetFunctions();
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x00009DB6 File Offset: 0x00007FB6
		// (set) Token: 0x060002DA RID: 730 RVA: 0x00009DC9 File Offset: 0x00007FC9
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

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002DB RID: 731 RVA: 0x00009DD2 File Offset: 0x00007FD2
		// (set) Token: 0x060002DC RID: 732 RVA: 0x00009DDA File Offset: 0x00007FDA
		public string TypeName { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002DD RID: 733 RVA: 0x00009CAD File Offset: 0x00007EAD
		// (set) Token: 0x060002DE RID: 734 RVA: 0x00009CB5 File Offset: 0x00007EB5
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

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002DF RID: 735 RVA: 0x00009DE3 File Offset: 0x00007FE3
		// (set) Token: 0x060002E0 RID: 736 RVA: 0x00009DFF File Offset: 0x00007FFF
		internal ODataResourceMetadataBuilder MetadataBuilder
		{
			get
			{
				if (this.metadataBuilder == null)
				{
					this.metadataBuilder = new NoOpResourceMetadataBuilder(this);
				}
				return this.metadataBuilder;
			}
			set
			{
				this.metadataBuilder = value;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x00009E08 File Offset: 0x00008008
		internal Uri NonComputedId
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x00009E10 File Offset: 0x00008010
		internal bool HasNonComputedId
		{
			get
			{
				return this.hasNonComputedId;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x00009E18 File Offset: 0x00008018
		internal Uri NonComputedEditLink
		{
			get
			{
				return this.editLink;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x00009E20 File Offset: 0x00008020
		internal bool HasNonComputedEditLink
		{
			get
			{
				return this.hasNonComputedEditLink;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x00009E28 File Offset: 0x00008028
		internal Uri NonComputedReadLink
		{
			get
			{
				return this.readLink;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x00009E30 File Offset: 0x00008030
		internal bool HasNonComputedReadLink
		{
			get
			{
				return this.hasNonComputedReadLink;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x00009E38 File Offset: 0x00008038
		internal string NonComputedETag
		{
			get
			{
				return this.etag;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x00009E40 File Offset: 0x00008040
		internal bool HasNonComputedETag
		{
			get
			{
				return this.hasNonComputedETag;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x00009E48 File Offset: 0x00008048
		internal ODataStreamReferenceValue NonComputedMediaResource
		{
			get
			{
				return this.mediaResource;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002EA RID: 746 RVA: 0x00009E50 File Offset: 0x00008050
		internal IEnumerable<ODataProperty> NonComputedProperties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002EB RID: 747 RVA: 0x00009E58 File Offset: 0x00008058
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

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00009E6F File Offset: 0x0000806F
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

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002ED RID: 749 RVA: 0x00009E86 File Offset: 0x00008086
		// (set) Token: 0x060002EE RID: 750 RVA: 0x00009E8E File Offset: 0x0000808E
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

		// Token: 0x060002EF RID: 751 RVA: 0x00009E9C File Offset: 0x0000809C
		public void AddAction(ODataAction action)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataAction>(action, "action");
			if (!this.actions.Contains(action))
			{
				this.actions.Add(action);
			}
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00009EC4 File Offset: 0x000080C4
		public void AddFunction(ODataFunction function)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataFunction>(function, "function");
			if (!this.functions.Contains(function))
			{
				this.functions.Add(function);
			}
		}

		// Token: 0x04000197 RID: 407
		private ODataResourceMetadataBuilder metadataBuilder;

		// Token: 0x04000198 RID: 408
		private string etag;

		// Token: 0x04000199 RID: 409
		private bool hasNonComputedETag;

		// Token: 0x0400019A RID: 410
		private Uri id;

		// Token: 0x0400019B RID: 411
		private bool hasNonComputedId;

		// Token: 0x0400019C RID: 412
		private Uri editLink;

		// Token: 0x0400019D RID: 413
		private bool hasNonComputedEditLink;

		// Token: 0x0400019E RID: 414
		private Uri readLink;

		// Token: 0x0400019F RID: 415
		private bool hasNonComputedReadLink;

		// Token: 0x040001A0 RID: 416
		private ODataStreamReferenceValue mediaResource;

		// Token: 0x040001A1 RID: 417
		private IEnumerable<ODataProperty> properties;

		// Token: 0x040001A2 RID: 418
		private List<ODataAction> actions = new List<ODataAction>();

		// Token: 0x040001A3 RID: 419
		private List<ODataFunction> functions = new List<ODataFunction>();

		// Token: 0x040001A4 RID: 420
		private ODataResourceSerializationInfo serializationInfo;
	}
}
