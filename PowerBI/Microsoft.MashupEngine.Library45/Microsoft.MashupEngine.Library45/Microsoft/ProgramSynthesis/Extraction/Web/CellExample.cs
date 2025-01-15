using System;

namespace Microsoft.ProgramSynthesis.Extraction.Web
{
	// Token: 0x02000FD2 RID: 4050
	public class CellExample
	{
		// Token: 0x170013E0 RID: 5088
		// (get) Token: 0x06006FBC RID: 28604 RVA: 0x0016CEA3 File Offset: 0x0016B0A3
		public string Value { get; }

		// Token: 0x170013E1 RID: 5089
		// (get) Token: 0x06006FBD RID: 28605 RVA: 0x0016CEAB File Offset: 0x0016B0AB
		public bool IsSoft { get; }

		// Token: 0x170013E2 RID: 5090
		// (get) Token: 0x06006FBE RID: 28606 RVA: 0x0016CEB3 File Offset: 0x0016B0B3
		public int NodeIndex { get; }

		// Token: 0x06006FBF RID: 28607 RVA: 0x0016CEBB File Offset: 0x0016B0BB
		public CellExample(string value, bool isSoft = false, int nodeIndex = -1)
		{
			this.Value = value;
			this.IsSoft = isSoft;
			this.NodeIndex = nodeIndex;
		}
	}
}
