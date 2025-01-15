using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Translation.Python
{
	// Token: 0x02000325 RID: 805
	public static class SchemaSemanticsLoader
	{
		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x060011C6 RID: 4550 RVA: 0x000349B9 File Offset: 0x00032BB9
		public static string Load
		{
			get
			{
				return AssemblyResourceUtils.LoadResourceFromAssembly(typeof(SchemaSemanticsLoader).GetTypeInfo().Assembly, SchemaSemanticsLoader.ResourceName);
			}
		}

		// Token: 0x0400089F RID: 2207
		private static readonly Assembly Assembly = typeof(SchemaSemanticsLoader).GetTypeInfo().Assembly;

		// Token: 0x040008A0 RID: 2208
		private static readonly string AssemblyName = SchemaSemanticsLoader.Assembly.GetName().Name;

		// Token: 0x040008A1 RID: 2209
		private static readonly string ResourceName = FormattableString.Invariant(FormattableStringFactory.Create("{0}.Translation.Python.schema_semantics.py", new object[] { SchemaSemanticsLoader.AssemblyName }));
	}
}
