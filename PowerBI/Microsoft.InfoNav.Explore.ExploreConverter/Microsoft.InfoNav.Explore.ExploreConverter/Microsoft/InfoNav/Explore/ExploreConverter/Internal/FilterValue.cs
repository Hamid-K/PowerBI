using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000017 RID: 23
	[DataContract]
	internal sealed class FilterValue
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00003823 File Offset: 0x00001A23
		internal FilterValue()
		{
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000382C File Offset: 0x00001A2C
		internal FilterValue(PrimitiveValue value)
		{
			switch (value.Type)
			{
			case ConceptualPrimitiveType.Null:
				this.Type = FilterValueType.Null;
				return;
			case ConceptualPrimitiveType.Text:
			{
				string text = (string)value.GetValueAsObject();
				this.Type = FilterValueType.String;
				this.StringValue = text;
				if (!string.IsNullOrEmpty(text))
				{
					if (text[0] == '\'')
					{
						this.StringValue = text.Substring(1, text.Length - 2);
						return;
					}
					if (text[text.Length - 1] == 'M' || text[text.Length - 1] == 'D' || text[text.Length - 1] == 'L')
					{
						this.Type = FilterValueType.NumberODataEncoded;
						double num;
						if (double.TryParse(text.Substring(0, text.Length - 1), out num))
						{
							this.NumberODataEncodedValue = text;
							return;
						}
						if (text == "INFD")
						{
							this.NumberODataEncodedValue = double.PositiveInfinity.ToString() + "D";
							return;
						}
						if (text == "-INFD")
						{
							this.NumberODataEncodedValue = double.NegativeInfinity.ToString() + "D";
							return;
						}
					}
				}
				return;
			}
			case ConceptualPrimitiveType.Decimal:
			case ConceptualPrimitiveType.Double:
			case ConceptualPrimitiveType.Integer:
				this.NumberValue = Convert.ToDecimal(value.GetValueAsObject(), CultureInfo.InvariantCulture);
				this.Type = FilterValueType.Number;
				return;
			case ConceptualPrimitiveType.Boolean:
				this.BoolValue = (bool)value.GetValueAsObject();
				this.Type = FilterValueType.Bool;
				return;
			case ConceptualPrimitiveType.DateTime:
				this.DateTimeValue = (DateTime)value.GetValueAsObject();
				this.Type = FilterValueType.DateTime;
				return;
			}
			throw new InvalidOperationException("Unsupported filter value");
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000039D8 File Offset: 0x00001BD8
		// (set) Token: 0x0600006D RID: 109 RVA: 0x000039E0 File Offset: 0x00001BE0
		public FilterValueType Type { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000039E9 File Offset: 0x00001BE9
		// (set) Token: 0x0600006F RID: 111 RVA: 0x000039F1 File Offset: 0x00001BF1
		public string StringValue { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000039FA File Offset: 0x00001BFA
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00003A02 File Offset: 0x00001C02
		public decimal NumberValue { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003A0B File Offset: 0x00001C0B
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00003A13 File Offset: 0x00001C13
		public string NumberODataEncodedValue { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003A1C File Offset: 0x00001C1C
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00003A24 File Offset: 0x00001C24
		public bool BoolValue { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003A2D File Offset: 0x00001C2D
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00003A35 File Offset: 0x00001C35
		public DateTime DateTimeValue { get; set; }
	}
}
