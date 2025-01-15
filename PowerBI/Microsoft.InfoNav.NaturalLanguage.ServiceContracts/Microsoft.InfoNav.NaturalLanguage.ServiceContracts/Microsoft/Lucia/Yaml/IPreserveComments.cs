using System;
using System.Collections.Generic;

namespace Microsoft.Lucia.Yaml
{
	// Token: 0x0200001D RID: 29
	public interface IPreserveComments
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000068 RID: 104
		// (set) Token: 0x06000069 RID: 105
		List<string> Comments { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600006A RID: 106
		CommentStyle CommentStyle { get; }
	}
}
