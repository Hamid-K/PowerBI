using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Constraints
{
	// Token: 0x020019CE RID: 6606
	public class CSharpTranslationConstraint : TranslationConstraint, ICSharpTranslationOptions, ITranslationOptions, IUniqueConstraint<CSharpTranslationConstraint>
	{
		// Token: 0x170023D2 RID: 9170
		// (get) Token: 0x0600D781 RID: 55169 RVA: 0x002DD074 File Offset: 0x002DB274
		// (set) Token: 0x0600D782 RID: 55170 RVA: 0x002DD07C File Offset: 0x002DB27C
		public string ClassName { get; set; } = "Formula";

		// Token: 0x170023D3 RID: 9171
		// (get) Token: 0x0600D783 RID: 55171 RVA: 0x002DD085 File Offset: 0x002DB285
		// (set) Token: 0x0600D784 RID: 55172 RVA: 0x002DD08D File Offset: 0x002DB28D
		public string MethodName { get; set; } = "Execute";

		// Token: 0x170023D4 RID: 9172
		// (get) Token: 0x0600D785 RID: 55173 RVA: 0x002DD096 File Offset: 0x002DB296
		// (set) Token: 0x0600D786 RID: 55174 RVA: 0x002DD09E File Offset: 0x002DB29E
		public string NamespaceName { get; set; } = "Microsoft.ProgramSynthesis.Transformation";

		// Token: 0x170023D5 RID: 9173
		// (get) Token: 0x0600D787 RID: 55175 RVA: 0x002DD0A7 File Offset: 0x002DB2A7
		// (set) Token: 0x0600D788 RID: 55176 RVA: 0x002DD0AF File Offset: 0x002DB2AF
		public CSharpOptimizations Optimizations { get; set; } = CSharpOptimizations.All;

		// Token: 0x170023D6 RID: 9174
		// (get) Token: 0x0600D789 RID: 55177 RVA: 0x002DD0B8 File Offset: 0x002DB2B8
		// (set) Token: 0x0600D78A RID: 55178 RVA: 0x002DD0C0 File Offset: 0x002DB2C0
		public CSharpCodeStyle Style { get; set; } = CSharpCodeStyle.Script;

		// Token: 0x0600D78B RID: 55179 RVA: 0x002DD0CC File Offset: 0x002DB2CC
		internal override string ToEqualString()
		{
			return string.Concat(new string[]
			{
				base.ToEqualString(),
				" NamespaceName=",
				this.NamespaceName,
				"; ClassName=",
				this.ClassName,
				"; MethodName=",
				this.MethodName,
				";",
				string.Format(" {0}={1};", "Optimizations", this.Optimizations)
			});
		}
	}
}
