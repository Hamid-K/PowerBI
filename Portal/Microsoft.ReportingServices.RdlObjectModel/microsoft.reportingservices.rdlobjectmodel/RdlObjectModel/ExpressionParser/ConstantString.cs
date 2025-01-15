using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000270 RID: 624
	[Serializable]
	internal sealed class ConstantString : Constant
	{
		// Token: 0x060013EC RID: 5100 RVA: 0x0002F698 File Offset: 0x0002D898
		public ConstantString(string value)
			: base(value)
		{
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x0002F6A1 File Offset: 0x0002D8A1
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.String;
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x0002F6A5 File Offset: 0x0002D8A5
		public override string WriteSource(NameChanges nameChanges)
		{
			string text = "\"";
			object obj = base.Evaluate();
			return text + ((obj != null) ? obj.ToString() : null) + "\"";
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x060013EF RID: 5103 RVA: 0x0002F6C8 File Offset: 0x0002D8C8
		public string ConstantValue
		{
			get
			{
				return (string)this.valueConst;
			}
		}
	}
}
