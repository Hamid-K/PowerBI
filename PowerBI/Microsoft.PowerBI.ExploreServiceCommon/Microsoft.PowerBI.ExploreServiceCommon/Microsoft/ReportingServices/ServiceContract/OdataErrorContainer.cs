using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace Microsoft.ReportingServices.ServiceContract
{
	// Token: 0x02000005 RID: 5
	[DataContract]
	internal sealed class OdataErrorContainer
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002067 File Offset: 0x00000267
		// (set) Token: 0x06000004 RID: 4 RVA: 0x0000206F File Offset: 0x0000026F
		[DataMember(Name = "odata.error", IsRequired = true, EmitDefaultValue = false)]
		internal OdataError Error { get; set; }

		// Token: 0x06000005 RID: 5 RVA: 0x00002078 File Offset: 0x00000278
		internal static OdataErrorContainer Deserialize(MemoryStream outputStream)
		{
			return (OdataErrorContainer)OdataErrorContainer.OdataErrorDeserializer.ReadObject(outputStream);
		}

		// Token: 0x0400002A RID: 42
		public static readonly DataContractJsonSerializer OdataErrorDeserializer = new DataContractJsonSerializer(typeof(OdataErrorContainer));
	}
}
