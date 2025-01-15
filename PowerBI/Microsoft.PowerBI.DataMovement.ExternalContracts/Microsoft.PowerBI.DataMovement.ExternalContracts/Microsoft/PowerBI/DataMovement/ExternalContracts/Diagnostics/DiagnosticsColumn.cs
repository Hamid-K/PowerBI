using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.Diagnostics
{
	// Token: 0x0200001D RID: 29
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	public class DiagnosticsColumn
	{
		// Token: 0x0600009E RID: 158 RVA: 0x00002CD2 File Offset: 0x00000ED2
		public DiagnosticsColumn(string name, Type dataType)
		{
			this.Name = name;
			this.DataType = dataType;
		}

		// Token: 0x04000091 RID: 145
		[DataMember(Name = "name", Order = 0)]
		public readonly string Name;

		// Token: 0x04000092 RID: 146
		[DataMember(Name = "type", Order = 10)]
		public readonly Type DataType;
	}
}
