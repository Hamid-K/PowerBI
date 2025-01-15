using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;

namespace Microsoft.Mashup.Engine1.DataSourceReference
{
	// Token: 0x020018BB RID: 6331
	public class FormulaCreationResult : IFormulaCreationResult
	{
		// Token: 0x0600A141 RID: 41281 RVA: 0x002173BC File Offset: 0x002155BC
		public FormulaCreationResult(DataSourceReferenceReaderFailureReason failureReason, string errorMessage = null)
			: this(false, null, failureReason)
		{
			this.ErrorMessage = errorMessage;
		}

		// Token: 0x0600A142 RID: 41282 RVA: 0x002173CE File Offset: 0x002155CE
		public FormulaCreationResult(IExpression formula)
			: this(true, ExpressionBuilder.Instance.ToSource(formula), DataSourceReferenceReaderFailureReason.None)
		{
			this.formulaExpression = formula;
		}

		// Token: 0x0600A143 RID: 41283 RVA: 0x002173EA File Offset: 0x002155EA
		private FormulaCreationResult(bool success, string formula, DataSourceReferenceReaderFailureReason failureReason)
		{
			this.Success = success;
			this.Formula = formula;
			this.FailureReason = failureReason;
		}

		// Token: 0x0600A144 RID: 41284 RVA: 0x00217407 File Offset: 0x00215607
		public static FormulaCreationResult FromText(string formula)
		{
			return new FormulaCreationResult(true, formula, DataSourceReferenceReaderFailureReason.None);
		}

		// Token: 0x1700293F RID: 10559
		// (get) Token: 0x0600A145 RID: 41285 RVA: 0x00217411 File Offset: 0x00215611
		public IExpression FormulaExpression
		{
			get
			{
				if (this.formulaExpression == null)
				{
					throw new InvalidOperationException("no formula expression available");
				}
				return this.formulaExpression;
			}
		}

		// Token: 0x17002940 RID: 10560
		// (get) Token: 0x0600A146 RID: 41286 RVA: 0x0021742C File Offset: 0x0021562C
		// (set) Token: 0x0600A147 RID: 41287 RVA: 0x00217434 File Offset: 0x00215634
		public bool Success { get; private set; }

		// Token: 0x17002941 RID: 10561
		// (get) Token: 0x0600A148 RID: 41288 RVA: 0x0021743D File Offset: 0x0021563D
		// (set) Token: 0x0600A149 RID: 41289 RVA: 0x00217445 File Offset: 0x00215645
		public string Formula { get; private set; }

		// Token: 0x17002942 RID: 10562
		// (get) Token: 0x0600A14A RID: 41290 RVA: 0x0021744E File Offset: 0x0021564E
		// (set) Token: 0x0600A14B RID: 41291 RVA: 0x00217456 File Offset: 0x00215656
		public DataSourceReferenceReaderFailureReason FailureReason { get; private set; }

		// Token: 0x17002943 RID: 10563
		// (get) Token: 0x0600A14C RID: 41292 RVA: 0x0021745F File Offset: 0x0021565F
		// (set) Token: 0x0600A14D RID: 41293 RVA: 0x00217467 File Offset: 0x00215667
		public string ErrorMessage { get; private set; }

		// Token: 0x0400547E RID: 21630
		private readonly IExpression formulaExpression;
	}
}
