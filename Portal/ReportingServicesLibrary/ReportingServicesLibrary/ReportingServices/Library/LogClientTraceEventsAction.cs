using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Exceptions;
using Microsoft.ReportingServices.ProgressivePackaging;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000183 RID: 387
	internal class LogClientTraceEventsAction : ProgressivePackageActionBase
	{
		// Token: 0x06000E24 RID: 3620 RVA: 0x00033D38 File Offset: 0x00031F38
		internal LogClientTraceEventsAction(IRenderEditSession session, Stream inputStream, Stream outputStream, IList<string> responseFlags, RSService service)
			: base(outputStream, responseFlags, service)
		{
			RSTrace.CatalogTrace.Assert(session != null, "LogClientTraceEventsAction.ctor: session != null");
			RSTrace.CatalogTrace.Assert(inputStream != null, "LogClientTraceEventsAction.ctor: inputStream != null");
			this.m_session = session;
			this.m_inputStream = inputStream;
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x00033D84 File Offset: 0x00031F84
		protected override bool InitializeAction()
		{
			return base.TryGetProgressivePackageReader(this.m_inputStream, out this.m_reader);
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x00033D98 File Offset: 0x00031F98
		protected override void ExecuteAction()
		{
			ProgressiveCacheEntry progressiveCacheEntry;
			if (!base.EnsureValidSessionExists(this.m_session, out progressiveCacheEntry))
			{
				return;
			}
			RSTrace.CatalogTrace.Assert(this.m_reader != null, "ProgressivePackageReader is null");
			Stream stream = this.m_reader.ConsumeOptionalValue<Stream>("logClientTraceEventsRequest");
			if (stream == null)
			{
				RSTrace.CatalogTrace.Trace(TraceLevel.Error, "LogClientTraceEventsRequestStream is missing");
				this.WriteErrorMessage("serverErrorCode", "MissingLogClientTraceEventsRequest");
				return;
			}
			using (XmlReader xmlReader = XmlReader.Create(stream, LogClientTraceEventsAction.GetXmlReaderSettings()))
			{
				TraceRequest traceRequest = new TraceRequest();
				try
				{
					this.ValidateXmlNamespace(xmlReader);
					foreach (TraceEvent traceEvent in traceRequest.ReadTraceEvents(xmlReader))
					{
						string traceEventLog = this.GetTraceEventLog(traceEvent);
						RSEventProvider.Current.NotifyCrescentClientTraceEvent((TraceLevel)traceEvent.TraceLevel, traceEventLog);
					}
				}
				catch (XmlSchemaException ex)
				{
					throw new LogClientTraceEventsException(ErrorStrings.LogClientTraceEventsInvalidSyntax, ErrorCode.rsInvalidXml, ex);
				}
				catch (XmlException ex2)
				{
					throw new LogClientTraceEventsException(ErrorStrings.LogClientTraceEventsInvalidSyntax, ErrorCode.rsMalformedXml, ex2);
				}
				catch (FormatException ex3)
				{
					throw new LogClientTraceEventsException(ErrorStrings.LogClientTraceEventsInvalidSyntax, ErrorCode.rsMalformedXml, ex3);
				}
				catch (InvalidOperationException ex4)
				{
					throw new LogClientTraceEventsException(ErrorStrings.LogClientTraceEventsInvalidSyntax, ErrorCode.rsInvalidXml, ex4);
				}
			}
			this.WriteResponseFlag(true);
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x00033F08 File Offset: 0x00032108
		private string GetTraceEventLog(TraceEvent traceEvent)
		{
			return string.Format("{0},{1},{2},{3},{4},{5}", new object[]
			{
				LogClientTraceEventsAction.FormatTraceField(traceEvent.ProgressiveSessionId),
				LogClientTraceEventsAction.FormatTraceField(traceEvent.ClientDateTime),
				LogClientTraceEventsAction.FormatTraceField(traceEvent.EventType.ToString()),
				LogClientTraceEventsAction.FormatTraceField(traceEvent.Message),
				LogClientTraceEventsAction.FormatTraceField(traceEvent.HostApplication),
				LogClientTraceEventsAction.FormatTraceField(traceEvent.Details)
			});
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x00033F87 File Offset: 0x00032187
		private static string FormatTraceField(string field)
		{
			if (string.IsNullOrEmpty(field))
			{
				return field;
			}
			if (field.IndexOfAny(LogClientTraceEventsAction.m_specialCharacters) != -1)
			{
				field = field.Replace("\"", "\"\"");
				field = "\"" + field + "\"";
			}
			return field;
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x00033FC8 File Offset: 0x000321C8
		private static XmlReaderSettings GetXmlReaderSettings()
		{
			if (LogClientTraceEventsAction.m_readerSettings == null)
			{
				XmlReaderSettings xmlReaderSettings = ProgressiveXmlUtil.CreateXmlReaderSettings("Microsoft.ReportingServices.ProgressiveReport.LogClientTraceEventsRequest.xsd");
				Interlocked.CompareExchange<XmlReaderSettings>(ref LogClientTraceEventsAction.m_readerSettings, xmlReaderSettings, null);
			}
			return LogClientTraceEventsAction.m_readerSettings;
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x00033FFC File Offset: 0x000321FC
		private void ValidateXmlNamespace(XmlReader reader)
		{
			LogClientTraceEventsAction.LogClientTraceEventsVersion? logClientTraceEventsVersion = null;
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.Element && "ClientRequest".Equals(reader.Name, StringComparison.Ordinal))
				{
					if (reader.MoveToAttribute("xmlns"))
					{
						logClientTraceEventsVersion = new LogClientTraceEventsAction.LogClientTraceEventsVersion?(this.GetVersionFromNamespace(reader.Value));
						break;
					}
					break;
				}
			}
			if (logClientTraceEventsVersion == null)
			{
				throw new LogClientTraceEventsException(ErrorStrings.LogClientTraceEventsInvalidNamespace, ErrorCode.rsInvalidXml);
			}
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x0003406E File Offset: 0x0003226E
		private LogClientTraceEventsAction.LogClientTraceEventsVersion GetVersionFromNamespace(string requestNamespace)
		{
			if ("http://schemas.microsoft.com/sqlserver/reporting/2011/01/logclienttraceevents".Equals(requestNamespace, StringComparison.Ordinal))
			{
				return LogClientTraceEventsAction.LogClientTraceEventsVersion.InitialVersion;
			}
			throw new LogClientTraceEventsException(ErrorStrings.LogClientTraceEventsInvalidNamespace, ErrorCode.rsInvalidXml);
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x00005BF2 File Offset: 0x00003DF2
		protected override void CleanupForException()
		{
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x0003408B File Offset: 0x0003228B
		protected override void WriteErrorMessage(string errorKey, string error)
		{
			this.WriteResponseFlag(false);
			base.WriteErrorMessage(errorKey, error);
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x0003409C File Offset: 0x0003229C
		private void WriteResponseFlag(bool success)
		{
			base.MessageWriter.WriteMessage("processedTraceEvents", success);
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x000340B4 File Offset: 0x000322B4
		protected override void FinalCleanup(ErrorCode status)
		{
			if (this.m_reader != null)
			{
				this.m_reader.Dispose();
				this.m_reader = null;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x000340D0 File Offset: 0x000322D0
		protected override string OperationName
		{
			get
			{
				return "LogClientTraceEvents";
			}
		}

		// Token: 0x040005D1 RID: 1489
		private const string XsdResourceName = "Microsoft.ReportingServices.ProgressiveReport.LogClientTraceEventsRequest.xsd";

		// Token: 0x040005D2 RID: 1490
		private static XmlReaderSettings m_readerSettings;

		// Token: 0x040005D3 RID: 1491
		private const string Qualifier = "\"";

		// Token: 0x040005D4 RID: 1492
		private static readonly char[] m_specialCharacters = new char[] { '"', ',' };

		// Token: 0x040005D5 RID: 1493
		private readonly IRenderEditSession m_session;

		// Token: 0x040005D6 RID: 1494
		private readonly Stream m_inputStream;

		// Token: 0x040005D7 RID: 1495
		private ProgressivePackageReader m_reader;

		// Token: 0x02000479 RID: 1145
		private enum LogClientTraceEventsVersion
		{
			// Token: 0x04000FF0 RID: 4080
			InitialVersion
		}
	}
}
