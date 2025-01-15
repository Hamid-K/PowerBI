using System;
using System.Linq;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004AD RID: 1197
	internal sealed class EdmModelValidationContext
	{
		// Token: 0x1400000F RID: 15
		// (add) Token: 0x06003AD0 RID: 15056 RVA: 0x000C2780 File Offset: 0x000C0980
		// (remove) Token: 0x06003AD1 RID: 15057 RVA: 0x000C27B8 File Offset: 0x000C09B8
		public event EventHandler<DataModelErrorEventArgs> OnError;

		// Token: 0x06003AD2 RID: 15058 RVA: 0x000C27ED File Offset: 0x000C09ED
		public EdmModelValidationContext(EdmModel model, bool validateSyntax)
		{
			this._model = model;
			this._validateSyntax = validateSyntax;
		}

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x06003AD3 RID: 15059 RVA: 0x000C2803 File Offset: 0x000C0A03
		public bool ValidateSyntax
		{
			get
			{
				return this._validateSyntax;
			}
		}

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x06003AD4 RID: 15060 RVA: 0x000C280B File Offset: 0x000C0A0B
		public EdmModel Model
		{
			get
			{
				return this._model;
			}
		}

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x06003AD5 RID: 15061 RVA: 0x000C2813 File Offset: 0x000C0A13
		public bool IsCSpace
		{
			get
			{
				return this._model.Containers.First<EntityContainer>().DataSpace == DataSpace.CSpace;
			}
		}

		// Token: 0x06003AD6 RID: 15062 RVA: 0x000C282D File Offset: 0x000C0A2D
		public void AddError(MetadataItem item, string propertyName, string errorMessage)
		{
			this.RaiseDataModelValidationEvent(new DataModelErrorEventArgs
			{
				ErrorMessage = errorMessage,
				Item = item,
				PropertyName = propertyName
			});
		}

		// Token: 0x06003AD7 RID: 15063 RVA: 0x000C284F File Offset: 0x000C0A4F
		private void RaiseDataModelValidationEvent(DataModelErrorEventArgs error)
		{
			if (this.OnError != null)
			{
				this.OnError(this, error);
			}
		}

		// Token: 0x04001466 RID: 5222
		private readonly EdmModel _model;

		// Token: 0x04001467 RID: 5223
		private readonly bool _validateSyntax;
	}
}
