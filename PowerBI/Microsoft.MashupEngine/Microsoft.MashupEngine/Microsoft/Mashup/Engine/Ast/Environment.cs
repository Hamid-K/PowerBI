using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine.Ast
{
	// Token: 0x02001B6E RID: 7022
	public sealed class Environment<T>
	{
		// Token: 0x0600AFE4 RID: 45028 RVA: 0x002402E5 File Offset: 0x0023E4E5
		public Environment()
		{
			this.dictionary = new Dictionary<Identifier, Environment<T>.Entry>();
		}

		// Token: 0x0600AFE5 RID: 45029 RVA: 0x002402F8 File Offset: 0x0023E4F8
		public void EnterScope()
		{
			this.depth++;
		}

		// Token: 0x0600AFE6 RID: 45030 RVA: 0x00240308 File Offset: 0x0023E508
		public void ExitScope()
		{
			this.depth--;
			if (this.depth == 0)
			{
				this.dictionary.Clear();
			}
		}

		// Token: 0x0600AFE7 RID: 45031 RVA: 0x0024032C File Offset: 0x0023E52C
		public void Add(Identifier identifier, T value)
		{
			Environment<T>.Entry entry;
			if (this.dictionary.TryGetValue(identifier, out entry) && entry.Depth == this.depth)
			{
				Environment<T>.Entry entry2 = entry;
				while (entry2.Exclusion)
				{
					entry2 = entry2.Previous;
				}
				value = entry2.Value;
			}
			this.dictionary[identifier] = Environment<T>.Entry.New(value, this.depth, entry);
		}

		// Token: 0x0600AFE8 RID: 45032 RVA: 0x0024038C File Offset: 0x0023E58C
		public void Remove(Identifier identifier)
		{
			Environment<T>.Entry previous = this.dictionary[identifier].Previous;
			if (previous != null)
			{
				this.dictionary[identifier] = previous;
				return;
			}
			this.dictionary.Remove(identifier);
		}

		// Token: 0x0600AFE9 RID: 45033 RVA: 0x002403CC File Offset: 0x0023E5CC
		public T GetValue(Identifier identifier, bool inclusive)
		{
			T t;
			if (!this.TryGetValue(identifier, inclusive, out t))
			{
				throw new InvalidOperationException("Identifier not found: " + identifier);
			}
			return t;
		}

		// Token: 0x0600AFEA RID: 45034 RVA: 0x002403FC File Offset: 0x0023E5FC
		public void EnterExclusion(Identifier identifier)
		{
			Environment<T>.Entry entry;
			this.dictionary.TryGetValue(identifier, out entry);
			this.dictionary[identifier] = Environment<T>.Entry.NewExclusion(this.depth, entry);
		}

		// Token: 0x0600AFEB RID: 45035 RVA: 0x00240430 File Offset: 0x0023E630
		public void ExitExclusion(Identifier identifier)
		{
			Environment<T>.Entry previous = this.dictionary[identifier].Previous;
			if (previous != null)
			{
				this.dictionary[identifier] = previous;
				return;
			}
			this.dictionary.Remove(identifier);
		}

		// Token: 0x0600AFEC RID: 45036 RVA: 0x00240470 File Offset: 0x0023E670
		public bool TryGetValue(Identifier identifier, bool inclusive, out T value)
		{
			Environment<T>.Entry entry;
			if (this.TryGetEntry(identifier, inclusive, out entry))
			{
				value = entry.Value;
				return true;
			}
			value = default(T);
			return false;
		}

		// Token: 0x0600AFED RID: 45037 RVA: 0x002404A0 File Offset: 0x0023E6A0
		public bool TryGetValueAtCurrentDepth(Identifier identifier, bool inclusive, out T value)
		{
			Environment<T>.Entry entry;
			if (this.TryGetEntry(identifier, inclusive, out entry) && entry.Depth == this.depth)
			{
				value = entry.Value;
				return true;
			}
			value = default(T);
			return false;
		}

		// Token: 0x0600AFEE RID: 45038 RVA: 0x002404DD File Offset: 0x0023E6DD
		private bool TryGetEntry(Identifier identifier, bool inclusive, out Environment<T>.Entry entry)
		{
			if (this.dictionary.TryGetValue(identifier, out entry))
			{
				while (entry != null)
				{
					if (!entry.Exclusion)
					{
						return true;
					}
					if (!inclusive)
					{
						entry = entry.Previous;
					}
					entry = entry.Previous;
				}
			}
			entry = null;
			return false;
		}

		// Token: 0x04005A89 RID: 23177
		private int depth;

		// Token: 0x04005A8A RID: 23178
		private Dictionary<Identifier, Environment<T>.Entry> dictionary;

		// Token: 0x02001B6F RID: 7023
		private class Entry
		{
			// Token: 0x0600AFEF RID: 45039 RVA: 0x0024051A File Offset: 0x0023E71A
			public static Environment<T>.Entry New(T value, int depth, Environment<T>.Entry previous)
			{
				return new Environment<T>.Entry(value, depth, false, previous);
			}

			// Token: 0x0600AFF0 RID: 45040 RVA: 0x00240528 File Offset: 0x0023E728
			public static Environment<T>.Entry NewExclusion(int depth, Environment<T>.Entry previous)
			{
				return new Environment<T>.Entry(default(T), depth, true, previous);
			}

			// Token: 0x0600AFF1 RID: 45041 RVA: 0x00240546 File Offset: 0x0023E746
			private Entry(T value, int depth, bool exclusion, Environment<T>.Entry previous)
			{
				this.value = value;
				this.depthAndExclusion = (uint)(depth & -134217729);
				if (exclusion)
				{
					this.depthAndExclusion |= 134217728U;
				}
				this.previous = previous;
			}

			// Token: 0x17002C0B RID: 11275
			// (get) Token: 0x0600AFF2 RID: 45042 RVA: 0x0024057F File Offset: 0x0023E77F
			public T Value
			{
				get
				{
					return this.value;
				}
			}

			// Token: 0x17002C0C RID: 11276
			// (get) Token: 0x0600AFF3 RID: 45043 RVA: 0x00240587 File Offset: 0x0023E787
			public int Depth
			{
				get
				{
					return (int)(this.depthAndExclusion & 4160749567U);
				}
			}

			// Token: 0x17002C0D RID: 11277
			// (get) Token: 0x0600AFF4 RID: 45044 RVA: 0x00240595 File Offset: 0x0023E795
			public bool Exclusion
			{
				get
				{
					return (this.depthAndExclusion & 134217728U) > 0U;
				}
			}

			// Token: 0x17002C0E RID: 11278
			// (get) Token: 0x0600AFF5 RID: 45045 RVA: 0x002405A6 File Offset: 0x0023E7A6
			public Environment<T>.Entry Previous
			{
				get
				{
					return this.previous;
				}
			}

			// Token: 0x04005A8B RID: 23179
			private const uint exclusionMask = 134217728U;

			// Token: 0x04005A8C RID: 23180
			private T value;

			// Token: 0x04005A8D RID: 23181
			private uint depthAndExclusion;

			// Token: 0x04005A8E RID: 23182
			private Environment<T>.Entry previous;
		}
	}
}
