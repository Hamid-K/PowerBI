using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001B7 RID: 439
	public abstract class SharedPhrasingProperties : IStateItem
	{
		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x00011A49 File Offset: 0x0000FC49
		// (set) Token: 0x06000903 RID: 2307 RVA: 0x00011A51 File Offset: 0x0000FC51
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public State State { get; set; }

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000904 RID: 2308 RVA: 0x00011A5A File Offset: 0x0000FC5A
		// (set) Token: 0x06000905 RID: 2309 RVA: 0x00011A62 File Offset: 0x0000FC62
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		[DefaultValue(1.0)]
		public double Weight { get; set; } = 1.0;

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x00011A6B File Offset: 0x0000FC6B
		// (set) Token: 0x06000907 RID: 2311 RVA: 0x00011A73 File Offset: 0x0000FC73
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string TemplateSchema { get; set; }

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x00011A7C File Offset: 0x0000FC7C
		// (set) Token: 0x06000909 RID: 2313 RVA: 0x00011A84 File Offset: 0x0000FC84
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public DateTime? LastModified
		{
			get
			{
				return this._lastModified;
			}
			set
			{
				this._lastModified = LsdlAsserts.DateTimeShouldBeUtc(value);
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x0600090A RID: 2314 RVA: 0x00011A92 File Offset: 0x0000FC92
		// (set) Token: 0x0600090B RID: 2315 RVA: 0x00011A9A File Offset: 0x0000FC9A
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string ID { get; set; }

		// Token: 0x04000785 RID: 1925
		private DateTime? _lastModified;
	}
}
