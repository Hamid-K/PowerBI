using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Linq;

namespace Microsoft.Lucia.Core.TermIndex
{
	// Token: 0x0200015F RID: 351
	[ImmutableObject(true)]
	public sealed class DataIndexVersion
	{
		// Token: 0x060006EF RID: 1775 RVA: 0x0000BE70 File Offset: 0x0000A070
		private DataIndexVersion(Version value, IEnumerable<Version> compatibleVersions, [Nullable] Version next, [Nullable] IDataIndexMetadataUpgradeTransform upgradeTransform)
		{
			this.Value = value;
			this.Next = next;
			this.UpgradeTransform = upgradeTransform;
			this.CompatibleVersions = ((compatibleVersions == null) ? ImmutableHashSet<Version>.Empty : compatibleVersions.ToImmutableHashSet<Version>());
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x0000BEA4 File Offset: 0x0000A0A4
		public Version Value { get; }

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x0000BEAC File Offset: 0x0000A0AC
		[Nullable]
		internal Version Next { get; }

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060006F2 RID: 1778 RVA: 0x0000BEB4 File Offset: 0x0000A0B4
		[Nullable]
		internal IDataIndexMetadataUpgradeTransform UpgradeTransform { get; }

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x0000BEBC File Offset: 0x0000A0BC
		internal ImmutableHashSet<Version> CompatibleVersions { get; }

		// Token: 0x060006F4 RID: 1780 RVA: 0x0000BEC4 File Offset: 0x0000A0C4
		public bool CompatibleWith(Version version)
		{
			return this.Value == version || this.CompatibleVersions.Contains(version);
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0000BEE4 File Offset: 0x0000A0E4
		private static IReadOnlyDictionary<Version, DataIndexVersion> CreateSupportedVersionsMap(IReadOnlyList<Version> versionSequence, IReadOnlyList<IDataIndexMetadataUpgradeTransform> versionTransforms, IReadOnlyList<global::System.ValueTuple<Version, Version>> compatibleVersions)
		{
			Dictionary<Version, DataIndexVersion> dictionary = new Dictionary<Version, DataIndexVersion>();
			IEnumerator<Version> enumerator = versionSequence.GetEnumerator();
			if (!enumerator.MoveNext())
			{
				return dictionary;
			}
			Version version = enumerator.Current;
			Version version2 = null;
			IEnumerator<IDataIndexMetadataUpgradeTransform> enumerator2 = versionTransforms.GetEnumerator();
			IDataIndexMetadataUpgradeTransform dataIndexMetadataUpgradeTransform = (enumerator2.MoveNext() ? enumerator2.Current : null);
			IDataIndexMetadataUpgradeTransform dataIndexMetadataUpgradeTransform2 = null;
			while (enumerator.MoveNext())
			{
				version2 = enumerator.Current;
				if (dataIndexMetadataUpgradeTransform != null && dataIndexMetadataUpgradeTransform.SourceVersion == version)
				{
					dataIndexMetadataUpgradeTransform2 = dataIndexMetadataUpgradeTransform;
				}
				dictionary.Add(version, DataIndexVersion.CreateDataIndexVersion(version, compatibleVersions, version2, dataIndexMetadataUpgradeTransform2));
				version = enumerator.Current;
				version2 = null;
				if (dataIndexMetadataUpgradeTransform2 != null)
				{
					dataIndexMetadataUpgradeTransform2 = null;
					dataIndexMetadataUpgradeTransform = (enumerator2.MoveNext() ? enumerator2.Current : null);
				}
			}
			dictionary.Add(version, DataIndexVersion.CreateDataIndexVersion(version, compatibleVersions, version2, dataIndexMetadataUpgradeTransform2));
			return dictionary;
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0000BFA4 File Offset: 0x0000A1A4
		private static DataIndexVersion CreateDataIndexVersion(Version version, IReadOnlyList<global::System.ValueTuple<Version, Version>> compatibleVersions, Version nextVersion, IDataIndexMetadataUpgradeTransform upgradeTransform)
		{
			return new DataIndexVersion(version, (from e in compatibleVersions
				where e.Item1 == version
				select e.Item2).Union(from e in compatibleVersions
				where e.Item2 == version
				select e.Item1), nextVersion, upgradeTransform);
		}

		// Token: 0x0400069C RID: 1692
		public static readonly Version V0_4 = new Version(0, 4);

		// Token: 0x0400069D RID: 1693
		public static readonly Version V1_0 = new Version(1, 0);

		// Token: 0x0400069E RID: 1694
		public static readonly Version V1_1 = new Version(1, 1);

		// Token: 0x0400069F RID: 1695
		public static readonly Version V1_2 = new Version(1, 2);

		// Token: 0x040006A0 RID: 1696
		public static readonly Version V2_0 = new Version(2, 0);

		// Token: 0x040006A1 RID: 1697
		public static readonly Version Default = DataIndexVersion.V1_1;

		// Token: 0x040006A2 RID: 1698
		public static readonly Version Latest = DataIndexVersion.V2_0;

		// Token: 0x040006A3 RID: 1699
		internal static IReadOnlyDictionary<Version, DataIndexVersion> SupportedVersions = DataIndexVersion.CreateSupportedVersionsMap(new Version[]
		{
			DataIndexVersion.V0_4,
			DataIndexVersion.V1_0,
			DataIndexVersion.V1_1,
			DataIndexVersion.V1_2,
			DataIndexVersion.V2_0
		}, new IDataIndexMetadataUpgradeTransform[]
		{
			new DataIndexMetadataVersionTransforms.V1_0ToV1_1()
		}, new global::System.ValueTuple<Version, Version>[]
		{
			new global::System.ValueTuple<Version, Version>(DataIndexVersion.V1_0, DataIndexVersion.V1_1)
		});
	}
}
