using System;
using AngleSharp.Dom;
using AngleSharp.Extensions;
using AngleSharp.Html;
using AngleSharp.Network;

namespace AngleSharp
{
	// Token: 0x02000007 RID: 7
	public sealed class BrowsingContext : EventTarget, IBrowsingContext, IEventTarget, IDisposable
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000022 RID: 34 RVA: 0x000024F7 File Offset: 0x000006F7
		// (remove) Token: 0x06000023 RID: 35 RVA: 0x00002506 File Offset: 0x00000706
		event DomEventHandler IBrowsingContext.Parsing
		{
			add
			{
				base.AddEventListener(EventNames.ParseStart, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.ParseStart, value, false);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000024 RID: 36 RVA: 0x00002515 File Offset: 0x00000715
		// (remove) Token: 0x06000025 RID: 37 RVA: 0x00002524 File Offset: 0x00000724
		event DomEventHandler IBrowsingContext.Parsed
		{
			add
			{
				base.AddEventListener(EventNames.ParseEnd, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.ParseEnd, value, false);
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000026 RID: 38 RVA: 0x00002533 File Offset: 0x00000733
		// (remove) Token: 0x06000027 RID: 39 RVA: 0x00002542 File Offset: 0x00000742
		event DomEventHandler IBrowsingContext.Requesting
		{
			add
			{
				base.AddEventListener(EventNames.RequestStart, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.RequestStart, value, false);
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000028 RID: 40 RVA: 0x00002551 File Offset: 0x00000751
		// (remove) Token: 0x06000029 RID: 41 RVA: 0x00002560 File Offset: 0x00000760
		event DomEventHandler IBrowsingContext.Requested
		{
			add
			{
				base.AddEventListener(EventNames.RequestEnd, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.RequestEnd, value, false);
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600002A RID: 42 RVA: 0x0000256F File Offset: 0x0000076F
		// (remove) Token: 0x0600002B RID: 43 RVA: 0x0000257E File Offset: 0x0000077E
		event DomEventHandler IBrowsingContext.ParseError
		{
			add
			{
				base.AddEventListener(EventNames.ParseError, value, false);
			}
			remove
			{
				base.RemoveEventListener(EventNames.ParseError, value, false);
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000258D File Offset: 0x0000078D
		internal BrowsingContext(IConfiguration configuration, Sandboxes security)
		{
			this._configuration = configuration;
			this._security = security;
			this._loader = this.CreateService<IDocumentLoader>();
			this._history = this.CreateService<IHistory>();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000025BB File Offset: 0x000007BB
		internal BrowsingContext(IBrowsingContext parent, Sandboxes security)
			: this(parent.Configuration, security)
		{
			this._parent = parent;
			this._creator = this._parent.Active;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000025E2 File Offset: 0x000007E2
		// (set) Token: 0x0600002F RID: 47 RVA: 0x000025EA File Offset: 0x000007EA
		public IDocument Active { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000025F3 File Offset: 0x000007F3
		public IDocumentLoader Loader
		{
			get
			{
				return this._loader;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000025FB File Offset: 0x000007FB
		public IConfiguration Configuration
		{
			get
			{
				return this._configuration;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002603 File Offset: 0x00000803
		public IDocument Creator
		{
			get
			{
				return this._creator;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000260B File Offset: 0x0000080B
		public IWindow Current
		{
			get
			{
				IDocument active = this.Active;
				if (active == null)
				{
					return null;
				}
				return active.DefaultView;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000034 RID: 52 RVA: 0x0000261E File Offset: 0x0000081E
		public IBrowsingContext Parent
		{
			get
			{
				return this._parent;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002626 File Offset: 0x00000826
		public IHistory SessionHistory
		{
			get
			{
				return this._history;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000036 RID: 54 RVA: 0x0000262E File Offset: 0x0000082E
		public Sandboxes Security
		{
			get
			{
				return this._security;
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002636 File Offset: 0x00000836
		public static IBrowsingContext New(IConfiguration configuration = null)
		{
			if (configuration == null)
			{
				configuration = AngleSharp.Configuration.Default;
			}
			return configuration.NewContext(Sandboxes.None);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002649 File Offset: 0x00000849
		void IDisposable.Dispose()
		{
			IDocument active = this.Active;
			if (active != null)
			{
				active.Dispose();
			}
			this.Active = null;
		}

		// Token: 0x04000008 RID: 8
		private readonly IConfiguration _configuration;

		// Token: 0x04000009 RID: 9
		private readonly Sandboxes _security;

		// Token: 0x0400000A RID: 10
		private readonly IBrowsingContext _parent;

		// Token: 0x0400000B RID: 11
		private readonly IDocument _creator;

		// Token: 0x0400000C RID: 12
		private readonly IDocumentLoader _loader;

		// Token: 0x0400000D RID: 13
		private readonly IHistory _history;
	}
}
