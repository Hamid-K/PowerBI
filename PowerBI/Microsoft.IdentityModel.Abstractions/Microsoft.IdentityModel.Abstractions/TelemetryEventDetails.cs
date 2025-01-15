using System;
using System.Collections.Generic;

namespace Microsoft.IdentityModel.Abstractions
{
	// Token: 0x02000009 RID: 9
	public abstract class TelemetryEventDetails
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000020E3 File Offset: 0x000002E3
		protected internal IDictionary<string, object> PropertyValues { get; } = new Dictionary<string, object>();

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000020EB File Offset: 0x000002EB
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000020F3 File Offset: 0x000002F3
		public virtual string Name { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000020FC File Offset: 0x000002FC
		public virtual IReadOnlyDictionary<string, object> Properties
		{
			get
			{
				return (IReadOnlyDictionary<string, object>)this.PropertyValues;
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002109 File Offset: 0x00000309
		public virtual void SetProperty(string key, string value)
		{
			this.SetPropertyCore(key, value);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002113 File Offset: 0x00000313
		public virtual void SetProperty(string key, long value)
		{
			this.SetPropertyCore(key, value);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002122 File Offset: 0x00000322
		public virtual void SetProperty(string key, bool value)
		{
			this.SetPropertyCore(key, value);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002131 File Offset: 0x00000331
		public virtual void SetProperty(string key, DateTime value)
		{
			this.SetPropertyCore(key, value);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002140 File Offset: 0x00000340
		public virtual void SetProperty(string key, double value)
		{
			this.SetPropertyCore(key, value);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000214F File Offset: 0x0000034F
		public virtual void SetProperty(string key, Guid value)
		{
			this.SetPropertyCore(key, value);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000215E File Offset: 0x0000035E
		private void SetPropertyCore(string key, object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.PropertyValues[key] = value;
		}
	}
}
