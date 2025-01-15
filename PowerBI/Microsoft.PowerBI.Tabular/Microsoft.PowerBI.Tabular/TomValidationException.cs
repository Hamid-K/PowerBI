using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200011A RID: 282
	[Serializable]
	public class TomValidationException : TomException
	{
		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06001218 RID: 4632 RVA: 0x0007E9E0 File Offset: 0x0007CBE0
		// (set) Token: 0x06001219 RID: 4633 RVA: 0x0007E9E8 File Offset: 0x0007CBE8
		public ValidationError Error { get; internal set; }

		// Token: 0x0600121A RID: 4634 RVA: 0x0007E9F1 File Offset: 0x0007CBF1
		internal TomValidationException(ValidationError error)
			: base(error.Message)
		{
			this.Error = error;
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x0007EA06 File Offset: 0x0007CC06
		internal TomValidationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.Error = (ValidationError)info.GetValue("Error", typeof(ValidationError));
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x0007EA30 File Offset: 0x0007CC30
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("Error", this.Error, typeof(ValidationError));
			base.GetObjectData(info, context);
		}
	}
}
