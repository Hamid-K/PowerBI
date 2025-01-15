using System;
using System.Globalization;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Eventing.Base
{
	// Token: 0x020003CB RID: 971
	public class EventMetadata
	{
		// Token: 0x06001DF8 RID: 7672 RVA: 0x00071820 File Offset: 0x0006FA20
		public EventMetadata(Guid id, [NotNull] string name, [NotNull] Type type, Guid packageId, int index, EventAttributes attributes)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(name, "name");
			ExtendedDiagnostics.EnsureArgumentNotNull<Type>(type, "type");
			ExtendedDiagnostics.EnsureArgumentIsNotNegative(index, "index");
			this.Id = id;
			this.Name = name;
			this.EventType = type;
			this.PackageId = packageId;
			this.Index = index;
			this.Attributes = attributes;
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06001DF9 RID: 7673 RVA: 0x00071882 File Offset: 0x0006FA82
		// (set) Token: 0x06001DFA RID: 7674 RVA: 0x0007188A File Offset: 0x0006FA8A
		public Guid Id { get; private set; }

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06001DFB RID: 7675 RVA: 0x00071893 File Offset: 0x0006FA93
		// (set) Token: 0x06001DFC RID: 7676 RVA: 0x0007189B File Offset: 0x0006FA9B
		public int Index { get; private set; }

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001DFD RID: 7677 RVA: 0x000718A4 File Offset: 0x0006FAA4
		// (set) Token: 0x06001DFE RID: 7678 RVA: 0x000718AC File Offset: 0x0006FAAC
		public Type EventType { get; private set; }

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06001DFF RID: 7679 RVA: 0x000718B5 File Offset: 0x0006FAB5
		// (set) Token: 0x06001E00 RID: 7680 RVA: 0x000718BD File Offset: 0x0006FABD
		public string Name { get; private set; }

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06001E01 RID: 7681 RVA: 0x000718C6 File Offset: 0x0006FAC6
		// (set) Token: 0x06001E02 RID: 7682 RVA: 0x000718CE File Offset: 0x0006FACE
		public Guid PackageId { get; private set; }

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06001E03 RID: 7683 RVA: 0x000718D7 File Offset: 0x0006FAD7
		// (set) Token: 0x06001E04 RID: 7684 RVA: 0x000718DF File Offset: 0x0006FADF
		public EventAttributes Attributes { get; private set; }

		// Token: 0x06001E05 RID: 7685 RVA: 0x000718E8 File Offset: 0x0006FAE8
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "<Id: {0} Name: {1} Index: {2}>", new object[] { this.Id, this.Name, this.Index });
		}

		// Token: 0x06001E06 RID: 7686 RVA: 0x00071924 File Offset: 0x0006FB24
		internal bool Matches(EventIdentifier eid)
		{
			return eid.EventId == this.Id;
		}
	}
}
