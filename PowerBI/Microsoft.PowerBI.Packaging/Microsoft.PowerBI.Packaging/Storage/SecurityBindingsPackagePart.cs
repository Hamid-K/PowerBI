using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;

namespace Microsoft.PowerBI.Packaging.Storage
{
	// Token: 0x02000053 RID: 83
	internal sealed class SecurityBindingsPackagePart : PowerBIPackagePart
	{
		// Token: 0x06000282 RID: 642 RVA: 0x00007AAE File Offset: 0x00005CAE
		public SecurityBindingsPackagePart(Package package)
			: base(package, SecurityBindingsPackagePart.SecurityBindingsPath, SecurityBindingsPackagePart.IsOptional, CompressionOption.Normal)
		{
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00007AC4 File Offset: 0x00005CC4
		public IStreamablePowerBIPackagePartContent Serialize()
		{
			IStreamablePowerBIPackagePartContent streamablePowerBIPackagePartContent;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					ushort num = 3;
					binaryWriter.Write(num);
					foreach (Uri uri in SecurityBindingsPackagePart.GetProtectedPartsUri(num, base.Package))
					{
						byte[] array = SecurityBindingsPackagePart.HashPart(new PowerBIPackagePart(base.Package, uri, true, CompressionOption.Normal));
						binaryWriter.Write(array);
					}
					binaryWriter.Flush();
				}
				streamablePowerBIPackagePartContent = new StreamablePowerBIPackagePartContent(SecurityUtils.EncryptBytes(memoryStream.ToArray(), SecurityBindingsPackagePart.additionalEntropy), "");
			}
			return streamablePowerBIPackagePartContent;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00007B9C File Offset: 0x00005D9C
		public byte[] Deserialize(IStreamablePowerBIPackagePartContent streamablePowerBIPackagePartContent)
		{
			return PowerBIPackagingUtils.GetContentAsBytes(streamablePowerBIPackagePartContent, SecurityBindingsPackagePart.IsOptional);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00007BA9 File Offset: 0x00005DA9
		private static IEnumerable<Uri> GetProtectedPartsUri(ushort version, Package package)
		{
			if (version == 3)
			{
				List<Uri> list = new List<Uri>();
				list.AddRange(SecurityBindingsPackagePart.GetReportParts(package));
				list.Add(SecurityBindingsPackagePart.ConnectionsPath);
				list.Add(SecurityBindingsPackagePart.ReportSettingsPath);
				list.Add(SecurityBindingsPackagePart.ReportMobileStatePath);
				return list;
			}
			return new Uri[0];
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00007BE8 File Offset: 0x00005DE8
		private static IEnumerable<Uri> GetReportParts(Package package)
		{
			bool flag = package.PartExists(SecurityBindingsPackagePart.EnhancedReportFilePath);
			List<Uri> list = new List<Uri>();
			if (flag)
			{
				list.AddRange(SecurityBindingsPackagePart.GetEnhancedReportPaths(package));
			}
			else
			{
				list.Add(SecurityBindingsPackagePart.ReportLayoutPath);
			}
			return list;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00007C24 File Offset: 0x00005E24
		private static IEnumerable<Uri> GetEnhancedReportPaths(Package package)
		{
			return from part in package.GetParts()
				where part.Uri.ToString().StartsWith(SecurityBindingsPackagePart.enhancedReportFolder)
				orderby part.Uri.ToString()
				select part.Uri;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x00007CA4 File Offset: 0x00005EA4
		private static byte[] HashPart(PowerBIPackagePart part)
		{
			IStreamablePowerBIPackagePartContent content = part.GetContent();
			return SecurityUtils.HashContent((content != null) ? content.GetStream() : new MemoryStream(new byte[0]));
		}

		// Token: 0x0400014F RID: 335
		public static readonly Uri SecurityBindingsPath = new Uri("/SecurityBindings", UriKind.Relative);

		// Token: 0x04000150 RID: 336
		public static readonly bool IsOptional = true;

		// Token: 0x04000151 RID: 337
		private static readonly Uri ConnectionsPath = new Uri("/Connections", UriKind.Relative);

		// Token: 0x04000152 RID: 338
		private static readonly Uri ReportLayoutPath = new Uri("/Report/Layout", UriKind.Relative);

		// Token: 0x04000153 RID: 339
		private static readonly Uri ReportSettingsPath = new Uri("/Settings", UriKind.Relative);

		// Token: 0x04000154 RID: 340
		private static readonly Uri ReportMobileStatePath = new Uri("/Report/MobileState", UriKind.Relative);

		// Token: 0x04000155 RID: 341
		private static readonly Uri EnhancedReportFilePath = new Uri("/Report/definition/report.json", UriKind.Relative);

		// Token: 0x04000156 RID: 342
		private static readonly string enhancedReportFolder = "/Report/definition/";

		// Token: 0x04000157 RID: 343
		private static readonly string additionalEntropy = "DataExplorer Package Components";

		// Token: 0x04000158 RID: 344
		private const ushort currentVersion = 3;
	}
}
