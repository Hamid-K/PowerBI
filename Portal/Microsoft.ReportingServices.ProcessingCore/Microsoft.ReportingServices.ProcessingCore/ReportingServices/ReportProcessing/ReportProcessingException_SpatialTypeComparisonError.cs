using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020005F1 RID: 1521
	[Serializable]
	internal sealed class ReportProcessingException_SpatialTypeComparisonError : Exception
	{
		// Token: 0x06005428 RID: 21544 RVA: 0x00161BA5 File Offset: 0x0015FDA5
		internal ReportProcessingException_SpatialTypeComparisonError(string type)
		{
			this.m_type = type;
		}

		// Token: 0x06005429 RID: 21545 RVA: 0x00161BB4 File Offset: 0x0015FDB4
		internal ReportProcessingException_SpatialTypeComparisonError(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.m_type = info.GetString("type");
		}

		// Token: 0x0600542A RID: 21546 RVA: 0x00161BCF File Offset: 0x0015FDCF
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("type", this.m_type);
		}

		// Token: 0x17001F01 RID: 7937
		// (get) Token: 0x0600542B RID: 21547 RVA: 0x00161BEA File Offset: 0x0015FDEA
		internal string Type
		{
			get
			{
				return this.m_type;
			}
		}

		// Token: 0x04002CD9 RID: 11481
		private const string TypeSerializationID = "type";

		// Token: 0x04002CDA RID: 11482
		private string m_type;
	}
}
