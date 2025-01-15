using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000560 RID: 1376
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	[BaseTypeRequired(typeof(Attribute))]
	public sealed class BaseTypeRequiredAttribute : Attribute
	{
		// Token: 0x06002A2B RID: 10795 RVA: 0x0009820D File Offset: 0x0009640D
		public BaseTypeRequiredAttribute(Type baseType)
		{
			this.BaseTypes = new Type[] { baseType };
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06002A2C RID: 10796 RVA: 0x00098225 File Offset: 0x00096425
		// (set) Token: 0x06002A2D RID: 10797 RVA: 0x0009822D File Offset: 0x0009642D
		public Type[] BaseTypes { get; private set; }
	}
}
