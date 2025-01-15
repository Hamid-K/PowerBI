using System;

namespace System.Data.Entity.Infrastructure.Design
{
	// Token: 0x0200029E RID: 670
	public abstract class HandlerBase : MarshalByRefObject
	{
		// Token: 0x0600216A RID: 8554 RVA: 0x0005DCEC File Offset: 0x0005BEEC
		public virtual bool ImplementsContract(string interfaceName)
		{
			Type type;
			try
			{
				type = Type.GetType(interfaceName, true);
			}
			catch
			{
				return false;
			}
			return type.IsAssignableFrom(base.GetType());
		}
	}
}
