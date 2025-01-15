using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000201 RID: 513
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	internal sealed class RazorInjectionAttribute : Attribute
	{
		// Token: 0x06001491 RID: 5265 RVA: 0x00036BEB File Offset: 0x00034DEB
		public RazorInjectionAttribute([NotNull] string type, [NotNull] string fieldName)
		{
			this.Type = type;
			this.FieldName = fieldName;
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06001492 RID: 5266 RVA: 0x00036C01 File Offset: 0x00034E01
		// (set) Token: 0x06001493 RID: 5267 RVA: 0x00036C09 File Offset: 0x00034E09
		[NotNull]
		public string Type { get; private set; }

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06001494 RID: 5268 RVA: 0x00036C12 File Offset: 0x00034E12
		// (set) Token: 0x06001495 RID: 5269 RVA: 0x00036C1A File Offset: 0x00034E1A
		[NotNull]
		public string FieldName { get; private set; }
	}
}
