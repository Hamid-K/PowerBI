using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator.Services
{
	// Token: 0x02001DA6 RID: 7590
	public abstract class TrackingService<T> : ITrackingService<T>
	{
		// Token: 0x0600BC2D RID: 48173 RVA: 0x002613E6 File Offset: 0x0025F5E6
		public TrackingService()
		{
			this.items = new HashSet<T>(ObjectComparer<T>.Instance);
		}

		// Token: 0x0600BC2E RID: 48174 RVA: 0x00261400 File Offset: 0x0025F600
		public void Register(T item)
		{
			T t = default(T);
			HashSet<T> hashSet = this.items;
			lock (hashSet)
			{
				if (this.closed)
				{
					t = item;
				}
				else
				{
					if (this.closing)
					{
						using (IHostTrace hostTrace = EvaluatorTracing.CreateTrace("TrackingService/RegisterClosing", null, TraceEventType.Information, null))
						{
							hostTrace.Add("Tracker", base.GetType().FullName, false);
							IHostTrace hostTrace2 = hostTrace;
							string text = "Item";
							ref T ptr = ref item;
							T t2 = default(T);
							object obj;
							if (t2 == null)
							{
								t2 = item;
								ptr = ref t2;
								if (t2 == null)
								{
									obj = null;
									goto IL_0094;
								}
							}
							obj = ptr.GetType().FullName;
							IL_0094:
							hostTrace2.Add(text, obj, false);
						}
					}
					this.items.Add(item);
				}
			}
			if (t != null)
			{
				this.RegisterAfterClose(t);
			}
		}

		// Token: 0x0600BC2F RID: 48175 RVA: 0x002614F8 File Offset: 0x0025F6F8
		public void Unregister(T task)
		{
			HashSet<T> hashSet = this.items;
			lock (hashSet)
			{
				this.items.Remove(task);
			}
		}

		// Token: 0x0600BC30 RID: 48176 RVA: 0x00261540 File Offset: 0x0025F740
		public IEnumerable<T> RemoveAllThenClose()
		{
			for (;;)
			{
				HashSet<T> hashSet = this.items;
				T[] array;
				lock (hashSet)
				{
					this.closing = true;
					array = this.items.ToArray<T>();
					this.items.Clear();
					if (array.Length == 0)
					{
						this.closed = true;
						yield break;
					}
				}
				foreach (T t in array)
				{
					yield return t;
				}
				T[] array2 = null;
			}
			yield break;
		}

		// Token: 0x0600BC31 RID: 48177
		protected abstract void RegisterAfterClose(T item);

		// Token: 0x04005FC7 RID: 24519
		private readonly HashSet<T> items;

		// Token: 0x04005FC8 RID: 24520
		private bool closing;

		// Token: 0x04005FC9 RID: 24521
		private bool closed;
	}
}
