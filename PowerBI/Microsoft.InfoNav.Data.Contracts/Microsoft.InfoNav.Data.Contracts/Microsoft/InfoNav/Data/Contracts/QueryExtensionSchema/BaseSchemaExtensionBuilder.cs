using System;

namespace Microsoft.InfoNav.Data.Contracts.QueryExtensionSchema
{
	// Token: 0x020000C6 RID: 198
	public abstract class BaseSchemaExtensionBuilder<TObject, TParent>
	{
		// Token: 0x0600050A RID: 1290 RVA: 0x0000BEBF File Offset: 0x0000A0BF
		protected BaseSchemaExtensionBuilder(TObject obj, TParent parent)
		{
			this.ActiveObject = obj;
			this.Parent = parent;
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x0000BED5 File Offset: 0x0000A0D5
		internal TObject ActiveObject { get; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x0000BEDD File Offset: 0x0000A0DD
		public TParent Parent { get; }
	}
}
