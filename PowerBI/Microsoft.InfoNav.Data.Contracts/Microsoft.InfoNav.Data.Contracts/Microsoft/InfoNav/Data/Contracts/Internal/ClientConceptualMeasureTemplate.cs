using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200015F RID: 351
	[DataContract(Name = "ConceptualTemplate", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	internal sealed class ClientConceptualMeasureTemplate
	{
		// Token: 0x060008DF RID: 2271 RVA: 0x0001235E File Offset: 0x0001055E
		internal ClientConceptualMeasureTemplate()
		{
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00012366 File Offset: 0x00010566
		internal ClientConceptualMeasureTemplate(string daxTemplateName)
		{
			this._daxTemplateName = daxTemplateName;
		}

		// Token: 0x0400046D RID: 1133
		[DataMember(Name = "DaxTemplateName", IsRequired = true, EmitDefaultValue = true, Order = 0)]
		private readonly string _daxTemplateName;
	}
}
