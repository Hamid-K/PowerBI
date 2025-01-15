using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4.Write
{
	// Token: 0x020008A0 RID: 2208
	internal sealed class InsertRequest : ODataWriteRequest
	{
		// Token: 0x06003F3C RID: 16188 RVA: 0x000CFDE0 File Offset: 0x000CDFE0
		public InsertRequest(Uri odataUri, IValueReference content, Dictionary<string, string> headers, Microsoft.OData.Edm.IEdmNavigationSource source, ODataEnvironment environment)
			: base(odataUri, content, headers, "POST", source, environment)
		{
		}

		// Token: 0x06003F3D RID: 16189 RVA: 0x000CFDF4 File Offset: 0x000CDFF4
		public override IValueReference ProcessWebResponse(HttpResponseData data)
		{
			string text = null;
			try
			{
				if (data.StatusCode == 201)
				{
					return WriteHelper.ReadFromHttpResponseData(data, base.OdataEnvironment, base.OdataUri);
				}
				string text2;
				if (data.StatusCode == 204 && data.Headers.TryGetValue("Location", out text2))
				{
					return ODataResponse.GetResponseValue(new Uri(text2), base.OdataEnvironment);
				}
			}
			catch (ValueException ex)
			{
				text = Strings.ODataCommonError(Strings.ODataCannotReadWrittenData, ex.MessageString);
			}
			if (text == null && !ODataCommonErrors.TryExtractErrorMessage(base.OdataEnvironment.Host, base.OdataEnvironment.Resource, data, out text))
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
