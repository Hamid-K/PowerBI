using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Microsoft.ReportingServices.ServiceContract
{
	// Token: 0x02000002 RID: 2
	[DataContract]
	internal sealed class OdataErrorContainer
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		[DataMember(Name = "odata.error", IsRequired = true, EmitDefaultValue = false)]
		internal OdataError Error { get; set; }

		// Token: 0x06000003 RID: 3 RVA: 0x00002061 File Offset: 0x00000261
		internal static OdataErrorContainer Deserialize(MemoryStream outputStream)
		{
			return (OdataErrorContainer)OdataErrorContainer.OdataErrorDeserializer.ReadObject(outputStream);
		}

		// Token: 0x04000002 RID: 2
		public static readonly DataContractJsonSerializer OdataErrorDeserializer = new DataContractJsonSerializer(typeof(OdataErrorContainer));
	}
}
