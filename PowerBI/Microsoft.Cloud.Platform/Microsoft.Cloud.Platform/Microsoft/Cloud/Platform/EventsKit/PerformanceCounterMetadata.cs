using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x02000363 RID: 867
	[DataContract]
	[CannotApplyEqualityOperator]
	[Serializable]
	public sealed class PerformanceCounterMetadata : IEquatable<PerformanceCounterMetadata>
	{
		// Token: 0x060019D9 RID: 6617 RVA: 0x0005FDDC File Offset: 0x0005DFDC
		public PerformanceCounterMetadata([NotNull] PerformanceCounterAttribute performanceCounterAttribute, [NotNull] EventsKitEventMetadata eventMetadata)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<PerformanceCounterAttribute>(performanceCounterAttribute, "perfCounterAttribute");
			ExtendedDiagnostics.EnsureArgumentNotNull<EventsKitEventMetadata>(eventMetadata, "eventMetadata");
			this.Initallize(eventMetadata, performanceCounterAttribute.Id, performanceCounterAttribute.CounterName, (PerformanceCounterType)performanceCounterAttribute.CounterType, performanceCounterAttribute.CounterModifier, performanceCounterAttribute.ModifierExpression);
			if (performanceCounterAttribute.CounterType.Equals(Microsoft.Cloud.Platform.Common.CounterType.AverageDelta))
			{
				this.Base = new PerformanceCounterMetadata(eventMetadata, this.CounterId + 1, this.CounterName + "Base", PerformanceCounterType.AverageBase, CounterModifier.Increment, performanceCounterAttribute.BaseModifierExpression);
			}
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x0005FE7A File Offset: 0x0005E07A
		private PerformanceCounterMetadata(EventsKitEventMetadata eventMetadata, int counterId, string counterName, PerformanceCounterType counterType, CounterModifier counterModifier, string modifierExpression)
		{
			this.Initallize(eventMetadata, counterId, counterName, counterType, counterModifier, modifierExpression);
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x0005FE94 File Offset: 0x0005E094
		private void Initallize(EventsKitEventMetadata eventMetadata, int counterId, string counterName, PerformanceCounterType counterType, CounterModifier counterModifier, string modifierExpression)
		{
			this.CounterName = counterName;
			this.CounterType = counterType;
			this.Modifier = counterModifier;
			this.ModifierExpression = modifierExpression;
			this.Category = eventMetadata.EventsKit.PerformanceCountersCategory;
			this.CounterId = counterId;
			this.CounterSymbol = Auxiliary.CreateIdentifierFromString(this.CounterName);
			this.Name = "{0}_{1}".FormatWithInvariantCulture(new object[] { eventMetadata.Name, this.CounterSymbol });
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x060019DC RID: 6620 RVA: 0x0005FF11 File Offset: 0x0005E111
		// (set) Token: 0x060019DD RID: 6621 RVA: 0x0005FF19 File Offset: 0x0005E119
		[DataMember]
		public string Name { get; private set; }

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x060019DE RID: 6622 RVA: 0x0005FF22 File Offset: 0x0005E122
		// (set) Token: 0x060019DF RID: 6623 RVA: 0x0005FF2A File Offset: 0x0005E12A
		[DataMember]
		public int CounterId { get; private set; }

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x060019E0 RID: 6624 RVA: 0x0005FF33 File Offset: 0x0005E133
		// (set) Token: 0x060019E1 RID: 6625 RVA: 0x0005FF3B File Offset: 0x0005E13B
		[DataMember]
		public string CounterName { get; private set; }

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x060019E2 RID: 6626 RVA: 0x0005FF44 File Offset: 0x0005E144
		// (set) Token: 0x060019E3 RID: 6627 RVA: 0x0005FF4C File Offset: 0x0005E14C
		[DataMember]
		public string CounterSymbol { get; private set; }

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x060019E4 RID: 6628 RVA: 0x0005FF55 File Offset: 0x0005E155
		// (set) Token: 0x060019E5 RID: 6629 RVA: 0x0005FF5D File Offset: 0x0005E15D
		[DataMember]
		public PerformanceCounterCategoryMetadata Category { get; private set; }

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x060019E6 RID: 6630 RVA: 0x0005FF66 File Offset: 0x0005E166
		// (set) Token: 0x060019E7 RID: 6631 RVA: 0x0005FF6E File Offset: 0x0005E16E
		[DataMember]
		public PerformanceCounterType CounterType { get; private set; }

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x060019E8 RID: 6632 RVA: 0x0005FF77 File Offset: 0x0005E177
		// (set) Token: 0x060019E9 RID: 6633 RVA: 0x0005FF7F File Offset: 0x0005E17F
		[DataMember]
		public CounterModifier Modifier { get; private set; }

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x060019EA RID: 6634 RVA: 0x0005FF88 File Offset: 0x0005E188
		// (set) Token: 0x060019EB RID: 6635 RVA: 0x0005FF90 File Offset: 0x0005E190
		[DataMember]
		public string ModifierExpression { get; private set; }

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x060019EC RID: 6636 RVA: 0x0005FF99 File Offset: 0x0005E199
		// (set) Token: 0x060019ED RID: 6637 RVA: 0x0005FFA1 File Offset: 0x0005E1A1
		[DataMember]
		public PerformanceCounterMetadata Base { get; private set; }

		// Token: 0x060019EE RID: 6638 RVA: 0x0005FFAA File Offset: 0x0005E1AA
		public bool Equals(PerformanceCounterMetadata other)
		{
			return other != null && (this == other || this.CounterId == other.CounterId);
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x0005FFC5 File Offset: 0x0005E1C5
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (obj is PerformanceCounterMetadata && this.Equals((PerformanceCounterMetadata)obj)));
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x0005FFE8 File Offset: 0x0005E1E8
		public override int GetHashCode()
		{
			return this.CounterId;
		}
	}
}
