using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000A9 RID: 169
	public sealed class ApplicationCredentialPropertiesAdornment : IResourceCredential, IEquatable<IResourceCredential>
	{
		// Token: 0x060002CE RID: 718 RVA: 0x000045BF File Offset: 0x000027BF
		public ApplicationCredentialPropertiesAdornment(Dictionary<string, object> properties)
		{
			this.properties = properties;
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060002CF RID: 719 RVA: 0x000045CE File Offset: 0x000027CE
		public Dictionary<string, object> Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x000045D6 File Offset: 0x000027D6
		public override int GetHashCode()
		{
			return this.properties.Values.Aggregate(0, (int h, object c) => c.GetHashCode() ^ h);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00004608 File Offset: 0x00002808
		public override bool Equals(object other)
		{
			return this.Equals(other as ApplicationCredentialPropertiesAdornment);
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00004608 File Offset: 0x00002808
		public bool Equals(IResourceCredential other)
		{
			return this.Equals(other as ApplicationCredentialPropertiesAdornment);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00004616 File Offset: 0x00002816
		private bool Equals(ApplicationCredentialPropertiesAdornment other)
		{
			return this.GetCacheParts().SequenceEqual(other.GetCacheParts());
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00004629 File Offset: 0x00002829
		public IEnumerable<string> GetCacheParts()
		{
			foreach (KeyValuePair<string, object> keyValuePair in this.properties.OrderBy((KeyValuePair<string, object> pair) => pair.Key))
			{
				string key = keyValuePair.Key;
				string text = "\n";
				object value = keyValuePair.Value;
				yield return key + text + ((value != null) ? value.ToString() : null);
			}
			IEnumerator<KeyValuePair<string, object>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x040001B5 RID: 437
		private readonly Dictionary<string, object> properties;
	}
}
