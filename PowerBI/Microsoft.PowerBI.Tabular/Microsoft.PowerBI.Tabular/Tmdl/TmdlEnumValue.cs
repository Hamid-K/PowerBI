using System;

namespace Microsoft.AnalysisServices.Tabular.Tmdl
{
	// Token: 0x02000139 RID: 313
	internal sealed class TmdlEnumValue : TmdlValue
	{
		// Token: 0x060014D0 RID: 5328 RVA: 0x0008C8E5 File Offset: 0x0008AAE5
		internal TmdlEnumValue(string rawValue, Enum value)
			: base(TmdlValueType.Scalar, rawValue, false, true)
		{
			this.value = value;
			this.EnumType = value.GetType();
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x060014D1 RID: 5329 RVA: 0x0008C904 File Offset: 0x0008AB04
		public override object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x060014D2 RID: 5330 RVA: 0x0008C90C File Offset: 0x0008AB0C
		public Type EnumType { get; }

		// Token: 0x060014D3 RID: 5331 RVA: 0x0008C914 File Offset: 0x0008AB14
		private protected override void WriteValue(ITmdlWriter writer)
		{
			writer.Write(writer.FormatKeyword(this.value.ToString("G")), Array.Empty<object>());
		}

		// Token: 0x04000368 RID: 872
		private readonly Enum value;
	}
}
