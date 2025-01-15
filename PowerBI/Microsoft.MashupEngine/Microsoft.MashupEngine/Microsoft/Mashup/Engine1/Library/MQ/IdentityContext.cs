using System;
using System.Reflection;

namespace Microsoft.Mashup.Engine1.Library.MQ
{
	// Token: 0x02000947 RID: 2375
	public class IdentityContext
	{
		// Token: 0x060043A1 RID: 17313 RVA: 0x000E4546 File Offset: 0x000E2746
		public IdentityContext(object value)
		{
			this.RealValue = value;
		}

		// Token: 0x17001586 RID: 5510
		// (get) Token: 0x060043A2 RID: 17314 RVA: 0x000E4555 File Offset: 0x000E2755
		// (set) Token: 0x060043A3 RID: 17315 RVA: 0x000E455D File Offset: 0x000E275D
		public object RealValue { get; protected set; }

		// Token: 0x17001587 RID: 5511
		// (get) Token: 0x060043A4 RID: 17316 RVA: 0x000E4566 File Offset: 0x000E2766
		// (set) Token: 0x060043A5 RID: 17317 RVA: 0x000E456D File Offset: 0x000E276D
		private static Type RealType { get; set; } = AssemblyLoader.HisConnectors.GetType("Microsoft.HostIntegration.MqClient.IdentityContext");

		// Token: 0x17001588 RID: 5512
		// (get) Token: 0x060043A6 RID: 17318 RVA: 0x000E4575 File Offset: 0x000E2775
		// (set) Token: 0x060043A7 RID: 17319 RVA: 0x000E458D File Offset: 0x000E278D
		public string UserId
		{
			get
			{
				return (string)IdentityContext.UserIdInfo.GetValue(this.RealValue, null);
			}
			set
			{
				IdentityContext.UserIdInfo.SetValue(this.RealValue, value, null);
			}
		}

		// Token: 0x17001589 RID: 5513
		// (get) Token: 0x060043A8 RID: 17320 RVA: 0x000E45A1 File Offset: 0x000E27A1
		// (set) Token: 0x060043A9 RID: 17321 RVA: 0x000E45B9 File Offset: 0x000E27B9
		public byte[] AccountToken
		{
			get
			{
				return (byte[])IdentityContext.AccountTokenInfo.GetValue(this.RealValue, null);
			}
			set
			{
				IdentityContext.AccountTokenInfo.SetValue(this.RealValue, value, null);
			}
		}

		// Token: 0x040023AC RID: 9132
		private static readonly PropertyInfo UserIdInfo = IdentityContext.RealType.GetProperty("UserId");

		// Token: 0x040023AD RID: 9133
		private static readonly PropertyInfo AccountTokenInfo = IdentityContext.RealType.GetProperty("AccountToken");
	}
}
