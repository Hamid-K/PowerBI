using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.AnalysisServices.Extensions;

namespace Microsoft.AnalysisServices
{
	// Token: 0x020000A8 RID: 168
	[Serializable]
	public sealed class OperationException : AmoException
	{
		// Token: 0x06000839 RID: 2105 RVA: 0x00027BC8 File Offset: 0x00025DC8
		public OperationException(XmlaResultCollection results)
			: base(OperationException.ValidateInputAndBuildMessage(results))
		{
			this.Results = results;
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00027BDD File Offset: 0x00025DDD
		internal OperationException(XmlaResult result)
			: this(new XmlaResultCollection(result))
		{
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x00027BEB File Offset: 0x00025DEB
		internal OperationException(XmlaError error)
			: this(new XmlaResultCollection(new XmlaResult(error)))
		{
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00027BFE File Offset: 0x00025DFE
		internal OperationException(string message, XmlaResultCollection results, string xmlaRequest)
			: base(message)
		{
			this.Results = results;
			this.XmlaRequest = xmlaRequest;
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00027C15 File Offset: 0x00025E15
		private OperationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.Results = (XmlaResultCollection)info.GetValue("Results", typeof(XmlaResultCollection));
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x00027C3F File Offset: 0x00025E3F
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("Results", this.Results, typeof(XmlaResultCollection));
			base.GetObjectData(info, context);
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x00027C72 File Offset: 0x00025E72
		// (set) Token: 0x06000840 RID: 2112 RVA: 0x00027C7A File Offset: 0x00025E7A
		public XmlaResultCollection Results { get; private set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000841 RID: 2113 RVA: 0x00027C83 File Offset: 0x00025E83
		// (set) Token: 0x06000842 RID: 2114 RVA: 0x00027C8B File Offset: 0x00025E8B
		public string XmlaRequest { get; private set; }

		// Token: 0x06000843 RID: 2115 RVA: 0x00027C94 File Offset: 0x00025E94
		private static string ValidateInputAndBuildMessage(XmlaResultCollection results)
		{
			if (results == null)
			{
				throw new ArgumentNullException("results");
			}
			return results.GetAggregatedMessage();
		}
	}
}
