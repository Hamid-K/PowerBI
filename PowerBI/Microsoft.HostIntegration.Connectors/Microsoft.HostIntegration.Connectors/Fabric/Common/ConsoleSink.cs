using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003CD RID: 973
	internal class ConsoleSink : IEventSink
	{
		// Token: 0x0600223A RID: 8762 RVA: 0x00069BDC File Offset: 0x00067DDC
		private static Dictionary<string, ConsoleColor> GetColorMapping()
		{
			Dictionary<string, ConsoleColor> dictionary = new Dictionary<string, ConsoleColor>();
			ConsoleSink.s_oldColor = Console.ForegroundColor;
			ConsoleSink.AddColor(dictionary, "Error", "Red");
			ConsoleSink.AddColor(dictionary, "Warning", "Yellow");
			ConsoleSink.AddColor(dictionary, "Verbose", "Cyan");
			ConfigFile config = ConfigFile.Config;
			if (config != null)
			{
				Hashtable hashtable = (Hashtable)config.GetValue("colorMapping");
				if (hashtable != null)
				{
					foreach (object obj in hashtable.Keys)
					{
						string text = (string)obj;
						ConsoleSink.AddColor(dictionary, text, (string)hashtable[text]);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x0600223B RID: 8763 RVA: 0x00069CA8 File Offset: 0x00067EA8
		private static void AddColor(Dictionary<string, ConsoleColor> map, string key, string color)
		{
			int backgroundColor = (int)Console.BackgroundColor;
			bool flag = backgroundColor <= 7;
			int num = (int)Enum.Parse(typeof(ConsoleColor), color);
			if (flag)
			{
				num |= 8;
			}
			else
			{
				num &= 7;
			}
			map[key] = (ConsoleColor)num;
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x0600223C RID: 8764 RVA: 0x00069CEE File Offset: 0x00067EEE
		// (set) Token: 0x0600223D RID: 8765 RVA: 0x00069CF5 File Offset: 0x00067EF5
		public static bool DisableLogging
		{
			get
			{
				return ConsoleSink.s_bDisableLogging;
			}
			set
			{
				ConsoleSink.s_bDisableLogging = value;
			}
		}

		// Token: 0x0600223E RID: 8766 RVA: 0x00069D00 File Offset: 0x00067F00
		public void WriteEntry(string src, TraceEventType msgType, string msgText)
		{
			if (!ConsoleSink.s_bDisableLogging)
			{
				if (!string.IsNullOrEmpty(this.m_timeFormat) && msgText.Length > 0)
				{
					msgText = msgText + " [" + DateTime.Now.ToString(this.m_timeFormat, CultureInfo.InvariantCulture) + "]";
				}
				ConsoleSink.WriteLine(msgType.ToString(), "{0}", new object[] { msgText });
			}
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x00069D78 File Offset: 0x00067F78
		public static void WriteLine(string key, string format, params object[] args)
		{
			lock (ConsoleSink.s_colorMapping)
			{
				ConsoleColor consoleColor = ConsoleSink.s_oldColor;
				try
				{
					if (ConsoleSink.s_colorMapping.TryGetValue(key, out consoleColor))
					{
						Console.ForegroundColor = consoleColor;
					}
					Console.WriteLine(format, args);
				}
				finally
				{
					if (consoleColor != ConsoleSink.s_oldColor)
					{
						Console.ForegroundColor = ConsoleSink.s_oldColor;
					}
				}
			}
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x00069DF0 File Offset: 0x00067FF0
		public bool Load(string id)
		{
			this.m_timeFormat = id;
			return true;
		}

		// Token: 0x040015A3 RID: 5539
		private string m_timeFormat;

		// Token: 0x040015A4 RID: 5540
		private static Dictionary<string, ConsoleColor> s_colorMapping = ConsoleSink.GetColorMapping();

		// Token: 0x040015A5 RID: 5541
		private static ConsoleColor s_oldColor;

		// Token: 0x040015A6 RID: 5542
		private static bool s_bDisableLogging = false;
	}
}
