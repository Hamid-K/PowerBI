using System;
using System.Collections;
using System.Diagnostics;
using System.Security.Permissions;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000852 RID: 2130
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public class DrdaConsoleTraceListener : ConsoleTraceListener, IDrdaTraceListener
	{
		// Token: 0x060043E2 RID: 17378 RVA: 0x000E4C24 File Offset: 0x000E2E24
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

		// Token: 0x060043E3 RID: 17379 RVA: 0x000E4D0C File Offset: 0x000E2F0C
		protected override string[] GetSupportedAttributes()
		{
			return new string[] { "TraceLevel", "traceLevel", "Settings", "settings" };
		}

		// Token: 0x17001017 RID: 4119
		// (get) Token: 0x060043E4 RID: 17380 RVA: 0x000E4D34 File Offset: 0x000E2F34
		// (set) Token: 0x060043E5 RID: 17381 RVA: 0x000E4D4A File Offset: 0x000E2F4A
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

		// Token: 0x17001018 RID: 4120
		// (get) Token: 0x060043E6 RID: 17382 RVA: 0x000E4D53 File Offset: 0x000E2F53
		// (set) Token: 0x060043E7 RID: 17383 RVA: 0x000E4D69 File Offset: 0x000E2F69
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

		// Token: 0x17001019 RID: 4121
		// (get) Token: 0x060043E9 RID: 17385 RVA: 0x000E4D7B File Offset: 0x000E2F7B
		// (set) Token: 0x060043E8 RID: 17384 RVA: 0x000E4D72 File Offset: 0x000E2F72
		public long MaxTraceEntries { get; set; }

		// Token: 0x1700101A RID: 4122
		// (get) Token: 0x060043EB RID: 17387 RVA: 0x000E4D8C File Offset: 0x000E2F8C
		// (set) Token: 0x060043EA RID: 17386 RVA: 0x000E4D83 File Offset: 0x000E2F83
		public int MaxTraceFiles { get; set; }

		// Token: 0x1700101B RID: 4123
		// (get) Token: 0x060043EC RID: 17388 RVA: 0x00002B16 File Offset: 0x00000D16
		public bool AutoFlush
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060043ED RID: 17389 RVA: 0x000E4D94 File Offset: 0x000E2F94
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
		{
			if (eventType != TraceEventType.Error)
			{
				if (eventType == TraceEventType.Warning)
				{
					base.WriteLine(string.Format("Warning:{0}:{1}", id, message));
					return;
				}
				if (eventType == TraceEventType.Information)
				{
					base.WriteLine(string.Format("Information:{0}:{1}", id, message));
					return;
				}
			}
			else
			{
				base.WriteLine(string.Format("Error:{0}:{1}", id, message));
			}
		}

		// Token: 0x060043EE RID: 17390 RVA: 0x000E4DFC File Offset: 0x000E2FFC
		public void TraceEvent(TraceEventType eventType, int id, string message)
		{
			if (eventType != TraceEventType.Error)
			{
				if (eventType != TraceEventType.Warning)
				{
					if (eventType == TraceEventType.Information)
					{
						base.WriteLine(string.Format("Information:{0}:{1}", id, message));
					}
				}
				else
				{
					base.WriteLine(string.Format("Warning:{0}:{1}", id, message));
				}
			}
			else
			{
				base.WriteLine(string.Format("Error:{0}:{1}", id, message));
			}
			base.Flush();
		}

		// Token: 0x04002FBA RID: 12218
		private int _level = 4;

		// Token: 0x04002FBB RID: 12219
		private string _settings;

		// Token: 0x04002FBC RID: 12220
		private bool _attributeValuesInited;
	}
}
