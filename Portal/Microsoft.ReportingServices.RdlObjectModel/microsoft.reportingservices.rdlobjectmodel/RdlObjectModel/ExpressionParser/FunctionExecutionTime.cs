using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200027D RID: 637
	[Serializable]
	internal sealed class FunctionExecutionTime : BaseInternalExpression
	{
		// Token: 0x06001425 RID: 5157 RVA: 0x0002FB6E File Offset: 0x0002DD6E
		public FunctionExecutionTime()
		{
			this.StartReport = DateTime.MinValue;
		}

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06001426 RID: 5158 RVA: 0x0002FB81 File Offset: 0x0002DD81
		// (set) Token: 0x06001427 RID: 5159 RVA: 0x0002FB89 File Offset: 0x0002DD89
		internal DateTime StartReport
		{
			get
			{
				return this._StartReport;
			}
			set
			{
				this._StartReport = value;
			}
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x0002FB92 File Offset: 0x0002DD92
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.DateTime;
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x0002FB96 File Offset: 0x0002DD96
		public override string WriteSource(NameChanges nameChanges)
		{
			return "Globals!ExecutionTime";
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x0002FB9D File Offset: 0x0002DD9D
		public override object Evaluate()
		{
			return DateTime.Now;
		}

		// Token: 0x040006A0 RID: 1696
		[NonSerialized]
		private DateTime _StartReport;
	}
}
