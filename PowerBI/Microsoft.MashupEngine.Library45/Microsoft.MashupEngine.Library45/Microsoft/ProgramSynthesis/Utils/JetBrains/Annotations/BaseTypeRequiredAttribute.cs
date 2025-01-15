using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x02000553 RID: 1363
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class BaseTypeRequiredAttribute : Attribute
	{
		// Token: 0x06001EBA RID: 7866 RVA: 0x000598C7 File Offset: 0x00057AC7
		public BaseTypeRequiredAttribute(Type baseType)
		{
			this.BaseType = baseType;
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001EBB RID: 7867 RVA: 0x000598D6 File Offset: 0x00057AD6
		public Type BaseType { get; }
	}
}
