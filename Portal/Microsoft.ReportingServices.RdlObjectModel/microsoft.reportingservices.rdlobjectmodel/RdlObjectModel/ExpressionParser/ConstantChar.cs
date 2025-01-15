using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x02000267 RID: 615
	[Serializable]
	internal sealed class ConstantChar : Constant
	{
		// Token: 0x060013D5 RID: 5077 RVA: 0x0002F548 File Offset: 0x0002D748
		public ConstantChar(string value)
			: base(char.Parse(value))
		{
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x0002F55B File Offset: 0x0002D75B
		public override TypeCode TypeCode()
		{
			return global::System.TypeCode.Char;
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x0002F55E File Offset: 0x0002D75E
		public override string WriteSource(NameChanges nameChanges)
		{
			string text = "\"";
			object obj = base.Evaluate();
			return text + ((obj != null) ? obj.ToString() : null) + "\"C";
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x060013D8 RID: 5080 RVA: 0x0002F581 File Offset: 0x0002D781
		public char ConstantValue
		{
			get
			{
				return (char)this.valueConst;
			}
		}
	}
}
