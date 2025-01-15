using System;
using System.Diagnostics;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Security
{
	// Token: 0x02000063 RID: 99
	[EventsKit(7852094758486841705L)]
	[PublishedEvent]
	[Trace(typeof(SecretManagerTracer))]
	public interface ISecretManagerEventsKit
	{
		// Token: 0x060002B0 RID: 688
		[Event(9042913250091296430L, 1, Level = EventLevel.Error, Format = "Updated certificate with CN={0} and thumbprint = {1} has start date {2}, when the existing version has further date {3}.", Version = 1)]
		[WindowsEventLog(EventLogEntryType.Error, 5107)]
		void NotifyUpdatedCertificateStartDateIsLessThanCurrent(string certificateName, string certificateThumbprint, DateTime newStartTime, DateTime oldStartTime);

		// Token: 0x060002B1 RID: 689
		[Event(2589511315134539502L, 2, Level = EventLevel.Error, Format = "Certificate key {0} was requested, but it is not supported. Details: {1}", Version = 1)]
		[WindowsEventLog(EventLogEntryType.Error, 5106)]
		void NotifyCertificateKeyNotSupported(string key, MonitoredException error);

		// Token: 0x060002B2 RID: 690
		[Event(2904689920128866886L, 3, Level = EventLevel.Informational, Format = "Certificate {0} does not exist in certificate store.", Version = 1)]
		[WindowsEventLog(EventLogEntryType.Error, 5105)]
		void NotifyCertificateDoesNotExist(string certificateName);

		// Token: 0x060002B3 RID: 691
		[Event(1158210893037073100L, 4, Level = EventLevel.Error, Format = "Certificate with CN={0} and thumbprint = {1}  was updated to a new expiration date: {2}, but the new certificate cannot be used until {3}.", Version = 1)]
		[WindowsEventLog(EventLogEntryType.Error, 5104)]
		void NotifyUpdatedCertificateCannotBeUsedBeforeFutureTime(string certificateName, string certificateThumbprint, DateTime newExpiration, DateTime newStartTime);

		// Token: 0x060002B4 RID: 692
		[Event(7019148286620659283L, 5, Level = EventLevel.Warning, Format = "Certificate with CN={0} and thumbprint = {1}  is about to expire. Will expire at {2}.", Version = 1)]
		[WindowsEventLog(EventLogEntryType.Error, 5103)]
		void NotifyCertificateAboutToExpire(string certificateName, string certificateThumbprint, DateTime expiration);

		// Token: 0x060002B5 RID: 693
		[Event(7019143286610649283L, 6, Level = EventLevel.Informational, Format = "Certificates with CN={0} were updated. Old thumbprints={1}. New thumbprints{2}.", Version = 1)]
		[Audit]
		void NotifyCertificateChanged(string certificateName, string oldCertificates, string newCertificates);
	}
}
