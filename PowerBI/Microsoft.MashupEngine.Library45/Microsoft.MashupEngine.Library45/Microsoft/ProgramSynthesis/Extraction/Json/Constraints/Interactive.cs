using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Extraction.Json.Learning;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Constraints
{
	// Token: 0x02000BA1 RID: 2977
	public abstract class Interactive : Constraint<string, ITable<string>>, IOptionConstraint<SynthesisOptions>
	{
		// Token: 0x06004BA6 RID: 19366 RVA: 0x000EECA9 File Offset: 0x000ECEA9
		public Interactive(string[] path)
			: this(new string[][] { path })
		{
		}

		// Token: 0x06004BA7 RID: 19367 RVA: 0x000EECBC File Offset: 0x000ECEBC
		public Interactive(IReadOnlyList<string[]> paths)
		{
			if (paths != null)
			{
				if (!paths.Any((string[] p) => p == null))
				{
					this.Paths = paths;
					return;
				}
			}
			throw new ArgumentNullException("paths");
		}

		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x06004BA8 RID: 19368 RVA: 0x000EED0B File Offset: 0x000ECF0B
		public IReadOnlyList<string[]> Paths { get; }

		// Token: 0x06004BA9 RID: 19369
		public abstract void SetOptions(SynthesisOptions options);
	}
}
