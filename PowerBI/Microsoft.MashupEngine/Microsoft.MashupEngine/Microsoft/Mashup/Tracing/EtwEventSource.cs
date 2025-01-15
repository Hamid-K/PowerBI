using System;
using Microsoft.Diagnostics.Tracing;

namespace Microsoft.Mashup.Tracing
{
	// Token: 0x020020B1 RID: 8369
	[EventSource(Name = "Microsoft.Data.Mashup.ETW.Provider", Guid = "F86E2480-889F-437D-B1FF-26864117B386")]
	public class EtwEventSource : EventSource
	{
		// Token: 0x0600CCD6 RID: 52438 RVA: 0x0028B93C File Offset: 0x00289B3C
		[Event(1, Level = EventLevel.Critical, Version = 1, Message = "Critical: {5}")]
		public void Critical(string productVersion, string processName, int processId, Guid activityId, string action, string message, string correlationId)
		{
			base.WriteEvent(1, new object[] { productVersion, processName, processId, activityId, action, message, correlationId });
		}

		// Token: 0x0600CCD7 RID: 52439 RVA: 0x0028B975 File Offset: 0x00289B75
		[Event(2, Level = EventLevel.Error, Version = 1, Message = "Error: {5}")]
		public void Error(string productVersion, string processName, int processId, Guid activityId, string action, string message, string correlationId)
		{
			base.WriteEvent(2, new object[] { productVersion, processName, processId, activityId, action, message, correlationId });
		}

		// Token: 0x0600CCD8 RID: 52440 RVA: 0x0028B9AE File Offset: 0x00289BAE
		[Event(3, Level = EventLevel.Warning, Version = 1, Message = "Warning: {5}")]
		public void Warning(string productVersion, string processName, int processId, Guid activityId, string action, string message, string correlationId)
		{
			base.WriteEvent(3, new object[] { productVersion, processName, processId, activityId, action, message, correlationId });
		}

		// Token: 0x0600CCD9 RID: 52441 RVA: 0x0028B9E7 File Offset: 0x00289BE7
		[Event(4, Level = EventLevel.Informational, Version = 1, Opcode = EventOpcode.Info, Message = "Information: {5}")]
		public void Informational(string productVersion, string processName, int processId, Guid activityId, string action, string message, string correlationId)
		{
			base.WriteEvent(4, new object[] { productVersion, processName, processId, activityId, action, message, correlationId });
		}

		// Token: 0x0600CCDA RID: 52442 RVA: 0x0028BA20 File Offset: 0x00289C20
		[Event(5, Level = EventLevel.Verbose, Version = 1, Opcode = EventOpcode.Info, Message = "Verbose: {5}")]
		public void Verbose(string productVersion, string processName, int processId, Guid activityId, string action, string message, string correlationId)
		{
			base.WriteEvent(5, new object[] { productVersion, processName, processId, activityId, action, message, correlationId });
		}

		// Token: 0x0600CCDB RID: 52443 RVA: 0x0028BA59 File Offset: 0x00289C59
		[Event(6, Level = EventLevel.Critical, Version = 1, Message = "UserTrace/Critical: {5}")]
		public void UserTraceCritical(string productVersion, string processName, int processId, Guid activityId, string action, string message, string correlationId)
		{
			base.WriteEvent(6, new object[] { productVersion, processName, processId, activityId, action, message, correlationId });
		}

		// Token: 0x0600CCDC RID: 52444 RVA: 0x0028BA92 File Offset: 0x00289C92
		[Event(7, Level = EventLevel.Error, Version = 1, Message = "UserTrace/Error: {5}")]
		public void UserTraceError(string productVersion, string processName, int processId, Guid activityId, string action, string message, string correlationId)
		{
			base.WriteEvent(7, new object[] { productVersion, processName, processId, activityId, action, message, correlationId });
		}

		// Token: 0x0600CCDD RID: 52445 RVA: 0x0028BACB File Offset: 0x00289CCB
		[Event(8, Level = EventLevel.Warning, Version = 1, Message = "UserTrace/Warning: {5}")]
		public void UserTraceWarning(string productVersion, string processName, int processId, Guid activityId, string action, string message, string correlationId)
		{
			base.WriteEvent(8, new object[] { productVersion, processName, processId, activityId, action, message, correlationId });
		}

		// Token: 0x0600CCDE RID: 52446 RVA: 0x0028BB04 File Offset: 0x00289D04
		[Event(9, Level = EventLevel.Informational, Version = 1, Message = "UserTrace/Information: {5}")]
		public void UserTraceInformational(string productVersion, string processName, int processId, Guid activityId, string action, string message, string correlationId)
		{
			base.WriteEvent(9, new object[] { productVersion, processName, processId, activityId, action, message, correlationId });
		}

		// Token: 0x0600CCDF RID: 52447 RVA: 0x0028BB3E File Offset: 0x00289D3E
		[Event(10, Level = EventLevel.Verbose, Version = 1, Message = "UserTrace/Verbose: {5}")]
		public void UserTraceVerbose(string productVersion, string processName, int processId, Guid activityId, string action, string message, string correlationId)
		{
			base.WriteEvent(10, new object[] { productVersion, processName, processId, activityId, action, message, correlationId });
		}

		// Token: 0x0600CCE0 RID: 52448 RVA: 0x0028BB78 File Offset: 0x00289D78
		[Event(11, Level = EventLevel.Critical, Version = 1, Message = "Critical: {5}")]
		public void CriticalWithoutPii(string productVersion, string processName, int processId, Guid activityId, string action, string message, string correlationId)
		{
			base.WriteEvent(11, new object[] { productVersion, processName, processId, activityId, action, message, correlationId });
		}

		// Token: 0x0600CCE1 RID: 52449 RVA: 0x0028BBB2 File Offset: 0x00289DB2
		[Event(12, Level = EventLevel.Error, Version = 1, Message = "Error: {5}")]
		public void ErrorWithoutPii(string productVersion, string processName, int processId, Guid activityId, string action, string message, string correlationId)
		{
			base.WriteEvent(12, new object[] { productVersion, processName, processId, activityId, action, message, correlationId });
		}

		// Token: 0x0600CCE2 RID: 52450 RVA: 0x0028BBEC File Offset: 0x00289DEC
		[Event(13, Level = EventLevel.Warning, Version = 1, Message = "Warning: {5}")]
		public void WarningWithoutPii(string productVersion, string processName, int processId, Guid activityId, string action, string message, string correlationId)
		{
			base.WriteEvent(13, new object[] { productVersion, processName, processId, activityId, action, message, correlationId });
		}

		// Token: 0x0600CCE3 RID: 52451 RVA: 0x0028BC26 File Offset: 0x00289E26
		[Event(14, Level = EventLevel.Informational, Version = 1, Opcode = EventOpcode.Info, Message = "Information: {5}")]
		public void InformationalWithoutPii(string productVersion, string processName, int processId, Guid activityId, string action, string message, string correlationId)
		{
			base.WriteEvent(14, new object[] { productVersion, processName, processId, activityId, action, message, correlationId });
		}

		// Token: 0x0600CCE4 RID: 52452 RVA: 0x0028BC60 File Offset: 0x00289E60
		[Event(15, Level = EventLevel.Verbose, Version = 1, Opcode = EventOpcode.Info, Message = "Verbose: {5}")]
		public void VerboseWithoutPii(string productVersion, string processName, int processId, Guid activityId, string action, string message, string correlationId)
		{
			base.WriteEvent(15, new object[] { productVersion, processName, processId, activityId, action, message, correlationId });
		}

		// Token: 0x040067B4 RID: 26548
		public static readonly EtwEventSource Log = new EtwEventSource();
	}
}
