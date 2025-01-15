using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Globalization;
using System.Linq;
using Microsoft.Mashup.Engine1.Library.Cube;

namespace Microsoft.Mashup.Engine1.Library.Mdx
{
	// Token: 0x0200098A RID: 2442
	internal class MdxCubeMetadataProviderCube : MdxCube
	{
		// Token: 0x0600460F RID: 17935 RVA: 0x000EB1AD File Offset: 0x000E93AD
		public MdxCubeMetadataProviderCube(Func<MdxCube, MdxCubeMetadataProvider> metadataCreator, string name)
		{
			this.metadata = metadataCreator(this);
			this.name = name;
		}

		// Token: 0x06004610 RID: 17936 RVA: 0x000EB1C9 File Offset: 0x000E93C9
		public MdxCubeMetadataProviderCube(MdxCubeMetadataProvider metadata, string name)
		{
			this.metadata = metadata;
			this.name = name;
		}

		// Token: 0x17001655 RID: 5717
		// (get) Token: 0x06004611 RID: 17937 RVA: 0x000EB1DF File Offset: 0x000E93DF
		public MdxCubeMetadataProvider Metadata
		{
			get
			{
				return this.metadata;
			}
		}

		// Token: 0x17001656 RID: 5718
		// (get) Token: 0x06004612 RID: 17938 RVA: 0x000EB1E7 File Offset: 0x000E93E7
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17001657 RID: 5719
		// (get) Token: 0x06004613 RID: 17939 RVA: 0x000EB1EF File Offset: 0x000E93EF
		public override string MdxIdentifier
		{
			get
			{
				return Microsoft.Mashup.Engine1.Library.Mdx.MdxIdentifier.QuotePart(this.name);
			}
		}

		// Token: 0x17001658 RID: 5720
		// (get) Token: 0x06004614 RID: 17940 RVA: 0x000EB1FC File Offset: 0x000E93FC
		public override IDictionary<string, MdxDimension> Dimensions
		{
			get
			{
				this.EnsureInitialized();
				return this.dimensions;
			}
		}

		// Token: 0x17001659 RID: 5721
		// (get) Token: 0x06004615 RID: 17941 RVA: 0x000EB20A File Offset: 0x000E940A
		public override IList<MdxMeasure> Measures
		{
			get
			{
				this.EnsureInitialized();
				return this.measures;
			}
		}

		// Token: 0x1700165A RID: 5722
		// (get) Token: 0x06004616 RID: 17942 RVA: 0x000EB218 File Offset: 0x000E9418
		public override IList<MdxKpi> Kpis
		{
			get
			{
				this.EnsureInitialized();
				return this.kpis;
			}
		}

		// Token: 0x1700165B RID: 5723
		// (get) Token: 0x06004617 RID: 17943 RVA: 0x00002105 File Offset: 0x00000305
		public virtual bool SupportsProperties
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004618 RID: 17944 RVA: 0x000EB226 File Offset: 0x000E9426
		public override bool TryGetDefaultMeasure(out string uniqueName)
		{
			uniqueName = this.defaultMeasure;
			return uniqueName != null;
		}

		// Token: 0x06004619 RID: 17945 RVA: 0x000EB238 File Offset: 0x000E9438
		public override MdxMeasureGroup GetMeasureGroup(string measureGroupName)
		{
			if (measureGroupName == null)
			{
				return null;
			}
			if (this.measureGroups == null)
			{
				this.LoadMeasureGroups();
			}
			MdxMeasureGroup mdxMeasureGroup;
			this.measureGroups.TryGetValue(measureGroupName, out mdxMeasureGroup);
			return mdxMeasureGroup;
		}

		// Token: 0x0600461A RID: 17946 RVA: 0x000EB268 File Offset: 0x000E9468
		protected override bool TryGetObjectFromMdxIdentifier(string uniqueName, out MdxCubeObject obj)
		{
			this.EnsureInitialized();
			return this.objects.TryGetValue(uniqueName, out obj);
		}

		// Token: 0x0600461B RID: 17947 RVA: 0x000EB27D File Offset: 0x000E947D
		private void EnsureInitialized()
		{
			if (!this.isInitialized)
			{
				this.objects = new Dictionary<string, MdxCubeObject>();
				this.LoadMeasures();
				this.LoadDimensions();
				this.isInitialized = true;
			}
		}

		// Token: 0x0600461C RID: 17948 RVA: 0x000EB2A8 File Offset: 0x000E94A8
		private void LoadMeasures()
		{
			List<MdxCellPropertyMetadata> list = this.metadata.GetCellProperties().ToList<MdxCellPropertyMetadata>();
			Dictionary<string, MdxMeasure> dictionary = new Dictionary<string, MdxMeasure>();
			foreach (MdxMeasureMetadata mdxMeasureMetadata in this.metadata.GetMeasures())
			{
				dictionary[mdxMeasureMetadata.UniqueName] = new MdxMeasure(this, mdxMeasureMetadata.UniqueName, mdxMeasureMetadata.Caption, mdxMeasureMetadata.DataType, mdxMeasureMetadata.MeasureGroupName, mdxMeasureMetadata.DisplayFolder, true, this.BuildCellProperties(list, mdxMeasureMetadata.UniqueName, mdxMeasureMetadata.Caption));
			}
			if (!this.metadata.TryGetDefaultMeasure(out this.defaultMeasure))
			{
				this.defaultMeasure = null;
			}
			this.kpis = new List<MdxKpi>();
			try
			{
				foreach (MdxKpiMetadata mdxKpiMetadata in this.metadata.GetKPIs())
				{
					MdxMeasure mdxMeasure = this.CreateKpiMeasure(mdxKpiMetadata.MeasureGroupName, mdxKpiMetadata.Goal);
					MdxMeasure mdxMeasure2 = this.CreateKpiMeasure(mdxKpiMetadata.MeasureGroupName, mdxKpiMetadata.Status);
					MdxMeasure mdxMeasure3 = this.CreateKpiMeasure(mdxKpiMetadata.MeasureGroupName, mdxKpiMetadata.Trend);
					MdxMeasure mdxMeasure4 = this.CreateKpiMeasure(mdxKpiMetadata.MeasureGroupName, mdxKpiMetadata.Value);
					MdxKpi mdxKpi = new MdxKpi(this, mdxKpiMetadata.Name, mdxKpiMetadata.Caption, mdxKpiMetadata.MeasureGroupName, mdxKpiMetadata.DisplayFolder, mdxMeasure, mdxMeasure2, mdxMeasure3, mdxMeasure4);
					this.kpis.Add(mdxKpi);
					MdxCubeMetadataProviderCube.AddMeasureIfNotPresent(dictionary, mdxMeasure);
					MdxCubeMetadataProviderCube.AddMeasureIfNotPresent(dictionary, mdxMeasure2);
					MdxCubeMetadataProviderCube.AddMeasureIfNotPresent(dictionary, mdxMeasure3);
					MdxCubeMetadataProviderCube.AddMeasureIfNotPresent(dictionary, mdxMeasure4);
				}
			}
			catch (NotSupportedException)
			{
			}
			this.measures = new List<MdxMeasure>(dictionary.Values);
			foreach (MdxMeasure mdxMeasure5 in this.measures)
			{
				this.AddObject(mdxMeasure5);
				if (mdxMeasure5.CellProperties != null)
				{
					foreach (MdxCellProperty mdxCellProperty in mdxMeasure5.CellProperties)
					{
						this.AddObject(mdxCellProperty);
					}
				}
			}
		}

		// Token: 0x0600461D RID: 17949 RVA: 0x000EB51C File Offset: 0x000E971C
		private IList<MdxCellProperty> BuildCellProperties(List<MdxCellPropertyMetadata> cellProperties, string uniqueName, string caption)
		{
			List<MdxCellProperty> list = new List<MdxCellProperty>();
			foreach (MdxCellPropertyMetadata mdxCellPropertyMetadata in cellProperties)
			{
				list.Add(new MdxCellProperty(mdxCellPropertyMetadata.Name, string.Format(CultureInfo.InvariantCulture, "{0}.{1}", uniqueName, mdxCellPropertyMetadata.Name), string.Format(CultureInfo.InvariantCulture, "{0}.{1}", caption, mdxCellPropertyMetadata.Caption), mdxCellPropertyMetadata.DataType));
			}
			return list;
		}

		// Token: 0x0600461E RID: 17950 RVA: 0x000EB5B0 File Offset: 0x000E97B0
		private static void AddMeasureIfNotPresent(Dictionary<string, MdxMeasure> measures, MdxMeasure measure)
		{
			if (!measures.ContainsKey(measure.MdxIdentifier))
			{
				measures[measure.MdxIdentifier] = measure;
			}
		}

		// Token: 0x0600461F RID: 17951 RVA: 0x000EB5CD File Offset: 0x000E97CD
		private MdxMeasure CreateKpiMeasure(string measureGroupName, string measureUniqueName)
		{
			return new MdxMeasure(this, measureUniqueName, measureUniqueName, OleDbType.Variant, measureGroupName, string.Empty, false, null);
		}

		// Token: 0x06004620 RID: 17952 RVA: 0x000EB5E4 File Offset: 0x000E97E4
		private void LoadDimensions()
		{
			Dictionary<string, MdxDimension> dictionary = new Dictionary<string, MdxDimension>();
			foreach (MdxDimensionMetadata mdxDimensionMetadata in this.metadata.GetDimensions())
			{
				MdxDimension mdxDimension = new MdxDimension(mdxDimensionMetadata.UniqueName, mdxDimensionMetadata.Caption, mdxDimensionMetadata.DimensionType, mdxDimensionMetadata.DataType, this);
				dictionary[mdxDimension.MdxIdentifier] = mdxDimension;
				this.AddObject(mdxDimension);
			}
			foreach (MdxHierarchyMetadata mdxHierarchyMetadata in this.metadata.GetHierarchies())
			{
				MdxDimension mdxDimension2;
				MdxHierarchyType mdxHierarchyType;
				if (dictionary.TryGetValue(mdxHierarchyMetadata.DimensionUniqueName, out mdxDimension2) && !mdxDimension2.Hierarchies.ContainsKey(mdxHierarchyMetadata.UniqueName) && this.TryGetHierarchyType(mdxHierarchyMetadata.Origin, out mdxHierarchyType))
				{
					MdxHierarchy mdxHierarchy = new MdxHierarchy(mdxHierarchyMetadata.UniqueName, mdxHierarchyMetadata.UniqueIdentifier, mdxHierarchyMetadata.Caption, mdxHierarchyMetadata.DisplayFolder, mdxHierarchyType, mdxDimension2, mdxHierarchyMetadata.IsVisible);
					mdxDimension2.Hierarchies[mdxHierarchyMetadata.UniqueName] = mdxHierarchy;
					this.AddObject(mdxHierarchy);
				}
			}
			foreach (MdxLevelMetadata mdxLevelMetadata in this.metadata.GetLevels())
			{
				MdxDimension mdxDimension3;
				MdxHierarchy mdxHierarchy2;
				if (dictionary.TryGetValue(mdxLevelMetadata.DimensionUniqueName, out mdxDimension3) && mdxDimension3.Hierarchies.TryGetValue(mdxLevelMetadata.HierarchyUniqueName, out mdxHierarchy2))
				{
					MdxLevel mdxLevel = new MdxLevel(mdxLevelMetadata.UniqueName, mdxLevelMetadata.Caption, mdxLevelMetadata.Number, mdxHierarchy2, new Func<MdxLevel, List<MdxProperty>>(this.BuildProperties));
					mdxHierarchy2.Levels.Add(mdxLevel);
					this.AddObject(mdxLevel);
				}
			}
			this.dimensions = dictionary;
		}

		// Token: 0x06004621 RID: 17953 RVA: 0x000EB7E0 File Offset: 0x000E99E0
		private List<MdxProperty> BuildProperties(MdxLevel level)
		{
			List<MdxProperty> list = new List<MdxProperty>();
			Dictionary<string, MdxProperty> dictionary = new Dictionary<string, MdxProperty>();
			MdxProperty mdxProperty = new MdxProperty(MdxPropertyKind.MemberUniqueName, MdxMemberProperties.QuotedMemberUniqueName, level.MdxIdentifier + "." + MdxMemberProperties.QuotedMemberUniqueName, level.Caption + ".UniqueName", level, OleDbType.BSTR, null);
			this.AddObject(mdxProperty);
			list.Add(mdxProperty);
			dictionary.Add(MdxMemberProperties.QuotedMemberUniqueName, mdxProperty);
			string text = Microsoft.Mashup.Engine1.Library.Mdx.MdxIdentifier.QuotePart("MEMBER_CAPTION");
			MdxProperty mdxProperty2 = new MdxProperty(MdxPropertyKind.MemberCaption, text, level.MdxIdentifier + "." + text, level.Caption + ".Caption", level, OleDbType.BSTR, mdxProperty);
			this.AddObject(mdxProperty2);
			list.Add(mdxProperty2);
			foreach (MdxPropertyMetadata mdxPropertyMetadata in level.Hierarchy.Dimension.PropertiesMetadata.Values)
			{
				this.AddProperty(level, mdxPropertyMetadata, list, dictionary);
			}
			return list;
		}

		// Token: 0x06004622 RID: 17954 RVA: 0x000EB8EC File Offset: 0x000E9AEC
		protected virtual void AddProperty(MdxLevel level, MdxPropertyMetadata mdProperty, List<MdxProperty> properties, Dictionary<string, MdxProperty> propertyKeys)
		{
			MdxProperty mdxProperty = null;
			MdxPropertyMetadata mdxPropertyMetadata;
			if (mdProperty.KeyUniqueName != null && !propertyKeys.TryGetValue(mdProperty.KeyUniqueName, out mdxProperty) && level.Hierarchy.Dimension.PropertiesMetadata.TryGetValue(mdProperty.KeyUniqueName, out mdxPropertyMetadata))
			{
				mdxProperty = this.BuildProperty(level, mdxPropertyMetadata, null);
				propertyKeys.Add(mdxProperty.Name, mdxProperty);
			}
			MdxProperty mdxProperty2 = this.BuildProperty(level, mdProperty, mdxProperty);
			this.AddObject(mdxProperty2);
			properties.Add(mdxProperty2);
		}

		// Token: 0x06004623 RID: 17955 RVA: 0x000EB963 File Offset: 0x000E9B63
		private MdxProperty BuildProperty(MdxLevel level, MdxPropertyMetadata mdProperty, MdxProperty propertyKey)
		{
			return new MdxProperty(mdProperty.PropertyKind, mdProperty.UniqueName, this.BuildPropertyMdxIdentifier(level, mdProperty), mdProperty.Caption, level, mdProperty.DataType, propertyKey);
		}

		// Token: 0x06004624 RID: 17956 RVA: 0x000EB98C File Offset: 0x000E9B8C
		protected virtual string BuildPropertyMdxIdentifier(MdxLevel level, MdxPropertyMetadata mdProperty)
		{
			return level.MdxIdentifier + "." + mdProperty.UniqueName;
		}

		// Token: 0x06004625 RID: 17957 RVA: 0x000EB9A4 File Offset: 0x000E9BA4
		private void LoadMeasureGroups()
		{
			this.measureGroups = new Dictionary<string, MdxMeasureGroup>();
			try
			{
				foreach (MdxMeasureGroupMetadata mdxMeasureGroupMetadata in this.metadata.GetMeasureGroups())
				{
					this.measureGroups[mdxMeasureGroupMetadata.Name] = new MdxMeasureGroup(mdxMeasureGroupMetadata.Name, mdxMeasureGroupMetadata.Caption);
				}
			}
			catch (NotSupportedException)
			{
			}
		}

		// Token: 0x06004626 RID: 17958 RVA: 0x000EBA30 File Offset: 0x000E9C30
		public void AddObject(MdxCubeObject cubeObject)
		{
			this.objects[cubeObject.MdxIdentifier] = cubeObject;
		}

		// Token: 0x06004627 RID: 17959 RVA: 0x000EBA44 File Offset: 0x000E9C44
		private bool TryGetHierarchyType(int flags, out MdxHierarchyType type)
		{
			bool flag = (flags & 2) != 0;
			if ((flags & 1) != 0)
			{
				if (flag)
				{
					type = MdxHierarchyType.ParentChild;
				}
				else
				{
					type = MdxHierarchyType.UserDefined;
				}
				return true;
			}
			if (flag)
			{
				type = MdxHierarchyType.Attribute;
				return true;
			}
			type = MdxHierarchyType.Attribute;
			return false;
		}

		// Token: 0x06004628 RID: 17960 RVA: 0x000EBA78 File Offset: 0x000E9C78
		private bool TryGetLevel(string levelName, out MdxLevel level)
		{
			level = null;
			MdxCubeObject mdxCubeObject;
			if (this.objects.TryGetValue(levelName, out mdxCubeObject))
			{
				level = mdxCubeObject as MdxLevel;
			}
			return level != null;
		}

		// Token: 0x06004629 RID: 17961 RVA: 0x000EBAA8 File Offset: 0x000E9CA8
		public override MdxExpression CompileLevelMemberUserDefined(IdentifierCubeExpression identifier, MdxExpression mdx, MdxProperty property)
		{
			MdxIdentifier mdxIdentifier;
			if (Microsoft.Mashup.Engine1.Library.Mdx.MdxIdentifier.TryParse(property.Name, out mdxIdentifier) && mdxIdentifier.PropertyOrMemberName != null)
			{
				return new InvocationMdxExpression(MdxFunction.Properties, new MdxExpression[]
				{
					mdx,
					new ConstantMdxExpression(mdxIdentifier.PropertyOrMemberName)
				});
			}
			return new InvocationMdxExpression(MdxFunction.Properties, new MdxExpression[]
			{
				mdx,
				new ConstantMdxExpression(property.Name.Trim(new char[] { '[', ']' }))
			});
		}

		// Token: 0x04002504 RID: 9476
		private readonly MdxCubeMetadataProvider metadata;

		// Token: 0x04002505 RID: 9477
		private readonly string name;

		// Token: 0x04002506 RID: 9478
		private IList<MdxMeasure> measures;

		// Token: 0x04002507 RID: 9479
		private IDictionary<string, MdxDimension> dimensions;

		// Token: 0x04002508 RID: 9480
		private IList<MdxKpi> kpis;

		// Token: 0x04002509 RID: 9481
		private IDictionary<string, MdxMeasureGroup> measureGroups;

		// Token: 0x0400250A RID: 9482
		private string defaultMeasure;

		// Token: 0x0400250B RID: 9483
		private Dictionary<string, MdxCubeObject> objects;

		// Token: 0x0400250C RID: 9484
		private bool isInitialized;

		// Token: 0x0200098B RID: 2443
		private static class HierarchyOrigin
		{
			// Token: 0x0400250D RID: 9485
			public const ushort UserDefined = 1;

			// Token: 0x0400250E RID: 9486
			public const ushort SystemEnabled = 2;

			// Token: 0x0400250F RID: 9487
			public const ushort Internal = 4;
		}
	}
}
