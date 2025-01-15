using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData.Core;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4.Write
{
	// Token: 0x0200089F RID: 2207
	internal sealed class DeleteRequest : ODataWriteRequest
	{
		// Token: 0x06003F39 RID: 16185 RVA: 0x000CFD01 File Offset: 0x000CDF01
		public DeleteRequest(Uri odataUri, IValueReference content, Dictionary<string, string> headers, Microsoft.OData.Edm.IEdmNavigationSource source, ODataEnvironment environment)
			: base(odataUri, content, headers, "DELETE", source, environment)
		{
		}

		// Token: 0x06003F3A RID: 16186 RVA: 0x000CFD15 File Offset: 0x000CDF15
		public override void SetODataRequestContents(IODataRequestMessage requestMessage)
		{
			this.SetODataRequestHeaders(requestMessage);
		}

		// Token: 0x06003F3B RID: 16187 RVA: 0x000CFD20 File Offset: 0x000CDF20
		public override IValueReference ProcessWebResponse(HttpResponseData data)
		{
			if (data.StatusCode == 204)
			{
				return base.Content;
			}
			string text = string.Empty;
			if (!ODataCommonErrors.TryExtractErrorMessage(base.OdataEnvironment.Host, base.OdataEnvironment.Resource, data, out text))
			{
				text = Strings.ODataCannotWriteData;
			}
			IList<RecordKeyDefinition> list = new List<RecordKeyDefinition>();
			list.Add(new RecordKeyDefinition("StatusCode", NumberValue.New(data.StatusCode), TypeValue.Number));
			list.Add(new RecordKeyDefinition("ODataUri", TextValue.New(base.OdataUri.OriginalString), TypeValue.Text));
			return new ExceptionValueReference(DataSourceException.NewDataSourceError(base.OdataEnvironment.Host, text, base.OdataEnvironment.Resource, list, null));
		}
	}
}
