using System;
using Microsoft.OData;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000029 RID: 41
	[NonValidatingParameterBinding]
	public class EdmDeltaDeletedEntityObject : EdmEntityObject, IEdmDeltaDeletedEntityObject, IEdmChangedObject, IEdmStructuredObject, IEdmObject
	{
		// Token: 0x06000117 RID: 279 RVA: 0x0000612F File Offset: 0x0000432F
		public EdmDeltaDeletedEntityObject(IEdmEntityType entityType)
			: this(entityType, false)
		{
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00006139 File Offset: 0x00004339
		public EdmDeltaDeletedEntityObject(IEdmEntityTypeReference entityTypeReference)
			: this(entityTypeReference.EntityDefinition(), entityTypeReference.IsNullable)
		{
		}

		// Token: 0x06000119 RID: 281 RVA: 0x0000614D File Offset: 0x0000434D
		public EdmDeltaDeletedEntityObject(IEdmEntityType entityType, bool isNullable)
			: base(entityType, isNullable)
		{
			this._edmType = new EdmDeltaType(entityType, EdmDeltaEntityKind.DeletedEntry);
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00006164 File Offset: 0x00004364
		// (set) Token: 0x0600011B RID: 283 RVA: 0x0000616C File Offset: 0x0000436C
		public string Id
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00006175 File Offset: 0x00004375
		// (set) Token: 0x0600011D RID: 285 RVA: 0x0000617D File Offset: 0x0000437D
		public DeltaDeletedEntryReason Reason
		{
			get
			{
				return this._reason;
			}
			set
			{
				this._reason = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00006186 File Offset: 0x00004386
		public EdmDeltaEntityKind DeltaKind
		{
			get
			{
				return this._edmType.DeltaKind;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00006193 File Offset: 0x00004393
		// (set) Token: 0x06000120 RID: 288 RVA: 0x0000619B File Offset: 0x0000439B
		public IEdmNavigationSource NavigationSource
		{
			get
			{
				return this._navigationSource;
			}
			set
			{
				this._navigationSource = value;
			}
		}

		// Token: 0x04000044 RID: 68
		private string _id;

		// Token: 0x04000045 RID: 69
		private DeltaDeletedEntryReason _reason;

		// Token: 0x04000046 RID: 70
		private EdmDeltaType _edmType;

		// Token: 0x04000047 RID: 71
		private IEdmNavigationSource _navigationSource;
	}
}
