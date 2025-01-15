using System;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData
{
	// Token: 0x0200002A RID: 42
	[NonValidatingParameterBinding]
	public class EdmDeltaDeletedLink : EdmEntityObject, IEdmDeltaDeletedLink, IEdmDeltaLinkBase, IEdmChangedObject, IEdmStructuredObject, IEdmObject
	{
		// Token: 0x06000121 RID: 289 RVA: 0x000061A4 File Offset: 0x000043A4
		public EdmDeltaDeletedLink(IEdmEntityType entityType)
			: this(entityType, false)
		{
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000061AE File Offset: 0x000043AE
		public EdmDeltaDeletedLink(IEdmEntityTypeReference entityTypeReference)
			: this(entityTypeReference.EntityDefinition(), entityTypeReference.IsNullable)
		{
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000061C2 File Offset: 0x000043C2
		public EdmDeltaDeletedLink(IEdmEntityType entityType, bool isNullable)
			: base(entityType, isNullable)
		{
			this._edmType = new EdmDeltaType(entityType, EdmDeltaEntityKind.DeletedLinkEntry);
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000124 RID: 292 RVA: 0x000061D9 File Offset: 0x000043D9
		// (set) Token: 0x06000125 RID: 293 RVA: 0x000061E1 File Offset: 0x000043E1
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

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000126 RID: 294 RVA: 0x000061EA File Offset: 0x000043EA
		// (set) Token: 0x06000127 RID: 295 RVA: 0x000061F2 File Offset: 0x000043F2
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

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000128 RID: 296 RVA: 0x000061FB File Offset: 0x000043FB
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00006203 File Offset: 0x00004403
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

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600012A RID: 298 RVA: 0x0000620C File Offset: 0x0000440C
		public EdmDeltaEntityKind DeltaKind
		{
			get
			{
				return this._edmType.DeltaKind;
			}
		}

		// Token: 0x04000048 RID: 72
		private Uri _source;

		// Token: 0x04000049 RID: 73
		private Uri _target;

		// Token: 0x0400004A RID: 74
		private string _relationship;

		// Token: 0x0400004B RID: 75
		private EdmDeltaType _edmType;
	}
}
