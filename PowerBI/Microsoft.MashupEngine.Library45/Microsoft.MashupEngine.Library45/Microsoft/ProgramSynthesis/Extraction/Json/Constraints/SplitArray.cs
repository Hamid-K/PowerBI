using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Extraction.Json.Learning;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Constraints
{
	// Token: 0x02000BA7 RID: 2983
	public class SplitArray : Interactive
	{
		// Token: 0x06004BD2 RID: 19410 RVA: 0x000EEBC6 File Offset: 0x000ECDC6
		public SplitArray(string[] path)
			: base(path)
		{
		}

		// Token: 0x06004BD3 RID: 19411 RVA: 0x000EEBCF File Offset: 0x000ECDCF
		public SplitArray(IReadOnlyList<string[]> paths)
			: base(paths)
		{
		}

		// Token: 0x06004BD4 RID: 19412 RVA: 0x000EEE5E File Offset: 0x000ED05E
		public override void SetOptions(SynthesisOptions options)
		{
			options.SplitArrayPaths = options.SplitArrayPaths.Concat(base.Paths).ToList<string[]>();
		}

		// Token: 0x06004BD5 RID: 19413 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override bool ConflictsWith(Constraint<string, ITable<string>> other)
		{
			return false;
		}

		// Token: 0x06004BD6 RID: 19414 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<string, ITable<string>> program)
		{
			return true;
		}

		// Token: 0x06004BD7 RID: 19415 RVA: 0x000EEBF6 File Offset: 0x000ECDF6
		private bool Equals(SplitArray other)
		{
			return other != null && (this == other || base.Paths.Equals(other.Paths));
		}

		// Token: 0x06004BD8 RID: 19416 RVA: 0x000EEE7C File Offset: 0x000ED07C
		public override bool Equals(object obj)
		{
			return this.Equals(obj as SplitArray);
		}

		// Token: 0x06004BD9 RID: 19417 RVA: 0x000EEE7C File Offset: 0x000ED07C
		public override bool Equals(Constraint<string, ITable<string>> other)
		{
			return this.Equals(other as SplitArray);
		}

		// Token: 0x06004BDA RID: 19418 RVA: 0x000EEE8A File Offset: 0x000ED08A
		public override int GetHashCode()
		{
			return 787 ^ base.Paths.GetHashCode();
		}
	}
}
