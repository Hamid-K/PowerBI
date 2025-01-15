using System;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x02000546 RID: 1350
	public class NilpotentTaggedActivityFactory : ITaggedActivityFactory
	{
		// Token: 0x060028F8 RID: 10488 RVA: 0x0009289B File Offset: 0x00090A9B
		public NilpotentTaggedActivityFactory()
		{
			this.m_emptyActivity = new EmptyDisposable();
		}

		// Token: 0x060028F9 RID: 10489 RVA: 0x000928AE File Offset: 0x00090AAE
		public IDisposable CreateTaggedActivity(ActivityTag tag)
		{
			return this.m_emptyActivity;
		}

		// Token: 0x04000E9F RID: 3743
		private EmptyDisposable m_emptyActivity;
	}
}
