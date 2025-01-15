using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000149 RID: 329
	internal static class VersioningUtility
	{
		// Token: 0x06000A3D RID: 2621 RVA: 0x00022748 File Offset: 0x00020948
		internal static void LoadSupportedClientVersions()
		{
			Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("config.policy.VersionInfo.xml");
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				using (XmlReader xmlReader = XmlReader.Create(manifestResourceStream))
				{
					xmlDocument.Load(xmlReader);
				}
				XmlNode xmlNode = xmlDocument.SelectSingleNode(VersioningUtility.GetXmlPath());
				XmlNodeList xmlNodeList = xmlNode.SelectNodes("OtherSupportedOldClientVersions/version");
				foreach (object obj in xmlNodeList)
				{
					XmlNode xmlNode2 = (XmlNode)obj;
					VersioningUtility.OtherSupportedOldClients.Add(long.Parse(xmlNode2.InnerText, NumberFormatInfo.InvariantInfo));
				}
				xmlNodeList = xmlNode.SelectNodes("OtherSupportedNewClientVersions/version");
				foreach (object obj2 in xmlNodeList)
				{
					XmlNode xmlNode3 = (XmlNode)obj2;
					VersioningUtility.OtherSupportedNewClients.Add(long.Parse(xmlNode3.InnerText, NumberFormatInfo.InvariantInfo));
				}
			}
			catch (XmlException ex)
			{
				VersioningUtility.ThrowException(ex);
			}
			catch (FormatException ex2)
			{
				VersioningUtility.ThrowException(ex2);
			}
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x000228AC File Offset: 0x00020AAC
		private static void ThrowException(Exception innerEx)
		{
			throw new DataCacheException(VersioningUtility.VersionFileError, innerEx);
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x000228BC File Offset: 0x00020ABC
		private static string GetXmlPath()
		{
			return CloudUtility.IsVASDeployment ? string.Format(CultureInfo.InvariantCulture, "/VersionInfo/Project[@type='{0}']/Release[@codeversion='{1}']", new object[]
			{
				"VAS",
				ConfigManager.CodeVersion.ToString(NumberFormatInfo.InvariantInfo)
			}) : string.Format(CultureInfo.InvariantCulture, "/VersionInfo/Project[@type='{0}']/Release[@codeversion='{1}']", new object[]
			{
				"OnPrem",
				ConfigManager.CodeVersion.ToString(NumberFormatInfo.InvariantInfo)
			});
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x00022936 File Offset: 0x00020B36
		internal static List<long> GetOtherSupportedClientVersions()
		{
			return VersioningUtility.GetOtherSupportedClientVersions(ConfigManager.CodeVersion, ConfigManager.CodeVersion);
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x00022948 File Offset: 0x00020B48
		internal static List<long> GetOtherSupportedClientVersions(long beginAllowedCodeVersion, long endAllowedCodeVersion)
		{
			List<long> list = new List<long>();
			if (beginAllowedCodeVersion == ConfigManager.CodeVersion)
			{
				list.AddRange(VersioningUtility.OtherSupportedNewClients);
			}
			if (endAllowedCodeVersion == ConfigManager.CodeVersion)
			{
				list.AddRange(VersioningUtility.OtherSupportedOldClients);
			}
			return list;
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x00022984 File Offset: 0x00020B84
		internal static Version StringToVersion(string stringVersion)
		{
			Version version = null;
			try
			{
				version = new Version(stringVersion);
			}
			catch (Exception ex)
			{
				if (ex is ArgumentException || ex is ArgumentNullException || ex is ArgumentOutOfRangeException || ex is FormatException || ex is OverflowException)
				{
					throw new DataCacheException("VersioningUtility", 9001, GlobalResourceLoader.GetString(CultureInfo.CurrentUICulture, 9001, ex.Message), ex, false);
				}
				throw;
			}
			return version;
		}

		// Token: 0x0400071D RID: 1821
		private const string LogSource = "VersioningUtility";

		// Token: 0x0400071E RID: 1822
		private const string VersionFile = "config.policy.VersionInfo.xml";

		// Token: 0x0400071F RID: 1823
		private const string ReleaseNodePath = "/VersionInfo/Project[@type='{0}']/Release[@codeversion='{1}']";

		// Token: 0x04000720 RID: 1824
		private const string OldClientVersionsNodePath = "OtherSupportedOldClientVersions/version";

		// Token: 0x04000721 RID: 1825
		private const string NewClientVersionsNodePath = "OtherSupportedNewClientVersions/version";

		// Token: 0x04000722 RID: 1826
		private const string VASNodeName = "VAS";

		// Token: 0x04000723 RID: 1827
		private const string OnPremNodeName = "OnPrem";

		// Token: 0x04000724 RID: 1828
		private static List<long> OtherSupportedOldClients = new List<long>();

		// Token: 0x04000725 RID: 1829
		private static List<long> OtherSupportedNewClients = new List<long>();

		// Token: 0x04000726 RID: 1830
		private static string VersionFileError = "Error - 'VersionInfo.xml' file has some invalid values";
	}
}
