using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200025A RID: 602
	[ImmutableObject(true)]
	public sealed class ResolvedQueryTransform : IEquatable<ResolvedQueryTransform>
	{
		// Token: 0x0600121B RID: 4635 RVA: 0x0001FF8B File Offset: 0x0001E18B
		internal ResolvedQueryTransform(string name, string algorithm, ResolvedQueryTransformInput input, ResolvedQueryTransformOutput output)
		{
			this._name = name;
			this._algorithm = algorithm;
			this._input = input;
			this._output = output;
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x0600121C RID: 4636 RVA: 0x0001FFB0 File Offset: 0x0001E1B0
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x0600121D RID: 4637 RVA: 0x0001FFB8 File Offset: 0x0001E1B8
		public string Algorithm
		{
			get
			{
				return this._algorithm;
			}
		}

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x0600121E RID: 4638 RVA: 0x0001FFC0 File Offset: 0x0001E1C0
		public ResolvedQueryTransformInput Input
		{
			get
			{
				return this._input;
			}
		}

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x0600121F RID: 4639 RVA: 0x0001FFC8 File Offset: 0x0001E1C8
		public ResolvedQueryTransformOutput Output
		{
			get
			{
				return this._output;
			}
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x0001FFD0 File Offset: 0x0001E1D0
		public bool Equals(ResolvedQueryTransform other)
		{
			return DefaultResolvedQueryDefinitionEqualityComparer.Instance.Equals(this, other);
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x0001FFDE File Offset: 0x0001E1DE
		public override int GetHashCode()
		{
			return DefaultResolvedQueryDefinitionEqualityComparer.Instance.GetHashCode(this);
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x0001FFEB File Offset: 0x0001E1EB
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ResolvedQueryTransform);
		}

		// Token: 0x040007B0 RID: 1968
		private readonly string _name;

		// Token: 0x040007B1 RID: 1969
		private readonly string _algorithm;

		// Token: 0x040007B2 RID: 1970
		private readonly ResolvedQueryTransformInput _input;

		// Token: 0x040007B3 RID: 1971
		private readonly ResolvedQueryTransformOutput _output;
	}
}
