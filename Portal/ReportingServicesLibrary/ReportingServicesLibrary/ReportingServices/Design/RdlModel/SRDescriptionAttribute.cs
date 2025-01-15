using System;
using System.ComponentModel;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x0200040B RID: 1035
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	internal class SRDescriptionAttribute : DescriptionAttribute
	{
		// Token: 0x060020EF RID: 8431 RVA: 0x0007FFC5 File Offset: 0x0007E1C5
		public SRDescriptionAttribute(string key)
		{
			this.m_key = key;
			this.m_initialized = false;
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x060020F0 RID: 8432 RVA: 0x0007FFDB File Offset: 0x0007E1DB
		public override string Description
		{
			get
			{
				if (!this.m_initialized)
				{
					base.DescriptionValue = SRDescription.Keys.GetString(this.m_key);
					this.m_initialized = true;
				}
				return base.Description;
			}
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x060020F1 RID: 8433 RVA: 0x00080003 File Offset: 0x0007E203
		public override object TypeId
		{
			get
			{
				return typeof(DescriptionAttribute);
			}
		}

		// Token: 0x04000E5D RID: 3677
		private string m_key;

		// Token: 0x04000E5E RID: 3678
		private bool m_initialized;
	}
}
