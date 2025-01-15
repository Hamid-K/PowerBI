using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Runtime.Serialization;

namespace System.Data.Entity.ModelConfiguration
{
	// Token: 0x02000157 RID: 343
	[Serializable]
	public class ModelValidationException : Exception
	{
		// Token: 0x060015ED RID: 5613 RVA: 0x00038D18 File Offset: 0x00036F18
		public ModelValidationException()
		{
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x00038D20 File Offset: 0x00036F20
		public ModelValidationException(string message)
			: base(message)
		{
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x00038D29 File Offset: 0x00036F29
		public ModelValidationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x00038D33 File Offset: 0x00036F33
		internal ModelValidationException(IEnumerable<DataModelErrorEventArgs> validationErrors)
			: base(validationErrors.ToErrorMessage())
		{
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x00038D41 File Offset: 0x00036F41
		protected ModelValidationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
