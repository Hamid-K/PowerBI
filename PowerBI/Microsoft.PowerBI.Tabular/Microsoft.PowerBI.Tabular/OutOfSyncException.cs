using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000109 RID: 265
	[Serializable]
	public sealed class OutOfSyncException : AmoException
	{
		// Token: 0x06001167 RID: 4455 RVA: 0x0007CBDD File Offset: 0x0007ADDD
		public OutOfSyncException(ObjectReference unknownReference)
			: base((unknownReference == null) ? string.Empty : SR.OutOfSyncException(unknownReference.Serialize()))
		{
			if (unknownReference == null)
			{
				throw new ArgumentNullException("unknownReference");
			}
			this.unknownReference = unknownReference;
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x0007CC0F File Offset: 0x0007AE0F
		private OutOfSyncException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.unknownReference = (ObjectReference)info.GetValue("ObjectReference", typeof(ObjectReference));
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x0007CC39 File Offset: 0x0007AE39
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("ObjectReference", this.unknownReference, typeof(IObjectReference));
			base.GetObjectData(info, context);
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x0600116A RID: 4458 RVA: 0x0007CC6C File Offset: 0x0007AE6C
		public ObjectReference UnknownReference
		{
			get
			{
				return this.unknownReference;
			}
		}

		// Token: 0x040002B3 RID: 691
		private ObjectReference unknownReference;
	}
}
