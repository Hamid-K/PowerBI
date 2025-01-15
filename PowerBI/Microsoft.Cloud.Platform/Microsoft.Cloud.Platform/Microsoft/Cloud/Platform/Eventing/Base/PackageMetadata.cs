using System;
using System.Globalization;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Eventing.Base
{
	// Token: 0x020003CC RID: 972
	public class PackageMetadata
	{
		// Token: 0x06001E07 RID: 7687 RVA: 0x00071938 File Offset: 0x0006FB38
		public PackageMetadata(Guid packageId, [NotNull] string name, [NotNull] Type type, int priority)
		{
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(name, "name");
			ExtendedDiagnostics.EnsureArgumentNotNull<Type>(type, "type");
			ExtendedDiagnostics.EnsureArgumentIsNotNegative(priority, "priority");
			this.Id = packageId;
			this.Name = name;
			this.PackageType = type;
			this.Priority = priority;
		}

		// Token: 0x06001E08 RID: 7688 RVA: 0x0007198C File Offset: 0x0006FB8C
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "<Name: {0} Id: {1} Assembly: {2}>", new object[]
			{
				this.Name,
				this.Id,
				this.PackageType.Assembly.GetName()
			});
		}

		// Token: 0x1700045A RID: 1114
		// (get) Token: 0x06001E09 RID: 7689 RVA: 0x000719D8 File Offset: 0x0006FBD8
		// (set) Token: 0x06001E0A RID: 7690 RVA: 0x000719E0 File Offset: 0x0006FBE0
		public Guid Id { get; private set; }

		// Token: 0x1700045B RID: 1115
		// (get) Token: 0x06001E0B RID: 7691 RVA: 0x000719E9 File Offset: 0x0006FBE9
		// (set) Token: 0x06001E0C RID: 7692 RVA: 0x000719F1 File Offset: 0x0006FBF1
		public string Name { get; private set; }

		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06001E0D RID: 7693 RVA: 0x000719FA File Offset: 0x0006FBFA
		// (set) Token: 0x06001E0E RID: 7694 RVA: 0x00071A02 File Offset: 0x0006FC02
		public Type PackageType { get; private set; }

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001E0F RID: 7695 RVA: 0x00071A0B File Offset: 0x0006FC0B
		// (set) Token: 0x06001E10 RID: 7696 RVA: 0x00071A13 File Offset: 0x0006FC13
		public int Priority { get; private set; }
	}
}
