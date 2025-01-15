using System;
using System.Collections.Generic;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001B9 RID: 441
	public abstract class PhrasingProperties
	{
		// Token: 0x06000920 RID: 2336
		public abstract void Accept(IPhrasingVisitor visitor);

		// Token: 0x06000921 RID: 2337
		public abstract T Accept<T>(IPhrasingVisitor<T> visitor);

		// Token: 0x06000922 RID: 2338
		public abstract T Accept<T, TArg>(IPhrasingVisitor<T, TArg> visitor, TArg arg);

		// Token: 0x06000923 RID: 2339
		internal abstract IEnumerable<RoleReference> GetRoleReferences();
	}
}
