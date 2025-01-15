using System;
using AngleSharp.Dom;
using AngleSharp.Network;

namespace AngleSharp
{
	// Token: 0x0200000B RID: 11
	public interface IBrowsingContext : IEventTarget
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000050 RID: 80
		IWindow Current { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000051 RID: 81
		// (set) Token: 0x06000052 RID: 82
		IDocument Active { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000053 RID: 83
		IHistory SessionHistory { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000054 RID: 84
		Sandboxes Security { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000055 RID: 85
		IConfiguration Configuration { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000056 RID: 86
		IDocumentLoader Loader { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000057 RID: 87
		IBrowsingContext Parent { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000058 RID: 88
		IDocument Creator { get; }

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000059 RID: 89
		// (remove) Token: 0x0600005A RID: 90
		event DomEventHandler Parsing;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600005B RID: 91
		// (remove) Token: 0x0600005C RID: 92
		event DomEventHandler Parsed;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600005D RID: 93
		// (remove) Token: 0x0600005E RID: 94
		event DomEventHandler ParseError;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x0600005F RID: 95
		// (remove) Token: 0x06000060 RID: 96
		event DomEventHandler Requesting;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000061 RID: 97
		// (remove) Token: 0x06000062 RID: 98
		event DomEventHandler Requested;
	}
}
