using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.AnalysisServices.Tabular.DataRefresh;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200008D RID: 141
	public sealed class Partition : NamedMetadataObject, IMetadataObjectWithOverrides
	{
		// Token: 0x06000881 RID: 2177 RVA: 0x00048320 File Offset: 0x00046520
		public Partition()
		{
			this.InitBodyAndCollections(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x00048333 File Offset: 0x00046533
		internal Partition(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections(comparer);
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x00048344 File Offset: 0x00046544
		private void InitBodyAndCollections(IEqualityComparer<string> comparer)
		{
			this.body = new Partition.ObjectBody(this);
			this.body.Name = string.Empty;
			this.body.Description = string.Empty;
			this.body.QueryDefinition = string.Empty;
			this.body.State = ObjectState.NoData;
			this.body.Type = PartitionSourceType.None;
			this.body.Mode = ModeType.Default;
			this.body.DataView = DataViewType.Default;
			this.body.ErrorMessage = string.Empty;
			this.body.RetainDataTillForceCalculate = false;
			this.body.RangeStart = new DateTime(0L);
			this.body.RangeEnd = new DateTime(0L);
			this.body.RangeGranularity = RefreshGranularityType.Invalid;
			this.body.RefreshBookmark = string.Empty;
			this.body.MAttributes = string.Empty;
			this.body.SchemaName = string.Empty;
			this._Annotations = new PartitionAnnotationCollection(this, comparer);
			this._ExtendedProperties = new PartitionExtendedPropertyCollection(this, comparer);
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000884 RID: 2180 RVA: 0x00048453 File Offset: 0x00046653
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.Partition;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x00048456 File Offset: 0x00046656
		// (set) Token: 0x06000886 RID: 2182 RVA: 0x00048468 File Offset: 0x00046668
		public override MetadataObject Parent
		{
			get
			{
				return this.body.TableID.Object;
			}
			internal set
			{
				if (this.body.TableID.Object != value)
				{
					MetadataObject.UpdateMetadataObjectParent<Partition, Table>(this.body.TableID, (Table)value, null, null);
				}
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x00048495 File Offset: 0x00046695
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.TableID.ObjectID;
			}
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x000484A8 File Offset: 0x000466A8
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.Partition, null, "Partition object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
				{
					writer.WriteProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, typeof(string));
				}
				if (writer.ShouldIncludeProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
				{
					writer.WriteProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, typeof(string));
				}
				if (writer.ShouldIncludeProperty("state", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred))
				{
					writer.WriteEnumProperty<ObjectState>("state", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred, PropertyHelper.GetObjectStateCompatibleValues(context.CompatibilityMode, context.DbCompatibilityLevel));
				}
				if (writer.ShouldIncludeProperty("mode", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<ModeType>("mode", MetadataPropertyNature.RegularProperty, PropertyHelper.GetModeTypeCompatibleValues(context.CompatibilityMode, context.DbCompatibilityLevel));
				}
				if (writer.ShouldIncludeProperty("dataView", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<DataViewType>("dataView", MetadataPropertyNature.RegularProperty, null);
				}
				if (writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
				{
					writer.WriteProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
				}
				if (writer.ShouldIncludeProperty("refreshedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
				{
					writer.WriteProperty("refreshedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
				}
				if (writer.ShouldIncludeProperty("errorMessage", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred))
				{
					writer.WriteProperty("errorMessage", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred, typeof(string));
				}
				if (CompatibilityRestrictions.Partition_QueryGroup.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("queryGroup", MetadataPropertyNature.CrossLinkProperty))
				{
					CrossLink<Partition, QueryGroup>.WriteMetadataSchema(ObjectType.QueryGroup, ObjectType.QueryGroup, true, "queryGroup", false, writer);
				}
				if (writer.ShouldIncludeProperty("source", MetadataPropertyNature.ChildProperty))
				{
					PartitionSource.WriteMetadataSchema(context, writer);
				}
				if (CompatibilityRestrictions.Partition_DataCoverageDefinition.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("dataCoverageDefinition", MetadataPropertyNature.ChildProperty))
				{
					writer.WriteSingleChild(context, "dataCoverageDefinition", MetadataPropertyNature.ChildProperty, ObjectType.DataCoverageDefinition);
				}
				if (CompatibilityRestrictions.ExtendedProperty.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("extendedProperties", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "extendedProperties", MetadataPropertyNature.ChildCollection, ObjectType.ExtendedProperty);
				}
				if (writer.ShouldIncludeProperty("annotations", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "annotations", MetadataPropertyNature.ChildCollection, ObjectType.Annotation);
				}
			}
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0004871C File Offset: 0x0004691C
		internal static void WritePartitionPropertiesMetadataSchemaInMergedMode(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			if (writer.ShouldIncludeProperty("state", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred))
			{
				writer.WriteEnumProperty<ObjectState>("state", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred, PropertyHelper.GetObjectStateCompatibleValues(context.CompatibilityMode, context.DbCompatibilityLevel));
			}
			if (writer.ShouldIncludeProperty("mode", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteEnumProperty<ModeType>("mode", MetadataPropertyNature.RegularProperty, PropertyHelper.GetModeTypeCompatibleValues(context.CompatibilityMode, context.DbCompatibilityLevel));
			}
			if (writer.ShouldIncludeProperty("dataView", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteEnumProperty<DataViewType>("dataView", MetadataPropertyNature.RegularProperty, null);
			}
			if (writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
			}
			if (writer.ShouldIncludeProperty("refreshedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteProperty("refreshedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
			}
			if (writer.ShouldIncludeProperty("errorMessage", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred))
			{
				writer.WriteProperty("errorMessage", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred, typeof(string));
			}
			if (CompatibilityRestrictions.Partition_QueryGroup.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("queryGroup", MetadataPropertyNature.CrossLinkProperty))
			{
				CrossLink<Partition, QueryGroup>.WriteMetadataSchema(ObjectType.QueryGroup, ObjectType.QueryGroup, true, "queryGroup", false, writer);
			}
			if (writer.ShouldIncludeProperty("source", MetadataPropertyNature.ChildProperty))
			{
				PartitionSource.WriteMetadataSchema(context, writer);
			}
			if (CompatibilityRestrictions.Partition_DataCoverageDefinition.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("dataCoverageDefinition", MetadataPropertyNature.ChildProperty))
			{
				writer.WriteSingleChild(context, "dataCoverageDefinition", MetadataPropertyNature.ChildProperty, ObjectType.DataCoverageDefinition);
			}
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x000488A8 File Offset: 0x00046AA8
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			base.GetCompatibilityRequirementByMembers(mode, out requiredLevel, out requestingPath);
			if (requiredLevel == -2)
			{
				return;
			}
			if (this.body.Type != PartitionSourceType.None)
			{
				int num = PropertyHelper.GetPartitionSourceTypeCompatibilityRestrictions(this.body.Type)[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "Type");
					requiredLevel = num;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (this.body.Mode != ModeType.Default)
			{
				int num2 = PropertyHelper.GetModeTypeCompatibilityRestrictions(this.body.Mode)[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num2, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "Mode");
					requiredLevel = num2;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (this.body.RetainDataTillForceCalculate)
			{
				int num3 = CompatibilityRestrictions.Partition_RetainDataTillForceCalculate[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num3, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "RetainDataTillForceCalculate");
					requiredLevel = num3;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (this.body.RangeStart.CompareTo(new DateTime(0L)) != 0)
			{
				int num4 = CompatibilityRestrictions.Partition_RangeStart[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num4, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "RangeStart");
					requiredLevel = num4;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (this.body.RangeEnd.CompareTo(new DateTime(0L)) != 0)
			{
				int num5 = CompatibilityRestrictions.Partition_RangeEnd[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num5, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "RangeEnd");
					requiredLevel = num5;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (this.body.RangeGranularity != RefreshGranularityType.Invalid)
			{
				int num6;
				CompatibilityRestrictionSet.MergeLevelDemand(CompatibilityRestrictions.Partition_RangeGranularity[mode], PropertyHelper.GetRefreshGranularityTypeCompatibilityRestrictions(this.body.RangeGranularity)[mode], out num6);
				if (CompatibilityRestrictionSet.CompareLevel(num6, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "RangeGranularity");
					requiredLevel = num6;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (!string.IsNullOrEmpty(this.body.RefreshBookmark))
			{
				int num7 = CompatibilityRestrictions.Partition_RefreshBookmark[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num7, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "RefreshBookmark");
					requiredLevel = num7;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (this.body.QueryGroupID.Object != null)
			{
				int num8 = CompatibilityRestrictions.Partition_QueryGroup[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num8, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "QueryGroupID");
					requiredLevel = num8;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (this.body.ExpressionSourceID.Object != null)
			{
				int num9 = CompatibilityRestrictions.Partition_ExpressionSource[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num9, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "ExpressionSourceID");
					requiredLevel = num9;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (!string.IsNullOrEmpty(this.body.MAttributes))
			{
				int num10 = CompatibilityRestrictions.Partition_MAttributes[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num10, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "MAttributes");
					requiredLevel = num10;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (!string.IsNullOrEmpty(this.body.SchemaName))
			{
				int num11 = CompatibilityRestrictions.Partition_SchemaName[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num11, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "SchemaName");
					requiredLevel = num11;
					int num12 = requiredLevel;
					return;
				}
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x0600088B RID: 2187 RVA: 0x00048C37 File Offset: 0x00046E37
		// (set) Token: 0x0600088C RID: 2188 RVA: 0x00048C3F File Offset: 0x00046E3F
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (Partition.ObjectBody)value;
			}
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x00048C4D File Offset: 0x00046E4D
		internal override ITxObjectBody CreateBody()
		{
			return new Partition.ObjectBody(this);
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00048C55 File Offset: 0x00046E55
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new Partition();
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00048C5C File Offset: 0x00046E5C
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return ((Table)parent).Partitions;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x00048C6C File Offset: 0x00046E6C
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			Table table = MetadataObject.ResolveMetadataObjectParentById<Partition, Table>(this.body.TableID, objectMap, throwIfCantResolve, null, null);
			this.body.DataSourceID.ResolveById(objectMap, throwIfCantResolve);
			KeyValuePair<CompatibilityMode, Stack<string>>[] array = ((!this.body.QueryGroupID.IsNull) ? base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Partition_QueryGroup, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "QueryGroup")) : null);
			if (this.body.QueryGroupID.ResolveById(objectMap, throwIfCantResolve))
			{
				base.SetCompatibilityRequirement(CompatibilityRestrictions.Partition_QueryGroup, array);
			}
			array = ((!this.body.ExpressionSourceID.IsNull) ? base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Partition_ExpressionSource, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "ExpressionSource")) : null);
			if (this.body.ExpressionSourceID.ResolveById(objectMap, throwIfCantResolve))
			{
				base.SetCompatibilityRequirement(CompatibilityRestrictions.Partition_ExpressionSource, array);
			}
			this.body.DataCoverageDefinitionID.ResolveById(objectMap, throwIfCantResolve);
			if (table != null)
			{
				table.Partitions.Add(this);
			}
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00048D70 File Offset: 0x00046F70
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			this.body.DataSourceID.ResolveById(objectMap, throwIfCantResolve);
			KeyValuePair<CompatibilityMode, Stack<string>>[] array = ((!this.body.QueryGroupID.IsNull) ? base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Partition_QueryGroup, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "QueryGroup")) : null);
			if (this.body.QueryGroupID.ResolveById(objectMap, throwIfCantResolve))
			{
				base.SetCompatibilityRequirement(CompatibilityRestrictions.Partition_QueryGroup, array);
			}
			array = ((!this.body.ExpressionSourceID.IsNull) ? base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Partition_ExpressionSource, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "ExpressionSource")) : null);
			if (this.body.ExpressionSourceID.ResolveById(objectMap, throwIfCantResolve))
			{
				base.SetCompatibilityRequirement(CompatibilityRestrictions.Partition_ExpressionSource, array);
			}
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00048E3C File Offset: 0x0004703C
		internal override bool TryResolveCrossLinksByPath(ICollection<string> linksFailedToResolve)
		{
			bool flag = true;
			if (!this.body.DataSourceID.IsResolved && !this.body.DataSourceID.TryResolveByPath())
			{
				if (linksFailedToResolve != null)
				{
					linksFailedToResolve.Add(string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "DataSource"));
				}
				flag = false;
			}
			if (!this.body.QueryGroupID.IsResolved)
			{
				KeyValuePair<CompatibilityMode, Stack<string>>[] array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Partition_QueryGroup, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "QueryGroup"));
				if (this.body.QueryGroupID.TryResolveByPath())
				{
					base.SetCompatibilityRequirement(CompatibilityRestrictions.Partition_QueryGroup, array);
				}
				else
				{
					if (linksFailedToResolve != null)
					{
						linksFailedToResolve.Add(string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "QueryGroup"));
					}
					flag = false;
				}
			}
			if (!this.body.ExpressionSourceID.IsResolved)
			{
				KeyValuePair<CompatibilityMode, Stack<string>>[] array2 = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Partition_ExpressionSource, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "ExpressionSource"));
				if (this.body.ExpressionSourceID.TryResolveByPath())
				{
					base.SetCompatibilityRequirement(CompatibilityRestrictions.Partition_ExpressionSource, array2);
				}
				else
				{
					if (linksFailedToResolve != null)
					{
						linksFailedToResolve.Add(string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "ExpressionSource"));
					}
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x00048F76 File Offset: 0x00047176
		internal override void TryResolveCrossLinksAfterCopy(CopyContext copyContext)
		{
			this.body.DataSourceID.TryResolveAfterCopy(copyContext);
			this.body.QueryGroupID.TryResolveAfterCopy(copyContext);
			this.body.ExpressionSourceID.TryResolveAfterCopy(copyContext);
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00048FAE File Offset: 0x000471AE
		internal override void ValidateObjectImpl(ValidationResult result, bool throwOnError)
		{
			this.body.DataSourceID.Validate(result, throwOnError);
			this.body.QueryGroupID.Validate(result, throwOnError);
			this.body.ExpressionSourceID.Validate(result, throwOnError);
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00048FE8 File Offset: 0x000471E8
		internal override bool ContainsUnresolvedCrossLinksImpl()
		{
			return !this.body.DataSourceID.IsResolved || !this.body.QueryGroupID.IsResolved || !this.body.ExpressionSourceID.IsResolved || base.ContainsUnresolvedCrossLinksImpl();
		}

		// Token: 0x06000896 RID: 2198 RVA: 0x00049037 File Offset: 0x00047237
		internal override IEnumerable<MetadataObject> GetDirectChildren(bool isLogicalStructure)
		{
			if (this.body.DataCoverageDefinitionID.Object != null)
			{
				yield return this.body.DataCoverageDefinitionID.Object;
			}
			yield break;
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00049047 File Offset: 0x00047247
		internal override IEnumerable<IMetadataObjectCollection> GetChildrenCollections(bool isLogicalStructure)
		{
			yield return this._Annotations;
			yield return this._ExtendedProperties;
			yield break;
		}

		// Token: 0x06000898 RID: 2200 RVA: 0x00049058 File Offset: 0x00047258
		private protected override void SetDirectChildImpl(MetadataObject child)
		{
			if (child.ObjectType == ObjectType.DataCoverageDefinition)
			{
				base.ValidateCompatibilityRequirement(child, "DataCoverageDefinition", CompatibilityRestrictions.Partition_DataCoverageDefinition);
				ObjectChangeTracker.RegisterPropertyChanging(this, "DataCoverageDefinition", typeof(DataCoverageDefinition), this.body.DataCoverageDefinitionID.Object, child);
				DataCoverageDefinition @object = this.body.DataCoverageDefinitionID.Object;
				this.body.DataCoverageDefinitionID.Object = (DataCoverageDefinition)child;
				ObjectChangeTracker.RegisterPropertyChanged(this, "DataCoverageDefinition", typeof(DataCoverageDefinition), @object, child);
				return;
			}
			base.SetDirectChildImpl(child);
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x000490F0 File Offset: 0x000472F0
		private protected override void RemoveDirectChildImpl(MetadataObject child)
		{
			if (child.ObjectType == ObjectType.DataCoverageDefinition)
			{
				if (this.body.DataCoverageDefinitionID.ObjectID == child.Id)
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "DataCoverageDefinition", typeof(DataCoverageDefinition), this.body.DataCoverageDefinitionID.Object, null);
					base.ResetCompatibilityRequirement();
					DataCoverageDefinition @object = this.body.DataCoverageDefinitionID.Object;
					this.body.DataCoverageDefinitionID.Object = null;
					ObjectChangeTracker.RegisterPropertyChanged(this, "DataCoverageDefinition", typeof(DataCoverageDefinition), @object, null);
					return;
				}
			}
			else
			{
				base.RemoveDirectChildImpl(child);
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x0600089A RID: 2202 RVA: 0x00049194 File Offset: 0x00047394
		public PartitionAnnotationCollection Annotations
		{
			get
			{
				return this._Annotations;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x0004919C File Offset: 0x0004739C
		[CompatibilityRequirement("1400")]
		public PartitionExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return this._ExtendedProperties;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x0600089C RID: 2204 RVA: 0x000491A4 File Offset: 0x000473A4
		// (set) Token: 0x0600089D RID: 2205 RVA: 0x000491B4 File Offset: 0x000473B4
		public override string Name
		{
			get
			{
				return this.body.Name;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Name, value))
				{
					string text;
					if (!Utils.IsSyntacticallyValidName(value, ObjectType.Partition, out text))
					{
						throw new ArgumentException(text);
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "Name", typeof(string), this.body.Name, value);
					string name = this.body.Name;
					this.body.Name = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Name", typeof(string), name, value);
				}
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x00049236 File Offset: 0x00047436
		// (set) Token: 0x0600089F RID: 2207 RVA: 0x00049244 File Offset: 0x00047444
		public string Description
		{
			get
			{
				return this.body.Description;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Description, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Description", typeof(string), this.body.Description, value);
					string description = this.body.Description;
					this.body.Description = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Description", typeof(string), description, value);
				}
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060008A0 RID: 2208 RVA: 0x000492B4 File Offset: 0x000474B4
		// (set) Token: 0x060008A1 RID: 2209 RVA: 0x000492C4 File Offset: 0x000474C4
		internal string QueryDefinition
		{
			get
			{
				return this.body.QueryDefinition;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.QueryDefinition, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "QueryDefinition", typeof(string), this.body.QueryDefinition, value);
					string queryDefinition = this.body.QueryDefinition;
					this.body.QueryDefinition = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "QueryDefinition", typeof(string), queryDefinition, value);
				}
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060008A2 RID: 2210 RVA: 0x00049334 File Offset: 0x00047534
		// (set) Token: 0x060008A3 RID: 2211 RVA: 0x00049344 File Offset: 0x00047544
		public ObjectState State
		{
			get
			{
				return this.body.State;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.State, value))
				{
					CompatibilityRestrictionSet objectStateCompatibilityRestrictions = PropertyHelper.GetObjectStateCompatibilityRestrictions(value);
					CompatibilityRestrictionSet objectStateCompatibilityRestrictions2 = PropertyHelper.GetObjectStateCompatibilityRestrictions(this.body.State);
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					RestrictionsComapreResult restrictionsComapreResult = objectStateCompatibilityRestrictions.Compare(objectStateCompatibilityRestrictions2);
					if (restrictionsComapreResult == RestrictionsComapreResult.MoreRestrictive || restrictionsComapreResult == RestrictionsComapreResult.Incomparable || (restrictionsComapreResult == RestrictionsComapreResult.Equal && value != ObjectState.NoData))
					{
						array = base.ValidateCompatibilityRequirement(objectStateCompatibilityRestrictions, string.Format("[{0}]::[{1}]=[{2}]", this.GetFormattedObjectPath(), "State", value));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "State", typeof(ObjectState), this.body.State, value);
					switch (restrictionsComapreResult)
					{
					case RestrictionsComapreResult.Incomparable:
						base.ResetCompatibilityRequirement();
						base.SetCompatibilityRequirement(objectStateCompatibilityRestrictions, array);
						break;
					case RestrictionsComapreResult.LessRestrictive:
						base.ResetCompatibilityRequirement();
						break;
					case RestrictionsComapreResult.Equal:
						if (array != null)
						{
							base.SetCompatibilityRequirement(objectStateCompatibilityRestrictions, array);
						}
						break;
					case RestrictionsComapreResult.MoreRestrictive:
						base.SetCompatibilityRequirement(objectStateCompatibilityRestrictions, array);
						break;
					}
					ObjectState state = this.body.State;
					this.body.State = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "State", typeof(ObjectState), state, value);
				}
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x00049466 File Offset: 0x00047666
		// (set) Token: 0x060008A5 RID: 2213 RVA: 0x00049474 File Offset: 0x00047674
		internal PartitionSourceType Type
		{
			get
			{
				return this.body.Type;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Type, value))
				{
					CompatibilityRestrictionSet partitionSourceTypeCompatibilityRestrictions = PropertyHelper.GetPartitionSourceTypeCompatibilityRestrictions(value);
					CompatibilityRestrictionSet partitionSourceTypeCompatibilityRestrictions2 = PropertyHelper.GetPartitionSourceTypeCompatibilityRestrictions(this.body.Type);
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					RestrictionsComapreResult restrictionsComapreResult = partitionSourceTypeCompatibilityRestrictions.Compare(partitionSourceTypeCompatibilityRestrictions2);
					if (restrictionsComapreResult == RestrictionsComapreResult.MoreRestrictive || restrictionsComapreResult == RestrictionsComapreResult.Incomparable || (restrictionsComapreResult == RestrictionsComapreResult.Equal && value != PartitionSourceType.None))
					{
						array = base.ValidateCompatibilityRequirement(partitionSourceTypeCompatibilityRestrictions, string.Format("[{0}]::[{1}]=[{2}]", this.GetFormattedObjectPath(), "Type", value));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "Type", typeof(PartitionSourceType), this.body.Type, value);
					switch (restrictionsComapreResult)
					{
					case RestrictionsComapreResult.Incomparable:
						base.ResetCompatibilityRequirement();
						base.SetCompatibilityRequirement(partitionSourceTypeCompatibilityRestrictions, array);
						break;
					case RestrictionsComapreResult.LessRestrictive:
						base.ResetCompatibilityRequirement();
						break;
					case RestrictionsComapreResult.Equal:
						if (array != null)
						{
							base.SetCompatibilityRequirement(partitionSourceTypeCompatibilityRestrictions, array);
						}
						break;
					case RestrictionsComapreResult.MoreRestrictive:
						base.SetCompatibilityRequirement(partitionSourceTypeCompatibilityRestrictions, array);
						break;
					}
					PartitionSourceType type = this.body.Type;
					this.body.Type = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Type", typeof(PartitionSourceType), type, value);
				}
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x00049596 File Offset: 0x00047796
		// (set) Token: 0x060008A7 RID: 2215 RVA: 0x000495A4 File Offset: 0x000477A4
		public ModeType Mode
		{
			get
			{
				return this.body.Mode;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Mode, value))
				{
					CompatibilityRestrictionSet modeTypeCompatibilityRestrictions = PropertyHelper.GetModeTypeCompatibilityRestrictions(value);
					CompatibilityRestrictionSet modeTypeCompatibilityRestrictions2 = PropertyHelper.GetModeTypeCompatibilityRestrictions(this.body.Mode);
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					RestrictionsComapreResult restrictionsComapreResult = modeTypeCompatibilityRestrictions.Compare(modeTypeCompatibilityRestrictions2);
					if (restrictionsComapreResult == RestrictionsComapreResult.MoreRestrictive || restrictionsComapreResult == RestrictionsComapreResult.Incomparable || (restrictionsComapreResult == RestrictionsComapreResult.Equal && value != ModeType.Default))
					{
						array = base.ValidateCompatibilityRequirement(modeTypeCompatibilityRestrictions, string.Format("[{0}]::[{1}]=[{2}]", this.GetFormattedObjectPath(), "Mode", value));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "Mode", typeof(ModeType), this.body.Mode, value);
					switch (restrictionsComapreResult)
					{
					case RestrictionsComapreResult.Incomparable:
						base.ResetCompatibilityRequirement();
						base.SetCompatibilityRequirement(modeTypeCompatibilityRestrictions, array);
						break;
					case RestrictionsComapreResult.LessRestrictive:
						base.ResetCompatibilityRequirement();
						break;
					case RestrictionsComapreResult.Equal:
						if (array != null)
						{
							base.SetCompatibilityRequirement(modeTypeCompatibilityRestrictions, array);
						}
						break;
					case RestrictionsComapreResult.MoreRestrictive:
						base.SetCompatibilityRequirement(modeTypeCompatibilityRestrictions, array);
						break;
					}
					ModeType mode = this.body.Mode;
					this.body.Mode = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Mode", typeof(ModeType), mode, value);
				}
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x000496C6 File Offset: 0x000478C6
		// (set) Token: 0x060008A9 RID: 2217 RVA: 0x000496D4 File Offset: 0x000478D4
		public DataViewType DataView
		{
			get
			{
				return this.body.DataView;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.DataView, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "DataView", typeof(DataViewType), this.body.DataView, value);
					DataViewType dataView = this.body.DataView;
					this.body.DataView = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "DataView", typeof(DataViewType), dataView, value);
				}
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x00049758 File Offset: 0x00047958
		// (set) Token: 0x060008AB RID: 2219 RVA: 0x00049768 File Offset: 0x00047968
		public DateTime ModifiedTime
		{
			get
			{
				return this.body.ModifiedTime;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.ModifiedTime, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "ModifiedTime", typeof(DateTime), this.body.ModifiedTime, value);
					DateTime modifiedTime = this.body.ModifiedTime;
					this.body.ModifiedTime = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "ModifiedTime", typeof(DateTime), modifiedTime, value);
				}
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060008AC RID: 2220 RVA: 0x000497EC File Offset: 0x000479EC
		// (set) Token: 0x060008AD RID: 2221 RVA: 0x000497FC File Offset: 0x000479FC
		public DateTime RefreshedTime
		{
			get
			{
				return this.body.RefreshedTime;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.RefreshedTime, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "RefreshedTime", typeof(DateTime), this.body.RefreshedTime, value);
					DateTime refreshedTime = this.body.RefreshedTime;
					this.body.RefreshedTime = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "RefreshedTime", typeof(DateTime), refreshedTime, value);
				}
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060008AE RID: 2222 RVA: 0x00049880 File Offset: 0x00047A80
		// (set) Token: 0x060008AF RID: 2223 RVA: 0x00049890 File Offset: 0x00047A90
		public string ErrorMessage
		{
			get
			{
				return this.body.ErrorMessage;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.ErrorMessage, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "ErrorMessage", typeof(string), this.body.ErrorMessage, value);
					string errorMessage = this.body.ErrorMessage;
					this.body.ErrorMessage = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "ErrorMessage", typeof(string), errorMessage, value);
				}
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060008B0 RID: 2224 RVA: 0x00049900 File Offset: 0x00047B00
		// (set) Token: 0x060008B1 RID: 2225 RVA: 0x00049910 File Offset: 0x00047B10
		[CompatibilityRequirement("1400")]
		[Obsolete("Deprecated. Use CalculatedPartitionSource.RetainDataTillForceCalculate property instead.")]
		public bool RetainDataTillForceCalculate
		{
			get
			{
				return this.body.RetainDataTillForceCalculate;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.RetainDataTillForceCalculate, value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (value)
					{
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Partition_RetainDataTillForceCalculate, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "RetainDataTillForceCalculate"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "RetainDataTillForceCalculate", typeof(bool), this.body.RetainDataTillForceCalculate, value);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.Partition_RetainDataTillForceCalculate, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					bool retainDataTillForceCalculate = this.body.RetainDataTillForceCalculate;
					this.body.RetainDataTillForceCalculate = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "RetainDataTillForceCalculate", typeof(bool), retainDataTillForceCalculate, value);
				}
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x000499D4 File Offset: 0x00047BD4
		// (set) Token: 0x060008B3 RID: 2227 RVA: 0x000499E4 File Offset: 0x00047BE4
		[CompatibilityRequirement("1450")]
		internal DateTime RangeStart
		{
			get
			{
				return this.body.RangeStart;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.RangeStart, value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (value.CompareTo(new DateTime(0L)) != 0)
					{
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Partition_RangeStart, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "RangeStart"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "RangeStart", typeof(DateTime), this.body.RangeStart, value);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.Partition_RangeStart, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					DateTime rangeStart = this.body.RangeStart;
					this.body.RangeStart = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "RangeStart", typeof(DateTime), rangeStart, value);
				}
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x00049AB5 File Offset: 0x00047CB5
		// (set) Token: 0x060008B5 RID: 2229 RVA: 0x00049AC4 File Offset: 0x00047CC4
		[CompatibilityRequirement("1450")]
		internal DateTime RangeEnd
		{
			get
			{
				return this.body.RangeEnd;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.RangeEnd, value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (value.CompareTo(new DateTime(0L)) != 0)
					{
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Partition_RangeEnd, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "RangeEnd"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "RangeEnd", typeof(DateTime), this.body.RangeEnd, value);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.Partition_RangeEnd, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					DateTime rangeEnd = this.body.RangeEnd;
					this.body.RangeEnd = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "RangeEnd", typeof(DateTime), rangeEnd, value);
				}
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x00049B95 File Offset: 0x00047D95
		// (set) Token: 0x060008B7 RID: 2231 RVA: 0x00049BA4 File Offset: 0x00047DA4
		[CompatibilityRequirement("1450")]
		internal RefreshGranularityType RangeGranularity
		{
			get
			{
				return this.body.RangeGranularity;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.RangeGranularity, value))
				{
					CompatibilityRestrictionSet compatibilityRestrictionSet = CompatibilityRestrictions.Partition_RangeGranularity.Merge(PropertyHelper.GetRefreshGranularityTypeCompatibilityRestrictions(value));
					CompatibilityRestrictionSet compatibilityRestrictionSet2 = CompatibilityRestrictions.Partition_RangeGranularity.Merge(PropertyHelper.GetRefreshGranularityTypeCompatibilityRestrictions(this.body.RangeGranularity));
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					RestrictionsComapreResult restrictionsComapreResult = compatibilityRestrictionSet.Compare(compatibilityRestrictionSet2);
					if (restrictionsComapreResult == RestrictionsComapreResult.MoreRestrictive || restrictionsComapreResult == RestrictionsComapreResult.Incomparable || (restrictionsComapreResult == RestrictionsComapreResult.Equal && value != RefreshGranularityType.Invalid))
					{
						array = base.ValidateCompatibilityRequirement(compatibilityRestrictionSet, string.Format("[{0}]::[{1}]=[{2}]", this.GetFormattedObjectPath(), "RangeGranularity", value));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "RangeGranularity", typeof(RefreshGranularityType), this.body.RangeGranularity, value);
					switch (restrictionsComapreResult)
					{
					case RestrictionsComapreResult.Incomparable:
						base.ResetCompatibilityRequirement();
						base.SetCompatibilityRequirement(compatibilityRestrictionSet, array);
						break;
					case RestrictionsComapreResult.LessRestrictive:
						base.ResetCompatibilityRequirement();
						break;
					case RestrictionsComapreResult.Equal:
						if (array != null)
						{
							base.SetCompatibilityRequirement(compatibilityRestrictionSet, array);
						}
						break;
					case RestrictionsComapreResult.MoreRestrictive:
						base.SetCompatibilityRequirement(compatibilityRestrictionSet, array);
						break;
					}
					RefreshGranularityType rangeGranularity = this.body.RangeGranularity;
					this.body.RangeGranularity = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "RangeGranularity", typeof(RefreshGranularityType), rangeGranularity, value);
				}
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x00049CDA File Offset: 0x00047EDA
		// (set) Token: 0x060008B9 RID: 2233 RVA: 0x00049CE8 File Offset: 0x00047EE8
		[CompatibilityRequirement("1450")]
		internal string RefreshBookmark
		{
			get
			{
				return this.body.RefreshBookmark;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.RefreshBookmark, value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (!string.IsNullOrEmpty(value))
					{
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Partition_RefreshBookmark, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "RefreshBookmark"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "RefreshBookmark", typeof(string), this.body.RefreshBookmark, value);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.Partition_RefreshBookmark, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					string refreshBookmark = this.body.RefreshBookmark;
					this.body.RefreshBookmark = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "RefreshBookmark", typeof(string), refreshBookmark, value);
				}
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x00049D9D File Offset: 0x00047F9D
		// (set) Token: 0x060008BB RID: 2235 RVA: 0x00049DAC File Offset: 0x00047FAC
		[CompatibilityRequirement("1535")]
		internal string MAttributes
		{
			get
			{
				return this.body.MAttributes;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.MAttributes, value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (!string.IsNullOrEmpty(value))
					{
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Partition_MAttributes, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "MAttributes"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "MAttributes", typeof(string), this.body.MAttributes, value);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.Partition_MAttributes, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					string mattributes = this.body.MAttributes;
					this.body.MAttributes = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "MAttributes", typeof(string), mattributes, value);
				}
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x00049E61 File Offset: 0x00048061
		// (set) Token: 0x060008BD RID: 2237 RVA: 0x00049E70 File Offset: 0x00048070
		[CompatibilityRequirement("1604")]
		internal string SchemaName
		{
			get
			{
				return this.body.SchemaName;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.SchemaName, value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (!string.IsNullOrEmpty(value))
					{
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Partition_SchemaName, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "SchemaName"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "SchemaName", typeof(string), this.body.SchemaName, value);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.Partition_SchemaName, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					string schemaName = this.body.SchemaName;
					this.body.SchemaName = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "SchemaName", typeof(string), schemaName, value);
				}
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x00049F25 File Offset: 0x00048125
		// (set) Token: 0x060008BF RID: 2239 RVA: 0x00049F38 File Offset: 0x00048138
		public Table Table
		{
			get
			{
				return this.body.TableID.Object;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.TableID.Object, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Table", typeof(Table), this.body.TableID.Object, value);
					Table @object = this.body.TableID.Object;
					this.body.TableID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Table", typeof(Table), @object, value);
				}
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x00049FBC File Offset: 0x000481BC
		// (set) Token: 0x060008C1 RID: 2241 RVA: 0x00049FCE File Offset: 0x000481CE
		internal ObjectId _TableID
		{
			get
			{
				return this.body.TableID.ObjectID;
			}
			set
			{
				this.body.TableID.ObjectID = value;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x00049FE1 File Offset: 0x000481E1
		// (set) Token: 0x060008C3 RID: 2243 RVA: 0x00049FF4 File Offset: 0x000481F4
		internal DataSource DataSource
		{
			get
			{
				return this.body.DataSourceID.Object;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.DataSourceID.Object, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "DataSource", typeof(DataSource), this.body.DataSourceID.Object, value);
					DataSource @object = this.body.DataSourceID.Object;
					this.body.DataSourceID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "DataSource", typeof(DataSource), @object, value);
				}
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x0004A078 File Offset: 0x00048278
		// (set) Token: 0x060008C5 RID: 2245 RVA: 0x0004A08A File Offset: 0x0004828A
		internal ObjectId _DataSourceID
		{
			get
			{
				return this.body.DataSourceID.ObjectID;
			}
			set
			{
				this.body.DataSourceID.ObjectID = value;
			}
		}

		// Token: 0x060008C6 RID: 2246 RVA: 0x0004A0A0 File Offset: 0x000482A0
		internal void SetDataSource(StandaloneCrossLink<Partition, DataSource> standaloneLink)
		{
			if (standaloneLink.Object != null)
			{
				ObjectChangeTracker.RegisterPropertyChanging(this, "DataSource", typeof(DataSource), this.body.DataSourceID.Object, standaloneLink.Object);
				DataSource @object = this.body.DataSourceID.Object;
				this.body.DataSourceID.Object = standaloneLink.Object;
				ObjectChangeTracker.RegisterPropertyChanged(this, "DataSource", typeof(DataSource), @object, standaloneLink.Object);
				return;
			}
			if (standaloneLink.Path != null && !standaloneLink.Path.IsEmpty)
			{
				ObjectChangeTracker.RegisterPropertyChanging(this, "DataSource.Path", typeof(ObjectPath), this.body.DataSourceID.Path, standaloneLink.Path);
				ObjectPath objectPath;
				if ((objectPath = this.body.DataSourceID.Path) == null)
				{
					DataSource object2 = this.body.DataSourceID.Object;
					objectPath = ((object2 != null) ? object2.GetPath(null) : null);
				}
				ObjectPath objectPath2 = objectPath;
				this.body.DataSourceID.ObjectID = ObjectId.Null;
				this.body.DataSourceID.Path = standaloneLink.Path.Clone();
				ObjectChangeTracker.RegisterPropertyChanged(this, "DataSource.Path", typeof(ObjectPath), objectPath2, standaloneLink.Path);
				return;
			}
			if (this.body.DataSourceID.Object != null || (this.body.DataSourceID.Path != null && !this.body.DataSourceID.Path.IsEmpty))
			{
				ObjectChangeTracker.RegisterPropertyChanging(this, "DataSource", typeof(DataSource), this.body.DataSourceID.Object, null);
				DataSource object3 = this.body.DataSourceID.Object;
				this.body.DataSourceID.ObjectID = ObjectId.Null;
				this.body.DataSourceID.Path = null;
				ObjectChangeTracker.RegisterPropertyChanged(this, "DataSource", typeof(DataSource), object3, null);
			}
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x0004A29C File Offset: 0x0004849C
		internal void ResetDataSource()
		{
			if (this.body.DataSourceID.Object != null || (this.body.DataSourceID.Path != null && !this.body.DataSourceID.Path.IsEmpty))
			{
				ObjectChangeTracker.RegisterPropertyChanging(this, "DataSource", typeof(DataSource), this.body.DataSourceID.Object, null);
				DataSource @object = this.body.DataSourceID.Object;
				this.body.DataSourceID.ObjectID = ObjectId.Null;
				this.body.DataSourceID.Path = null;
				ObjectChangeTracker.RegisterPropertyChanged(this, "DataSource", typeof(DataSource), @object, null);
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060008C8 RID: 2248 RVA: 0x0004A35B File Offset: 0x0004855B
		// (set) Token: 0x060008C9 RID: 2249 RVA: 0x0004A370 File Offset: 0x00048570
		[CompatibilityRequirement("1480")]
		public QueryGroup QueryGroup
		{
			get
			{
				return this.body.QueryGroupID.Object;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.QueryGroupID.Object, value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (value != null)
					{
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Partition_QueryGroup, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "QueryGroup"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "QueryGroup", typeof(QueryGroup), this.body.QueryGroupID.Object, value);
					if (value != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.Partition_QueryGroup, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					QueryGroup @object = this.body.QueryGroupID.Object;
					this.body.QueryGroupID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "QueryGroup", typeof(QueryGroup), @object, value);
				}
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x0004A434 File Offset: 0x00048634
		// (set) Token: 0x060008CB RID: 2251 RVA: 0x0004A446 File Offset: 0x00048646
		internal ObjectId _QueryGroupID
		{
			get
			{
				return this.body.QueryGroupID.ObjectID;
			}
			set
			{
				this.body.QueryGroupID.ObjectID = value;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x0004A459 File Offset: 0x00048659
		// (set) Token: 0x060008CD RID: 2253 RVA: 0x0004A46C File Offset: 0x0004866C
		[CompatibilityRequirement("1530")]
		internal NamedExpression ExpressionSource
		{
			get
			{
				return this.body.ExpressionSourceID.Object;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.ExpressionSourceID.Object, value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (value != null)
					{
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Partition_ExpressionSource, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "ExpressionSource"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "ExpressionSource", typeof(NamedExpression), this.body.ExpressionSourceID.Object, value);
					if (value != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.Partition_ExpressionSource, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					NamedExpression @object = this.body.ExpressionSourceID.Object;
					this.body.ExpressionSourceID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "ExpressionSource", typeof(NamedExpression), @object, value);
				}
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x0004A530 File Offset: 0x00048730
		// (set) Token: 0x060008CF RID: 2255 RVA: 0x0004A542 File Offset: 0x00048742
		internal ObjectId _ExpressionSourceID
		{
			get
			{
				return this.body.ExpressionSourceID.ObjectID;
			}
			set
			{
				this.body.ExpressionSourceID.ObjectID = value;
			}
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0004A558 File Offset: 0x00048758
		internal void SetExpressionSource(StandaloneCrossLink<Partition, NamedExpression> standaloneLink)
		{
			if (standaloneLink.Object != null)
			{
				KeyValuePair<CompatibilityMode, Stack<string>>[] array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Partition_ExpressionSource, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "ExpressionSource"));
				ObjectChangeTracker.RegisterPropertyChanging(this, "ExpressionSource", typeof(NamedExpression), this.body.ExpressionSourceID.Object, standaloneLink.Object);
				base.SetCompatibilityRequirement(CompatibilityRestrictions.Partition_ExpressionSource, array);
				NamedExpression @object = this.body.ExpressionSourceID.Object;
				this.body.ExpressionSourceID.Object = standaloneLink.Object;
				ObjectChangeTracker.RegisterPropertyChanged(this, "ExpressionSource", typeof(NamedExpression), @object, standaloneLink.Object);
				return;
			}
			if (standaloneLink.Path != null && !standaloneLink.Path.IsEmpty)
			{
				KeyValuePair<CompatibilityMode, Stack<string>>[] array2 = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.Partition_ExpressionSource, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "ExpressionSource"));
				ObjectChangeTracker.RegisterPropertyChanging(this, "ExpressionSource.Path", typeof(ObjectPath), this.body.ExpressionSourceID.Path, standaloneLink.Path);
				base.SetCompatibilityRequirement(CompatibilityRestrictions.Partition_ExpressionSource, array2);
				ObjectPath objectPath;
				if ((objectPath = this.body.ExpressionSourceID.Path) == null)
				{
					NamedExpression object2 = this.body.ExpressionSourceID.Object;
					objectPath = ((object2 != null) ? object2.GetPath(null) : null);
				}
				ObjectPath objectPath2 = objectPath;
				this.body.ExpressionSourceID.ObjectID = ObjectId.Null;
				this.body.ExpressionSourceID.Path = standaloneLink.Path.Clone();
				ObjectChangeTracker.RegisterPropertyChanged(this, "ExpressionSource.Path", typeof(ObjectPath), objectPath2, standaloneLink.Path);
				return;
			}
			if (this.body.ExpressionSourceID.Object != null || (this.body.ExpressionSourceID.Path != null && !this.body.ExpressionSourceID.Path.IsEmpty))
			{
				ObjectChangeTracker.RegisterPropertyChanging(this, "ExpressionSource", typeof(NamedExpression), this.body.ExpressionSourceID.Object, null);
				base.ResetCompatibilityRequirement();
				NamedExpression object3 = this.body.ExpressionSourceID.Object;
				this.body.ExpressionSourceID.ObjectID = ObjectId.Null;
				this.body.ExpressionSourceID.Path = null;
				ObjectChangeTracker.RegisterPropertyChanged(this, "ExpressionSource", typeof(NamedExpression), object3, null);
			}
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0004A7B8 File Offset: 0x000489B8
		internal void ResetExpressionSource()
		{
			if (this.body.ExpressionSourceID.Object != null || (this.body.ExpressionSourceID.Path != null && !this.body.ExpressionSourceID.Path.IsEmpty))
			{
				ObjectChangeTracker.RegisterPropertyChanging(this, "ExpressionSource", typeof(NamedExpression), this.body.ExpressionSourceID.Object, null);
				base.ResetCompatibilityRequirement();
				NamedExpression @object = this.body.ExpressionSourceID.Object;
				this.body.ExpressionSourceID.ObjectID = ObjectId.Null;
				this.body.ExpressionSourceID.Path = null;
				ObjectChangeTracker.RegisterPropertyChanged(this, "ExpressionSource", typeof(NamedExpression), @object, null);
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060008D2 RID: 2258 RVA: 0x0004A87D File Offset: 0x00048A7D
		// (set) Token: 0x060008D3 RID: 2259 RVA: 0x0004A890 File Offset: 0x00048A90
		[CompatibilityRequirement("1603")]
		public DataCoverageDefinition DataCoverageDefinition
		{
			get
			{
				return this.body.DataCoverageDefinitionID.Object;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.DataCoverageDefinitionID.Object, value))
				{
					if (value != null)
					{
						base.ValidateCompatibilityRequirement(value, "DataCoverageDefinition", CompatibilityRestrictions.Partition_DataCoverageDefinition);
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "DataCoverageDefinition", typeof(DataCoverageDefinition), this.body.DataCoverageDefinitionID.Object, value);
					DataCoverageDefinition @object = this.body.DataCoverageDefinitionID.Object;
					this.body.DataCoverageDefinitionID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "DataCoverageDefinition", typeof(DataCoverageDefinition), @object, value);
				}
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x0004A929 File Offset: 0x00048B29
		// (set) Token: 0x060008D5 RID: 2261 RVA: 0x0004A93B File Offset: 0x00048B3B
		internal ObjectId _DataCoverageDefinitionID
		{
			get
			{
				return this.body.DataCoverageDefinitionID.ObjectID;
			}
			set
			{
				this.body.DataCoverageDefinitionID.ObjectID = value;
			}
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0004A950 File Offset: 0x00048B50
		internal void CopyFrom(Partition other, CopyContext context)
		{
			base.CopyFrom(other, context);
			if ((context.Flags & CopyFlags.IncludeCompatRestictions) == CopyFlags.IncludeCompatRestictions || !this.body.IsEqualTo(other.body, context))
			{
				ObjectChangeTracker.RegisterUpcomingPropertyChange(this);
				this.body.CopyFrom(other.body, context);
			}
			else if ((context.Flags & CopyFlags.ShallowCopy) != CopyFlags.ShallowCopy && this.body.DataCoverageDefinitionID.Object != null && other.body.DataCoverageDefinitionID.Object != null)
			{
				this.body.DataCoverageDefinitionID.Object.CopyFrom(other.body.DataCoverageDefinitionID.Object, context);
			}
			if ((context.Flags & CopyFlags.ShallowCopy) != CopyFlags.ShallowCopy)
			{
				this.Annotations.CopyFrom(other.Annotations, context);
				this.ExtendedProperties.CopyFrom(other.ExtendedProperties, context);
			}
			this.RecreatePartitionSourceIfMismatchWithPartitionType((context.Flags & CopyFlags.DontResolveCrossLinks) != CopyFlags.DontResolveCrossLinks);
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0004AA44 File Offset: 0x00048C44
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((Partition)other, context);
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0004AA53 File Offset: 0x00048C53
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(Partition other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0004AA6F File Offset: 0x00048C6F
		public void CopyTo(Partition other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0004AA8B File Offset: 0x00048C8B
		public Partition Clone()
		{
			return base.CloneInternal<Partition>();
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x0004AA94 File Offset: 0x00048C94
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			if (this.body.TableID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "TableID", this.body.TableID.Object);
			}
			this.body.DataSourceID.Validate(null, true);
			if (this.body.DataSourceID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "DataSourceID", this.body.DataSourceID.Object);
			}
			this.body.QueryGroupID.Validate(null, true);
			if (this.body.QueryGroupID.Object != null)
			{
				if (!CompatibilityRestrictions.Partition_QueryGroup.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member QueryGroupID is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				MetadataObject.WriteObjectId(writer, options, "QueryGroupID", this.body.QueryGroupID.Object);
			}
			this.body.ExpressionSourceID.Validate(null, true);
			if (this.body.ExpressionSourceID.Object != null)
			{
				if (!CompatibilityRestrictions.Partition_ExpressionSource.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member ExpressionSourceID is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				MetadataObject.WriteObjectId(writer, options, "ExpressionSourceID", this.body.ExpressionSourceID.Object);
			}
			if (!string.IsNullOrEmpty(this.body.Name))
			{
				writer.WriteProperty<string>(options, "Name", this.body.Name);
			}
			if (!string.IsNullOrEmpty(this.body.Description))
			{
				writer.WriteProperty<string>(options, "Description", this.body.Description);
			}
			if (!string.IsNullOrEmpty(this.body.QueryDefinition))
			{
				writer.WriteProperty<string>(options, "QueryDefinition", this.body.QueryDefinition);
			}
			if (this.body.Type != PartitionSourceType.None)
			{
				if (!PropertyHelper.IsPartitionSourceTypeValueCompatible(this.body.Type, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member Type is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<PartitionSourceType>(options, "Type", this.body.Type);
			}
			if (this.body.Mode != ModeType.Default)
			{
				if (!PropertyHelper.IsModeTypeValueCompatible(this.body.Mode, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member Mode is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<ModeType>(options, "Mode", this.body.Mode);
			}
			if (this.body.DataView != DataViewType.Default)
			{
				writer.WriteProperty<DataViewType>(options, "DataView", this.body.DataView);
			}
			if (this.body.RetainDataTillForceCalculate)
			{
				if (!CompatibilityRestrictions.Partition_RetainDataTillForceCalculate.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member RetainDataTillForceCalculate is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<bool>(options, "RetainDataTillForceCalculate", this.body.RetainDataTillForceCalculate);
			}
			if (this.body.RangeStart.CompareTo(new DateTime(0L)) != 0)
			{
				if (!CompatibilityRestrictions.Partition_RangeStart.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member RangeStart is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<DateTime>(options, "RangeStart", this.body.RangeStart);
			}
			if (this.body.RangeEnd.CompareTo(new DateTime(0L)) != 0)
			{
				if (!CompatibilityRestrictions.Partition_RangeEnd.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member RangeEnd is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<DateTime>(options, "RangeEnd", this.body.RangeEnd);
			}
			if (this.body.RangeGranularity != RefreshGranularityType.Invalid)
			{
				if (!CompatibilityRestrictions.Partition_RangeGranularity.IsCompatible(mode, dbCompatibilityLevel) || !PropertyHelper.IsRefreshGranularityTypeValueCompatible(this.body.RangeGranularity, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member RangeGranularity is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<RefreshGranularityType>(options, "RangeGranularity", this.body.RangeGranularity);
			}
			if (!string.IsNullOrEmpty(this.body.RefreshBookmark))
			{
				if (!CompatibilityRestrictions.Partition_RefreshBookmark.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member RefreshBookmark is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<string>(options, "RefreshBookmark", this.body.RefreshBookmark);
			}
			if (!string.IsNullOrEmpty(this.body.MAttributes))
			{
				if (!CompatibilityRestrictions.Partition_MAttributes.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member MAttributes is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<string>(options, "MAttributes", this.body.MAttributes);
			}
			if (!string.IsNullOrEmpty(this.body.SchemaName))
			{
				if (!CompatibilityRestrictions.Partition_SchemaName.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member SchemaName is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<string>(options, "SchemaName", this.body.SchemaName);
			}
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0004AFFC File Offset: 0x000491FC
		void IMetadataObjectWithOverrides.WriteAllOverridenBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel, ReplacementPropertiesCollection newProperties)
		{
			this.body.DataSourceID.Validate(null, true);
			MetadataObject metadataObject;
			if (newProperties.IsLinkOverriden("DataSourceID", out metadataObject) && metadataObject != null && this.body.DataSourceID.Object != metadataObject)
			{
				MetadataObject.WriteObjectId(writer, options, "DataSourceID", metadataObject);
			}
			this.body.ExpressionSourceID.Validate(null, true);
			MetadataObject metadataObject2;
			if (newProperties.IsLinkOverriden("ExpressionSourceID", out metadataObject2) && metadataObject2 != null && this.body.ExpressionSourceID.Object != metadataObject2)
			{
				if (!CompatibilityRestrictions.Partition_ExpressionSource.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member ExpressionSourceID is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				MetadataObject.WriteObjectId(writer, options, "ExpressionSourceID", metadataObject2);
			}
			string text;
			if (newProperties.IsPropertyOverriden<string>("QueryDefinition", out text) && !PropertyHelper.AreValuesIdentical(this.body.QueryDefinition, text))
			{
				writer.WriteProperty<string>(options, "QueryDefinition", text);
			}
			PartitionSourceType partitionSourceType;
			if (newProperties.IsPropertyOverriden<PartitionSourceType>("Type", out partitionSourceType) && !PropertyHelper.AreValuesIdentical(this.body.Type, partitionSourceType))
			{
				if (!PropertyHelper.IsPartitionSourceTypeValueCompatible(partitionSourceType, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member Type is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<PartitionSourceType>(options, "Type", partitionSourceType);
			}
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0004B158 File Offset: 0x00049358
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("TableID", out objectId))
			{
				this.body.TableID.ObjectID = objectId;
			}
			ObjectId objectId2;
			if (reader.TryReadProperty<ObjectId>("DataSourceID", out objectId2))
			{
				this.body.DataSourceID.ObjectID = objectId2;
			}
			ObjectId objectId3;
			if (CompatibilityRestrictions.Partition_QueryGroup.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<ObjectId>("QueryGroupID", out objectId3))
			{
				this.body.QueryGroupID.ObjectID = objectId3;
			}
			ObjectId objectId4;
			if (CompatibilityRestrictions.Partition_ExpressionSource.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<ObjectId>("ExpressionSourceID", out objectId4))
			{
				this.body.ExpressionSourceID.ObjectID = objectId4;
			}
			ObjectId objectId5;
			if (CompatibilityRestrictions.Partition_DataCoverageDefinition.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<ObjectId>("DataCoverageDefinitionID", out objectId5))
			{
				this.body.DataCoverageDefinitionID.ObjectID = objectId5;
			}
			string text;
			if (reader.TryReadProperty<string>("Name", out text))
			{
				this.body.Name = text;
			}
			string text2;
			if (reader.TryReadProperty<string>("Description", out text2))
			{
				this.body.Description = text2;
			}
			string text3;
			if (reader.TryReadProperty<string>("QueryDefinition", out text3))
			{
				this.body.QueryDefinition = text3;
			}
			ObjectState objectState;
			if (reader.TryReadProperty<ObjectState>("State", out objectState))
			{
				this.body.State = objectState;
			}
			PartitionSourceType partitionSourceType;
			if (reader.TryReadProperty<PartitionSourceType>("Type", out partitionSourceType))
			{
				this.body.Type = partitionSourceType;
			}
			ModeType modeType;
			if (reader.TryReadProperty<ModeType>("Mode", out modeType))
			{
				this.body.Mode = modeType;
			}
			DataViewType dataViewType;
			if (reader.TryReadProperty<DataViewType>("DataView", out dataViewType))
			{
				this.body.DataView = dataViewType;
			}
			DateTime dateTime;
			if (reader.TryReadProperty<DateTime>("ModifiedTime", out dateTime))
			{
				this.body.ModifiedTime = dateTime;
			}
			DateTime dateTime2;
			if (reader.TryReadProperty<DateTime>("RefreshedTime", out dateTime2))
			{
				this.body.RefreshedTime = dateTime2;
			}
			string text4;
			if (reader.TryReadProperty<string>("ErrorMessage", out text4))
			{
				this.body.ErrorMessage = text4;
			}
			bool flag;
			if (CompatibilityRestrictions.Partition_RetainDataTillForceCalculate.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<bool>("RetainDataTillForceCalculate", out flag))
			{
				this.body.RetainDataTillForceCalculate = flag;
			}
			DateTime dateTime3;
			if (CompatibilityRestrictions.Partition_RangeStart.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<DateTime>("RangeStart", out dateTime3))
			{
				this.body.RangeStart = dateTime3;
			}
			DateTime dateTime4;
			if (CompatibilityRestrictions.Partition_RangeEnd.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<DateTime>("RangeEnd", out dateTime4))
			{
				this.body.RangeEnd = dateTime4;
			}
			RefreshGranularityType refreshGranularityType;
			if (CompatibilityRestrictions.Partition_RangeGranularity.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<RefreshGranularityType>("RangeGranularity", out refreshGranularityType))
			{
				this.body.RangeGranularity = refreshGranularityType;
			}
			string text5;
			if (CompatibilityRestrictions.Partition_RefreshBookmark.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<string>("RefreshBookmark", out text5))
			{
				this.body.RefreshBookmark = text5;
			}
			string text6;
			if (CompatibilityRestrictions.Partition_MAttributes.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<string>("MAttributes", out text6))
			{
				this.body.MAttributes = text6;
			}
			string text7;
			if (CompatibilityRestrictions.Partition_SchemaName.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<string>("SchemaName", out text7))
			{
				this.body.SchemaName = text7;
			}
			this.RecreatePartitionSourceIfMismatchWithPartitionType(true);
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x0004B480 File Offset: 0x00049680
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataBodyProperties(context, writer);
			if (this.body.TableID.Object != null && writer.ShouldIncludeProperty("TableID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly))
			{
				writer.WriteObjectIdProperty("TableID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly, this.body.TableID.Object);
			}
			this.body.DataSourceID.Validate(null, true);
			if (this.body.DataSourceID.Object != null && writer.ShouldIncludeProperty("DataSourceID", MetadataPropertyNature.CrossLinkProperty))
			{
				writer.WriteObjectIdProperty("DataSourceID", MetadataPropertyNature.CrossLinkProperty, this.body.DataSourceID.Object);
			}
			this.body.QueryGroupID.Validate(null, true);
			if (this.body.QueryGroupID.Object != null)
			{
				if (!CompatibilityRestrictions.Partition_QueryGroup.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member QueryGroupID is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("QueryGroupID", MetadataPropertyNature.CrossLinkProperty))
				{
					writer.WriteObjectIdProperty("QueryGroupID", MetadataPropertyNature.CrossLinkProperty, this.body.QueryGroupID.Object);
				}
			}
			this.body.ExpressionSourceID.Validate(null, true);
			if (this.body.ExpressionSourceID.Object != null)
			{
				if (!CompatibilityRestrictions.Partition_ExpressionSource.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member ExpressionSourceID is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("ExpressionSourceID", MetadataPropertyNature.CrossLinkProperty))
				{
					writer.WriteObjectIdProperty("ExpressionSourceID", MetadataPropertyNature.CrossLinkProperty, this.body.ExpressionSourceID.Object);
				}
			}
			if (!string.IsNullOrEmpty(this.body.Name) && writer.ShouldIncludeProperty("Name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("Name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.Name);
			}
			if (!string.IsNullOrEmpty(this.body.Description) && writer.ShouldIncludeProperty("Description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("Description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, this.body.Description);
			}
			if (!string.IsNullOrEmpty(this.body.QueryDefinition) && writer.ShouldIncludeProperty("QueryDefinition", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("QueryDefinition", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, this.body.QueryDefinition);
			}
			if (this.body.Type != PartitionSourceType.None)
			{
				if (!PropertyHelper.IsPartitionSourceTypeValueCompatible(this.body.Type, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member Type is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("Type", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.DefaultProperty))
				{
					writer.WriteEnumProperty<PartitionSourceType>("Type", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.DefaultProperty, this.body.Type);
				}
			}
			if (this.body.Mode != ModeType.Default)
			{
				if (!PropertyHelper.IsModeTypeValueCompatible(this.body.Mode, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member Mode is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("Mode", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<ModeType>("Mode", MetadataPropertyNature.RegularProperty, this.body.Mode);
				}
			}
			if (this.body.DataView != DataViewType.Default && writer.ShouldIncludeProperty("DataView", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteEnumProperty<DataViewType>("DataView", MetadataPropertyNature.RegularProperty, this.body.DataView);
			}
			if (this.body.RetainDataTillForceCalculate)
			{
				if (!CompatibilityRestrictions.Partition_RetainDataTillForceCalculate.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member RetainDataTillForceCalculate is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("RetainDataTillForceCalculate", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteBooleanProperty("RetainDataTillForceCalculate", MetadataPropertyNature.RegularProperty, this.body.RetainDataTillForceCalculate);
				}
			}
			if (this.body.RangeStart.CompareTo(new DateTime(0L)) != 0)
			{
				if (!CompatibilityRestrictions.Partition_RangeStart.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member RangeStart is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("RangeStart", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Timestamp))
				{
					writer.WriteDateTimeProperty("RangeStart", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Timestamp, this.body.RangeStart);
				}
			}
			if (this.body.RangeEnd.CompareTo(new DateTime(0L)) != 0)
			{
				if (!CompatibilityRestrictions.Partition_RangeEnd.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member RangeEnd is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("RangeEnd", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Timestamp))
				{
					writer.WriteDateTimeProperty("RangeEnd", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Timestamp, this.body.RangeEnd);
				}
			}
			if (this.body.RangeGranularity != RefreshGranularityType.Invalid)
			{
				if (!CompatibilityRestrictions.Partition_RangeGranularity.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) || !PropertyHelper.IsRefreshGranularityTypeValueCompatible(this.body.RangeGranularity, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member RangeGranularity is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("RangeGranularity", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<RefreshGranularityType>("RangeGranularity", MetadataPropertyNature.RegularProperty, this.body.RangeGranularity);
				}
			}
			if (!string.IsNullOrEmpty(this.body.RefreshBookmark))
			{
				if (!CompatibilityRestrictions.Partition_RefreshBookmark.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member RefreshBookmark is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("RefreshBookmark", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteStringProperty("RefreshBookmark", MetadataPropertyNature.RegularProperty, this.body.RefreshBookmark);
				}
			}
			if (!string.IsNullOrEmpty(this.body.MAttributes))
			{
				if (!CompatibilityRestrictions.Partition_MAttributes.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member MAttributes is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("MAttributes", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteStringProperty("MAttributes", MetadataPropertyNature.RegularProperty, this.body.MAttributes);
				}
			}
			if (!string.IsNullOrEmpty(this.body.SchemaName))
			{
				if (!CompatibilityRestrictions.Partition_SchemaName.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member SchemaName is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("SchemaName", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
				{
					writer.WriteStringProperty("SchemaName", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, this.body.SchemaName);
				}
			}
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0004BBE4 File Offset: 0x00049DE4
		internal void WritePartitionPropertiesInMergedModeToMetadataStream(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (this.body.State != ObjectState.NoData)
			{
				if (!PropertyHelper.IsObjectStateValueCompatible(this.body.State, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member State is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("state", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred))
				{
					writer.WriteEnumProperty<ObjectState>("state", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred, this.body.State);
				}
			}
			if (this.body.Mode != ModeType.Default)
			{
				if (!PropertyHelper.IsModeTypeValueCompatible(this.body.Mode, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member Mode is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("mode", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<ModeType>("mode", MetadataPropertyNature.RegularProperty, this.body.Mode);
				}
			}
			if (this.body.DataView != DataViewType.Default && writer.ShouldIncludeProperty("dataView", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteEnumProperty<DataViewType>("dataView", MetadataPropertyNature.RegularProperty, this.body.DataView);
			}
			if (this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.ModifiedTime);
			}
			if (this.body.RefreshedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("refreshedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("refreshedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.RefreshedTime);
			}
			if (!string.IsNullOrEmpty(this.body.ErrorMessage) && writer.ShouldIncludeProperty("errorMessage", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred))
			{
				writer.WriteStringProperty("errorMessage", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred, this.body.ErrorMessage);
			}
			if (this.body.QueryGroupID.Object != null)
			{
				if (!CompatibilityRestrictions.Partition_QueryGroup.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member QueryGroupID is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("queryGroup", MetadataPropertyNature.CrossLinkProperty))
				{
					this.body.QueryGroupID.WriteToMetadataStream(ObjectType.QueryGroup, true, "queryGroup", false, writer);
				}
			}
			if (writer.ShouldIncludeProperty("source", MetadataPropertyNature.ChildProperty))
			{
				IEnumerable<MetadataProperty> enumerable = ((this.source != null) ? this.source.GetMetadataProperties(context) : PartitionSource.GetJsonMetadataPropertiesForEmptySource());
				writer.WriteComplexProperty("source", MetadataPropertyNature.ChildProperty, enumerable);
			}
			if (this.body.DataCoverageDefinitionID.Object != null)
			{
				if (!CompatibilityRestrictions.Partition_DataCoverageDefinition.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DataCoverageDefinitionID is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("dataCoverageDefinition", MetadataPropertyNature.ChildProperty))
				{
					writer.WriteSingleChild(context, "dataCoverageDefinition", MetadataPropertyNature.ChildProperty, this.body.DataCoverageDefinitionID.Object);
				}
			}
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x0004BF28 File Offset: 0x0004A128
		internal bool TryReadPartitionPropertyInMergedModeFromMetadataStream(SerializationActivityContext context, IMetadataReader reader, ref UnexpectedPropertyClassification classification)
		{
			string propertyName = reader.PropertyName;
			if (propertyName != null)
			{
				int length = propertyName.Length;
				switch (length)
				{
				case 4:
					if (propertyName == "mode")
					{
						ModeType modeType = reader.ReadEnumProperty<ModeType>();
						if (!PropertyHelper.IsModeTypeValueCompatible(modeType, context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							classification = UnexpectedPropertyClassification.IncompatiblePropertyValue;
							return false;
						}
						this.body.Mode = modeType;
						return true;
					}
					break;
				case 5:
					if (propertyName == "state")
					{
						ObjectState objectState = reader.ReadEnumProperty<ObjectState>();
						if (!PropertyHelper.IsObjectStateValueCompatible(objectState, context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							classification = UnexpectedPropertyClassification.IncompatiblePropertyValue;
							return false;
						}
						this.body.State = objectState;
						return true;
					}
					break;
				case 6:
					if (propertyName == "source")
					{
						IMetadataReader metadataReader = reader.ReadComplexProperty(true);
						PartitionSource partitionSource = PartitionSource.CreateFromMetadataStream(context, metadataReader);
						if (partitionSource != null)
						{
							this.Source = partitionSource;
						}
						return true;
					}
					break;
				case 7:
				case 9:
				case 11:
					break;
				case 8:
					if (propertyName == "dataView")
					{
						this.body.DataView = reader.ReadEnumProperty<DataViewType>();
						return true;
					}
					break;
				case 10:
					if (propertyName == "queryGroup")
					{
						if (!CompatibilityRestrictions.Partition_QueryGroup.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							classification = UnexpectedPropertyClassification.IncompatibleProperty;
							return false;
						}
						this.body.QueryGroupID.Path = reader.ReadCrossLinkProperty((string p) => new ObjectPath(ObjectType.QueryGroup, p));
						return true;
					}
					break;
				case 12:
				{
					char c = propertyName[0];
					if (c != 'e')
					{
						if (c == 'm')
						{
							if (propertyName == "modifiedTime")
							{
								this.body.ModifiedTime = reader.ReadDateTimeProperty();
								return true;
							}
						}
					}
					else if (propertyName == "errorMessage")
					{
						this.body.ErrorMessage = reader.ReadStringProperty();
						return true;
					}
					break;
				}
				case 13:
					if (propertyName == "refreshedTime")
					{
						this.body.RefreshedTime = reader.ReadDateTimeProperty();
						return true;
					}
					break;
				default:
					if (length == 22)
					{
						if (propertyName == "dataCoverageDefinition")
						{
							if (!CompatibilityRestrictions.Partition_DataCoverageDefinition.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
							{
								classification = UnexpectedPropertyClassification.IncompatibleProperty;
								return false;
							}
							using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
							{
								DataCoverageDefinition dataCoverageDefinition = reader.ReadSingleChildProperty<DataCoverageDefinition>(context);
								try
								{
									this.body.DataCoverageDefinitionID.Object = dataCoverageDefinition;
								}
								catch (Exception ex)
								{
									throw reader.CreateInvalidChildException(context, dataCoverageDefinition, TomSR.Exception_FailedAddDeserializedObject("DataCoverageDefinition", ex.Message), ex);
								}
							}
							return true;
						}
					}
					break;
				}
			}
			return false;
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x0004C218 File Offset: 0x0004A418
		private protected override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataTree(context, writer);
			if (!string.IsNullOrEmpty(this.body.Name) && writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.Name);
			}
			if (!string.IsNullOrEmpty(this.body.Description) && writer.ShouldIncludeProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, this.body.Description);
			}
			if (this.body.State != ObjectState.NoData)
			{
				if (!PropertyHelper.IsObjectStateValueCompatible(this.body.State, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member State is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("state", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred))
				{
					writer.WriteEnumProperty<ObjectState>("state", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred, this.body.State);
				}
			}
			if (this.body.Mode != ModeType.Default)
			{
				if (!PropertyHelper.IsModeTypeValueCompatible(this.body.Mode, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member Mode is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("mode", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<ModeType>("mode", MetadataPropertyNature.RegularProperty, this.body.Mode);
				}
			}
			if (this.body.DataView != DataViewType.Default && writer.ShouldIncludeProperty("dataView", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteEnumProperty<DataViewType>("dataView", MetadataPropertyNature.RegularProperty, this.body.DataView);
			}
			if (this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.ModifiedTime);
			}
			if (this.body.RefreshedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("refreshedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("refreshedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.RefreshedTime);
			}
			if (!string.IsNullOrEmpty(this.body.ErrorMessage) && writer.ShouldIncludeProperty("errorMessage", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred))
			{
				writer.WriteStringProperty("errorMessage", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred, this.body.ErrorMessage);
			}
			if (this.body.QueryGroupID.Object != null)
			{
				if (!CompatibilityRestrictions.Partition_QueryGroup.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member QueryGroupID is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("queryGroup", MetadataPropertyNature.CrossLinkProperty))
				{
					this.body.QueryGroupID.WriteToMetadataStream(ObjectType.QueryGroup, true, "queryGroup", false, writer);
				}
			}
			if (writer.ShouldIncludeProperty("source", MetadataPropertyNature.ChildProperty))
			{
				if (this.source != null)
				{
					writer.WriteComplexProperty("source", MetadataPropertyNature.ChildProperty, this.source.GetMetadataProperties(context));
				}
				else if (context.SerializationMode == MetadataSerializationMode.Json)
				{
					writer.WriteComplexProperty("source", MetadataPropertyNature.ChildProperty, PartitionSource.GetJsonMetadataPropertiesForEmptySource());
				}
			}
			if (this.body.DataCoverageDefinitionID.Object != null)
			{
				if (!CompatibilityRestrictions.Partition_DataCoverageDefinition.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DataCoverageDefinitionID is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("dataCoverageDefinition", MetadataPropertyNature.ChildProperty))
				{
					writer.WriteSingleChild(context, "dataCoverageDefinition", MetadataPropertyNature.ChildProperty, this.body.DataCoverageDefinitionID.Object);
				}
			}
			if (this.ExtendedProperties.Count > 0)
			{
				if (!CompatibilityRestrictions.ExtendedProperty.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("A child ExtendedProperty is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("extendedProperties", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "extendedProperties", MetadataPropertyNature.ChildCollection, this.ExtendedProperties);
				}
			}
			if (this.Annotations.Count > 0 && writer.ShouldIncludeProperty("annotations", MetadataPropertyNature.ChildCollection))
			{
				writer.WriteChildCollection(context, "annotations", MetadataPropertyNature.ChildCollection, this.Annotations);
			}
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0004C698 File Offset: 0x0004A898
		private protected override bool TryReadNextMetadataProperty(SerializationActivityContext context, IMetadataReader reader, out UnexpectedPropertyClassification classification)
		{
			if (base.TryReadNextMetadataProperty(context, reader, out classification))
			{
				return true;
			}
			string propertyName = reader.PropertyName;
			if (propertyName != null)
			{
				switch (propertyName.Length)
				{
				case 4:
				{
					char c = propertyName[0];
					if (c <= 'N')
					{
						if (c != 'M')
						{
							if (c != 'N')
							{
								break;
							}
							if (!(propertyName == "Name"))
							{
								break;
							}
						}
						else
						{
							if (!(propertyName == "Mode"))
							{
								break;
							}
							goto IL_06A1;
						}
					}
					else if (c != 'T')
					{
						if (c != 'm')
						{
							if (c != 'n')
							{
								break;
							}
							if (!(propertyName == "name"))
							{
								break;
							}
						}
						else
						{
							if (!(propertyName == "mode"))
							{
								break;
							}
							goto IL_06A1;
						}
					}
					else
					{
						if (!(propertyName == "Type"))
						{
							break;
						}
						PartitionSourceType partitionSourceType = reader.ReadEnumProperty<PartitionSourceType>();
						if (!PropertyHelper.IsPartitionSourceTypeValueCompatible(partitionSourceType, context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							classification = UnexpectedPropertyClassification.IncompatiblePropertyValue;
							return false;
						}
						this.body.Type = partitionSourceType;
						return true;
					}
					this.body.Name = reader.ReadStringProperty();
					return true;
					IL_06A1:
					ModeType modeType = reader.ReadEnumProperty<ModeType>();
					if (!PropertyHelper.IsModeTypeValueCompatible(modeType, context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatiblePropertyValue;
						return false;
					}
					this.body.Mode = modeType;
					return true;
				}
				case 5:
				{
					char c = propertyName[0];
					if (c != 'S')
					{
						if (c != 's')
						{
							break;
						}
						if (!(propertyName == "state"))
						{
							break;
						}
					}
					else if (!(propertyName == "State"))
					{
						break;
					}
					ObjectState objectState = reader.ReadEnumProperty<ObjectState>();
					if (!PropertyHelper.IsObjectStateValueCompatible(objectState, context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatiblePropertyValue;
						return false;
					}
					this.body.State = objectState;
					return true;
				}
				case 6:
					if (propertyName == "source")
					{
						IMetadataReader metadataReader = reader.ReadComplexProperty(true);
						PartitionSource partitionSource = PartitionSource.CreateFromMetadataStream(context, metadataReader);
						if (partitionSource != null)
						{
							this.Source = partitionSource;
						}
						return true;
					}
					break;
				case 7:
					if (propertyName == "TableID")
					{
						this.body.TableID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					break;
				case 8:
				{
					char c = propertyName[0];
					if (c != 'D')
					{
						if (c != 'R')
						{
							if (c != 'd')
							{
								break;
							}
							if (!(propertyName == "dataView"))
							{
								break;
							}
						}
						else
						{
							if (!(propertyName == "RangeEnd"))
							{
								break;
							}
							if (!CompatibilityRestrictions.Partition_RangeEnd.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
							{
								classification = UnexpectedPropertyClassification.IncompatibleProperty;
								return false;
							}
							this.body.RangeEnd = reader.ReadDateTimeProperty();
							return true;
						}
					}
					else if (!(propertyName == "DataView"))
					{
						break;
					}
					this.body.DataView = reader.ReadEnumProperty<DataViewType>();
					return true;
				}
				case 10:
				{
					char c = propertyName[0];
					if (c != 'R')
					{
						if (c != 'S')
						{
							if (c == 'q')
							{
								if (propertyName == "queryGroup")
								{
									if (!CompatibilityRestrictions.Partition_QueryGroup.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
									{
										classification = UnexpectedPropertyClassification.IncompatibleProperty;
										return false;
									}
									this.body.QueryGroupID.Path = reader.ReadCrossLinkProperty((string p) => new ObjectPath(ObjectType.QueryGroup, p));
									return true;
								}
							}
						}
						else if (propertyName == "SchemaName")
						{
							if (!CompatibilityRestrictions.Partition_SchemaName.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
							{
								classification = UnexpectedPropertyClassification.IncompatibleProperty;
								return false;
							}
							this.body.SchemaName = reader.ReadStringProperty();
							return true;
						}
					}
					else if (propertyName == "RangeStart")
					{
						if (!CompatibilityRestrictions.Partition_RangeStart.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							classification = UnexpectedPropertyClassification.IncompatibleProperty;
							return false;
						}
						this.body.RangeStart = reader.ReadDateTimeProperty();
						return true;
					}
					break;
				}
				case 11:
				{
					char c = propertyName[0];
					if (c <= 'M')
					{
						if (c != 'D')
						{
							if (c != 'M')
							{
								break;
							}
							if (!(propertyName == "MAttributes"))
							{
								break;
							}
							if (!CompatibilityRestrictions.Partition_MAttributes.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
							{
								classification = UnexpectedPropertyClassification.IncompatibleProperty;
								return false;
							}
							this.body.MAttributes = reader.ReadStringProperty();
							return true;
						}
						else if (!(propertyName == "Description"))
						{
							break;
						}
					}
					else if (c != 'a')
					{
						if (c != 'd')
						{
							break;
						}
						if (!(propertyName == "description"))
						{
							break;
						}
					}
					else
					{
						if (!(propertyName == "annotations"))
						{
							break;
						}
						using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
						{
							foreach (Annotation annotation in reader.ReadChildCollectionProperty<Annotation>(context))
							{
								try
								{
									this.Annotations.Add(annotation);
								}
								catch (Exception ex)
								{
									throw reader.CreateInvalidChildException(context, annotation, TomSR.Exception_FailedAddDeserializedNamedObject("Annotation", (annotation != null) ? annotation.Name : null, ex.Message), ex);
								}
							}
						}
						return true;
					}
					this.body.Description = reader.ReadStringProperty();
					return true;
				}
				case 12:
				{
					char c = propertyName[0];
					if (c <= 'M')
					{
						if (c != 'D')
						{
							if (c != 'E')
							{
								if (c != 'M')
								{
									break;
								}
								if (!(propertyName == "ModifiedTime"))
								{
									break;
								}
							}
							else
							{
								if (!(propertyName == "ErrorMessage"))
								{
									break;
								}
								goto IL_0708;
							}
						}
						else
						{
							if (!(propertyName == "DataSourceID"))
							{
								break;
							}
							this.body.DataSourceID.ObjectID = reader.ReadObjectIdProperty();
							return true;
						}
					}
					else if (c != 'Q')
					{
						if (c != 'e')
						{
							if (c != 'm')
							{
								break;
							}
							if (!(propertyName == "modifiedTime"))
							{
								break;
							}
						}
						else
						{
							if (!(propertyName == "errorMessage"))
							{
								break;
							}
							goto IL_0708;
						}
					}
					else
					{
						if (!(propertyName == "QueryGroupID"))
						{
							break;
						}
						if (!CompatibilityRestrictions.Partition_QueryGroup.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							return false;
						}
						this.body.QueryGroupID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					this.body.ModifiedTime = reader.ReadDateTimeProperty();
					return true;
					IL_0708:
					this.body.ErrorMessage = reader.ReadStringProperty();
					return true;
				}
				case 13:
				{
					char c = propertyName[0];
					if (c != 'R')
					{
						if (c != 'r')
						{
							break;
						}
						if (!(propertyName == "refreshedTime"))
						{
							break;
						}
					}
					else if (!(propertyName == "RefreshedTime"))
					{
						break;
					}
					this.body.RefreshedTime = reader.ReadDateTimeProperty();
					return true;
				}
				case 15:
				{
					char c = propertyName[0];
					if (c != 'Q')
					{
						if (c == 'R')
						{
							if (propertyName == "RefreshBookmark")
							{
								if (!CompatibilityRestrictions.Partition_RefreshBookmark.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
								{
									classification = UnexpectedPropertyClassification.IncompatibleProperty;
									return false;
								}
								this.body.RefreshBookmark = reader.ReadStringProperty();
								return true;
							}
						}
					}
					else if (propertyName == "QueryDefinition")
					{
						this.body.QueryDefinition = reader.ReadStringProperty();
						return true;
					}
					break;
				}
				case 16:
					if (propertyName == "RangeGranularity")
					{
						if (!CompatibilityRestrictions.Partition_RangeGranularity.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) || !CompatibilityRestrictions.RefreshGranularityType.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							classification = UnexpectedPropertyClassification.IncompatibleProperty;
							return false;
						}
						this.body.RangeGranularity = reader.ReadEnumProperty<RefreshGranularityType>();
						return true;
					}
					break;
				case 18:
				{
					char c = propertyName[0];
					if (c != 'E')
					{
						if (c == 'e')
						{
							if (propertyName == "extendedProperties")
							{
								if (!CompatibilityRestrictions.ExtendedProperty.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
								{
									classification = UnexpectedPropertyClassification.IncompatibleProperty;
									return false;
								}
								using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
								{
									foreach (ExtendedProperty extendedProperty in reader.ReadChildCollectionProperty<ExtendedProperty>(context))
									{
										try
										{
											this.ExtendedProperties.Add(extendedProperty);
										}
										catch (Exception ex2)
										{
											throw reader.CreateInvalidChildException(context, extendedProperty, TomSR.Exception_FailedAddDeserializedNamedObject("ExtendedProperty", (extendedProperty != null) ? extendedProperty.Name : null, ex2.Message), ex2);
										}
									}
								}
								return true;
							}
						}
					}
					else if (propertyName == "ExpressionSourceID")
					{
						if (!CompatibilityRestrictions.Partition_ExpressionSource.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							return false;
						}
						this.body.ExpressionSourceID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					break;
				}
				case 22:
					if (propertyName == "dataCoverageDefinition")
					{
						if (!CompatibilityRestrictions.Partition_DataCoverageDefinition.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							classification = UnexpectedPropertyClassification.IncompatibleProperty;
							return false;
						}
						using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
						{
							DataCoverageDefinition dataCoverageDefinition = reader.ReadSingleChildProperty<DataCoverageDefinition>(context);
							try
							{
								this.body.DataCoverageDefinitionID.Object = dataCoverageDefinition;
							}
							catch (Exception ex3)
							{
								throw reader.CreateInvalidChildException(context, dataCoverageDefinition, TomSR.Exception_FailedAddDeserializedObject("DataCoverageDefinition", ex3.Message), ex3);
							}
						}
						return true;
					}
					break;
				case 24:
					if (propertyName == "DataCoverageDefinitionID")
					{
						if (!CompatibilityRestrictions.Partition_DataCoverageDefinition.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							return false;
						}
						this.body.DataCoverageDefinitionID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					break;
				case 28:
					if (propertyName == "RetainDataTillForceCalculate")
					{
						if (!CompatibilityRestrictions.Partition_RetainDataTillForceCalculate.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							classification = UnexpectedPropertyClassification.IncompatibleProperty;
							return false;
						}
						this.body.RetainDataTillForceCalculate = reader.ReadBooleanProperty();
						return true;
					}
					break;
				}
			}
			classification = UnexpectedPropertyClassification.UnknownProperty;
			return false;
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x0004D1A4 File Offset: 0x0004B3A4
		[Obsolete("Deprecated. Use RequestRefresh method instead.", false)]
		public void Refresh(RefreshType type)
		{
			this.RequestRefresh(type);
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x0004D1AD File Offset: 0x0004B3AD
		[Obsolete("Deprecated. Use RequestRefresh method instead.", false)]
		public void Refresh(RefreshType type, ICollection<OverrideCollection> overrides)
		{
			this.RequestRefresh(type, overrides);
		}

		// Token: 0x060008E5 RID: 2277 RVA: 0x0004D1B7 File Offset: 0x0004B3B7
		public void RequestRefresh(RefreshType type)
		{
			ObjectChangeTracker.RegisterObjectForRefresh(this, type, false);
			this.body.MarkForRefresh(type, null);
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x0004D1CE File Offset: 0x0004B3CE
		public void RequestRefresh(RefreshType type, ICollection<OverrideCollection> overrides)
		{
			ObjectChangeTracker.RegisterObjectForRefresh(this, type, overrides != null);
			this.body.MarkForRefresh(type, overrides);
		}

		// Token: 0x060008E7 RID: 2279 RVA: 0x0004D1E8 File Offset: 0x0004B3E8
		internal void MarkForRefresh(RefreshType type, ICollection<OverrideCollection> overrides)
		{
			if (this.body.Savepoint != base.Model.TxManager.CurrentSavepoint)
			{
				this.CloneBody(base.Model.TxManager.CurrentSavepoint);
			}
			this.body.RefreshRequested = true;
			this.body.RequestedRefreshMask = (this.body.RequestedRefreshMask |= Utils.ConvertRefreshTypeToMask(type));
			this.body.Overrides = overrides;
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0004D267 File Offset: 0x0004B467
		[Obsolete("Deprecated. Use RequestRename method instead.", false)]
		public void Rename(string newName)
		{
			this.RequestRename(newName);
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0004D270 File Offset: 0x0004B470
		public void RequestRename(string newName)
		{
			ObjectChangeTracker.RegisterObjectRenaming(this);
			this.Name = newName;
			this.body.RenameRequestedThroughAPI = true;
			ObjectChangeTracker.RegisterObjectRenamed(this);
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0004D294 File Offset: 0x0004B494
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!string.IsNullOrEmpty(this.body.Name))
			{
				result["name", TomPropCategory.Name, 2, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Name, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.Description))
			{
				result["description", TomPropCategory.Regular, 3, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Description, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreInferredProperties && this.body.State != ObjectState.NoData)
			{
				if (!PropertyHelper.IsObjectStateValueCompatible(this.body.State, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member State is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["state", TomPropCategory.Regular, 6, true] = JsonPropertyHelper.ConvertEnumToJsonValue<ObjectState>(this.body.State);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.Mode != ModeType.Default)
			{
				if (!PropertyHelper.IsModeTypeValueCompatible(this.body.Mode, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member Mode is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["mode", TomPropCategory.Regular, 9, false] = JsonPropertyHelper.ConvertEnumToJsonValue<ModeType>(this.body.Mode);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.DataView != DataViewType.Default)
			{
				result["dataView", TomPropCategory.Regular, 10, false] = JsonPropertyHelper.ConvertEnumToJsonValue<DataViewType>(this.body.DataView);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["modifiedTime", TomPropCategory.Regular, 11, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.ModifiedTime);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.RefreshedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["refreshedTime", TomPropCategory.Regular, 12, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.RefreshedTime);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreInferredProperties && !string.IsNullOrEmpty(this.body.ErrorMessage))
			{
				result["errorMessage", TomPropCategory.Regular, 14, true] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.ErrorMessage, SplitMultilineOptions.None);
			}
			this.SerializeAdditionalDataToJsonObject(result, options, mode, dbCompatibilityLevel);
			if (!options.IncludeTranslatablePropertiesOnly && this.body.QueryGroupID.Object != null)
			{
				if (!CompatibilityRestrictions.Partition_QueryGroup.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member QueryGroupID is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				this.body.QueryGroupID.SerializeToJsonObject(true, "queryGroup", ObjectType.QueryGroup, result, 20, false);
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations && this.body.DataCoverageDefinitionID.Object != null && !options.IncludeTranslatablePropertiesOnly && (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(this.body.DataCoverageDefinitionID.Object)))
			{
				if (!CompatibilityRestrictions.Partition_DataCoverageDefinition.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member DataCoverageDefinitionID is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["dataCoverageDefinition", TomPropCategory.ChildLink, 23, false] = this.body.DataCoverageDefinitionID.Object.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject();
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations && !options.IncludeTranslatablePropertiesOnly)
			{
				IEnumerable<ExtendedProperty> enumerable;
				if (!options.IgnoreInferredObjects)
				{
					IEnumerable<ExtendedProperty> extendedProperties = this.ExtendedProperties;
					enumerable = extendedProperties;
				}
				else
				{
					enumerable = this.ExtendedProperties.Where((ExtendedProperty o) => !ObjectTreeHelper.IsInferredObject(o));
				}
				IEnumerable<ExtendedProperty> enumerable2 = enumerable;
				if (enumerable2.Any<ExtendedProperty>())
				{
					if (!CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
					{
						throw TomInternalException.Create("A child ExtendedProperty is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
					}
					object[] array = enumerable2.Select((ExtendedProperty obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
					object[] array2 = array;
					result["extendedProperties", TomPropCategory.ChildCollection, 39, false] = array2;
				}
			}
			if (!options.IgnoreChildren && !options.IncludeTranslatablePropertiesOnly && this.Annotations.Any<Annotation>())
			{
				object[] array = this.Annotations.Select((Annotation obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
				object[] array3 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array3;
			}
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x0004D864 File Offset: 0x0004BA64
		private void SerializeAdditionalDataToJsonObject(JsonObject jsonObj, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!options.IncludeTranslatablePropertiesOnly)
			{
				JsonObject jsonObject = new JsonObject();
				if (this.source != null)
				{
					this.source.SerializeToJsonObject(jsonObject, options, mode, dbCompatibilityLevel);
				}
				else
				{
					PartitionSource.SerializePartitionSourceTypeToJsonObject(jsonObject, PartitionSourceType.None);
				}
				jsonObj["source", TomPropCategory.ChildLink, 0, false] = jsonObject.ToDictObject();
			}
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0004D8B4 File Offset: 0x0004BAB4
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (name != null)
			{
				int length = name.Length;
				switch (length)
				{
				case 4:
				{
					char c = name[0];
					if (c != 'm')
					{
						if (c == 'n')
						{
							if (name == "name")
							{
								this.body.Name = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
								return true;
							}
						}
					}
					else if (name == "mode")
					{
						ModeType modeType = JsonPropertyHelper.ConvertJsonValueToEnum<ModeType>(jsonProp.Value);
						if (jsonProp.Value.Type != 10 && !PropertyHelper.IsModeTypeValueCompatible(modeType, mode, dbCompatibilityLevel))
						{
							return false;
						}
						this.body.Mode = modeType;
						return true;
					}
					break;
				}
				case 5:
					if (name == "state")
					{
						ObjectState objectState = JsonPropertyHelper.ConvertJsonValueToEnum<ObjectState>(jsonProp.Value);
						if (jsonProp.Value.Type != 10 && !PropertyHelper.IsObjectStateValueCompatible(objectState, mode, dbCompatibilityLevel))
						{
							return false;
						}
						this.body.State = objectState;
						return true;
					}
					break;
				case 6:
				case 7:
				case 9:
				case 14:
				case 15:
				case 16:
				case 17:
					break;
				case 8:
					if (name == "dataView")
					{
						this.body.DataView = JsonPropertyHelper.ConvertJsonValueToEnum<DataViewType>(jsonProp.Value);
						return true;
					}
					break;
				case 10:
					if (name == "queryGroup")
					{
						if (!CompatibilityRestrictions.Partition_QueryGroup.IsCompatible(mode, dbCompatibilityLevel))
						{
							return false;
						}
						this.body.QueryGroupID.Path = new ObjectPath(ObjectType.QueryGroup, JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value));
						return true;
					}
					break;
				case 11:
				{
					char c = name[0];
					if (c != 'a')
					{
						if (c == 'd')
						{
							if (name == "description")
							{
								this.body.Description = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
								return true;
							}
						}
					}
					else if (name == "annotations")
					{
						JsonPropertyHelper.ReadObjectCollection(this.Annotations, jsonProp.Value, options, mode, dbCompatibilityLevel);
						return true;
					}
					break;
				}
				case 12:
				{
					char c = name[0];
					if (c != 'e')
					{
						if (c == 'm')
						{
							if (name == "modifiedTime")
							{
								this.body.ModifiedTime = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
								return true;
							}
						}
					}
					else if (name == "errorMessage")
					{
						this.body.ErrorMessage = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
						return true;
					}
					break;
				}
				case 13:
					if (name == "refreshedTime")
					{
						this.body.RefreshedTime = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
						return true;
					}
					break;
				case 18:
					if (name == "extendedProperties")
					{
						if (!JsonPropertyHelper.IsEmptyObjectCollection(jsonProp.Value) && !CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
						{
							return false;
						}
						JsonPropertyHelper.ReadObjectCollection(this.ExtendedProperties, jsonProp.Value, options, mode, dbCompatibilityLevel);
						return true;
					}
					break;
				default:
					if (length == 22)
					{
						if (name == "dataCoverageDefinition")
						{
							if (jsonProp.Value.Type != 10)
							{
								if (!CompatibilityRestrictions.Partition_DataCoverageDefinition.IsCompatible(mode, dbCompatibilityLevel))
								{
									return false;
								}
								DataCoverageDefinition dataCoverageDefinition = new DataCoverageDefinition();
								dataCoverageDefinition.DeserializeFromJsonObject(jsonProp.Value, options, mode, dbCompatibilityLevel);
								this.body.DataCoverageDefinitionID.Object = dataCoverageDefinition;
							}
							return true;
						}
					}
					break;
				}
			}
			bool flag = false;
			this.ReadAdditionalPropertyFromJson(jsonProp, options, mode, dbCompatibilityLevel, ref flag);
			return flag;
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x0004DC54 File Offset: 0x0004BE54
		private void ReadAdditionalPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel, ref bool wasRead)
		{
			if (jsonProp.Name == "source")
			{
				if (jsonProp.Value.Type != 10)
				{
					jsonProp.Value.VerifyTokenType(1);
					JObject jobject = (JObject)jsonProp.Value;
					PartitionSourceType? partitionSourceType = null;
					JToken jtoken;
					if (jobject.TryGetValue("type", ref jtoken))
					{
						partitionSourceType = new PartitionSourceType?(JsonPropertyHelper.ConvertJsonValueToEnum<PartitionSourceType>(jtoken));
					}
					if (partitionSourceType == null)
					{
						partitionSourceType = new PartitionSourceType?(PartitionSourceType.Query);
					}
					try
					{
						PartitionSource partitionSource = PartitionSource.Create(partitionSourceType.Value);
						if (partitionSource != null)
						{
							this.Source = partitionSource;
							this.Source.DeserializeFromJsonObject(jobject, options, mode, dbCompatibilityLevel);
						}
					}
					catch (ArgumentException ex)
					{
						throw JsonSerializationUtil.CreateException(ex.Message, ex);
					}
				}
				wasRead = true;
				return;
			}
			wasRead = false;
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060008EE RID: 2286 RVA: 0x0004DD2C File Offset: 0x0004BF2C
		public PartitionSourceType SourceType
		{
			get
			{
				return this.body.Type;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x0004DD39 File Offset: 0x0004BF39
		// (set) Token: 0x060008F0 RID: 2288 RVA: 0x0004DD44 File Offset: 0x0004BF44
		public PartitionSource Source
		{
			get
			{
				return this.source;
			}
			set
			{
				if (value == this.source)
				{
					return;
				}
				PartitionSourceType partitionSourceType = ((this.source != null) ? this.source.Type : PartitionSourceType.None);
				PartitionSourceType partitionSourceType2 = ((value != null) ? value.Type : PartitionSourceType.None);
				if (!PropertyHelper.AreValuesIdentical(partitionSourceType, partitionSourceType2))
				{
					this.Type = partitionSourceType2;
				}
				else
				{
					ObjectChangeTracker.RegisterUpcomingPropertyChange(this);
				}
				if (this.source != null)
				{
					this.source.DetachFromPartition(true, true);
				}
				if (value != null)
				{
					if (value.Partition != null)
					{
						throw new InvalidOperationException(TomSR.Exception_PartitionSourceAlreadyAttached);
					}
					if (value.IsRemoved)
					{
						throw new InvalidOperationException(TomSR.Exception_PartitionSourceAlreadyRemoved);
					}
					value.AttachToPartition(this, true);
				}
				this.source = value;
			}
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x0004DDE2 File Offset: 0x0004BFE2
		internal override void OnAfterBodyReverted()
		{
			base.OnAfterBodyReverted();
			this.RecreatePartitionSourceIfMismatchWithPartitionType(true);
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x0004DDF4 File Offset: 0x0004BFF4
		private void RecreatePartitionSourceIfMismatchWithPartitionType(bool canResolveLinks)
		{
			PartitionSourceType partitionSourceType = ((this.source != null) ? this.source.Type : PartitionSourceType.None);
			PartitionSourceType type = this.Type;
			if (partitionSourceType != type)
			{
				PartitionSource partitionSource = null;
				if (type != PartitionSourceType.None)
				{
					partitionSource = PartitionSource.Create(type);
					partitionSource.LoadDataFromPartition(this, canResolveLinks, false);
				}
				if (this.source != null)
				{
					this.source.DetachFromPartition(canResolveLinks, false);
				}
				if (partitionSource != null)
				{
					partitionSource.AttachToPartition(this, false);
					this.source = partitionSource;
				}
			}
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x0004DE5E File Offset: 0x0004C05E
		internal override IEnumerable<CustomizedPropertyName> GetCustomizedPropertyNames()
		{
			yield return new CustomizedPropertyName("Type", "SourceType");
			if (this.source != null)
			{
				foreach (CustomizedPropertyName customizedPropertyName in this.source.GetCustomizedPropertyNames())
				{
					yield return customizedPropertyName;
				}
				IEnumerator<CustomizedPropertyName> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x0004DE6E File Offset: 0x0004C06E
		public void RequestMerge(IEnumerable<Partition> sources)
		{
			if (sources == null)
			{
				throw new ArgumentNullException("sources");
			}
			this.RequestMergeImpl(sources.ToList<Partition>());
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x0004DE8A File Offset: 0x0004C08A
		internal void RequestMergeImpl(IEnumerable<Partition> sources)
		{
			ObjectChangeTracker.RegisterPartitionsMerging(this, sources);
			this.body.MergePartitionSources = sources;
			ObjectChangeTracker.RegisterPartitionsMerged(this);
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x0004DEA5 File Offset: 0x0004C0A5
		public void RequestRefreshPolicyImpact()
		{
			ObjectChangeTracker.RegisterPartitionForAnalyzeRefreshPolicyImpact(this);
			this.body.AnalyzeRefreshPolicyImpactRequested = true;
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x0004DEB9 File Offset: 0x0004C0B9
		internal override string GetFormattedObjectPath()
		{
			if (this.Table != null)
			{
				return TomSR.ObjectPath_Partition_2Args(this.Name, this.Table.Name);
			}
			return TomSR.ObjectPath_Partition_1Arg(this.Name);
		}

		// Token: 0x04000148 RID: 328
		internal Partition.ObjectBody body;

		// Token: 0x04000149 RID: 329
		private PartitionAnnotationCollection _Annotations;

		// Token: 0x0400014A RID: 330
		private PartitionExtendedPropertyCollection _ExtendedProperties;

		// Token: 0x0400014B RID: 331
		private PartitionSource source;

		// Token: 0x0200029D RID: 669
		internal class ObjectBody : RefreshablePartitionBody<Partition>
		{
			// Token: 0x060021CC RID: 8652 RVA: 0x000DACE8 File Offset: 0x000D8EE8
			public ObjectBody(Partition owner)
				: base(owner)
			{
				this.ModifiedTime = DateTime.MinValue;
				this.RefreshedTime = DateTime.MinValue;
				this.RangeStart = DateTime.MinValue;
				this.RangeEnd = DateTime.MinValue;
				this.TableID = new ParentLink<Partition, Table>(owner, "Table");
				this.DataSourceID = new CrossLink<Partition, DataSource>(owner, "DataSource");
				this.QueryGroupID = new CrossLink<Partition, QueryGroup>(owner, "QueryGroup");
				this.ExpressionSourceID = new CrossLink<Partition, NamedExpression>(owner, "ExpressionSource");
				this.DataCoverageDefinitionID = new ChildLink<Partition, DataCoverageDefinition>(owner, "DataCoverageDefinition");
			}

			// Token: 0x060021CD RID: 8653 RVA: 0x000DAD7D File Offset: 0x000D8F7D
			public override string GetObjectName()
			{
				return this.Name;
			}

			// Token: 0x060021CE RID: 8654 RVA: 0x000DAD88 File Offset: 0x000D8F88
			internal bool IsEqualTo(Partition.ObjectBody other, CopyContext context)
			{
				return PropertyHelper.AreValuesIdentical(this.Name, other.Name) && PropertyHelper.AreValuesIdentical(this.Description, other.Description) && PropertyHelper.AreValuesIdentical(this.QueryDefinition, other.QueryDefinition) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.State, other.State)) && PropertyHelper.AreValuesIdentical(this.Type, other.Type) && PropertyHelper.AreValuesIdentical(this.Mode, other.Mode) && PropertyHelper.AreValuesIdentical(this.DataView, other.DataView) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.RefreshedTime, other.RefreshedTime)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ErrorMessage, other.ErrorMessage)) && PropertyHelper.AreValuesIdentical(this.RetainDataTillForceCalculate, other.RetainDataTillForceCalculate) && PropertyHelper.AreValuesIdentical(this.RangeStart, other.RangeStart) && PropertyHelper.AreValuesIdentical(this.RangeEnd, other.RangeEnd) && PropertyHelper.AreValuesIdentical(this.RangeGranularity, other.RangeGranularity) && PropertyHelper.AreValuesIdentical(this.RefreshBookmark, other.RefreshBookmark) && PropertyHelper.AreValuesIdentical(this.MAttributes, other.MAttributes) && PropertyHelper.AreValuesIdentical(this.SchemaName, other.SchemaName) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.TableID.IsEqualTo(other.TableID, context)) && this.DataSourceID.IsEqualTo(other.DataSourceID, context) && this.QueryGroupID.IsEqualTo(other.QueryGroupID, context) && this.ExpressionSourceID.IsEqualTo(other.ExpressionSourceID, context) && this.DataCoverageDefinitionID.IsEqualTo(other.DataCoverageDefinitionID, context);
			}

			// Token: 0x060021CF RID: 8655 RVA: 0x000DAFC8 File Offset: 0x000D91C8
			internal void CopyFromImpl(Partition.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
				base.Owner.Name = other.Name;
				this.Description = other.Description;
				this.QueryDefinition = other.QueryDefinition;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.State = other.State;
				}
				this.Type = other.Type;
				this.Mode = other.Mode;
				this.DataView = other.DataView;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ModifiedTime = other.ModifiedTime;
				}
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.RefreshedTime = other.RefreshedTime;
				}
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ErrorMessage = other.ErrorMessage;
				}
				this.RetainDataTillForceCalculate = other.RetainDataTillForceCalculate;
				this.RangeStart = other.RangeStart;
				this.RangeEnd = other.RangeEnd;
				this.RangeGranularity = other.RangeGranularity;
				this.RefreshBookmark = other.RefreshBookmark;
				this.MAttributes = other.MAttributes;
				this.SchemaName = other.SchemaName;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.TableID.CopyFrom(other.TableID, context);
				}
				this.DataSourceID.CopyFrom(other.DataSourceID, context);
				this.QueryGroupID.CopyFrom(other.QueryGroupID, context);
				this.ExpressionSourceID.CopyFrom(other.ExpressionSourceID, context);
				this.DataCoverageDefinitionID.CopyFrom(other.DataCoverageDefinitionID, context);
			}

			// Token: 0x060021D0 RID: 8656 RVA: 0x000DB174 File Offset: 0x000D9374
			internal void CopyFromImpl(Partition.ObjectBody other)
			{
				this.Name = other.Name;
				this.Description = other.Description;
				this.QueryDefinition = other.QueryDefinition;
				this.State = other.State;
				this.Type = other.Type;
				this.Mode = other.Mode;
				this.DataView = other.DataView;
				this.ModifiedTime = other.ModifiedTime;
				this.RefreshedTime = other.RefreshedTime;
				this.ErrorMessage = other.ErrorMessage;
				this.RetainDataTillForceCalculate = other.RetainDataTillForceCalculate;
				this.RangeStart = other.RangeStart;
				this.RangeEnd = other.RangeEnd;
				this.RangeGranularity = other.RangeGranularity;
				this.RefreshBookmark = other.RefreshBookmark;
				this.MAttributes = other.MAttributes;
				this.SchemaName = other.SchemaName;
				this.TableID.CopyFrom(other.TableID, ObjectChangeTracker.BodyCloneContext);
				this.DataSourceID.CopyFrom(other.DataSourceID, ObjectChangeTracker.BodyCloneContext);
				this.QueryGroupID.CopyFrom(other.QueryGroupID, ObjectChangeTracker.BodyCloneContext);
				this.ExpressionSourceID.CopyFrom(other.ExpressionSourceID, ObjectChangeTracker.BodyCloneContext);
				this.DataCoverageDefinitionID.CopyFrom(other.DataCoverageDefinitionID, ObjectChangeTracker.BodyCloneContext);
			}

			// Token: 0x060021D1 RID: 8657 RVA: 0x000DB2BB File Offset: 0x000D94BB
			public override void CopyFrom(MetadataObjectBody<Partition> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((Partition.ObjectBody)other, context);
			}

			// Token: 0x060021D2 RID: 8658 RVA: 0x000DB2D4 File Offset: 0x000D94D4
			internal bool IsEqualTo(Partition.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.Name, other.Name) && PropertyHelper.AreValuesIdentical(this.Description, other.Description) && PropertyHelper.AreValuesIdentical(this.QueryDefinition, other.QueryDefinition) && PropertyHelper.AreValuesIdentical(this.State, other.State) && PropertyHelper.AreValuesIdentical(this.Type, other.Type) && PropertyHelper.AreValuesIdentical(this.Mode, other.Mode) && PropertyHelper.AreValuesIdentical(this.DataView, other.DataView) && PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime) && PropertyHelper.AreValuesIdentical(this.RefreshedTime, other.RefreshedTime) && PropertyHelper.AreValuesIdentical(this.ErrorMessage, other.ErrorMessage) && PropertyHelper.AreValuesIdentical(this.RetainDataTillForceCalculate, other.RetainDataTillForceCalculate) && PropertyHelper.AreValuesIdentical(this.RangeStart, other.RangeStart) && PropertyHelper.AreValuesIdentical(this.RangeEnd, other.RangeEnd) && PropertyHelper.AreValuesIdentical(this.RangeGranularity, other.RangeGranularity) && PropertyHelper.AreValuesIdentical(this.RefreshBookmark, other.RefreshBookmark) && PropertyHelper.AreValuesIdentical(this.MAttributes, other.MAttributes) && PropertyHelper.AreValuesIdentical(this.SchemaName, other.SchemaName) && this.TableID.IsEqualTo(other.TableID) && this.DataSourceID.IsEqualTo(other.DataSourceID) && this.QueryGroupID.IsEqualTo(other.QueryGroupID) && this.ExpressionSourceID.IsEqualTo(other.ExpressionSourceID) && this.DataCoverageDefinitionID.IsEqualTo(other.DataCoverageDefinitionID);
			}

			// Token: 0x060021D3 RID: 8659 RVA: 0x000DB4B0 File Offset: 0x000D96B0
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((Partition.ObjectBody)other);
			}

			// Token: 0x060021D4 RID: 8660 RVA: 0x000DB4CC File Offset: 0x000D96CC
			internal void CompareWith(Partition.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.Name, other.Name))
				{
					context.RegisterPropertyChange(base.Owner, "Name", typeof(string), base.RenameRequestedThroughAPI ? PropertyFlags.User : PropertyFlags.DdlAndUser, other.Name, this.Name);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Description, other.Description))
				{
					context.RegisterPropertyChange(base.Owner, "Description", typeof(string), PropertyFlags.DdlAndUser, other.Description, this.Description);
				}
				if (!PropertyHelper.AreValuesIdentical(this.State, other.State))
				{
					context.RegisterPropertyChange(base.Owner, "State", typeof(ObjectState), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.State, this.State);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Type, other.Type))
				{
					string text = "Type";
					string customizedPropertyName = base.Owner.GetCustomizedPropertyName("Type");
					if (text == customizedPropertyName)
					{
						context.RegisterPropertyChange(base.Owner, text, typeof(PartitionSourceType), PropertyFlags.DdlAndUser, other.Type, this.Type);
					}
					else
					{
						context.RegisterPropertyChange(base.Owner, customizedPropertyName, typeof(PartitionSourceType), PropertyFlags.User, other.Type, this.Type);
						context.RegisterPropertyChange(base.Owner, text, typeof(PartitionSourceType), PropertyFlags.Ddl, other.Type, this.Type);
					}
				}
				if (!PropertyHelper.AreValuesIdentical(this.Mode, other.Mode))
				{
					context.RegisterPropertyChange(base.Owner, "Mode", typeof(ModeType), PropertyFlags.DdlAndUser, other.Mode, this.Mode);
				}
				if (!PropertyHelper.AreValuesIdentical(this.DataView, other.DataView))
				{
					context.RegisterPropertyChange(base.Owner, "DataView", typeof(DataViewType), PropertyFlags.DdlAndUser, other.DataView, this.DataView);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "ModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ModifiedTime, this.ModifiedTime);
				}
				if (!PropertyHelper.AreValuesIdentical(this.RefreshedTime, other.RefreshedTime))
				{
					context.RegisterPropertyChange(base.Owner, "RefreshedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.RefreshedTime, this.RefreshedTime);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ErrorMessage, other.ErrorMessage))
				{
					context.RegisterPropertyChange(base.Owner, "ErrorMessage", typeof(string), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ErrorMessage, this.ErrorMessage);
				}
				if (!PropertyHelper.AreValuesIdentical(this.RetainDataTillForceCalculate, other.RetainDataTillForceCalculate))
				{
					string text2 = "RetainDataTillForceCalculate";
					string customizedPropertyName2 = base.Owner.GetCustomizedPropertyName("RetainDataTillForceCalculate");
					if (text2 == customizedPropertyName2)
					{
						context.RegisterPropertyChange(base.Owner, text2, typeof(bool), PropertyFlags.DdlAndUser, other.RetainDataTillForceCalculate, this.RetainDataTillForceCalculate);
					}
					else
					{
						context.RegisterPropertyChange(base.Owner, customizedPropertyName2, typeof(bool), PropertyFlags.User, other.RetainDataTillForceCalculate, this.RetainDataTillForceCalculate);
						context.RegisterPropertyChange(base.Owner, text2, typeof(bool), PropertyFlags.Ddl, other.RetainDataTillForceCalculate, this.RetainDataTillForceCalculate);
					}
				}
				if (!PropertyHelper.AreValuesIdentical(this.RangeStart, other.RangeStart))
				{
					string text3 = "RangeStart";
					string customizedPropertyName3 = base.Owner.GetCustomizedPropertyName("RangeStart");
					if (text3 == customizedPropertyName3)
					{
						context.RegisterPropertyChange(base.Owner, text3, typeof(DateTime), PropertyFlags.DdlAndUser, other.RangeStart, this.RangeStart);
					}
					else
					{
						context.RegisterPropertyChange(base.Owner, customizedPropertyName3, typeof(DateTime), PropertyFlags.User, other.RangeStart, this.RangeStart);
						context.RegisterPropertyChange(base.Owner, text3, typeof(DateTime), PropertyFlags.Ddl, other.RangeStart, this.RangeStart);
					}
				}
				if (!PropertyHelper.AreValuesIdentical(this.RangeEnd, other.RangeEnd))
				{
					string text4 = "RangeEnd";
					string customizedPropertyName4 = base.Owner.GetCustomizedPropertyName("RangeEnd");
					if (text4 == customizedPropertyName4)
					{
						context.RegisterPropertyChange(base.Owner, text4, typeof(DateTime), PropertyFlags.DdlAndUser, other.RangeEnd, this.RangeEnd);
					}
					else
					{
						context.RegisterPropertyChange(base.Owner, customizedPropertyName4, typeof(DateTime), PropertyFlags.User, other.RangeEnd, this.RangeEnd);
						context.RegisterPropertyChange(base.Owner, text4, typeof(DateTime), PropertyFlags.Ddl, other.RangeEnd, this.RangeEnd);
					}
				}
				if (!PropertyHelper.AreValuesIdentical(this.RangeGranularity, other.RangeGranularity))
				{
					string text5 = "RangeGranularity";
					string customizedPropertyName5 = base.Owner.GetCustomizedPropertyName("RangeGranularity");
					if (text5 == customizedPropertyName5)
					{
						context.RegisterPropertyChange(base.Owner, text5, typeof(RefreshGranularityType), PropertyFlags.DdlAndUser, other.RangeGranularity, this.RangeGranularity);
					}
					else
					{
						context.RegisterPropertyChange(base.Owner, customizedPropertyName5, typeof(RefreshGranularityType), PropertyFlags.User, other.RangeGranularity, this.RangeGranularity);
						context.RegisterPropertyChange(base.Owner, text5, typeof(RefreshGranularityType), PropertyFlags.Ddl, other.RangeGranularity, this.RangeGranularity);
					}
				}
				if (!PropertyHelper.AreValuesIdentical(this.RefreshBookmark, other.RefreshBookmark))
				{
					string text6 = "RefreshBookmark";
					string customizedPropertyName6 = base.Owner.GetCustomizedPropertyName("RefreshBookmark");
					if (text6 == customizedPropertyName6)
					{
						context.RegisterPropertyChange(base.Owner, text6, typeof(string), PropertyFlags.DdlAndUser, other.RefreshBookmark, this.RefreshBookmark);
					}
					else
					{
						context.RegisterPropertyChange(base.Owner, customizedPropertyName6, typeof(string), PropertyFlags.User, other.RefreshBookmark, this.RefreshBookmark);
						context.RegisterPropertyChange(base.Owner, text6, typeof(string), PropertyFlags.Ddl, other.RefreshBookmark, this.RefreshBookmark);
					}
				}
				if (!PropertyHelper.AreValuesIdentical(this.MAttributes, other.MAttributes))
				{
					string text7 = "MAttributes";
					string customizedPropertyName7 = base.Owner.GetCustomizedPropertyName("MAttributes");
					if (text7 == customizedPropertyName7)
					{
						context.RegisterPropertyChange(base.Owner, text7, typeof(string), PropertyFlags.DdlAndUser, other.MAttributes, this.MAttributes);
					}
					else
					{
						context.RegisterPropertyChange(base.Owner, customizedPropertyName7, typeof(string), PropertyFlags.User, other.MAttributes, this.MAttributes);
						context.RegisterPropertyChange(base.Owner, text7, typeof(string), PropertyFlags.Ddl, other.MAttributes, this.MAttributes);
					}
				}
				if (!PropertyHelper.AreValuesIdentical(this.SchemaName, other.SchemaName))
				{
					string text8 = "SchemaName";
					string customizedPropertyName8 = base.Owner.GetCustomizedPropertyName("SchemaName");
					if (text8 == customizedPropertyName8)
					{
						context.RegisterPropertyChange(base.Owner, text8, typeof(string), PropertyFlags.DdlAndUser, other.SchemaName, this.SchemaName);
					}
					else
					{
						context.RegisterPropertyChange(base.Owner, customizedPropertyName8, typeof(string), PropertyFlags.User, other.SchemaName, this.SchemaName);
						context.RegisterPropertyChange(base.Owner, text8, typeof(string), PropertyFlags.Ddl, other.SchemaName, this.SchemaName);
					}
				}
				this.TableID.CompareWith(other.TableID, "TableID", "Table", PropertyFlags.ReadOnly, context);
				this.QueryGroupID.CompareWith(other.QueryGroupID, "QueryGroupID", "QueryGroup", PropertyFlags.None, context);
				this.DataCoverageDefinitionID.CompareWith(other.DataCoverageDefinitionID, "DataCoverageDefinitionID", "DataCoverageDefinition", PropertyFlags.None, context);
				this.ComparePropertiesWithCustomLogic(other, context);
			}

			// Token: 0x060021D5 RID: 8661 RVA: 0x000DBD2D File Offset: 0x000D9F2D
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((Partition.ObjectBody)other, context);
			}

			// Token: 0x060021D6 RID: 8662 RVA: 0x000DBD44 File Offset: 0x000D9F44
			private void ComparePropertiesWithCustomLogic(Partition.ObjectBody other, CompareContext context)
			{
				bool flag = this.Type != other.Type;
				string text;
				string text2;
				if (!PropertyHelper.AreValuesIdentical(this.QueryDefinition, other.QueryDefinition))
				{
					text = "QueryDefinition";
					text2 = base.Owner.GetCustomizedPropertyName("QueryDefinition");
					if (text == text2)
					{
						context.RegisterPropertyChange(base.Owner, text, typeof(string), flag ? PropertyFlags.Ddl : PropertyFlags.DdlAndUser, other.QueryDefinition, this.QueryDefinition);
					}
					else
					{
						if (!flag)
						{
							context.RegisterPropertyChange(base.Owner, text2, typeof(string), PropertyFlags.User, other.QueryDefinition, this.QueryDefinition);
						}
						context.RegisterPropertyChange(base.Owner, text, typeof(string), PropertyFlags.Ddl, other.QueryDefinition, this.QueryDefinition);
					}
				}
				text = "DataSourceID";
				text2 = base.Owner.GetCustomizedPropertyName("DataSourceID");
				this.DataSourceID.CompareWith(other.DataSourceID, text, flag ? null : text2, PropertyFlags.None, context);
				text = "ExpressionSourceID";
				text2 = base.Owner.GetCustomizedPropertyName("ExpressionSourceID");
				this.ExpressionSourceID.CompareWith(other.ExpressionSourceID, text, flag ? null : text2, PropertyFlags.None, context);
			}

			// Token: 0x04000967 RID: 2407
			internal string Name;

			// Token: 0x04000968 RID: 2408
			internal string Description;

			// Token: 0x04000969 RID: 2409
			internal string QueryDefinition;

			// Token: 0x0400096A RID: 2410
			internal ObjectState State;

			// Token: 0x0400096B RID: 2411
			internal PartitionSourceType Type;

			// Token: 0x0400096C RID: 2412
			internal ModeType Mode;

			// Token: 0x0400096D RID: 2413
			internal DataViewType DataView;

			// Token: 0x0400096E RID: 2414
			internal DateTime ModifiedTime;

			// Token: 0x0400096F RID: 2415
			internal DateTime RefreshedTime;

			// Token: 0x04000970 RID: 2416
			internal string ErrorMessage;

			// Token: 0x04000971 RID: 2417
			internal bool RetainDataTillForceCalculate;

			// Token: 0x04000972 RID: 2418
			internal DateTime RangeStart;

			// Token: 0x04000973 RID: 2419
			internal DateTime RangeEnd;

			// Token: 0x04000974 RID: 2420
			internal RefreshGranularityType RangeGranularity;

			// Token: 0x04000975 RID: 2421
			internal string RefreshBookmark;

			// Token: 0x04000976 RID: 2422
			internal string MAttributes;

			// Token: 0x04000977 RID: 2423
			internal string SchemaName;

			// Token: 0x04000978 RID: 2424
			internal ParentLink<Partition, Table> TableID;

			// Token: 0x04000979 RID: 2425
			internal CrossLink<Partition, DataSource> DataSourceID;

			// Token: 0x0400097A RID: 2426
			internal CrossLink<Partition, QueryGroup> QueryGroupID;

			// Token: 0x0400097B RID: 2427
			internal CrossLink<Partition, NamedExpression> ExpressionSourceID;

			// Token: 0x0400097C RID: 2428
			internal ChildLink<Partition, DataCoverageDefinition> DataCoverageDefinitionID;
		}
	}
}
