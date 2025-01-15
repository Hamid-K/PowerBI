using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Extraction.Json.Learning;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Constraints
{
	// Token: 0x02000BA4 RID: 2980
	public class JoinArray : Interactive
	{
		// Token: 0x06004BB6 RID: 19382 RVA: 0x000EEBC6 File Offset: 0x000ECDC6
		public JoinArray(string[] path)
			: base(path)
		{
		}

		// Token: 0x06004BB7 RID: 19383 RVA: 0x000EEBCF File Offset: 0x000ECDCF
		public JoinArray(IReadOnlyList<string[]> paths)
			: base(paths)
		{
		}

		// Token: 0x06004BB8 RID: 19384 RVA: 0x000EED5F File Offset: 0x000ECF5F
		public override void SetOptions(SynthesisOptions options)
		{
			options.JoinArrayPaths = options.JoinArrayPaths.Concat(base.Paths).ToList<string[]>();
		}

		// Token: 0x06004BB9 RID: 19385 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override bool ConflictsWith(Constraint<string, ITable<string>> other)
		{
			return false;
		}

		// Token: 0x06004BBA RID: 19386 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<string, ITable<string>> program)
		{
			return true;
		}

		// Token: 0x06004BBB RID: 19387 RVA: 0x000EEBF6 File Offset: 0x000ECDF6
		private bool Equals(JoinArray other)
		{
			return other != null && (this == other || base.Paths.Equals(other.Paths));
		}

		// Token: 0x06004BBC RID: 19388 RVA: 0x000EED7D File Offset: 0x000ECF7D
		public override bool Equals(object obj)
		{
			return this.Equals(obj as JoinArray);
		}

		// Token: 0x06004BBD RID: 19389 RVA: 0x000EED7D File Offset: 0x000ECF7D
		public override bool Equals(Constraint<string, ITable<string>> other)
		{
			return this.Equals(other as JoinArray);
		}

		// Token: 0x06004BBE RID: 19390 RVA: 0x000EEC22 File Offset: 0x000ECE22
		public override int GetHashCode()
		{
			return 461 ^ base.Paths.GetHashCode();
		}
	}
}
