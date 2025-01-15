using System;
using System.Data;
using System.Data.Common;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x0200002E RID: 46
	public sealed class SapBwParameter : DbParameter
	{
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000A58C File Offset: 0x0000878C
		// (set) Token: 0x06000245 RID: 581 RVA: 0x0000A594 File Offset: 0x00008794
		public override DbType DbType
		{
			get
			{
				return this.dbType;
			}
			set
			{
				this.dbType = value;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000A59D File Offset: 0x0000879D
		// (set) Token: 0x06000247 RID: 583 RVA: 0x0000A5A5 File Offset: 0x000087A5
		public override ParameterDirection Direction { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000248 RID: 584 RVA: 0x0000A5AE File Offset: 0x000087AE
		// (set) Token: 0x06000249 RID: 585 RVA: 0x0000A5B6 File Offset: 0x000087B6
		public override bool IsNullable { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000A5BF File Offset: 0x000087BF
		// (set) Token: 0x0600024B RID: 587 RVA: 0x0000A5C7 File Offset: 0x000087C7
		public override string ParameterName { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0000A5D0 File Offset: 0x000087D0
		// (set) Token: 0x0600024D RID: 589 RVA: 0x0000A5D8 File Offset: 0x000087D8
		public override int Size { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000A5E1 File Offset: 0x000087E1
		// (set) Token: 0x0600024F RID: 591 RVA: 0x0000A5E9 File Offset: 0x000087E9
		public override string SourceColumn { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000A5F2 File Offset: 0x000087F2
		// (set) Token: 0x06000251 RID: 593 RVA: 0x0000A5FA File Offset: 0x000087FA
		public override bool SourceColumnNullMapping { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000252 RID: 594 RVA: 0x0000A603 File Offset: 0x00008803
		// (set) Token: 0x06000253 RID: 595 RVA: 0x0000A60B File Offset: 0x0000880B
		public override DataRowVersion SourceVersion { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0000A614 File Offset: 0x00008814
		// (set) Token: 0x06000255 RID: 597 RVA: 0x0000A61C File Offset: 0x0000881C
		public override object Value { get; set; }

		// Token: 0x06000256 RID: 598 RVA: 0x0000A625 File Offset: 0x00008825
		public override void ResetDbType()
		{
			this.dbType = DbType.String;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000A630 File Offset: 0x00008830
		public string AsString()
		{
			string text = this.Value as string;
			if (text == null)
			{
				throw new SapBwException(Resources.IncorrectParameterValueType(this.ParameterName, "string"));
			}
			if (text.Length == 0)
			{
				throw new SapBwException(Resources.MissingParameterValue(this.ParameterName));
			}
			return text;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000A684 File Offset: 0x00008884
		public int AsInt()
		{
			long? num = this.Value as long?;
			int? num4;
			if (num != null)
			{
				long? num2 = num;
				long num3 = -2147483648L;
				if ((num2.GetValueOrDefault() >= num3) & (num2 != null))
				{
					num2 = num;
					num3 = 2147483647L;
					if ((num2.GetValueOrDefault() <= num3) & (num2 != null))
					{
						num4 = new int?(Convert.ToInt32(num.Value));
						goto IL_00BF;
					}
				}
				throw new SapBwException(Resources.IncorrectParameterValueType(this.ParameterName, "int"));
			}
			num4 = this.Value as int?;
			if (num4 == null)
			{
				throw new SapBwException(Resources.IncorrectParameterValueType(this.ParameterName, "int"));
			}
			IL_00BF:
			return num4.Value;
		}

		// Token: 0x040001A5 RID: 421
		private DbType dbType = DbType.String;
	}
}
