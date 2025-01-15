using System;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x0200049B RID: 1179
	internal class DataModelValidator
	{
		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06003A0D RID: 14861 RVA: 0x000C0124 File Offset: 0x000BE324
		// (remove) Token: 0x06003A0E RID: 14862 RVA: 0x000C015C File Offset: 0x000BE35C
		public event EventHandler<DataModelErrorEventArgs> OnError;

		// Token: 0x06003A0F RID: 14863 RVA: 0x000C0191 File Offset: 0x000BE391
		public void Validate(EdmModel model, bool validateSyntax)
		{
			EdmModelValidationContext edmModelValidationContext = new EdmModelValidationContext(model, validateSyntax);
			edmModelValidationContext.OnError += this.OnError;
			new EdmModelValidationVisitor(edmModelValidationContext, EdmModelRuleSet.CreateEdmModelRuleSet(model.SchemaVersion, validateSyntax)).Visit(model);
		}
	}
}
