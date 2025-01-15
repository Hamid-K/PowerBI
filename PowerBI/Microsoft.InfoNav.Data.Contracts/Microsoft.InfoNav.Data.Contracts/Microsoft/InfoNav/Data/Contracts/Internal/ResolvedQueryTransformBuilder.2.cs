using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000225 RID: 549
	public class ResolvedQueryTransformBuilder<TParent>
	{
		// Token: 0x06000FE4 RID: 4068 RVA: 0x0001E261 File Offset: 0x0001C461
		internal ResolvedQueryTransformBuilder(TParent parent, Action<ResolvedQueryTransform> addToParent, string name, string algorithm)
		{
			this._parent = parent;
			this._addToParent = addToParent;
			this._name = name;
			this._algorithm = algorithm;
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x0001E286 File Offset: 0x0001C486
		public ResolvedQueryTransformInputBuilder<ResolvedQueryTransformBuilder<TParent>> WithInput()
		{
			return new ResolvedQueryTransformInputBuilder<ResolvedQueryTransformBuilder<TParent>>(this, delegate(ResolvedQueryTransformInput input)
			{
				this.WithInput(input);
			});
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x0001E29A File Offset: 0x0001C49A
		public ResolvedQueryTransformBuilder<TParent> WithInput(ResolvedQueryTransformInput input)
		{
			this._input = input;
			return this;
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x0001E2A4 File Offset: 0x0001C4A4
		public ResolvedQueryTransformOutputBuilder<ResolvedQueryTransformBuilder<TParent>> WithOutput()
		{
			return new ResolvedQueryTransformOutputBuilder<ResolvedQueryTransformBuilder<TParent>>(this, delegate(ResolvedQueryTransformOutput output)
			{
				this.WithOutput(output);
			});
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x0001E2B8 File Offset: 0x0001C4B8
		public ResolvedQueryTransformBuilder<TParent> WithOutput(ResolvedQueryTransformOutput output)
		{
			this._output = output;
			return this;
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x06000FE9 RID: 4073 RVA: 0x0001E2C2 File Offset: 0x0001C4C2
		public TParent Parent
		{
			get
			{
				this._addToParent(this.Build());
				return this._parent;
			}
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x0001E2DB File Offset: 0x0001C4DB
		public ResolvedQueryTransform Build()
		{
			return new ResolvedQueryTransform(this._name, this._algorithm, this._input, this._output);
		}

		// Token: 0x04000759 RID: 1881
		private readonly TParent _parent;

		// Token: 0x0400075A RID: 1882
		private readonly Action<ResolvedQueryTransform> _addToParent;

		// Token: 0x0400075B RID: 1883
		private readonly string _name;

		// Token: 0x0400075C RID: 1884
		private readonly string _algorithm;

		// Token: 0x0400075D RID: 1885
		private ResolvedQueryTransformInput _input;

		// Token: 0x0400075E RID: 1886
		private ResolvedQueryTransformOutput _output;
	}
}
