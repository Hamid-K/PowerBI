using System;
using System.Globalization;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Eventing.Base
{
	// Token: 0x020003B6 RID: 950
	public class EventIdentifier : IEquatable<EventIdentifier>
	{
		// Token: 0x06001D55 RID: 7509 RVA: 0x0006FE27 File Offset: 0x0006E027
		public EventIdentifier(Guid eventId, [NotNull] ElementId elementId)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<ElementId>(elementId, "elementId");
			this.EventId = eventId;
			this.ElementId = elementId;
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06001D56 RID: 7510 RVA: 0x0006FE48 File Offset: 0x0006E048
		// (set) Token: 0x06001D57 RID: 7511 RVA: 0x0006FE50 File Offset: 0x0006E050
		public Guid EventId { get; private set; }

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06001D58 RID: 7512 RVA: 0x0006FE59 File Offset: 0x0006E059
		// (set) Token: 0x06001D59 RID: 7513 RVA: 0x0006FE61 File Offset: 0x0006E061
		public ElementId ElementId { get; private set; }

		// Token: 0x06001D5A RID: 7514 RVA: 0x0006FE6C File Offset: 0x0006E06C
		public bool Equals(EventIdentifier other)
		{
			return other != null && this.EventId.Equals(other.EventId) && (this.ElementId.Equals(other.ElementId) || this.ElementId.Equals(ElementId.Any) || other.ElementId.Equals(ElementId.Any));
		}

		// Token: 0x06001D5B RID: 7515 RVA: 0x0006FECC File Offset: 0x0006E0CC
		public override int GetHashCode()
		{
			return this.ElementId.GetHashCode() ^ this.EventId.GetHashCode();
		}

		// Token: 0x06001D5C RID: 7516 RVA: 0x0006FEF9 File Offset: 0x0006E0F9
		public override bool Equals(object obj)
		{
			return this.Equals(obj as EventIdentifier);
		}

		// Token: 0x06001D5D RID: 7517 RVA: 0x0006FF07 File Offset: 0x0006E107
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "<ElementId: {0}, EventId: {1}>", new object[] { this.ElementId, this.EventId });
		}
	}
}
