using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000393 RID: 915
	[DataContract(Name = "DataCacheTag", Namespace = "http://schemas.microsoft.com/velocity/namespace")]
	public class DataCacheTag
	{
		// Token: 0x06002036 RID: 8246 RVA: 0x00062097 File Offset: 0x00060297
		public DataCacheTag(string tag)
			: this(tag, true)
		{
		}

		// Token: 0x06002037 RID: 8247 RVA: 0x000620A1 File Offset: 0x000602A1
		internal DataCacheTag(string tag, bool checkLength)
			: this(tag, (int)CsHash32.ComputeString(tag, 0U, true), checkLength)
		{
		}

		// Token: 0x06002038 RID: 8248 RVA: 0x000620B4 File Offset: 0x000602B4
		internal DataCacheTag(string tag, int hash, bool checkLength)
		{
			if (checkLength && DataCacheTag._encoding.GetMaxByteCount(tag.Length) > VelocityWireProtocol.MaxTagLength && DataCacheTag._encoding.GetByteCount(tag) > VelocityWireProtocol.MaxTagLength)
			{
				throw new ArgumentException(GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, "TagTooLarge"), "tag");
			}
			this._tag = tag;
			this._hash = hash;
		}

		// Token: 0x06002039 RID: 8249 RVA: 0x0006211B File Offset: 0x0006031B
		public override string ToString()
		{
			return this._tag;
		}

		// Token: 0x0600203A RID: 8250 RVA: 0x00062123 File Offset: 0x00060323
		public override bool Equals(object obj)
		{
			return obj != null && this._tag.Equals(obj.ToString(), StringComparison.Ordinal);
		}

		// Token: 0x0600203B RID: 8251 RVA: 0x0006213C File Offset: 0x0006033C
		public override int GetHashCode()
		{
			return this._hash;
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x0600203C RID: 8252 RVA: 0x00062144 File Offset: 0x00060344
		internal int Length
		{
			get
			{
				return this._tag.Length;
			}
		}

		// Token: 0x04001303 RID: 4867
		private const uint seed = 0U;

		// Token: 0x04001304 RID: 4868
		[DataMember]
		private string _tag;

		// Token: 0x04001305 RID: 4869
		[DataMember]
		private int _hash;

		// Token: 0x04001306 RID: 4870
		private static Encoding _encoding = new UTF8Encoding(false, false);
	}
}
