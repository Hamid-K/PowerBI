using System;
using System.IO;
using System.Xml;

namespace Microsoft.ReportingServices.RdlObjectModel2005.Upgrade
{
	// Token: 0x02000059 RID: 89
	internal class Upgrader
	{
		// Token: 0x06000399 RID: 921 RVA: 0x000157BC File Offset: 0x000139BC
		public static void Upgrade(string inputFile, string outputFile, bool throwUpgradeException)
		{
			using (FileStream fileStream = File.OpenRead(inputFile))
			{
				using (FileStream fileStream2 = File.Open(outputFile, FileMode.Create, FileAccess.ReadWrite))
				{
					Upgrader.Upgrade(fileStream, fileStream2, throwUpgradeException);
				}
			}
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00015814 File Offset: 0x00013A14
		public static void Upgrade(Stream inStream, Stream outStream, bool throwUpgradeException)
		{
			Upgrader.Upgrade(new XmlTextReader(inStream)
			{
				DtdProcessing = DtdProcessing.Prohibit
			}, outStream, throwUpgradeException);
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0001582A File Offset: 0x00013A2A
		public static void Upgrade(XmlReader xmlReader, Stream outStream, bool throwUpgradeException)
		{
			new UpgradeImpl2005(throwUpgradeException).Upgrade(xmlReader, outStream);
		}
	}
}
