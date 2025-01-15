using System;
using System.Globalization;

namespace Microsoft.AnalysisServices.Tabular.Tmdl
{
	// Token: 0x02000146 RID: 326
	internal sealed class TmdlScalarValue<T> : TmdlValue where T : struct
	{
		// Token: 0x06001533 RID: 5427 RVA: 0x0008F035 File Offset: 0x0008D235
		internal TmdlScalarValue(string rawValue, T? value)
			: base(TmdlValueType.Scalar, rawValue, false, true)
		{
			this.value = value;
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001534 RID: 5428 RVA: 0x0008F048 File Offset: 0x0008D248
		public override object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x0008F055 File Offset: 0x0008D255
		public T? GetValue()
		{
			return this.value;
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x0008F05D File Offset: 0x0008D25D
		public override TypeCode GetTypeCode()
		{
			return global::System.Type.GetTypeCode(typeof(T));
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x0008F070 File Offset: 0x0008D270
		private protected override void WriteValue(ITmdlWriter writer)
		{
			if (this.value != null)
			{
				T t = this.value.Value;
				string text;
				if (t is bool)
				{
					text = (t as bool).ToString().ToLowerInvariant();
				}
				else
				{
					t = this.value.Value;
					if (t is int)
					{
						text = (t as int).ToString("G");
					}
					else
					{
						t = this.value.Value;
						if (t is long)
						{
							text = (t as long).ToString("G");
						}
						else
						{
							t = this.value.Value;
							if (t is double)
							{
								text = (t as double).ToString("G");
							}
							else
							{
								t = this.value.Value;
								if (t is DateTime)
								{
									text = (t as DateTime).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
								}
								else
								{
									t = this.value.Value;
									text = t.ToString();
								}
							}
						}
					}
				}
				writer.Write(text, Array.Empty<object>());
			}
		}

		// Token: 0x040003A0 RID: 928
		private readonly T? value;
	}
}
