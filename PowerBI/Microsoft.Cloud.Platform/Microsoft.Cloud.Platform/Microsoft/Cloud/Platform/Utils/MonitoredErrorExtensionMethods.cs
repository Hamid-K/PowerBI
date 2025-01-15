using System;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000252 RID: 594
	public static class MonitoredErrorExtensionMethods
	{
		// Token: 0x06000F5A RID: 3930 RVA: 0x000346E0 File Offset: 0x000328E0
		public static string ToStringLimitedLength<T>(this T error) where T : class, IMonitoredError
		{
			return error.ToStringLimitedLength(true);
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x000346E9 File Offset: 0x000328E9
		public static string ToStringLimitedLength<T>(this T error, bool needsScrubbing) where T : class, IMonitoredError
		{
			return ExtendedText.ChopTextByLength(needsScrubbing ? error.ToPrivateString().ObfuscatePrivateValue(false) : error.ToString(), 16384);
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x00034718 File Offset: 0x00032918
		public static string InnerMessage<T>([NotNull] this T error) where T : class, IMonitoredError
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<T>(error, "error");
			IMonitoredError monitoredError = error;
			Exception causeException;
			for (;;)
			{
				causeException = MonitoredErrorExtensionMethods.GetCauseException(monitoredError);
				if (causeException == null)
				{
					break;
				}
				monitoredError = causeException as IMonitoredError;
				if (monitoredError == null)
				{
					goto Block_2;
				}
			}
			return MonitoredErrorExtensionMethods.GetCauseMessage(monitoredError);
			Block_2:
			return causeException.Message ?? string.Empty;
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x00034761 File Offset: 0x00032961
		public static string InnerMessageLimitedLength<T>(this T error) where T : class, IMonitoredError
		{
			return ExtendedText.ChopTextByLength(error.InnerMessage<T>(), 512);
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x00034774 File Offset: 0x00032974
		private static string GetCauseMessage(IMonitoredError error)
		{
			IMonitoredErrorCause monitoredErrorCause = error as IMonitoredErrorCause;
			if (monitoredErrorCause != null && !string.IsNullOrEmpty(monitoredErrorCause.Message))
			{
				return monitoredErrorCause.Message;
			}
			Exception ex = error as Exception;
			if (ex != null && !string.IsNullOrEmpty(ex.Message))
			{
				return ex.Message;
			}
			return string.Empty;
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x000347C4 File Offset: 0x000329C4
		[CanBeNull]
		private static Exception GetCauseException(IMonitoredError error)
		{
			IMonitoredErrorCause monitoredErrorCause = error as IMonitoredErrorCause;
			if (monitoredErrorCause != null && monitoredErrorCause.CauseException != null)
			{
				return monitoredErrorCause.CauseException;
			}
			Exception ex = error as Exception;
			if (ex != null && ex.InnerException != null)
			{
				return ex.InnerException;
			}
			return null;
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x00034804 File Offset: 0x00032A04
		public static string MonitoringScopeNameOrEmptyString<T>(this T error) where T : class, IMonitoredError
		{
			MonitoringScopeId monitoringScope = error.MonitoringScope;
			if (monitoringScope != null)
			{
				return monitoringScope.ToString();
			}
			return string.Empty;
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x0003482C File Offset: 0x00032A2C
		public static bool SetMonitoringScopeIfNullOrEmpty<T>(this T error, MonitoringScopeId newMonitoringScope) where T : class, IMonitoredError
		{
			if (error.MonitoringScope == null || string.IsNullOrEmpty(error.MonitoringScope.ToString()))
			{
				error.MonitoringScope = newMonitoringScope;
				return true;
			}
			return false;
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x00034864 File Offset: 0x00032A64
		public static bool TrySetFiringEventErrorParametersIfNullOrEmpty<T>(this T error, string eventName, long eventId, string eventsKitName, long eventsKitId) where T : IMonitoredError
		{
			if (error.ErrorEventId == 0L && eventId != 0L)
			{
				error.ErrorEventId = eventId;
				error.ErrorEventsKitId = eventsKitId;
				error.ErrorEventName = eventName;
				error.ErrorEventsKitName = eventsKitName;
				return true;
			}
			return false;
		}

		// Token: 0x040005D3 RID: 1491
		private const int c_toStringLengthLimit = 16384;

		// Token: 0x040005D4 RID: 1492
		private const int c_innerMessageLengthLimit = 512;
	}
}
