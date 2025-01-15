using System;

namespace JetBrains.Annotations
{
	// Token: 0x020001C6 RID: 454
	[AttributeUsage(AttributeTargets.Method)]
	internal sealed class NotifyPropertyChangedInvocatorAttribute : Attribute
	{
		// Token: 0x06001408 RID: 5128 RVA: 0x000366A1 File Offset: 0x000348A1
		public NotifyPropertyChangedInvocatorAttribute()
		{
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x000366A9 File Offset: 0x000348A9
		public NotifyPropertyChangedInvocatorAttribute([NotNull] string parameterName)
		{
			this.ParameterName = parameterName;
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x0600140A RID: 5130 RVA: 0x000366B8 File Offset: 0x000348B8
		// (set) Token: 0x0600140B RID: 5131 RVA: 0x000366C0 File Offset: 0x000348C0
		[CanBeNull]
		public string ParameterName { get; private set; }
	}
}
