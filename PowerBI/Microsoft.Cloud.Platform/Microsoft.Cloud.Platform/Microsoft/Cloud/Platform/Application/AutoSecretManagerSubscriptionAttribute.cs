using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Application
{
	// Token: 0x020003ED RID: 1005
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class AutoSecretManagerSubscriptionAttribute : Attribute
	{
		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001EE5 RID: 7909 RVA: 0x00073C8E File Offset: 0x00071E8E
		// (set) Token: 0x06001EE6 RID: 7910 RVA: 0x00073C96 File Offset: 0x00071E96
		public string Key1 { get; set; }

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06001EE7 RID: 7911 RVA: 0x00073C9F File Offset: 0x00071E9F
		// (set) Token: 0x06001EE8 RID: 7912 RVA: 0x00073CA7 File Offset: 0x00071EA7
		public string Key2 { get; set; }

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06001EE9 RID: 7913 RVA: 0x00073CB0 File Offset: 0x00071EB0
		// (set) Token: 0x06001EEA RID: 7914 RVA: 0x00073CB8 File Offset: 0x00071EB8
		public string Key3 { get; set; }

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001EEB RID: 7915 RVA: 0x00073CC1 File Offset: 0x00071EC1
		// (set) Token: 0x06001EEC RID: 7916 RVA: 0x00073CC9 File Offset: 0x00071EC9
		public bool SubscribeManually { get; set; }

		// Token: 0x06001EED RID: 7917 RVA: 0x00073CD2 File Offset: 0x00071ED2
		public AutoSecretManagerSubscriptionAttribute(string key)
		{
			this.Key1 = key;
			this.Validate();
		}

		// Token: 0x06001EEE RID: 7918 RVA: 0x00073CE7 File Offset: 0x00071EE7
		public AutoSecretManagerSubscriptionAttribute(string key1, string key2)
		{
			this.Key1 = key1;
			this.Key2 = key2;
			this.Validate();
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x00073D03 File Offset: 0x00071F03
		public AutoSecretManagerSubscriptionAttribute(string key1, string key2, string key3)
		{
			this.Key1 = key1;
			this.Key2 = key2;
			this.Key3 = key3;
			this.Validate();
		}

		// Token: 0x06001EF0 RID: 7920 RVA: 0x00073D28 File Offset: 0x00071F28
		public IEnumerable<string> GetKeys()
		{
			List<string> list = new List<string>();
			if (this.Key1 != null)
			{
				list.Add(this.Key1);
			}
			if (this.Key2 != null)
			{
				list.Add(this.Key2);
			}
			if (this.Key3 != null)
			{
				list.Add(this.Key3);
			}
			return list;
		}

		// Token: 0x06001EF1 RID: 7921 RVA: 0x00073D78 File Offset: 0x00071F78
		private void Validate()
		{
			if (this.Key1 == null && this.Key2 == null && this.Key3 == null)
			{
				throw new ArgumentException(base.GetType().Name + " cannot be constructed with no keys");
			}
		}
	}
}
