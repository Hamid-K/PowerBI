using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x020002B0 RID: 688
	internal sealed class EnumSystem : BaseInternalExpression
	{
		// Token: 0x0600154D RID: 5453 RVA: 0x000317B3 File Offset: 0x0002F9B3
		public EnumSystem(Type type, string enumName)
		{
			this._EnumType = type;
			this._Enum = enumName;
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x000317C9 File Offset: 0x0002F9C9
		public override TypeCode TypeCode()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x000317D0 File Offset: 0x0002F9D0
		public override string WriteSource(NameChanges nameChanges)
		{
			return this._EnumType.Name + "." + this._Enum;
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x000317ED File Offset: 0x0002F9ED
		public override object Evaluate()
		{
			return this._EvaluateValue;
		}

		// Token: 0x040006BA RID: 1722
		private readonly string _Enum;

		// Token: 0x040006BB RID: 1723
		private readonly Type _EnumType;

		// Token: 0x040006BC RID: 1724
		private readonly object _EvaluateValue;
	}
}
