using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x0200000B RID: 11
	[NullableContext(2)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
	internal sealed class DynamicDependencyAttribute : Attribute
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002167 File Offset: 0x00000367
		[NullableContext(1)]
		public DynamicDependencyAttribute(string memberSignature)
		{
			this.MemberSignature = memberSignature;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002176 File Offset: 0x00000376
		[NullableContext(1)]
		public DynamicDependencyAttribute(string memberSignature, Type type)
		{
			this.MemberSignature = memberSignature;
			this.Type = type;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x0000218C File Offset: 0x0000038C
		[NullableContext(1)]
		public DynamicDependencyAttribute(string memberSignature, string typeName, string assemblyName)
		{
			this.MemberSignature = memberSignature;
			this.TypeName = typeName;
			this.AssemblyName = assemblyName;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000021A9 File Offset: 0x000003A9
		[NullableContext(1)]
		public DynamicDependencyAttribute(DynamicallyAccessedMemberTypes memberTypes, Type type)
		{
			this.MemberTypes = memberTypes;
			this.Type = type;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000021BF File Offset: 0x000003BF
		[NullableContext(1)]
		public DynamicDependencyAttribute(DynamicallyAccessedMemberTypes memberTypes, string typeName, string assemblyName)
		{
			this.MemberTypes = memberTypes;
			this.TypeName = typeName;
			this.AssemblyName = assemblyName;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000021DC File Offset: 0x000003DC
		public string MemberSignature { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000021E4 File Offset: 0x000003E4
		public DynamicallyAccessedMemberTypes MemberTypes { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000021EC File Offset: 0x000003EC
		public Type Type { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000023 RID: 35 RVA: 0x000021F4 File Offset: 0x000003F4
		public string TypeName { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000021FC File Offset: 0x000003FC
		public string AssemblyName { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002204 File Offset: 0x00000404
		// (set) Token: 0x06000026 RID: 38 RVA: 0x0000220C File Offset: 0x0000040C
		public string Condition { get; set; }
	}
}
