using System;
using System.Runtime.Serialization;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x02000102 RID: 258
	[DataContract(Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public abstract class ModelEntityTermBinding : EntityTermBinding
	{
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x0000920D File Offset: 0x0000740D
		// (set) Token: 0x0600050B RID: 1291 RVA: 0x00009215 File Offset: 0x00007415
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 10)]
		public string ConceptualSchema { get; set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x0000921E File Offset: 0x0000741E
		// (set) Token: 0x0600050D RID: 1293 RVA: 0x00009226 File Offset: 0x00007426
		[DataMember(IsRequired = true, Order = 20)]
		public string ConceptualEntity { get; set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x0000922F File Offset: 0x0000742F
		// (set) Token: 0x0600050F RID: 1295 RVA: 0x00009237 File Offset: 0x00007437
		[DataMember(IsRequired = false, EmitDefaultValue = false, Order = 30)]
		public SemanticType SemanticType { get; set; }

		// Token: 0x06000510 RID: 1296 RVA: 0x00009240 File Offset: 0x00007440
		public override string ToString()
		{
			return StringUtil.FormatInvariant("{0}_{1}", base.ToString(), this.ConceptualEntity);
		}
	}
}
