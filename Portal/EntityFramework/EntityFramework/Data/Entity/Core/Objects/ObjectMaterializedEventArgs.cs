using System;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x02000415 RID: 1045
	public class ObjectMaterializedEventArgs : EventArgs
	{
		// Token: 0x06003205 RID: 12805 RVA: 0x000A0CB9 File Offset: 0x0009EEB9
		public ObjectMaterializedEventArgs(object entity)
		{
			this._entity = entity;
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06003206 RID: 12806 RVA: 0x000A0CC8 File Offset: 0x0009EEC8
		public object Entity
		{
			get
			{
				return this._entity;
			}
		}

		// Token: 0x0400106A RID: 4202
		private readonly object _entity;
	}
}
