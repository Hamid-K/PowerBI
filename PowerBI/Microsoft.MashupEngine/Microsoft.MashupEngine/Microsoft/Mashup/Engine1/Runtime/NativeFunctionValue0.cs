using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001586 RID: 5510
	public abstract class NativeFunctionValue0 : NativeFunctionValue
	{
		// Token: 0x17002423 RID: 9251
		// (get) Token: 0x0600893A RID: 35130 RVA: 0x001D0917 File Offset: 0x001CEB17
		public sealed override TypeValue Type
		{
			get
			{
				if (this.functionType == null)
				{
					this.functionType = FunctionTypeValue.New(this.ReturnType, RecordValue.Empty, 0);
				}
				return this.functionType;
			}
		}

		// Token: 0x17002424 RID: 9252
		// (get) Token: 0x0600893B RID: 35131 RVA: 0x001BE5CD File Offset: 0x001BC7CD
		protected virtual TypeValue ReturnType
		{
			get
			{
				return TypeValue.Any;
			}
		}

		// Token: 0x0600893C RID: 35132
		public abstract override Value Invoke();

		// Token: 0x04004BC5 RID: 19397
		private TypeValue functionType;
	}
}
