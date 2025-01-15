using System;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001AB RID: 427
	public sealed class EntityDefinition
	{
		// Token: 0x060008D1 RID: 2257 RVA: 0x0001187B File Offset: 0x0000FA7B
		public EntityDefinition()
		{
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x00011883 File Offset: 0x0000FA83
		public EntityDefinition(Binding binding)
		{
			this.Binding = binding;
		}

		// Token: 0x060008D3 RID: 2259 RVA: 0x00011892 File Offset: 0x0000FA92
		public EntityDefinition(string text)
		{
			this.Text = text;
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x000118A1 File Offset: 0x0000FAA1
		// (set) Token: 0x060008D5 RID: 2261 RVA: 0x000118A9 File Offset: 0x0000FAA9
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		[JsonConverter(typeof(Binding.AnyBindingConverter))]
		public Binding Binding { get; set; }

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x000118B2 File Offset: 0x0000FAB2
		// (set) Token: 0x060008D7 RID: 2263 RVA: 0x000118BA File Offset: 0x0000FABA
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string Text { get; set; }
	}
}
