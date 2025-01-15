using System;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004E0 RID: 1248
	public class ServiceIdentification
	{
		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x060025DC RID: 9692 RVA: 0x00086950 File Offset: 0x00084B50
		// (set) Token: 0x060025DD RID: 9693 RVA: 0x00086958 File Offset: 0x00084B58
		public string Name { get; private set; }

		// Token: 0x060025DE RID: 9694 RVA: 0x00086961 File Offset: 0x00084B61
		public ServiceIdentification(string name)
		{
			this.Name = name;
		}
	}
}
