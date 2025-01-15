using System;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.Eventing;
using System.Security.Permissions;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000853 RID: 2131
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public class DrdaEventProviderTraceListener : EventProviderTraceListener, IDrdaTraceListener
	{
		// Token: 0x060043EF RID: 17391 RVA: 0x000E4E64 File Offset: 0x000E3064
		public DrdaEventProviderTraceListener(string providerId)
			: base(providerId)
		{
		}

		// Token: 0x060043F0 RID: 17392 RVA: 0x000E4E74 File Offset: 0x000E3074
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

		// Token: 0x060043F1 RID: 17393 RVA: 0x000E4D0C File Offset: 0x000E2F0C
		protected override string[] GetSupportedAttributes()
		{
			return new string[] { "TraceLevel", "traceLevel", "Settings", "settings" };
		}

		// Token: 0x1700101C RID: 4124
		// (get) Token: 0x060043F2 RID: 17394 RVA: 0x000E4F5C File Offset: 0x000E315C
		// (set) Token: 0x060043F3 RID: 17395 RVA: 0x000E4F72 File Offset: 0x000E3172
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

		// Token: 0x1700101D RID: 4125
		// (get) Token: 0x060043F4 RID: 17396 RVA: 0x000E4F7B File Offset: 0x000E317B
		// (set) Token: 0x060043F5 RID: 17397 RVA: 0x000E4F91 File Offset: 0x000E3191
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

		// Token: 0x1700101E RID: 4126
		// (get) Token: 0x060043F7 RID: 17399 RVA: 0x000E4FA3 File Offset: 0x000E31A3
		// (set) Token: 0x060043F6 RID: 17398 RVA: 0x000E4F9A File Offset: 0x000E319A
		public long MaxTraceEntries { get; set; }

		// Token: 0x1700101F RID: 4127
		// (get) Token: 0x060043F9 RID: 17401 RVA: 0x000E4FB4 File Offset: 0x000E31B4
		// (set) Token: 0x060043F8 RID: 17400 RVA: 0x000E4FAB File Offset: 0x000E31AB
		public int MaxTraceFiles { get; set; }

		// Token: 0x17001020 RID: 4128
		// (get) Token: 0x060043FA RID: 17402 RVA: 0x00002B16 File Offset: 0x00000D16
		public bool AutoFlush
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060043FB RID: 17403 RVA: 0x000E4FBC File Offset: 0x000E31BC
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

		// Token: 0x04002FBF RID: 12223
		private int _level = 4;

		// Token: 0x04002FC0 RID: 12224
		private string _settings;

		// Token: 0x04002FC1 RID: 12225
		private bool _attributeValuesInited;
	}
}
