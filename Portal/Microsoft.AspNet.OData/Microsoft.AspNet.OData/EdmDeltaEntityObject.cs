using System;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000027 RID: 39
	[NonValidatingParameterBinding]
	public class EdmDeltaEntityObject : EdmEntityObject, IEdmChangedObject, IEdmStructuredObject, IEdmObject
	{
		// Token: 0x06000107 RID: 263 RVA: 0x00006067 File Offset: 0x00004267
		public EdmDeltaEntityObject(IEdmEntityType entityType)
			: this(entityType, false)
		{
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00006071 File Offset: 0x00004271
		public EdmDeltaEntityObject(IEdmEntityTypeReference entityTypeReference)
			: this(entityTypeReference.EntityDefinition(), entityTypeReference.IsNullable)
		{
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00006085 File Offset: 0x00004285
		public EdmDeltaEntityObject(IEdmEntityType entityType, bool isNullable)
			: base(entityType, isNullable)
		{
			this._edmType = new EdmDeltaType(entityType, EdmDeltaEntityKind.Entry);
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600010A RID: 266 RVA: 0x0000609C File Offset: 0x0000429C
		public EdmDeltaEntityKind DeltaKind
		{
			get
			{
				return this._edmType.DeltaKind;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600010B RID: 267 RVA: 0x000060A9 File Offset: 0x000042A9
		// (set) Token: 0x0600010C RID: 268 RVA: 0x000060B1 File Offset: 0x000042B1
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

		// Token: 0x0400003E RID: 62
		private EdmDeltaType _edmType;

		// Token: 0x0400003F RID: 63
		private IEdmNavigationSource _navigationSource;
	}
}
