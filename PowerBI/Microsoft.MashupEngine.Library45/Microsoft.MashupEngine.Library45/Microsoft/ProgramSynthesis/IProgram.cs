using System;
using System.Globalization;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis
{
	// Token: 0x0200008C RID: 140
	public interface IProgram
	{
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000311 RID: 785
		ProgramNode ProgramNode { get; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000312 RID: 786
		ProgramNode RawProgramNode { get; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000313 RID: 787
		double Score { get; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000314 RID: 788
		Version Version { get; }

		// Token: 0x06000315 RID: 789
		string Serialize(ASTSerializationFormat serializationFormat = ASTSerializationFormat.XML);

		// Token: 0x06000316 RID: 790
		string Serialize(ASTSerializationSettings serializationSettings);

		// Token: 0x06000317 RID: 791
		string Describe(CultureInfo cultureInfo = null);

		// Token: 0x06000318 RID: 792
		object Run(object input);
	}
}
