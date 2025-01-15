using System;
using System.Collections;
using System.Diagnostics;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000850 RID: 2128
	public class BaseDrdaTraceListener : TraceListener, IDrdaTraceListener
	{
		// Token: 0x060043C6 RID: 17350 RVA: 0x000E41EC File Offset: 0x000E23EC
		public BaseDrdaTraceListener()
		{
		}

		// Token: 0x060043C7 RID: 17351 RVA: 0x000E4216 File Offset: 0x000E2416
		public BaseDrdaTraceListener(string name)
			: base(name)
		{
		}

		// Token: 0x060043C8 RID: 17352 RVA: 0x000E4244 File Offset: 0x000E2444
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
						if (dictionaryEntry.Key.ToString().Equals("tracefilefolder", StringComparison.InvariantCultureIgnoreCase))
						{
							this._traceFileFolder = dictionaryEntry.Value.ToString();
						}
						else if (dictionaryEntry.Key.ToString().Equals("settings", StringComparison.InvariantCultureIgnoreCase))
						{
							this._settings = dictionaryEntry.Value.ToString();
						}
						else if (dictionaryEntry.Key.ToString().Equals("maxtraceentries", StringComparison.InvariantCultureIgnoreCase))
						{
							this._maxtraceentries = (long)int.Parse(dictionaryEntry.Value.ToString());
							if (this._maxtraceentries <= 0L)
							{
								this._maxtraceentries = 1000000L;
							}
						}
						else if (dictionaryEntry.Key.ToString().Equals("maxtracefiles", StringComparison.InvariantCultureIgnoreCase))
						{
							this._maxtracefiles = int.Parse(dictionaryEntry.Value.ToString());
							if (this._maxtracefiles <= 0)
							{
								this._maxtracefiles = 10;
							}
						}
						else if (dictionaryEntry.Key.ToString().Equals("autoflush", StringComparison.InvariantCultureIgnoreCase))
						{
							this._autoFlush = bool.Parse(dictionaryEntry.Value.ToString());
						}
						else if (dictionaryEntry.Key.ToString().Equals("tracelevel", StringComparison.InvariantCultureIgnoreCase))
						{
							this._level = int.Parse(dictionaryEntry.Value.ToString());
						}
					}
				}
			}
		}

		// Token: 0x060043C9 RID: 17353 RVA: 0x000E4444 File Offset: 0x000E2644
		protected override string[] GetSupportedAttributes()
		{
			return new string[]
			{
				"TraceLevel", "traceLevel", "MaxTraceFiles", "maxTraceFiles", "MaxTraceEntries", "maxTraceEntries", "traceFileFolder", "TraceFileFolder", "autoFlush", "autoflush",
				"Settings", "settings"
			};
		}

		// Token: 0x17001011 RID: 4113
		// (get) Token: 0x060043CA RID: 17354 RVA: 0x000E44BB File Offset: 0x000E26BB
		// (set) Token: 0x060043CB RID: 17355 RVA: 0x000E44D1 File Offset: 0x000E26D1
		public string TraceFileFolder
		{
			get
			{
				if (!this._attributeValuesInited)
				{
					this.InitAttributeValues();
				}
				return this._traceFileFolder;
			}
			set
			{
				this._traceFileFolder = value;
			}
		}

		// Token: 0x17001012 RID: 4114
		// (get) Token: 0x060043CC RID: 17356 RVA: 0x000E44DA File Offset: 0x000E26DA
		// (set) Token: 0x060043CD RID: 17357 RVA: 0x000E44F0 File Offset: 0x000E26F0
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

		// Token: 0x17001013 RID: 4115
		// (get) Token: 0x060043CE RID: 17358 RVA: 0x000E44F9 File Offset: 0x000E26F9
		// (set) Token: 0x060043CF RID: 17359 RVA: 0x000E450F File Offset: 0x000E270F
		public long MaxTraceEntries
		{
			get
			{
				if (!this._attributeValuesInited)
				{
					this.InitAttributeValues();
				}
				return this._maxtraceentries;
			}
			set
			{
				this._maxtraceentries = value;
			}
		}

		// Token: 0x17001014 RID: 4116
		// (get) Token: 0x060043D0 RID: 17360 RVA: 0x000E4518 File Offset: 0x000E2718
		// (set) Token: 0x060043D1 RID: 17361 RVA: 0x000E452E File Offset: 0x000E272E
		public int MaxTraceFiles
		{
			get
			{
				if (!this._attributeValuesInited)
				{
					this.InitAttributeValues();
				}
				return this._maxtracefiles;
			}
			set
			{
				this._maxtracefiles = value;
			}
		}

		// Token: 0x17001015 RID: 4117
		// (get) Token: 0x060043D2 RID: 17362 RVA: 0x000E4537 File Offset: 0x000E2737
		// (set) Token: 0x060043D3 RID: 17363 RVA: 0x000E454D File Offset: 0x000E274D
		public bool AutoFlush
		{
			get
			{
				if (!this._attributeValuesInited)
				{
					this.InitAttributeValues();
				}
				return this._autoFlush;
			}
			set
			{
				this._autoFlush = value;
			}
		}

		// Token: 0x17001016 RID: 4118
		// (get) Token: 0x060043D4 RID: 17364 RVA: 0x000E4556 File Offset: 0x000E2756
		// (set) Token: 0x060043D5 RID: 17365 RVA: 0x000E456C File Offset: 0x000E276C
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

		// Token: 0x060043D6 RID: 17366 RVA: 0x000036A9 File Offset: 0x000018A9
		public virtual void TraceEvent(TraceEventType type, int id, string msg)
		{
		}

		// Token: 0x060043D7 RID: 17367 RVA: 0x000036A9 File Offset: 0x000018A9
		public override void Write(string message)
		{
		}

		// Token: 0x060043D8 RID: 17368 RVA: 0x000036A9 File Offset: 0x000018A9
		public override void WriteLine(string message)
		{
		}

		// Token: 0x04002FAA RID: 12202
		private const int MAX_TRACE_FILES = 10;

		// Token: 0x04002FAB RID: 12203
		private const long MAX_TRACE_ENTRIES = 1000000L;

		// Token: 0x04002FAC RID: 12204
		private const int MAX_TRACE_ENTRIES_TO_BUFFER = -1;

		// Token: 0x04002FAD RID: 12205
		private const int TRACE_LEVEL = 4;

		// Token: 0x04002FAE RID: 12206
		protected int _level = 4;

		// Token: 0x04002FAF RID: 12207
		protected long _maxtraceentries = 1000000L;

		// Token: 0x04002FB0 RID: 12208
		protected int _maxtracefiles = 10;

		// Token: 0x04002FB1 RID: 12209
		protected bool _autoFlush = true;

		// Token: 0x04002FB2 RID: 12210
		protected long _traceCount;

		// Token: 0x04002FB3 RID: 12211
		protected string _traceFileFolder;

		// Token: 0x04002FB4 RID: 12212
		protected string _settings;

		// Token: 0x04002FB5 RID: 12213
		private bool _attributeValuesInited;
	}
}
