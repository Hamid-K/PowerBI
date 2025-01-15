using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x02000063 RID: 99
	[DataContract]
	public abstract class OperationResultBase : OperationDataContract
	{
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00003184 File Offset: 0x00001384
		[IgnoreDataMember]
		internal bool IsQueryResult
		{
			get
			{
				return this is ExecuteQueryResult || this is OleDbQueryResultBase || this is GatewayHttpWebResult || this is GatewayXmlWebResult;
			}
		}
	}
}
