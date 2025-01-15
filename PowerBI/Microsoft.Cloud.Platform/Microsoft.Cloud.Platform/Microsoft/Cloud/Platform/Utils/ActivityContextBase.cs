using System;
using Newtonsoft.Json;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001A6 RID: 422
	public abstract class ActivityContextBase
	{
		// Token: 0x06000AC6 RID: 2758 RVA: 0x0002599D File Offset: 0x00023B9D
		public override string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}
