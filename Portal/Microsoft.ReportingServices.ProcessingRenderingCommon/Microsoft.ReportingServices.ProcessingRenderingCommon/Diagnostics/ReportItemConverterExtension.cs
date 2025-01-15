using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200003F RID: 63
	[Serializable]
	internal class ReportItemConverterExtension : Extension
	{
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00007803 File Offset: 0x00005A03
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x0000780B File Offset: 0x00005A0B
		public string Source
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00007814 File Offset: 0x00005A14
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x0000781C File Offset: 0x00005A1C
		public string Target
		{
			get
			{
				return this.m_target;
			}
			set
			{
				this.m_target = value;
			}
		}

		// Token: 0x040000EE RID: 238
		private string m_target = "";
	}
}
