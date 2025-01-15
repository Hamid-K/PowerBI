using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Library.Odbc;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x02000422 RID: 1058
	internal abstract class SapHanaCubeBase : ICube
	{
		// Token: 0x060023FD RID: 9213 RVA: 0x00065634 File Offset: 0x00063834
		protected SapHanaCubeBase(IResource resource, SapHanaOdbcDataSource dataSource, OdbcDataSourceInfo dataSourceInfo, string catalogName, string schemaName, string name, bool hasParameters, bool useHierarchies, SapHanaViewType viewType)
		{
			this.dataSource = dataSource;
			this.name = name;
			this.catalogName = catalogName;
			this.schemaName = schemaName;
			this.hasParameters = hasParameters;
			this.useHierarchies = useHierarchies;
			if (this.useHierarchies)
			{
				this.dimensions = new SapHanaDimensionCollection2(dataSource, this);
				this.measures = new SapHanaMeasureCollection2(dataSource, this);
			}
			else
			{
				this.dimensions = new SapHanaDimensionCollection1(dataSource, this);
				this.measures = new SapHanaMeasureCollection1(dataSource, this);
			}
			this.parameters = new SapHanaParameterCollection(dataSource, this);
			this.resource = resource;
			string text = this.dataSource.Host.QueryService<IUniqueIdService>().NewUniqueId();
			this.uniqueIdSuffix = "." + text + ".UniqueId";
			this.captionSuffix = "." + text + ".Caption";
			this.dynamicDimension = new SapHanaDimension("dynamic." + text, "Dimension for dynamic attributes");
			this.viewType = viewType;
		}

		// Token: 0x17000EC9 RID: 3785
		// (get) Token: 0x060023FE RID: 9214 RVA: 0x0006572D File Offset: 0x0006392D
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x060023FF RID: 9215 RVA: 0x00065735 File Offset: 0x00063935
		public string ViewName
		{
			get
			{
				return this.viewName;
			}
		}

		// Token: 0x17000ECB RID: 3787
		// (get) Token: 0x06002400 RID: 9216 RVA: 0x0006573D File Offset: 0x0006393D
		public bool HasParameters
		{
			get
			{
				return this.hasParameters;
			}
		}

		// Token: 0x17000ECC RID: 3788
		// (get) Token: 0x06002401 RID: 9217 RVA: 0x00065745 File Offset: 0x00063945
		public bool UseHierarchies
		{
			get
			{
				return this.useHierarchies;
			}
		}

		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x06002402 RID: 9218 RVA: 0x00065745 File Offset: 0x00063945
		public bool FlattenDimensions
		{
			get
			{
				return this.useHierarchies;
			}
		}

		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x06002403 RID: 9219 RVA: 0x0006574D File Offset: 0x0006394D
		public string CatalogName
		{
			get
			{
				return this.catalogName;
			}
		}

		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x06002404 RID: 9220 RVA: 0x00065755 File Offset: 0x00063955
		public string SchemaName
		{
			get
			{
				return this.schemaName;
			}
		}

		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x06002405 RID: 9221 RVA: 0x0006575D File Offset: 0x0006395D
		public OdbcColumnInfoCollection Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x17000ED1 RID: 3793
		// (get) Token: 0x06002406 RID: 9222 RVA: 0x00065765 File Offset: 0x00063965
		public SapHanaMeasureCollection Measures
		{
			get
			{
				return this.measures;
			}
		}

		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x06002407 RID: 9223 RVA: 0x0006576D File Offset: 0x0006396D
		public SapHanaParameterCollection Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x17000ED3 RID: 3795
		// (get) Token: 0x06002408 RID: 9224 RVA: 0x00065775 File Offset: 0x00063975
		public SapHanaDimensionCollection Dimensions
		{
			get
			{
				return this.dimensions;
			}
		}

		// Token: 0x17000ED4 RID: 3796
		// (get) Token: 0x06002409 RID: 9225 RVA: 0x0006577D File Offset: 0x0006397D
		public SapHanaViewType ViewType
		{
			get
			{
				return this.viewType;
			}
		}

		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x0600240A RID: 9226 RVA: 0x00065785 File Offset: 0x00063985
		public IResource Resource
		{
			get
			{
				return this.resource;
			}
		}

		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x0600240B RID: 9227 RVA: 0x00065790 File Offset: 0x00063990
		public Dictionary<string, SapHanaDimensionAttribute> Attributes
		{
			get
			{
				if (this.attributes == null)
				{
					this.attributes = new Dictionary<string, SapHanaDimensionAttribute>();
					foreach (SapHanaDimension sapHanaDimension in this.dimensions)
					{
						foreach (SapHanaDimensionAttribute sapHanaDimensionAttribute in sapHanaDimension.Attributes.Values)
						{
							this.attributes.Add(sapHanaDimensionAttribute.Name, sapHanaDimensionAttribute);
						}
					}
				}
				return this.attributes;
			}
		}

		// Token: 0x17000ED7 RID: 3799
		// (get) Token: 0x0600240C RID: 9228 RVA: 0x00065840 File Offset: 0x00063A40
		public Dictionary<string, List<SapHanaLevel>> AttributeLevels
		{
			get
			{
				if (this.attributeLevels == null)
				{
					this.attributeLevels = new Dictionary<string, List<SapHanaLevel>>();
					foreach (SapHanaDimension sapHanaDimension in this.dimensions)
					{
						foreach (SapHanaHierarchy sapHanaHierarchy in sapHanaDimension.Hierarchies.Values)
						{
							foreach (SapHanaLevel sapHanaLevel in sapHanaHierarchy.Levels)
							{
								List<SapHanaLevel> list;
								if (!this.attributeLevels.TryGetValue(sapHanaLevel.Attribute.Name, out list))
								{
									list = new List<SapHanaLevel>();
									this.attributeLevels.Add(sapHanaLevel.Attribute.Name, list);
								}
								list.Add(sapHanaLevel);
							}
						}
					}
				}
				return this.attributeLevels;
			}
		}

		// Token: 0x17000ED8 RID: 3800
		// (get) Token: 0x0600240D RID: 9229 RVA: 0x00065960 File Offset: 0x00063B60
		public IdentifierCubeExpression Identifier
		{
			get
			{
				return new IdentifierCubeExpression(this.ViewName);
			}
		}

		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x0600240E RID: 9230 RVA: 0x00065970 File Offset: 0x00063B70
		public bool SupportsDynamicAttributes
		{
			get
			{
				if (this.supportsDynamicAttributes == null)
				{
					Version version;
					this.supportsDynamicAttributes = new bool?(this.useHierarchies && this.viewType == SapHanaViewType.Calculation && this.dataSource.TryGetSapHanaVersion(out version) && version.Major >= 2);
				}
				return this.supportsDynamicAttributes.Value;
			}
		}

		// Token: 0x0600240F RID: 9231 RVA: 0x000659D0 File Offset: 0x00063BD0
		public bool TryGetObject(IdentifierCubeExpression identifier, out ICubeObject obj)
		{
			SapHanaMeasure sapHanaMeasure;
			if (this.Measures.TryGetMeasure(identifier.Identifier, out sapHanaMeasure))
			{
				obj = sapHanaMeasure;
				return true;
			}
			SapHanaDimensionAttribute sapHanaDimensionAttribute;
			if (this.Attributes.TryGetValue(identifier.Identifier, out sapHanaDimensionAttribute))
			{
				obj = sapHanaDimensionAttribute;
				return true;
			}
			if (this.dynamicDimension.Attributes.TryGetValue(identifier.Identifier, out sapHanaDimensionAttribute))
			{
				obj = sapHanaDimensionAttribute;
				return true;
			}
			IdentifierCubeExpression identifierCubeExpression;
			CubePropertyKind cubePropertyKind;
			if (this.TryGetPropertyAttributeAndKind(identifier, out identifierCubeExpression, out cubePropertyKind) && this.Attributes.TryGetValue(identifierCubeExpression.Identifier, out sapHanaDimensionAttribute) && sapHanaDimensionAttribute is ColumnSapHanaDimensionAttribute)
			{
				ColumnSapHanaDimensionAttribute columnSapHanaDimensionAttribute = (ColumnSapHanaDimensionAttribute)sapHanaDimensionAttribute;
				SapHanaProperty sapHanaProperty;
				if (cubePropertyKind != CubePropertyKind.UniqueId)
				{
					if (cubePropertyKind != CubePropertyKind.Caption)
					{
						sapHanaProperty = null;
					}
					else
					{
						sapHanaProperty = new SapHanaProperty(columnSapHanaDimensionAttribute, cubePropertyKind, identifier.Identifier, sapHanaDimensionAttribute.Caption);
						sapHanaProperty.Column = columnSapHanaDimensionAttribute.CaptionColumn;
					}
				}
				else
				{
					sapHanaProperty = new SapHanaProperty(columnSapHanaDimensionAttribute, cubePropertyKind, identifier.Identifier, sapHanaDimensionAttribute.Caption);
					sapHanaProperty.Column = columnSapHanaDimensionAttribute.Column;
				}
				obj = sapHanaProperty;
				return obj != null;
			}
			obj = null;
			return false;
		}

		// Token: 0x06002410 RID: 9232 RVA: 0x00065ACC File Offset: 0x00063CCC
		public IdentifierCubeExpression GetPropertyAttributeAndKind(IdentifierCubeExpression property, out CubePropertyKind kind)
		{
			IdentifierCubeExpression identifierCubeExpression;
			if (this.TryGetPropertyAttributeAndKind(property, out identifierCubeExpression, out kind))
			{
				return identifierCubeExpression;
			}
			throw new NotSupportedException();
		}

		// Token: 0x06002411 RID: 9233 RVA: 0x00065AEC File Offset: 0x00063CEC
		public IdentifierCubeExpression GetProperty(IdentifierCubeExpression dimensionAttribute, CubePropertyKind kind)
		{
			if (kind == CubePropertyKind.UniqueId)
			{
				return new IdentifierCubeExpression(dimensionAttribute.Identifier + this.uniqueIdSuffix);
			}
			if (kind != CubePropertyKind.Caption)
			{
				throw new InvalidOperationException("Unexpected PropertyKind: " + kind.ToString());
			}
			return new IdentifierCubeExpression(dimensionAttribute.Identifier + this.captionSuffix);
		}

		// Token: 0x06002412 RID: 9234 RVA: 0x00065B4C File Offset: 0x00063D4C
		private bool TryGetPropertyAttributeAndKind(IdentifierCubeExpression property, out IdentifierCubeExpression dimensionAttribute, out CubePropertyKind kind)
		{
			int num = property.Identifier.LastIndexOf(this.uniqueIdSuffix, StringComparison.Ordinal);
			int num2 = property.Identifier.LastIndexOf(this.captionSuffix, StringComparison.Ordinal);
			if (num != -1 && num == property.Identifier.Length - this.uniqueIdSuffix.Length)
			{
				dimensionAttribute = new IdentifierCubeExpression(property.Identifier.Substring(0, num));
				kind = CubePropertyKind.UniqueId;
				return true;
			}
			if (num2 != -1 && num2 == property.Identifier.Length - this.captionSuffix.Length)
			{
				dimensionAttribute = new IdentifierCubeExpression(property.Identifier.Substring(0, num2));
				kind = CubePropertyKind.Caption;
				return true;
			}
			kind = CubePropertyKind.UniqueId;
			dimensionAttribute = null;
			return false;
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x00065BF4 File Offset: 0x00063DF4
		public bool TryGetDynamicDimensionAttribute(CubeExpression expression, TypeValue typeValue, out IdentifierCubeExpression dimensionAttribute)
		{
			if (this.SupportsDynamicAttributes)
			{
				IdentifierCubeExpression identifierCubeExpression;
				CubePropertyKind cubePropertyKind;
				if (expression.Kind == CubeExpressionKind.Identifier && this.TryGetPropertyAttributeAndKind((IdentifierCubeExpression)expression, out identifierCubeExpression, out cubePropertyKind))
				{
					dimensionAttribute = null;
					return false;
				}
				string text = this.dynamicDimension.Name + "." + this.dynamicDimension.Attributes.Count.ToString();
				string text2 = "(attribute " + this.dynamicDimension.Attributes.Count.ToString() + ")";
				DynamicSapHanaDimensionAttribute dynamicSapHanaDimensionAttribute;
				if (DynamicSapHanaDimensionAttribute.TryNew(this, this.dynamicDimension, text, text2, expression, typeValue, out dynamicSapHanaDimensionAttribute))
				{
					this.dynamicDimension.Attributes.Add(dynamicSapHanaDimensionAttribute.Name, dynamicSapHanaDimensionAttribute);
					dimensionAttribute = new IdentifierCubeExpression(dynamicSapHanaDimensionAttribute.Name);
					return true;
				}
			}
			dimensionAttribute = null;
			return false;
		}

		// Token: 0x06002414 RID: 9236 RVA: 0x00065CC8 File Offset: 0x00063EC8
		public static bool TryCreateCube(IDataReader reader, IResource resource, SapHanaOdbcDataSource dataSource, string catalog, bool useHierarchies, out SapHanaCubeBase cube)
		{
			string text = reader[1] as string;
			SapHanaViewType sapHanaViewType;
			if (!(text == "OLAP"))
			{
				if (!(text == "CALC"))
				{
					cube = null;
					return false;
				}
				sapHanaViewType = SapHanaViewType.Calculation;
			}
			else
			{
				sapHanaViewType = SapHanaViewType.Analytical;
			}
			object obj = reader[2];
			bool flag = obj is long && (long)obj > 0L;
			string text2 = reader[3] as string;
			if (text2 == "_SYS_BIC")
			{
				cube = new SapHanaCube(resource, dataSource, dataSource.Info, catalog, reader.GetString(0), flag, useHierarchies, sapHanaViewType);
			}
			else
			{
				cube = new SapHanaHDICube(resource, dataSource, dataSource.Info, catalog, text2, reader.GetString(0), flag, useHierarchies, reader[4] as string, sapHanaViewType);
			}
			return true;
		}

		// Token: 0x04000E82 RID: 3714
		protected readonly IResource resource;

		// Token: 0x04000E83 RID: 3715
		protected readonly SapHanaOdbcDataSource dataSource;

		// Token: 0x04000E84 RID: 3716
		protected readonly string name;

		// Token: 0x04000E85 RID: 3717
		protected readonly string catalogName;

		// Token: 0x04000E86 RID: 3718
		protected readonly string schemaName;

		// Token: 0x04000E87 RID: 3719
		protected readonly bool hasParameters;

		// Token: 0x04000E88 RID: 3720
		protected readonly bool useHierarchies;

		// Token: 0x04000E89 RID: 3721
		protected readonly SapHanaMeasureCollection measures;

		// Token: 0x04000E8A RID: 3722
		protected readonly SapHanaDimensionCollection dimensions;

		// Token: 0x04000E8B RID: 3723
		protected readonly SapHanaParameterCollection parameters;

		// Token: 0x04000E8C RID: 3724
		protected readonly SapHanaDimension dynamicDimension;

		// Token: 0x04000E8D RID: 3725
		protected readonly SapHanaViewType viewType;

		// Token: 0x04000E8E RID: 3726
		protected OdbcColumnInfoCollection columns;

		// Token: 0x04000E8F RID: 3727
		protected string viewName;

		// Token: 0x04000E90 RID: 3728
		protected string uniqueIdSuffix;

		// Token: 0x04000E91 RID: 3729
		protected string captionSuffix;

		// Token: 0x04000E92 RID: 3730
		protected Dictionary<string, SapHanaDimensionAttribute> attributes;

		// Token: 0x04000E93 RID: 3731
		protected Dictionary<string, List<SapHanaLevel>> attributeLevels;

		// Token: 0x04000E94 RID: 3732
		private bool? supportsDynamicAttributes;
	}
}
