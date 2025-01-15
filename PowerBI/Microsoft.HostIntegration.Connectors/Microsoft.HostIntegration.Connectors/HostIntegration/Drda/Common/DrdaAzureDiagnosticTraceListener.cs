using System;
using System.Collections;
using System.Diagnostics;
using System.Security.Permissions;
using Microsoft.WindowsAzure.Diagnostics;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000854 RID: 2132
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public class DrdaAzureDiagnosticTraceListener : DiagnosticMonitorTraceListener, IDrdaAzureTraceListener, IDrdaTraceListener
	{
		// Token: 0x060043FD RID: 17405 RVA: 0x000E504C File Offset: 0x000E324C
		public void InitAttributeValues()
		{
			lock (this)
			{
				if (!this._attributeValuesInited)
				{
					this._attributeValuesInited = true;
					foreach (object obj in base.Attributes)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						if (dictionaryEntry.Key.ToString().Equals("settings", StringComparison.InvariantCultureIgnoreCase))
						{
							this._settings = dictionaryEntry.Value.ToString();
						}
						else if (dictionaryEntry.Key.ToString().Equals("tracelevel", StringComparison.InvariantCultureIgnoreCase))
						{
							this._level = int.Parse(dictionaryEntry.Value.ToString());
						}
					}
				}
			}
		}

		// Token: 0x060043FE RID: 17406 RVA: 0x000E4D0C File Offset: 0x000E2F0C
		protected override string[] GetSupportedAttributes()
		{
			return new string[] { "TraceLevel", "traceLevel", "Settings", "settings" };
		}

		// Token: 0x17001021 RID: 4129
		// (get) Token: 0x060043FF RID: 17407 RVA: 0x000E5134 File Offset: 0x000E3334
		// (set) Token: 0x06004400 RID: 17408 RVA: 0x000E514A File Offset: 0x000E334A
		public int TraceLevel
		{
			get
			{
				if (!this._attributeValuesInited)
				{
					this.InitAttributeValues();
				}
				return this._level;
			}
			set
			{
				this._level = value;
			}
		}

		// Token: 0x17001022 RID: 4130
		// (get) Token: 0x06004401 RID: 17409 RVA: 0x000E5153 File Offset: 0x000E3353
		// (set) Token: 0x06004402 RID: 17410 RVA: 0x000E5169 File Offset: 0x000E3369
		public string Settings
		{
			get
			{
				if (!this._attributeValuesInited)
				{
					this.InitAttributeValues();
				}
				return this._settings;
			}
			set
			{
				this._settings = value;
			}
		}

		// Token: 0x17001023 RID: 4131
		// (get) Token: 0x06004404 RID: 17412 RVA: 0x000E517B File Offset: 0x000E337B
		// (set) Token: 0x06004403 RID: 17411 RVA: 0x000E5172 File Offset: 0x000E3372
		public long MaxTraceEntries { get; set; }

		// Token: 0x17001024 RID: 4132
		// (get) Token: 0x06004406 RID: 17414 RVA: 0x000E518C File Offset: 0x000E338C
		// (set) Token: 0x06004405 RID: 17413 RVA: 0x000E5183 File Offset: 0x000E3383
		public int MaxTraceFiles { get; set; }

		// Token: 0x17001025 RID: 4133
		// (get) Token: 0x06004407 RID: 17415 RVA: 0x00002B16 File Offset: 0x00000D16
		public bool AutoFlush
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004408 RID: 17416 RVA: 0x000E5194 File Offset: 0x000E3394
		public void TraceEvent(TraceEventType eventType, int id, string message)
		{
			if (eventType != TraceEventType.Error)
			{
				if (eventType != TraceEventType.Warning)
				{
					if (eventType == TraceEventType.Information)
					{
						base.TraceEvent(null, "MsDrdaService", eventType, id, string.Format("Information:{0}:{1}", id, message));
					}
				}
				else
				{
					base.TraceEvent(null, "MsDrdaService", eventType, id, string.Format("Warning:{0}:{1}", id, message));
				}
			}
			else
			{
				base.TraceEvent(null, "MsDrdaService", eventType, id, string.Format("Error:{0}:{1}", id, message));
			}
			base.Flush();
		}

		// Token: 0x04002FC4 RID: 12228
		private int _level = 4;

		// Token: 0x04002FC5 RID: 12229
		private string _settings;

		// Token: 0x04002FC6 RID: 12230
		private bool _attributeValuesInited;
	}
}
