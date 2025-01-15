using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x020000E7 RID: 231
	internal class DataTransformBuilder<TParent> : BuilderBase<DataTransform, TParent>
	{
		// Token: 0x0600066D RID: 1645 RVA: 0x0000DCF3 File Offset: 0x0000BEF3
		internal DataTransformBuilder(TParent parent, DataTransform activeObject)
			: base(parent, activeObject)
		{
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0000DCFD File Offset: 0x0000BEFD
		public DataTransformInputBuilder<DataTransformBuilder<TParent>> WithInput()
		{
			base.ActiveObject.Input = new DataTransformInput();
			return new DataTransformInputBuilder<DataTransformBuilder<TParent>>(this, base.ActiveObject.Input);
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0000DD20 File Offset: 0x0000BF20
		public DataTransformOutputBuilder<DataTransformBuilder<TParent>> WithOutput()
		{
			base.ActiveObject.Output = new DataTransformOutput();
			return new DataTransformOutputBuilder<DataTransformBuilder<TParent>>(this, base.ActiveObject.Output);
		}
	}
}
