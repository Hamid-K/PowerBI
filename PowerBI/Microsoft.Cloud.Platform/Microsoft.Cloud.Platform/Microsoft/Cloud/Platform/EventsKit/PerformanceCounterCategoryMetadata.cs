using System;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x02000362 RID: 866
	[CannotApplyEqualityOperator]
	[Serializable]
	public sealed class PerformanceCounterCategoryMetadata : IEquatable<PerformanceCounterCategoryMetadata>
	{
		// Token: 0x060019CA RID: 6602 RVA: 0x0005FC8D File Offset: 0x0005DE8D
		public PerformanceCounterCategoryMetadata([NotNull] PerformanceCountersCategoryAttribute performanceCountersCategoryAttribute, [NotNull] EventsKitMetadata eventsKit)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<PerformanceCountersCategoryAttribute>(performanceCountersCategoryAttribute, "performanceCountersCategoryAttribute");
			ExtendedDiagnostics.EnsureArgumentNotNull<EventsKitMetadata>(eventsKit, "eventsKit");
			this.Initialize(performanceCountersCategoryAttribute.Name, eventsKit.Id.FullId);
		}

		// Token: 0x060019CB RID: 6603 RVA: 0x0005FCC2 File Offset: 0x0005DEC2
		public PerformanceCounterCategoryMetadata([NotNull] EventsKitMetadata eventsKit)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<EventsKitMetadata>(eventsKit, "eventsKit");
			this.Initialize(eventsKit.Name, eventsKit.Id.FullId);
		}

		// Token: 0x060019CC RID: 6604 RVA: 0x0005FCEC File Offset: 0x0005DEEC
		private void Initialize(string name, Guid categoryId)
		{
			this.CategoryName = name;
			this.CategoryId = categoryId;
			this.CategorySymbol = Auxiliary.CreateIdentifierFromString(name);
			this.CategoryProvider = PerformanceCounterCategoryMetadata.c_defaultPerformanceCounterProvider;
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x060019CD RID: 6605 RVA: 0x0005FD13 File Offset: 0x0005DF13
		// (set) Token: 0x060019CE RID: 6606 RVA: 0x0005FD1B File Offset: 0x0005DF1B
		public string CategorySymbol { get; private set; }

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x060019CF RID: 6607 RVA: 0x0005FD24 File Offset: 0x0005DF24
		// (set) Token: 0x060019D0 RID: 6608 RVA: 0x0005FD2C File Offset: 0x0005DF2C
		public string CategoryName { get; private set; }

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x060019D1 RID: 6609 RVA: 0x0005FD35 File Offset: 0x0005DF35
		// (set) Token: 0x060019D2 RID: 6610 RVA: 0x0005FD3D File Offset: 0x0005DF3D
		public Guid CategoryId { get; private set; }

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x060019D3 RID: 6611 RVA: 0x0005FD46 File Offset: 0x0005DF46
		// (set) Token: 0x060019D4 RID: 6612 RVA: 0x0005FD4E File Offset: 0x0005DF4E
		public Guid CategoryProvider { get; private set; }

		// Token: 0x060019D5 RID: 6613 RVA: 0x0005FD58 File Offset: 0x0005DF58
		public bool Equals(PerformanceCounterCategoryMetadata other)
		{
			return other != null && (this == other || this.CategoryId.Equals(other.CategoryId));
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x0005FD84 File Offset: 0x0005DF84
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (obj is PerformanceCounterCategoryMetadata && this.Equals((PerformanceCounterCategoryMetadata)obj)));
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x0005FDA8 File Offset: 0x0005DFA8
		public override int GetHashCode()
		{
			return this.CategoryId.GetHashCode();
		}

		// Token: 0x040008F1 RID: 2289
		private static readonly Guid c_defaultPerformanceCounterProvider = Guid.Parse("{0BB04049-2D1F-40E9-B2BB-88626B7A691F}");
	}
}
