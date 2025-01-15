using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DE3 RID: 3555
	[DataContract]
	internal abstract class CdpaOperation : IEquatable<CdpaOperation>
	{
		// Token: 0x17001C60 RID: 7264
		// (get) Token: 0x06006017 RID: 24599
		[DataMember(Name = "name")]
		public abstract string Name { get; }

		// Token: 0x06006018 RID: 24600
		public abstract override int GetHashCode();

		// Token: 0x06006019 RID: 24601
		public abstract bool Equals(CdpaOperation other);

		// Token: 0x0600601A RID: 24602 RVA: 0x001494F1 File Offset: 0x001476F1
		public override bool Equals(object other)
		{
			return this.Equals(other as CdpaOperation);
		}
	}
}
