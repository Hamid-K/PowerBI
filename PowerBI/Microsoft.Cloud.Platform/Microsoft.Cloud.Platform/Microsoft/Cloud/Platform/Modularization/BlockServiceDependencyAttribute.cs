using System;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000A9 RID: 169
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	public sealed class BlockServiceDependencyAttribute : Attribute
	{
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x0001202C File Offset: 0x0001022C
		// (set) Token: 0x060004E0 RID: 1248 RVA: 0x00012034 File Offset: 0x00010234
		public Type ServiceType { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x0001203D File Offset: 0x0001023D
		// (set) Token: 0x060004E2 RID: 1250 RVA: 0x00012045 File Offset: 0x00010245
		public string ServiceIdentity { get; set; }

		// Token: 0x060004E3 RID: 1251 RVA: 0x0001204E File Offset: 0x0001024E
		public BlockServiceDependencyAttribute()
			: this(null, null)
		{
		}

		// Token: 0x060004E4 RID: 1252 RVA: 0x00012058 File Offset: 0x00010258
		public BlockServiceDependencyAttribute(Type serviceType)
			: this(serviceType, null)
		{
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x00012062 File Offset: 0x00010262
		public BlockServiceDependencyAttribute(Type serviceType, string serviceIdentity)
		{
			this.ServiceType = serviceType;
			this.ServiceIdentity = serviceIdentity;
		}
	}
}
