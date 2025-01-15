using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020015F5 RID: 5621
	internal class ResourceEqualityComparer : IEqualityComparer<IResource>
	{
		// Token: 0x06008D6B RID: 36203 RVA: 0x000020FD File Offset: 0x000002FD
		private ResourceEqualityComparer()
		{
		}

		// Token: 0x06008D6C RID: 36204 RVA: 0x001D98CF File Offset: 0x001D7ACF
		bool IEqualityComparer<IResource>.Equals(IResource x, IResource y)
		{
			return x.Kind == y.Kind && x.Path == y.Path;
		}

		// Token: 0x06008D6D RID: 36205 RVA: 0x001D98F7 File Offset: 0x001D7AF7
		int IEqualityComparer<IResource>.GetHashCode(IResource obj)
		{
			return obj.Kind.GetHashCode() * 17 + obj.Path.GetHashCode();
		}

		// Token: 0x04004CFD RID: 19709
		public static readonly IEqualityComparer<IResource> Instance = new ResourceEqualityComparer();
	}
}
