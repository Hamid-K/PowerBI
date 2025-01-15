using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Wrangling.SignificantInputs
{
	// Token: 0x020000FF RID: 255
	public class SignificantInputProgramProfile<TProgram, TInput, TOutput> where TProgram : Program<TInput, TOutput>
	{
		// Token: 0x060005E9 RID: 1513 RVA: 0x000132EC File Offset: 0x000114EC
		internal SignificantInputProgramProfile(IReadOnlyList<TProgram> topPrograms, IReadOnlyList<TProgram> randomPrograms, TProgram topProgramNoInputs)
		{
			this.TopPrograms = topPrograms;
			this.RandomPrograms = randomPrograms;
			this.TopProgram = ((topPrograms != null) ? topPrograms.FirstOrDefault<TProgram>() : default(TProgram));
			this.TopProgramNoInputs = topProgramNoInputs;
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x0001332E File Offset: 0x0001152E
		public TProgram AnyTopProgram
		{
			get
			{
				TProgram tprogram;
				if ((tprogram = this.TopProgram) == null)
				{
					tprogram = this.TopProgramNoInputs;
				}
				return tprogram;
			}
		}

		// Token: 0x060005EB RID: 1515 RVA: 0x00013348 File Offset: 0x00011548
		internal Record<Func<int, bool>, IReadOnlyList<TProgram>> RankedPrograms()
		{
			List<TProgram> list = new List<TProgram>();
			list.AddRange(this.TopPrograms);
			int topProgramCount = this.TopPrograms.Count;
			if (this.TopProgramNoInputs != null)
			{
				list.Add(this.TopProgramNoInputs);
				topProgramCount++;
			}
			list.AddRange(this.RandomPrograms);
			return Record.Create<Func<int, bool>, IReadOnlyList<TProgram>>((int index) => index < topProgramCount, list);
		}

		// Token: 0x0400026D RID: 621
		internal readonly IReadOnlyList<TProgram> TopPrograms;

		// Token: 0x0400026E RID: 622
		internal readonly IReadOnlyList<TProgram> RandomPrograms;

		// Token: 0x0400026F RID: 623
		public readonly TProgram TopProgram;

		// Token: 0x04000270 RID: 624
		public readonly TProgram TopProgramNoInputs;
	}
}
