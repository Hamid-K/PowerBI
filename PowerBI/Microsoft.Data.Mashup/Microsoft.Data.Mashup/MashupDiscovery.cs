using System;

namespace Microsoft.Data.Mashup
{
	// Token: 0x0200002A RID: 42
	public sealed class MashupDiscovery : IEquatable<MashupDiscovery>
	{
		// Token: 0x06000259 RID: 601 RVA: 0x0000A26C File Offset: 0x0000846C
		private MashupDiscovery(MashupDiscoveryKind kind, string moduleName, string moduleVersion)
		{
			this.kind = kind;
			this.moduleName = moduleName;
			this.moduleVersion = moduleVersion;
			this.coordinate = MashupPartitionCoordinate.Empty;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000A294 File Offset: 0x00008494
		internal MashupDiscovery(MashupDiscoveryKind kind, string functionName, DataSourceReference dataSourceReference, MashupPartitionCoordinate coordinate, string options = null, bool hasUnknownOptions = true, string metadata = null)
		{
			this.kind = kind;
			this.functionName = functionName;
			this.dataSourceReference = dataSourceReference;
			this.coordinate = coordinate;
			this.options = options;
			this.hasUnknownOptions = hasUnknownOptions;
			this.metadata = metadata;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000A2D1 File Offset: 0x000084D1
		internal static MashupDiscovery NewDependency(MashupDiscoveryKind dependencyKind, string moduleName, string version)
		{
			return new MashupDiscovery(dependencyKind, moduleName, version);
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000A2DB File Offset: 0x000084DB
		public MashupDiscoveryKind Kind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000A2E3 File Offset: 0x000084E3
		public string FunctionName
		{
			get
			{
				return this.functionName;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000A2EB File Offset: 0x000084EB
		public string ModuleName
		{
			get
			{
				return this.moduleName;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000A2F3 File Offset: 0x000084F3
		public string ModuleVersion
		{
			get
			{
				return this.moduleVersion;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000A2FB File Offset: 0x000084FB
		public DataSourceReference DataSourceReference
		{
			get
			{
				return this.dataSourceReference;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000A303 File Offset: 0x00008503
		public string Options
		{
			get
			{
				return this.options;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000A30B File Offset: 0x0000850B
		public bool HasUnknownOptions
		{
			get
			{
				return this.hasUnknownOptions;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000A313 File Offset: 0x00008513
		public string Metadata
		{
			get
			{
				return this.metadata;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000A31B File Offset: 0x0000851B
		public MashupPartitionCoordinate Coordinate
		{
			get
			{
				return this.coordinate;
			}
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000A324 File Offset: 0x00008524
		public bool Equals(MashupDiscovery other)
		{
			return other != null && this.Kind == other.Kind && MashupDiscovery.AreEqual<string>(this.FunctionName, other.FunctionName) && this.HasUnknownOptions == other.HasUnknownOptions && MashupDiscovery.AreEqual<DataSourceReference>(this.DataSourceReference, other.DataSourceReference) && MashupDiscovery.AreEqual<string>(this.Options, other.Options) && MashupDiscovery.AreEqual<string>(this.ModuleName, other.ModuleName) && MashupDiscovery.AreEqual<string>(this.ModuleVersion, other.ModuleVersion) && this.Coordinate.Equals(other.Coordinate) && MashupDiscovery.AreEqual<string>(this.metadata, other.metadata);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000A3DB File Offset: 0x000085DB
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MashupDiscovery);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000A3EC File Offset: 0x000085EC
		public override int GetHashCode()
		{
			return this.Kind.GetHashCode() + 23 * MashupDiscovery.GetHashCode<string>(this.FunctionName ?? this.ModuleName) + 37 * MashupDiscovery.GetHashCode<DataSourceReference>(this.DataSourceReference);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000A435 File Offset: 0x00008635
		private static int GetHashCode<T>(T value) where T : class, IEquatable<T>
		{
			if (value != null)
			{
				return value.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000A44C File Offset: 0x0000864C
		private static bool AreEqual<T>(T value1, T value2) where T : class, IEquatable<T>
		{
			return (value1 == null && value2 == null) || (value1 != null && value1.Equals(value2));
		}

		// Token: 0x04000134 RID: 308
		private readonly MashupDiscoveryKind kind;

		// Token: 0x04000135 RID: 309
		private readonly string functionName;

		// Token: 0x04000136 RID: 310
		private readonly DataSourceReference dataSourceReference;

		// Token: 0x04000137 RID: 311
		private readonly MashupPartitionCoordinate coordinate;

		// Token: 0x04000138 RID: 312
		private readonly string options;

		// Token: 0x04000139 RID: 313
		private readonly bool hasUnknownOptions;

		// Token: 0x0400013A RID: 314
		private readonly string metadata;

		// Token: 0x0400013B RID: 315
		private readonly string moduleName;

		// Token: 0x0400013C RID: 316
		private readonly string moduleVersion;
	}
}
