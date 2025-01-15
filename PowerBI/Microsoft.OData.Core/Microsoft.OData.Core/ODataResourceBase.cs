using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Evaluation;

namespace Microsoft.OData
{
	// Token: 0x02000080 RID: 128
	[DebuggerDisplay("Id: {Id} TypeName: {TypeName}")]
	public abstract class ODataResourceBase : ODataItem
	{
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x0000BB8B File Offset: 0x00009D8B
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x0000BB98 File Offset: 0x00009D98
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

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x0000BBA8 File Offset: 0x00009DA8
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x0000BBB5 File Offset: 0x00009DB5
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

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x0000BBC5 File Offset: 0x00009DC5
		// (set) Token: 0x0600045A RID: 1114 RVA: 0x0000BBD2 File Offset: 0x00009DD2
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

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x0000BBE2 File Offset: 0x00009DE2
		// (set) Token: 0x0600045C RID: 1116 RVA: 0x0000BBEA File Offset: 0x00009DEA
		public bool IsTransient { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x0000BBF3 File Offset: 0x00009DF3
		// (set) Token: 0x0600045E RID: 1118 RVA: 0x0000BC00 File Offset: 0x00009E00
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

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x0000BC10 File Offset: 0x00009E10
		// (set) Token: 0x06000460 RID: 1120 RVA: 0x0000BC1D File Offset: 0x00009E1D
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

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x0000BC26 File Offset: 0x00009E26
		public IEnumerable<ODataAction> Actions
		{
			get
			{
				return this.MetadataBuilder.GetActions();
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0000BC33 File Offset: 0x00009E33
		public IEnumerable<ODataFunction> Functions
		{
			get
			{
				return this.MetadataBuilder.GetFunctions();
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0000BC40 File Offset: 0x00009E40
		// (set) Token: 0x06000464 RID: 1124 RVA: 0x0000BC53 File Offset: 0x00009E53
		public IEnumerable<ODataProperty> Properties
		{
			get
			{
				return this.MetadataBuilder.GetProperties(this.properties);
			}
			set
			{
				ODataResourceBase.VerifyProperties(value);
				this.properties = value;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x0000BC62 File Offset: 0x00009E62
		// (set) Token: 0x06000466 RID: 1126 RVA: 0x0000BC6A File Offset: 0x00009E6A
		public string TypeName { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x000035C0 File Offset: 0x000017C0
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x000035C8 File Offset: 0x000017C8
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

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x0000BC73 File Offset: 0x00009E73
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x0000BC8F File Offset: 0x00009E8F
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

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x0000BC98 File Offset: 0x00009E98
		internal Uri NonComputedId
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x0000BCA0 File Offset: 0x00009EA0
		internal bool HasNonComputedId
		{
			get
			{
				return this.hasNonComputedId;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x0000BCA8 File Offset: 0x00009EA8
		internal Uri NonComputedEditLink
		{
			get
			{
				return this.editLink;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x0000BCB0 File Offset: 0x00009EB0
		internal bool HasNonComputedEditLink
		{
			get
			{
				return this.hasNonComputedEditLink;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x0000BCB8 File Offset: 0x00009EB8
		internal Uri NonComputedReadLink
		{
			get
			{
				return this.readLink;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x0000BCC0 File Offset: 0x00009EC0
		internal bool HasNonComputedReadLink
		{
			get
			{
				return this.hasNonComputedReadLink;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x0000BCC8 File Offset: 0x00009EC8
		internal string NonComputedETag
		{
			get
			{
				return this.etag;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x0000BCD0 File Offset: 0x00009ED0
		internal bool HasNonComputedETag
		{
			get
			{
				return this.hasNonComputedETag;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x0000BCD8 File Offset: 0x00009ED8
		internal ODataStreamReferenceValue NonComputedMediaResource
		{
			get
			{
				return this.mediaResource;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x0000BCE0 File Offset: 0x00009EE0
		internal IEnumerable<ODataProperty> NonComputedProperties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x0000BCE8 File Offset: 0x00009EE8
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

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x0000BCFF File Offset: 0x00009EFF
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

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x0000BD16 File Offset: 0x00009F16
		// (set) Token: 0x06000478 RID: 1144 RVA: 0x0000BD1E File Offset: 0x00009F1E
		internal virtual ODataResourceSerializationInfo SerializationInfo
		{
			get
			{
				return this.serializationInfo;
			}
			set
			{
				this.serializationInfo = value;
			}
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0000BD27 File Offset: 0x00009F27
		public void AddAction(ODataAction action)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataAction>(action, "action");
			if (!this.actions.Contains(action))
			{
				this.actions.Add(action);
			}
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0000BD4F File Offset: 0x00009F4F
		public void AddFunction(ODataFunction function)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataFunction>(function, "function");
			if (!this.functions.Contains(function))
			{
				this.functions.Add(function);
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0000BD78 File Offset: 0x00009F78
		private static void VerifyProperties(IEnumerable<ODataProperty> properties)
		{
			if (properties != null)
			{
				foreach (ODataProperty odataProperty in properties)
				{
					if (odataProperty.Value is ODataResourceValue)
					{
						throw new ODataException(Strings.ODataResource_PropertyValueCannotBeODataResourceValue(odataProperty.Name));
					}
					ODataCollectionValue odataCollectionValue;
					if ((odataCollectionValue = odataProperty.Value as ODataCollectionValue) != null && odataCollectionValue != null && odataCollectionValue.Items != null)
					{
						if (odataCollectionValue.Items.Any((object t) => t is ODataResourceValue))
						{
							throw new ODataException(Strings.ODataResource_PropertyValueCannotBeODataResourceValue(odataProperty.Name));
						}
					}
				}
			}
		}

		// Token: 0x040001F9 RID: 505
		private ODataResourceMetadataBuilder metadataBuilder;

		// Token: 0x040001FA RID: 506
		private string etag;

		// Token: 0x040001FB RID: 507
		private bool hasNonComputedETag;

		// Token: 0x040001FC RID: 508
		private Uri id;

		// Token: 0x040001FD RID: 509
		private bool hasNonComputedId;

		// Token: 0x040001FE RID: 510
		private Uri editLink;

		// Token: 0x040001FF RID: 511
		private bool hasNonComputedEditLink;

		// Token: 0x04000200 RID: 512
		private Uri readLink;

		// Token: 0x04000201 RID: 513
		private bool hasNonComputedReadLink;

		// Token: 0x04000202 RID: 514
		private ODataStreamReferenceValue mediaResource;

		// Token: 0x04000203 RID: 515
		private IEnumerable<ODataProperty> properties;

		// Token: 0x04000204 RID: 516
		private List<ODataAction> actions = new List<ODataAction>();

		// Token: 0x04000205 RID: 517
		private List<ODataFunction> functions = new List<ODataFunction>();

		// Token: 0x04000206 RID: 518
		private ODataResourceSerializationInfo serializationInfo;
	}
}
