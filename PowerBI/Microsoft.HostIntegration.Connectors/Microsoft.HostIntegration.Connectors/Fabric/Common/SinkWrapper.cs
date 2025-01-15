using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003D1 RID: 977
	internal class SinkWrapper
	{
		// Token: 0x0600225F RID: 8799 RVA: 0x00069F78 File Offset: 0x00068178
		public SinkWrapper(IEventSink sink, int defaultLevel, Hashtable sourceOverride)
		{
			this.m_sink = sink;
			this.m_defaultLevel = defaultLevel;
			this.m_maxTraceLevel = defaultLevel;
			if (sourceOverride != null)
			{
				this.m_sourceOverride = new Dictionary<string, int>(sourceOverride.Count);
				using (IDictionaryEnumerator enumerator = sourceOverride.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						int num = Utility.ToInt((string)dictionaryEntry.Value, this.m_defaultLevel);
						this.m_sourceOverride[(string)dictionaryEntry.Key] = num;
						if (num > this.m_maxTraceLevel)
						{
							this.m_maxTraceLevel = num;
						}
					}
					return;
				}
			}
			this.m_sourceOverride = null;
		}

		// Token: 0x06002260 RID: 8800 RVA: 0x0006A040 File Offset: 0x00068240
		public void WriteEntry(int msgLevel, string src, TraceEventType msgType, string msgText, params object[] args)
		{
			int num;
			if (this.m_sourceOverride != null)
			{
				string text = src;
				while (!this.m_sourceOverride.TryGetValue(text, out num))
				{
					int num2 = text.LastIndexOf('.');
					if (num2 <= 0)
					{
						num = this.m_defaultLevel;
						break;
					}
					text = text.Substring(0, num2);
				}
			}
			else
			{
				num = this.m_defaultLevel;
			}
			if (msgLevel <= num)
			{
				string text2 = string.Format(CultureInfo.InvariantCulture, msgText, args);
				this.m_sink.WriteEntry(src, msgType, text2);
			}
		}

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06002261 RID: 8801 RVA: 0x0006A0B4 File Offset: 0x000682B4
		public int MaxTraceLevel
		{
			get
			{
				return this.m_maxTraceLevel;
			}
		}

		// Token: 0x040015A7 RID: 5543
		private IEventSink m_sink;

		// Token: 0x040015A8 RID: 5544
		private int m_defaultLevel;

		// Token: 0x040015A9 RID: 5545
		private int m_maxTraceLevel;

		// Token: 0x040015AA RID: 5546
		private Dictionary<string, int> m_sourceOverride;
	}
}
