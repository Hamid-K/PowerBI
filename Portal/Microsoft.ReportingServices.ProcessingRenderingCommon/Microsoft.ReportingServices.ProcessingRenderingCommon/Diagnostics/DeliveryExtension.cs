using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000032 RID: 50
	[Serializable]
	internal sealed class DeliveryExtension : Extension
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000172 RID: 370 RVA: 0x000066C2 File Offset: 0x000048C2
		// (set) Token: 0x06000173 RID: 371 RVA: 0x000066CA File Offset: 0x000048CA
		public int MaxRetries
		{
			get
			{
				return this.m_maxRetries;
			}
			set
			{
				this.m_maxRetries = value;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000174 RID: 372 RVA: 0x000066D3 File Offset: 0x000048D3
		// (set) Token: 0x06000175 RID: 373 RVA: 0x000066DB File Offset: 0x000048DB
		public int SecondsBeforeRetry
		{
			get
			{
				return this.m_secondsBeforeRetry;
			}
			set
			{
				this.m_secondsBeforeRetry = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000176 RID: 374 RVA: 0x000066E4 File Offset: 0x000048E4
		// (set) Token: 0x06000177 RID: 375 RVA: 0x000066EC File Offset: 0x000048EC
		public bool DefaultDeliveryExtension
		{
			get
			{
				return this.m_defaultDeliveryExtension;
			}
			set
			{
				this.m_defaultDeliveryExtension = value;
			}
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000066F5 File Offset: 0x000048F5
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00006700 File Offset: 0x00004900
		public override bool Equals(object extension)
		{
			bool flag = true;
			if (!(extension is DeliveryExtension))
			{
				return false;
			}
			DeliveryExtension deliveryExtension = (DeliveryExtension)extension;
			if (deliveryExtension.MaxRetries != this.MaxRetries || deliveryExtension.SecondsBeforeRetry != this.SecondsBeforeRetry)
			{
				flag = false;
			}
			if (!flag)
			{
				return flag;
			}
			return base.Equals(deliveryExtension);
		}

		// Token: 0x040000B3 RID: 179
		private int m_maxRetries;

		// Token: 0x040000B4 RID: 180
		private int m_secondsBeforeRetry = 300;

		// Token: 0x040000B5 RID: 181
		private bool m_defaultDeliveryExtension;
	}
}
