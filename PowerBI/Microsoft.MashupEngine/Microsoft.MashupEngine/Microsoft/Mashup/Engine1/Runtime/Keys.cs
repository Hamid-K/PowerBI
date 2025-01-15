using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200135E RID: 4958
	public abstract class Keys : IKeys, IEnumerable<string>, IEnumerable, IEquatable<Keys>
	{
		// Token: 0x1700232E RID: 9006
		// (get) Token: 0x06008288 RID: 33416
		public abstract int Length { get; }

		// Token: 0x1700232F RID: 9007
		public abstract string this[int index] { get; }

		// Token: 0x0600828A RID: 33418
		public abstract bool Contains(string key);

		// Token: 0x0600828B RID: 33419 RVA: 0x001BAB78 File Offset: 0x001B8D78
		public bool Equals(Keys other)
		{
			if (this == other)
			{
				return true;
			}
			if (this.Length != other.Length)
			{
				return false;
			}
			for (int i = 0; i < this.Length; i++)
			{
				if (this[i] != other[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600828C RID: 33420 RVA: 0x001BABC4 File Offset: 0x001B8DC4
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Keys);
		}

		// Token: 0x0600828D RID: 33421 RVA: 0x001BABD2 File Offset: 0x001B8DD2
		public Keys.StringKeysEnumerator GetEnumerator()
		{
			return new Keys.StringKeysEnumerator(this);
		}

		// Token: 0x0600828E RID: 33422 RVA: 0x001BABDC File Offset: 0x001B8DDC
		public override int GetHashCode()
		{
			int num = this.Length;
			for (int i = 0; i < this.Length; i++)
			{
				num = num * 29 + this[i].GetHashCode();
			}
			return num;
		}

		// Token: 0x0600828F RID: 33423 RVA: 0x001BAC14 File Offset: 0x001B8E14
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new Keys.StringKeysEnumerator(this);
		}

		// Token: 0x06008290 RID: 33424 RVA: 0x001BAC14 File Offset: 0x001B8E14
		IEnumerator<string> IEnumerable<string>.GetEnumerator()
		{
			return new Keys.StringKeysEnumerator(this);
		}

		// Token: 0x06008291 RID: 33425 RVA: 0x001BAC24 File Offset: 0x001B8E24
		public static Keys New(int length, Func<int, string> getKey)
		{
			string[] array = new string[length];
			for (int i = 0; i < length; i++)
			{
				array[i] = getKey(i);
			}
			return Keys.New(array);
		}

		// Token: 0x06008292 RID: 33426 RVA: 0x001BAC54 File Offset: 0x001B8E54
		public static Keys New(string key0)
		{
			return new Keys.StringKeys1(key0);
		}

		// Token: 0x06008293 RID: 33427 RVA: 0x001BAC5C File Offset: 0x001B8E5C
		public static Keys New(string key0, string key1)
		{
			return new Keys.StringKeys2(key0, key1);
		}

		// Token: 0x06008294 RID: 33428 RVA: 0x001BAC65 File Offset: 0x001B8E65
		public static Keys New(string key0, string key1, string key2)
		{
			return new Keys.StringKeys3(key0, key1, key2);
		}

		// Token: 0x06008295 RID: 33429 RVA: 0x001BAC6F File Offset: 0x001B8E6F
		public static Keys New(string key0, string key1, string key2, string key3)
		{
			return new Keys.StringKeys4(key0, key1, key2, key3);
		}

		// Token: 0x06008296 RID: 33430 RVA: 0x001BAC7A File Offset: 0x001B8E7A
		public static Keys New(Dictionary<string, int> keys)
		{
			return new Keys.DictionaryStringKeys(keys);
		}

		// Token: 0x06008297 RID: 33431 RVA: 0x001BAC84 File Offset: 0x001B8E84
		public static Keys New(params string[] keys)
		{
			switch (keys.Length)
			{
			case 0:
				return Keys.Empty;
			case 1:
				return Keys.New(keys[0]);
			case 2:
				return Keys.New(keys[0], keys[1]);
			case 3:
				return Keys.New(keys[0], keys[1], keys[2]);
			case 4:
				return Keys.New(keys[0], keys[1], keys[2], keys[3]);
			default:
				return new Keys.DictionaryStringKeys(keys);
			}
		}

		// Token: 0x06008298 RID: 33432 RVA: 0x001BACF4 File Offset: 0x001B8EF4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[");
			for (int i = 0; i < this.Length; i++)
			{
				if (i != 0)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(Escape.AsQuotedString(this[i]));
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x06008299 RID: 33433
		public abstract bool TryGetKeyIndex(string key, out int index);

		// Token: 0x0600829A RID: 33434 RVA: 0x001BAD58 File Offset: 0x001B8F58
		public int IndexOfKey(string key)
		{
			int num;
			if (this.TryGetKeyIndex(key, out num))
			{
				return num;
			}
			return -1;
		}

		// Token: 0x17002330 RID: 9008
		// (get) Token: 0x0600829B RID: 33435 RVA: 0x001BAD73 File Offset: 0x001B8F73
		int IKeys.Length
		{
			get
			{
				return this.Length;
			}
		}

		// Token: 0x17002331 RID: 9009
		string IKeys.this[int index]
		{
			get
			{
				return this[index];
			}
		}

		// Token: 0x0600829D RID: 33437 RVA: 0x001BAD84 File Offset: 0x001B8F84
		bool IKeys.TryGetIndex(string key, out int index)
		{
			return this.TryGetKeyIndex(key, out index);
		}

		// Token: 0x040046EA RID: 18154
		public static readonly Keys Empty = new Keys.EmptyKeys();

		// Token: 0x040046EB RID: 18155
		public static readonly IEqualityComparer<Keys> OrderedEqualityComparer = new Keys.OrderedKeysEqualityComparer();

		// Token: 0x0200135F RID: 4959
		public struct StringKeysEnumerator : IEnumerator<string>, IDisposable, IEnumerator
		{
			// Token: 0x060082A0 RID: 33440 RVA: 0x001BADA4 File Offset: 0x001B8FA4
			internal StringKeysEnumerator(Keys keys)
			{
				this.count = keys.Length;
				this.index = -1;
				this.keys = keys;
			}

			// Token: 0x17002332 RID: 9010
			// (get) Token: 0x060082A1 RID: 33441 RVA: 0x001BADC0 File Offset: 0x001B8FC0
			public string Current
			{
				get
				{
					return this.keys[this.index];
				}
			}

			// Token: 0x17002333 RID: 9011
			// (get) Token: 0x060082A2 RID: 33442 RVA: 0x001BADD3 File Offset: 0x001B8FD3
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060082A3 RID: 33443 RVA: 0x0000336E File Offset: 0x0000156E
			public void Dispose()
			{
			}

			// Token: 0x060082A4 RID: 33444 RVA: 0x001BADDC File Offset: 0x001B8FDC
			public bool MoveNext()
			{
				int num = this.index + 1;
				if (num >= this.count)
				{
					return false;
				}
				this.index = num;
				return true;
			}

			// Token: 0x060082A5 RID: 33445 RVA: 0x001BAE05 File Offset: 0x001B9005
			public void Reset()
			{
				this.index = -1;
			}

			// Token: 0x040046EC RID: 18156
			private readonly int count;

			// Token: 0x040046ED RID: 18157
			private int index;

			// Token: 0x040046EE RID: 18158
			private readonly Keys keys;
		}

		// Token: 0x02001360 RID: 4960
		private class DictionaryStringKeys : Keys.StringKeys
		{
			// Token: 0x060082A6 RID: 33446 RVA: 0x001BAE10 File Offset: 0x001B9010
			public DictionaryStringKeys(string[] keys)
			{
				this.index = new Dictionary<string, int>(keys.Length);
				this.keys = keys;
				for (int i = 0; i < keys.Length; i++)
				{
					string text = keys[i];
					if (this.index.ContainsKey(text))
					{
						throw ValueException.DuplicateField(text);
					}
					this.index.Add(text, i);
				}
			}

			// Token: 0x060082A7 RID: 33447 RVA: 0x001BAE6C File Offset: 0x001B906C
			public DictionaryStringKeys(Dictionary<string, int> index)
			{
				this.index = index;
				this.keys = new string[index.Count];
				foreach (KeyValuePair<string, int> keyValuePair in index)
				{
					this.keys[keyValuePair.Value] = keyValuePair.Key;
				}
			}

			// Token: 0x17002334 RID: 9012
			// (get) Token: 0x060082A8 RID: 33448 RVA: 0x001BAEE8 File Offset: 0x001B90E8
			public override int Length
			{
				get
				{
					return this.keys.Length;
				}
			}

			// Token: 0x060082A9 RID: 33449 RVA: 0x001BAEF2 File Offset: 0x001B90F2
			public override bool Contains(string key)
			{
				return this.index.ContainsKey(key);
			}

			// Token: 0x17002335 RID: 9013
			public override string this[int index]
			{
				get
				{
					return this.keys[index];
				}
			}

			// Token: 0x060082AB RID: 33451 RVA: 0x001BAF0A File Offset: 0x001B910A
			public override bool TryGetKeyIndex(string key, out int index)
			{
				return this.index.TryGetValue(key, out index);
			}

			// Token: 0x040046EF RID: 18159
			private readonly Dictionary<string, int> index;

			// Token: 0x040046F0 RID: 18160
			private readonly string[] keys;
		}

		// Token: 0x02001361 RID: 4961
		private class EmptyKeys : Keys
		{
			// Token: 0x17002336 RID: 9014
			// (get) Token: 0x060082AC RID: 33452 RVA: 0x00002105 File Offset: 0x00000305
			public override int Length
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x060082AD RID: 33453 RVA: 0x00002105 File Offset: 0x00000305
			public override bool Contains(string key)
			{
				return false;
			}

			// Token: 0x17002337 RID: 9015
			public override string this[int index]
			{
				get
				{
					throw new ArgumentOutOfRangeException("index");
				}
			}

			// Token: 0x060082AF RID: 33455 RVA: 0x001BAF25 File Offset: 0x001B9125
			public override bool TryGetKeyIndex(string key, out int index)
			{
				index = -1;
				return false;
			}
		}

		// Token: 0x02001362 RID: 4962
		private sealed class OrderedKeysEqualityComparer : IEqualityComparer<Keys>
		{
			// Token: 0x060082B1 RID: 33457 RVA: 0x001BAF33 File Offset: 0x001B9133
			public bool Equals(Keys x, Keys y)
			{
				return x.Equals(y);
			}

			// Token: 0x060082B2 RID: 33458 RVA: 0x001BAF3C File Offset: 0x001B913C
			public int GetHashCode(Keys obj)
			{
				return obj.GetHashCode();
			}
		}

		// Token: 0x02001363 RID: 4963
		protected abstract class StringKeys : Keys
		{
			// Token: 0x060082B4 RID: 33460 RVA: 0x001BAF44 File Offset: 0x001B9144
			public override bool Contains(string key)
			{
				int num;
				return this.TryGetKeyIndex(key, out num);
			}
		}

		// Token: 0x02001364 RID: 4964
		private class StringKeys1 : Keys.StringKeys
		{
			// Token: 0x060082B6 RID: 33462 RVA: 0x001BAF5A File Offset: 0x001B915A
			public StringKeys1(string key0)
			{
				this.key0 = key0;
			}

			// Token: 0x17002338 RID: 9016
			// (get) Token: 0x060082B7 RID: 33463 RVA: 0x00002139 File Offset: 0x00000339
			public override int Length
			{
				get
				{
					return 1;
				}
			}

			// Token: 0x17002339 RID: 9017
			public override string this[int index]
			{
				get
				{
					if (index == 0)
					{
						return this.key0;
					}
					throw new ArgumentOutOfRangeException("index");
				}
			}

			// Token: 0x060082B9 RID: 33465 RVA: 0x001BAF7F File Offset: 0x001B917F
			public override bool TryGetKeyIndex(string key, out int index)
			{
				if (key == this.key0)
				{
					index = 0;
					return true;
				}
				index = -1;
				return false;
			}

			// Token: 0x040046F1 RID: 18161
			private readonly string key0;
		}

		// Token: 0x02001365 RID: 4965
		private class StringKeys2 : Keys.StringKeys
		{
			// Token: 0x060082BA RID: 33466 RVA: 0x001BAF98 File Offset: 0x001B9198
			public StringKeys2(string key0, string key1)
			{
				if (key0 == key1)
				{
					throw ValueException.DuplicateField(key0);
				}
				this.key0 = key0;
				this.key1 = key1;
			}

			// Token: 0x1700233A RID: 9018
			// (get) Token: 0x060082BB RID: 33467 RVA: 0x000023C4 File Offset: 0x000005C4
			public override int Length
			{
				get
				{
					return 2;
				}
			}

			// Token: 0x1700233B RID: 9019
			public override string this[int index]
			{
				get
				{
					if (index == 0)
					{
						return this.key0;
					}
					if (index == 1)
					{
						return this.key1;
					}
					throw new ArgumentOutOfRangeException("index");
				}
			}

			// Token: 0x060082BD RID: 33469 RVA: 0x001BAFDF File Offset: 0x001B91DF
			public override bool TryGetKeyIndex(string key, out int index)
			{
				if (key == this.key0)
				{
					index = 0;
					return true;
				}
				if (key == this.key1)
				{
					index = 1;
					return true;
				}
				index = -1;
				return false;
			}

			// Token: 0x040046F2 RID: 18162
			private readonly string key0;

			// Token: 0x040046F3 RID: 18163
			private readonly string key1;
		}

		// Token: 0x02001366 RID: 4966
		private class StringKeys3 : Keys.StringKeys
		{
			// Token: 0x060082BE RID: 33470 RVA: 0x001BB00C File Offset: 0x001B920C
			public StringKeys3(string key0, string key1, string key2)
			{
				if (key0 == key1 || key0 == key2)
				{
					throw ValueException.DuplicateField(key0);
				}
				if (key1 == key2)
				{
					throw ValueException.DuplicateField(key1);
				}
				this.key0 = key0;
				this.key1 = key1;
				this.key2 = key2;
			}

			// Token: 0x1700233C RID: 9020
			// (get) Token: 0x060082BF RID: 33471 RVA: 0x0000240C File Offset: 0x0000060C
			public override int Length
			{
				get
				{
					return 3;
				}
			}

			// Token: 0x1700233D RID: 9021
			public override string this[int index]
			{
				get
				{
					if (index == 0)
					{
						return this.key0;
					}
					if (index == 1)
					{
						return this.key1;
					}
					if (index == 2)
					{
						return this.key2;
					}
					throw new ArgumentOutOfRangeException("index");
				}
			}

			// Token: 0x060082C1 RID: 33473 RVA: 0x001BB089 File Offset: 0x001B9289
			public override bool TryGetKeyIndex(string key, out int index)
			{
				if (key == this.key0)
				{
					index = 0;
					return true;
				}
				if (key == this.key1)
				{
					index = 1;
					return true;
				}
				if (key == this.key2)
				{
					index = 2;
					return true;
				}
				index = -1;
				return false;
			}

			// Token: 0x040046F4 RID: 18164
			private readonly string key0;

			// Token: 0x040046F5 RID: 18165
			private readonly string key1;

			// Token: 0x040046F6 RID: 18166
			private readonly string key2;
		}

		// Token: 0x02001367 RID: 4967
		private class StringKeys4 : Keys.StringKeys
		{
			// Token: 0x060082C2 RID: 33474 RVA: 0x001BB0C8 File Offset: 0x001B92C8
			public StringKeys4(string key0, string key1, string key2, string key3)
			{
				if (key0 == key1 || key0 == key1 || key0 == key2)
				{
					throw ValueException.DuplicateField(key0);
				}
				if (key1 == key2 || key1 == key3)
				{
					throw ValueException.DuplicateField(key1);
				}
				if (key2 == key3)
				{
					throw ValueException.DuplicateField(key2);
				}
				this.key0 = key0;
				this.key1 = key1;
				this.key2 = key2;
				this.key3 = key3;
			}

			// Token: 0x1700233E RID: 9022
			// (get) Token: 0x060082C3 RID: 33475 RVA: 0x0000244F File Offset: 0x0000064F
			public override int Length
			{
				get
				{
					return 4;
				}
			}

			// Token: 0x1700233F RID: 9023
			public override string this[int index]
			{
				get
				{
					if (index == 0)
					{
						return this.key0;
					}
					if (index == 1)
					{
						return this.key1;
					}
					if (index == 2)
					{
						return this.key2;
					}
					if (index == 3)
					{
						return this.key3;
					}
					throw new ArgumentOutOfRangeException("index");
				}
			}

			// Token: 0x060082C5 RID: 33477 RVA: 0x001BB17C File Offset: 0x001B937C
			public override bool TryGetKeyIndex(string key, out int index)
			{
				if (key == this.key0)
				{
					index = 0;
					return true;
				}
				if (key == this.key1)
				{
					index = 1;
					return true;
				}
				if (key == this.key2)
				{
					index = 2;
					return true;
				}
				if (key == this.key3)
				{
					index = 3;
					return true;
				}
				index = -1;
				return false;
			}

			// Token: 0x040046F7 RID: 18167
			private readonly string key0;

			// Token: 0x040046F8 RID: 18168
			private readonly string key1;

			// Token: 0x040046F9 RID: 18169
			private readonly string key2;

			// Token: 0x040046FA RID: 18170
			private readonly string key3;
		}
	}
}
