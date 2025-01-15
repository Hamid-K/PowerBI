using System;
using System.Reflection;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004E7 RID: 1255
	internal class ECFParameter
	{
		// Token: 0x0600262C RID: 9772 RVA: 0x0008774B File Offset: 0x0008594B
		internal ECFParameter(ParameterInfo parameter, bool removeParam)
		{
			this.Parameter = parameter;
			this.Remove = removeParam;
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x0600262D RID: 9773 RVA: 0x00087761 File Offset: 0x00085961
		// (set) Token: 0x0600262E RID: 9774 RVA: 0x00087769 File Offset: 0x00085969
		public ParameterInfo Parameter { get; private set; }

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x0600262F RID: 9775 RVA: 0x00087772 File Offset: 0x00085972
		// (set) Token: 0x06002630 RID: 9776 RVA: 0x0008777A File Offset: 0x0008597A
		public bool Remove { get; set; }
	}
}
