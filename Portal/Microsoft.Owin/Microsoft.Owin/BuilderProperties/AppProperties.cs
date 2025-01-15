using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Owin.BuilderProperties
{
	// Token: 0x02000048 RID: 72
	public struct AppProperties
	{
		// Token: 0x0600028C RID: 652 RVA: 0x00007363 File Offset: 0x00005563
		public AppProperties(IDictionary<string, object> dictionary)
		{
			this._dictionary = dictionary;
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000736C File Offset: 0x0000556C
		// (set) Token: 0x0600028E RID: 654 RVA: 0x00007379 File Offset: 0x00005579
		public string OwinVersion
		{
			get
			{
				return this.Get<string>("owin.Version");
			}
			set
			{
				this.Set("owin.Version", value);
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600028F RID: 655 RVA: 0x00007388 File Offset: 0x00005588
		// (set) Token: 0x06000290 RID: 656 RVA: 0x00007395 File Offset: 0x00005595
		public Func<IDictionary<string, object>, Task> DefaultApp
		{
			get
			{
				return this.Get<Func<IDictionary<string, object>, Task>>("builder.DefaultApp");
			}
			set
			{
				this.Set("builder.DefaultApp", value);
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000291 RID: 657 RVA: 0x000073A4 File Offset: 0x000055A4
		// (set) Token: 0x06000292 RID: 658 RVA: 0x000073B1 File Offset: 0x000055B1
		public Action<Delegate> AddSignatureConversionDelegate
		{
			get
			{
				return this.Get<Action<Delegate>>("builder.AddSignatureConversion");
			}
			set
			{
				this.Set("builder.AddSignatureConversion", value);
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000293 RID: 659 RVA: 0x000073C0 File Offset: 0x000055C0
		// (set) Token: 0x06000294 RID: 660 RVA: 0x000073CD File Offset: 0x000055CD
		public string AppName
		{
			get
			{
				return this.Get<string>("host.AppName");
			}
			set
			{
				this.Set("host.AppName", value);
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000295 RID: 661 RVA: 0x000073DC File Offset: 0x000055DC
		// (set) Token: 0x06000296 RID: 662 RVA: 0x000073E9 File Offset: 0x000055E9
		public TextWriter TraceOutput
		{
			get
			{
				return this.Get<TextWriter>("host.TraceOutput");
			}
			set
			{
				this.Set("host.TraceOutput", value);
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000297 RID: 663 RVA: 0x000073F8 File Offset: 0x000055F8
		// (set) Token: 0x06000298 RID: 664 RVA: 0x00007405 File Offset: 0x00005605
		public CancellationToken OnAppDisposing
		{
			get
			{
				return this.Get<CancellationToken>("host.OnAppDisposing");
			}
			set
			{
				this.Set("host.OnAppDisposing", value);
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00007419 File Offset: 0x00005619
		// (set) Token: 0x0600029A RID: 666 RVA: 0x0000742B File Offset: 0x0000562B
		public AddressCollection Addresses
		{
			get
			{
				return new AddressCollection(this.Get<IList<IDictionary<string, object>>>("host.Addresses"));
			}
			set
			{
				this.Set("host.Addresses", value.List);
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600029B RID: 667 RVA: 0x00007440 File Offset: 0x00005640
		// (set) Token: 0x0600029C RID: 668 RVA: 0x00007452 File Offset: 0x00005652
		public Capabilities Capabilities
		{
			get
			{
				return new Capabilities(this.Get<IDictionary<string, object>>("server.Capabilities"));
			}
			set
			{
				this.Set("server.Capabilities", value.Dictionary);
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00007467 File Offset: 0x00005667
		public IDictionary<string, object> Dictionary
		{
			get
			{
				return this._dictionary;
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000746F File Offset: 0x0000566F
		public bool Equals(AppProperties other)
		{
			return object.Equals(this._dictionary, other._dictionary);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00007482 File Offset: 0x00005682
		public override bool Equals(object obj)
		{
			return obj is AppProperties && this.Equals((AppProperties)obj);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000749A File Offset: 0x0000569A
		public override int GetHashCode()
		{
			if (this._dictionary == null)
			{
				return 0;
			}
			return this._dictionary.GetHashCode();
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x000074B1 File Offset: 0x000056B1
		public static bool operator ==(AppProperties left, AppProperties right)
		{
			return left.Equals(right);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x000074BB File Offset: 0x000056BB
		public static bool operator !=(AppProperties left, AppProperties right)
		{
			return !left.Equals(right);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x000074C8 File Offset: 0x000056C8
		public T Get<T>(string key)
		{
			object value;
			if (!this._dictionary.TryGetValue(key, out value))
			{
				return default(T);
			}
			return (T)((object)value);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x000074F5 File Offset: 0x000056F5
		public AppProperties Set(string key, object value)
		{
			this._dictionary[key] = value;
			return this;
		}

		// Token: 0x04000080 RID: 128
		private readonly IDictionary<string, object> _dictionary;
	}
}
