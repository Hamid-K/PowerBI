using System;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x0200036A RID: 874
	public class VariableMetadata
	{
		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06001A02 RID: 6658 RVA: 0x000603E3 File Offset: 0x0005E5E3
		// (set) Token: 0x06001A03 RID: 6659 RVA: 0x000603EB File Offset: 0x0005E5EB
		public string Name { get; private set; }

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06001A04 RID: 6660 RVA: 0x000603F4 File Offset: 0x0005E5F4
		// (set) Token: 0x06001A05 RID: 6661 RVA: 0x000603FC File Offset: 0x0005E5FC
		public Type VariableMetadataType { get; private set; }

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06001A06 RID: 6662 RVA: 0x00060405 File Offset: 0x0005E605
		// (set) Token: 0x06001A07 RID: 6663 RVA: 0x0006040D File Offset: 0x0005E60D
		public string TypeNameInDeclaration { get; private set; }

		// Token: 0x06001A08 RID: 6664 RVA: 0x00060416 File Offset: 0x0005E616
		public VariableMetadata(Type type, string name)
			: this(type, type.FullName.Replace('+', '.'), name)
		{
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x0006042F File Offset: 0x0005E62F
		public VariableMetadata([NotNull] Type type, [NotNull] string typeNameInDeclaration, [NotNull] string name)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Type>(type, "type");
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(typeNameInDeclaration, "typeNameInDeclaration");
			ExtendedDiagnostics.EnsureStringNotNullOrEmpty(name, "name");
			this.Name = name;
			this.TypeNameInDeclaration = typeNameInDeclaration;
			this.VariableMetadataType = type;
		}
	}
}
