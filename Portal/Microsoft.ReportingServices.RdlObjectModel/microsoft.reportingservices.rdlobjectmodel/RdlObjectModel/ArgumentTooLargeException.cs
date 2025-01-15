using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001BF RID: 447
	[Serializable]
	public class ArgumentTooLargeException : ArgumentConstraintException
	{
		// Token: 0x06000E9E RID: 3742 RVA: 0x00023DA5 File Offset: 0x00021FA5
		public ArgumentTooLargeException(object component, string property, object value, object maximum)
			: base(component, property, value, maximum, SRErrorsWrapper.InvalidParamLessThan(property, maximum))
		{
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x00023DBA File Offset: 0x00021FBA
		protected ArgumentTooLargeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
