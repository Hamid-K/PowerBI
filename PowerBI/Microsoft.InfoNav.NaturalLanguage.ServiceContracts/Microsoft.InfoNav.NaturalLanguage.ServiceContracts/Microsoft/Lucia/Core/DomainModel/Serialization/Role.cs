using System;
using System.Collections.Generic;
using Microsoft.InfoNav;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001C5 RID: 453
	public sealed class Role : IScalarForm<string>
	{
		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x00012397 File Offset: 0x00010597
		// (set) Token: 0x060009A9 RID: 2473 RVA: 0x0001239F File Offset: 0x0001059F
		[JsonProperty(Required = Required.Always)]
		public EntityReference Target { get; set; }

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x000123A8 File Offset: 0x000105A8
		[JsonProperty]
		public List<Term> Nouns { get; } = new TermList();

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x000123B0 File Offset: 0x000105B0
		// (set) Token: 0x060009AC RID: 2476 RVA: 0x000123B8 File Offset: 0x000105B8
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public RoleReference Quantity { get; set; }

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x000123C1 File Offset: 0x000105C1
		// (set) Token: 0x060009AE RID: 2478 RVA: 0x000123C9 File Offset: 0x000105C9
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public RoleReference Amount { get; set; }

		// Token: 0x060009AF RID: 2479 RVA: 0x000123D2 File Offset: 0x000105D2
		public bool ShouldSerializeNouns()
		{
			return this.Nouns.Count > 0;
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x000123E2 File Offset: 0x000105E2
		internal bool TryGetScalarForm(out string value)
		{
			if (this.Target.TryGetScalarForm(out value) && this.Nouns.IsEmpty<Term>() && this.Quantity == null && this.Amount == null)
			{
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x00012415 File Offset: 0x00010615
		internal static Role FromScalarForm(string value)
		{
			return new Role
			{
				Target = EntityReference.FromScalarForm(value)
			};
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x00012428 File Offset: 0x00010628
		bool IScalarForm<string>.TryGetScalarForm(out string value)
		{
			return this.TryGetScalarForm(out value);
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x00012431 File Offset: 0x00010631
		void IScalarForm<string>.SetFromScalarForm(string value)
		{
			this.Target = EntityReference.FromScalarForm(value);
		}
	}
}
