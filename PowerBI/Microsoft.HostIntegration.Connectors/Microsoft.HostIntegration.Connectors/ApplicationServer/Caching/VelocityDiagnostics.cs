using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Microsoft.Fabric.Common;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001B7 RID: 439
	internal class VelocityDiagnostics
	{
		// Token: 0x06000E51 RID: 3665 RVA: 0x000304F8 File Offset: 0x0002E6F8
		public static void InitializeDiagnostics(string velocityDiagnosticMode, int maxBufferedEvents)
		{
			lock (VelocityDiagnostics._lockObject)
			{
				if (!VelocityDiagnostics._isInitialized)
				{
					if (!string.IsNullOrEmpty(velocityDiagnosticMode))
					{
						if (velocityDiagnosticMode.Equals("InfoWithAllReqLite", StringComparison.OrdinalIgnoreCase))
						{
							DiagConfigManager.DiagMode = VelocityDiagMode.InfoWithAllReqLite;
						}
						else if (velocityDiagnosticMode.Equals("WarningWithFailedReq", StringComparison.OrdinalIgnoreCase))
						{
							DiagConfigManager.DiagMode = VelocityDiagMode.WarningWithFailedReq;
						}
						else if (velocityDiagnosticMode.Equals("NoBuffering", StringComparison.OrdinalIgnoreCase))
						{
							DiagConfigManager.DiagMode = VelocityDiagMode.NoBuffering;
						}
						else if (velocityDiagnosticMode.Equals("InfoWithAllReq", StringComparison.OrdinalIgnoreCase))
						{
							DiagConfigManager.DiagMode = VelocityDiagMode.InfoWithAllReq;
						}
						else if (velocityDiagnosticMode.Equals("WarningWithFailedReqExt", StringComparison.OrdinalIgnoreCase))
						{
							DiagConfigManager.DiagMode = VelocityDiagMode.WarningWithFailedReqExt;
						}
					}
					DiagEventManager.MaxBufferedOperations = maxBufferedEvents;
					VelocityDiagnostics._verifier = new VelocityVerifier();
					DiagEventManager.Init(new ProcessOpStateCallback(VelocityDiagnostics.ValidateAndLog));
					VelocityDiagnostics._isInitialized = true;
				}
			}
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x000305D8 File Offset: 0x0002E7D8
		public static bool ValidateAndLog(DiagOperationState opState, bool isExceededLogQuota)
		{
			bool flag = false;
			if (DiagConfigManager.DiagMode == VelocityDiagMode.InfoWithAllReqLite || DiagConfigManager.DiagMode == VelocityDiagMode.InfoWithAllReq)
			{
				EventLogWriter.WriteInfo(VelocityDiagnostics._logSourceDiagAll, "{0}", new object[] { opState.ToString() });
				return false;
			}
			List<DiagRuleViolation> list = VelocityDiagnostics._verifier.Validate(opState);
			if (list != null && list.Count > 0)
			{
				if (!isExceededLogQuota)
				{
					EventLogWriter.WriteWarning(VelocityDiagnostics._logSourceDiagError, "{0}", new object[] { opState.ToString() + "-- > Violations --> " + VelocityDiagnostics.GetViolationsString(list) });
				}
				flag = true;
			}
			opState = null;
			return flag;
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x0003066A File Offset: 0x0002E86A
		internal static void DrainLogsOnUnhandledException(object sender, UnhandledExceptionEventArgs args)
		{
			DiagEventManager.DrainRequestsOnException(new ProcessOpStateCallback(VelocityDiagnostics.LogOnCrash));
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x00030680 File Offset: 0x0002E880
		public static bool LogOnCrash(DiagOperationState opState, bool isExceededLogQuota)
		{
			EventLogWriter.WriteInfo(VelocityDiagnostics._logSourceDiagAll, "{0}", new object[] { opState.ToString() });
			return true;
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x000306AE File Offset: 0x0002E8AE
		internal static bool IsBasicOnlyLogBufferingEnabled()
		{
			return DiagEventManager.IsEventManagementEnabled && (DiagConfigManager.DiagMode == VelocityDiagMode.InfoWithAllReqLite || DiagConfigManager.DiagMode == VelocityDiagMode.WarningWithFailedReq);
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x000306CB File Offset: 0x0002E8CB
		internal static bool IsLogBufferingEnabled()
		{
			return DiagEventManager.IsEventManagementEnabled && (DiagConfigManager.DiagMode == VelocityDiagMode.WarningWithFailedReq || DiagConfigManager.DiagMode == VelocityDiagMode.InfoWithAllReqLite || DiagConfigManager.DiagMode == VelocityDiagMode.InfoWithAllReq || DiagConfigManager.DiagMode == VelocityDiagMode.WarningWithFailedReqExt);
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x000306F7 File Offset: 0x0002E8F7
		internal static bool IsExtendedLogBufferingEnabled()
		{
			return DiagEventManager.IsEventManagementEnabled && (DiagConfigManager.DiagMode == VelocityDiagMode.InfoWithAllReq || DiagConfigManager.DiagMode == VelocityDiagMode.WarningWithFailedReqExt);
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x00030714 File Offset: 0x0002E914
		internal static bool FabricLoggingEnabled(TraceLevel tlevel)
		{
			return DiagConfigManager.DiagMode == VelocityDiagMode.NoBuffering && Provider.IsEnabled(tlevel);
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x00030728 File Offset: 0x0002E928
		internal static string GetViolationsString(List<DiagRuleViolation> violations)
		{
			string text = "";
			foreach (DiagRuleViolation diagRuleViolation in violations)
			{
				text = text + VelocityDiagnostics.violationDelimiter + diagRuleViolation.ViolationId;
			}
			return text;
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x00030790 File Offset: 0x0002E990
		internal static void Publish(DiagOperationState opState)
		{
			if (opState != null && VelocityDiagnostics.IsLogBufferingEnabled())
			{
				DiagEventManager.AddRequestStates(opState);
			}
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x000307A2 File Offset: 0x0002E9A2
		internal static void SetDiagnosticMode(VelocityDiagMode diagMode)
		{
			DiagConfigManager.DiagMode = diagMode;
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x000307AA File Offset: 0x0002E9AA
		internal static void Publish(DiagEventName curState, string eventKey, TraceLevel level, string logSource, string format, params object[] args)
		{
			VelocityDiagnostics.Publish(curState, true, eventKey, level, logSource, format, args);
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x000307BC File Offset: 0x0002E9BC
		internal static void Publish(DiagEventName curState, bool result, string eventKey, TraceLevel level, string logSource, string format, params object[] args)
		{
			VelocityDiagnostics.FabricLogging(level, logSource, format, args);
			DiagEvent diagEvent = VelocityDiagnostics.GetDiagEvent(curState, result, format, args);
			if (diagEvent != null)
			{
				DiagEventManager.AddRequestStates(eventKey, diagEvent);
			}
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x000307EB File Offset: 0x0002E9EB
		internal static void Publish(TraceLevel level, string logSource, string format, params object[] args)
		{
			VelocityDiagnostics.FabricLogging(level, logSource, format, args);
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x000307F8 File Offset: 0x0002E9F8
		internal static void Publish(DiagEventName curState, bool result, DiagOperationState opState, TraceLevel level, string logSource, string format, params object[] args)
		{
			VelocityDiagnostics.FabricLogging(level, logSource, format, args);
			if (opState != null)
			{
				DiagEvent diagEvent = VelocityDiagnostics.GetDiagEvent(curState, result, format, args);
				if (diagEvent != null)
				{
					opState.AddEvent(diagEvent);
				}
			}
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x0003082A File Offset: 0x0002EA2A
		internal static void AddOrAppendOperationState(DiagOperationState opState)
		{
			if (opState != null)
			{
				DiagEventManager.AddRequestStates(opState);
			}
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x00030838 File Offset: 0x0002EA38
		private static DiagEvent GetDiagEvent(DiagEventName curState, bool result, string format, params object[] args)
		{
			string text = "";
			if (VelocityDiagnostics.IsBasicOnlyLogBufferingEnabled())
			{
				if (!result)
				{
					text = string.Format(CultureInfo.InvariantCulture, format, args);
				}
				return new DiagEvent(curState, text, result);
			}
			if (VelocityDiagnostics.IsExtendedLogBufferingEnabled())
			{
				text = string.Format(CultureInfo.InvariantCulture, format, args);
				return new DiagEvent(curState, text, result);
			}
			return null;
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x00030889 File Offset: 0x0002EA89
		internal static void Publish(DiagEventName curState, bool result, DiagOperationState opState)
		{
			if (VelocityDiagnostics.IsLogBufferingEnabled() && opState != null)
			{
				opState.AddEvent(new DiagEvent(curState, result));
			}
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x000308A2 File Offset: 0x0002EAA2
		internal static void Complete(DiagOperationState opState)
		{
			if (VelocityDiagnostics.IsLogBufferingEnabled() && opState != null)
			{
				DiagEventManager.Complete(opState, true);
			}
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x000308B5 File Offset: 0x0002EAB5
		internal static DiagOperationState GetOrAddOperationState(string uniqueId)
		{
			if (VelocityDiagnostics.IsLogBufferingEnabled())
			{
				return DiagEventManager.GetOrAddOperationState(uniqueId);
			}
			return null;
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x000308C6 File Offset: 0x0002EAC6
		internal static DiagOperationState GetOperationState(string uniqueId)
		{
			if (VelocityDiagnostics.IsLogBufferingEnabled())
			{
				return DiagEventManager.GetOperationState(uniqueId);
			}
			return null;
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x000308D7 File Offset: 0x0002EAD7
		private static void FabricLogging(TraceLevel level, string logSource, string format, params object[] args)
		{
			if (VelocityDiagnostics.FabricLoggingEnabled(level))
			{
				if (level == TraceLevel.Error)
				{
					EventLogWriter.WriteError(logSource, format, args);
					return;
				}
				if (level == TraceLevel.Warning)
				{
					EventLogWriter.WriteWarning(logSource, format, args);
					return;
				}
				if (level == TraceLevel.Info)
				{
					EventLogWriter.WriteInfo(logSource, format, args);
					return;
				}
				if (level == TraceLevel.Verbose)
				{
					EventLogWriter.WriteVerbose(logSource, format, args);
				}
			}
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x00030914 File Offset: 0x0002EB14
		public static void Clear()
		{
			DiagEventManager.Clear();
		}

		// Token: 0x040009EA RID: 2538
		private static VelocityVerifier _verifier;

		// Token: 0x040009EB RID: 2539
		private static string violationDelimiter = "#V#";

		// Token: 0x040009EC RID: 2540
		private static string _logSourceDiagAll = "DiagnosticsAll";

		// Token: 0x040009ED RID: 2541
		private static string _logSourceDiagError = "DiagnosticsError";

		// Token: 0x040009EE RID: 2542
		private static object _lockObject = new object();

		// Token: 0x040009EF RID: 2543
		private static bool _isInitialized = false;
	}
}
