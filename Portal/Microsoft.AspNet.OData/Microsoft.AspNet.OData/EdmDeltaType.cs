using System;
using Microsoft.AspNet.OData.Common;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData
{
	// Token: 0x0200002C RID: 44
	internal class EdmDeltaType : IEdmType, IEdmElement
	{
		// Token: 0x0600012B RID: 299 RVA: 0x00006219 File Offset: 0x00004419
		internal EdmDeltaType(IEdmEntityType entityType, EdmDeltaEntityKind deltaKind)
		{
			if (entityType == null)
			{
				throw Error.ArgumentNull("entityType");
			}
			this._entityType = entityType;
			this._deltaKind = deltaKind;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600012C RID: 300 RVA: 0x0000623D File Offset: 0x0000443D
		public EdmTypeKind TypeKind
		{
			get
			{
				return EdmTypeKind.Entity;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00006240 File Offset: 0x00004440
		public IEdmEntityType EntityType
		{
			get
			{
				return this._entityType;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00006248 File Offset: 0x00004448
		public EdmDeltaEntityKind DeltaKind
		{
			get
			{
				return this._deltaKind;
			}
		}

		// Token: 0x04000052 RID: 82
		private IEdmEntityType _entityType;

		// Token: 0x04000053 RID: 83
		private EdmDeltaEntityKind _deltaKind;
	}
}
