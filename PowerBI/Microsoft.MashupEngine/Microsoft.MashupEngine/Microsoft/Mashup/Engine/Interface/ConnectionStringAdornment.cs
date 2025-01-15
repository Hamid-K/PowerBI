using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000AC RID: 172
	public sealed class ConnectionStringAdornment : IResourceCredential, IEquatable<IResourceCredential>
	{
		// Token: 0x060002E2 RID: 738 RVA: 0x00004803 File Offset: 0x00002A03
		public ConnectionStringAdornment(string connectionString)
		{
			this.connectionString = connectionString;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x00004812 File Offset: 0x00002A12
		public string ConnectionString
		{
			get
			{
				return this.connectionString;
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000481A File Offset: 0x00002A1A
		public override int GetHashCode()
		{
			return this.connectionString.GetHashCode();
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00004827 File Offset: 0x00002A27
		public override bool Equals(object other)
		{
			return this.Equals(other as ConnectionStringAdornment);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00004827 File Offset: 0x00002A27
		public bool Equals(IResourceCredential other)
		{
			return this.Equals(other as ConnectionStringAdornment);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00004835 File Offset: 0x00002A35
		private bool Equals(ConnectionStringAdornment other)
		{
			return other != null && this.connectionString == other.connectionString;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000484D File Offset: 0x00002A4D
		public IEnumerable<string> GetCacheParts()
		{
			yield return this.GetHashCode().ToString(CultureInfo.InvariantCulture);
			yield break;
		}

		// Token: 0x040001BE RID: 446
		private readonly string connectionString;
	}
}
