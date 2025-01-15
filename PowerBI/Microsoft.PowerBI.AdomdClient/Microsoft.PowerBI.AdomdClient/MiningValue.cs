using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000D7 RID: 215
	public sealed class MiningValue
	{
		// Token: 0x06000C04 RID: 3076 RVA: 0x0002DE0F File Offset: 0x0002C00F
		internal MiningValue()
		{
			this.valueType = MiningValueType.PreRenderedString;
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x0002DE1E File Offset: 0x0002C01E
		internal MiningValue(MiningValueType valueType, int index, object objValue)
		{
			this.valueType = valueType;
			this.index = index;
			this.objValue = objValue;
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x0002DE3B File Offset: 0x0002C03B
		public override string ToString()
		{
			if (this.objValue != null)
			{
				return this.objValue.ToString();
			}
			return string.Empty;
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x0002DE56 File Offset: 0x0002C056
		public MiningValueType ValueType
		{
			get
			{
				return this.valueType;
			}
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000C08 RID: 3080 RVA: 0x0002DE5E File Offset: 0x0002C05E
		public object Value
		{
			get
			{
				return this.objValue;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06000C09 RID: 3081 RVA: 0x0002DE66 File Offset: 0x0002C066
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x040007BA RID: 1978
		private MiningValueType valueType;

		// Token: 0x040007BB RID: 1979
		private object objValue;

		// Token: 0x040007BC RID: 1980
		private int index;
	}
}
