using System;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x0200049C RID: 1180
	[CannotApplyEqualityOperator]
	internal class EndpointDifferentiator : IEquatable<EndpointDifferentiator>
	{
		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06002461 RID: 9313 RVA: 0x00083080 File Offset: 0x00081280
		// (set) Token: 0x06002462 RID: 9314 RVA: 0x00083088 File Offset: 0x00081288
		public Uri Uri { get; set; }

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06002463 RID: 9315 RVA: 0x00083091 File Offset: 0x00081291
		// (set) Token: 0x06002464 RID: 9316 RVA: 0x00083099 File Offset: 0x00081299
		public int Index { get; set; }

		// Token: 0x06002465 RID: 9317 RVA: 0x000830A2 File Offset: 0x000812A2
		public EndpointDifferentiator(Uri uri)
		{
			this.Uri = uri;
			this.Index = 0;
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x000830B8 File Offset: 0x000812B8
		public override bool Equals(object obj)
		{
			return this.Equals(obj as EndpointDifferentiator);
		}

		// Token: 0x06002467 RID: 9319 RVA: 0x000830C8 File Offset: 0x000812C8
		public bool Equals(EndpointDifferentiator other)
		{
			return other != null && this.Uri.Equals(other.Uri) && this.Index.Equals(other.Index);
		}

		// Token: 0x06002468 RID: 9320 RVA: 0x00083104 File Offset: 0x00081304
		public override int GetHashCode()
		{
			return this.Uri.GetHashCode() ^ this.Index.GetHashCode();
		}
	}
}
