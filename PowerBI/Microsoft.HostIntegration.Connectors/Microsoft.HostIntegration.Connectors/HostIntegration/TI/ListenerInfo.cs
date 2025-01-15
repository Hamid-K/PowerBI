using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x02000730 RID: 1840
	public class ListenerInfo
	{
		// Token: 0x060039AA RID: 14762 RVA: 0x000C62B8 File Offset: 0x000C44B8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(this.ListenerName);
			if (string.IsNullOrEmpty(this.ListenerLuName))
			{
				stringBuilder.AppendFormat(", State: {0}, Action: {1}", this.ListenerStatus.ToString(), this.PerformAction.ToString());
			}
			else
			{
				stringBuilder.AppendFormat(", LuName: {0}, State: {1}, Action: {2}", this.ListenerLuName, this.ListenerStatus.ToString(), this.PerformAction.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040022EF RID: 8943
		public string ListenerName;

		// Token: 0x040022F0 RID: 8944
		public string ListenerLuName;

		// Token: 0x040022F1 RID: 8945
		public ListenerAction PerformAction;

		// Token: 0x040022F2 RID: 8946
		public StatusOfListener ListenerStatus;

		// Token: 0x040022F3 RID: 8947
		public List<string> ListenList;

		// Token: 0x040022F4 RID: 8948
		public object IAdmin;

		// Token: 0x040022F5 RID: 8949
		public object HIPProxy;

		// Token: 0x040022F6 RID: 8950
		public object EventLogging;
	}
}
