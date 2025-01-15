using System;
using System.Reflection;

namespace Microsoft.Mashup.Engine1.Library.MQ
{
	// Token: 0x02000955 RID: 2389
	public sealed class SendMessage : Message
	{
		// Token: 0x06004458 RID: 17496 RVA: 0x000E5F64 File Offset: 0x000E4164
		static SendMessage()
		{
			Type type = AssemblyLoader.HisConnectors.GetType("Microsoft.HostIntegration.MqClient.MessageType");
			SendMessage.Constructor1 = SendMessage.RealType.GetConstructor(new Type[] { type });
			SendMessage.OptionsInfo = SendMessage.RealType.GetProperty("Options");
		}

		// Token: 0x06004459 RID: 17497 RVA: 0x000E5FD6 File Offset: 0x000E41D6
		public SendMessage()
		{
			base.RealValue = SendMessage.Constructor.Invoke(null);
		}

		// Token: 0x0600445A RID: 17498 RVA: 0x000E5FEF File Offset: 0x000E41EF
		public SendMessage(MessageType messageType)
		{
			base.RealValue = SendMessage.Constructor1.Invoke(new object[] { messageType });
		}

		// Token: 0x170015D9 RID: 5593
		// (get) Token: 0x0600445B RID: 17499 RVA: 0x000E6016 File Offset: 0x000E4216
		// (set) Token: 0x0600445C RID: 17500 RVA: 0x000E601D File Offset: 0x000E421D
		public static Type RealType { get; private set; } = AssemblyLoader.HisConnectors.GetType("Microsoft.HostIntegration.MqClient.SendMessage");

		// Token: 0x170015DA RID: 5594
		// (get) Token: 0x0600445D RID: 17501 RVA: 0x000E6025 File Offset: 0x000E4225
		// (set) Token: 0x0600445E RID: 17502 RVA: 0x000E603D File Offset: 0x000E423D
		public SendOptions Options
		{
			get
			{
				return (SendOptions)SendMessage.OptionsInfo.GetValue(base.RealValue, null);
			}
			set
			{
				SendMessage.OptionsInfo.SetValue(base.RealValue, value.RealValue, null);
			}
		}

		// Token: 0x04002432 RID: 9266
		private static readonly ConstructorInfo Constructor = SendMessage.RealType.GetConstructor(Type.EmptyTypes);

		// Token: 0x04002433 RID: 9267
		private static readonly ConstructorInfo Constructor1;

		// Token: 0x04002434 RID: 9268
		private static readonly PropertyInfo OptionsInfo;
	}
}
