using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Contracts.DataShapeResult
{
	// Token: 0x02000112 RID: 274
	public sealed class DsrErrorBuilder : BaseBindingBuilder<ODataError, DsrBuilder>
	{
		// Token: 0x0600074E RID: 1870 RVA: 0x0000F1C5 File Offset: 0x0000D3C5
		public DsrErrorBuilder(DsrBuilder parent)
			: base(parent)
		{
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0000F1CE File Offset: 0x0000D3CE
		public DsrErrorBuilder WithCode(string code)
		{
			Contract.CheckNonEmpty(code, "code");
			this._code = code;
			return this;
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0000F1E3 File Offset: 0x0000D3E3
		public DsrErrorBuilder WithMessage(string message)
		{
			Contract.CheckNonEmpty(message, "message");
			this._errorMessage = new ErrorMessage
			{
				Language = "en-US",
				Value = message
			};
			return this;
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0000F20E File Offset: 0x0000D40E
		public DsrErrorBuilder WithSource(string source)
		{
			Contract.CheckNonEmpty(source, "source");
			this._source = source;
			return this;
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0000F223 File Offset: 0x0000D423
		public DsrErrorBuilder WithAzureValue(AzureValue azureValue)
		{
			Contract.CheckValue<AzureValue>(azureValue, "azureValue");
			BaseBindingBuilder<ODataError, DsrBuilder>.AddToLazyList<AzureValue>(ref this._azureValuesList, azureValue);
			this._azureValues = this._azureValuesList.ToArray();
			return this;
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0000F24E File Offset: 0x0000D44E
		public override ODataError Build()
		{
			return new ODataError
			{
				Code = this._code,
				Source = this._source,
				Message = this._errorMessage,
				AzureValues = this._azureValues
			};
		}

		// Token: 0x04000328 RID: 808
		private string _code;

		// Token: 0x04000329 RID: 809
		private string _source;

		// Token: 0x0400032A RID: 810
		private ErrorMessage _errorMessage;

		// Token: 0x0400032B RID: 811
		private AzureValue[] _azureValues;

		// Token: 0x0400032C RID: 812
		private List<AzureValue> _azureValuesList;
	}
}
