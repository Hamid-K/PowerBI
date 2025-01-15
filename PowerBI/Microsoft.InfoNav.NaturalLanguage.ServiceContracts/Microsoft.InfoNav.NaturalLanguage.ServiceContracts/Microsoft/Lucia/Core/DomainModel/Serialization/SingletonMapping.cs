using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x020001D7 RID: 471
	public abstract class SingletonMapping<TProp> : IDictionary<string, TProp>, ICollection<KeyValuePair<string, TProp>>, IEnumerable<KeyValuePair<string, TProp>>, IEnumerable where TProp : class, new()
	{
		// Token: 0x06000A43 RID: 2627 RVA: 0x000131CC File Offset: 0x000113CC
		protected SingletonMapping()
			: this(string.Empty)
		{
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x000131D9 File Offset: 0x000113D9
		protected SingletonMapping(string key)
			: this(key, new TProp())
		{
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x000131E7 File Offset: 0x000113E7
		protected SingletonMapping(string key, TProp value)
		{
			this.UnderlyingKey = key;
			this.UnderlyingValue = value;
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000A46 RID: 2630 RVA: 0x000131FD File Offset: 0x000113FD
		// (set) Token: 0x06000A47 RID: 2631 RVA: 0x00013205 File Offset: 0x00011405
		protected string UnderlyingKey
		{
			get
			{
				return this.__key;
			}
			set
			{
				this.__key = value;
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000A48 RID: 2632 RVA: 0x0001320E File Offset: 0x0001140E
		// (set) Token: 0x06000A49 RID: 2633 RVA: 0x00013216 File Offset: 0x00011416
		protected TProp UnderlyingValue
		{
			get
			{
				return this.__value;
			}
			set
			{
				this.__value = value;
			}
		}

		// Token: 0x17000315 RID: 789
		TProp IDictionary<string, TProp>.this[string key]
		{
			get
			{
				if (!(this.UnderlyingKey == key))
				{
					throw Contract.ExceptRange("key");
				}
				return this.UnderlyingValue;
			}
			set
			{
				this.UnderlyingKey = key;
				this.UnderlyingValue = value;
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000A4C RID: 2636 RVA: 0x0001325C File Offset: 0x0001145C
		int ICollection<KeyValuePair<string, TProp>>.Count
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x0001325F File Offset: 0x0001145F
		bool ICollection<KeyValuePair<string, TProp>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000A4E RID: 2638 RVA: 0x00013262 File Offset: 0x00011462
		ICollection<string> IDictionary<string, TProp>.Keys
		{
			get
			{
				throw Contract.ExceptNotSupported();
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x00013269 File Offset: 0x00011469
		ICollection<TProp> IDictionary<string, TProp>.Values
		{
			get
			{
				throw Contract.ExceptNotSupported();
			}
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x00013270 File Offset: 0x00011470
		void IDictionary<string, TProp>.Add(string key, TProp value)
		{
			this.UnderlyingKey = key;
			this.UnderlyingValue = value;
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x00013280 File Offset: 0x00011480
		void ICollection<KeyValuePair<string, TProp>>.Add(KeyValuePair<string, TProp> item)
		{
			this.UnderlyingKey = item.Key;
			this.UnderlyingValue = item.Value;
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0001329C File Offset: 0x0001149C
		IEnumerator<KeyValuePair<string, TProp>> IEnumerable<KeyValuePair<string, TProp>>.GetEnumerator()
		{
			yield return new KeyValuePair<string, TProp>(this.UnderlyingKey, this.UnderlyingValue);
			yield break;
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x000132AB File Offset: 0x000114AB
		IEnumerator IEnumerable.GetEnumerator()
		{
			yield return new KeyValuePair<string, TProp>(this.UnderlyingKey, this.UnderlyingValue);
			yield break;
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x000132BA File Offset: 0x000114BA
		bool ICollection<KeyValuePair<string, TProp>>.Contains(KeyValuePair<string, TProp> item)
		{
			throw Contract.ExceptNotSupported();
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x000132C1 File Offset: 0x000114C1
		bool IDictionary<string, TProp>.ContainsKey(string key)
		{
			throw Contract.ExceptNotSupported();
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x000132C8 File Offset: 0x000114C8
		bool IDictionary<string, TProp>.TryGetValue(string key, out TProp value)
		{
			throw Contract.ExceptNotSupported();
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x000132CF File Offset: 0x000114CF
		void ICollection<KeyValuePair<string, TProp>>.Clear()
		{
			throw Contract.ExceptNotSupported();
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x000132D6 File Offset: 0x000114D6
		void ICollection<KeyValuePair<string, TProp>>.CopyTo(KeyValuePair<string, TProp>[] array, int arrayIndex)
		{
			throw Contract.ExceptNotSupported();
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x000132DD File Offset: 0x000114DD
		bool IDictionary<string, TProp>.Remove(string key)
		{
			throw Contract.ExceptNotSupported();
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x000132E4 File Offset: 0x000114E4
		bool ICollection<KeyValuePair<string, TProp>>.Remove(KeyValuePair<string, TProp> item)
		{
			throw Contract.ExceptNotSupported();
		}

		// Token: 0x040007ED RID: 2029
		private string __key;

		// Token: 0x040007EE RID: 2030
		private TProp __value;
	}
}
