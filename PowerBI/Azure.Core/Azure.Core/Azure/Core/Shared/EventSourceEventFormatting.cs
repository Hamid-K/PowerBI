using System;
using System.Collections.ObjectModel;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Azure.Core.Shared
{
	// Token: 0x02000083 RID: 131
	internal static class EventSourceEventFormatting
	{
		// Token: 0x06000432 RID: 1074 RVA: 0x0000C920 File Offset: 0x0000AB20
		[NullableContext(1)]
		public static string Format(EventWrittenEventArgs eventData)
		{
			ReadOnlyCollection<object> payload = eventData.Payload;
			object[] array = ((payload != null) ? payload.ToArray<object>() : null) ?? Array.Empty<object>();
			EventSourceEventFormatting.ProcessPayloadArray(array);
			if (eventData.Message != null)
			{
				try
				{
					return string.Format(CultureInfo.InvariantCulture, eventData.Message, array);
				}
				catch (FormatException)
				{
				}
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(eventData.EventName);
			if (!string.IsNullOrWhiteSpace(eventData.Message))
			{
				stringBuilder.AppendLine();
				stringBuilder.Append("Message").Append(" = ").Append(eventData.Message);
			}
			if (eventData.PayloadNames != null)
			{
				for (int i = 0; i < eventData.PayloadNames.Count; i++)
				{
					stringBuilder.AppendLine();
					stringBuilder.Append(eventData.PayloadNames[i]).Append(" = ").Append(array[i]);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000CA1C File Offset: 0x0000AC1C
		private static void ProcessPayloadArray([Nullable(new byte[] { 1, 2 })] object[] payloadArray)
		{
			for (int i = 0; i < payloadArray.Length; i++)
			{
				payloadArray[i] = EventSourceEventFormatting.FormatValue(payloadArray[i]);
			}
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0000CA44 File Offset: 0x0000AC44
		[NullableContext(2)]
		private static object FormatValue(object o)
		{
			byte[] array = o as byte[];
			if (array != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (byte b in array)
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0:X2}", b);
				}
				return stringBuilder.ToString();
			}
			return o;
		}
	}
}
