using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Data.OData.Evaluation;

namespace Microsoft.Data.OData
{
	// Token: 0x020002AC RID: 684
	[DebuggerDisplay("Id: {Id} TypeName: {TypeName}")]
	public sealed class ODataEntry : ODataItem
	{
		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060015D8 RID: 5592 RVA: 0x0004EFD1 File Offset: 0x0004D1D1
		// (set) Token: 0x060015D9 RID: 5593 RVA: 0x0004EFDE File Offset: 0x0004D1DE
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

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060015DA RID: 5594 RVA: 0x0004EFEE File Offset: 0x0004D1EE
		// (set) Token: 0x060015DB RID: 5595 RVA: 0x0004EFFB File Offset: 0x0004D1FB
		public string Id
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

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060015DC RID: 5596 RVA: 0x0004F00B File Offset: 0x0004D20B
		// (set) Token: 0x060015DD RID: 5597 RVA: 0x0004F018 File Offset: 0x0004D218
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

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060015DE RID: 5598 RVA: 0x0004F028 File Offset: 0x0004D228
		// (set) Token: 0x060015DF RID: 5599 RVA: 0x0004F035 File Offset: 0x0004D235
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

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060015E0 RID: 5600 RVA: 0x0004F045 File Offset: 0x0004D245
		// (set) Token: 0x060015E1 RID: 5601 RVA: 0x0004F052 File Offset: 0x0004D252
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

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060015E2 RID: 5602 RVA: 0x0004F05B File Offset: 0x0004D25B
		// (set) Token: 0x060015E3 RID: 5603 RVA: 0x0004F063 File Offset: 0x0004D263
		public IEnumerable<ODataAssociationLink> AssociationLinks { get; set; }

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060015E4 RID: 5604 RVA: 0x0004F06C File Offset: 0x0004D26C
		// (set) Token: 0x060015E5 RID: 5605 RVA: 0x0004F079 File Offset: 0x0004D279
		public IEnumerable<ODataAction> Actions
		{
			get
			{
				return this.MetadataBuilder.GetActions();
			}
			set
			{
				this.actions = value;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060015E6 RID: 5606 RVA: 0x0004F082 File Offset: 0x0004D282
		// (set) Token: 0x060015E7 RID: 5607 RVA: 0x0004F08F File Offset: 0x0004D28F
		public IEnumerable<ODataFunction> Functions
		{
			get
			{
				return this.MetadataBuilder.GetFunctions();
			}
			set
			{
				this.functions = value;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060015E8 RID: 5608 RVA: 0x0004F098 File Offset: 0x0004D298
		// (set) Token: 0x060015E9 RID: 5609 RVA: 0x0004F0AB File Offset: 0x0004D2AB
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

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x060015EA RID: 5610 RVA: 0x0004F0B4 File Offset: 0x0004D2B4
		// (set) Token: 0x060015EB RID: 5611 RVA: 0x0004F0BC File Offset: 0x0004D2BC
		public string TypeName { get; set; }

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x060015EC RID: 5612 RVA: 0x0004F0C5 File Offset: 0x0004D2C5
		// (set) Token: 0x060015ED RID: 5613 RVA: 0x0004F0CD File Offset: 0x0004D2CD
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

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x060015EE RID: 5614 RVA: 0x0004F0D6 File Offset: 0x0004D2D6
		// (set) Token: 0x060015EF RID: 5615 RVA: 0x0004F0F2 File Offset: 0x0004D2F2
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

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x060015F0 RID: 5616 RVA: 0x0004F0FB File Offset: 0x0004D2FB
		internal string NonComputedId
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x060015F1 RID: 5617 RVA: 0x0004F103 File Offset: 0x0004D303
		internal bool HasNonComputedId
		{
			get
			{
				return this.hasNonComputedId;
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x060015F2 RID: 5618 RVA: 0x0004F10B File Offset: 0x0004D30B
		internal Uri NonComputedEditLink
		{
			get
			{
				return this.editLink;
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x060015F3 RID: 5619 RVA: 0x0004F113 File Offset: 0x0004D313
		internal bool HasNonComputedEditLink
		{
			get
			{
				return this.hasNonComputedEditLink;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x060015F4 RID: 5620 RVA: 0x0004F11B File Offset: 0x0004D31B
		internal Uri NonComputedReadLink
		{
			get
			{
				return this.readLink;
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x060015F5 RID: 5621 RVA: 0x0004F123 File Offset: 0x0004D323
		internal bool HasNonComputedReadLink
		{
			get
			{
				return this.hasNonComputedReadLink;
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x060015F6 RID: 5622 RVA: 0x0004F12B File Offset: 0x0004D32B
		internal string NonComputedETag
		{
			get
			{
				return this.etag;
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x060015F7 RID: 5623 RVA: 0x0004F133 File Offset: 0x0004D333
		internal bool HasNonComputedETag
		{
			get
			{
				return this.hasNonComputedETag;
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x060015F8 RID: 5624 RVA: 0x0004F13B File Offset: 0x0004D33B
		internal ODataStreamReferenceValue NonComputedMediaResource
		{
			get
			{
				return this.mediaResource;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x060015F9 RID: 5625 RVA: 0x0004F143 File Offset: 0x0004D343
		internal IEnumerable<ODataProperty> NonComputedProperties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x060015FA RID: 5626 RVA: 0x0004F14B File Offset: 0x0004D34B
		internal IEnumerable<ODataAction> NonComputedActions
		{
			get
			{
				return this.actions;
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060015FB RID: 5627 RVA: 0x0004F153 File Offset: 0x0004D353
		internal IEnumerable<ODataFunction> NonComputedFunctions
		{
			get
			{
				return this.functions;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x060015FC RID: 5628 RVA: 0x0004F15B File Offset: 0x0004D35B
		// (set) Token: 0x060015FD RID: 5629 RVA: 0x0004F163 File Offset: 0x0004D363
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

		// Token: 0x0400098C RID: 2444
		private ODataEntityMetadataBuilder metadataBuilder;

		// Token: 0x0400098D RID: 2445
		private string etag;

		// Token: 0x0400098E RID: 2446
		private bool hasNonComputedETag;

		// Token: 0x0400098F RID: 2447
		private string id;

		// Token: 0x04000990 RID: 2448
		private bool hasNonComputedId;

		// Token: 0x04000991 RID: 2449
		private Uri editLink;

		// Token: 0x04000992 RID: 2450
		private bool hasNonComputedEditLink;

		// Token: 0x04000993 RID: 2451
		private Uri readLink;

		// Token: 0x04000994 RID: 2452
		private bool hasNonComputedReadLink;

		// Token: 0x04000995 RID: 2453
		private ODataStreamReferenceValue mediaResource;

		// Token: 0x04000996 RID: 2454
		private IEnumerable<ODataProperty> properties;

		// Token: 0x04000997 RID: 2455
		private IEnumerable<ODataAction> actions;

		// Token: 0x04000998 RID: 2456
		private IEnumerable<ODataFunction> functions;

		// Token: 0x04000999 RID: 2457
		private ODataFeedAndEntrySerializationInfo serializationInfo;
	}
}
