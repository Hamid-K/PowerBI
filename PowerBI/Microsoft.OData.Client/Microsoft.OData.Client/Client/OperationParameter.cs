using System;

namespace Microsoft.OData.Client
{
	// Token: 0x0200005B RID: 91
	public abstract class OperationParameter
	{
		// Token: 0x060002DD RID: 733 RVA: 0x0000B02A File Offset: 0x0000922A
		protected OperationParameter(string name, object value)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException(Strings.Context_MissingOperationParameterName);
			}
			this.parameterName = name;
			this.parameterValue = value;
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002DE RID: 734 RVA: 0x0000B053 File Offset: 0x00009253
		public string Name
		{
			get
			{
				return this.parameterName;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000B05B File Offset: 0x0000925B
		public object Value
		{
			get
			{
				return this.parameterValue;
			}
		}

		// Token: 0x040000F3 RID: 243
		private readonly string parameterName;

		// Token: 0x040000F4 RID: 244
		private readonly object parameterValue;
	}
}
