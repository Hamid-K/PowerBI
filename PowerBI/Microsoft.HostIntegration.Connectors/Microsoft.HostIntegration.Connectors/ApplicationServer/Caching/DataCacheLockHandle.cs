using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000331 RID: 817
	[DataContract(Name = "DataCacheLockHandle", Namespace = "http://schemas.microsoft.com/velocity/namespace")]
	public class DataCacheLockHandle
	{
		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001D85 RID: 7557 RVA: 0x00059146 File Offset: 0x00057346
		// (set) Token: 0x06001D86 RID: 7558 RVA: 0x0005914E File Offset: 0x0005734E
		internal DMLockHandle Handle
		{
			get
			{
				return this._handle;
			}
			set
			{
				this._handle = value;
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06001D87 RID: 7559 RVA: 0x00059157 File Offset: 0x00057357
		// (set) Token: 0x06001D88 RID: 7560 RVA: 0x0005915F File Offset: 0x0005735F
		internal InternalCacheItemVersion Version
		{
			get
			{
				return this._version;
			}
			set
			{
				this._version = value;
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001D89 RID: 7561 RVA: 0x00059168 File Offset: 0x00057368
		internal bool IsValid
		{
			get
			{
				return this.Handle.IsValid;
			}
		}

		// Token: 0x06001D8A RID: 7562 RVA: 0x00059184 File Offset: 0x00057384
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0}:{1}", new object[] { this._version, this._handle });
		}

		// Token: 0x06001D8B RID: 7563 RVA: 0x000591C4 File Offset: 0x000573C4
		internal DataCacheLockHandle(DMLockHandle handle, InternalCacheItemVersion version)
		{
			this._handle = handle;
			this._version = version;
		}

		// Token: 0x0400104D RID: 4173
		[DataMember]
		private DMLockHandle _handle;

		// Token: 0x0400104E RID: 4174
		[DataMember]
		private InternalCacheItemVersion _version;
	}
}
