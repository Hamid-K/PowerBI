using System;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001E2 RID: 482
	public class FileNameToken : PathToken
	{
		// Token: 0x06000A80 RID: 2688 RVA: 0x0001FF0B File Offset: 0x0001E10B
		public FileNameToken(string source, int start, int end, string fileName, string extension)
			: base(source, start, end)
		{
			this.FileName = fileName;
			this.Extension = extension;
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x0001FF26 File Offset: 0x0001E126
		public string FileName { get; }

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x0001FF2E File Offset: 0x0001E12E
		public string Extension { get; }

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x0001FF36 File Offset: 0x0001E136
		public override string EntityName
		{
			get
			{
				return "File Name";
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x0001FF3D File Offset: 0x0001E13D
		public override double ScoreMultiplier
		{
			get
			{
				return base.ScoreMultiplier * 2.0;
			}
		}
	}
}
