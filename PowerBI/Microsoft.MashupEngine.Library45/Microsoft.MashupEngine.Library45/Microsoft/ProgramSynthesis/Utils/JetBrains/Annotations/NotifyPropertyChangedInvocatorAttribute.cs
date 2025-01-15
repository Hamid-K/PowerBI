using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x0200054F RID: 1359
	[AttributeUsage(AttributeTargets.Method)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class NotifyPropertyChangedInvocatorAttribute : Attribute
	{
		// Token: 0x06001EAF RID: 7855 RVA: 0x0000957E File Offset: 0x0000777E
		public NotifyPropertyChangedInvocatorAttribute()
		{
		}

		// Token: 0x06001EB0 RID: 7856 RVA: 0x00059860 File Offset: 0x00057A60
		public NotifyPropertyChangedInvocatorAttribute(string parameterName)
		{
			this.ParameterName = parameterName;
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001EB1 RID: 7857 RVA: 0x0005986F File Offset: 0x00057A6F
		public string ParameterName { get; }
	}
}
