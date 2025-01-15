using System;
using System.Text;

namespace Microsoft.Mashup.Client.Packaging.SerializationObjectModel
{
	// Token: 0x0200000F RID: 15
	public class SerializedPackageItemLocation
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002ACD File Offset: 0x00000CCD
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00002AD5 File Offset: 0x00000CD5
		public SerializedPackageItemType ItemType { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002ADE File Offset: 0x00000CDE
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002AE6 File Offset: 0x00000CE6
		public string ItemPath { get; set; }

		// Token: 0x06000041 RID: 65 RVA: 0x00002AEF File Offset: 0x00000CEF
		public string[] ParseItemPath()
		{
			return SerializedPackageItemLocation.PartsFromItemPath(this.ItemPath);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002AFC File Offset: 0x00000CFC
		public static string ItemPathFromParts(params string[] parts)
		{
			string text = string.Empty;
			foreach (string text2 in parts)
			{
				if (text2 != null)
				{
					if (text != string.Empty)
					{
						text += "/";
					}
					text += SerializedPackageItemLocation.EscapeLongDataString(text2);
				}
			}
			return text;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002B50 File Offset: 0x00000D50
		public static string[] PartsFromItemPath(string itemPath)
		{
			string[] array = itemPath.Split(new char[] { '/' });
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Uri.UnescapeDataString(array[i]);
			}
			return array;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002B8C File Offset: 0x00000D8C
		private static string EscapeLongDataString(string str)
		{
			if (str.Length <= 65519)
			{
				return Uri.EscapeDataString(str);
			}
			int i = 0;
			StringBuilder stringBuilder = new StringBuilder();
			while (i < str.Length)
			{
				int num = Math.Min(str.Length - i, 65519);
				string text = str.Substring(i, num);
				text = Uri.EscapeDataString(text);
				stringBuilder.Append(text);
				i += num;
			}
			return stringBuilder.ToString();
		}
	}
}
