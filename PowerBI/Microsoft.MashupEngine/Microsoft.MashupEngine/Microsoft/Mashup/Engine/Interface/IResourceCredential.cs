using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200008E RID: 142
	public interface IResourceCredential : IEquatable<IResourceCredential>
	{
		// Token: 0x06000212 RID: 530
		IEnumerable<string> GetCacheParts();
	}
}
