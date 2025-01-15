using System;
using System.Collections.Generic;

namespace Microsoft.Owin.BuilderProperties
{
	// Token: 0x02000046 RID: 70
	public struct Address
	{
		// Token: 0x0600026B RID: 619 RVA: 0x0000712B File Offset: 0x0000532B
		public Address(IDictionary<string, object> dictionary)
		{
			this._dictionary = dictionary;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00007134 File Offset: 0x00005334
		public Address(string scheme, string host, string port, string path)
		{
			this = new Address(new Dictionary<string, object>());
			this.Scheme = scheme;
			this.Host = host;
			this.Port = port;
			this.Path = path;
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000715E File Offset: 0x0000535E
		public IDictionary<string, object> Dictionary
		{
			get
			{
				return this._dictionary;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600026E RID: 622 RVA: 0x00007166 File Offset: 0x00005366
		// (set) Token: 0x0600026F RID: 623 RVA: 0x00007173 File Offset: 0x00005373
		public string Scheme
		{
			get
			{
				return this.Get<string>("scheme");
			}
			set
			{
				this.Set("scheme", value);
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000270 RID: 624 RVA: 0x00007182 File Offset: 0x00005382
		// (set) Token: 0x06000271 RID: 625 RVA: 0x0000718F File Offset: 0x0000538F
		public string Host
		{
			get
			{
				return this.Get<string>("host");
			}
			set
			{
				this.Set("host", value);
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000719E File Offset: 0x0000539E
		// (set) Token: 0x06000273 RID: 627 RVA: 0x000071AB File Offset: 0x000053AB
		public string Port
		{
			get
			{
				return this.Get<string>("port");
			}
			set
			{
				this.Set("port", value);
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000274 RID: 628 RVA: 0x000071BA File Offset: 0x000053BA
		// (set) Token: 0x06000275 RID: 629 RVA: 0x000071C7 File Offset: 0x000053C7
		public string Path
		{
			get
			{
				return this.Get<string>("path");
			}
			set
			{
				this.Set("path", value);
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x000071D6 File Offset: 0x000053D6
		public static Address Create()
		{
			return new Address(new Dictionary<string, object>());
		}

		// Token: 0x06000277 RID: 631 RVA: 0x000071E2 File Offset: 0x000053E2
		public bool Equals(Address other)
		{
			return object.Equals(this._dictionary, other._dictionary);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x000071F5 File Offset: 0x000053F5
		public override bool Equals(object obj)
		{
			return obj is Address && this.Equals((Address)obj);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000720D File Offset: 0x0000540D
		public override int GetHashCode()
		{
			if (this._dictionary == null)
			{
				return 0;
			}
			return this._dictionary.GetHashCode();
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00007224 File Offset: 0x00005424
		public static bool operator ==(Address left, Address right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000722E File Offset: 0x0000542E
		public static bool operator !=(Address left, Address right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000723C File Offset: 0x0000543C
		public T Get<T>(string key)
		{
			object value;
			if (!this._dictionary.TryGetValue(key, out value))
			{
				return default(T);
			}
			return (T)((object)value);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00007269 File Offset: 0x00005469
		public Address Set(string key, object value)
		{
			this._dictionary[key] = value;
			return this;
		}

		// Token: 0x0400007E RID: 126
		private readonly IDictionary<string, object> _dictionary;
	}
}
