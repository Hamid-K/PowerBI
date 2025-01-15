using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.Write
{
	// Token: 0x0200077E RID: 1918
	internal sealed class DeleteRequest : ODataWriteRequest
	{
		// Token: 0x06003873 RID: 14451 RVA: 0x000B5989 File Offset: 0x000B3B89
		public DeleteRequest(Uri odataUri, IValueReference content, Dictionary<string, string> headers, Microsoft.OData.Edm.IEdmNavigationSource source, ODataEnvironment environment)
			: base(odataUri, content, headers, "DELETE", source, environment)
		{
		}

		// Token: 0x06003874 RID: 14452 RVA: 0x000B599D File Offset: 0x000B3B9D
		public override void SetODataRequestContents(IODataRequestMessage requestMessage)
		{
			this.SetODataRequestHeaders(requestMessage);
		}

		// Token: 0x06003875 RID: 14453 RVA: 0x000B59A8 File Offset: 0x000B3BA8
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
