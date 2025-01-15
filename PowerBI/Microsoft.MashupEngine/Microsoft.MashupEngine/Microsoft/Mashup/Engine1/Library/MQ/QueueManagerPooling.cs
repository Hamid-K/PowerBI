using System;
using System.Reflection;

namespace Microsoft.Mashup.Engine1.Library.MQ
{
	// Token: 0x02000950 RID: 2384
	public class QueueManagerPooling
	{
		// Token: 0x06004422 RID: 17442 RVA: 0x000E58CC File Offset: 0x000E3ACC
		public QueueManagerPooling(object value)
		{
			this.RealValue = value;
		}

		// Token: 0x170015C1 RID: 5569
		// (get) Token: 0x06004423 RID: 17443 RVA: 0x000E58DB File Offset: 0x000E3ADB
		// (set) Token: 0x06004424 RID: 17444 RVA: 0x000E58E3 File Offset: 0x000E3AE3
		public object RealValue { get; protected set; }

		// Token: 0x170015C2 RID: 5570
		// (get) Token: 0x06004425 RID: 17445 RVA: 0x000E58EC File Offset: 0x000E3AEC
		// (set) Token: 0x06004426 RID: 17446 RVA: 0x000E58F3 File Offset: 0x000E3AF3
		public static Type RealType { get; protected set; } = AssemblyLoader.HisConnectors.GetType("Microsoft.HostIntegration.MqClient.QueueManagerPooling");

		// Token: 0x170015C3 RID: 5571
		// (get) Token: 0x06004427 RID: 17447 RVA: 0x000E58FB File Offset: 0x000E3AFB
		// (set) Token: 0x06004428 RID: 17448 RVA: 0x000E5913 File Offset: 0x000E3B13
		public bool Pool
		{
			get
			{
				return (bool)QueueManagerPooling.PoolInfo.GetValue(this.RealValue, null);
			}
			set
			{
				QueueManagerPooling.PoolInfo.SetValue(this.RealValue, value, null);
			}
		}

		// Token: 0x04002415 RID: 9237
		private static readonly PropertyInfo PoolInfo = QueueManagerPooling.RealType.GetProperty("Pool");
	}
}
