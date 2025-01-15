using System;

namespace Microsoft.IdentityModel.Json
{
	// Token: 0x02000024 RID: 36
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple = false)]
	internal sealed class JsonObjectAttribute : JsonContainerAttribute
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00003044 File Offset: 0x00001244
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x0000304C File Offset: 0x0000124C
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
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x00003055 File Offset: 0x00001255
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x00003062 File Offset: 0x00001262
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
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00003070 File Offset: 0x00001270
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x0000307D File Offset: 0x0000127D
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
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x0000308B File Offset: 0x0000128B
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x00003098 File Offset: 0x00001298
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

		// Token: 0x060000AA RID: 170 RVA: 0x000030A6 File Offset: 0x000012A6
		public JsonObjectAttribute()
		{
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000030AE File Offset: 0x000012AE
		public JsonObjectAttribute(MemberSerialization memberSerialization)
		{
			this.MemberSerialization = memberSerialization;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000030BD File Offset: 0x000012BD
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
