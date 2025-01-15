using System;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000083 RID: 131
	internal sealed class RdmQueryProjectExpression : IRdmQueryExpression
	{
		// Token: 0x06000289 RID: 649 RVA: 0x0000C58D File Offset: 0x0000A78D
		internal RdmQueryProjectExpression(IRdmQueryExpression inputExpression, string inputVariableName, IRdmQueryExpression projection)
		{
			this._inputExpression = inputExpression;
			this._inputVariableName = inputVariableName;
			this._projection = projection;
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000C5AA File Offset: 0x0000A7AA
		internal IRdmQueryExpression InputExpression
		{
			get
			{
				return this._inputExpression;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000C5B2 File Offset: 0x0000A7B2
		internal string InputVariableName
		{
			get
			{
				return this._inputVariableName;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000C5BA File Offset: 0x0000A7BA
		internal IRdmQueryExpression Projection
		{
			get
			{
				return this._projection;
			}
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000C5C2 File Offset: 0x0000A7C2
		public void FindFormulaComponents(FormulaParserContext context)
		{
			if (this._inputExpression != null)
			{
				this._inputExpression.FindFormulaComponents(context);
			}
			if (this._projection != null)
			{
				this._projection.FindFormulaComponents(context);
			}
		}

		// Token: 0x0400019F RID: 415
		private readonly IRdmQueryExpression _inputExpression;

		// Token: 0x040001A0 RID: 416
		private readonly string _inputVariableName;

		// Token: 0x040001A1 RID: 417
		private readonly IRdmQueryExpression _projection;
	}
}
