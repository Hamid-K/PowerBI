using System;
using System.ComponentModel;
using Microsoft.Lucia.Json;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.TermIndex
{
	// Token: 0x0200015A RID: 346
	[ImmutableObject(true)]
	[JsonConverter(typeof(DefaultJsonConverter))]
	public sealed class EntityInstanceSynonymsIndexElement : DataIndexElement
	{
		// Token: 0x060006DE RID: 1758 RVA: 0x0000BC27 File Offset: 0x00009E27
		public EntityInstanceSynonymsIndexElement(DataIndexElementBinding synonymBinding, DataIndexElementBinding valueBinding, DataIndexElementBinding weightBinding, int? instanceCount, DataIndexElementStatus status, InstanceIndex instanceIndex, InstancePluralNormalization pluralNormalization)
			: base(instanceCount, status, instanceIndex, pluralNormalization)
		{
			this.SynonymBinding = synonymBinding;
			this.ValueBinding = valueBinding;
			this.WeightBinding = weightBinding;
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x0000BC4C File Offset: 0x00009E4C
		public override DataIndexElementKind Kind
		{
			get
			{
				return DataIndexElementKind.EntityInstanceSynonyms;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x0000BC4F File Offset: 0x00009E4F
		public DataIndexElementBinding SynonymBinding { get; }

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x0000BC57 File Offset: 0x00009E57
		public DataIndexElementBinding ValueBinding { get; }

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x0000BC5F File Offset: 0x00009E5F
		public DataIndexElementBinding WeightBinding { get; }

		// Token: 0x060006E3 RID: 1763 RVA: 0x0000BC68 File Offset: 0x00009E68
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
			return new EntityInstanceSynonymsIndexElement(this.SynonymBinding, this.ValueBinding, this.WeightBinding, instanceCount, status, base.InstanceIndex, base.PluralNormalization);
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0000BCD4 File Offset: 0x00009ED4
		protected override int StableCompareTo(DataIndexElement other)
		{
			EntityInstanceSynonymsIndexElement entityInstanceSynonymsIndexElement = other as EntityInstanceSynonymsIndexElement;
			if (entityInstanceSynonymsIndexElement == null)
			{
				return base.StableCompareToBase(other);
			}
			int num = DataIndexElement.StableCompare(this.SynonymBinding, entityInstanceSynonymsIndexElement.SynonymBinding);
			if (num != 0)
			{
				return num;
			}
			int num2 = DataIndexElement.StableCompare(this.ValueBinding, entityInstanceSynonymsIndexElement.ValueBinding);
			if (num2 != 0)
			{
				return num2;
			}
			return DataIndexElement.StableCompare(this.WeightBinding, entityInstanceSynonymsIndexElement.WeightBinding);
		}
	}
}
