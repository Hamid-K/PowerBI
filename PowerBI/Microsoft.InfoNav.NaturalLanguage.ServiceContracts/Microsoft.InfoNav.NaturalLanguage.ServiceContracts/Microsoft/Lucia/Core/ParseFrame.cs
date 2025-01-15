using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000F4 RID: 244
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class ParseFrame
	{
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x00008AE1 File Offset: 0x00006CE1
		// (set) Token: 0x060004B6 RID: 1206 RVA: 0x00008AE9 File Offset: 0x00006CE9
		[DataMember(IsRequired = true, Order = 10)]
		public int StartIndex { get; set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x00008AF2 File Offset: 0x00006CF2
		// (set) Token: 0x060004B8 RID: 1208 RVA: 0x00008AFA File Offset: 0x00006CFA
		[DataMember(IsRequired = true, Order = 20)]
		public int Length { get; set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x00008B03 File Offset: 0x00006D03
		// (set) Token: 0x060004BA RID: 1210 RVA: 0x00008B0B File Offset: 0x00006D0B
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public ParseFrameType FrameType { get; set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x00008B14 File Offset: 0x00006D14
		// (set) Token: 0x060004BC RID: 1212 RVA: 0x00008B1C File Offset: 0x00006D1C
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 40)]
		public ParseFrameFeature Feature { get; set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x00008B25 File Offset: 0x00006D25
		// (set) Token: 0x060004BE RID: 1214 RVA: 0x00008B2D File Offset: 0x00006D2D
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 50)]
		public DataType DataType { get; set; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x00008B36 File Offset: 0x00006D36
		// (set) Token: 0x060004C0 RID: 1216 RVA: 0x00008B3E File Offset: 0x00006D3E
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 60)]
		public string Entity { get; set; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x00008B47 File Offset: 0x00006D47
		// (set) Token: 0x060004C2 RID: 1218 RVA: 0x00008B4F File Offset: 0x00006D4F
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 65)]
		public CoreEntity CoreEntity { get; set; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00008B58 File Offset: 0x00006D58
		// (set) Token: 0x060004C4 RID: 1220 RVA: 0x00008B60 File Offset: 0x00006D60
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 70)]
		public IList<ParseFrame> Children { get; set; }

		// Token: 0x060004C5 RID: 1221 RVA: 0x00008B69 File Offset: 0x00006D69
		public override string ToString()
		{
			return this.ToString(null);
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x00008B74 File Offset: 0x00006D74
		internal string ToString(string utterance)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[");
			stringBuilder.Append(this.GetLabel());
			if (!this.Children.IsNullOrEmptyCollection<ParseFrame>())
			{
				using (IEnumerator<ParseFrame> enumerator = this.Children.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ParseFrame parseFrame = enumerator.Current;
						stringBuilder.Append(" ");
						stringBuilder.Append(parseFrame.ToString(utterance));
					}
					goto IL_009C;
				}
			}
			if (!string.IsNullOrEmpty(utterance))
			{
				stringBuilder.Append(" ");
				stringBuilder.Append(utterance.Substring(this.StartIndex, this.Length));
			}
			IL_009C:
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00008C40 File Offset: 0x00006E40
		internal string GetLabel()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.FrameType);
			if (this.Feature != ParseFrameFeature.None)
			{
				stringBuilder.Append(":");
				stringBuilder.Append(this.Feature.ToString().Replace(" ", string.Empty));
			}
			if (!string.IsNullOrEmpty(this.Entity))
			{
				stringBuilder.Append(":");
				stringBuilder.Append(this.Entity);
				stringBuilder.Append(":");
				stringBuilder.Append(this.DataType);
			}
			else if (this.DataType != DataType.None)
			{
				stringBuilder.Append(":");
				stringBuilder.Append(this.DataType);
			}
			else if (this.FrameType == ParseFrameType.CoreEntity)
			{
				stringBuilder.Append(":");
				stringBuilder.Append(this.CoreEntity);
			}
			return stringBuilder.ToString();
		}
	}
}
