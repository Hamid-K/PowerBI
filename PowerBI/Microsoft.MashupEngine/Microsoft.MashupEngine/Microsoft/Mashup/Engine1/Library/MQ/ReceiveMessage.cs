using System;
using System.Reflection;

namespace Microsoft.Mashup.Engine1.Library.MQ
{
	// Token: 0x02000951 RID: 2385
	public sealed class ReceiveMessage : Message
	{
		// Token: 0x0600442A RID: 17450 RVA: 0x000E5980 File Offset: 0x000E3B80
		public ReceiveMessage(object value)
		{
			base.RealValue = value;
		}

		// Token: 0x170015C4 RID: 5572
		// (get) Token: 0x0600442B RID: 17451 RVA: 0x000E598F File Offset: 0x000E3B8F
		// (set) Token: 0x0600442C RID: 17452 RVA: 0x000E5996 File Offset: 0x000E3B96
		private static Type RealType { get; set; } = AssemblyLoader.HisConnectors.GetType("Microsoft.HostIntegration.MqClient.ReceiveMessage");

		// Token: 0x170015C5 RID: 5573
		// (get) Token: 0x0600442D RID: 17453 RVA: 0x000E599E File Offset: 0x000E3B9E
		// (set) Token: 0x0600442E RID: 17454 RVA: 0x000E59B6 File Offset: 0x000E3BB6
		public int OriginalLength
		{
			get
			{
				return (int)ReceiveMessage.OriginalLengthInfo.GetValue(base.RealValue, null);
			}
			set
			{
				ReceiveMessage.OriginalLengthInfo.SetValue(base.RealValue, value, null);
			}
		}

		// Token: 0x170015C6 RID: 5574
		// (get) Token: 0x0600442F RID: 17455 RVA: 0x000E59CF File Offset: 0x000E3BCF
		public byte[] Token
		{
			get
			{
				return (byte[])ReceiveMessage.TokenInfo.GetValue(base.RealValue, null);
			}
		}

		// Token: 0x04002418 RID: 9240
		public static readonly PropertyInfo OriginalLengthInfo = ReceiveMessage.RealType.GetProperty("OriginalLength");

		// Token: 0x04002419 RID: 9241
		public static readonly PropertyInfo TokenInfo = ReceiveMessage.RealType.GetProperty("Token");

		// Token: 0x0400241A RID: 9242
		public static ReceiveMessage EmptyMessage = new ReceiveMessage(null);
	}
}
