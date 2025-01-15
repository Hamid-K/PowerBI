using System;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000016 RID: 22
	public abstract class FilterBase : TransformBase
	{
		// Token: 0x06000069 RID: 105 RVA: 0x00003B8D File Offset: 0x00001D8D
		protected FilterBase(IHostEnvironment env, string name, IDataView input)
			: base(env, name, input)
		{
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003B98 File Offset: 0x00001D98
		protected FilterBase(IHost host, IDataView input)
			: base(host, input)
		{
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003BA4 File Offset: 0x00001DA4
		public override long? GetRowCount(bool lazy = true)
		{
			return null;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003BBA File Offset: 0x00001DBA
		public sealed override ISchema Schema
		{
			get
			{
				return this._input.Schema;
			}
		}
	}
}
