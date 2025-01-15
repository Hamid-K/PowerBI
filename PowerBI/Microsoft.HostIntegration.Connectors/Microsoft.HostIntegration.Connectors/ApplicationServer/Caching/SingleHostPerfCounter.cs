using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000391 RID: 913
	internal class SingleHostPerfCounter : HostPerfCounter
	{
		// Token: 0x0600202A RID: 8234 RVA: 0x00061E9C File Offset: 0x0006009C
		public SingleHostPerfCounter(HostPerfCounter.Name name, bool register)
			: base(name, register)
		{
		}

		// Token: 0x0600202B RID: 8235 RVA: 0x00061EA6 File Offset: 0x000600A6
		internal override long GetValue()
		{
			return this._value;
		}

		// Token: 0x0600202C RID: 8236 RVA: 0x00061EAE File Offset: 0x000600AE
		internal void UpdateCounterValue(long value)
		{
			this._value = value;
			this.Update();
		}

		// Token: 0x0600202D RID: 8237 RVA: 0x00061EBD File Offset: 0x000600BD
		internal void Close()
		{
			base.Delete();
		}

		// Token: 0x04001302 RID: 4866
		private long _value;
	}
}
