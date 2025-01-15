using System;
using System.ComponentModel;
using Microsoft.Lucia.Yaml;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001CF RID: 463
	public sealed class TermProperties : ICustomSerializationOptions
	{
		// Token: 0x1700030C RID: 780
		// (get) Token: 0x060009EF RID: 2543 RVA: 0x0001277C File Offset: 0x0001097C
		// (set) Token: 0x060009F0 RID: 2544 RVA: 0x00012784 File Offset: 0x00010984
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public TermPropertiesType? Type { get; set; }

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x060009F1 RID: 2545 RVA: 0x0001278D File Offset: 0x0001098D
		// (set) Token: 0x060009F2 RID: 2546 RVA: 0x00012795 File Offset: 0x00010995
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public State State { get; set; }

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x0001279E File Offset: 0x0001099E
		// (set) Token: 0x060009F4 RID: 2548 RVA: 0x000127A6 File Offset: 0x000109A6
		[JsonProperty]
		public Source Source { get; set; }

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x060009F5 RID: 2549 RVA: 0x000127AF File Offset: 0x000109AF
		// (set) Token: 0x060009F6 RID: 2550 RVA: 0x000127B7 File Offset: 0x000109B7
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		[DefaultValue(1.0)]
		public double Weight { get; set; } = 1.0;

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x060009F7 RID: 2551 RVA: 0x000127C0 File Offset: 0x000109C0
		// (set) Token: 0x060009F8 RID: 2552 RVA: 0x000127C8 File Offset: 0x000109C8
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string TemplateSchema { get; set; }

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x000127D1 File Offset: 0x000109D1
		// (set) Token: 0x060009FA RID: 2554 RVA: 0x000127D9 File Offset: 0x000109D9
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

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x000127E7 File Offset: 0x000109E7
		YamlSerializationOptions ICustomSerializationOptions.Options
		{
			get
			{
				return YamlSerializationOptions.Compact;
			}
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x000127EC File Offset: 0x000109EC
		internal bool IsDefault()
		{
			return this.Type == null && this.Source.IsDefault() && this.Weight.IsDefaultWeight() && this.TemplateSchema == null && this.LastModified == null && this.State == State.Authored;
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0001284C File Offset: 0x00010A4C
		public bool ShouldSerializeSource()
		{
			return this.Source.ShouldSerialize();
		}

		// Token: 0x040007E2 RID: 2018
		private DateTime? _lastModified;
	}
}
