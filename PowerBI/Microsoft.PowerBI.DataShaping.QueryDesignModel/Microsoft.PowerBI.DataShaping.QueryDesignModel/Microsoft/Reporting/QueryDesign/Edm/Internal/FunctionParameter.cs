using System;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x02000200 RID: 512
	internal sealed class FunctionParameter
	{
		// Token: 0x0600181F RID: 6175 RVA: 0x00042794 File Offset: 0x00040994
		internal FunctionParameter(string name, ConceptualResultType type)
		{
			this.Name = name;
			this.Type = type;
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001820 RID: 6176 RVA: 0x000427AA File Offset: 0x000409AA
		public string Name { get; }

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001821 RID: 6177 RVA: 0x000427B2 File Offset: 0x000409B2
		public ConceptualResultType Type { get; }

		// Token: 0x06001822 RID: 6178 RVA: 0x000427BC File Offset: 0x000409BC
		public override bool Equals(object obj)
		{
			FunctionParameter functionParameter = obj as FunctionParameter;
			return functionParameter != null && this.Name == functionParameter.Name && this.Type == functionParameter.Type;
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x000427F6 File Offset: 0x000409F6
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.Name.GetHashCode(), this.Type.GetHashCode());
		}
	}
}
