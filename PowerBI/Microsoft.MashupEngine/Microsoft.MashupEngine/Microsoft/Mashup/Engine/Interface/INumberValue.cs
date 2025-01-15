using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000F5 RID: 245
	public interface INumberValue : IValue
	{
		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060003B3 RID: 947
		bool IsInteger32 { get; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060003B4 RID: 948
		bool IsDouble { get; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060003B5 RID: 949
		bool IsDecimal { get; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060003B6 RID: 950
		bool IsInteger64 { get; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060003B7 RID: 951
		int AsInteger32 { get; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060003B8 RID: 952
		double AsDouble { get; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060003B9 RID: 953
		decimal AsDecimal { get; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060003BA RID: 954
		long AsInteger64 { get; }

		// Token: 0x060003BB RID: 955
		IValue RoundToEven(IValue digits);

		// Token: 0x060003BC RID: 956
		IValue Negate();

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060003BD RID: 957
		bool IsNaN { get; }

		// Token: 0x060003BE RID: 958
		string ToString(string format, IFormatProvider provider);

		// Token: 0x060003BF RID: 959
		object ToObject();
	}
}
