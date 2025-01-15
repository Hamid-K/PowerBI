using System;
using System.IO.Packaging;

namespace Microsoft.Mashup.Client.Packaging
{
	// Token: 0x02000009 RID: 9
	public class PowerBIDesktopMashupReader : MashupReader
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002310 File Offset: 0x00000510
		public override bool TryGetMashupBytes(Package powerBIPackage, out byte[] mashupBytes)
		{
			if (powerBIPackage.PartExists(PowerBIDesktopMashupReader.mashupPath))
			{
				mashupBytes = powerBIPackage.GetPart(PowerBIDesktopMashupReader.mashupPath).GetStream(3, 1).ReadAllBytes();
				return mashupBytes.Length != 0;
			}
			mashupBytes = null;
			return false;
		}

		// Token: 0x04000036 RID: 54
		private static readonly Uri mashupPath = new Uri("/DataMashup", 2);
	}
}
