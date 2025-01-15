using System;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006A4 RID: 1700
	[Serializable]
	internal sealed class DrillthroughParameters : NameObjectCollectionBase, INameObjectCollection
	{
		// Token: 0x06005C7E RID: 23678 RVA: 0x001799C9 File Offset: 0x00177BC9
		public DrillthroughParameters()
		{
		}

		// Token: 0x06005C7F RID: 23679 RVA: 0x001799D1 File Offset: 0x00177BD1
		internal DrillthroughParameters(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x06005C80 RID: 23680 RVA: 0x001799DA File Offset: 0x00177BDA
		internal DrillthroughParameters(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06005C81 RID: 23681 RVA: 0x001799E4 File Offset: 0x00177BE4
		public void Add(string key, object value)
		{
			base.BaseAdd(key, value);
		}

		// Token: 0x06005C82 RID: 23682 RVA: 0x001799EE File Offset: 0x00177BEE
		public string GetKey(int index)
		{
			return base.BaseGetKey(index);
		}

		// Token: 0x06005C83 RID: 23683 RVA: 0x001799F7 File Offset: 0x00177BF7
		public object GetValue(int index)
		{
			return base.BaseGet(index);
		}
	}
}
