using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Wrangling.Tree
{
	// Token: 0x020000DE RID: 222
	[DataContract]
	public class UnaryPredicates : IEquatable<UnaryPredicates>
	{
		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x00010D69 File Offset: 0x0000EF69
		// (set) Token: 0x060004F2 RID: 1266 RVA: 0x00010D71 File Offset: 0x0000EF71
		[DataMember]
		public bool IsNullable { get; private set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x00010D7A File Offset: 0x0000EF7A
		// (set) Token: 0x060004F4 RID: 1268 RVA: 0x00010D82 File Offset: 0x0000EF82
		[DataMember]
		public string Type { get; private set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x00010D8B File Offset: 0x0000EF8B
		// (set) Token: 0x060004F6 RID: 1270 RVA: 0x00010D93 File Offset: 0x0000EF93
		[DataMember]
		public UnaryPredicates.Length MatchLength { get; private set; }

		// Token: 0x060004F7 RID: 1271 RVA: 0x00010D9C File Offset: 0x0000EF9C
		private UnaryPredicates(bool isNullable, string type, UnaryPredicates.Length matchLength)
		{
			this.IsNullable = isNullable;
			this.Type = type;
			this.MatchLength = matchLength;
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x00010DB9 File Offset: 0x0000EFB9
		public static UnaryPredicates Empty
		{
			get
			{
				return new UnaryPredicates(false, "", UnaryPredicates.Length.One);
			}
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00010DC8 File Offset: 0x0000EFC8
		public static UnaryPredicates From(IEnumerable<Node> nodes)
		{
			IReadOnlyList<Node> readOnlyList = (nodes as IReadOnlyList<Node>) ?? nodes.ToList<Node>();
			int count = readOnlyList.Count;
			UnaryPredicates unaryPredicates;
			if (count != 0)
			{
				if (count != 1)
				{
					unaryPredicates = new UnaryPredicates(false, "Any", UnaryPredicates.Length.Many);
				}
				else
				{
					unaryPredicates = new UnaryPredicates(false, readOnlyList[0].Label, UnaryPredicates.Length.One);
				}
			}
			else
			{
				unaryPredicates = new UnaryPredicates(true, "", UnaryPredicates.Length.One);
			}
			return unaryPredicates;
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00010E29 File Offset: 0x0000F029
		public bool Equals(UnaryPredicates obj)
		{
			return this == obj || (obj != null && this.IsNullable == obj.IsNullable && this.Type == obj.Type && this.MatchLength == obj.MatchLength);
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x00010E65 File Offset: 0x0000F065
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((UnaryPredicates)obj)));
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x00010E94 File Offset: 0x0000F094
		public override int GetHashCode()
		{
			return ((-244751520 * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.IsNullable)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.Type)) * -1521134295 + EqualityComparer<UnaryPredicates.Length>.Default.GetHashCode(this.MatchLength);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x00010EEB File Offset: 0x0000F0EB
		public override string ToString()
		{
			return string.Format("IsNullable: {0}, Type:{1}, MatchLength: {2}", this.IsNullable, this.Type, this.MatchLength);
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00010F14 File Offset: 0x0000F114
		internal void Serialize(JsonWriter jsonWriter)
		{
			jsonWriter.WriteStartObject();
			jsonWriter.WritePropertyName("IsNullable");
			jsonWriter.WriteValue(this.IsNullable);
			jsonWriter.WritePropertyName("Type");
			jsonWriter.WriteValue(this.Type);
			jsonWriter.WritePropertyName("MatchLength");
			jsonWriter.WriteValue(this.MatchLength);
			jsonWriter.WriteEndObject();
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00010F78 File Offset: 0x0000F178
		internal static UnaryPredicates Deserialize(JObject predicateJson)
		{
			bool flag = predicateJson.Value<bool>("IsNullable");
			string text = predicateJson.Value<string>("Type");
			UnaryPredicates.Length length = (UnaryPredicates.Length)predicateJson.Value<byte>("MatchLength");
			return new UnaryPredicates(flag, text, length);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00010FAF File Offset: 0x0000F1AF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool Join(bool isNullable1, bool isNullable2)
		{
			return isNullable1 || isNullable2;
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00010FB4 File Offset: 0x0000F1B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static string Join(string type1, string type2)
		{
			if (type1 == "")
			{
				return type2;
			}
			if (type2 == "")
			{
				return type1;
			}
			if (!(type1 == type2))
			{
				return "Any";
			}
			return type1;
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00010FE4 File Offset: 0x0000F1E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static UnaryPredicates.Length Join(UnaryPredicates.Length len1, UnaryPredicates.Length len2)
		{
			if (len1 != UnaryPredicates.Length.Many && len2 != UnaryPredicates.Length.Many)
			{
				return UnaryPredicates.Length.One;
			}
			return UnaryPredicates.Length.Many;
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00010FF4 File Offset: 0x0000F1F4
		public UnaryPredicates Generalize(Node other)
		{
			if (other == null)
			{
				if (this.IsNullable)
				{
					return this;
				}
				return new UnaryPredicates(true, this.Type, this.MatchLength);
			}
			else
			{
				HoleNode holeNode = other as HoleNode;
				if (holeNode != null)
				{
					UnaryPredicates predicates = holeNode.Predicates;
					return new UnaryPredicates(UnaryPredicates.Join(this.IsNullable, predicates.IsNullable), UnaryPredicates.Join(this.Type, predicates.Type), UnaryPredicates.Join(this.MatchLength, predicates.MatchLength));
				}
				return new UnaryPredicates(this.IsNullable, UnaryPredicates.Join(this.Type, other.Label), this.MatchLength);
			}
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x00011090 File Offset: 0x0000F290
		public bool Check(Node node)
		{
			if (node == null)
			{
				return this.IsNullable;
			}
			HoleNode holeNode = node as HoleNode;
			if (holeNode != null)
			{
				UnaryPredicates predicates = holeNode.Predicates;
				return this.IsNullable == UnaryPredicates.Join(this.IsNullable, predicates.IsNullable) && this.Type == UnaryPredicates.Join(this.Type, predicates.Type) && this.MatchLength == UnaryPredicates.Join(this.MatchLength, predicates.MatchLength);
			}
			return this.Type == "Any" || node.Label == this.Type;
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00011130 File Offset: 0x0000F330
		public bool Check(Node[] node)
		{
			int num = node.Length;
			bool flag;
			if (num != 0)
			{
				if (num != 1)
				{
					flag = this.MatchLength == UnaryPredicates.Length.Many;
				}
				else
				{
					flag = this.Check(node[0]);
				}
			}
			else
			{
				flag = this.IsNullable;
			}
			return flag;
		}

		// Token: 0x020000DF RID: 223
		[JsonConverter(typeof(StringEnumConverter))]
		[DataContract]
		public enum Length : byte
		{
			// Token: 0x0400021F RID: 543
			[EnumMember(Value = "One")]
			One,
			// Token: 0x04000220 RID: 544
			[EnumMember(Value = "Many")]
			Many
		}

		// Token: 0x020000E0 RID: 224
		public class SpecialTokens
		{
			// Token: 0x04000221 RID: 545
			public const string Any = "Any";

			// Token: 0x04000222 RID: 546
			public const string NA = "";
		}
	}
}
