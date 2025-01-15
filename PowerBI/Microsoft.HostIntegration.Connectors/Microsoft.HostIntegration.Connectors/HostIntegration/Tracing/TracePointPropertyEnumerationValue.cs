using System;

namespace Microsoft.HostIntegration.Tracing
{
	// Token: 0x02000672 RID: 1650
	public class TracePointPropertyEnumerationValue : ITracePointPropertyEnumerationValue
	{
		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x06003784 RID: 14212 RVA: 0x000BAE9D File Offset: 0x000B909D
		public int Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x06003785 RID: 14213 RVA: 0x000BAEA5 File Offset: 0x000B90A5
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06003786 RID: 14214 RVA: 0x000BAEAD File Offset: 0x000B90AD
		public TracePointPropertyEnumerationValue(string enumerationName, int enumerationIdentifier)
		{
			this.name = enumerationName;
			this.identifier = enumerationIdentifier;
		}

		// Token: 0x06003787 RID: 14215 RVA: 0x000BAEA5 File Offset: 0x000B90A5
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x04001FC5 RID: 8133
		private int identifier;

		// Token: 0x04001FC6 RID: 8134
		private string name;
	}
}
