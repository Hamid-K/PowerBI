using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000282 RID: 642
	[Serializable]
	internal sealed class FunctionPageName : BaseInternalExpression
	{
		// Token: 0x06001444 RID: 5188 RVA: 0x0002FDFC File Offset: 0x0002DFFC
		public FunctionPageName()
		{
			this.Name = "";
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x0002FE0F File Offset: 0x0002E00F
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.String;
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x0002FE13 File Offset: 0x0002E013
		public override bool IsConstant()
		{
			return true;
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x0002FE16 File Offset: 0x0002E016
		public override string WriteSource(NameChanges nameChanges)
		{
			return "Globals!PageName";
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x0002FE1D File Offset: 0x0002E01D
		public override object Evaluate()
		{
			return "";
		}

		// Token: 0x040006A7 RID: 1703
		internal string Name;
	}
}
