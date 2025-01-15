using System;
using System.Runtime.Serialization;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x0200049E RID: 1182
	[DataContract]
	[KnownType(typeof(RetryState))]
	[CannotApplyEqualityOperator]
	public class EndpointIdentifier : IEquatable<EndpointIdentifier>
	{
		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06002473 RID: 9331 RVA: 0x000831E0 File Offset: 0x000813E0
		// (set) Token: 0x06002474 RID: 9332 RVA: 0x000831E8 File Offset: 0x000813E8
		[DataMember]
		public Uri Uri { get; private set; }

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06002475 RID: 9333 RVA: 0x000831F1 File Offset: 0x000813F1
		// (set) Token: 0x06002476 RID: 9334 RVA: 0x000831F9 File Offset: 0x000813F9
		[DataMember]
		public string Identifier { get; private set; }

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06002477 RID: 9335 RVA: 0x00083202 File Offset: 0x00081402
		// (set) Token: 0x06002478 RID: 9336 RVA: 0x0008320A File Offset: 0x0008140A
		[DataMember]
		internal object State { get; set; }

		// Token: 0x06002479 RID: 9337 RVA: 0x00083213 File Offset: 0x00081413
		public EndpointIdentifier(Uri uri, string identifier, object state)
		{
			this.Uri = uri;
			this.Identifier = identifier;
			this.State = state;
		}

		// Token: 0x0600247A RID: 9338 RVA: 0x00083230 File Offset: 0x00081430
		public bool Equals(EndpointIdentifier other)
		{
			return other != null && object.Equals(this.Uri, other.Uri);
		}

		// Token: 0x0600247B RID: 9339 RVA: 0x00083248 File Offset: 0x00081448
		public override bool Equals(object other)
		{
			return this.Equals(other as EndpointIdentifier);
		}

		// Token: 0x0600247C RID: 9340 RVA: 0x00083256 File Offset: 0x00081456
		public override int GetHashCode()
		{
			return this.Uri.GetHashCode();
		}

		// Token: 0x0600247D RID: 9341 RVA: 0x00083263 File Offset: 0x00081463
		public override string ToString()
		{
			return this.Uri.ToString();
		}
	}
}
