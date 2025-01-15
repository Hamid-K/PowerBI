using System;
using AngleSharp.Dom.Collections;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003A9 RID: 937
	internal abstract class HtmlUrlBaseElement : HtmlElement, IUrlUtilities
	{
		// Token: 0x06001D6B RID: 7531 RVA: 0x000525E5 File Offset: 0x000507E5
		public HtmlUrlBaseElement(Document owner, string name, string prefix, NodeFlags flags)
			: base(owner, name, prefix, flags)
		{
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06001D6C RID: 7532 RVA: 0x000559E2 File Offset: 0x00053BE2
		// (set) Token: 0x06001D6D RID: 7533 RVA: 0x000559EF File Offset: 0x00053BEF
		public string Download
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Download);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Download, value, false);
			}
		}

		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06001D6E RID: 7534 RVA: 0x00053185 File Offset: 0x00051385
		// (set) Token: 0x06001D6F RID: 7535 RVA: 0x000559FE File Offset: 0x00053BFE
		public string Href
		{
			get
			{
				return this.GetUrlAttribute(AttributeNames.Href);
			}
			set
			{
				base.SetAttribute(AttributeNames.Href, value);
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06001D70 RID: 7536 RVA: 0x00055A0C File Offset: 0x00053C0C
		// (set) Token: 0x06001D71 RID: 7537 RVA: 0x00055A34 File Offset: 0x00053C34
		public string Hash
		{
			get
			{
				return this.GetLocationPart((ILocation m) => m.Hash);
			}
			set
			{
				this.SetLocationPart(delegate(ILocation m)
				{
					m.Hash = value;
				});
			}
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06001D72 RID: 7538 RVA: 0x00055A60 File Offset: 0x00053C60
		// (set) Token: 0x06001D73 RID: 7539 RVA: 0x00055A88 File Offset: 0x00053C88
		public string Host
		{
			get
			{
				return this.GetLocationPart((ILocation m) => m.Host);
			}
			set
			{
				this.SetLocationPart(delegate(ILocation m)
				{
					m.Host = value;
				});
			}
		}

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06001D74 RID: 7540 RVA: 0x00055AB4 File Offset: 0x00053CB4
		// (set) Token: 0x06001D75 RID: 7541 RVA: 0x00055ADC File Offset: 0x00053CDC
		public string HostName
		{
			get
			{
				return this.GetLocationPart((ILocation m) => m.HostName);
			}
			set
			{
				this.SetLocationPart(delegate(ILocation m)
				{
					m.HostName = value;
				});
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06001D76 RID: 7542 RVA: 0x00055B08 File Offset: 0x00053D08
		// (set) Token: 0x06001D77 RID: 7543 RVA: 0x00055B30 File Offset: 0x00053D30
		public string PathName
		{
			get
			{
				return this.GetLocationPart((ILocation m) => m.PathName);
			}
			set
			{
				this.SetLocationPart(delegate(ILocation m)
				{
					m.PathName = value;
				});
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06001D78 RID: 7544 RVA: 0x00055B5C File Offset: 0x00053D5C
		// (set) Token: 0x06001D79 RID: 7545 RVA: 0x00055B84 File Offset: 0x00053D84
		public string Port
		{
			get
			{
				return this.GetLocationPart((ILocation m) => m.Port);
			}
			set
			{
				this.SetLocationPart(delegate(ILocation m)
				{
					m.Port = value;
				});
			}
		}

		// Token: 0x170008A7 RID: 2215
		// (get) Token: 0x06001D7A RID: 7546 RVA: 0x00055BB0 File Offset: 0x00053DB0
		// (set) Token: 0x06001D7B RID: 7547 RVA: 0x00055BD8 File Offset: 0x00053DD8
		public string Protocol
		{
			get
			{
				return this.GetLocationPart((ILocation m) => m.Protocol);
			}
			set
			{
				this.SetLocationPart(delegate(ILocation m)
				{
					m.Protocol = value;
				});
			}
		}

		// Token: 0x170008A8 RID: 2216
		// (get) Token: 0x06001D7C RID: 7548 RVA: 0x00055C04 File Offset: 0x00053E04
		// (set) Token: 0x06001D7D RID: 7549 RVA: 0x00055C2C File Offset: 0x00053E2C
		public string UserName
		{
			get
			{
				return this.GetLocationPart((ILocation m) => m.UserName);
			}
			set
			{
				this.SetLocationPart(delegate(ILocation m)
				{
					m.UserName = value;
				});
			}
		}

		// Token: 0x170008A9 RID: 2217
		// (get) Token: 0x06001D7E RID: 7550 RVA: 0x00055C58 File Offset: 0x00053E58
		// (set) Token: 0x06001D7F RID: 7551 RVA: 0x00055C80 File Offset: 0x00053E80
		public string Password
		{
			get
			{
				return this.GetLocationPart((ILocation m) => m.Password);
			}
			set
			{
				this.SetLocationPart(delegate(ILocation m)
				{
					m.Password = value;
				});
			}
		}

		// Token: 0x170008AA RID: 2218
		// (get) Token: 0x06001D80 RID: 7552 RVA: 0x00055CAC File Offset: 0x00053EAC
		// (set) Token: 0x06001D81 RID: 7553 RVA: 0x00055CD4 File Offset: 0x00053ED4
		public string Search
		{
			get
			{
				return this.GetLocationPart((ILocation m) => m.Search);
			}
			set
			{
				this.SetLocationPart(delegate(ILocation m)
				{
					m.Search = value;
				});
			}
		}

		// Token: 0x170008AB RID: 2219
		// (get) Token: 0x06001D82 RID: 7554 RVA: 0x00055D00 File Offset: 0x00053F00
		public string Origin
		{
			get
			{
				return this.GetLocationPart((ILocation m) => m.Origin);
			}
		}

		// Token: 0x170008AC RID: 2220
		// (get) Token: 0x06001D83 RID: 7555 RVA: 0x00053192 File Offset: 0x00051392
		// (set) Token: 0x06001D84 RID: 7556 RVA: 0x0005319F File Offset: 0x0005139F
		public string TargetLanguage
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.HrefLang);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.HrefLang, value, false);
			}
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06001D85 RID: 7557 RVA: 0x0005326D File Offset: 0x0005146D
		// (set) Token: 0x06001D86 RID: 7558 RVA: 0x0005327A File Offset: 0x0005147A
		public string Media
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Media);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Media, value, false);
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06001D87 RID: 7559 RVA: 0x000531AE File Offset: 0x000513AE
		// (set) Token: 0x06001D88 RID: 7560 RVA: 0x000531BB File Offset: 0x000513BB
		public string Relation
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Rel);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Rel, value, false);
			}
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06001D89 RID: 7561 RVA: 0x00055D27 File Offset: 0x00053F27
		public ITokenList RelationList
		{
			get
			{
				if (this._relList == null)
				{
					this._relList = new TokenList(this.GetOwnAttribute(AttributeNames.Rel));
					this._relList.Changed += delegate(string value)
					{
						base.UpdateAttribute(AttributeNames.Rel, value);
					};
				}
				return this._relList;
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06001D8A RID: 7562 RVA: 0x00055D64 File Offset: 0x00053F64
		public ISettableTokenList Ping
		{
			get
			{
				if (this._ping == null)
				{
					this._ping = new SettableTokenList(this.GetOwnAttribute(AttributeNames.Ping));
					this._ping.Changed += delegate(string value)
					{
						base.UpdateAttribute(AttributeNames.Ping, value);
					};
				}
				return this._ping;
			}
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06001D8B RID: 7563 RVA: 0x0004FDAD File Offset: 0x0004DFAD
		// (set) Token: 0x06001D8C RID: 7564 RVA: 0x0004FDBA File Offset: 0x0004DFBA
		public string Target
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Target);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Target, value, false);
			}
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06001D8D RID: 7565 RVA: 0x00051A27 File Offset: 0x0004FC27
		// (set) Token: 0x06001D8E RID: 7566 RVA: 0x0004FF58 File Offset: 0x0004E158
		public string Type
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Type);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Type, value, false);
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06001D8F RID: 7567 RVA: 0x00055DA1 File Offset: 0x00053FA1
		// (set) Token: 0x06001D90 RID: 7568 RVA: 0x00055DA9 File Offset: 0x00053FA9
		internal bool IsVisited { get; set; }

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06001D91 RID: 7569 RVA: 0x00055DB2 File Offset: 0x00053FB2
		// (set) Token: 0x06001D92 RID: 7570 RVA: 0x00055DBA File Offset: 0x00053FBA
		internal bool IsActive { get; set; }

		// Token: 0x06001D93 RID: 7571 RVA: 0x00055DC3 File Offset: 0x00053FC3
		internal void UpdateRel(string value)
		{
			TokenList relList = this._relList;
			if (relList == null)
			{
				return;
			}
			relList.Update(value);
		}

		// Token: 0x06001D94 RID: 7572 RVA: 0x00055DD6 File Offset: 0x00053FD6
		internal void UpdatePing(string value)
		{
			SettableTokenList ping = this._ping;
			if (ping == null)
			{
				return;
			}
			ping.Update(value);
		}

		// Token: 0x06001D95 RID: 7573 RVA: 0x00055DEC File Offset: 0x00053FEC
		private string GetLocationPart(Func<ILocation, string> getter)
		{
			string ownAttribute = this.GetOwnAttribute(AttributeNames.Href);
			Url url = ((ownAttribute != null) ? new Url(base.BaseUrl, ownAttribute) : null);
			if (url != null && !url.IsInvalid)
			{
				Location location = new Location(url);
				return getter(location);
			}
			return string.Empty;
		}

		// Token: 0x06001D96 RID: 7574 RVA: 0x00055E38 File Offset: 0x00054038
		private void SetLocationPart(Action<ILocation> setter)
		{
			string ownAttribute = this.GetOwnAttribute(AttributeNames.Href);
			Url url = ((ownAttribute != null) ? new Url(base.BaseUrl, ownAttribute) : null);
			if (url == null || url.IsInvalid)
			{
				url = new Url(base.BaseUrl);
			}
			Location location = new Location(url);
			setter(location);
			this.SetOwnAttribute(AttributeNames.Href, location.Href, false);
		}

		// Token: 0x04000D08 RID: 3336
		private TokenList _relList;

		// Token: 0x04000D09 RID: 3337
		private SettableTokenList _ping;
	}
}
