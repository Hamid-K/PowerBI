using System;
using System.Collections.Generic;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000017 RID: 23
	public class EventCommandEventArgs : EventArgs
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000086B0 File Offset: 0x000068B0
		// (set) Token: 0x060000DF RID: 223 RVA: 0x000086B8 File Offset: 0x000068B8
		public EventCommand Command { get; internal set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x000086C1 File Offset: 0x000068C1
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x000086C9 File Offset: 0x000068C9
		public IDictionary<string, string> Arguments { get; internal set; }

		// Token: 0x060000E2 RID: 226 RVA: 0x000086D2 File Offset: 0x000068D2
		public bool EnableEvent(int eventId)
		{
			if (this.Command != EventCommand.Enable && this.Command != EventCommand.Disable)
			{
				throw new InvalidOperationException();
			}
			return this.eventSource.EnableEventForDispatcher(this.dispatcher, eventId, true);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00008701 File Offset: 0x00006901
		public bool DisableEvent(int eventId)
		{
			if (this.Command != EventCommand.Enable && this.Command != EventCommand.Disable)
			{
				throw new InvalidOperationException();
			}
			return this.eventSource.EnableEventForDispatcher(this.dispatcher, eventId, false);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00008730 File Offset: 0x00006930
		internal EventCommandEventArgs(EventCommand command, IDictionary<string, string> arguments, EventSource eventSource, EventListener listener, int perEventSourceSessionId, int etwSessionId, bool enable, EventLevel level, EventKeywords matchAnyKeyword)
		{
			this.Command = command;
			this.Arguments = arguments;
			this.eventSource = eventSource;
			this.listener = listener;
			this.perEventSourceSessionId = perEventSourceSessionId;
			this.etwSessionId = etwSessionId;
			this.enable = enable;
			this.level = level;
			this.matchAnyKeyword = matchAnyKeyword;
		}

		// Token: 0x0400005A RID: 90
		internal EventSource eventSource;

		// Token: 0x0400005B RID: 91
		internal EventDispatcher dispatcher;

		// Token: 0x0400005C RID: 92
		internal EventListener listener;

		// Token: 0x0400005D RID: 93
		internal int perEventSourceSessionId;

		// Token: 0x0400005E RID: 94
		internal int etwSessionId;

		// Token: 0x0400005F RID: 95
		internal bool enable;

		// Token: 0x04000060 RID: 96
		internal EventLevel level;

		// Token: 0x04000061 RID: 97
		internal EventKeywords matchAnyKeyword;

		// Token: 0x04000062 RID: 98
		internal EventCommandEventArgs nextCommand;
	}
}
