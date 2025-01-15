using System;

namespace Microsoft.ProgramSynthesis.Utils.Text
{
	// Token: 0x02000525 RID: 1317
	public class TextFieldSetBuilder
	{
		// Token: 0x06001D5B RID: 7515 RVA: 0x000574B8 File Offset: 0x000556B8
		private TextFieldSetBuilder(int minLabelWidth, int minValueWidth, string fieldSeparator, bool omitNullValues, string nullProjection)
		{
			this._omitNullValues = omitNullValues;
			this._nullProjection = nullProjection;
			TextTableBuilder textTableBuilder = new TextTableBuilder(TextTableBorder.None, null, null);
			string text = "";
			int? num = new int?(0);
			TextTableBuilder textTableBuilder2 = textTableBuilder.AddColumn(text, minLabelWidth, null, false, null, null, num, null).AddStaticColumn(fieldSeparator, false, true, false);
			string text2 = "";
			int? num2 = new int?(0);
			num = null;
			int? num3 = num;
			bool flag = false;
			string text3 = null;
			string text4 = null;
			num = null;
			this._table = textTableBuilder2.AddColumn(text2, minValueWidth, num3, flag, text3, text4, num, num2);
		}

		// Token: 0x06001D5C RID: 7516 RVA: 0x00057554 File Offset: 0x00055754
		public TextFieldSetBuilder AddField(string label, bool? value)
		{
			return this.AddField(label, value, null);
		}

		// Token: 0x06001D5D RID: 7517 RVA: 0x00057564 File Offset: 0x00055764
		public TextFieldSetBuilder AddField(string label, int? value, string format = "N0")
		{
			return this.AddField(label, value, format);
		}

		// Token: 0x06001D5E RID: 7518 RVA: 0x00057574 File Offset: 0x00055774
		public TextFieldSetBuilder AddField(string label, uint? value, string format = "N0")
		{
			return this.AddField(label, value, format);
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x00057584 File Offset: 0x00055784
		public TextFieldSetBuilder AddField(string label, double? value, string format = "N3")
		{
			return this.AddField(label, value, format);
		}

		// Token: 0x06001D60 RID: 7520 RVA: 0x00057594 File Offset: 0x00055794
		public TextFieldSetBuilder AddField(string label, object value, string format = null)
		{
			if (value != null)
			{
				if (format == null)
				{
					string text;
					if (!(value is int))
					{
						if (!(value is double))
						{
							text = null;
						}
						else
						{
							text = "N3";
						}
					}
					else
					{
						text = "N0";
					}
					format = text;
				}
				string text2 = ((format == null) ? value.ToString() : string.Format("{0:" + format + "}", value));
				this._table.AddDataRow(new object[] { label, text2 });
				return this;
			}
			if (this._omitNullValues)
			{
				return this;
			}
			this._table.AddDataRow(new object[] { label, this._nullProjection });
			return this;
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x00057636 File Offset: 0x00055836
		public static TextFieldSetBuilder Create(int minLabelWidth = 0, int minValueWidth = 0, string fieldSeparator = ":", bool omitNullValues = false, string nullProjection = "--")
		{
			return new TextFieldSetBuilder(minLabelWidth, minValueWidth, fieldSeparator, omitNullValues, nullProjection);
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x00057643 File Offset: 0x00055843
		public string Render()
		{
			return this._table.Render();
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x00057650 File Offset: 0x00055850
		public override string ToString()
		{
			return this.Render();
		}

		// Token: 0x04000E3C RID: 3644
		private readonly string _nullProjection;

		// Token: 0x04000E3D RID: 3645
		private readonly bool _omitNullValues;

		// Token: 0x04000E3E RID: 3646
		private readonly TextTableBuilder _table;
	}
}
