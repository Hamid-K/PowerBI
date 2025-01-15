using System;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Pipeline
{
	// Token: 0x020019A5 RID: 6565
	public class FindModel : PipelineModel
	{
		// Token: 0x170023BA RID: 9146
		// (get) Token: 0x0600D69E RID: 54942 RVA: 0x002DA2A0 File Offset: 0x002D84A0
		// (set) Token: 0x0600D69F RID: 54943 RVA: 0x002DA2A8 File Offset: 0x002D84A8
		public string Delimiter { get; set; }

		// Token: 0x170023BB RID: 9147
		// (get) Token: 0x0600D6A0 RID: 54944 RVA: 0x002DA2B1 File Offset: 0x002D84B1
		// (set) Token: 0x0600D6A1 RID: 54945 RVA: 0x002DA2B9 File Offset: 0x002D84B9
		public int Instance { get; set; }

		// Token: 0x170023BC RID: 9148
		// (get) Token: 0x0600D6A2 RID: 54946 RVA: 0x002DA2C2 File Offset: 0x002D84C2
		// (set) Token: 0x0600D6A3 RID: 54947 RVA: 0x002DA2CA File Offset: 0x002D84CA
		public int Offset { get; set; }

		// Token: 0x0600D6A4 RID: 54948 RVA: 0x002DA2D4 File Offset: 0x002D84D4
		public override string ToOperatorString()
		{
			string text = ((this.Offset < 0) ? string.Format(" - {0}", -this.Offset) : ((this.Offset > 0) ? string.Format(" + {0}", this.Offset) : string.Empty));
			return string.Format("Find({0})[{1}]{2}", this.Delimiter.ToCSharpPseudoLiteral(), this.Instance, text);
		}
	}
}
