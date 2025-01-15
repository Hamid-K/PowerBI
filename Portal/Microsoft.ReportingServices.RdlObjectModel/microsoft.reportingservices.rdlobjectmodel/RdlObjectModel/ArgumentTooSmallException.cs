using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001C0 RID: 448
	[Serializable]
	public class ArgumentTooSmallException : ArgumentConstraintException
	{
		// Token: 0x06000EA0 RID: 3744 RVA: 0x00023DC4 File Offset: 0x00021FC4
		public ArgumentTooSmallException(object component, string property, object value, object minimum)
			: base(component, property, value, minimum, SRErrorsWrapper.InvalidParamGreaterThan(property, minimum))
		{
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x00023DD9 File Offset: 0x00021FD9
		protected ArgumentTooSmallException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
