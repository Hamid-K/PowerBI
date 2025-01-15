using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.AspNet.OData.Common;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.OData.Edm;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x02000119 RID: 281
	public class EnumTypeConfiguration : IEdmTypeConfiguration
	{
		// Token: 0x060009A0 RID: 2464 RVA: 0x000282F0 File Offset: 0x000264F0
		public EnumTypeConfiguration(ODataModelBuilder builder, Type clrType)
		{
			if (builder == null)
			{
				throw Error.ArgumentNull("builder");
			}
			if (clrType == null)
			{
				throw Error.ArgumentNull("clrType");
			}
			if (!TypeHelper.IsEnum(clrType))
			{
				throw Error.Argument("clrType", SRResources.TypeCannotBeEnum, new object[] { clrType.FullName });
			}
			this.ClrType = clrType;
			this.IsFlags = TypeHelper.AsMemberInfo(clrType).GetCustomAttributes(typeof(FlagsAttribute), false).Any<object>();
			this.UnderlyingType = Enum.GetUnderlyingType(clrType);
			this.ModelBuilder = builder;
			this._name = clrType.EdmName();
			this._namespace = (builder.HasAssignedNamespace ? builder.Namespace : (clrType.Namespace ?? builder.Namespace));
			this.ExplicitMembers = new Dictionary<Enum, EnumMemberConfiguration>();
			this.RemovedMembers = new List<Enum>();
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x060009A1 RID: 2465 RVA: 0x000283CF File Offset: 0x000265CF
		public EdmTypeKind Kind
		{
			get
			{
				return EdmTypeKind.Enum;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060009A2 RID: 2466 RVA: 0x000283D2 File Offset: 0x000265D2
		// (set) Token: 0x060009A3 RID: 2467 RVA: 0x000283DA File Offset: 0x000265DA
		public bool IsFlags { get; private set; }

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x000283E3 File Offset: 0x000265E3
		// (set) Token: 0x060009A5 RID: 2469 RVA: 0x000283EB File Offset: 0x000265EB
		public Type ClrType { get; private set; }

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x000283F4 File Offset: 0x000265F4
		// (set) Token: 0x060009A7 RID: 2471 RVA: 0x000283FC File Offset: 0x000265FC
		public Type UnderlyingType { get; private set; }

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x00028405 File Offset: 0x00026605
		public string FullName
		{
			get
			{
				return this.Namespace + "." + this.Name;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x0002841D File Offset: 0x0002661D
		// (set) Token: 0x060009AA RID: 2474 RVA: 0x00028425 File Offset: 0x00026625
		public string Namespace
		{
			get
			{
				return this._namespace;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._namespace = value;
				this.AddedExplicitly = true;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x0002843E File Offset: 0x0002663E
		// (set) Token: 0x060009AC RID: 2476 RVA: 0x00028446 File Offset: 0x00026646
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._name = value;
				this.AddedExplicitly = true;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x0002845F File Offset: 0x0002665F
		public IEnumerable<EnumMemberConfiguration> Members
		{
			get
			{
				return this.ExplicitMembers.Values;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x060009AE RID: 2478 RVA: 0x0002846C File Offset: 0x0002666C
		public ReadOnlyCollection<Enum> IgnoredMembers
		{
			get
			{
				return new ReadOnlyCollection<Enum>(this.RemovedMembers);
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x00028479 File Offset: 0x00026679
		// (set) Token: 0x060009B0 RID: 2480 RVA: 0x00028481 File Offset: 0x00026681
		public bool AddedExplicitly { get; set; }

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x0002848A File Offset: 0x0002668A
		// (set) Token: 0x060009B2 RID: 2482 RVA: 0x00028492 File Offset: 0x00026692
		public ODataModelBuilder ModelBuilder { get; private set; }

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x0002849B File Offset: 0x0002669B
		// (set) Token: 0x060009B4 RID: 2484 RVA: 0x000284A3 File Offset: 0x000266A3
		protected internal IList<Enum> RemovedMembers { get; private set; }

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x000284AC File Offset: 0x000266AC
		// (set) Token: 0x060009B6 RID: 2486 RVA: 0x000284B4 File Offset: 0x000266B4
		protected internal IDictionary<Enum, EnumMemberConfiguration> ExplicitMembers { get; private set; }

		// Token: 0x060009B7 RID: 2487 RVA: 0x000284C0 File Offset: 0x000266C0
		public EnumMemberConfiguration AddMember(Enum member)
		{
			if (member == null)
			{
				throw Error.ArgumentNull("member");
			}
			if (member.GetType() != this.ClrType)
			{
				throw Error.Argument("member", SRResources.PropertyDoesNotBelongToType, new object[]
				{
					member.ToString(),
					this.ClrType.FullName
				});
			}
			if (this.RemovedMembers.Contains(member))
			{
				this.RemovedMembers.Remove(member);
			}
			EnumMemberConfiguration enumMemberConfiguration;
			if (this.ExplicitMembers.ContainsKey(member))
			{
				enumMemberConfiguration = this.ExplicitMembers[member];
			}
			else
			{
				enumMemberConfiguration = new EnumMemberConfiguration(member, this);
				this.ExplicitMembers[member] = enumMemberConfiguration;
			}
			return enumMemberConfiguration;
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0002856C File Offset: 0x0002676C
		public void RemoveMember(Enum member)
		{
			if (member == null)
			{
				throw Error.ArgumentNull("member");
			}
			if (member.GetType() != this.ClrType)
			{
				throw Error.Argument("member", SRResources.PropertyDoesNotBelongToType, new object[]
				{
					member.ToString(),
					this.ClrType.FullName
				});
			}
			if (this.ExplicitMembers.ContainsKey(member))
			{
				this.ExplicitMembers.Remove(member);
			}
			if (!this.RemovedMembers.Contains(member))
			{
				this.RemovedMembers.Add(member);
			}
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x000285FC File Offset: 0x000267FC
		internal NullableEnumTypeConfiguration GetNullableEnumTypeConfiguration()
		{
			if (this.nullableEnumTypeConfiguration == null)
			{
				this.nullableEnumTypeConfiguration = new NullableEnumTypeConfiguration(this);
			}
			return this.nullableEnumTypeConfiguration;
		}

		// Token: 0x04000311 RID: 785
		private string _namespace;

		// Token: 0x04000312 RID: 786
		private string _name;

		// Token: 0x04000313 RID: 787
		private NullableEnumTypeConfiguration nullableEnumTypeConfiguration;
	}
}
