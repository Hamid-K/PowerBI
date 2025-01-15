using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace System.Web.Http.Dispatcher
{
	// Token: 0x0200007C RID: 124
	public class DefaultAssembliesResolver : IAssembliesResolver
	{
		// Token: 0x0600032D RID: 813 RVA: 0x00009357 File Offset: 0x00007557
		public virtual ICollection<Assembly> GetAssemblies()
		{
			return AppDomain.CurrentDomain.GetAssemblies().ToList<Assembly>();
		}
	}
}
