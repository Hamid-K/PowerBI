using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Caching;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Wrangling.Tree
{
	// Token: 0x020000D9 RID: 217
	[DataContract]
	public class Attributes : IEquatable<Attributes>
	{
		// Token: 0x060004C9 RID: 1225 RVA: 0x00010638 File Offset: 0x0000E838
		public static void AddKnownSoftAttributes(HashSet<string> newSoftAttributes)
		{
			if (Attributes._knownSoftAttributes.IsSupersetOf(newSoftAttributes))
			{
				return;
			}
			bool flag = false;
			while (!flag)
			{
				HashSet<string> knownSoftAttributes = Attributes._knownSoftAttributes;
				HashSet<string> hashSet = knownSoftAttributes.Concat(newSoftAttributes).ConvertToHashSet<string>();
				flag = Interlocked.CompareExchange<HashSet<string>>(ref Attributes._knownSoftAttributes, hashSet, knownSoftAttributes) == knownSoftAttributes;
			}
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0001067D File Offset: 0x0000E87D
		public static void AddKnownSoftAttribute(string newSoftAttribute)
		{
			if (Attributes._knownSoftAttributes.Contains(newSoftAttribute))
			{
				return;
			}
			Attributes.AddKnownSoftAttributes(newSoftAttribute.Yield<string>().ConvertToHashSet<string>());
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x000106A0 File Offset: 0x0000E8A0
		public Attributes(IEnumerable<Attributes.Attribute> attributes)
		{
			this._attributes = attributes.Distinct<Attributes.Attribute>().ToArray<Attributes.Attribute>();
			if (!this._attributes.Any((Attributes.Attribute a) => a.Name == null))
			{
				if (!this._attributes.Any((Attributes.Attribute a) => a.Value == null))
				{
					return;
				}
			}
			throw new ArgumentException("Attributes should have non-null key and value.");
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x00010727 File Offset: 0x0000E927
		[JsonConstructor]
		public Attributes(params Attributes.Attribute[] attributes)
			: this(Seq.Of<Attributes.Attribute>(attributes))
		{
		}

		// Token: 0x17000164 RID: 356
		public string this[string name]
		{
			get
			{
				for (int i = 0; i < this._attributes.Length; i++)
				{
					if (this._attributes[i].Name == name)
					{
						return this._attributes[i].Value;
					}
				}
				throw new ArgumentException("Invalid indexer value name");
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x00010790 File Offset: 0x0000E990
		public Attributes.Attribute[] StrongAttributes
		{
			get
			{
				if (this._strongAttributes == null)
				{
					this._strongAttributes = this.AllAttributes.Where((Attributes.Attribute e) => !Attributes._knownSoftAttributes.Contains(e.Name)).ToArray<Attributes.Attribute>();
				}
				return this._strongAttributes;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x000107E0 File Offset: 0x0000E9E0
		public Attributes.Attribute[] SoftAttributes
		{
			get
			{
				if (this._softAttributes == null)
				{
					this._softAttributes = this.AllAttributes.Where((Attributes.Attribute e) => Attributes._knownSoftAttributes.Contains(e.Name)).ToArray<Attributes.Attribute>();
				}
				return this._softAttributes;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x00010830 File Offset: 0x0000EA30
		public Attributes.Attribute[] AllAttributes
		{
			get
			{
				return this._attributes;
			}
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00010838 File Offset: 0x0000EA38
		public bool TryGetValue(string key, out string value)
		{
			value = null;
			for (int i = 0; i < this._attributes.Length; i++)
			{
				if (this._attributes[i].Name == key)
				{
					value = this._attributes[i].Value;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0001088C File Offset: 0x0000EA8C
		public Optional<string> MaybeGet(string key)
		{
			string text;
			return this.TryGetValue(key, out text).Then(text);
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x000108A8 File Offset: 0x0000EAA8
		public bool Equals(Attributes other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			if (this.StrongAttributes.Length != other.StrongAttributes.Length)
			{
				return false;
			}
			if (this.StrongAttributes.Length == 1)
			{
				return this.StrongAttributes[0].Equals(other.StrongAttributes[0]);
			}
			int num = this.StrongAttributes.Length;
			for (int i = 0; i < num; i++)
			{
				bool flag = false;
				for (int j = 0; j < num; j++)
				{
					if (other.StrongAttributes[j].Equals(this.StrongAttributes[i]))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00010949 File Offset: 0x0000EB49
		public override bool Equals(object obj)
		{
			return obj != null && (this == obj || (!(obj.GetType() != base.GetType()) && this.Equals((Attributes)obj)));
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00010978 File Offset: 0x0000EB78
		public override int GetHashCode()
		{
			if (this._hashCodeCache == 0)
			{
				Attributes.Attribute[] strongAttributes = this.StrongAttributes;
				int num = 0;
				for (int i = 0; i < strongAttributes.Length; i++)
				{
					num ^= strongAttributes[i].GetHashCode();
				}
				this._hashCodeCache = num;
			}
			return this._hashCodeCache;
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0000BE9E File Offset: 0x0000A09E
		public static bool operator ==(Attributes left, Attributes right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0000BEA7 File Offset: 0x0000A0A7
		public static bool operator !=(Attributes left, Attributes right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x000109C6 File Offset: 0x0000EBC6
		public override string ToString()
		{
			return string.Join(", ", this._attributes.Select((Attributes.Attribute c) => c.ToString()));
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x000109FC File Offset: 0x0000EBFC
		public Attributes With(string name, Func<string, string> valueTransform, string missingValue)
		{
			List<Attributes.Attribute> list = new List<Attributes.Attribute>();
			bool flag = false;
			foreach (Attributes.Attribute attribute in this.AllAttributes)
			{
				if (attribute.Name != name)
				{
					list.Add(attribute);
				}
				else
				{
					list.Add(Attributes.Attribute.Create(attribute.Name, valueTransform(attribute.Value)));
					flag = true;
				}
			}
			if (!flag)
			{
				list.Add(Attributes.Attribute.Create(name, missingValue));
			}
			return new Attributes(list);
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x00010A7F File Offset: 0x0000EC7F
		public static Attributes Empty { get; } = new Attributes(Array.Empty<Attributes.Attribute>());

		// Token: 0x0400020A RID: 522
		[DataMember]
		private readonly Attributes.Attribute[] _attributes;

		// Token: 0x0400020B RID: 523
		private static HashSet<string> _knownSoftAttributes = new HashSet<string>();

		// Token: 0x0400020C RID: 524
		private Attributes.Attribute[] _strongAttributes;

		// Token: 0x0400020D RID: 525
		private Attributes.Attribute[] _softAttributes;

		// Token: 0x0400020E RID: 526
		private int _hashCodeCache;

		// Token: 0x020000DA RID: 218
		[DataContract]
		public struct Attribute : IEquatable<Attributes.Attribute>
		{
			// Token: 0x060004DC RID: 1244 RVA: 0x00010AA1 File Offset: 0x0000ECA1
			public static Attributes.Attribute Create(string name, string value)
			{
				return new Attributes.Attribute(name, value);
			}

			// Token: 0x060004DD RID: 1245 RVA: 0x00010AAC File Offset: 0x0000ECAC
			public static Attributes.Attribute Create(string name, string value, ConcurrentLruCache<Record<string, string>, Attributes.Attribute> cache)
			{
				Record<string, string> record = new Record<string, string>(name, value);
				Attributes.Attribute attribute;
				if (cache.Lookup(record, out attribute))
				{
					return attribute;
				}
				attribute = new Attributes.Attribute(name, value);
				cache.Add(record, attribute);
				return attribute;
			}

			// Token: 0x060004DE RID: 1246 RVA: 0x00010AE1 File Offset: 0x0000ECE1
			private Attribute(string name, string value)
			{
				this.Name = name;
				this.Value = value;
				this._hashcode = null;
			}

			// Token: 0x060004DF RID: 1247 RVA: 0x00010B00 File Offset: 0x0000ED00
			public override bool Equals(object obj)
			{
				if (obj is Attributes.Attribute)
				{
					Attributes.Attribute attribute = (Attributes.Attribute)obj;
					return this.Equals(attribute);
				}
				return false;
			}

			// Token: 0x060004E0 RID: 1248 RVA: 0x00010B25 File Offset: 0x0000ED25
			public bool Equals(Attributes.Attribute other)
			{
				return this.Name == other.Name && this.Value == other.Value;
			}

			// Token: 0x060004E1 RID: 1249 RVA: 0x00010B4D File Offset: 0x0000ED4D
			public static bool operator !=(Attributes.Attribute left, Attributes.Attribute right)
			{
				return !object.Equals(left, right);
			}

			// Token: 0x060004E2 RID: 1250 RVA: 0x00010B63 File Offset: 0x0000ED63
			public static bool operator ==(Attributes.Attribute left, Attributes.Attribute right)
			{
				return object.Equals(left, right);
			}

			// Token: 0x060004E3 RID: 1251 RVA: 0x00010B78 File Offset: 0x0000ED78
			public override int GetHashCode()
			{
				if (this._hashcode != null)
				{
					return this._hashcode.Value;
				}
				int num = -244751520;
				num = num * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.Name);
				num = num * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.Value);
				this._hashcode = new int?(num);
				return num;
			}

			// Token: 0x060004E4 RID: 1252 RVA: 0x00010BE3 File Offset: 0x0000EDE3
			public override string ToString()
			{
				return string.Concat(new string[] { "\"", this.Name, "\": \"", this.Value, "\"" });
			}

			// Token: 0x04000210 RID: 528
			[DataMember]
			public readonly string Name;

			// Token: 0x04000211 RID: 529
			[DataMember]
			public readonly string Value;

			// Token: 0x04000212 RID: 530
			private int? _hashcode;

			// Token: 0x020000DB RID: 219
			public static class WellKnownAttributes
			{
				// Token: 0x04000213 RID: 531
				public static readonly Attributes.Attribute EmptyValueAttribute = new Attributes.Attribute("value", string.Empty);
			}
		}

		// Token: 0x020000DC RID: 220
		public class StrongComparer : IEqualityComparer<Attributes>
		{
			// Token: 0x060004E6 RID: 1254 RVA: 0x00002130 File Offset: 0x00000330
			private StrongComparer()
			{
			}

			// Token: 0x17000169 RID: 361
			// (get) Token: 0x060004E7 RID: 1255 RVA: 0x00010C30 File Offset: 0x0000EE30
			public static Attributes.StrongComparer Default
			{
				get
				{
					Attributes.StrongComparer strongComparer;
					if ((strongComparer = Attributes.StrongComparer._default) == null)
					{
						strongComparer = (Attributes.StrongComparer._default = new Attributes.StrongComparer());
					}
					return strongComparer;
				}
			}

			// Token: 0x060004E8 RID: 1256 RVA: 0x00010C48 File Offset: 0x0000EE48
			public bool Equals(Attributes x, Attributes y)
			{
				if (x == y)
				{
					return true;
				}
				if (x == null)
				{
					return false;
				}
				if (y == null)
				{
					return false;
				}
				if (x.AllAttributes.Length != y.AllAttributes.Length)
				{
					return false;
				}
				if (x.AllAttributes.Length == 1)
				{
					return y.AllAttributes[0].Equals(x.AllAttributes[0]);
				}
				for (int i = 0; i < x.AllAttributes.Length; i++)
				{
					if (!y.AllAttributes.Contains(x.AllAttributes[i]))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x060004E9 RID: 1257 RVA: 0x00010CD4 File Offset: 0x0000EED4
			public int GetHashCode(Attributes n)
			{
				int num = 0;
				for (int i = 0; i < n.AllAttributes.Length; i++)
				{
					num ^= n.AllAttributes[i].GetHashCode();
				}
				return num;
			}

			// Token: 0x04000214 RID: 532
			private static Attributes.StrongComparer _default;
		}
	}
}
