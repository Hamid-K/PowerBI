using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Extraction.Json.Learning;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Constraints
{
	// Token: 0x02000B9F RID: 2975
	public class Delete : Interactive
	{
		// Token: 0x06004B96 RID: 19350 RVA: 0x000EEBC6 File Offset: 0x000ECDC6
		public Delete(string[] path)
			: base(path)
		{
		}

		// Token: 0x06004B97 RID: 19351 RVA: 0x000EEBCF File Offset: 0x000ECDCF
		public Delete(IReadOnlyList<string[]> paths)
			: base(paths)
		{
		}

		// Token: 0x06004B98 RID: 19352 RVA: 0x000EEBD8 File Offset: 0x000ECDD8
		public override void SetOptions(SynthesisOptions options)
		{
			options.DeletePaths = options.DeletePaths.Concat(base.Paths).ToList<string[]>();
		}

		// Token: 0x06004B99 RID: 19353 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override bool ConflictsWith(Constraint<string, ITable<string>> other)
		{
			return false;
		}

		// Token: 0x06004B9A RID: 19354 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<string, ITable<string>> program)
		{
			return true;
		}

		// Token: 0x06004B9B RID: 19355 RVA: 0x000EEBF6 File Offset: 0x000ECDF6
		private bool Equals(Delete other)
		{
			return other != null && (this == other || base.Paths.Equals(other.Paths));
		}

		// Token: 0x06004B9C RID: 19356 RVA: 0x000EEC14 File Offset: 0x000ECE14
		public override bool Equals(object obj)
		{
			return this.Equals(obj as Delete);
		}

		// Token: 0x06004B9D RID: 19357 RVA: 0x000EEC14 File Offset: 0x000ECE14
		public override bool Equals(Constraint<string, ITable<string>> other)
		{
			return this.Equals(other as Delete);
		}

		// Token: 0x06004B9E RID: 19358 RVA: 0x000EEC22 File Offset: 0x000ECE22
		public override int GetHashCode()
		{
			return 461 ^ base.Paths.GetHashCode();
		}
	}
}
