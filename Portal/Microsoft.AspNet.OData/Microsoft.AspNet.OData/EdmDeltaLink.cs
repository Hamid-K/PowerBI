using System;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000028 RID: 40
	[NonValidatingParameterBinding]
	public class EdmDeltaLink : EdmEntityObject, IEdmDeltaLink, IEdmDeltaLinkBase, IEdmChangedObject, IEdmStructuredObject, IEdmObject
	{
		// Token: 0x0600010D RID: 269 RVA: 0x000060BA File Offset: 0x000042BA
		public EdmDeltaLink(IEdmEntityType entityType)
			: this(entityType, false)
		{
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000060C4 File Offset: 0x000042C4
		public EdmDeltaLink(IEdmEntityTypeReference entityTypeReference)
			: this(entityTypeReference.EntityDefinition(), entityTypeReference.IsNullable)
		{
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000060D8 File Offset: 0x000042D8
		public EdmDeltaLink(IEdmEntityType entityType, bool isNullable)
			: base(entityType, isNullable)
		{
			this._edmType = new EdmDeltaType(entityType, EdmDeltaEntityKind.LinkEntry);
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000110 RID: 272 RVA: 0x000060EF File Offset: 0x000042EF
		// (set) Token: 0x06000111 RID: 273 RVA: 0x000060F7 File Offset: 0x000042F7
		public Uri Source
		{
			get
			{
				return this._source;
			}
			set
			{
				this._source = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00006100 File Offset: 0x00004300
		// (set) Token: 0x06000113 RID: 275 RVA: 0x00006108 File Offset: 0x00004308
		public Uri Target
		{
			get
			{
				return this._target;
			}
			set
			{
				this._target = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00006111 File Offset: 0x00004311
		// (set) Token: 0x06000115 RID: 277 RVA: 0x00006119 File Offset: 0x00004319
		public string Relationship
		{
			get
			{
				return this._relationship;
			}
			set
			{
				this._relationship = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00006122 File Offset: 0x00004322
		public EdmDeltaEntityKind DeltaKind
		{
			get
			{
				return this._edmType.DeltaKind;
			}
		}

		// Token: 0x04000040 RID: 64
		private Uri _source;

		// Token: 0x04000041 RID: 65
		private Uri _target;

		// Token: 0x04000042 RID: 66
		private string _relationship;

		// Token: 0x04000043 RID: 67
		private EdmDeltaType _edmType;
	}
}
