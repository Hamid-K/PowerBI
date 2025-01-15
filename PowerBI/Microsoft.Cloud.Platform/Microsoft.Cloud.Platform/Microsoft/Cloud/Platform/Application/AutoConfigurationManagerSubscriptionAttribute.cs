using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Application
{
	// Token: 0x020003EC RID: 1004
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class AutoConfigurationManagerSubscriptionAttribute : Attribute
	{
		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001ED8 RID: 7896 RVA: 0x00073B40 File Offset: 0x00071D40
		// (set) Token: 0x06001ED9 RID: 7897 RVA: 0x00073B48 File Offset: 0x00071D48
		public Type ConfigurationType1 { get; set; }

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06001EDA RID: 7898 RVA: 0x00073B51 File Offset: 0x00071D51
		// (set) Token: 0x06001EDB RID: 7899 RVA: 0x00073B59 File Offset: 0x00071D59
		public Type ConfigurationType2 { get; set; }

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001EDC RID: 7900 RVA: 0x00073B62 File Offset: 0x00071D62
		// (set) Token: 0x06001EDD RID: 7901 RVA: 0x00073B6A File Offset: 0x00071D6A
		public Type ConfigurationType3 { get; set; }

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06001EDE RID: 7902 RVA: 0x00073B73 File Offset: 0x00071D73
		// (set) Token: 0x06001EDF RID: 7903 RVA: 0x00073B7B File Offset: 0x00071D7B
		public bool SubscribeManually { get; set; }

		// Token: 0x06001EE0 RID: 7904 RVA: 0x00073B84 File Offset: 0x00071D84
		public AutoConfigurationManagerSubscriptionAttribute(Type configurationType)
		{
			this.ConfigurationType1 = configurationType;
			this.Validate();
		}

		// Token: 0x06001EE1 RID: 7905 RVA: 0x00073B99 File Offset: 0x00071D99
		public AutoConfigurationManagerSubscriptionAttribute(Type configurationType1, Type configurationType2)
		{
			this.ConfigurationType1 = configurationType1;
			this.ConfigurationType2 = configurationType2;
			this.Validate();
		}

		// Token: 0x06001EE2 RID: 7906 RVA: 0x00073BB5 File Offset: 0x00071DB5
		public AutoConfigurationManagerSubscriptionAttribute(Type configurationType1, Type configurationType2, Type configurationType3)
		{
			this.ConfigurationType1 = configurationType1;
			this.ConfigurationType2 = configurationType2;
			this.ConfigurationType3 = configurationType3;
			this.Validate();
		}

		// Token: 0x06001EE3 RID: 7907 RVA: 0x00073BD8 File Offset: 0x00071DD8
		public IEnumerable<Type> GetConfigurationTypes()
		{
			List<Type> list = new List<Type>();
			if (this.ConfigurationType1 != null)
			{
				list.Add(this.ConfigurationType1);
			}
			if (this.ConfigurationType2 != null)
			{
				list.Add(this.ConfigurationType2);
			}
			if (this.ConfigurationType3 != null)
			{
				list.Add(this.ConfigurationType3);
			}
			return list;
		}

		// Token: 0x06001EE4 RID: 7908 RVA: 0x00073C3C File Offset: 0x00071E3C
		private void Validate()
		{
			if (this.ConfigurationType1 == null && this.ConfigurationType2 == null && this.ConfigurationType3 == null)
			{
				throw new ArgumentException(base.GetType().Name + " cannot be constructed with no configuration types");
			}
		}
	}
}
