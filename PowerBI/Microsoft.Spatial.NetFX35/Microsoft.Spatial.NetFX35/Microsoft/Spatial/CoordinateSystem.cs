using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Microsoft.Spatial
{
	// Token: 0x02000039 RID: 57
	public class CoordinateSystem
	{
		// Token: 0x0600017C RID: 380 RVA: 0x000045E0 File Offset: 0x000027E0
		[SuppressMessage("Microsoft.MSInternal", "CA908", Justification = "generic of int required")]
		[SuppressMessage("Microsoft.Performance", "CA1810", Justification = "Static Constructor required")]
		static CoordinateSystem()
		{
			CoordinateSystem.AddRef(CoordinateSystem.DefaultGeometry);
			CoordinateSystem.AddRef(CoordinateSystem.DefaultGeography);
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00004640 File Offset: 0x00002840
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "epsg", Justification = "This is not hungarian notation, but the widley accepted abreviation")]
		internal CoordinateSystem(int epsgId, string name, CoordinateSystem.Topology topology)
		{
			this.topology = topology;
			this.EpsgId = new int?(epsgId);
			this.Name = name;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00004662 File Offset: 0x00002862
		// (set) Token: 0x0600017F RID: 383 RVA: 0x0000466A File Offset: 0x0000286A
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Epsg", Justification = "This is not hungarian notation, but the widley accepted abreviation")]
		public int? EpsgId { get; internal set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00004674 File Offset: 0x00002874
		public string Id
		{
			get
			{
				return this.EpsgId.Value.ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000181 RID: 385 RVA: 0x0000469C File Offset: 0x0000289C
		// (set) Token: 0x06000182 RID: 386 RVA: 0x000046A4 File Offset: 0x000028A4
		public string Name { get; internal set; }

		// Token: 0x06000183 RID: 387 RVA: 0x000046AD File Offset: 0x000028AD
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "epsg", Justification = "This is not hungarian notation, but the widley accepted abreviation")]
		public static CoordinateSystem Geography(int? epsgId)
		{
			if (epsgId == null)
			{
				return CoordinateSystem.DefaultGeography;
			}
			return CoordinateSystem.GetOrCreate(epsgId.Value, CoordinateSystem.Topology.Geography);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000046CB File Offset: 0x000028CB
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "epsg", Justification = "This is not hungarian notation, but the widley accepted abreviation")]
		public static CoordinateSystem Geometry(int? epsgId)
		{
			if (epsgId == null)
			{
				return CoordinateSystem.DefaultGeometry;
			}
			return CoordinateSystem.GetOrCreate(epsgId.Value, CoordinateSystem.Topology.Geometry);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000046EC File Offset: 0x000028EC
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}CoordinateSystem(EpsgId={1})", new object[] { this.topology, this.EpsgId });
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000472C File Offset: 0x0000292C
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Wkt", Justification = "This is not hungarian notation, but the widley accepted abreviation")]
		public string ToWktId()
		{
			return "SRID=" + this.EpsgId + ";";
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00004748 File Offset: 0x00002948
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CoordinateSystem);
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00004758 File Offset: 0x00002958
		public bool Equals(CoordinateSystem other)
		{
			return !object.ReferenceEquals(null, other) && (object.ReferenceEquals(this, other) || (object.Equals(other.topology, this.topology) && other.EpsgId.Equals(this.EpsgId)));
		}

		// Token: 0x06000189 RID: 393 RVA: 0x000047BC File Offset: 0x000029BC
		public override int GetHashCode()
		{
			return (this.topology.GetHashCode() * 397) ^ ((this.EpsgId != null) ? this.EpsgId.Value : 0);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00004801 File Offset: 0x00002A01
		internal bool TopologyIs(CoordinateSystem.Topology expected)
		{
			return this.topology == expected;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000480C File Offset: 0x00002A0C
		private static CoordinateSystem GetOrCreate(int epsgId, CoordinateSystem.Topology topology)
		{
			CoordinateSystem coordinateSystem;
			lock (CoordinateSystem.referencesLock)
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

		// Token: 0x0600018C RID: 396 RVA: 0x00004878 File Offset: 0x00002A78
		private static void AddRef(CoordinateSystem coords)
		{
			CoordinateSystem.References.Add(CoordinateSystem.KeyFor(coords.EpsgId.Value, coords.topology), coords);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000048A9 File Offset: 0x00002AA9
		private static CompositeKey<int, CoordinateSystem.Topology> KeyFor(int epsgId, CoordinateSystem.Topology topology)
		{
			return new CompositeKey<int, CoordinateSystem.Topology>(epsgId, topology);
		}

		// Token: 0x0400002D RID: 45
		public static readonly CoordinateSystem DefaultGeometry = new CoordinateSystem(0, "Unitless Plane", CoordinateSystem.Topology.Geometry);

		// Token: 0x0400002E RID: 46
		public static readonly CoordinateSystem DefaultGeography = new CoordinateSystem(4326, "WGS84", CoordinateSystem.Topology.Geography);

		// Token: 0x0400002F RID: 47
		private static readonly Dictionary<CompositeKey<int, CoordinateSystem.Topology>, CoordinateSystem> References = new Dictionary<CompositeKey<int, CoordinateSystem.Topology>, CoordinateSystem>(EqualityComparer<CompositeKey<int, CoordinateSystem.Topology>>.Default);

		// Token: 0x04000030 RID: 48
		private static readonly object referencesLock = new object();

		// Token: 0x04000031 RID: 49
		private CoordinateSystem.Topology topology;

		// Token: 0x0200003A RID: 58
		internal enum Topology
		{
			// Token: 0x04000035 RID: 53
			Geography,
			// Token: 0x04000036 RID: 54
			Geometry
		}
	}
}
