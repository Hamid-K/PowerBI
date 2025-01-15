using System;

namespace antlr
{
	// Token: 0x02000015 RID: 21
	internal abstract class FileLineFormatter
	{
		// Token: 0x060000D2 RID: 210 RVA: 0x00003D1C File Offset: 0x00001F1C
		public static FileLineFormatter getFormatter()
		{
			return FileLineFormatter.formatter;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003D23 File Offset: 0x00001F23
		public static void setFormatter(FileLineFormatter f)
		{
			FileLineFormatter.formatter = f;
		}

		// Token: 0x060000D4 RID: 212
		public abstract string getFormatString(string fileName, int line, int column);

		// Token: 0x04000047 RID: 71
		private static FileLineFormatter formatter = new DefaultFileLineFormatter();
	}
}
