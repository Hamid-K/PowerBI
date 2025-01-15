using System;

namespace Microsoft.Identity.Json
{
	// Token: 0x02000024 RID: 36
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple = false)]
	internal sealed class JsonObjectAttribute : JsonContainerAttribute
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003030 File Offset: 0x00001230
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x00003038 File Offset: 0x00001238
		public MemberSerialization MemberSerialization
		{
			get
			{
				return this._memberSerialization;
			}
			set
			{
				this._memberSerialization = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00003041 File Offset: 0x00001241
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x0000304E File Offset: 0x0000124E
		public MissingMemberHandling MissingMemberHandling
		{
			get
			{
				return this._missingMemberHandling.GetValueOrDefault();
			}
			set
			{
				this._missingMemberHandling = new MissingMemberHandling?(value);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x0000305C File Offset: 0x0000125C
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x00003069 File Offset: 0x00001269
		public NullValueHandling ItemNullValueHandling
		{
			get
			{
				return this._itemNullValueHandling.GetValueOrDefault();
			}
			set
			{
				this._itemNullValueHandling = new NullValueHandling?(value);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00003077 File Offset: 0x00001277
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x00003084 File Offset: 0x00001284
		public Required ItemRequired
		{
			get
			{
				return this._itemRequired.GetValueOrDefault();
			}
			set
			{
				this._itemRequired = new Required?(value);
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003092 File Offset: 0x00001292
		public JsonObjectAttribute()
		{
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000309A File Offset: 0x0000129A
		public JsonObjectAttribute(MemberSerialization memberSerialization)
		{
			this.MemberSerialization = memberSerialization;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000030A9 File Offset: 0x000012A9
		public JsonObjectAttribute(string id)
			: base(id)
		{
		}

		// Token: 0x04000041 RID: 65
		private MemberSerialization _memberSerialization;

		// Token: 0x04000042 RID: 66
		internal MissingMemberHandling? _missingMemberHandling;

		// Token: 0x04000043 RID: 67
		internal Required? _itemRequired;

		// Token: 0x04000044 RID: 68
		internal NullValueHandling? _itemNullValueHandling;
	}
}
