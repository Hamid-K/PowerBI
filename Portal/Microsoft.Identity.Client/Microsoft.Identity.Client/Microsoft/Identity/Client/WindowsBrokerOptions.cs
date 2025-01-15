using System;
using System.ComponentModel;

namespace Microsoft.Identity.Client
{
	// Token: 0x0200012B RID: 299
	public class WindowsBrokerOptions
	{
		// Token: 0x06000EC0 RID: 3776 RVA: 0x000386BF File Offset: 0x000368BF
		public WindowsBrokerOptions()
		{
			WindowsBrokerOptions.ValidatePlatformAvailability();
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x000386CC File Offset: 0x000368CC
		internal static WindowsBrokerOptions CreateDefault()
		{
			return new WindowsBrokerOptions();
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000EC2 RID: 3778 RVA: 0x000386D3 File Offset: 0x000368D3
		// (set) Token: 0x06000EC3 RID: 3779 RVA: 0x000386DB File Offset: 0x000368DB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool MsaPassthrough { get; set; }

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000EC4 RID: 3780 RVA: 0x000386E4 File Offset: 0x000368E4
		// (set) Token: 0x06000EC5 RID: 3781 RVA: 0x000386EC File Offset: 0x000368EC
		public bool ListWindowsWorkAndSchoolAccounts { get; set; }

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000EC6 RID: 3782 RVA: 0x000386F5 File Offset: 0x000368F5
		// (set) Token: 0x06000EC7 RID: 3783 RVA: 0x000386FD File Offset: 0x000368FD
		public string HeaderText { get; set; }

		// Token: 0x06000EC8 RID: 3784 RVA: 0x00038706 File Offset: 0x00036906
		internal static void ValidatePlatformAvailability()
		{
		}
	}
}
