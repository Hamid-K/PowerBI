using System;
using System.Reflection;

namespace Microsoft.Mashup.Engine1.Library.MQ
{
	// Token: 0x02000956 RID: 2390
	public class SendOptions
	{
		// Token: 0x06004460 RID: 17504 RVA: 0x000E6094 File Offset: 0x000E4294
		public SendOptions()
		{
			this.RealValue = SendOptions.Constructor.Invoke(AssemblyLoader.EmptyArray);
		}

		// Token: 0x170015DB RID: 5595
		// (get) Token: 0x06004461 RID: 17505 RVA: 0x000E60B1 File Offset: 0x000E42B1
		// (set) Token: 0x06004462 RID: 17506 RVA: 0x000E60B9 File Offset: 0x000E42B9
		public object RealValue { get; protected set; }

		// Token: 0x170015DC RID: 5596
		// (get) Token: 0x06004463 RID: 17507 RVA: 0x000E60C2 File Offset: 0x000E42C2
		// (set) Token: 0x06004464 RID: 17508 RVA: 0x000E60C9 File Offset: 0x000E42C9
		public static Type RealType { get; protected set; } = AssemblyLoader.HisConnectors.GetType("Microsoft.HostIntegration.MqClient.SendOptions");

		// Token: 0x170015DD RID: 5597
		// (get) Token: 0x06004465 RID: 17509 RVA: 0x000E60D1 File Offset: 0x000E42D1
		// (set) Token: 0x06004466 RID: 17510 RVA: 0x000E60E9 File Offset: 0x000E42E9
		public SendOption Options
		{
			get
			{
				return (SendOption)SendOptions.OptionsInfo.GetValue(this.RealValue, null);
			}
			internal set
			{
				SendOptions.OptionsInfo.SetValue(this.RealValue, value, null);
			}
		}

		// Token: 0x04002436 RID: 9270
		private static readonly ConstructorInfo Constructor = SendOptions.RealType.GetConstructor(Type.EmptyTypes);

		// Token: 0x04002437 RID: 9271
		private static readonly PropertyInfo OptionsInfo = SendOptions.RealType.GetProperty("Options");
	}
}
