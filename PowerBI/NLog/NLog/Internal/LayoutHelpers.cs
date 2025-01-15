using System;
using System.Globalization;
using NLog.Common;
using NLog.Layouts;

namespace NLog.Internal
{
	// Token: 0x02000126 RID: 294
	internal static class LayoutHelpers
	{
		// Token: 0x06000EE5 RID: 3813 RVA: 0x00024C44 File Offset: 0x00022E44
		public static short RenderShort(this Layout layout, LogEventInfo logEvent, short defaultValue, string layoutName)
		{
			if (layout == null)
			{
				InternalLogger.Debug(layoutName + " is null so default value of " + defaultValue);
				return defaultValue;
			}
			if (logEvent == null)
			{
				InternalLogger.Debug(layoutName + ": logEvent is null so default value of " + defaultValue);
				return defaultValue;
			}
			string text = layout.Render(logEvent);
			short num;
			if (!short.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
			{
				InternalLogger.Warn(string.Concat(new object[] { layoutName, ": parse of value '", text, "' failed, return ", defaultValue }));
				return defaultValue;
			}
			return num;
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x00024CD4 File Offset: 0x00022ED4
		public static int RenderInt(this Layout layout, LogEventInfo logEvent, int defaultValue, string layoutName)
		{
			if (layout == null)
			{
				InternalLogger.Debug(layoutName + " is null so default value of " + defaultValue);
				return defaultValue;
			}
			if (logEvent == null)
			{
				InternalLogger.Debug(layoutName + ": logEvent is null so default value of " + defaultValue);
				return defaultValue;
			}
			string text = layout.Render(logEvent);
			int num;
			if (!int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
			{
				InternalLogger.Warn(string.Concat(new object[] { layoutName, ": parse of value '", text, "' failed, return ", defaultValue }));
				return defaultValue;
			}
			return num;
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x00024D64 File Offset: 0x00022F64
		public static bool RenderBool(this Layout layout, LogEventInfo logEvent, bool defaultValue, string layoutName)
		{
			if (layout == null)
			{
				InternalLogger.Debug(layoutName + " is null so default value of " + defaultValue.ToString());
				return defaultValue;
			}
			if (logEvent == null)
			{
				InternalLogger.Debug(layoutName + ": logEvent is null so default value of " + defaultValue.ToString());
				return defaultValue;
			}
			string text = layout.Render(logEvent);
			bool flag;
			if (!bool.TryParse(text, out flag))
			{
				InternalLogger.Warn(string.Concat(new string[]
				{
					layoutName,
					": parse of value '",
					text,
					"' failed, return ",
					defaultValue.ToString()
				}));
				return defaultValue;
			}
			return flag;
		}
	}
}
