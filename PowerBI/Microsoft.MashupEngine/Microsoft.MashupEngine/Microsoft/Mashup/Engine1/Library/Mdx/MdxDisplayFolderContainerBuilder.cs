using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x02000996 RID: 2454
	internal abstract class MdxDisplayFolderContainerBuilder
	{
		// Token: 0x06004654 RID: 18004 RVA: 0x000EC1C1 File Offset: 0x000EA3C1
		public MdxDisplayFolderContainerBuilder(bool supportsProperties)
		{
			this.supportsProperties = supportsProperties;
		}

		// Token: 0x06004655 RID: 18005
		public abstract void AddValue(string id, string name, TextValue kind, Value value);

		// Token: 0x06004656 RID: 18006
		public abstract void AddHierarchy(string id, string name, TableValue hierarchyFolder);

		// Token: 0x06004657 RID: 18007 RVA: 0x000EC1D0 File Offset: 0x000EA3D0
		public void Add(MdxMeasure measure)
		{
			IdentifierCubeExpression identifierCubeExpression = new IdentifierCubeExpression(measure.MdxIdentifier);
			TypeValue typeValue = measure.Type.GetTypeValue();
			MeasureValue measureValue = new MeasureValue(identifierCubeExpression, typeValue);
			this.AddValue(measure.MdxIdentifier, measure.Caption, CubeObjectTableBuilder.MeasureKind, measureValue);
		}

		// Token: 0x06004658 RID: 18008 RVA: 0x000EC214 File Offset: 0x000EA414
		public void Add(MdxKpi kpi, bool includeTrend)
		{
			MdxDisplayFolderBuilder mdxDisplayFolderBuilder = new MdxDisplayFolderBuilder();
			mdxDisplayFolderBuilder.AddKpiMeasure(kpi.ValueMeasure, Strings.AnalysisServicesKpiValue);
			mdxDisplayFolderBuilder.AddKpiMeasure(kpi.GoalMeasure, Strings.AnalysisServicesKpiGoal);
			mdxDisplayFolderBuilder.AddKpiMeasure(kpi.StatusMeasure, Strings.AnalysisServicesKpiStatus);
			if (includeTrend)
			{
				mdxDisplayFolderBuilder.AddKpiMeasure(kpi.TrendMeasure, Strings.AnalysisServicesKpiTrend);
			}
			this.AddValue(kpi.MdxIdentifier, kpi.Caption, CubeObjectTableBuilder.KpiKind, mdxDisplayFolderBuilder.ToTable());
		}

		// Token: 0x06004659 RID: 18009 RVA: 0x000EC2A0 File Offset: 0x000EA4A0
		public void Add(MdxHierarchy hierarchy)
		{
			if (hierarchy.Levels.Count > 0)
			{
				if (hierarchy.Type == MdxHierarchyType.Attribute)
				{
					this.AddDimensionAttributeAndProperties(hierarchy.Levels[0]);
					return;
				}
				MdxDisplayFolderBuilder mdxDisplayFolderBuilder = new MdxDisplayFolderBuilder();
				foreach (MdxLevel mdxLevel in hierarchy.Levels)
				{
					mdxDisplayFolderBuilder.AddValue(mdxLevel.MdxIdentifier, mdxLevel.Caption, CubeObjectTableBuilder.DimensionAttributeKind, Value.Null);
				}
				this.AddHierarchy(hierarchy.MdxIdentifier, hierarchy.Caption, mdxDisplayFolderBuilder.ToTable());
			}
		}

		// Token: 0x0600465A RID: 18010 RVA: 0x000EC34C File Offset: 0x000EA54C
		public void AddWithPropertiesPerLevel(MdxHierarchy hierarchy)
		{
			if (hierarchy.Levels.Count > 0)
			{
				if (hierarchy.Type == MdxHierarchyType.Attribute)
				{
					using (IEnumerator<MdxLevel> enumerator = hierarchy.Levels.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							MdxLevel mdxLevel = enumerator.Current;
							this.AddAttributeAndProperties(this, mdxLevel, CubeObjectTableBuilder.LevelFolderKind);
						}
						return;
					}
				}
				MdxDisplayFolderBuilder mdxDisplayFolderBuilder = new MdxDisplayFolderBuilder();
				foreach (MdxLevel mdxLevel2 in hierarchy.Levels)
				{
					this.AddAttributeAndProperties(mdxDisplayFolderBuilder, mdxLevel2, CubeObjectTableBuilder.LevelFolderKind);
				}
				this.AddHierarchy(hierarchy.MdxIdentifier, hierarchy.Caption, mdxDisplayFolderBuilder.ToTable());
			}
		}

		// Token: 0x0600465B RID: 18011 RVA: 0x000EC418 File Offset: 0x000EA618
		private void AddAttributeAndProperties(MdxDisplayFolderContainerBuilder hierarchyFolder, MdxLevel level, TextValue kind)
		{
			if (level.Properties.Count > 0)
			{
				hierarchyFolder.AddValue(level.MdxIdentifier, level.Caption, kind, CubeObjectTableBuilder.NewLazy(delegate(CubeObjectTableBuilder populate)
				{
					foreach (MdxProperty mdxProperty in level.Properties.Where((MdxProperty p) => p.PropertyKind != MdxPropertyKind.MemberCaption))
					{
						populate.AddObject(mdxProperty.Name, mdxProperty.Caption, CubeObjectTableBuilder.DimensionPropertyKind, Value.Null);
					}
					populate.AddObject(level.MdxIdentifier, level.Caption, CubeObjectTableBuilder.DimensionAttributeKind, Value.Null);
				}));
			}
		}

		// Token: 0x0600465C RID: 18012 RVA: 0x000EC474 File Offset: 0x000EA674
		private void AddDimensionAttributeAndProperties(MdxLevel level)
		{
			if (this.supportsProperties)
			{
				this.AddValue("Properties", "Properties", CubeObjectTableBuilder.DimensionPropertyFolderKind, CubeObjectTableBuilder.NewLazy(delegate(CubeObjectTableBuilder populate)
				{
					foreach (MdxProperty mdxProperty in level.Properties.Where((MdxProperty p) => p.PropertyKind != MdxPropertyKind.MemberCaption))
					{
						populate.AddObject(mdxProperty.Name, mdxProperty.Caption, CubeObjectTableBuilder.DimensionPropertyKind, Value.Null);
					}
				}));
			}
			this.AddValue(level.MdxIdentifier, level.Caption, CubeObjectTableBuilder.DimensionAttributeKind, Value.Null);
		}

		// Token: 0x04002534 RID: 9524
		private readonly bool supportsProperties;
	}
}
