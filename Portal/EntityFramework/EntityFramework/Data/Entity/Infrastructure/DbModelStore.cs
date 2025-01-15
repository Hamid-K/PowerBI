using System;
using System.Xml.Linq;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200022C RID: 556
	public abstract class DbModelStore
	{
		// Token: 0x06001D3C RID: 7484
		public abstract DbCompiledModel TryLoad(Type contextType);

		// Token: 0x06001D3D RID: 7485
		public abstract XDocument TryGetEdmx(Type contextType);

		// Token: 0x06001D3E RID: 7486
		public abstract void Save(Type contextType, DbModel model);

		// Token: 0x06001D3F RID: 7487 RVA: 0x00053286 File Offset: 0x00051486
		protected virtual string GetDefaultSchema(Type contextType)
		{
			return "dbo";
		}
	}
}
