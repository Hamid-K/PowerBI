using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.InfoNav;
using Microsoft.Lucia.Yaml;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001C4 RID: 452
	public sealed class Relationship : ICustomSerializationOptions, IStateItem
	{
		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x000120BE File Offset: 0x000102BE
		// (set) Token: 0x06000993 RID: 2451 RVA: 0x000120C6 File Offset: 0x000102C6
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public ConceptualEntityBinding Binding { get; set; }

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x000120CF File Offset: 0x000102CF
		// (set) Token: 0x06000995 RID: 2453 RVA: 0x000120D7 File Offset: 0x000102D7
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public State State { get; set; }

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000996 RID: 2454 RVA: 0x000120E0 File Offset: 0x000102E0
		// (set) Token: 0x06000997 RID: 2455 RVA: 0x000120E8 File Offset: 0x000102E8
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		[DefaultValue(1.0)]
		public double Weight { get; set; } = 1.0;

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x000120F1 File Offset: 0x000102F1
		// (set) Token: 0x06000999 RID: 2457 RVA: 0x000120F9 File Offset: 0x000102F9
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string TemplateSchema { get; set; }

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x00012102 File Offset: 0x00010302
		// (set) Token: 0x0600099B RID: 2459 RVA: 0x0001210A File Offset: 0x0001030A
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

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x00012118 File Offset: 0x00010318
		[JsonProperty]
		public Dictionary<string, Role> Roles { get; } = new Dictionary<string, Role>();

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x00012120 File Offset: 0x00010320
		// (set) Token: 0x0600099E RID: 2462 RVA: 0x00012128 File Offset: 0x00010328
		[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
		public SemanticSlots SemanticSlots { get; set; }

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x00012131 File Offset: 0x00010331
		[JsonProperty]
		public List<Condition> Conditions { get; } = new List<Condition>();

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060009A0 RID: 2464 RVA: 0x00012139 File Offset: 0x00010339
		[JsonProperty]
		public List<Phrasing> Phrasings { get; } = new List<Phrasing>();

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x00012141 File Offset: 0x00010341
		YamlSerializationOptions ICustomSerializationOptions.Options
		{
			get
			{
				return YamlSerializationOptions.LineBreakAfter;
			}
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x00012144 File Offset: 0x00010344
		public bool ShouldSerializeRoles()
		{
			return this.Roles.Count > 0;
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x00012154 File Offset: 0x00010354
		public bool ShouldSerializeConditions()
		{
			return this.Conditions.Count > 0;
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x00012164 File Offset: 0x00010364
		public bool ShouldSerializePhrasings()
		{
			return this.Phrasings.Count > 0;
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x00012174 File Offset: 0x00010374
		public IReadOnlyList<KeyValuePair<string, Role>> GetRoles()
		{
			HashSet<string> hashSet = new HashSet<string>();
			List<KeyValuePair<string, Role>> list = new List<KeyValuePair<string, Role>>();
			Phrasing phrasing = this.Phrasings.FirstOrDefault<Phrasing>();
			if (phrasing != null)
			{
				List<RoleReference> list2 = new List<RoleReference>();
				foreach (RoleReference roleReference in phrasing.Properties.GetRoleReferences())
				{
					string role = roleReference.Role;
					Role role2;
					if (this.Roles.TryGetValue(role, out role2))
					{
						if (hashSet.Add(role))
						{
							list.Add(Util.ToKeyValuePair<string, Role>(role, role2));
						}
						list2.Add(role2.Quantity);
						list2.Add(role2.Amount);
					}
				}
				foreach (RoleReference roleReference2 in list2)
				{
					this.AddRole(roleReference2, list, hashSet);
				}
			}
			if (this.SemanticSlots != null)
			{
				this.AddRole(this.SemanticSlots.Where, list, hashSet);
				this.AddRole(this.SemanticSlots.When, list, hashSet);
				this.AddRole(this.SemanticSlots.Duration, list, hashSet);
				this.AddRole(this.SemanticSlots.Occurrences, list, hashSet);
			}
			foreach (KeyValuePair<string, Role> keyValuePair in this.Roles)
			{
				if (hashSet.Add(keyValuePair.Key))
				{
					list.Add(keyValuePair);
				}
			}
			return list;
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x00012320 File Offset: 0x00010520
		private void AddRole(RoleReference roleReference, List<KeyValuePair<string, Role>> roles, HashSet<string> rolesAdded)
		{
			if (roleReference == null)
			{
				return;
			}
			string role = roleReference.Role;
			Role role2;
			if (!this.Roles.TryGetValue(role, out role2) || !rolesAdded.Add(role))
			{
				return;
			}
			roles.Add(Util.ToKeyValuePair<string, Role>(role, role2));
		}

		// Token: 0x040007AE RID: 1966
		private DateTime? _lastModified;
	}
}
