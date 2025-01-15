using System;
using System.ComponentModel;
using Microsoft.Lucia.Json;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.TermIndex
{
	// Token: 0x02000159 RID: 345
	[ImmutableObject(true)]
	[JsonConverter(typeof(DefaultJsonConverter))]
	public sealed class EntityInstancesIndexElement : DataIndexElement
	{
		// Token: 0x060006D8 RID: 1752 RVA: 0x0000BB4B File Offset: 0x00009D4B
		public EntityInstancesIndexElement(DataIndexElementBinding binding, DataIndexElementBinding weightBinding, int? instanceCount, DataIndexElementStatus status, InstanceIndex instanceIndex, InstancePluralNormalization pluralNormalization)
			: base(instanceCount, status, instanceIndex, pluralNormalization)
		{
			this.Binding = binding;
			this.WeightBinding = weightBinding;
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x0000BB68 File Offset: 0x00009D68
		public override DataIndexElementKind Kind
		{
			get
			{
				return DataIndexElementKind.EntityInstances;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x0000BB6B File Offset: 0x00009D6B
		public DataIndexElementBinding Binding { get; }

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x0000BB73 File Offset: 0x00009D73
		public DataIndexElementBinding WeightBinding { get; }

		// Token: 0x060006DC RID: 1756 RVA: 0x0000BB7C File Offset: 0x00009D7C
		public override DataIndexElement WithStatus(DataIndexElementStatus status, int? instanceCount)
		{
			if (base.Status == status)
			{
				int? num = instanceCount;
				int? instanceCount2 = base.InstanceCount;
				if ((num.GetValueOrDefault() == instanceCount2.GetValueOrDefault()) & (num != null == (instanceCount2 != null)))
				{
					return this;
				}
			}
			return new EntityInstancesIndexElement(this.Binding, this.WeightBinding, instanceCount, status, base.InstanceIndex, base.PluralNormalization);
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0000BBE0 File Offset: 0x00009DE0
		protected override int StableCompareTo(DataIndexElement other)
		{
			EntityInstancesIndexElement entityInstancesIndexElement = other as EntityInstancesIndexElement;
			if (entityInstancesIndexElement == null)
			{
				return base.StableCompareToBase(other);
			}
			int num = DataIndexElement.StableCompare(this.Binding, entityInstancesIndexElement.Binding);
			if (num != 0)
			{
				return num;
			}
			return DataIndexElement.StableCompare(this.WeightBinding, entityInstancesIndexElement.WeightBinding);
		}
	}
}
