using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.Write
{
	// Token: 0x0200078A RID: 1930
	internal class UpdateRequest : ODataWriteRequest
	{
		// Token: 0x060038A9 RID: 14505 RVA: 0x000B6C23 File Offset: 0x000B4E23
		public UpdateRequest(Uri odataUri, IValueReference content, Dictionary<string, string> headers, Microsoft.OData.Edm.IEdmNavigationSource source, ODataEnvironment environment)
			: base(odataUri, content, headers, "PATCH", source, environment)
		{
		}

		// Token: 0x060038AA RID: 14506 RVA: 0x000B6C38 File Offset: 0x000B4E38
		public override IValueReference ProcessWebResponse(HttpResponseData data)
		{
			string text = null;
			try
			{
				if (data.StatusCode == 200)
				{
					return WriteHelper.ReadFromHttpResponseData(data, base.OdataEnvironment, base.OdataUri);
				}
				if (data.StatusCode == 204)
				{
					return ODataResponse.GetResponseValue(base.OdataUri, base.OdataEnvironment, null);
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
