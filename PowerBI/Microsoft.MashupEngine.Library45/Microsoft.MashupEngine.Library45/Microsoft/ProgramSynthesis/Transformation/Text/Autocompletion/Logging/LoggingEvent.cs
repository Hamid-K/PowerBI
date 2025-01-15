using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.ProgramSynthesis.Wrangling.Logging;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Autocompletion.Logging
{
	// Token: 0x02001E18 RID: 7704
	internal abstract class LoggingEvent
	{
		// Token: 0x17002AC1 RID: 10945
		// (get) Token: 0x060101A3 RID: 65955 RVA: 0x00374A44 File Offset: 0x00372C44
		// (set) Token: 0x060101A4 RID: 65956 RVA: 0x00374A4C File Offset: 0x00372C4C
		public Guid Id { get; private set; }

		// Token: 0x17002AC2 RID: 10946
		// (get) Token: 0x060101A5 RID: 65957
		public abstract string EventName { get; }

		// Token: 0x060101A6 RID: 65958 RVA: 0x00374A55 File Offset: 0x00372C55
		protected LoggingEvent()
		{
			this.Id = Guid.NewGuid();
		}

		// Token: 0x060101A7 RID: 65959 RVA: 0x00374A68 File Offset: 0x00372C68
		protected LoggingEvent(Guid id)
		{
			this.Id = id;
		}

		// Token: 0x060101A8 RID: 65960 RVA: 0x00374A77 File Offset: 0x00372C77
		public virtual void Log(ILogger logger)
		{
			if (logger != null)
			{
				logger.TrackEvent(this.EventName, this.GetMetrics(), this.GetProperties(), this.GetUserDataProperties());
			}
		}

		// Token: 0x060101A9 RID: 65961 RVA: 0x00374A9A File Offset: 0x00372C9A
		protected virtual IReadOnlyCollection<KeyValuePair<string, double>> GetMetrics()
		{
			return new KeyValuePair<string, double>[0];
		}

		// Token: 0x060101AA RID: 65962 RVA: 0x00374AA4 File Offset: 0x00372CA4
		protected virtual IReadOnlyCollection<KeyValuePair<string, string>> GetProperties()
		{
			return new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("Id", this.Id.ToString()),
				new KeyValuePair<string, string>("ProseVersion", LoggingEvent.ProseVersion)
			};
		}

		// Token: 0x060101AB RID: 65963 RVA: 0x00374AEF File Offset: 0x00372CEF
		protected virtual IReadOnlyCollection<KeyValuePair<string, string>> GetUserDataProperties()
		{
			return new KeyValuePair<string, string>[0];
		}

		// Token: 0x04006124 RID: 24868
		protected static readonly string ProseVersion = typeof(LoggingEvent).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
	}
}
