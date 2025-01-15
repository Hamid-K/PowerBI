using System;
using System.IO;
using System.Xml;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.RdlObjectModel2008.Upgrade
{
	// Token: 0x02000076 RID: 118
	internal class Upgrader
	{
		// Token: 0x06000435 RID: 1077 RVA: 0x00016F88 File Offset: 0x00015188
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

		// Token: 0x06000436 RID: 1078 RVA: 0x00016FE0 File Offset: 0x000151E0
		internal static void Upgrade(Stream inStream, Stream outStream, bool throwUpgradeException)
		{
			Upgrader.Upgrade(new XmlTextReader(inStream)
			{
				DtdProcessing = DtdProcessing.Prohibit
			}, outStream, throwUpgradeException);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00016FF6 File Offset: 0x000151F6
		internal static void Upgrade(XmlReader xmlReader, Stream outStream, bool throwUpgradeException)
		{
			new UpgradeImpl2008().Upgrade(xmlReader, outStream);
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00017004 File Offset: 0x00015204
		internal static Stream UpgradeToCurrent(Stream inStream, bool throwUpgradeException)
		{
			return RDLUpgrader.UpgradeToCurrent(inStream, throwUpgradeException, true);
		}
	}
}
