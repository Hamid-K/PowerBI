using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Microsoft.Cis.Eventing.Listeners
{
	// Token: 0x0200048A RID: 1162
	public class RDConsoleTraceListener : ConsoleTraceListener
	{
		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06002862 RID: 10338 RVA: 0x00079B1F File Offset: 0x00077D1F
		// (set) Token: 0x06002863 RID: 10339 RVA: 0x00079B27 File Offset: 0x00077D27
		private ConsoleColor PreviousColor { get; set; }

		// Token: 0x06002864 RID: 10340 RVA: 0x00079B30 File Offset: 0x00077D30
		private bool ShouldTrace(object o)
		{
			return o != null && !string.Empty.Equals(o) && !Guid.Empty.Equals(o) && !0.Equals(o);
		}

		// Token: 0x06002865 RID: 10341 RVA: 0x00079B74 File Offset: 0x00077D74
		private void SetConsoleColor(TraceEventType eventType)
		{
			this.PreviousColor = Console.ForegroundColor;
			switch (eventType)
			{
			case TraceEventType.Critical:
				Console.ForegroundColor = ConsoleColor.Magenta;
				return;
			case TraceEventType.Error:
				Console.ForegroundColor = ConsoleColor.Red;
				return;
			case (TraceEventType)3:
				break;
			case TraceEventType.Warning:
				Console.ForegroundColor = ConsoleColor.Yellow;
				return;
			default:
				if (eventType == TraceEventType.Information)
				{
					Console.ForegroundColor = ConsoleColor.Green;
					return;
				}
				if (eventType != TraceEventType.Verbose)
				{
					return;
				}
				Console.ForegroundColor = ConsoleColor.Gray;
				break;
			}
		}

		// Token: 0x06002866 RID: 10342 RVA: 0x00079BD6 File Offset: 0x00077DD6
		private void ResetConsoleColor()
		{
			Console.ForegroundColor = this.PreviousColor;
		}

		// Token: 0x06002867 RID: 10343 RVA: 0x00079BE3 File Offset: 0x00077DE3
		private void Write(TraceEventType eventType, string message)
		{
			this.SetConsoleColor(eventType);
			this.Write(message);
			this.ResetConsoleColor();
		}

		// Token: 0x06002868 RID: 10344 RVA: 0x00079BF9 File Offset: 0x00077DF9
		private void WriteLine(TraceEventType eventType, string message)
		{
			this.SetConsoleColor(eventType);
			this.WriteLine(message);
			this.ResetConsoleColor();
		}

		// Token: 0x06002869 RID: 10345 RVA: 0x00079C10 File Offset: 0x00077E10
		private void Trace(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
		{
			string text = string.Format("[{0:HH:mm:ss.fff}] [{1}] {2}", eventCache.DateTime.ToUniversalTime(), id, message);
			this.WriteLine(eventType, text);
		}

		// Token: 0x0600286A RID: 10346 RVA: 0x00079C4C File Offset: 0x00077E4C
		private void Trace(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in data)
			{
				stringBuilder.AppendFormat("{0}, ", obj);
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Remove(stringBuilder.Length - 2, 2);
			}
			this.Trace(eventCache, source, eventType, id, stringBuilder.ToString());
		}

		// Token: 0x0600286B RID: 10347 RVA: 0x00079CAC File Offset: 0x00077EAC
		private void Trace(TraceEventCache eventCache, string source, TraceEventType eventType, int id, Dictionary<string, object> dictionary)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, object> keyValuePair in dictionary)
			{
				stringBuilder.AppendFormat("{0}: {1} | ", keyValuePair.Key, keyValuePair.Value);
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Remove(stringBuilder.Length - 3, 3);
			}
			this.Trace(eventCache, source, eventType, id, stringBuilder.ToString());
		}

		// Token: 0x0600286C RID: 10348 RVA: 0x00079D40 File Offset: 0x00077F40
		private void Trace(TraceEventCache eventCache, string source, TraceEventType eventType, object data, RDEvent[] attributes)
		{
			string text = string.Empty;
			if (data is ExceptionEventBase)
			{
				text = "Exception";
			}
			else
			{
				text = data.GetType().Name;
				int num = text.LastIndexOf("Event");
				if (num > 0)
				{
					text = text.Remove(num, "Event".Length);
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (PropertyInfo propertyInfo in data.GetType().GetProperties())
			{
				RDEventProperty[] array = (RDEventProperty[])propertyInfo.GetCustomAttributes(typeof(RDEventProperty), true);
				if (array.Any<RDEventProperty>() && !array[0].ExcludeFromConsole)
				{
					object value = propertyInfo.GetValue(data, null);
					if (this.ShouldTrace(value))
					{
						stringBuilder.AppendFormat("{0}: {1} | ", propertyInfo.Name, value);
					}
				}
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Remove(stringBuilder.Length - 3, 3);
			}
			DateTime dateTime;
			if (data is RDEventBase)
			{
				dateTime = ((RDEventBase)data).InitiationTime;
			}
			else
			{
				dateTime = eventCache.DateTime.ToUniversalTime();
			}
			string text2 = string.Format("[{0:HH:mm:ss.fff}] [{1}] {2}", dateTime, text, stringBuilder.ToString());
			this.WriteLine(eventType, text2);
		}

		// Token: 0x0600286D RID: 10349 RVA: 0x00079E7C File Offset: 0x0007807C
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
		{
			if (base.Filter == null || base.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data, null))
			{
				if (data == null)
				{
					this.Trace(eventCache, source, eventType, id, string.Empty);
					return;
				}
				Dictionary<string, object> dictionary = data as Dictionary<string, object>;
				if (dictionary != null)
				{
					this.Trace(eventCache, source, eventType, id, dictionary);
					return;
				}
				RDEvent[] array = (RDEvent[])data.GetType().GetCustomAttributes(typeof(RDEvent), true);
				if (array.Any<RDEvent>())
				{
					this.Trace(eventCache, source, eventType, data, array);
					return;
				}
				this.TraceEvent(eventCache, source, eventType, id, data.ToString());
			}
		}

		// Token: 0x0600286E RID: 10350 RVA: 0x00079F1C File Offset: 0x0007811C
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
		{
			if (base.Filter == null || base.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, null, data))
			{
				if (data == null)
				{
					data = new object[0];
				}
				this.Trace(eventCache, source, eventType, id, data);
			}
		}

		// Token: 0x0600286F RID: 10351 RVA: 0x00079F61 File Offset: 0x00078161
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id)
		{
			this.TraceEvent(eventCache, source, eventType, id, string.Empty);
		}

		// Token: 0x06002870 RID: 10352 RVA: 0x00079F74 File Offset: 0x00078174
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
		{
			if (base.Filter == null || base.Filter.ShouldTrace(eventCache, source, eventType, id, format, args, null, null))
			{
				string text = string.Empty;
				if (format != null && args != null)
				{
					try
					{
						text = string.Format(format, args);
						goto IL_0044;
					}
					catch (FormatException)
					{
						goto IL_0044;
					}
				}
				if (format != null)
				{
					text = format;
				}
				IL_0044:
				this.Trace(eventCache, source, eventType, id, text);
			}
		}

		// Token: 0x06002871 RID: 10353 RVA: 0x00079FE4 File Offset: 0x000781E4
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
		{
			if (base.Filter == null || base.Filter.ShouldTrace(eventCache, source, eventType, id, message, null, null, null))
			{
				this.Trace(eventCache, source, eventType, id, message);
			}
		}

		// Token: 0x040017AF RID: 6063
		private const string FormatString = "[{0:HH:mm:ss.fff}] [{1}] {2}";
	}
}
