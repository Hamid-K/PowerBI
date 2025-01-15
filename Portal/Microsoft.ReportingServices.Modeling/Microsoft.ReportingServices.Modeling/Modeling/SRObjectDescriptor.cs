using System;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x020000BD RID: 189
	internal struct SRObjectDescriptor
	{
		// Token: 0x06000B03 RID: 2819 RVA: 0x00024AC8 File Offset: 0x00022CC8
		public SRObjectDescriptor(string objectType, string objectName)
		{
			this = default(SRObjectDescriptor);
			this.ObjectType = objectType;
			this.ObjectName = objectName;
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000B04 RID: 2820 RVA: 0x00024ADF File Offset: 0x00022CDF
		// (set) Token: 0x06000B05 RID: 2821 RVA: 0x00024AF0 File Offset: 0x00022CF0
		public string ObjectType
		{
			get
			{
				return this.__objectType ?? string.Empty;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new InternalModelingException("SRObjectDescriptor.ObjectType set to null/empty");
				}
				this.__objectType = value;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000B06 RID: 2822 RVA: 0x00024B0C File Offset: 0x00022D0C
		// (set) Token: 0x06000B07 RID: 2823 RVA: 0x00024B1D File Offset: 0x00022D1D
		public string ObjectName
		{
			get
			{
				return this.__objectName ?? string.Empty;
			}
			set
			{
				this.__objectName = value;
			}
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00024B26 File Offset: 0x00022D26
		public override string ToString()
		{
			if (this.ObjectName.Length > 0)
			{
				return SRErrors.ObjectDescriptor_TypeAndName(this.ObjectType, this.ObjectName);
			}
			return this.ObjectType;
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x00024B50 File Offset: 0x00022D50
		internal static SRObjectDescriptor FromScope(IValidationScope scope)
		{
			if (scope == null)
			{
				throw new InternalModelingException("scope is null");
			}
			SRObjectDescriptor srobjectDescriptor = default(SRObjectDescriptor);
			srobjectDescriptor.ObjectType = scope.ObjectType;
			if (scope.ObjectName != null)
			{
				srobjectDescriptor.ObjectName = ((scope.ObjectName.Length > 0) ? scope.ObjectName : scope.ObjectID);
			}
			return srobjectDescriptor;
		}

		// Token: 0x0400048D RID: 1165
		private string __objectType;

		// Token: 0x0400048E RID: 1166
		private string __objectName;
	}
}
