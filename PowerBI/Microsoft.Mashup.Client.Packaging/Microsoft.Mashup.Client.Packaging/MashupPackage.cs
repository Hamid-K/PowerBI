using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Text;

namespace Microsoft.Mashup.Client.Packaging
{
	// Token: 0x02000008 RID: 8
	public class MashupPackage
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000021A8 File Offset: 0x000003A8
		public MashupPackage(PackageComponents packageComponents)
		{
			this.mFiles = new Dictionary<string, string>();
			using (MemoryStream memoryStream = new MemoryStream(packageComponents.PartsBytes))
			{
				using (Package package = Package.Open(memoryStream))
				{
					foreach (PackagePart packagePart in package.GetParts())
					{
						if (packagePart.Uri.OriginalString.StartsWith("/formulas/", 5))
						{
							using (Stream stream = packagePart.GetStream())
							{
								this.mFiles.Add(packagePart.Uri.OriginalString, Encoding.UTF8.GetString(stream.ReadAllBytes()));
							}
						}
					}
				}
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000022A0 File Offset: 0x000004A0
		public Dictionary<string, string> MFiles
		{
			get
			{
				return this.mFiles;
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000022A8 File Offset: 0x000004A8
		public static bool TryCreateFromExcelFile(Stream excelStream, out MashupPackage mashupPackage)
		{
			byte[] array;
			PackageComponents packageComponents;
			if (new ExcelMashupReader().TryGetMashupBytes(excelStream, out array) && PackageComponents.TryDeserialize(array, out packageComponents))
			{
				mashupPackage = new MashupPackage(packageComponents);
				return true;
			}
			mashupPackage = null;
			return false;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000022DC File Offset: 0x000004DC
		public static bool TryCreateFromPowerBIDesktopFile(Stream powerBIStream, out MashupPackage mashupPackage)
		{
			byte[] array;
			PackageComponents packageComponents;
			if (new PowerBIDesktopMashupReader().TryGetMashupBytes(powerBIStream, out array) && PackageComponents.TryDeserialize(array, out packageComponents))
			{
				mashupPackage = new MashupPackage(packageComponents);
				return true;
			}
			mashupPackage = null;
			return false;
		}

		// Token: 0x04000034 RID: 52
		private const string mFilePrefix = "/formulas/";

		// Token: 0x04000035 RID: 53
		private readonly Dictionary<string, string> mFiles;
	}
}
