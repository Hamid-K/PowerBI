using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.Write
{
	// Token: 0x0200077F RID: 1919
	internal sealed class InsertRequest : ODataWriteRequest
	{
		// Token: 0x06003876 RID: 14454 RVA: 0x000B5A68 File Offset: 0x000B3C68
		public InsertRequest(Uri odataUri, IValueReference content, Dictionary<string, string> headers, Microsoft.OData.Edm.IEdmNavigationSource source, ODataEnvironment environment)
			: base(odataUri, content, headers, "POST", source, environment)
		{
		}

		// Token: 0x06003877 RID: 14455 RVA: 0x000B5A7C File Offset: 0x000B3C7C
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
					return ODataResponse.GetResponseValue(new Uri(text2), base.OdataEnvironment, null);
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
