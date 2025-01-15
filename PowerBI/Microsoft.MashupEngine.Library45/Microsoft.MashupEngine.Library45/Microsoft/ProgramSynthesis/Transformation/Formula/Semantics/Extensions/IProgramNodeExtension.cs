using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Wrangling;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions
{
	// Token: 0x02001785 RID: 6021
	public static class IProgramNodeExtension
	{
		// Token: 0x0600C788 RID: 51080 RVA: 0x002ADB21 File Offset: 0x002ABD21
		public static string Render(this IEnumerable<IProgram> programs, int startNumber = 1)
		{
			if (programs == null)
			{
				return null;
			}
			return programs.Select((IProgram program) => program.ToString()).RenderNumbered(startNumber);
		}

		// Token: 0x0600C789 RID: 51081 RVA: 0x002ADB54 File Offset: 0x002ABD54
		public static string Render(this IEnumerable<IProgram> programs, IEnumerable<Example> examples, int startNumber = 1)
		{
			if (programs == null)
			{
				return null;
			}
			programs = programs.ToReadOnlyList<IProgram>();
			if (!programs.Any<IProgram>())
			{
				return "<none>";
			}
			return programs.Select((IProgram program) => string.Format("{0}{1}{2}", program, Environment.NewLine, examples.Render(null))).RenderNumbered(startNumber);
		}
	}
}
