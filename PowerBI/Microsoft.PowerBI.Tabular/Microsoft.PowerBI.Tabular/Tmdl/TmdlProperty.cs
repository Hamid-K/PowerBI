using System;
using System.Diagnostics;
using Microsoft.AnalysisServices.Tabular.Extensions;

namespace Microsoft.AnalysisServices.Tabular.Tmdl
{
	// Token: 0x02000144 RID: 324
	[DebuggerDisplay("TmdlProperty - name={Name}")]
	internal sealed class TmdlProperty
	{
		// Token: 0x0600152A RID: 5418 RVA: 0x0008EE0C File Offset: 0x0008D00C
		public TmdlProperty(string name, TmdlValue value)
		{
			this.Name = name;
			this.Value = value;
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x0600152B RID: 5419 RVA: 0x0008EE22 File Offset: 0x0008D022
		// (set) Token: 0x0600152C RID: 5420 RVA: 0x0008EE2A File Offset: 0x0008D02A
		public TmdlSourceLocation SourceLocation { get; internal set; }

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x0600152D RID: 5421 RVA: 0x0008EE33 File Offset: 0x0008D033
		public string Name { get; }

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x0600152E RID: 5422 RVA: 0x0008EE3B File Offset: 0x0008D03B
		public TmdlValue Value { get; }

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x0600152F RID: 5423 RVA: 0x0008EE43 File Offset: 0x0008D043
		// (set) Token: 0x06001530 RID: 5424 RVA: 0x0008EE4B File Offset: 0x0008D04B
		public bool DoNotSerialize { get; set; }

		// Token: 0x06001531 RID: 5425 RVA: 0x0008EE54 File Offset: 0x0008D054
		public void WriteTo(ITmdlWriter writer)
		{
			bool flag = false;
			int? num = null;
			TmdlStringValue tmdlStringValue = this.Value as TmdlStringValue;
			if (tmdlStringValue != null && tmdlStringValue.UseExpressionSemantics)
			{
				if (tmdlStringValue.ContainsExpressionThatRequiresQuotes)
				{
					writer.Write("{0} = {1}", new object[]
					{
						writer.FormatKeyword(this.Name),
						"```"
					});
					num = new int?(1);
					flag = true;
				}
				else if (tmdlStringValue.HasBody)
				{
					writer.Write("{0} =", new object[] { writer.FormatKeyword(this.Name) });
					num = new int?(1);
				}
				else
				{
					writer.Write("{0} = ", new object[] { writer.FormatKeyword(this.Name) });
				}
			}
			else if (this.Value.HasBody)
			{
				writer.Write("{0}", new object[] { writer.FormatKeyword(this.Name) });
				num = new int?(1);
			}
			else
			{
				TmdlScalarValue<bool> tmdlScalarValue = this.Value as TmdlScalarValue<bool>;
				if (tmdlScalarValue != null && tmdlScalarValue.GetValue() == null)
				{
					writer.Write("{0}", new object[] { writer.FormatKeyword(this.Name) });
				}
				else
				{
					writer.Write("{0}: ", new object[] { writer.FormatKeyword(this.Name) });
				}
			}
			this.Value.WriteTo(writer, num);
			if (flag)
			{
				writer.WriteLineWithAdditionalIndentation(num.Value, "```", Array.Empty<object>());
			}
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x0008EFE4 File Offset: 0x0008D1E4
		internal TValue ValueAs<TValue>() where TValue : TmdlValue
		{
			TValue tvalue = this.Value as TValue;
			if (tvalue != null)
			{
				return tvalue;
			}
			throw new InvalidCastException(TomSR.Exception_TmdlInvalidValueCast(this.Value.GetType().Name, typeof(TValue).Name));
		}
	}
}
