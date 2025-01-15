using System;
using System.Diagnostics;
using System.IO;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportPublishing
{
	// Token: 0x02000390 RID: 912
	internal sealed class RdlObjectModelUpgradeStrategy : ReportUpgradeStrategy
	{
		// Token: 0x0600252C RID: 9516 RVA: 0x000B1B8B File Offset: 0x000AFD8B
		internal RdlObjectModelUpgradeStrategy(bool renameInvalidDataSources, bool throwUpgradeException)
		{
			this.m_renameInvalidDataSources = renameInvalidDataSources;
			this.m_throwUpgradeException = throwUpgradeException;
		}

		// Token: 0x0600252D RID: 9517 RVA: 0x000B1BA4 File Offset: 0x000AFDA4
		internal override Stream Upgrade(Stream definitionStream)
		{
			Stream stream = RDLUpgrader.UpgradeToCurrent(definitionStream, this.m_throwUpgradeException, this.m_renameInvalidDataSources);
			if (definitionStream != stream)
			{
				definitionStream.Close();
				definitionStream = null;
				if (Global.Tracer.TraceVerbose)
				{
					try
					{
						StreamReader streamReader = new StreamReader(stream);
						Global.Tracer.Trace(TraceLevel.Verbose, "Upgraded Report Definition\r\n");
						Global.Tracer.Trace(TraceLevel.Verbose, streamReader.ReadToEnd());
						Global.Tracer.Trace(TraceLevel.Verbose, "\r\n");
						stream.Seek(0L, SeekOrigin.Begin);
					}
					catch (Exception ex)
					{
						if (AsynchronousExceptionDetection.IsStoppingException(ex))
						{
							throw;
						}
					}
				}
			}
			return stream;
		}

		// Token: 0x040015C4 RID: 5572
		private readonly bool m_renameInvalidDataSources;

		// Token: 0x040015C5 RID: 5573
		private readonly bool m_throwUpgradeException;
	}
}
