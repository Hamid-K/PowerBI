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
	// Token: 0x02000086 RID: 134
	[CompatibilityRequirement("1400")]
	public sealed class NamedExpression : NamedMetadataObject, IMetadataObjectWithOverrides, IMetadataObjectWithLineage
	{
		// Token: 0x060007E6 RID: 2022 RVA: 0x00042FAF File Offset: 0x000411AF
		public NamedExpression()
		{
			this.InitBodyAndCollections(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00042FC2 File Offset: 0x000411C2
		internal NamedExpression(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections(comparer);
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00042FD4 File Offset: 0x000411D4
		private void InitBodyAndCollections(IEqualityComparer<string> comparer)
		{
			this.body = new NamedExpression.ObjectBody(this);
			this.body.Name = string.Empty;
			this.body.Description = string.Empty;
			this.body.Kind = (ExpressionKind)(-1);
			this.body.Expression = string.Empty;
			this.body.MAttributes = string.Empty;
			this.body.LineageTag = string.Empty;
			this.body.SourceLineageTag = string.Empty;
			this.body.RemoteParameterName = string.Empty;
			this._Annotations = new NamedExpressionAnnotationCollection(this, comparer);
			this._ExtendedProperties = new NamedExpressionExtendedPropertyCollection(this, comparer);
			this._ExcludedArtifacts = new NamedExpressionExcludedArtifactCollection(this);
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060007E9 RID: 2025 RVA: 0x0004308F File Offset: 0x0004128F
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.Expression;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x00043093 File Offset: 0x00041293
		// (set) Token: 0x060007EB RID: 2027 RVA: 0x000430A5 File Offset: 0x000412A5
		public override MetadataObject Parent
		{
			get
			{
				return this.body.ModelID.Object;
			}
			internal set
			{
				if (this.body.ModelID.Object != value)
				{
					MetadataObject.UpdateMetadataObjectParent<NamedExpression, Model>(this.body.ModelID, (Model)value, null, null);
				}
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x000430D2 File Offset: 0x000412D2
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.ModelID.ObjectID;
			}
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x000430E4 File Offset: 0x000412E4
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.Expression, null, "NamedExpression object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
				{
					writer.WriteProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, typeof(string));
				}
				if (writer.ShouldIncludeProperty("expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty))
				{
					writer.WriteProperty("expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty, typeof(string));
				}
				if (writer.ShouldIncludeProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
				{
					writer.WriteProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, typeof(string));
				}
				if (writer.ShouldIncludeProperty("kind", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<ExpressionKind>("kind", MetadataPropertyNature.RegularProperty, null);
				}
				if (writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
				{
					writer.WriteProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
				}
				if (CompatibilityRestrictions.NamedExpression_MAttributes.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("mAttributes", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("mAttributes", MetadataPropertyNature.RegularProperty, typeof(string));
				}
				if (CompatibilityRestrictions.NamedExpression_LineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("lineageTag", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("lineageTag", MetadataPropertyNature.RegularProperty, typeof(string));
				}
				if (CompatibilityRestrictions.NamedExpression_SourceLineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("sourceLineageTag", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("sourceLineageTag", MetadataPropertyNature.RegularProperty, typeof(string));
				}
				if (CompatibilityRestrictions.NamedExpression_RemoteParameterName.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("remoteParameterName", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("remoteParameterName", MetadataPropertyNature.RegularProperty, typeof(string));
				}
				if (CompatibilityRestrictions.NamedExpression_QueryGroup.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("queryGroup", MetadataPropertyNature.CrossLinkProperty))
				{
					CrossLink<NamedExpression, QueryGroup>.WriteMetadataSchema(ObjectType.QueryGroup, ObjectType.QueryGroup, true, "queryGroup", false, writer);
				}
				if (CompatibilityRestrictions.NamedExpression_ParameterValuesColumn.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("parameterValuesColumn", MetadataPropertyNature.CrossLinkProperty))
				{
					CrossLink<NamedExpression, Column>.WriteMetadataSchema(ObjectType.Column, ObjectType.Table, true, "parameterValuesColumn", false, writer);
				}
				if (CompatibilityRestrictions.NamedExpression_ExpressionSource.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("expressionSource", MetadataPropertyNature.CrossLinkProperty))
				{
					CrossLink<NamedExpression, NamedExpression>.WriteMetadataSchema(ObjectType.Expression, ObjectType.Expression, true, "expressionSource", false, writer);
				}
				if (CompatibilityRestrictions.ExtendedProperty.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("extendedProperties", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "extendedProperties", MetadataPropertyNature.ChildCollection, ObjectType.ExtendedProperty);
				}
				if (CompatibilityRestrictions.ExcludedArtifact.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("excludedArtifacts", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "excludedArtifacts", MetadataPropertyNature.ChildCollection, ObjectType.ExcludedArtifact);
				}
				if (writer.ShouldIncludeProperty("annotations", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "annotations", MetadataPropertyNature.ChildCollection, ObjectType.Annotation);
				}
			}
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00043414 File Offset: 0x00041614
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			requiredLevel = CompatibilityRestrictions.NamedExpression[mode];
			requestingPath = ((!CompatibilityRestrictionSet.IsUnbound(requiredLevel)) ? string.Format("[{0}]", this.GetFormattedObjectPath()) : string.Empty);
			if (requiredLevel == -2)
			{
				return;
			}
			if (this.body.Kind != (ExpressionKind)(-1))
			{
				int num = PropertyHelper.GetExpressionKindCompatibilityRestrictions(this.body.Kind)[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "Kind");
					requiredLevel = num;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (this.body.QueryGroupID.Object != null)
			{
				int num2 = CompatibilityRestrictions.NamedExpression_QueryGroup[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num2, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "QueryGroupID");
					requiredLevel = num2;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (this.body.ParameterValuesColumnID.Object != null)
			{
				int num3 = CompatibilityRestrictions.NamedExpression_ParameterValuesColumn[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num3, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "ParameterValuesColumnID");
					requiredLevel = num3;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (!string.IsNullOrEmpty(this.body.MAttributes))
			{
				int num4 = CompatibilityRestrictions.NamedExpression_MAttributes[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num4, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "MAttributes");
					requiredLevel = num4;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (!string.IsNullOrEmpty(this.body.LineageTag))
			{
				int num5 = CompatibilityRestrictions.NamedExpression_LineageTag[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num5, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "LineageTag");
					requiredLevel = num5;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (!string.IsNullOrEmpty(this.body.SourceLineageTag))
			{
				int num6 = CompatibilityRestrictions.NamedExpression_SourceLineageTag[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num6, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "SourceLineageTag");
					requiredLevel = num6;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (!string.IsNullOrEmpty(this.body.RemoteParameterName))
			{
				int num7 = CompatibilityRestrictions.NamedExpression_RemoteParameterName[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num7, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "RemoteParameterName");
					requiredLevel = num7;
					if (requiredLevel == -2)
					{
						return;
					}
				}
			}
			if (this.body.ExpressionSourceID.Object != null)
			{
				int num8 = CompatibilityRestrictions.NamedExpression_ExpressionSource[mode];
				if (CompatibilityRestrictionSet.CompareLevel(num8, requiredLevel) == RestrictionsComapreResult.MoreRestrictive)
				{
					requestingPath = string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "ExpressionSourceID");
					requiredLevel = num8;
					int num9 = requiredLevel;
					return;
				}
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060007EF RID: 2031 RVA: 0x000436BA File Offset: 0x000418BA
		// (set) Token: 0x060007F0 RID: 2032 RVA: 0x000436C2 File Offset: 0x000418C2
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (NamedExpression.ObjectBody)value;
			}
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x000436D0 File Offset: 0x000418D0
		internal override ITxObjectBody CreateBody()
		{
			return new NamedExpression.ObjectBody(this);
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x000436D8 File Offset: 0x000418D8
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new NamedExpression();
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x000436DF File Offset: 0x000418DF
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return ((Model)parent).Expressions;
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x000436EC File Offset: 0x000418EC
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			Model model = MetadataObject.ResolveMetadataObjectParentById<NamedExpression, Model>(this.body.ModelID, objectMap, throwIfCantResolve, null, null);
			KeyValuePair<CompatibilityMode, Stack<string>>[] array = ((!this.body.QueryGroupID.IsNull) ? base.ValidateCompatibilityRequirement(CompatibilityRestrictions.NamedExpression_QueryGroup, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "QueryGroup")) : null);
			if (this.body.QueryGroupID.ResolveById(objectMap, throwIfCantResolve))
			{
				base.SetCompatibilityRequirement(CompatibilityRestrictions.NamedExpression_QueryGroup, array);
			}
			array = ((!this.body.ParameterValuesColumnID.IsNull) ? base.ValidateCompatibilityRequirement(CompatibilityRestrictions.NamedExpression_ParameterValuesColumn, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "ParameterValuesColumn")) : null);
			if (this.body.ParameterValuesColumnID.ResolveById(objectMap, throwIfCantResolve))
			{
				base.SetCompatibilityRequirement(CompatibilityRestrictions.NamedExpression_ParameterValuesColumn, array);
			}
			array = ((!this.body.ExpressionSourceID.IsNull) ? base.ValidateCompatibilityRequirement(CompatibilityRestrictions.NamedExpression_ExpressionSource, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "ExpressionSource")) : null);
			if (this.body.ExpressionSourceID.ResolveById(objectMap, throwIfCantResolve))
			{
				base.SetCompatibilityRequirement(CompatibilityRestrictions.NamedExpression_ExpressionSource, array);
			}
			if (model != null)
			{
				model.Expressions.Add(this);
			}
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00043820 File Offset: 0x00041A20
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			KeyValuePair<CompatibilityMode, Stack<string>>[] array = ((!this.body.QueryGroupID.IsNull) ? base.ValidateCompatibilityRequirement(CompatibilityRestrictions.NamedExpression_QueryGroup, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "QueryGroup")) : null);
			if (this.body.QueryGroupID.ResolveById(objectMap, throwIfCantResolve))
			{
				base.SetCompatibilityRequirement(CompatibilityRestrictions.NamedExpression_QueryGroup, array);
			}
			array = ((!this.body.ParameterValuesColumnID.IsNull) ? base.ValidateCompatibilityRequirement(CompatibilityRestrictions.NamedExpression_ParameterValuesColumn, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "ParameterValuesColumn")) : null);
			if (this.body.ParameterValuesColumnID.ResolveById(objectMap, throwIfCantResolve))
			{
				base.SetCompatibilityRequirement(CompatibilityRestrictions.NamedExpression_ParameterValuesColumn, array);
			}
			array = ((!this.body.ExpressionSourceID.IsNull) ? base.ValidateCompatibilityRequirement(CompatibilityRestrictions.NamedExpression_ExpressionSource, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "ExpressionSource")) : null);
			if (this.body.ExpressionSourceID.ResolveById(objectMap, throwIfCantResolve))
			{
				base.SetCompatibilityRequirement(CompatibilityRestrictions.NamedExpression_ExpressionSource, array);
			}
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00043930 File Offset: 0x00041B30
		internal override bool TryResolveCrossLinksByPath(ICollection<string> linksFailedToResolve)
		{
			bool flag = true;
			if (!this.body.QueryGroupID.IsResolved)
			{
				KeyValuePair<CompatibilityMode, Stack<string>>[] array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.NamedExpression_QueryGroup, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "QueryGroup"));
				if (this.body.QueryGroupID.TryResolveByPath())
				{
					base.SetCompatibilityRequirement(CompatibilityRestrictions.NamedExpression_QueryGroup, array);
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
			if (!this.body.ParameterValuesColumnID.IsResolved)
			{
				KeyValuePair<CompatibilityMode, Stack<string>>[] array2 = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.NamedExpression_ParameterValuesColumn, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "ParameterValuesColumn"));
				if (this.body.ParameterValuesColumnID.TryResolveByPath())
				{
					base.SetCompatibilityRequirement(CompatibilityRestrictions.NamedExpression_ParameterValuesColumn, array2);
				}
				else
				{
					if (linksFailedToResolve != null)
					{
						linksFailedToResolve.Add(string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "ParameterValuesColumn"));
					}
					flag = false;
				}
			}
			if (!this.body.ExpressionSourceID.IsResolved)
			{
				KeyValuePair<CompatibilityMode, Stack<string>>[] array3 = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.NamedExpression_ExpressionSource, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "ExpressionSource"));
				if (this.body.ExpressionSourceID.TryResolveByPath())
				{
					base.SetCompatibilityRequirement(CompatibilityRestrictions.NamedExpression_ExpressionSource, array3);
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

		// Token: 0x060007F7 RID: 2039 RVA: 0x00043A99 File Offset: 0x00041C99
		internal override void TryResolveCrossLinksAfterCopy(CopyContext copyContext)
		{
			this.body.QueryGroupID.TryResolveAfterCopy(copyContext);
			this.body.ParameterValuesColumnID.TryResolveAfterCopy(copyContext);
			this.body.ExpressionSourceID.TryResolveAfterCopy(copyContext);
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x00043AD1 File Offset: 0x00041CD1
		internal override void ValidateObjectImpl(ValidationResult result, bool throwOnError)
		{
			this.body.QueryGroupID.Validate(result, throwOnError);
			this.body.ParameterValuesColumnID.Validate(result, throwOnError);
			this.body.ExpressionSourceID.Validate(result, throwOnError);
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00043B0C File Offset: 0x00041D0C
		internal override bool ContainsUnresolvedCrossLinksImpl()
		{
			return !this.body.QueryGroupID.IsResolved || !this.body.ParameterValuesColumnID.IsResolved || !this.body.ExpressionSourceID.IsResolved || base.ContainsUnresolvedCrossLinksImpl();
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00043B5B File Offset: 0x00041D5B
		internal override IEnumerable<IMetadataObjectCollection> GetChildrenCollections(bool isLogicalStructure)
		{
			yield return this._Annotations;
			yield return this._ExtendedProperties;
			yield return this._ExcludedArtifacts;
			yield break;
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060007FB RID: 2043 RVA: 0x00043B6B File Offset: 0x00041D6B
		public NamedExpressionAnnotationCollection Annotations
		{
			get
			{
				return this._Annotations;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x00043B73 File Offset: 0x00041D73
		public NamedExpressionExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return this._ExtendedProperties;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060007FD RID: 2045 RVA: 0x00043B7B File Offset: 0x00041D7B
		[EditorBrowsable(EditorBrowsableState.Never)]
		[CompatibilityRequirement("Preview")]
		public NamedExpressionExcludedArtifactCollection ExcludedArtifacts
		{
			get
			{
				return this._ExcludedArtifacts;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x00043B83 File Offset: 0x00041D83
		// (set) Token: 0x060007FF RID: 2047 RVA: 0x00043B90 File Offset: 0x00041D90
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
					if (!Utils.IsSyntacticallyValidName(value, ObjectType.Expression, out text))
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

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x00043C13 File Offset: 0x00041E13
		// (set) Token: 0x06000801 RID: 2049 RVA: 0x00043C20 File Offset: 0x00041E20
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

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x00043C90 File Offset: 0x00041E90
		// (set) Token: 0x06000803 RID: 2051 RVA: 0x00043CA0 File Offset: 0x00041EA0
		public ExpressionKind Kind
		{
			get
			{
				return this.body.Kind;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Kind, value))
				{
					CompatibilityRestrictionSet expressionKindCompatibilityRestrictions = PropertyHelper.GetExpressionKindCompatibilityRestrictions(value);
					CompatibilityRestrictionSet expressionKindCompatibilityRestrictions2 = PropertyHelper.GetExpressionKindCompatibilityRestrictions(this.body.Kind);
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					RestrictionsComapreResult restrictionsComapreResult = expressionKindCompatibilityRestrictions.Compare(expressionKindCompatibilityRestrictions2);
					if (restrictionsComapreResult == RestrictionsComapreResult.MoreRestrictive || restrictionsComapreResult == RestrictionsComapreResult.Incomparable || (restrictionsComapreResult == RestrictionsComapreResult.Equal && value != (ExpressionKind)(-1)))
					{
						array = base.ValidateCompatibilityRequirement(expressionKindCompatibilityRestrictions, string.Format("[{0}]::[{1}]=[{2}]", this.GetFormattedObjectPath(), "Kind", value));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "Kind", typeof(ExpressionKind), this.body.Kind, value);
					switch (restrictionsComapreResult)
					{
					case RestrictionsComapreResult.Incomparable:
						base.ResetCompatibilityRequirement();
						base.SetCompatibilityRequirement(expressionKindCompatibilityRestrictions, array);
						break;
					case RestrictionsComapreResult.LessRestrictive:
						base.ResetCompatibilityRequirement();
						break;
					case RestrictionsComapreResult.Equal:
						if (array != null)
						{
							base.SetCompatibilityRequirement(expressionKindCompatibilityRestrictions, array);
						}
						break;
					case RestrictionsComapreResult.MoreRestrictive:
						base.SetCompatibilityRequirement(expressionKindCompatibilityRestrictions, array);
						break;
					}
					ExpressionKind kind = this.body.Kind;
					this.body.Kind = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Kind", typeof(ExpressionKind), kind, value);
				}
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x00043DC2 File Offset: 0x00041FC2
		// (set) Token: 0x06000805 RID: 2053 RVA: 0x00043DD0 File Offset: 0x00041FD0
		public string Expression
		{
			get
			{
				return this.body.Expression;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Expression, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Expression", typeof(string), this.body.Expression, value);
					string expression = this.body.Expression;
					this.body.Expression = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Expression", typeof(string), expression, value);
				}
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x00043E40 File Offset: 0x00042040
		// (set) Token: 0x06000807 RID: 2055 RVA: 0x00043E50 File Offset: 0x00042050
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

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x00043ED4 File Offset: 0x000420D4
		// (set) Token: 0x06000809 RID: 2057 RVA: 0x00043EE4 File Offset: 0x000420E4
		[CompatibilityRequirement("1535")]
		public string MAttributes
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
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.NamedExpression_MAttributes, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "MAttributes"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "MAttributes", typeof(string), this.body.MAttributes, value);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.NamedExpression_MAttributes, array);
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

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x00043F99 File Offset: 0x00042199
		// (set) Token: 0x0600080B RID: 2059 RVA: 0x00043FA8 File Offset: 0x000421A8
		[CompatibilityRequirement("1540")]
		public string LineageTag
		{
			get
			{
				return this.body.LineageTag;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.LineageTag, value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (!string.IsNullOrEmpty(value))
					{
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.NamedExpression_LineageTag, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "LineageTag"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "LineageTag", typeof(string), this.body.LineageTag, value);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.NamedExpression_LineageTag, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					string lineageTag = this.body.LineageTag;
					this.body.LineageTag = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "LineageTag", typeof(string), lineageTag, value);
				}
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x0004405D File Offset: 0x0004225D
		// (set) Token: 0x0600080D RID: 2061 RVA: 0x0004406C File Offset: 0x0004226C
		[CompatibilityRequirement("1550")]
		public string SourceLineageTag
		{
			get
			{
				return this.body.SourceLineageTag;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.SourceLineageTag, value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (!string.IsNullOrEmpty(value))
					{
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.NamedExpression_SourceLineageTag, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "SourceLineageTag"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "SourceLineageTag", typeof(string), this.body.SourceLineageTag, value);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.NamedExpression_SourceLineageTag, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					string sourceLineageTag = this.body.SourceLineageTag;
					this.body.SourceLineageTag = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "SourceLineageTag", typeof(string), sourceLineageTag, value);
				}
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x00044121 File Offset: 0x00042321
		// (set) Token: 0x0600080F RID: 2063 RVA: 0x00044130 File Offset: 0x00042330
		[CompatibilityRequirement("1570")]
		public string RemoteParameterName
		{
			get
			{
				return this.body.RemoteParameterName;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.RemoteParameterName, value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (!string.IsNullOrEmpty(value))
					{
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.NamedExpression_RemoteParameterName, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "RemoteParameterName"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "RemoteParameterName", typeof(string), this.body.RemoteParameterName, value);
					if (array != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.NamedExpression_RemoteParameterName, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					string remoteParameterName = this.body.RemoteParameterName;
					this.body.RemoteParameterName = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "RemoteParameterName", typeof(string), remoteParameterName, value);
				}
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x000441E5 File Offset: 0x000423E5
		// (set) Token: 0x06000811 RID: 2065 RVA: 0x000441F7 File Offset: 0x000423F7
		internal ObjectId _ModelID
		{
			get
			{
				return this.body.ModelID.ObjectID;
			}
			set
			{
				this.body.ModelID.ObjectID = value;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x0004420A File Offset: 0x0004240A
		// (set) Token: 0x06000813 RID: 2067 RVA: 0x0004421C File Offset: 0x0004241C
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
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.NamedExpression_QueryGroup, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "QueryGroup"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "QueryGroup", typeof(QueryGroup), this.body.QueryGroupID.Object, value);
					if (value != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.NamedExpression_QueryGroup, array);
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

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x000442E0 File Offset: 0x000424E0
		// (set) Token: 0x06000815 RID: 2069 RVA: 0x000442F2 File Offset: 0x000424F2
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

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000816 RID: 2070 RVA: 0x00044305 File Offset: 0x00042505
		// (set) Token: 0x06000817 RID: 2071 RVA: 0x00044318 File Offset: 0x00042518
		[CompatibilityRequirement("1545")]
		public Column ParameterValuesColumn
		{
			get
			{
				return this.body.ParameterValuesColumnID.Object;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.ParameterValuesColumnID.Object, value))
				{
					KeyValuePair<CompatibilityMode, Stack<string>>[] array = null;
					if (value != null)
					{
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.NamedExpression_ParameterValuesColumn, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "ParameterValuesColumn"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "ParameterValuesColumn", typeof(Column), this.body.ParameterValuesColumnID.Object, value);
					if (value != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.NamedExpression_ParameterValuesColumn, array);
					}
					else
					{
						base.ResetCompatibilityRequirement();
					}
					Column @object = this.body.ParameterValuesColumnID.Object;
					this.body.ParameterValuesColumnID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "ParameterValuesColumn", typeof(Column), @object, value);
				}
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000818 RID: 2072 RVA: 0x000443DC File Offset: 0x000425DC
		// (set) Token: 0x06000819 RID: 2073 RVA: 0x000443EE File Offset: 0x000425EE
		internal ObjectId _ParameterValuesColumnID
		{
			get
			{
				return this.body.ParameterValuesColumnID.ObjectID;
			}
			set
			{
				this.body.ParameterValuesColumnID.ObjectID = value;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x00044401 File Offset: 0x00042601
		// (set) Token: 0x0600081B RID: 2075 RVA: 0x00044414 File Offset: 0x00042614
		[CompatibilityRequirement("1570")]
		public NamedExpression ExpressionSource
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
						array = base.ValidateCompatibilityRequirement(CompatibilityRestrictions.NamedExpression_ExpressionSource, string.Format("[{0}]::[{1}]", this.GetFormattedObjectPath(), "ExpressionSource"));
					}
					ObjectChangeTracker.RegisterPropertyChanging(this, "ExpressionSource", typeof(NamedExpression), this.body.ExpressionSourceID.Object, value);
					if (value != null)
					{
						base.SetCompatibilityRequirement(CompatibilityRestrictions.NamedExpression_ExpressionSource, array);
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

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x000444D8 File Offset: 0x000426D8
		// (set) Token: 0x0600081D RID: 2077 RVA: 0x000444EA File Offset: 0x000426EA
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

		// Token: 0x0600081E RID: 2078 RVA: 0x00044500 File Offset: 0x00042700
		internal void CopyFrom(NamedExpression other, CopyContext context)
		{
			base.CopyFrom(other, context);
			bool flag;
			if ((context.Flags & CopyFlags.IncludeCompatRestictions) == CopyFlags.IncludeCompatRestictions)
			{
				flag = true;
			}
			else if ((context.Flags & CopyFlags.MetadataSync) == CopyFlags.MetadataSync)
			{
				flag = this.body.ModifiedTime.CompareTo(other.body.ModifiedTime) != 0;
			}
			else
			{
				flag = !this.body.IsEqualTo(other.body, context);
			}
			if (flag)
			{
				ObjectChangeTracker.RegisterUpcomingPropertyChange(this);
				this.body.CopyFrom(other.body, context);
			}
			if ((context.Flags & CopyFlags.ShallowCopy) != CopyFlags.ShallowCopy)
			{
				this.Annotations.CopyFrom(other.Annotations, context);
				this.ExtendedProperties.CopyFrom(other.ExtendedProperties, context);
				this.ExcludedArtifacts.CopyFrom(other.ExcludedArtifacts, context);
			}
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x000445D4 File Offset: 0x000427D4
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((NamedExpression)other, context);
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x000445E3 File Offset: 0x000427E3
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(NamedExpression other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x000445FF File Offset: 0x000427FF
		public void CopyTo(NamedExpression other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x06000822 RID: 2082 RVA: 0x0004461B File Offset: 0x0004281B
		public NamedExpression Clone()
		{
			return base.CloneInternal<NamedExpression>();
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x00044624 File Offset: 0x00042824
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.NamedExpression.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object Expression is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			this.body.QueryGroupID.Validate(null, true);
			if (this.body.QueryGroupID.Object != null)
			{
				if (!CompatibilityRestrictions.NamedExpression_QueryGroup.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member QueryGroupID is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				MetadataObject.WriteObjectId(writer, options, "QueryGroupID", this.body.QueryGroupID.Object);
			}
			this.body.ParameterValuesColumnID.Validate(null, true);
			if (this.body.ParameterValuesColumnID.Object != null)
			{
				if (!CompatibilityRestrictions.NamedExpression_ParameterValuesColumn.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member ParameterValuesColumnID is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				MetadataObject.WriteObjectId(writer, options, "ParameterValuesColumnID", this.body.ParameterValuesColumnID.Object);
			}
			this.body.ExpressionSourceID.Validate(null, true);
			if (this.body.ExpressionSourceID.Object != null)
			{
				if (!CompatibilityRestrictions.NamedExpression_ExpressionSource.IsCompatible(mode, dbCompatibilityLevel))
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
			if (this.body.Kind != (ExpressionKind)(-1))
			{
				if (!PropertyHelper.IsExpressionKindValueCompatible(this.body.Kind, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member Kind is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<ExpressionKind>(options, "Kind", this.body.Kind);
			}
			if (!string.IsNullOrEmpty(this.body.Expression))
			{
				writer.WriteProperty<string>(options, "Expression", this.body.Expression);
			}
			if (!string.IsNullOrEmpty(this.body.MAttributes))
			{
				if (!CompatibilityRestrictions.NamedExpression_MAttributes.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member MAttributes is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<string>(options, "MAttributes", this.body.MAttributes);
			}
			if (!string.IsNullOrEmpty(this.body.LineageTag))
			{
				if (!CompatibilityRestrictions.NamedExpression_LineageTag.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member LineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<string>(options, "LineageTag", this.body.LineageTag);
			}
			if (!string.IsNullOrEmpty(this.body.SourceLineageTag))
			{
				if (!CompatibilityRestrictions.NamedExpression_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member SourceLineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<string>(options, "SourceLineageTag", this.body.SourceLineageTag);
			}
			if (!string.IsNullOrEmpty(this.body.RemoteParameterName))
			{
				if (!CompatibilityRestrictions.NamedExpression_RemoteParameterName.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member RemoteParameterName is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				writer.WriteProperty<string>(options, "RemoteParameterName", this.body.RemoteParameterName);
			}
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00044A14 File Offset: 0x00042C14
		void IMetadataObjectWithOverrides.WriteAllOverridenBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel, ReplacementPropertiesCollection newProperties)
		{
			if (!CompatibilityRestrictions.NamedExpression.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object Expression is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			this.body.ExpressionSourceID.Validate(null, true);
			MetadataObject metadataObject;
			if (newProperties.IsLinkOverriden("ExpressionSourceID", out metadataObject) && metadataObject != null && this.body.ExpressionSourceID.Object != metadataObject)
			{
				if (!CompatibilityRestrictions.NamedExpression_ExpressionSource.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member ExpressionSourceID is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				MetadataObject.WriteObjectId(writer, options, "ExpressionSourceID", metadataObject);
			}
			string text;
			if (newProperties.IsPropertyOverriden<string>("Expression", out text) && !PropertyHelper.AreValuesIdentical(this.body.Expression, text))
			{
				writer.WriteProperty<string>(options, "Expression", text);
			}
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00044AFC File Offset: 0x00042CFC
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("ModelID", out objectId))
			{
				this.body.ModelID.ObjectID = objectId;
			}
			ObjectId objectId2;
			if (CompatibilityRestrictions.NamedExpression_QueryGroup.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<ObjectId>("QueryGroupID", out objectId2))
			{
				this.body.QueryGroupID.ObjectID = objectId2;
			}
			ObjectId objectId3;
			if (CompatibilityRestrictions.NamedExpression_ParameterValuesColumn.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<ObjectId>("ParameterValuesColumnID", out objectId3))
			{
				this.body.ParameterValuesColumnID.ObjectID = objectId3;
			}
			ObjectId objectId4;
			if (CompatibilityRestrictions.NamedExpression_ExpressionSource.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<ObjectId>("ExpressionSourceID", out objectId4))
			{
				this.body.ExpressionSourceID.ObjectID = objectId4;
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
			ExpressionKind expressionKind;
			if (reader.TryReadProperty<ExpressionKind>("Kind", out expressionKind))
			{
				this.body.Kind = expressionKind;
			}
			string text3;
			if (reader.TryReadProperty<string>("Expression", out text3))
			{
				this.body.Expression = text3;
			}
			DateTime dateTime;
			if (reader.TryReadProperty<DateTime>("ModifiedTime", out dateTime))
			{
				this.body.ModifiedTime = dateTime;
			}
			string text4;
			if (CompatibilityRestrictions.NamedExpression_MAttributes.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<string>("MAttributes", out text4))
			{
				this.body.MAttributes = text4;
			}
			string text5;
			if (CompatibilityRestrictions.NamedExpression_LineageTag.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<string>("LineageTag", out text5))
			{
				this.body.LineageTag = text5;
			}
			string text6;
			if (CompatibilityRestrictions.NamedExpression_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<string>("SourceLineageTag", out text6))
			{
				this.body.SourceLineageTag = text6;
			}
			string text7;
			if (CompatibilityRestrictions.NamedExpression_RemoteParameterName.IsCompatible(mode, dbCompatibilityLevel) && reader.TryReadProperty<string>("RemoteParameterName", out text7))
			{
				this.body.RemoteParameterName = text7;
			}
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00044CF0 File Offset: 0x00042EF0
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.NamedExpression.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object Expression is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataBodyProperties(context, writer);
			this.body.QueryGroupID.Validate(null, true);
			if (this.body.QueryGroupID.Object != null)
			{
				if (!CompatibilityRestrictions.NamedExpression_QueryGroup.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member QueryGroupID is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("QueryGroupID", MetadataPropertyNature.CrossLinkProperty))
				{
					writer.WriteObjectIdProperty("QueryGroupID", MetadataPropertyNature.CrossLinkProperty, this.body.QueryGroupID.Object);
				}
			}
			this.body.ParameterValuesColumnID.Validate(null, true);
			if (this.body.ParameterValuesColumnID.Object != null)
			{
				if (!CompatibilityRestrictions.NamedExpression_ParameterValuesColumn.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member ParameterValuesColumnID is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("ParameterValuesColumnID", MetadataPropertyNature.CrossLinkProperty))
				{
					writer.WriteObjectIdProperty("ParameterValuesColumnID", MetadataPropertyNature.CrossLinkProperty, this.body.ParameterValuesColumnID.Object);
				}
			}
			this.body.ExpressionSourceID.Validate(null, true);
			if (this.body.ExpressionSourceID.Object != null)
			{
				if (!CompatibilityRestrictions.NamedExpression_ExpressionSource.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
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
			if (!string.IsNullOrEmpty(this.body.Description) && writer.ShouldIncludeProperty("Description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("Description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, this.body.Description);
			}
			if (this.body.Kind != (ExpressionKind)(-1))
			{
				if (!PropertyHelper.IsExpressionKindValueCompatible(this.body.Kind, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member Kind is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("Kind", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<ExpressionKind>("Kind", MetadataPropertyNature.RegularProperty, this.body.Kind);
				}
			}
			if (!string.IsNullOrEmpty(this.body.Expression) && writer.ShouldIncludeProperty("Expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty))
			{
				writer.WriteStringProperty("Expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty, this.body.Expression);
			}
			if (!string.IsNullOrEmpty(this.body.MAttributes))
			{
				if (!CompatibilityRestrictions.NamedExpression_MAttributes.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member MAttributes is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("MAttributes", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteStringProperty("MAttributes", MetadataPropertyNature.RegularProperty, this.body.MAttributes);
				}
			}
			if (!string.IsNullOrEmpty(this.body.LineageTag))
			{
				if (!CompatibilityRestrictions.NamedExpression_LineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member LineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("LineageTag", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteStringProperty("LineageTag", MetadataPropertyNature.RegularProperty, this.body.LineageTag);
				}
			}
			if (!string.IsNullOrEmpty(this.body.SourceLineageTag))
			{
				if (!CompatibilityRestrictions.NamedExpression_SourceLineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member SourceLineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("SourceLineageTag", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteStringProperty("SourceLineageTag", MetadataPropertyNature.RegularProperty, this.body.SourceLineageTag);
				}
			}
			if (!string.IsNullOrEmpty(this.body.RemoteParameterName))
			{
				if (!CompatibilityRestrictions.NamedExpression_RemoteParameterName.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member RemoteParameterName is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("RemoteParameterName", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteStringProperty("RemoteParameterName", MetadataPropertyNature.RegularProperty, this.body.RemoteParameterName);
				}
			}
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x00045230 File Offset: 0x00043430
		private protected override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.NamedExpression.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object Expression is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataTree(context, writer);
			if (!string.IsNullOrEmpty(this.body.Name) && writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.Name);
			}
			if (!string.IsNullOrEmpty(this.body.Expression) && writer.ShouldIncludeProperty("expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty))
			{
				writer.WriteStringProperty("expression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString | MetadataPropertyNature.DefaultProperty, this.body.Expression);
			}
			if (!string.IsNullOrEmpty(this.body.Description) && writer.ShouldIncludeProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, this.body.Description);
			}
			if (this.body.Kind != (ExpressionKind)(-1))
			{
				if (!PropertyHelper.IsExpressionKindValueCompatible(this.body.Kind, context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member Kind is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("kind", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<ExpressionKind>("kind", MetadataPropertyNature.RegularProperty, this.body.Kind);
				}
			}
			if (this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.ModifiedTime);
			}
			if (!string.IsNullOrEmpty(this.body.MAttributes))
			{
				if (!CompatibilityRestrictions.NamedExpression_MAttributes.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member MAttributes is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("mAttributes", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteStringProperty("mAttributes", MetadataPropertyNature.RegularProperty, this.body.MAttributes);
				}
			}
			if (!string.IsNullOrEmpty(this.body.LineageTag))
			{
				if (!CompatibilityRestrictions.NamedExpression_LineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member LineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("lineageTag", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteStringProperty("lineageTag", MetadataPropertyNature.RegularProperty, this.body.LineageTag);
				}
			}
			if (!string.IsNullOrEmpty(this.body.SourceLineageTag))
			{
				if (!CompatibilityRestrictions.NamedExpression_SourceLineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member SourceLineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("sourceLineageTag", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteStringProperty("sourceLineageTag", MetadataPropertyNature.RegularProperty, this.body.SourceLineageTag);
				}
			}
			if (!string.IsNullOrEmpty(this.body.RemoteParameterName))
			{
				if (!CompatibilityRestrictions.NamedExpression_RemoteParameterName.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member RemoteParameterName is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("remoteParameterName", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteStringProperty("remoteParameterName", MetadataPropertyNature.RegularProperty, this.body.RemoteParameterName);
				}
			}
			if (this.body.QueryGroupID.Object != null)
			{
				if (!CompatibilityRestrictions.NamedExpression_QueryGroup.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member QueryGroupID is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("queryGroup", MetadataPropertyNature.CrossLinkProperty))
				{
					this.body.QueryGroupID.WriteToMetadataStream(ObjectType.QueryGroup, true, "queryGroup", false, writer);
				}
			}
			if (this.body.ParameterValuesColumnID.Object != null)
			{
				if (!CompatibilityRestrictions.NamedExpression_ParameterValuesColumn.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member ParameterValuesColumnID is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("parameterValuesColumn", MetadataPropertyNature.CrossLinkProperty))
				{
					this.body.ParameterValuesColumnID.WriteToMetadataStream(ObjectType.Table, true, "parameterValuesColumn", false, writer);
				}
			}
			if (this.body.ExpressionSourceID.Object != null)
			{
				if (!CompatibilityRestrictions.NamedExpression_ExpressionSource.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member ExpressionSourceID is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("expressionSource", MetadataPropertyNature.CrossLinkProperty))
				{
					this.body.ExpressionSourceID.WriteToMetadataStream(ObjectType.Expression, true, "expressionSource", false, writer);
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
			if (this.ExcludedArtifacts.Count > 0)
			{
				if (!CompatibilityRestrictions.ExcludedArtifact.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("A child ExcludedArtifact is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("excludedArtifacts", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "excludedArtifacts", MetadataPropertyNature.ChildCollection, this.ExcludedArtifacts);
				}
			}
			if (this.Annotations.Count > 0 && writer.ShouldIncludeProperty("annotations", MetadataPropertyNature.ChildCollection))
			{
				writer.WriteChildCollection(context, "annotations", MetadataPropertyNature.ChildCollection, this.Annotations);
			}
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x0004588C File Offset: 0x00043A8C
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
						if (c != 'K')
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
							if (!(propertyName == "Kind"))
							{
								break;
							}
							goto IL_04D7;
						}
					}
					else if (c != 'k')
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
						if (!(propertyName == "kind"))
						{
							break;
						}
						goto IL_04D7;
					}
					this.body.Name = reader.ReadStringProperty();
					return true;
					IL_04D7:
					if (!CompatibilityRestrictions.ExpressionKind.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatibleProperty;
						return false;
					}
					this.body.Kind = reader.ReadEnumProperty<ExpressionKind>();
					return true;
				}
				case 7:
					if (propertyName == "ModelID")
					{
						this.body.ModelID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					break;
				case 10:
				{
					char c = propertyName[0];
					if (c <= 'L')
					{
						if (c != 'E')
						{
							if (c != 'L')
							{
								break;
							}
							if (!(propertyName == "LineageTag"))
							{
								break;
							}
							goto IL_055D;
						}
						else if (!(propertyName == "Expression"))
						{
							break;
						}
					}
					else if (c != 'e')
					{
						if (c != 'l')
						{
							if (c != 'q')
							{
								break;
							}
							if (!(propertyName == "queryGroup"))
							{
								break;
							}
							if (!CompatibilityRestrictions.NamedExpression_QueryGroup.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
							{
								classification = UnexpectedPropertyClassification.IncompatibleProperty;
								return false;
							}
							this.body.QueryGroupID.Path = reader.ReadCrossLinkProperty((string p) => new ObjectPath(ObjectType.QueryGroup, p));
							return true;
						}
						else
						{
							if (!(propertyName == "lineageTag"))
							{
								break;
							}
							goto IL_055D;
						}
					}
					else if (!(propertyName == "expression"))
					{
						break;
					}
					this.body.Expression = reader.ReadStringProperty();
					return true;
					IL_055D:
					if (!CompatibilityRestrictions.NamedExpression_LineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatibleProperty;
						return false;
					}
					this.body.LineageTag = reader.ReadStringProperty();
					return true;
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
							goto IL_052D;
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
							if (c != 'm')
							{
								break;
							}
							if (!(propertyName == "mAttributes"))
							{
								break;
							}
							goto IL_052D;
						}
						else if (!(propertyName == "description"))
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
					IL_052D:
					if (!CompatibilityRestrictions.NamedExpression_MAttributes.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatibleProperty;
						return false;
					}
					this.body.MAttributes = reader.ReadStringProperty();
					return true;
				}
				case 12:
				{
					char c = propertyName[0];
					if (c != 'M')
					{
						if (c != 'Q')
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
							if (!(propertyName == "QueryGroupID"))
							{
								break;
							}
							if (!CompatibilityRestrictions.NamedExpression_QueryGroup.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
							{
								return false;
							}
							this.body.QueryGroupID.ObjectID = reader.ReadObjectIdProperty();
							return true;
						}
					}
					else if (!(propertyName == "ModifiedTime"))
					{
						break;
					}
					this.body.ModifiedTime = reader.ReadDateTimeProperty();
					return true;
				}
				case 16:
				{
					char c = propertyName[0];
					if (c != 'S')
					{
						if (c != 'e')
						{
							if (c != 's')
							{
								break;
							}
							if (!(propertyName == "sourceLineageTag"))
							{
								break;
							}
						}
						else
						{
							if (!(propertyName == "expressionSource"))
							{
								break;
							}
							if (!CompatibilityRestrictions.NamedExpression_ExpressionSource.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
							{
								classification = UnexpectedPropertyClassification.IncompatibleProperty;
								return false;
							}
							this.body.ExpressionSourceID.Path = reader.ReadCrossLinkProperty((string p) => new ObjectPath(ObjectType.Expression, p));
							return true;
						}
					}
					else if (!(propertyName == "SourceLineageTag"))
					{
						break;
					}
					if (!CompatibilityRestrictions.NamedExpression_SourceLineageTag.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatibleProperty;
						return false;
					}
					this.body.SourceLineageTag = reader.ReadStringProperty();
					return true;
				}
				case 17:
					if (propertyName == "excludedArtifacts")
					{
						if (!CompatibilityRestrictions.ExcludedArtifact.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							classification = UnexpectedPropertyClassification.IncompatibleProperty;
							return false;
						}
						using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
						{
							foreach (ExcludedArtifact excludedArtifact in reader.ReadChildCollectionProperty<ExcludedArtifact>(context))
							{
								try
								{
									this.ExcludedArtifacts.Add(excludedArtifact);
								}
								catch (Exception ex2)
								{
									throw reader.CreateInvalidChildException(context, excludedArtifact, TomSR.Exception_FailedAddDeserializedObject("ExcludedArtifact", ex2.Message), ex2);
								}
							}
						}
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
										catch (Exception ex3)
										{
											throw reader.CreateInvalidChildException(context, extendedProperty, TomSR.Exception_FailedAddDeserializedNamedObject("ExtendedProperty", (extendedProperty != null) ? extendedProperty.Name : null, ex3.Message), ex3);
										}
									}
								}
								return true;
							}
						}
					}
					else if (propertyName == "ExpressionSourceID")
					{
						if (!CompatibilityRestrictions.NamedExpression_ExpressionSource.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							return false;
						}
						this.body.ExpressionSourceID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					break;
				}
				case 19:
				{
					char c = propertyName[0];
					if (c != 'R')
					{
						if (c != 'r')
						{
							break;
						}
						if (!(propertyName == "remoteParameterName"))
						{
							break;
						}
					}
					else if (!(propertyName == "RemoteParameterName"))
					{
						break;
					}
					if (!CompatibilityRestrictions.NamedExpression_RemoteParameterName.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
					{
						classification = UnexpectedPropertyClassification.IncompatibleProperty;
						return false;
					}
					this.body.RemoteParameterName = reader.ReadStringProperty();
					return true;
				}
				case 21:
					if (propertyName == "parameterValuesColumn")
					{
						if (!CompatibilityRestrictions.NamedExpression_ParameterValuesColumn.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							classification = UnexpectedPropertyClassification.IncompatibleProperty;
							return false;
						}
						this.body.ParameterValuesColumnID.Path = reader.ReadCrossLinkProperty();
						return true;
					}
					break;
				case 23:
					if (propertyName == "ParameterValuesColumnID")
					{
						if (!CompatibilityRestrictions.NamedExpression_ParameterValuesColumn.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
						{
							return false;
						}
						this.body.ParameterValuesColumnID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					break;
				}
			}
			classification = UnexpectedPropertyClassification.UnknownProperty;
			return false;
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00046190 File Offset: 0x00044390
		[Obsolete("Deprecated. Use RequestRename method instead.", false)]
		public void Rename(string newName)
		{
			this.RequestRename(newName);
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00046199 File Offset: 0x00044399
		public void RequestRename(string newName)
		{
			ObjectChangeTracker.RegisterObjectRenaming(this);
			this.Name = newName;
			this.body.RenameRequestedThroughAPI = true;
			ObjectChangeTracker.RegisterObjectRenamed(this);
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x000461BC File Offset: 0x000443BC
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.NamedExpression.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object Expression is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			if (!string.IsNullOrEmpty(this.body.Name))
			{
				result["name", TomPropCategory.Name, 2, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Name, SplitMultilineOptions.None);
			}
			if (!string.IsNullOrEmpty(this.body.Description))
			{
				result["description", TomPropCategory.Regular, 3, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Description, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.Kind != (ExpressionKind)(-1))
			{
				if (!PropertyHelper.IsExpressionKindValueCompatible(this.body.Kind, mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member Kind is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["kind", TomPropCategory.Regular, 4, false] = JsonPropertyHelper.ConvertEnumToJsonValue<ExpressionKind>(this.body.Kind);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.Expression))
			{
				result["expression", TomPropCategory.Regular, 5, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Expression, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["modifiedTime", TomPropCategory.Regular, 6, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.ModifiedTime);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.MAttributes))
			{
				if (!CompatibilityRestrictions.NamedExpression_MAttributes.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member MAttributes is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["mAttributes", TomPropCategory.Regular, 9, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.MAttributes, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.LineageTag))
			{
				if (!CompatibilityRestrictions.NamedExpression_LineageTag.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member LineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["lineageTag", TomPropCategory.Regular, 10, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.LineageTag, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.SourceLineageTag))
			{
				if (!CompatibilityRestrictions.NamedExpression_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member SourceLineageTag is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["sourceLineageTag", TomPropCategory.Regular, 11, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.SourceLineageTag, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.RemoteParameterName))
			{
				if (!CompatibilityRestrictions.NamedExpression_RemoteParameterName.IsCompatible(mode, dbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member RemoteParameterName is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
				}
				result["remoteParameterName", TomPropCategory.Regular, 12, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.RemoteParameterName, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly)
			{
				if (this.body.QueryGroupID.Object != null)
				{
					if (!CompatibilityRestrictions.NamedExpression_QueryGroup.IsCompatible(mode, dbCompatibilityLevel))
					{
						throw TomInternalException.Create("Member QueryGroupID is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
					}
					this.body.QueryGroupID.SerializeToJsonObject(true, "queryGroup", ObjectType.QueryGroup, result, 7, false);
				}
				if (this.body.ParameterValuesColumnID.Object != null)
				{
					if (!CompatibilityRestrictions.NamedExpression_ParameterValuesColumn.IsCompatible(mode, dbCompatibilityLevel))
					{
						throw TomInternalException.Create("Member ParameterValuesColumnID is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
					}
					this.body.ParameterValuesColumnID.SerializeToJsonObject(true, "parameterValuesColumn", ObjectType.Table, result, 8, false);
				}
				if (this.body.ExpressionSourceID.Object != null)
				{
					if (!CompatibilityRestrictions.NamedExpression_ExpressionSource.IsCompatible(mode, dbCompatibilityLevel))
					{
						throw TomInternalException.Create("Member ExpressionSourceID is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
					}
					this.body.ExpressionSourceID.SerializeToJsonObject(true, "expressionSource", ObjectType.Expression, result, 13, false);
				}
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations)
			{
				if (!options.IncludeTranslatablePropertiesOnly)
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
				if (!options.IncludeTranslatablePropertiesOnly)
				{
					IEnumerable<ExcludedArtifact> enumerable3;
					if (!options.IgnoreInferredObjects)
					{
						IEnumerable<ExcludedArtifact> excludedArtifacts = this.ExcludedArtifacts;
						enumerable3 = excludedArtifacts;
					}
					else
					{
						enumerable3 = this.ExcludedArtifacts.Where((ExcludedArtifact o) => !ObjectTreeHelper.IsInferredObject(o));
					}
					IEnumerable<ExcludedArtifact> enumerable4 = enumerable3;
					if (enumerable4.Any<ExcludedArtifact>())
					{
						if (!CompatibilityRestrictions.ExcludedArtifact.IsCompatible(mode, dbCompatibilityLevel))
						{
							throw TomInternalException.Create("A child ExcludedArtifact is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
						}
						object[] array = enumerable4.Select((ExcludedArtifact obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
						object[] array3 = array;
						result["excludedArtifacts", TomPropCategory.ChildCollection, 53, false] = array3;
					}
				}
			}
			if (!options.IgnoreChildren && !options.IncludeTranslatablePropertiesOnly && this.Annotations.Any<Annotation>())
			{
				object[] array = this.Annotations.Select((Annotation obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
				object[] array4 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array4;
			}
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0004695C File Offset: 0x00044B5C
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (name != null)
			{
				int length = name.Length;
				if (length != 4)
				{
					switch (length)
					{
					case 10:
					{
						char c = name[0];
						if (c != 'e')
						{
							if (c != 'l')
							{
								if (c == 'q')
								{
									if (name == "queryGroup")
									{
										if (!CompatibilityRestrictions.NamedExpression_QueryGroup.IsCompatible(mode, dbCompatibilityLevel))
										{
											return false;
										}
										this.body.QueryGroupID.Path = new ObjectPath(ObjectType.QueryGroup, JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value));
										return true;
									}
								}
							}
							else if (name == "lineageTag")
							{
								if (jsonProp.Value.Type != 10 && !CompatibilityRestrictions.NamedExpression_LineageTag.IsCompatible(mode, dbCompatibilityLevel))
								{
									return false;
								}
								this.body.LineageTag = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
								return true;
							}
						}
						else if (name == "expression")
						{
							this.body.Expression = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
							return true;
						}
						break;
					}
					case 11:
					{
						char c = name[0];
						if (c != 'a')
						{
							if (c != 'd')
							{
								if (c == 'm')
								{
									if (name == "mAttributes")
									{
										if (jsonProp.Value.Type != 10 && !CompatibilityRestrictions.NamedExpression_MAttributes.IsCompatible(mode, dbCompatibilityLevel))
										{
											return false;
										}
										this.body.MAttributes = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
										return true;
									}
								}
							}
							else if (name == "description")
							{
								this.body.Description = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
								return true;
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
						if (name == "modifiedTime")
						{
							this.body.ModifiedTime = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
							return true;
						}
						break;
					case 16:
					{
						char c = name[0];
						if (c != 'e')
						{
							if (c == 's')
							{
								if (name == "sourceLineageTag")
								{
									if (jsonProp.Value.Type != 10 && !CompatibilityRestrictions.NamedExpression_SourceLineageTag.IsCompatible(mode, dbCompatibilityLevel))
									{
										return false;
									}
									this.body.SourceLineageTag = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
									return true;
								}
							}
						}
						else if (name == "expressionSource")
						{
							if (!CompatibilityRestrictions.NamedExpression_ExpressionSource.IsCompatible(mode, dbCompatibilityLevel))
							{
								return false;
							}
							this.body.ExpressionSourceID.Path = new ObjectPath(ObjectType.Expression, JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value));
							return true;
						}
						break;
					}
					case 17:
						if (name == "excludedArtifacts")
						{
							if (!JsonPropertyHelper.IsEmptyObjectCollection(jsonProp.Value) && !CompatibilityRestrictions.ExcludedArtifact.IsCompatible(mode, dbCompatibilityLevel))
							{
								return false;
							}
							JsonPropertyHelper.ReadObjectCollection(this.ExcludedArtifacts, jsonProp.Value, options, mode, dbCompatibilityLevel);
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
					case 19:
						if (name == "remoteParameterName")
						{
							if (jsonProp.Value.Type != 10 && !CompatibilityRestrictions.NamedExpression_RemoteParameterName.IsCompatible(mode, dbCompatibilityLevel))
							{
								return false;
							}
							this.body.RemoteParameterName = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
							return true;
						}
						break;
					case 21:
						if (name == "parameterValuesColumn")
						{
							if (!CompatibilityRestrictions.NamedExpression_ParameterValuesColumn.IsCompatible(mode, dbCompatibilityLevel))
							{
								return false;
							}
							this.body.ParameterValuesColumnID.Path = ObjectPath.Parse((JObject)jsonProp.Value);
							return true;
						}
						break;
					}
				}
				else
				{
					char c = name[0];
					if (c != 'k')
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
					else if (name == "kind")
					{
						ExpressionKind expressionKind = JsonPropertyHelper.ConvertJsonValueToEnum<ExpressionKind>(jsonProp.Value);
						if (jsonProp.Value.Type != 10 && !PropertyHelper.IsExpressionKindValueCompatible(expressionKind, mode, dbCompatibilityLevel))
						{
							return false;
						}
						this.body.Kind = expressionKind;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00046E0D File Offset: 0x0004500D
		internal override string GetFormattedObjectPath()
		{
			return TomSR.ObjectPath_Expression_1Arg(this.Name);
		}

		// Token: 0x04000140 RID: 320
		internal NamedExpression.ObjectBody body;

		// Token: 0x04000141 RID: 321
		private NamedExpressionAnnotationCollection _Annotations;

		// Token: 0x04000142 RID: 322
		private NamedExpressionExtendedPropertyCollection _ExtendedProperties;

		// Token: 0x04000143 RID: 323
		private NamedExpressionExcludedArtifactCollection _ExcludedArtifacts;

		// Token: 0x02000294 RID: 660
		internal class ObjectBody : NamedMetadataObjectBody<NamedExpression>
		{
			// Token: 0x0600218C RID: 8588 RVA: 0x000D9BD4 File Offset: 0x000D7DD4
			public ObjectBody(NamedExpression owner)
				: base(owner)
			{
				this.ModifiedTime = DateTime.MinValue;
				this.ModelID = new ParentLink<NamedExpression, Model>(owner, "Model");
				this.QueryGroupID = new CrossLink<NamedExpression, QueryGroup>(owner, "QueryGroup");
				this.ParameterValuesColumnID = new CrossLink<NamedExpression, Column>(owner, "ParameterValuesColumn");
				this.ExpressionSourceID = new CrossLink<NamedExpression, NamedExpression>(owner, "ExpressionSource");
			}

			// Token: 0x0600218D RID: 8589 RVA: 0x000D9C37 File Offset: 0x000D7E37
			public override string GetObjectName()
			{
				return this.Name;
			}

			// Token: 0x0600218E RID: 8590 RVA: 0x000D9C40 File Offset: 0x000D7E40
			internal bool IsEqualTo(NamedExpression.ObjectBody other, CopyContext context)
			{
				return PropertyHelper.AreValuesIdentical(this.Name, other.Name) && PropertyHelper.AreValuesIdentical(this.Description, other.Description) && PropertyHelper.AreValuesIdentical(this.Kind, other.Kind) && PropertyHelper.AreValuesIdentical(this.Expression, other.Expression) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime)) && PropertyHelper.AreValuesIdentical(this.MAttributes, other.MAttributes) && PropertyHelper.AreValuesIdentical(this.LineageTag, other.LineageTag) && PropertyHelper.AreValuesIdentical(this.SourceLineageTag, other.SourceLineageTag) && PropertyHelper.AreValuesIdentical(this.RemoteParameterName, other.RemoteParameterName) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.ModelID.IsEqualTo(other.ModelID, context)) && this.QueryGroupID.IsEqualTo(other.QueryGroupID, context) && this.ParameterValuesColumnID.IsEqualTo(other.ParameterValuesColumnID, context) && this.ExpressionSourceID.IsEqualTo(other.ExpressionSourceID, context);
			}

			// Token: 0x0600218F RID: 8591 RVA: 0x000D9D8C File Offset: 0x000D7F8C
			internal void CopyFromImpl(NamedExpression.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
				base.Owner.Name = other.Name;
				this.Description = other.Description;
				this.Kind = other.Kind;
				this.Expression = other.Expression;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ModifiedTime = other.ModifiedTime;
				}
				this.MAttributes = other.MAttributes;
				base.Owner.LineageTag = other.LineageTag;
				this.SourceLineageTag = other.SourceLineageTag;
				this.RemoteParameterName = other.RemoteParameterName;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ModelID.CopyFrom(other.ModelID, context);
				}
				this.QueryGroupID.CopyFrom(other.QueryGroupID, context);
				this.ParameterValuesColumnID.CopyFrom(other.ParameterValuesColumnID, context);
				this.ExpressionSourceID.CopyFrom(other.ExpressionSourceID, context);
			}

			// Token: 0x06002190 RID: 8592 RVA: 0x000D9E90 File Offset: 0x000D8090
			internal void CopyFromImpl(NamedExpression.ObjectBody other)
			{
				this.Name = other.Name;
				this.Description = other.Description;
				this.Kind = other.Kind;
				this.Expression = other.Expression;
				this.ModifiedTime = other.ModifiedTime;
				this.MAttributes = other.MAttributes;
				this.LineageTag = other.LineageTag;
				this.SourceLineageTag = other.SourceLineageTag;
				this.RemoteParameterName = other.RemoteParameterName;
				this.ModelID.CopyFrom(other.ModelID, ObjectChangeTracker.BodyCloneContext);
				this.QueryGroupID.CopyFrom(other.QueryGroupID, ObjectChangeTracker.BodyCloneContext);
				this.ParameterValuesColumnID.CopyFrom(other.ParameterValuesColumnID, ObjectChangeTracker.BodyCloneContext);
				this.ExpressionSourceID.CopyFrom(other.ExpressionSourceID, ObjectChangeTracker.BodyCloneContext);
			}

			// Token: 0x06002191 RID: 8593 RVA: 0x000D9F61 File Offset: 0x000D8161
			public override void CopyFrom(MetadataObjectBody<NamedExpression> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((NamedExpression.ObjectBody)other, context);
			}

			// Token: 0x06002192 RID: 8594 RVA: 0x000D9F78 File Offset: 0x000D8178
			internal bool IsEqualTo(NamedExpression.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.Name, other.Name) && PropertyHelper.AreValuesIdentical(this.Description, other.Description) && PropertyHelper.AreValuesIdentical(this.Kind, other.Kind) && PropertyHelper.AreValuesIdentical(this.Expression, other.Expression) && PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime) && PropertyHelper.AreValuesIdentical(this.MAttributes, other.MAttributes) && PropertyHelper.AreValuesIdentical(this.LineageTag, other.LineageTag) && PropertyHelper.AreValuesIdentical(this.SourceLineageTag, other.SourceLineageTag) && PropertyHelper.AreValuesIdentical(this.RemoteParameterName, other.RemoteParameterName) && this.ModelID.IsEqualTo(other.ModelID) && this.QueryGroupID.IsEqualTo(other.QueryGroupID) && this.ParameterValuesColumnID.IsEqualTo(other.ParameterValuesColumnID) && this.ExpressionSourceID.IsEqualTo(other.ExpressionSourceID);
			}

			// Token: 0x06002193 RID: 8595 RVA: 0x000DA097 File Offset: 0x000D8297
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((NamedExpression.ObjectBody)other);
			}

			// Token: 0x06002194 RID: 8596 RVA: 0x000DA0B0 File Offset: 0x000D82B0
			internal void CompareWith(NamedExpression.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.Name, other.Name))
				{
					context.RegisterPropertyChange(base.Owner, "Name", typeof(string), base.RenameRequestedThroughAPI ? PropertyFlags.User : PropertyFlags.DdlAndUser, other.Name, this.Name);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Description, other.Description))
				{
					context.RegisterPropertyChange(base.Owner, "Description", typeof(string), PropertyFlags.DdlAndUser, other.Description, this.Description);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Kind, other.Kind))
				{
					context.RegisterPropertyChange(base.Owner, "Kind", typeof(ExpressionKind), PropertyFlags.DdlAndUser, other.Kind, this.Kind);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Expression, other.Expression))
				{
					context.RegisterPropertyChange(base.Owner, "Expression", typeof(string), PropertyFlags.DdlAndUser, other.Expression, this.Expression);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "ModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ModifiedTime, this.ModifiedTime);
				}
				if (!PropertyHelper.AreValuesIdentical(this.MAttributes, other.MAttributes))
				{
					context.RegisterPropertyChange(base.Owner, "MAttributes", typeof(string), PropertyFlags.DdlAndUser, other.MAttributes, this.MAttributes);
				}
				if (!PropertyHelper.AreValuesIdentical(this.LineageTag, other.LineageTag))
				{
					context.RegisterPropertyChange(base.Owner, "LineageTag", typeof(string), PropertyFlags.DdlAndUser, other.LineageTag, this.LineageTag);
				}
				if (!PropertyHelper.AreValuesIdentical(this.SourceLineageTag, other.SourceLineageTag))
				{
					context.RegisterPropertyChange(base.Owner, "SourceLineageTag", typeof(string), PropertyFlags.DdlAndUser, other.SourceLineageTag, this.SourceLineageTag);
				}
				if (!PropertyHelper.AreValuesIdentical(this.RemoteParameterName, other.RemoteParameterName))
				{
					context.RegisterPropertyChange(base.Owner, "RemoteParameterName", typeof(string), PropertyFlags.DdlAndUser, other.RemoteParameterName, this.RemoteParameterName);
				}
				this.ModelID.CompareWith(other.ModelID, "ModelID", "Model", PropertyFlags.ReadOnly, context);
				this.QueryGroupID.CompareWith(other.QueryGroupID, "QueryGroupID", "QueryGroup", PropertyFlags.None, context);
				this.ParameterValuesColumnID.CompareWith(other.ParameterValuesColumnID, "ParameterValuesColumnID", "ParameterValuesColumn", PropertyFlags.None, context);
				this.ExpressionSourceID.CompareWith(other.ExpressionSourceID, "ExpressionSourceID", "ExpressionSource", PropertyFlags.None, context);
			}

			// Token: 0x06002195 RID: 8597 RVA: 0x000DA363 File Offset: 0x000D8563
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((NamedExpression.ObjectBody)other, context);
			}

			// Token: 0x04000940 RID: 2368
			internal string Name;

			// Token: 0x04000941 RID: 2369
			internal string Description;

			// Token: 0x04000942 RID: 2370
			internal ExpressionKind Kind;

			// Token: 0x04000943 RID: 2371
			internal string Expression;

			// Token: 0x04000944 RID: 2372
			internal DateTime ModifiedTime;

			// Token: 0x04000945 RID: 2373
			internal string MAttributes;

			// Token: 0x04000946 RID: 2374
			internal string LineageTag;

			// Token: 0x04000947 RID: 2375
			internal string SourceLineageTag;

			// Token: 0x04000948 RID: 2376
			internal string RemoteParameterName;

			// Token: 0x04000949 RID: 2377
			internal ParentLink<NamedExpression, Model> ModelID;

			// Token: 0x0400094A RID: 2378
			internal CrossLink<NamedExpression, QueryGroup> QueryGroupID;

			// Token: 0x0400094B RID: 2379
			internal CrossLink<NamedExpression, Column> ParameterValuesColumnID;

			// Token: 0x0400094C RID: 2380
			internal CrossLink<NamedExpression, NamedExpression> ExpressionSourceID;
		}
	}
}
