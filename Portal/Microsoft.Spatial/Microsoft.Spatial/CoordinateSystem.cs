using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Microsoft.Spatial
{
	// Token: 0x02000039 RID: 57
	public class CoordinateSystem
	{
		// Token: 0x060001AA RID: 426 RVA: 0x00004850 File Offset: 0x00002A50
		[SuppressMessage("Microsoft.Performance", "CA1810", Justification = "Static Constructor required")]
		static CoordinateSystem()
		{
			CoordinateSystem.AddRef(CoordinateSystem.DefaultGeometry);
			CoordinateSystem.AddRef(CoordinateSystem.DefaultGeography);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000048B0 File Offset: 0x00002AB0
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "epsg", Justification = "This is not hungarian notation, but the widley accepted abreviation")]
		internal CoordinateSystem(int epsgId, string name, CoordinateSystem.Topology topology)
		{
			this.topology = topology;
			this.EpsgId = new int?(epsgId);
			this.Name = name;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060001AC RID: 428 RVA: 0x000048D2 File Offset: 0x00002AD2
		// (set) Token: 0x060001AD RID: 429 RVA: 0x000048DA File Offset: 0x00002ADA
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Epsg", Justification = "This is not hungarian notation, but the widley accepted abreviation")]
		public int? EpsgId { get; internal set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060001AE RID: 430 RVA: 0x000048E4 File Offset: 0x00002AE4
		public string Id
		{
			get
			{
				return this.EpsgId.Value.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000490C File Offset: 0x00002B0C
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x00004914 File Offset: 0x00002B14
		public string Name { get; internal set; }

		// Token: 0x060001B1 RID: 433 RVA: 0x0000491D File Offset: 0x00002B1D
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "epsg", Justification = "This is not hungarian notation, but the widley accepted abreviation")]
		public static CoordinateSystem Geography(int? epsgId)
		{
			if (epsgId == null)
			{
				return CoordinateSystem.DefaultGeography;
			}
			return CoordinateSystem.GetOrCreate(epsgId.Value, CoordinateSystem.Topology.Geography);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000493B File Offset: 0x00002B3B
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "epsg", Justification = "This is not hungarian notation, but the widley accepted abreviation")]
		public static CoordinateSystem Geometry(int? epsgId)
		{
			if (epsgId == null)
			{
				return CoordinateSystem.DefaultGeometry;
			}
			return CoordinateSystem.GetOrCreate(epsgId.Value, CoordinateSystem.Topology.Geometry);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00004959 File Offset: 0x00002B59
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}CoordinateSystem(EpsgId={1})", new object[] { this.topology, this.EpsgId });
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000498C File Offset: 0x00002B8C
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Wkt", Justification = "This is not hungarian notation, but the widley accepted abreviation")]
		public string ToWktId()
		{
			return "SRID=" + this.EpsgId + ";";
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000049A8 File Offset: 0x00002BA8
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CoordinateSystem);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000049B8 File Offset: 0x00002BB8
		public bool Equals(CoordinateSystem other)
		{
			return other != null && (this == other || (object.Equals(other.topology, this.topology) && other.EpsgId.Equals(this.EpsgId)));
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00004A10 File Offset: 0x00002C10
		public override int GetHashCode()
		{
			return (this.topology.GetHashCode() * 397) ^ ((this.EpsgId != null) ? this.EpsgId.Value : 0);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00004A56 File Offset: 0x00002C56
		internal bool TopologyIs(CoordinateSystem.Topology expected)
		{
			return this.topology == expected;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00004A64 File Offset: 0x00002C64
		private static CoordinateSystem GetOrCreate(int epsgId, CoordinateSystem.Topology topology)
		{
			object obj = CoordinateSystem.referencesLock;
			CoordinateSystem coordinateSystem;
			lock (obj)
			{
				if (CoordinateSystem.References.TryGetValue(CoordinateSystem.KeyFor(epsgId, topology), out coordinateSystem))
				{
					return coordinateSystem;
				}
				coordinateSystem = new CoordinateSystem(epsgId, "ID " + epsgId, topology);
				CoordinateSystem.AddRef(coordinateSystem);
			}
			return coordinateSystem;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00004AD8 File Offset: 0x00002CD8
		private static void AddRef(CoordinateSystem coords)
		{
			CoordinateSystem.References.Add(CoordinateSystem.KeyFor(coords.EpsgId.Value, coords.topology), coords);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00004B09 File Offset: 0x00002D09
		private static CompositeKey<int, CoordinateSystem.Topology> KeyFor(int epsgId, CoordinateSystem.Topology topology)
		{
			return new CompositeKey<int, CoordinateSystem.Topology>(epsgId, topology);
		}

		// Token: 0x04000036 RID: 54
		public static readonly CoordinateSystem DefaultGeometry = new CoordinateSystem(0, "Unitless Plane", CoordinateSystem.Topology.Geometry);

		// Token: 0x04000037 RID: 55
		public static readonly CoordinateSystem DefaultGeography = new CoordinateSystem(4326, "WGS84", CoordinateSystem.Topology.Geography);

		// Token: 0x04000038 RID: 56
		private static readonly Dictionary<CompositeKey<int, CoordinateSystem.Topology>, CoordinateSystem> References = new Dictionary<CompositeKey<int, CoordinateSystem.Topology>, CoordinateSystem>(EqualityComparer<CompositeKey<int, CoordinateSystem.Topology>>.Default);

		// Token: 0x04000039 RID: 57
		private static readonly object referencesLock = new object();

		// Token: 0x0400003A RID: 58
		private CoordinateSystem.Topology topology;

		// Token: 0x02000081 RID: 129
		internal enum Topology
		{
			// Token: 0x0400011F RID: 287
			Geography,
			// Token: 0x04000120 RID: 288
			Geometry
		}
	}
}
