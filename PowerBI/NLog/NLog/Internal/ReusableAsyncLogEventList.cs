using System;
using System.Collections.Generic;
using NLog.Common;

namespace NLog.Internal
{
	// Token: 0x02000138 RID: 312
	internal class ReusableAsyncLogEventList : ReusableObjectCreator<IList<AsyncLogEventInfo>>
	{
		// Token: 0x06000F87 RID: 3975 RVA: 0x00027912 File Offset: 0x00025B12
		public ReusableAsyncLogEventList(int capacity)
			: base(new List<AsyncLogEventInfo>(capacity), delegate(IList<AsyncLogEventInfo> l)
			{
				l.Clear();
			})
		{
		}
	}
}
