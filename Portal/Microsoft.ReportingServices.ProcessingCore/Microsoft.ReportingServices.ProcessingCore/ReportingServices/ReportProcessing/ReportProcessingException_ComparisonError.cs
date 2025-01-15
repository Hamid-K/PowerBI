using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020005F0 RID: 1520
	[Serializable]
	internal sealed class ReportProcessingException_ComparisonError : Exception, IDataComparisonError
	{
		// Token: 0x06005423 RID: 21539 RVA: 0x00161B27 File Offset: 0x0015FD27
		internal ReportProcessingException_ComparisonError(string typeX, string typeY)
		{
			this.m_typeX = typeX;
			this.m_typeY = typeY;
		}

		// Token: 0x06005424 RID: 21540 RVA: 0x00161B3D File Offset: 0x0015FD3D
		private ReportProcessingException_ComparisonError(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.m_typeX = info.GetString("typex");
			this.m_typeY = info.GetString("typey");
		}

		// Token: 0x06005425 RID: 21541 RVA: 0x00161B69 File Offset: 0x0015FD69
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("typex", this.m_typeX);
			info.AddValue("typex", this.m_typeY);
		}

		// Token: 0x17001EFF RID: 7935
		// (get) Token: 0x06005426 RID: 21542 RVA: 0x00161B95 File Offset: 0x0015FD95
		public string TypeX
		{
			get
			{
				return this.m_typeX;
			}
		}

		// Token: 0x17001F00 RID: 7936
		// (get) Token: 0x06005427 RID: 21543 RVA: 0x00161B9D File Offset: 0x0015FD9D
		public string TypeY
		{
			get
			{
				return this.m_typeY;
			}
		}

		// Token: 0x04002CD5 RID: 11477
		private const string TypeXSerializationID = "typex";

		// Token: 0x04002CD6 RID: 11478
		private const string TypeYSerializationID = "typey";

		// Token: 0x04002CD7 RID: 11479
		private string m_typeX;

		// Token: 0x04002CD8 RID: 11480
		private string m_typeY;
	}
}
