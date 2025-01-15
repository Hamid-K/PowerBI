using System;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000CF5 RID: 3317
	internal static class CubeHierarchiesMetadata
	{
		// Token: 0x060059FE RID: 23038 RVA: 0x0013A970 File Offset: 0x00138B70
		public static TypeValue AddHierarchy(TypeValue type, string hierarchyId, string hierarchyCaption, int level, string levelCaption)
		{
			return CubeHierarchiesMetadata.AddHierarchies(type, new CubeHierarchiesMetadata.HierarchyInfo[]
			{
				new CubeHierarchiesMetadata.HierarchyInfo
				{
					hierarchyId = hierarchyId,
					hierarchyCaption = hierarchyCaption,
					level = level,
					levelCaption = levelCaption
				}
			});
		}

		// Token: 0x060059FF RID: 23039 RVA: 0x0013A9BC File Offset: 0x00138BBC
		public static TypeValue AddHierarchies(TypeValue type, params CubeHierarchiesMetadata.HierarchyInfo[] hierarchies)
		{
			string[] array = new string[hierarchies.Length];
			Value[] array2 = new Value[hierarchies.Length];
			for (int i = 0; i < hierarchies.Length; i++)
			{
				array[i] = hierarchies[i].hierarchyId;
				array2[i] = CubeHierarchiesMetadata.AddCaption(NumberValue.New(hierarchies[i].level), hierarchies[i].levelCaption);
			}
			RecordValue recordValue = RecordValue.New(Keys.New(array), array2);
			CubeHierarchiesMetadata.TypeCacheKey typeCacheKey = new CubeHierarchiesMetadata.TypeCacheKey(hierarchies);
			TypeValue typeValue;
			if (!CubeHierarchiesMetadata.hierarchyMetadataTypeCache.TryGetValue(typeCacheKey, out typeValue))
			{
				typeValue = recordValue.Type;
				Value[] array3 = new Value[hierarchies.Length];
				for (int j = 0; j < hierarchies.Length; j++)
				{
					array3[j] = TextValue.New(hierarchies[j].hierarchyCaption);
				}
				RecordValue recordValue2 = RecordValue.New(CubeHierarchiesMetadata.DocumentationFieldCaptionKeys, new Value[] { RecordValue.New(recordValue.Keys, array3) });
				typeValue = BinaryOperator.AddMeta.Invoke(typeValue, recordValue2).AsType;
				if (hierarchies.Length != 0 && hierarchies[0].dimensionId != null)
				{
					Value[] array4 = new Value[hierarchies.Length];
					for (int k = 0; k < hierarchies.Length; k++)
					{
						array4[k] = CubeHierarchiesMetadata.AddCaption(TextValue.New(hierarchies[k].dimensionId), hierarchies[k].dimensionCaption);
					}
					RecordValue recordValue3 = RecordValue.New(CubeHierarchiesMetadata.CubeDimensionsKeys, new Value[] { RecordValue.New(recordValue.Keys, array4) });
					typeValue = BinaryOperator.AddMeta.Invoke(typeValue, recordValue3).AsType;
				}
			}
			RecordValue recordValue4 = RecordValue.New(CubeHierarchiesMetadata.CubeHierarchiesKeys, new Value[] { recordValue.ReplaceType(typeValue) });
			return BinaryOperator.AddMeta.Invoke(type, recordValue4).AsType;
		}

		// Token: 0x06005A00 RID: 23040 RVA: 0x0013AB7C File Offset: 0x00138D7C
		private static Value AddCaption(Value value, string caption)
		{
			RecordValue recordValue = RecordValue.New(CubeHierarchiesMetadata.DocumentationCaptionKeys, new Value[] { TextValue.New(caption) });
			return BinaryOperator.AddMeta.Invoke(value, recordValue);
		}

		// Token: 0x04003239 RID: 12857
		private static readonly Keys CubeHierarchiesKeys = Keys.New("Cube.Hierarchies");

		// Token: 0x0400323A RID: 12858
		private static readonly Keys CubeDimensionsKeys = Keys.New("Cube.Dimensions");

		// Token: 0x0400323B RID: 12859
		private static readonly Keys DocumentationCaptionKeys = Keys.New("Documentation.Caption");

		// Token: 0x0400323C RID: 12860
		private static readonly Keys DocumentationFieldCaptionKeys = Keys.New("Documentation.FieldCaption");

		// Token: 0x0400323D RID: 12861
		private static readonly LruCache<CubeHierarchiesMetadata.TypeCacheKey, TypeValue> hierarchyMetadataTypeCache = new LruCache<CubeHierarchiesMetadata.TypeCacheKey, TypeValue>(64, null);

		// Token: 0x02000CF6 RID: 3318
		public struct HierarchyInfo
		{
			// Token: 0x0400323E RID: 12862
			public string hierarchyId;

			// Token: 0x0400323F RID: 12863
			public string hierarchyCaption;

			// Token: 0x04003240 RID: 12864
			public string dimensionId;

			// Token: 0x04003241 RID: 12865
			public string dimensionCaption;

			// Token: 0x04003242 RID: 12866
			public int level;

			// Token: 0x04003243 RID: 12867
			public string levelCaption;
		}

		// Token: 0x02000CF7 RID: 3319
		private class TypeCacheKey : IEquatable<CubeHierarchiesMetadata.TypeCacheKey>
		{
			// Token: 0x06005A02 RID: 23042 RVA: 0x0013AC06 File Offset: 0x00138E06
			public TypeCacheKey(CubeHierarchiesMetadata.HierarchyInfo[] infos)
			{
				this.infos = infos;
			}

			// Token: 0x06005A03 RID: 23043 RVA: 0x0013AC18 File Offset: 0x00138E18
			public bool Equals(CubeHierarchiesMetadata.TypeCacheKey other)
			{
				bool flag = other != null && this.infos.Length == other.infos.Length;
				int num = 0;
				while (flag && num < this.infos.Length)
				{
					flag = CubeHierarchiesMetadata.TypeCacheKey.HierarchyEquals(ref this.infos[num], ref other.infos[num]);
					num++;
				}
				return flag;
			}

			// Token: 0x06005A04 RID: 23044 RVA: 0x0013AC73 File Offset: 0x00138E73
			public override bool Equals(object other)
			{
				return this.Equals(other as CubeHierarchiesMetadata.TypeCacheKey);
			}

			// Token: 0x06005A05 RID: 23045 RVA: 0x0013AC84 File Offset: 0x00138E84
			public override int GetHashCode()
			{
				int num = 5011 * this.infos.Length;
				for (int i = 0; i < this.infos.Length; i++)
				{
					num += CubeHierarchiesMetadata.TypeCacheKey.GetHierarchyHashCode(ref this.infos[i]);
				}
				return num;
			}

			// Token: 0x06005A06 RID: 23046 RVA: 0x0013ACC8 File Offset: 0x00138EC8
			private static bool HierarchyEquals(ref CubeHierarchiesMetadata.HierarchyInfo x, ref CubeHierarchiesMetadata.HierarchyInfo y)
			{
				return x.hierarchyId == y.hierarchyId && x.hierarchyCaption == y.hierarchyCaption && x.dimensionId == y.dimensionId && x.dimensionCaption == y.dimensionCaption;
			}

			// Token: 0x06005A07 RID: 23047 RVA: 0x0013AD24 File Offset: 0x00138F24
			private static int GetHierarchyHashCode(ref CubeHierarchiesMetadata.HierarchyInfo info)
			{
				int num = info.hierarchyId.GetHashCode() + 37 * info.hierarchyCaption.GetHashCode();
				if (info.dimensionId != null)
				{
					num = 5011 * num + info.dimensionId.GetHashCode() + 37 * info.dimensionCaption.GetHashCode();
				}
				return num;
			}

			// Token: 0x04003244 RID: 12868
			private readonly CubeHierarchiesMetadata.HierarchyInfo[] infos;
		}
	}
}
