using System;

namespace Microsoft.Cloud.Platform.ConfigurationManagement
{
	// Token: 0x020003FE RID: 1022
	[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
	public sealed class ConfigurationParserAttribute : Attribute
	{
		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06001F53 RID: 8019 RVA: 0x00075C1B File Offset: 0x00073E1B
		// (set) Token: 0x06001F54 RID: 8020 RVA: 0x00075C23 File Offset: 0x00073E23
		public Type ConfigurationType { get; private set; }

		// Token: 0x06001F55 RID: 8021 RVA: 0x00075C2C File Offset: 0x00073E2C
		public ConfigurationParserAttribute(Type configurationType)
		{
			this.ConfigurationType = configurationType;
		}
	}
}
