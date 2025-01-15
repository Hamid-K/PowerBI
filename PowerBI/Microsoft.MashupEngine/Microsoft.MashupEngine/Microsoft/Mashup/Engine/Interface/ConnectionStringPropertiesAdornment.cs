using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000A6 RID: 166
	public sealed class ConnectionStringPropertiesAdornment : IResourceCredential, IEquatable<IResourceCredential>
	{
		// Token: 0x060002BA RID: 698 RVA: 0x0000438F File Offset: 0x0000258F
		public ConnectionStringPropertiesAdornment(Dictionary<string, string> properties)
		{
			this.properties = properties;
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000439E File Offset: 0x0000259E
		public Dictionary<string, string> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x000043A6 File Offset: 0x000025A6
		public override int GetHashCode()
		{
			return this.properties.Values.Aggregate(0, (int h, string s) => s.GetHashCode() ^ h);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x000043D8 File Offset: 0x000025D8
		public override bool Equals(object other)
		{
			return this.Equals(other as ConnectionStringPropertiesAdornment);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x000043D8 File Offset: 0x000025D8
		public bool Equals(IResourceCredential other)
		{
			return this.Equals(other as ConnectionStringPropertiesAdornment);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x000043E6 File Offset: 0x000025E6
		private bool Equals(ConnectionStringPropertiesAdornment other)
		{
			return this.GetCacheParts().SequenceEqual(other.GetCacheParts());
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x000043F9 File Offset: 0x000025F9
		public IEnumerable<string> GetCacheParts()
		{
			foreach (KeyValuePair<string, string> keyValuePair in this.properties.OrderBy((KeyValuePair<string, string> pair) => pair.Key))
			{
				yield return keyValuePair.Key + "\n" + keyValuePair.Value;
			}
			IEnumerator<KeyValuePair<string, string>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x040001AC RID: 428
		private readonly Dictionary<string, string> properties;
	}
}
