using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.AST;

namespace Microsoft.ProgramSynthesis.VersionSpace
{
	// Token: 0x02000293 RID: 659
	public struct RewriteRule
	{
		// Token: 0x06000E53 RID: 3667 RVA: 0x00029C6D File Offset: 0x00027E6D
		public RewriteRule(ProgramNode source, ProgramNode target)
		{
			this.Source = source;
			this.Target = target;
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x00029C7D File Offset: 0x00027E7D
		public RewriteRule Reverse()
		{
			return new RewriteRule(this.Target, this.Source);
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x00029C90 File Offset: 0x00027E90
		public override string ToString()
		{
			return FormattableString.Invariant(FormattableStringFactory.Create("{0} -> {1}", new object[] { this.Source, this.Target }));
		}

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000E56 RID: 3670 RVA: 0x00029CB9 File Offset: 0x00027EB9
		public readonly ProgramNode Source { get; }

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000E57 RID: 3671 RVA: 0x00029CC1 File Offset: 0x00027EC1
		public readonly ProgramNode Target { get; }
	}
}
