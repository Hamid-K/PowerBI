using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000010 RID: 16
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
	internal sealed class DynamicDependencyAttribute : Attribute
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x00002FEF File Offset: 0x000011EF
		public DynamicDependencyAttribute(string memberSignature)
		{
			this.MemberSignature = memberSignature;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00002FFE File Offset: 0x000011FE
		public DynamicDependencyAttribute(string memberSignature, Type type)
		{
			this.MemberSignature = memberSignature;
			this.Type = type;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00003014 File Offset: 0x00001214
		public DynamicDependencyAttribute(string memberSignature, string typeName, string assemblyName)
		{
			this.MemberSignature = memberSignature;
			this.TypeName = typeName;
			this.AssemblyName = assemblyName;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00003031 File Offset: 0x00001231
		public DynamicDependencyAttribute(DynamicallyAccessedMemberTypes memberTypes, Type type)
		{
			this.MemberTypes = memberTypes;
			this.Type = type;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00003047 File Offset: 0x00001247
		public DynamicDependencyAttribute(DynamicallyAccessedMemberTypes memberTypes, string typeName, string assemblyName)
		{
			this.MemberTypes = memberTypes;
			this.TypeName = typeName;
			this.AssemblyName = assemblyName;
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00003064 File Offset: 0x00001264
		public string MemberSignature { get; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060000FE RID: 254 RVA: 0x0000306C File Offset: 0x0000126C
		public DynamicallyAccessedMemberTypes MemberTypes { get; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00003074 File Offset: 0x00001274
		public Type Type { get; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000100 RID: 256 RVA: 0x0000307C File Offset: 0x0000127C
		public string TypeName { get; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00003084 File Offset: 0x00001284
		public string AssemblyName { get; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000102 RID: 258 RVA: 0x0000308C File Offset: 0x0000128C
		// (set) Token: 0x06000103 RID: 259 RVA: 0x00003094 File Offset: 0x00001294
		public string Condition { get; set; }
	}
}
