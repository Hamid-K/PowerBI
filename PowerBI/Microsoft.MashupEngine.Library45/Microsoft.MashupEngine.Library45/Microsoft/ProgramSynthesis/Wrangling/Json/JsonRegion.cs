using System;
using System.Diagnostics;
using System.Runtime.Serialization;
using Microsoft.ProgramSynthesis.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Wrangling.Json
{
	// Token: 0x02000188 RID: 392
	[DebuggerDisplay("{Value}: {Start}-{End}")]
	[DataContract]
	public class JsonRegion : IRegion<JsonRegion>, IComparable<JsonRegion>, IEquatable<JsonRegion>
	{
		// Token: 0x06000887 RID: 2183 RVA: 0x00019FF4 File Offset: 0x000181F4
		private JsonRegion(JToken token, string value, uint start, uint end)
		{
			this.Token = token;
			this.Value = value;
			this.Start = start;
			this.End = end;
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000888 RID: 2184 RVA: 0x0001A019 File Offset: 0x00018219
		public JToken Document
		{
			get
			{
				return this.Token.Root;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000889 RID: 2185 RVA: 0x0001A026 File Offset: 0x00018226
		[DataMember]
		public JToken Token { get; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600088A RID: 2186 RVA: 0x0001A02E File Offset: 0x0001822E
		[DataMember]
		public uint Start { get; }

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x0001A036 File Offset: 0x00018236
		[DataMember]
		public uint End { get; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x0600088C RID: 2188 RVA: 0x0001A03E File Offset: 0x0001823E
		public string Value { get; }

		// Token: 0x0600088D RID: 2189 RVA: 0x0001A048 File Offset: 0x00018248
		public static JsonRegion Create(JToken token)
		{
			string text = JsonRegion.TokenToString(token);
			return new JsonRegion(token, text, 0U, (uint)text.Length);
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x0001A06C File Offset: 0x0001826C
		public static JsonRegion Create(JToken token, uint start, uint end)
		{
			string text = JsonRegion.TokenToString(token);
			if ((ulong)start > (ulong)((long)text.Length) || (ulong)end > (ulong)((long)text.Length))
			{
				return null;
			}
			return new JsonRegion(token, text.Substring((int)start, (int)(end - start)), start, end);
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x0001A0AA File Offset: 0x000182AA
		private static string TokenToString(JToken token)
		{
			if (!(token is JValue))
			{
				return token.ToString(Formatting.None, Array.Empty<JsonConverter>());
			}
			return token.ToString();
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x0001A0C7 File Offset: 0x000182C7
		public bool Equals(JsonRegion other)
		{
			return other != null && (this == other || (this.Token == other.Token && this.Start == other.Start && this.End == other.End));
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x0001A100 File Offset: 0x00018300
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((JsonRegion)obj)));
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x0001A130 File Offset: 0x00018330
		public override int GetHashCode()
		{
			if (this._hashCode != null)
			{
				return this._hashCode.Value;
			}
			JToken token = this.Token;
			int num = ((token != null) ? token.GetHashCode() : 0);
			num = (num * 1613593) ^ (int)this.Start;
			num = (num * 1613593) ^ (int)this.End;
			this._hashCode = new int?(num);
			return this._hashCode.Value;
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0000BE9E File Offset: 0x0000A09E
		public static bool operator ==(JsonRegion left, JsonRegion right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x0000BEA7 File Offset: 0x0000A0A7
		public static bool operator !=(JsonRegion left, JsonRegion right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x000170F6 File Offset: 0x000152F6
		public bool Contains(JsonRegion other)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x000170F6 File Offset: 0x000152F6
		public bool IntersectNonEmpty(JsonRegion other)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x000170F6 File Offset: 0x000152F6
		public bool IsBefore(JsonRegion other)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x000170F6 File Offset: 0x000152F6
		public JsonRegion ClipBefore(JsonRegion other)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x000170F6 File Offset: 0x000152F6
		public int CompareTo(JsonRegion other)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000445 RID: 1093
		private int? _hashCode;
	}
}
