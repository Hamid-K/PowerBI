using System;
using System.Collections.Generic;
using System.Threading;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000006 RID: 6
	internal class ContextAwareEvent<T> where T : EventArgs
	{
		// Token: 0x06000024 RID: 36 RVA: 0x000027F5 File Offset: 0x000009F5
		public ContextAwareEvent()
		{
			this.eventHandlerMap = new Dictionary<EventHandler<T>, ContextAwareEvent<T>.ContextAwareEventHandler>();
			this.syncObject = new object();
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002814 File Offset: 0x00000A14
		public EventHandler<T> EventHandler
		{
			get
			{
				object obj = this.syncObject;
				EventHandler<T> eventHandler;
				lock (obj)
				{
					eventHandler = this.eventHandler;
				}
				return eventHandler;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002858 File Offset: 0x00000A58
		public void AddContextAwareHandler(EventHandler<T> handler)
		{
			object obj = this.syncObject;
			lock (obj)
			{
				this.eventHandler = (EventHandler<T>)Delegate.Combine(this.eventHandler, handler);
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000028AC File Offset: 0x00000AAC
		public void AddHandler(EventHandler<T> handler)
		{
			ContextAwareEvent<T>.ContextAwareEventHandler contextAwareEventHandler = new ContextAwareEvent<T>.ContextAwareEventHandler(handler);
			object obj = this.syncObject;
			lock (obj)
			{
				this.eventHandler = (EventHandler<T>)Delegate.Combine(this.eventHandler, new EventHandler<T>(contextAwareEventHandler.Handler));
				this.eventHandlerMap[handler] = contextAwareEventHandler;
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000291C File Offset: 0x00000B1C
		public void RemoveHandler(EventHandler<T> handler)
		{
			object obj = this.syncObject;
			lock (obj)
			{
				ContextAwareEvent<T>.ContextAwareEventHandler contextAwareEventHandler;
				if (this.eventHandlerMap.TryGetValue(handler, out contextAwareEventHandler))
				{
					this.eventHandler = (EventHandler<T>)Delegate.Remove(this.eventHandler, new EventHandler<T>(contextAwareEventHandler.Handler));
					this.eventHandlerMap.Remove(handler);
				}
			}
		}

		// Token: 0x04000006 RID: 6
		private readonly Dictionary<EventHandler<T>, ContextAwareEvent<T>.ContextAwareEventHandler> eventHandlerMap;

		// Token: 0x04000007 RID: 7
		private readonly object syncObject;

		// Token: 0x04000008 RID: 8
		private EventHandler<T> eventHandler;

		// Token: 0x02000054 RID: 84
		private class ContextAwareEventHandler
		{
			// Token: 0x060003E8 RID: 1000 RVA: 0x0000F0FE File Offset: 0x0000D2FE
			public ContextAwareEventHandler(EventHandler<T> handler)
			{
				this.handler = handler;
				this.executionContext = ExecutionContext.Capture();
			}

			// Token: 0x060003E9 RID: 1001 RVA: 0x0000F118 File Offset: 0x0000D318
			public void Handler(object sender, T e)
			{
				ExecutionContext.Run(this.executionContext.CreateCopy(), delegate(object state)
				{
					this.handler(sender, e);
				}, null);
			}

			// Token: 0x040001E5 RID: 485
			private readonly EventHandler<T> handler;

			// Token: 0x040001E6 RID: 486
			private readonly ExecutionContext executionContext;
		}
	}
}
