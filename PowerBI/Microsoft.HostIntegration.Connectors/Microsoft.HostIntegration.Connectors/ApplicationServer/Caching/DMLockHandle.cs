using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000332 RID: 818
	[DataContract(Name = "DMLockHandle", Namespace = "http://schemas.microsoft.com/velocity/namespace")]
	internal struct DMLockHandle
	{
		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001D8C RID: 7564 RVA: 0x000591DA File Offset: 0x000573DA
		// (set) Token: 0x06001D8D RID: 7565 RVA: 0x000591E2 File Offset: 0x000573E2
		internal int LockID
		{
			get
			{
				return this._lockId;
			}
			set
			{
				this._lockId = value;
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001D8E RID: 7566 RVA: 0x000591EB File Offset: 0x000573EB
		internal bool IsValid
		{
			get
			{
				return this._lockId > 0;
			}
		}

		// Token: 0x06001D8F RID: 7567 RVA: 0x000591F6 File Offset: 0x000573F6
		internal void FreeLockID()
		{
			if (this.IsValid)
			{
				this._lockId *= -1;
			}
		}

		// Token: 0x06001D90 RID: 7568 RVA: 0x00059210 File Offset: 0x00057410
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "LockId = {0}", new object[] { this._lockId });
		}

		// Token: 0x06001D91 RID: 7569 RVA: 0x00059242 File Offset: 0x00057442
		public void ReadStream(ISerializationReader reader)
		{
			this._lockId = reader.ReadInt32();
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x00059250 File Offset: 0x00057450
		public void WriteStream(ISerializationWriter writer)
		{
			writer.Write(this._lockId);
		}

		// Token: 0x06001D93 RID: 7571 RVA: 0x000373C9 File Offset: 0x000355C9
		public int GetSerializedSize()
		{
			return 4;
		}

		// Token: 0x0400104F RID: 4175
		[DataMember]
		private int _lockId;
	}
}
