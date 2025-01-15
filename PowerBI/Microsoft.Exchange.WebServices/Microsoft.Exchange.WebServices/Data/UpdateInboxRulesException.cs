using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200026A RID: 618
	[Serializable]
	public sealed class UpdateInboxRulesException : ServiceRemoteException
	{
		// Token: 0x060015D3 RID: 5587 RVA: 0x0003CFD0 File Offset: 0x0003BFD0
		internal UpdateInboxRulesException(UpdateInboxRulesResponse serviceResponse, IEnumerator<RuleOperation> ruleOperations)
		{
			this.serviceResponse = serviceResponse;
			this.errors = serviceResponse.Errors;
			foreach (RuleOperationError ruleOperationError in this.errors)
			{
				ruleOperationError.SetOperationByIndex(ruleOperations);
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x060015D4 RID: 5588 RVA: 0x0003D038 File Offset: 0x0003C038
		public ServiceResponse ServiceResponse
		{
			get
			{
				return this.serviceResponse;
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x060015D5 RID: 5589 RVA: 0x0003D040 File Offset: 0x0003C040
		public RuleOperationErrorCollection Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x060015D6 RID: 5590 RVA: 0x0003D048 File Offset: 0x0003C048
		public ServiceError ErrorCode
		{
			get
			{
				return this.serviceResponse.ErrorCode;
			}
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060015D7 RID: 5591 RVA: 0x0003D055 File Offset: 0x0003C055
		public string ErrorMessage
		{
			get
			{
				return this.serviceResponse.ErrorMessage;
			}
		}

		// Token: 0x040012BB RID: 4795
		private ServiceResponse serviceResponse;

		// Token: 0x040012BC RID: 4796
		private RuleOperationErrorCollection errors;
	}
}
