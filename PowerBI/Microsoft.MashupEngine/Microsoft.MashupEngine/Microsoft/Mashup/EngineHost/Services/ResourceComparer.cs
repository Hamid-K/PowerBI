using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.EngineHost.Services
{
	// Token: 0x02001B30 RID: 6960
	public class ResourceComparer : IEqualityComparer<IResource>
	{
		// Token: 0x0600AE48 RID: 44616 RVA: 0x001D98CF File Offset: 0x001D7ACF
		bool IEqualityComparer<IResource>.Equals(IResource x, IResource y)
		{
			return x.Kind == y.Kind && x.Path == y.Path;
		}

		// Token: 0x0600AE49 RID: 44617 RVA: 0x001D98F7 File Offset: 0x001D7AF7
		int IEqualityComparer<IResource>.GetHashCode(IResource obj)
		{
			return obj.Kind.GetHashCode() * 17 + obj.Path.GetHashCode();
		}

		// Token: 0x040059E0 RID: 23008
		public static readonly IEqualityComparer<IResource> Instance = new ResourceComparer();
	}
}
