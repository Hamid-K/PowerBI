using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000015 RID: 21
	[NullableContext(2)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
	internal sealed class DynamicDependencyAttribute : Attribute
	{
		// Token: 0x06000029 RID: 41 RVA: 0x000021F3 File Offset: 0x000003F3
		[NullableContext(1)]
		public DynamicDependencyAttribute(string memberSignature)
		{
			this.MemberSignature = memberSignature;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002202 File Offset: 0x00000402
		[NullableContext(1)]
		public DynamicDependencyAttribute(string memberSignature, Type type)
		{
			this.MemberSignature = memberSignature;
			this.Type = type;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002218 File Offset: 0x00000418
		[NullableContext(1)]
		public DynamicDependencyAttribute(string memberSignature, string typeName, string assemblyName)
		{
			this.MemberSignature = memberSignature;
			this.TypeName = typeName;
			this.AssemblyName = assemblyName;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002235 File Offset: 0x00000435
		[NullableContext(1)]
		public DynamicDependencyAttribute(DynamicallyAccessedMemberTypes memberTypes, Type type)
		{
			this.MemberTypes = memberTypes;
			this.Type = type;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000224B File Offset: 0x0000044B
		[NullableContext(1)]
		public DynamicDependencyAttribute(DynamicallyAccessedMemberTypes memberTypes, string typeName, string assemblyName)
		{
			this.MemberTypes = memberTypes;
			this.TypeName = typeName;
			this.AssemblyName = assemblyName;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002268 File Offset: 0x00000468
		public string MemberSignature { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002270 File Offset: 0x00000470
		public DynamicallyAccessedMemberTypes MemberTypes { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002278 File Offset: 0x00000478
		public Type Type { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002280 File Offset: 0x00000480
		public string TypeName { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002288 File Offset: 0x00000488
		public string AssemblyName { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002290 File Offset: 0x00000490
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002298 File Offset: 0x00000498
		public string Condition { get; set; }
	}
}
