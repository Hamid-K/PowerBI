using System;
using System.Diagnostics;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000308 RID: 776
	internal abstract class HostMultiplePerfCounter : PerfCounter
	{
		// Token: 0x06001C8E RID: 7310 RVA: 0x00056540 File Offset: 0x00054740
		protected HostMultiplePerfCounter(HostPerfCounter.Name[] name)
		{
			SingleHostPerfCounter[] array = new SingleHostPerfCounter[name.Length];
			try
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = new SingleHostPerfCounter(name[i], false);
				}
				this._counter = array;
			}
			catch (Exception ex)
			{
				if (!this.ProcessException(ex))
				{
					throw;
				}
			}
		}

		// Token: 0x06001C8F RID: 7311 RVA: 0x000565A0 File Offset: 0x000547A0
		private new void Delete()
		{
			base.Delete();
			if (this._counter != null)
			{
				for (int i = 0; i < this._counter.Length; i++)
				{
					if (this._counter[i] != null)
					{
						this._counter[i].Close();
					}
				}
				this._counter = null;
			}
		}

		// Token: 0x06001C90 RID: 7312 RVA: 0x000565EC File Offset: 0x000547EC
		internal override void Update()
		{
			if (this._counter != null)
			{
				try
				{
					long[] values = this.GetValues();
					if (values != null)
					{
						for (int i = 0; i < this._counter.Length; i++)
						{
							this._counter[i].UpdateCounterValue(values[i]);
						}
					}
				}
				catch (Exception ex)
				{
					if (!this.ProcessException(ex))
					{
						throw;
					}
				}
			}
		}

		// Token: 0x06001C91 RID: 7313
		internal abstract long[] GetValues();

		// Token: 0x06001C92 RID: 7314 RVA: 0x00056650 File Offset: 0x00054850
		private bool ProcessException(Exception e)
		{
			if (!PerformanceMonitorCounter.CheckException(e))
			{
				return false;
			}
			this.Delete();
			if (Provider.IsEnabled(TraceLevel.Error))
			{
				EventLogWriter.WriteError(PerfCounter.LogSource, "counter updation failed Category = {0}. {1}", new object[]
				{
					HostPerfCounter.GetCategory(),
					e
				});
			}
			return true;
		}

		// Token: 0x04000F6C RID: 3948
		private SingleHostPerfCounter[] _counter;
	}
}
