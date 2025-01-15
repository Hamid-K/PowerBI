using System;
using System.IO;
using System.Xml;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.RdlObjectModel2010.Upgrade
{
	// Token: 0x0200006F RID: 111
	internal class Upgrader
	{
		// Token: 0x0600040C RID: 1036 RVA: 0x00016AD0 File Offset: 0x00014CD0
		internal static void Upgrade(string inputFile, string outputFile, bool throwUpgradeException)
		{
			using (FileStream fileStream = File.OpenRead(inputFile))
			{
				using (FileStream fileStream2 = File.Open(outputFile, FileMode.Create, FileAccess.ReadWrite))
				{
					Upgrader.Upgrade(fileStream, fileStream2, throwUpgradeException);
				}
			}
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00016B28 File Offset: 0x00014D28
		internal static void Upgrade(Stream inStream, Stream outStream, bool throwUpgradeException)
		{
			Upgrader.Upgrade(new XmlTextReader(inStream)
			{
				DtdProcessing = DtdProcessing.Prohibit
			}, outStream, throwUpgradeException);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00016B3E File Offset: 0x00014D3E
		internal static void Upgrade(XmlReader xmlReader, Stream outStream, bool throwUpgradeException)
		{
			new UpgradeImpl2010().Upgrade(xmlReader, outStream);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00016B4C File Offset: 0x00014D4C
		internal static Stream UpgradeToCurrent(Stream inStream, bool throwUpgradeException)
		{
			return RDLUpgrader.UpgradeToCurrent(inStream, throwUpgradeException, true);
		}
	}
}
