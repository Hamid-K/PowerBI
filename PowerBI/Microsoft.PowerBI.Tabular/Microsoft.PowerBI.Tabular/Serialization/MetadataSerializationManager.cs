using System;
using System.IO;
using System.Text;
using Microsoft.AnalysisServices.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x02000185 RID: 389
	internal abstract class MetadataSerializationManager : MetadataSerializationManagerBase
	{
		// Token: 0x0600180D RID: 6157 RVA: 0x000A3200 File Offset: 0x000A1400
		private protected MetadataSerializationManager(string extension)
		{
			this.extension = extension.Trim();
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x000A3214 File Offset: 0x000A1414
		private protected static bool ParseLogicalPath(string logicalPath, out string relativePath, out string name)
		{
			name = null;
			relativePath = null;
			int num = logicalPath.Length - 1;
			while (num >= 0 && char.IsWhiteSpace(logicalPath, num))
			{
				num--;
			}
			if (num < 0)
			{
				return false;
			}
			int num2 = 0;
			while (num2 < num && char.IsWhiteSpace(logicalPath, num2))
			{
				num2++;
			}
			int num3 = num;
			while (num3 >= num2 && !FilesHelper.IsDirectorySeperator(logicalPath, num3))
			{
				num3--;
			}
			if (num3 == num)
			{
				return false;
			}
			if (num3 <= num2)
			{
				if (num3 == num2)
				{
					if (num2 == num)
					{
						return false;
					}
					num2++;
				}
				name = logicalPath.Substring(num2, num - num2 - 1);
			}
			else
			{
				name = logicalPath.Substring(num3 + 1, num - num3);
				relativePath = MetadataSerializationManager.NormalizeRelativePath(logicalPath, num2, num3 - num2);
			}
			return true;
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x000A32B8 File Offset: 0x000A14B8
		private static string NormalizeRelativePath(string logicalPath, int start, int count)
		{
			if (logicalPath[start] == '.' && count == 1)
			{
				return null;
			}
			if (logicalPath[start] == '.' && count > 1 && FilesHelper.IsDirectorySeperator(logicalPath, start + 1))
			{
				start += 2;
				count -= 2;
				if (count == 0)
				{
					return null;
				}
			}
			int num = logicalPath.IndexOf(Path.DirectorySeparatorChar, start, count);
			if (num == -1)
			{
				return logicalPath.Substring(start, count);
			}
			StringBuilder stringBuilder = new StringBuilder(logicalPath, start, count, count);
			while (num != -1)
			{
				stringBuilder[num - start] = Path.AltDirectorySeparatorChar;
				num = logicalPath.IndexOf(Path.DirectorySeparatorChar, num + 1, count - num - 1);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000480 RID: 1152
		private protected readonly string extension;
	}
}
