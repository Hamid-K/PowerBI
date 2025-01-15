using System;
using AngleSharp.Extensions;

namespace AngleSharp.Dom
{
	// Token: 0x02000157 RID: 343
	internal sealed class Location : ILocation, IUrlUtilities
	{
		// Token: 0x14000048 RID: 72
		// (add) Token: 0x06000BBE RID: 3006 RVA: 0x00043CB8 File Offset: 0x00041EB8
		// (remove) Token: 0x06000BBF RID: 3007 RVA: 0x00043CF0 File Offset: 0x00041EF0
		public event EventHandler<Location.LocationChangedEventArgs> Changed;

		// Token: 0x06000BC0 RID: 3008 RVA: 0x00043D25 File Offset: 0x00041F25
		internal Location()
			: this(string.Empty)
		{
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x00043D32 File Offset: 0x00041F32
		internal Location(string url)
			: this(new Url(url))
		{
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x00043D40 File Offset: 0x00041F40
		internal Location(Url url)
		{
			this._url = url ?? new Url(string.Empty);
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000BC3 RID: 3011 RVA: 0x00043D5D File Offset: 0x00041F5D
		public Url Original
		{
			get
			{
				return this._url;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x00043D65 File Offset: 0x00041F65
		public string Origin
		{
			get
			{
				return this._url.Origin;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x00043D72 File Offset: 0x00041F72
		public bool IsRelative
		{
			get
			{
				return this._url.IsRelative;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000BC6 RID: 3014 RVA: 0x00043D7F File Offset: 0x00041F7F
		// (set) Token: 0x06000BC7 RID: 3015 RVA: 0x00043D8C File Offset: 0x00041F8C
		public string UserName
		{
			get
			{
				return this._url.UserName;
			}
			set
			{
				this._url.UserName = value;
			}
		}

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x00043D9A File Offset: 0x00041F9A
		// (set) Token: 0x06000BC9 RID: 3017 RVA: 0x00043DA7 File Offset: 0x00041FA7
		public string Password
		{
			get
			{
				return this._url.Password;
			}
			set
			{
				this._url.Password = value;
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x00043DB5 File Offset: 0x00041FB5
		// (set) Token: 0x06000BCB RID: 3019 RVA: 0x00043DCC File Offset: 0x00041FCC
		public string Hash
		{
			get
			{
				return Location.NonEmptyPrefix(this._url.Fragment, "#");
			}
			set
			{
				string href = this._url.Href;
				if (value != null)
				{
					if (value.Has('#', 0))
					{
						value = value.Substring(1);
					}
					else if (value.Length == 0)
					{
						value = null;
					}
				}
				if (value != this._url.Fragment)
				{
					this._url.Fragment = value;
					this.RaiseChanged(href, true);
				}
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x00043E30 File Offset: 0x00042030
		// (set) Token: 0x06000BCD RID: 3021 RVA: 0x00043E40 File Offset: 0x00042040
		public string Host
		{
			get
			{
				return this._url.Host;
			}
			set
			{
				string href = this._url.Href;
				if (value != this._url.Host)
				{
					this._url.Host = value;
					this.RaiseChanged(href, false);
				}
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x00043E80 File Offset: 0x00042080
		// (set) Token: 0x06000BCF RID: 3023 RVA: 0x00043E90 File Offset: 0x00042090
		public string HostName
		{
			get
			{
				return this._url.HostName;
			}
			set
			{
				string href = this._url.Href;
				if (value != this._url.HostName)
				{
					this._url.HostName = value;
					this.RaiseChanged(href, false);
				}
			}
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x00043ED0 File Offset: 0x000420D0
		// (set) Token: 0x06000BD1 RID: 3025 RVA: 0x00043EE0 File Offset: 0x000420E0
		public string Href
		{
			get
			{
				return this._url.Href;
			}
			set
			{
				string href = this._url.Href;
				if (value != this._url.Href)
				{
					this._url.Href = value;
					this.RaiseChanged(href, false);
				}
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x00043F20 File Offset: 0x00042120
		// (set) Token: 0x06000BD3 RID: 3027 RVA: 0x00043F58 File Offset: 0x00042158
		public string PathName
		{
			get
			{
				string data = this._url.Data;
				if (!string.IsNullOrEmpty(data))
				{
					return data;
				}
				return "/" + this._url.Path;
			}
			set
			{
				string href = this._url.Href;
				if (value != this._url.Path)
				{
					this._url.Path = value;
					this.RaiseChanged(href, false);
				}
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x00043F98 File Offset: 0x00042198
		// (set) Token: 0x06000BD5 RID: 3029 RVA: 0x00043FA8 File Offset: 0x000421A8
		public string Port
		{
			get
			{
				return this._url.Port;
			}
			set
			{
				string href = this._url.Href;
				if (value != this._url.Port)
				{
					this._url.Port = value;
					this.RaiseChanged(href, false);
				}
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x00043FE8 File Offset: 0x000421E8
		// (set) Token: 0x06000BD7 RID: 3031 RVA: 0x00044000 File Offset: 0x00042200
		public string Protocol
		{
			get
			{
				return Location.NonEmptyPostfix(this._url.Scheme, ":");
			}
			set
			{
				string href = this._url.Href;
				if (value != this._url.Scheme)
				{
					this._url.Scheme = value;
					this.RaiseChanged(href, false);
				}
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x00044040 File Offset: 0x00042240
		// (set) Token: 0x06000BD9 RID: 3033 RVA: 0x00044058 File Offset: 0x00042258
		public string Search
		{
			get
			{
				return Location.NonEmptyPrefix(this._url.Query, "?");
			}
			set
			{
				string href = this._url.Href;
				if (value != this._url.Query)
				{
					this._url.Query = value;
					this.RaiseChanged(href, false);
				}
			}
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x00044098 File Offset: 0x00042298
		public void Assign(string url)
		{
			this._url.Href = url;
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x00044098 File Offset: 0x00042298
		public void Replace(string url)
		{
			this._url.Href = url;
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x000440A6 File Offset: 0x000422A6
		public void Reload()
		{
			this._url.Href = this.Href;
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x00043ED0 File Offset: 0x000420D0
		public override string ToString()
		{
			return this._url.Href;
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x000440B9 File Offset: 0x000422B9
		private void RaiseChanged(string oldAddress, bool hashChanged)
		{
			EventHandler<Location.LocationChangedEventArgs> changed = this.Changed;
			if (changed == null)
			{
				return;
			}
			changed(this, new Location.LocationChangedEventArgs(hashChanged, oldAddress, this._url.Href));
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x000440DE File Offset: 0x000422DE
		private static string NonEmptyPrefix(string check, string prefix)
		{
			if (!string.IsNullOrEmpty(check))
			{
				return prefix + check;
			}
			return string.Empty;
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x000440F5 File Offset: 0x000422F5
		private static string NonEmptyPostfix(string check, string postfix)
		{
			if (!string.IsNullOrEmpty(check))
			{
				return check + postfix;
			}
			return string.Empty;
		}

		// Token: 0x04000949 RID: 2377
		private readonly Url _url;

		// Token: 0x020004D5 RID: 1237
		public sealed class LocationChangedEventArgs : EventArgs
		{
			// Token: 0x0600258F RID: 9615 RVA: 0x00061DFC File Offset: 0x0005FFFC
			public LocationChangedEventArgs(bool hashChanged, string previousLocation, string currentLocation)
			{
				this.IsHashChanged = hashChanged;
				this.PreviousLocation = previousLocation;
				this.CurrentLocation = currentLocation;
			}

			// Token: 0x17000AC1 RID: 2753
			// (get) Token: 0x06002590 RID: 9616 RVA: 0x00061E19 File Offset: 0x00060019
			// (set) Token: 0x06002591 RID: 9617 RVA: 0x00061E21 File Offset: 0x00060021
			public bool IsHashChanged { get; private set; }

			// Token: 0x17000AC2 RID: 2754
			// (get) Token: 0x06002592 RID: 9618 RVA: 0x00061E2A File Offset: 0x0006002A
			// (set) Token: 0x06002593 RID: 9619 RVA: 0x00061E32 File Offset: 0x00060032
			public string PreviousLocation { get; private set; }

			// Token: 0x17000AC3 RID: 2755
			// (get) Token: 0x06002594 RID: 9620 RVA: 0x00061E3B File Offset: 0x0006003B
			// (set) Token: 0x06002595 RID: 9621 RVA: 0x00061E43 File Offset: 0x00060043
			public string CurrentLocation { get; private set; }
		}
	}
}
