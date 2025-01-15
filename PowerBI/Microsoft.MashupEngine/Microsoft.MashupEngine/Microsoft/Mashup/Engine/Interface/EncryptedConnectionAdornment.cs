using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000A4 RID: 164
	public sealed class EncryptedConnectionAdornment : IResourceCredential, IEquatable<IResourceCredential>
	{
		// Token: 0x060002AB RID: 683 RVA: 0x0000426B File Offset: 0x0000246B
		public EncryptedConnectionAdornment(bool requireEncryption)
		{
			this.requireEncryption = requireEncryption;
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000427A File Offset: 0x0000247A
		public bool RequireEncryption
		{
			get
			{
				return this.requireEncryption;
			}
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00004282 File Offset: 0x00002482
		public override int GetHashCode()
		{
			if (!this.requireEncryption)
			{
				return -16071707;
			}
			return 16071707;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00004297 File Offset: 0x00002497
		public override bool Equals(object other)
		{
			return this.Equals(other as EncryptedConnectionAdornment);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00004297 File Offset: 0x00002497
		public bool Equals(IResourceCredential other)
		{
			return this.Equals(other as EncryptedConnectionAdornment);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x000042A5 File Offset: 0x000024A5
		private bool Equals(EncryptedConnectionAdornment other)
		{
			return other != null && this.requireEncryption == other.requireEncryption;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x000042BA File Offset: 0x000024BA
		public IEnumerable<string> GetCacheParts()
		{
			yield return this.GetHashCode().ToString(CultureInfo.InvariantCulture);
			yield break;
		}

		// Token: 0x040001A6 RID: 422
		private const int hashCode = 16071707;

		// Token: 0x040001A7 RID: 423
		private readonly bool requireEncryption;
	}
}
