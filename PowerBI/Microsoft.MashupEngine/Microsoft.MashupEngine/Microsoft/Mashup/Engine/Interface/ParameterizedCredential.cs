using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000A1 RID: 161
	public sealed class ParameterizedCredential : IResourceCredential, IEquatable<IResourceCredential>
	{
		// Token: 0x06000296 RID: 662 RVA: 0x00003FBB File Offset: 0x000021BB
		public ParameterizedCredential(string credentialName, Dictionary<string, string> values)
		{
			this.credentialName = credentialName;
			this.values = values;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x00003FD1 File Offset: 0x000021D1
		public override bool Equals(object other)
		{
			return this.Equals(other as ParameterizedCredential);
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00003FDF File Offset: 0x000021DF
		public bool Equals(IResourceCredential other)
		{
			return other is ParameterizedCredential && this.GetCacheParts().SequenceEqual(other.GetCacheParts());
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00003FFC File Offset: 0x000021FC
		public string Name
		{
			get
			{
				return this.credentialName;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600029A RID: 666 RVA: 0x00004004 File Offset: 0x00002204
		public Dictionary<string, string> Values
		{
			get
			{
				return this.values;
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000400C File Offset: 0x0000220C
		public string GetValue(string key, string defaultValue = null)
		{
			string text;
			if (this.values.TryGetValue(key, out text))
			{
				return text;
			}
			return defaultValue;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000402C File Offset: 0x0000222C
		public IEnumerable<string> GetCacheParts()
		{
			yield return this.credentialName;
			foreach (KeyValuePair<string, string> keyValuePair in this.values.OrderBy((KeyValuePair<string, string> pair) => pair.Key))
			{
				yield return keyValuePair.Key + "\n" + keyValuePair.Value;
			}
			IEnumerator<KeyValuePair<string, string>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000403C File Offset: 0x0000223C
		public override int GetHashCode()
		{
			return this.values.Values.Aggregate(0, (int h, string s) => s.GetHashCode() ^ h) ^ this.credentialName.GetHashCode();
		}

		// Token: 0x0400019C RID: 412
		private readonly string credentialName;

		// Token: 0x0400019D RID: 413
		private readonly Dictionary<string, string> values;
	}
}
