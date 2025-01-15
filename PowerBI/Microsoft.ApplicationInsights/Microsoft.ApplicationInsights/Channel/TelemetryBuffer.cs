using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights.Extensibility.Implementation.Tracing;

namespace Microsoft.ApplicationInsights.Channel
{
	// Token: 0x020000E5 RID: 229
	internal class TelemetryBuffer
	{
		// Token: 0x06000861 RID: 2145 RVA: 0x0001B030 File Offset: 0x00019230
		internal TelemetryBuffer()
		{
			this.items = new List<ITelemetry>(this.Capacity);
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x0001B080 File Offset: 0x00019280
		// (set) Token: 0x06000863 RID: 2147 RVA: 0x0001B088 File Offset: 0x00019288
		public int Capacity
		{
			get
			{
				return this.capacity;
			}
			set
			{
				if (value < 1)
				{
					this.capacity = 500;
					return;
				}
				if (value > this.backlogSize)
				{
					this.capacity = this.backlogSize;
					return;
				}
				this.capacity = value;
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x0001B0B7 File Offset: 0x000192B7
		// (set) Token: 0x06000865 RID: 2149 RVA: 0x0001B0BF File Offset: 0x000192BF
		public int BacklogSize
		{
			get
			{
				return this.backlogSize;
			}
			set
			{
				if (value < this.minimumBacklogSize)
				{
					this.backlogSize = this.minimumBacklogSize;
					return;
				}
				if (value < this.capacity)
				{
					this.backlogSize = this.capacity;
					return;
				}
				this.backlogSize = value;
			}
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0001B0F4 File Offset: 0x000192F4
		public void Enqueue(ITelemetry item)
		{
			if (item == null)
			{
				CoreEventSource.Log.LogVerbose("item is null in TelemetryBuffer.Enqueue", "Incorrect");
				return;
			}
			object obj = this.lockObj;
			lock (obj)
			{
				if (this.items.Count >= this.BacklogSize)
				{
					if (!this.itemDroppedMessageLogged)
					{
						CoreEventSource.Log.ItemDroppedAsMaximumUnsentBacklogSizeReached(this.BacklogSize, "Incorrect");
						this.itemDroppedMessageLogged = true;
					}
				}
				else
				{
					this.items.Add(item);
					if (this.items.Count >= this.Capacity)
					{
						Action onFull = this.OnFull;
						if (onFull != null)
						{
							onFull();
						}
					}
				}
			}
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0001B1B0 File Offset: 0x000193B0
		public virtual IEnumerable<ITelemetry> Dequeue()
		{
			List<ITelemetry> list = null;
			if (this.items.Count > 0)
			{
				object obj = this.lockObj;
				lock (obj)
				{
					if (this.items.Count > 0)
					{
						list = this.items;
						this.items = new List<ITelemetry>(this.Capacity);
						this.itemDroppedMessageLogged = false;
					}
				}
			}
			return list;
		}

		// Token: 0x0400032C RID: 812
		public Action OnFull;

		// Token: 0x0400032D RID: 813
		private const int DefaultCapacity = 500;

		// Token: 0x0400032E RID: 814
		private const int DefaultBacklogSize = 1000000;

		// Token: 0x0400032F RID: 815
		private readonly object lockObj = new object();

		// Token: 0x04000330 RID: 816
		private int capacity = 500;

		// Token: 0x04000331 RID: 817
		private int backlogSize = 1000000;

		// Token: 0x04000332 RID: 818
		private int minimumBacklogSize = 1001;

		// Token: 0x04000333 RID: 819
		private List<ITelemetry> items;

		// Token: 0x04000334 RID: 820
		private bool itemDroppedMessageLogged;
	}
}
