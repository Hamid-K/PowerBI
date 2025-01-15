using System;
using System.Collections.Generic;
using Microsoft.Cloud.InstrumentationFramework;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.IfxAuditing
{
	// Token: 0x0200032D RID: 813
	public static class IfxAuditLogger
	{
		// Token: 0x1700033A RID: 826
		// (get) Token: 0x060017F1 RID: 6129 RVA: 0x0005833A File Offset: 0x0005653A
		private static ITraceSource Trace
		{
			get
			{
				return TraceSourceBase<IfxAuditingTrace>.Tracer;
			}
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x00058341 File Offset: 0x00056541
		static IfxAuditLogger()
		{
			IfxAuditLogger.InitIfx();
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x00058348 File Offset: 0x00056548
		public static void LogApplicationAudit(string operationName, OperationResult operationResult, AuditEventCategory auditCategory, CallerIdentity callerIdentity, IEnumerable<TargetResource> targetResources)
		{
			IfxAuditLogger.LogApplicationAudit(operationName, operationResult, new List<AuditEventCategory> { auditCategory }, new List<CallerIdentity> { callerIdentity }, targetResources);
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x0005836C File Offset: 0x0005656C
		public static void LogApplicationAudit(string operationName, OperationResult operationResult, IEnumerable<AuditEventCategory> auditCategories, IEnumerable<CallerIdentity> callerIdentities, IEnumerable<TargetResource> targetResources)
		{
			try
			{
				IfxAuditLogger.Trace.TraceInformation("LogApplicationAudit. OperationName: " + operationName);
				AuditMandatoryProperties auditMandatoryProperties = new AuditMandatoryProperties();
				auditMandatoryProperties.OperationName = operationName;
				auditMandatoryProperties.ResultType = IfxAuditLogger.GetIfxOperationResult(operationResult);
				foreach (AuditEventCategory auditEventCategory in auditCategories)
				{
					auditMandatoryProperties.AddAuditCategory(IfxAuditLogger.GetIfxAuditEventCategory(auditEventCategory));
				}
				foreach (CallerIdentity callerIdentity in callerIdentities)
				{
					auditMandatoryProperties.AddCallerIdentity(IfxAuditLogger.GetIfxCallerIdentity(callerIdentity));
				}
				foreach (TargetResource targetResource in targetResources)
				{
					auditMandatoryProperties.AddTargetResource(targetResource.TargetResourceType, targetResource.TargetResourceName);
				}
				bool flag = IfxAudit.LogApplicationAudit(auditMandatoryProperties, null);
				IfxAuditLogger.Trace.TraceInformation(string.Format("LogApplicationAudit. Result is {0}, OperationName: {1}", flag, operationName));
			}
			catch (Exception ex)
			{
				IfxAuditLogger.Trace.TraceError("LogApplicationAudit: Failed to emit ifx audit record. Exception: " + ex.Message);
				if (ExceptionUtility.IsFatal(ex))
				{
					throw ex;
				}
			}
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x000584D4 File Offset: 0x000566D4
		private static void InitIfx()
		{
			try
			{
				IfxAuditLogger.Trace.TraceInformation("InitIfx: Start");
				IfxInitializer.IfxInitialize("pbi_ifx_session", null, new AuditSpecification
				{
					heartbeatSpan = new TimeSpan?(new TimeSpan(0, 30, 0)),
					LogFormat = 1
				});
			}
			catch (Exception ex)
			{
				IfxAuditLogger.Trace.TraceError("InitIfx: Failed to init ifx. Exception: " + ex.Message);
				if (ExceptionUtility.IsFatal(ex))
				{
					throw ex;
				}
			}
			IfxAuditLogger.Trace.TraceInformation("InitIfx: End");
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x00058568 File Offset: 0x00056768
		private static CallerIdentity GetIfxCallerIdentity(CallerIdentity callerIdentety)
		{
			return new CallerIdentity(IfxAuditLogger.GetCallerIdentityType(callerIdentety.CallerIdentityType), callerIdentety.CallerIdentityValue);
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x00058580 File Offset: 0x00056780
		private static CallerIdentityType GetCallerIdentityType(CallerIdentityType callerIdentityType)
		{
			return callerIdentityType;
		}

		// Token: 0x060017F8 RID: 6136 RVA: 0x00058580 File Offset: 0x00056780
		private static AuditEventCategory GetIfxAuditEventCategory(AuditEventCategory auditCategory)
		{
			return auditCategory;
		}

		// Token: 0x060017F9 RID: 6137 RVA: 0x00058580 File Offset: 0x00056780
		private static OperationResult GetIfxOperationResult(OperationResult operationResult)
		{
			return operationResult;
		}

		// Token: 0x04000854 RID: 2132
		private const string c_ifxSession = "pbi_ifx_session";
	}
}
