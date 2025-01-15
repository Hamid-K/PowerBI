using System;
using System.Linq;

namespace dotless.Core.Parser
{
	// Token: 0x02000028 RID: 40
	public class Zone
	{
		// Token: 0x06000166 RID: 358 RVA: 0x0000836A File Offset: 0x0000656A
		public Zone(NodeLocation location)
			: this(location, null, null)
		{
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00008378 File Offset: 0x00006578
		public Zone(NodeLocation location, string error, Zone callZone)
		{
			string source = location.Source;
			if (location.Index > source.Length)
			{
				int length = source.Length;
			}
			int num;
			int num2;
			Zone.GetLineNumber(location, out num, out num2);
			string[] array = source.Split(new char[] { '\n' });
			this.FileName = location.FileName;
			this.Message = error;
			this.CallZone = callZone;
			this.LineNumber = num + 1;
			this.Position = num2;
			this.Extract = new Extract(array, num);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000083FC File Offset: 0x000065FC
		public static int GetLineNumber(NodeLocation location)
		{
			int num;
			int num2;
			Zone.GetLineNumber(location, out num, out num2);
			return num + 1;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00008418 File Offset: 0x00006618
		private static void GetLineNumber(NodeLocation location, out int lineNumber, out int position)
		{
			string source = location.Source;
			int num = location.Index;
			if (location.Index > source.Length)
			{
				num = source.Length;
			}
			string text = source.Substring(0, num);
			int num2 = text.LastIndexOf('\n') + 1;
			lineNumber = text.Count((char c) => c == '\n');
			position = num - num2;
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00008488 File Offset: 0x00006688
		// (set) Token: 0x0600016B RID: 363 RVA: 0x00008490 File Offset: 0x00006690
		public int LineNumber { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00008499 File Offset: 0x00006699
		// (set) Token: 0x0600016D RID: 365 RVA: 0x000084A1 File Offset: 0x000066A1
		public int Position { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600016E RID: 366 RVA: 0x000084AA File Offset: 0x000066AA
		// (set) Token: 0x0600016F RID: 367 RVA: 0x000084B2 File Offset: 0x000066B2
		public Extract Extract { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000170 RID: 368 RVA: 0x000084BB File Offset: 0x000066BB
		// (set) Token: 0x06000171 RID: 369 RVA: 0x000084C3 File Offset: 0x000066C3
		public string Message { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000172 RID: 370 RVA: 0x000084CC File Offset: 0x000066CC
		// (set) Token: 0x06000173 RID: 371 RVA: 0x000084D4 File Offset: 0x000066D4
		public string FileName { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000174 RID: 372 RVA: 0x000084DD File Offset: 0x000066DD
		// (set) Token: 0x06000175 RID: 373 RVA: 0x000084E5 File Offset: 0x000066E5
		public Zone CallZone { get; set; }
	}
}
