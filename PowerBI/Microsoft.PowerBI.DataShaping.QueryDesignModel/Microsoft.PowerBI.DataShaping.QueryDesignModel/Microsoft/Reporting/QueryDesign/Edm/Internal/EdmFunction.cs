using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Metadata.Edm;
using Microsoft.DataShaping.Common;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x020001FF RID: 511
	internal sealed class EdmFunction
	{
		// Token: 0x06001817 RID: 6167 RVA: 0x00042655 File Offset: 0x00040855
		private EdmFunction(string name, string fullName, IReadOnlyList<FunctionParameter> parameters, ConceptualResultType conceptualResultType)
		{
			this.Name = name;
			this.FullName = fullName;
			this.Parameters = parameters;
			this.ConceptualReturnType = conceptualResultType;
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001818 RID: 6168 RVA: 0x0004267A File Offset: 0x0004087A
		public IReadOnlyList<FunctionParameter> Parameters { get; }

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001819 RID: 6169 RVA: 0x00042682 File Offset: 0x00040882
		public string Name { get; }

		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x0600181A RID: 6170 RVA: 0x0004268A File Offset: 0x0004088A
		public string FullName { get; }

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x0600181B RID: 6171 RVA: 0x00042692 File Offset: 0x00040892
		public ConceptualResultType ConceptualReturnType { get; }

		// Token: 0x0600181C RID: 6172 RVA: 0x0004269C File Offset: 0x0004089C
		internal static EdmFunction Create(EdmFunction edmFunction)
		{
			List<FunctionParameter> list = edmFunction.Parameters.Select(delegate(FunctionParameter p)
			{
				ConceptualResultType conceptualResultType2 = EdmConceptualTypeConverter.ConvertTypeForFunction(p.TypeUsage);
				return new FunctionParameter(p.Name, conceptualResultType2);
			}).ToList<FunctionParameter>();
			ConceptualResultType conceptualResultType = EdmConceptualTypeConverter.ConvertTypeForFunction(edmFunction.ReturnParameter.TypeUsage);
			return new EdmFunction(edmFunction.Name, edmFunction.FullName, list, conceptualResultType);
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x00042700 File Offset: 0x00040900
		public override bool Equals(object obj)
		{
			EdmFunction edmFunction = obj as EdmFunction;
			return edmFunction != null && this.Name == edmFunction.Name && this.FullName == edmFunction.FullName && this.Parameters.SequenceEqualReadOnly(edmFunction.Parameters) && this.ConceptualReturnType == edmFunction.ConceptualReturnType;
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x00042760 File Offset: 0x00040960
		public override int GetHashCode()
		{
			return Hashing.CombineHash(this.Name.GetHashCode(), this.FullName.GetHashCode(), Hashing.GetHashCode<IReadOnlyList<FunctionParameter>>(this.Parameters, null), this.ConceptualReturnType.GetHashCode());
		}
	}
}
