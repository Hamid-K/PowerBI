using System;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Formula
{
	// Token: 0x020014E8 RID: 5352
	public class ExampleSubstring
	{
		// Token: 0x17001CAF RID: 7343
		// (get) Token: 0x0600A3F6 RID: 41974 RVA: 0x0022B3B7 File Offset: 0x002295B7
		// (set) Token: 0x0600A3F7 RID: 41975 RVA: 0x0022B3BF File Offset: 0x002295BF
		public ExampleInputToken Input { get; set; }

		// Token: 0x17001CB0 RID: 7344
		// (get) Token: 0x0600A3F8 RID: 41976 RVA: 0x0022B3C8 File Offset: 0x002295C8
		// (set) Token: 0x0600A3F9 RID: 41977 RVA: 0x0022B3D0 File Offset: 0x002295D0
		public ExampleToken Output { get; set; }

		// Token: 0x0600A3FA RID: 41978 RVA: 0x0022B3DC File Offset: 0x002295DC
		public JObject ToAnonymizedJson()
		{
			ExampleInputToken input = this.Input;
			string text = ((input != null) ? input.Id : null);
			ExampleInputToken input2 = this.Input;
			int? num = ((input2 != null) ? input2.Start : null);
			ExampleInputToken input3 = this.Input;
			int? num2 = ((input3 != null) ? input3.End : null);
			ExampleInputToken input4 = this.Input;
			bool? flag = ((input4 != null) ? new bool?(input4.IsConstant) : null);
			ExampleInputToken input5 = this.Input;
			string text2;
			if (input5 == null)
			{
				text2 = null;
			}
			else
			{
				string columnName = input5.ColumnName;
				text2 = ((columnName != null) ? columnName.ToAnonymizedString() : null);
			}
			var <>f__AnonymousType = new
			{
				Id = text,
				Start = num,
				End = num2,
				IsConstant = flag,
				ColumnName = text2
			};
			ExampleInputToken input6 = this.Input;
			string text3 = ((input6 != null) ? input6.Id : null);
			ExampleInputToken input7 = this.Input;
			int? num3 = ((input7 != null) ? input7.Start : null);
			ExampleInputToken input8 = this.Input;
			int? num4 = ((input8 != null) ? input8.End : null);
			ExampleInputToken input9 = this.Input;
			return JObject.FromObject(new
			{
				Input = <>f__AnonymousType,
				Output = new
				{
					Id = text3,
					Start = num3,
					End = num4,
					IsConstant = ((input9 != null) ? new bool?(input9.IsConstant) : null)
				}
			});
		}

		// Token: 0x0600A3FB RID: 41979 RVA: 0x0022AAEA File Offset: 0x00228CEA
		public JObject ToJson()
		{
			return JObject.FromObject(this);
		}
	}
}
