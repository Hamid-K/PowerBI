using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000D7 RID: 215
	public sealed class MiningValue
	{
		// Token: 0x06000C11 RID: 3089 RVA: 0x0002E13F File Offset: 0x0002C33F
		internal MiningValue()
		{
			this.valueType = MiningValueType.PreRenderedString;
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0002E14E File Offset: 0x0002C34E
		internal MiningValue(MiningValueType valueType, int index, object objValue)
		{
			this.valueType = valueType;
			this.index = index;
			this.objValue = objValue;
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x0002E16B File Offset: 0x0002C36B
		public override string ToString()
		{
			if (this.objValue != null)
			{
				return this.objValue.ToString();
			}
			return string.Empty;
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x0002E186 File Offset: 0x0002C386
		public MiningValueType ValueType
		{
			get
			{
				return this.valueType;
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x0002E18E File Offset: 0x0002C38E
		public object Value
		{
			get
			{
				return this.objValue;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x0002E196 File Offset: 0x0002C396
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x040007C7 RID: 1991
		private MiningValueType valueType;

		// Token: 0x040007C8 RID: 1992
		private object objValue;

		// Token: 0x040007C9 RID: 1993
		private int index;
	}
}
