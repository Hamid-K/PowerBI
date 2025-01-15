using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Microsoft.Spatial
{
	// Token: 0x02000034 RID: 52
	public class CoordinateSystem
	{
		// Token: 0x06000134 RID: 308 RVA: 0x00003B84 File Offset: 0x00001D84
		[SuppressMessage("Microsoft.Performance", "CA1810", Justification = "Static Constructor required")]
		static CoordinateSystem()
		{
			CoordinateSystem.AddRef(CoordinateSystem.DefaultGeometry);
			CoordinateSystem.AddRef(CoordinateSystem.DefaultGeography);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00003BE4 File Offset: 0x00001DE4
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "epsg", Justification = "This is not hungarian notation, but the widley accepted abreviation")]
		internal CoordinateSystem(int epsgId, string name, CoordinateSystem.Topology topology)
		{
			this.topology = topology;
			this.EpsgId = new int?(epsgId);
			this.Name = name;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00003C06 File Offset: 0x00001E06
		// (set) Token: 0x06000137 RID: 311 RVA: 0x00003C0E File Offset: 0x00001E0E
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Epsg", Justification = "This is not hungarian notation, but the widley accepted abreviation")]
		public int? EpsgId { get; internal set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00003C18 File Offset: 0x00001E18
		public string Id
		{
			get
			{
				return this.EpsgId.Value.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00003C40 File Offset: 0x00001E40
		// (set) Token: 0x0600013A RID: 314 RVA: 0x00003C48 File Offset: 0x00001E48
		public string Name { get; internal set; }

		// Token: 0x0600013B RID: 315 RVA: 0x00003C51 File Offset: 0x00001E51
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "epsg", Justification = "This is not hungarian notation, but the widley accepted abreviation")]
		public static CoordinateSystem Geography(int? epsgId)
		{
			if (epsgId == null)
			{
				return CoordinateSystem.DefaultGeography;
			}
			return CoordinateSystem.GetOrCreate(epsgId.Value, CoordinateSystem.Topology.Geography);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00003C6F File Offset: 0x00001E6F
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "epsg", Justification = "This is not hungarian notation, but the widley accepted abreviation")]
		public static CoordinateSystem Geometry(int? epsgId)
		{
			if (epsgId == null)
			{
				return CoordinateSystem.DefaultGeometry;
			}
			return CoordinateSystem.GetOrCreate(epsgId.Value, CoordinateSystem.Topology.Geometry);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00003C8D File Offset: 0x00001E8D
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}CoordinateSystem(EpsgId={1})", new object[] { this.topology, this.EpsgId });
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00003CC0 File Offset: 0x00001EC0
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Wkt", Justification = "This is not hungarian notation, but the widley accepted abreviation")]
		public string ToWktId()
		{
			return "SRID=" + this.EpsgId + ";";
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00003CDC File Offset: 0x00001EDC
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CoordinateSystem);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00003CEC File Offset: 0x00001EEC
		public bool Equals(CoordinateSystem other)
		{
			return other != null && (this == other || (object.Equals(other.topology, this.topology) && other.EpsgId.Equals(this.EpsgId)));
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00003D44 File Offset: 0x00001F44
		public override int GetHashCode()
		{
			return (this.topology.GetHashCode() * 397) ^ ((this.EpsgId != null) ? this.EpsgId.Value : 0);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00003D8A File Offset: 0x00001F8A
		internal bool TopologyIs(CoordinateSystem.Topology expected)
		{
			return this.topology == expected;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00003D98 File Offset: 0x00001F98
		private static CoordinateSystem GetOrCreate(int epsgId, CoordinateSystem.Topology topology)
		{
			object obj = CoordinateSystem.referencesLock;
			CoordinateSystem coordinateSystem;
			lock (obj)
			{
				if (CoordinateSystem.References.TryGetValue(CoordinateSystem.KeyFor(epsgId, topology), ref coordinateSystem))
				{
					return coordinateSystem;
				}
				coordinateSystem = new CoordinateSystem(epsgId, "ID " + epsgId, topology);
				CoordinateSystem.AddRef(coordinateSystem);
			}
			return coordinateSystem;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00003E04 File Offset: 0x00002004
		private static void AddRef(CoordinateSystem coords)
		{
			CoordinateSystem.References.Add(CoordinateSystem.KeyFor(coords.EpsgId.Value, coords.topology), coords);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00003E35 File Offset: 0x00002035
		private static CompositeKey<int, CoordinateSystem.Topology> KeyFor(int epsgId, CoordinateSystem.Topology topology)
		{
			return new CompositeKey<int, CoordinateSystem.Topology>(epsgId, topology);
		}

		// Token: 0x04000029 RID: 41
		public static readonly CoordinateSystem DefaultGeometry = new CoordinateSystem(0, "Unitless Plane", CoordinateSystem.Topology.Geometry);

		// Token: 0x0400002A RID: 42
		public static readonly CoordinateSystem DefaultGeography = new CoordinateSystem(4326, "WGS84", CoordinateSystem.Topology.Geography);

		// Token: 0x0400002B RID: 43
		private static readonly Dictionary<CompositeKey<int, CoordinateSystem.Topology>, CoordinateSystem> References = new Dictionary<CompositeKey<int, CoordinateSystem.Topology>, CoordinateSystem>(EqualityComparer<CompositeKey<int, CoordinateSystem.Topology>>.Default);

		// Token: 0x0400002C RID: 44
		private static readonly object referencesLock = new object();

		// Token: 0x0400002D RID: 45
		private CoordinateSystem.Topology topology;

		// Token: 0x02000075 RID: 117
		internal enum Topology
		{
			// Token: 0x04000103 RID: 259
			Geography,
			// Token: 0x04000104 RID: 260
			Geometry
		}
	}
}
