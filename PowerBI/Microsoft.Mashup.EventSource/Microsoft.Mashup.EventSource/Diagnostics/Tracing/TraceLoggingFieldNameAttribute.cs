using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000025 RID: 37
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
	internal sealed class TraceLoggingFieldNameAttribute : Attribute
	{
		// Token: 0x06000150 RID: 336 RVA: 0x0000ACEC File Offset: 0x00008EEC
		public TraceLoggingFieldNameAttribute(string fieldName)
		{
			this.fieldName = fieldName;
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000ACFB File Offset: 0x00008EFB
		public string FieldName
		{
			get
			{
				return this.fieldName;
			}
		}

		// Token: 0x040000AE RID: 174
		private readonly string fieldName;
	}
}
