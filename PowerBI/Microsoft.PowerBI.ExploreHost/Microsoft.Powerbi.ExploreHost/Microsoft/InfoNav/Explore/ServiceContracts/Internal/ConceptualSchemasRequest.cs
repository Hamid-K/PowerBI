using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Explore.ServiceContracts.Internal
{
	// Token: 0x0200000D RID: 13
	[DataContract]
	public sealed class ConceptualSchemasRequest : IValidatableObject
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000022F7 File Offset: 0x000004F7
		// (set) Token: 0x0600002B RID: 43 RVA: 0x000022FF File Offset: 0x000004FF
		[DataMember(IsRequired = false, Order = 0, Name = "modelIds")]
		public IList<long> ModelIds { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002308 File Offset: 0x00000508
		// (set) Token: 0x0600002D RID: 45 RVA: 0x00002310 File Offset: 0x00000510
		[DataMember(IsRequired = false, Order = 1, Name = "userPreferredLocale")]
		public string UserPreferredLocale { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002319 File Offset: 0x00000519
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002321 File Offset: 0x00000521
		[DataMember(IsRequired = false, Order = 2, Name = "modelObjectIds")]
		public IList<string> ModelObjectIds { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000030 RID: 48 RVA: 0x0000232A File Offset: 0x0000052A
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002332 File Offset: 0x00000532
		[DataMember(IsRequired = false, Order = 3, Name = "perspectiveIds")]
		public IList<string> PerspectiveIds { get; set; }

		// Token: 0x06000032 RID: 50 RVA: 0x0000233C File Offset: 0x0000053C
		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			int num = ((this.ModelIds == null) ? 0 : this.ModelIds.Count);
			int num2 = ((this.ModelObjectIds == null) ? 0 : this.ModelObjectIds.Count);
			int num3 = ((this.PerspectiveIds == null) ? 0 : this.PerspectiveIds.Count);
			if (num + num2 == 0)
			{
				return new ValidationResult[]
				{
					new ValidationResult("ValidationFailed: Both ModelIds and ModelObjectIds are empty")
				};
			}
			if (num > 100 || num2 > 100)
			{
				string text = "ValidationFailed: {0} acceded the max number of elements which is 100";
				return new ValidationResult[] { (num > 100) ? new ValidationResult(StringUtil.FormatInvariant(text, "ModelIds")) : new ValidationResult(StringUtil.FormatInvariant(text, "ModelObjectIds")) };
			}
			if (num3 > 0 && num3 != num)
			{
				return new ValidationResult[]
				{
					new ValidationResult("ValidationFailed: The count of ModelIds and PerspectiveIds is not the same")
				};
			}
			return new ValidationResult[] { ValidationResult.Success };
		}
	}
}
