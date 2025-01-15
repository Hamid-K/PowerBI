using System;

namespace JetBrains.Annotations
{
	// Token: 0x0200055B RID: 1371
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public sealed class NotifyPropertyChangedInvocatorAttribute : Attribute
	{
		// Token: 0x06002A1E RID: 10782 RVA: 0x00012024 File Offset: 0x00010224
		public NotifyPropertyChangedInvocatorAttribute()
		{
		}

		// Token: 0x06002A1F RID: 10783 RVA: 0x000981AB File Offset: 0x000963AB
		public NotifyPropertyChangedInvocatorAttribute(string parameterName)
		{
			this.ParameterName = parameterName;
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06002A20 RID: 10784 RVA: 0x000981BA File Offset: 0x000963BA
		// (set) Token: 0x06002A21 RID: 10785 RVA: 0x000981C2 File Offset: 0x000963C2
		[UsedImplicitly]
		public string ParameterName { get; private set; }
	}
}
