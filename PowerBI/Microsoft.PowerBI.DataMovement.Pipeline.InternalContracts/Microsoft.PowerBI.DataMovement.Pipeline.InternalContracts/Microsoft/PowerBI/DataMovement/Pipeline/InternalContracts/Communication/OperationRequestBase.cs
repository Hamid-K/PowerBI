using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics;

namespace Microsoft.PowerBI.DataMovement.Pipeline.InternalContracts.Communication
{
	// Token: 0x0200005B RID: 91
	[NullableContext(1)]
	[Nullable(0)]
	[DataContract]
	public abstract class OperationRequestBase : OperationDataContract
	{
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x0000302F File Offset: 0x0000122F
		// (set) Token: 0x060001BA RID: 442 RVA: 0x00003037 File Offset: 0x00001237
		[DataMember(Name = "resultStreamingBehaviorV2", IsRequired = false, EmitDefaultValue = false)]
		public ResultStreamingBehavior ResultStreamingBehavior { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00003040 File Offset: 0x00001240
		// (set) Token: 0x060001BC RID: 444 RVA: 0x00003048 File Offset: 0x00001248
		[DataMember(Name = "singleSignOnInformation", IsRequired = false, EmitDefaultValue = false)]
		public SingleSignOnInformation SingleSignOnInformation { get; set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00003051 File Offset: 0x00001251
		// (set) Token: 0x060001BE RID: 446 RVA: 0x00003059 File Offset: 0x00001259
		[DataMember(Name = "evaluationTraceContext", IsRequired = false, EmitDefaultValue = false)]
		public EvaluationTraceContext EvaluationTraceContext { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00003062 File Offset: 0x00001262
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x0000306A File Offset: 0x0000126A
		[DataMember(Name = "queryAdditionalInformation", IsRequired = false, EmitDefaultValue = false)]
		[StringLength(400, ErrorMessage = "The {0} value cannot exceed {1} characters.")]
		public string QueryAdditionalInformation { get; set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00003074 File Offset: 0x00001274
		[IgnoreDataMember]
		internal bool IsQueryRequest
		{
			get
			{
				return this is ExecuteQueryRequest || this is OleDbCreateRowsetRequest || this is GatewayHttpWebRequest || this is GatewayXmlWebRequest;
			}
		}
	}
}
