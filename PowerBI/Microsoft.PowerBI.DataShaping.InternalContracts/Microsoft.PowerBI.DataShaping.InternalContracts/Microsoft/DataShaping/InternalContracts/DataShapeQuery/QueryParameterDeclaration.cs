using System;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x020000B5 RID: 181
	internal sealed class QueryParameterDeclaration : IStructuredToString
	{
		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x000078C1 File Offset: 0x00005AC1
		// (set) Token: 0x06000438 RID: 1080 RVA: 0x000078C9 File Offset: 0x00005AC9
		public string Name { get; set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x000078D2 File Offset: 0x00005AD2
		// (set) Token: 0x0600043A RID: 1082 RVA: 0x000078DA File Offset: 0x00005ADA
		public ConceptualResultType Type { get; set; }

		// Token: 0x0600043B RID: 1083 RVA: 0x000078E4 File Offset: 0x00005AE4
		public void WriteTo(StructuredStringBuilder builder)
		{
			builder.BeginObject("QueryParameterDeclaration");
			builder.WriteAttribute<string>("Name", this.Name, false, false);
			string text = "Type";
			ConceptualResultType type = this.Type;
			builder.WriteProperty<string>(text, (type != null) ? type.ToString() : null, false);
			builder.EndObject();
		}
	}
}
