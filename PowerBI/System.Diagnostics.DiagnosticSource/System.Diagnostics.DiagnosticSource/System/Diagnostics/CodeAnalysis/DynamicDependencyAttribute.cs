using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200005F RID: 95
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
	internal sealed class DynamicDependencyAttribute : Attribute
	{
		// Token: 0x060002C3 RID: 707 RVA: 0x0000B1F3 File Offset: 0x000093F3
		public DynamicDependencyAttribute(string memberSignature)
		{
			this.MemberSignature = memberSignature;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000B202 File Offset: 0x00009402
		public DynamicDependencyAttribute(string memberSignature, Type type)
		{
			this.MemberSignature = memberSignature;
			this.Type = type;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000B218 File Offset: 0x00009418
		public DynamicDependencyAttribute(string memberSignature, string typeName, string assemblyName)
		{
			this.MemberSignature = memberSignature;
			this.TypeName = typeName;
			this.AssemblyName = assemblyName;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000B235 File Offset: 0x00009435
		public DynamicDependencyAttribute(DynamicallyAccessedMemberTypes memberTypes, Type type)
		{
			this.MemberTypes = memberTypes;
			this.Type = type;
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000B24B File Offset: 0x0000944B
		public DynamicDependencyAttribute(DynamicallyAccessedMemberTypes memberTypes, string typeName, string assemblyName)
		{
			this.MemberTypes = memberTypes;
			this.TypeName = typeName;
			this.AssemblyName = assemblyName;
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x0000B268 File Offset: 0x00009468
		public string MemberSignature { get; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000B270 File Offset: 0x00009470
		public DynamicallyAccessedMemberTypes MemberTypes { get; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000B278 File Offset: 0x00009478
		public Type Type { get; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0000B280 File Offset: 0x00009480
		public string TypeName { get; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000B288 File Offset: 0x00009488
		public string AssemblyName { get; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000B290 File Offset: 0x00009490
		// (set) Token: 0x060002CE RID: 718 RVA: 0x0000B298 File Offset: 0x00009498
		public string Condition { get; set; }
	}
}
