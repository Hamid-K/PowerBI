using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.InfoNav;
using Microsoft.Lucia.Json;
using Newtonsoft.Json;

namespace Microsoft.Lucia.Core.TermIndex
{
	// Token: 0x02000158 RID: 344
	[ImmutableObject(true)]
	[JsonConverter(typeof(DataIndexElement.JsonConverter))]
	public abstract class DataIndexElement
	{
		// Token: 0x060006CD RID: 1741 RVA: 0x0000BA7A File Offset: 0x00009C7A
		protected DataIndexElement(int? instanceCount, DataIndexElementStatus status, InstanceIndex instanceIndex, InstancePluralNormalization pluralNormalization)
		{
			this.InstanceCount = instanceCount;
			this.InstanceIndex = instanceIndex;
			this.Status = status;
			this.PluralNormalization = pluralNormalization;
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060006CE RID: 1742
		public abstract DataIndexElementKind Kind { get; }

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x0000BA9F File Offset: 0x00009C9F
		public int? InstanceCount { get; }

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x0000BAA7 File Offset: 0x00009CA7
		public DataIndexElementStatus Status { get; }

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x0000BAAF File Offset: 0x00009CAF
		public InstanceIndex InstanceIndex { get; }

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x0000BAB7 File Offset: 0x00009CB7
		public InstancePluralNormalization PluralNormalization { get; }

		// Token: 0x060006D3 RID: 1747
		public abstract DataIndexElement WithStatus(DataIndexElementStatus status, int? instanceCount);

		// Token: 0x060006D4 RID: 1748
		protected abstract int StableCompareTo(DataIndexElement other);

		// Token: 0x060006D5 RID: 1749 RVA: 0x0000BAC0 File Offset: 0x00009CC0
		protected int StableCompareToBase(DataIndexElement other)
		{
			int kind = (int)this.Kind;
			int kind2 = (int)other.Kind;
			return kind.CompareTo(kind2);
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0000BAE4 File Offset: 0x00009CE4
		protected static int StableCompare(DataIndexElementBinding x, DataIndexElementBinding y)
		{
			if (x == null || y == null)
			{
				if (x != null)
				{
					return 1;
				}
				if (y == null)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				int num = ConceptualNameComparer.Instance.Compare(x.ConceptualEntity, y.ConceptualEntity);
				if (num != 0)
				{
					return num;
				}
				return ConceptualNameComparer.Instance.Compare(x.ConceptualProperty, y.ConceptualProperty);
			}
		}

		// Token: 0x0400068C RID: 1676
		public static readonly IComparer<DataIndexElement> PriorityComparer = new DataIndexElement.PriorityComparerImpl();

		// Token: 0x0400068D RID: 1677
		internal static readonly IComparer<DataIndexElement> StableComparer = new DataIndexElement.StableComparerImpl();

		// Token: 0x0200020C RID: 524
		public sealed class JsonConverter : TaggedUnionConverter<DataIndexElement, DataIndexElementKind>
		{
			// Token: 0x17000335 RID: 821
			// (get) Token: 0x06000B45 RID: 2885 RVA: 0x00014E3C File Offset: 0x0001303C
			protected override string KindProperty
			{
				get
				{
					return "Kind";
				}
			}

			// Token: 0x06000B46 RID: 2886 RVA: 0x00014E44 File Offset: 0x00013044
			protected override Type GetTypeForKind(DataIndexElementKind? kind)
			{
				if (kind != null)
				{
					DataIndexElementKind valueOrDefault = kind.GetValueOrDefault();
					if (valueOrDefault != DataIndexElementKind.EntityInstances && valueOrDefault == DataIndexElementKind.EntityInstanceSynonyms)
					{
						return typeof(EntityInstanceSynonymsIndexElement);
					}
				}
				return typeof(EntityInstancesIndexElement);
			}
		}

		// Token: 0x0200020D RID: 525
		private sealed class PriorityComparerImpl : IComparer<DataIndexElement>
		{
			// Token: 0x06000B48 RID: 2888 RVA: 0x00014E88 File Offset: 0x00013088
			public int Compare(DataIndexElement x, DataIndexElement y)
			{
				int num = DataIndexElement.PriorityComparerImpl.GetIndexOptionPriority(x.InstanceIndex).CompareTo(DataIndexElement.PriorityComparerImpl.GetIndexOptionPriority(y.InstanceIndex));
				if (num != 0)
				{
					return num;
				}
				int num2 = x.InstanceCount ?? int.MaxValue;
				int num3 = y.InstanceCount ?? int.MaxValue;
				int num4 = num2.CompareTo(num3);
				if (num4 != 0)
				{
					return num4;
				}
				int kind = (int)x.Kind;
				int kind2 = (int)y.Kind;
				return kind.CompareTo(kind2);
			}

			// Token: 0x06000B49 RID: 2889 RVA: 0x00014F21 File Offset: 0x00013121
			private static int GetIndexOptionPriority(InstanceIndex instanceIndex)
			{
				if (instanceIndex == InstanceIndex.Default)
				{
					return 1;
				}
				if (instanceIndex == InstanceIndex.All)
				{
					return 0;
				}
				return 2;
			}
		}

		// Token: 0x0200020E RID: 526
		private sealed class StableComparerImpl : IComparer<DataIndexElement>
		{
			// Token: 0x06000B4B RID: 2891 RVA: 0x00014F37 File Offset: 0x00013137
			public int Compare(DataIndexElement x, DataIndexElement y)
			{
				return x.StableCompareTo(y);
			}
		}
	}
}
