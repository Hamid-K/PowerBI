using System;

namespace JetBrains.Annotations
{
	// Token: 0x020001CA RID: 458
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	[BaseTypeRequired(typeof(Attribute))]
	internal sealed class BaseTypeRequiredAttribute : Attribute
	{
		// Token: 0x06001417 RID: 5143 RVA: 0x0003673C File Offset: 0x0003493C
		public BaseTypeRequiredAttribute([NotNull] Type baseType)
		{
			this.BaseType = baseType;
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06001418 RID: 5144 RVA: 0x0003674B File Offset: 0x0003494B
		// (set) Token: 0x06001419 RID: 5145 RVA: 0x00036753 File Offset: 0x00034953
		[NotNull]
		public Type BaseType { get; private set; }
	}
}
