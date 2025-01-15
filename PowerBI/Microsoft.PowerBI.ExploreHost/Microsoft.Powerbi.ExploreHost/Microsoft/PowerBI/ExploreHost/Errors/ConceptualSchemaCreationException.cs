using System;
using System.Globalization;
using Microsoft.InfoNav.Explore.ServiceContracts.Internal;
using Microsoft.PowerBI.Query.Contracts;
using Microsoft.PowerBI.ReportingServicesHost;

namespace Microsoft.PowerBI.ExploreHost.Errors
{
	// Token: 0x02000088 RID: 136
	[Serializable]
	internal class ConceptualSchemaCreationException : PowerBIExploreException
	{
		// Token: 0x0600039D RID: 925 RVA: 0x0000B881 File Offset: 0x00009A81
		public ConceptualSchemaCreationException()
			: base("ConceptualSchemaCreationFailed", "Failed to create ConceptualSchema.", ErrorSource.PowerBI, ServiceErrorStatusCode.GeneralError)
		{
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000B895 File Offset: 0x00009A95
		public ConceptualSchemaCreationException(string message, ErrorSource errorSource)
			: base("ConceptualSchemaCreationFailed", message, errorSource, ServiceErrorStatusCode.GeneralError)
		{
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000B8A5 File Offset: 0x00009AA5
		public ConceptualSchemaCreationException(string message, Exception innerException, ErrorSource errorSource)
			: base("ConceptualSchemaCreationFailed", message, innerException, errorSource, ServiceErrorStatusCode.GeneralError)
		{
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000B8B6 File Offset: 0x00009AB6
		public ConceptualSchemaCreationException(long modelId, ErrorSource errorSource)
			: base("ConceptualSchemaCreationFailed", string.Format(CultureInfo.CurrentCulture, "Exception while parsing CSDL to create Conceptual Schema. Model ID={0}", modelId.ToString(CultureInfo.CurrentCulture)), errorSource, ServiceErrorStatusCode.GeneralError)
		{
		}
	}
}
