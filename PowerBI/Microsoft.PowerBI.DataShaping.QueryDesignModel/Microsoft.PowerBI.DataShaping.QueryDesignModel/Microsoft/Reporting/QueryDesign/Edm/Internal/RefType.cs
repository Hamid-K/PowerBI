using System;
using Microsoft.Data.Metadata.Edm;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x0200024D RID: 589
	internal sealed class RefType : EdmType
	{
		// Token: 0x060019E0 RID: 6624 RVA: 0x000475E8 File Offset: 0x000457E8
		internal RefType(RefType refType, EntityType elementType)
		{
			this._refType = ArgumentValidation.CheckNotNull<RefType>(refType, "refType");
			this._elementType = ArgumentValidation.CheckNotNull<EntityType>(elementType, "elementType");
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x060019E1 RID: 6625 RVA: 0x00047612 File Offset: 0x00045812
		public EntityType ElementType
		{
			get
			{
				return this._elementType;
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x060019E2 RID: 6626 RVA: 0x0004761A File Offset: 0x0004581A
		internal override EdmType InternalEdmType
		{
			get
			{
				return this._refType;
			}
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x00047624 File Offset: 0x00045824
		internal static RefType Create(RefType refType, IEdmItemLookup edmItemLookup)
		{
			EntityType entityType = refType.ElementType as EntityType;
			EntityType entityType2 = edmItemLookup.LookupEdmType(entityType) as EntityType;
			return new RefType(refType, entityType2);
		}

		// Token: 0x04000E6C RID: 3692
		private readonly RefType _refType;

		// Token: 0x04000E6D RID: 3693
		private readonly EntityType _elementType;
	}
}
