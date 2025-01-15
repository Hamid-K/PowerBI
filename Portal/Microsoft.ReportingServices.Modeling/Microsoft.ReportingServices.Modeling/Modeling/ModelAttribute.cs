using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x02000077 RID: 119
	public sealed class ModelAttribute : ModelField, IQueryAttributeInternal, IPersistable, IQueryAttribute, IValidationScope
	{
		// Token: 0x06000508 RID: 1288 RVA: 0x0000F934 File Offset: 0x0000DB34
		public ModelAttribute()
		{
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0000F93C File Offset: 0x0000DB3C
		internal override void Reset()
		{
			base.Reset();
			this.m_dataType = DataType.String;
			this.m_nullable = false;
			this.m_expression = null;
			this.m_aggregate = false;
			this.m_filter = false;
			this.m_omitSecurityFilters = false;
			this.m_sortDirection = SortDirection.None;
			this.m_width = 0;
			this.m_alignment = Alignment.General;
			this.m_format = string.Empty;
			this.m_mimeType = string.Empty;
			this.m_dataCulture = null;
			this.m_discourageGrouping = false;
			this.m_enableDrillthrough = false;
			this.m_contextualName = AttributeContextualName.Attribute;
			this.__defaultAggAttr = null;
			this.m_valueSelection = AttributeValueSelection.None;
			this.m_binding = null;
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0000F9D8 File Offset: 0x0000DBD8
		internal ModelAttribute(ModelAttribute baseItem, bool markAsHidden)
			: base(baseItem, markAsHidden)
		{
			this.m_dataType = baseItem.m_dataType;
			this.m_nullable = baseItem.m_nullable;
			this.m_aggregate = baseItem.m_aggregate;
			this.m_filter = baseItem.m_filter;
			this.m_omitSecurityFilters = baseItem.m_omitSecurityFilters;
			this.m_sortDirection = baseItem.m_sortDirection;
			this.m_width = baseItem.m_width;
			this.m_alignment = baseItem.m_alignment;
			this.m_format = baseItem.m_format;
			this.m_mimeType = baseItem.m_mimeType;
			this.m_dataCulture = baseItem.m_dataCulture;
			this.m_discourageGrouping = baseItem.m_discourageGrouping;
			this.m_enableDrillthrough = !SemanticModel.SuppressDrillthroughDuringLazyClone && baseItem.m_enableDrillthrough;
			this.m_contextualName = baseItem.m_contextualName;
			this.m_valueSelection = baseItem.m_valueSelection;
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x0000FAAB File Offset: 0x0000DCAB
		ModelAttribute IQueryAttribute.ModelAttribute
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x0000FAAE File Offset: 0x0000DCAE
		Expression IQueryAttribute.CalculatedAttribute
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x0000FAB1 File Offset: 0x0000DCB1
		bool IQueryAttribute.IsInvalidRefTarget
		{
			get
			{
				return base.IsInvalidRefTarget;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x0000FAB9 File Offset: 0x0000DCB9
		bool IQueryAttributeInternal.ReplaceWithExpression
		{
			get
			{
				return this.m_binding == null && this.m_expression != null;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x0000FACE File Offset: 0x0000DCCE
		IQueryEntity IQueryAttributeInternal.SourceEntity
		{
			get
			{
				if (!this.IsAnchored())
				{
					return null;
				}
				return base.Entity;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x0000FAE0 File Offset: 0x0000DCE0
		// (set) Token: 0x06000511 RID: 1297 RVA: 0x0000FAE8 File Offset: 0x0000DCE8
		public DataType DataType
		{
			get
			{
				return this.m_dataType;
			}
			set
			{
				if (!EnumUtil.IsDefined<DataType>(value))
				{
					throw new InvalidEnumArgumentException();
				}
				if (value == DataType.Null)
				{
					throw new ArgumentException(DevExceptionMessages.ModelAttribute_DataTypeNull);
				}
				base.CheckWriteable();
				this.m_dataType = value;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x0000FB14 File Offset: 0x0000DD14
		// (set) Token: 0x06000513 RID: 1299 RVA: 0x0000FB1C File Offset: 0x0000DD1C
		public bool Nullable
		{
			get
			{
				return this.m_nullable;
			}
			set
			{
				base.CheckWriteable();
				this.m_nullable = value;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x0000FB2B File Offset: 0x0000DD2B
		// (set) Token: 0x06000515 RID: 1301 RVA: 0x0000FB33 File Offset: 0x0000DD33
		public Expression Expression
		{
			get
			{
				return this.m_expression;
			}
			set
			{
				base.CheckWriteable();
				this.m_expression = value;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x0000FB42 File Offset: 0x0000DD42
		// (set) Token: 0x06000517 RID: 1303 RVA: 0x0000FB4A File Offset: 0x0000DD4A
		public bool IsAggregate
		{
			get
			{
				return this.m_aggregate;
			}
			set
			{
				base.CheckWriteable();
				this.m_aggregate = value;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x0000FB59 File Offset: 0x0000DD59
		// (set) Token: 0x06000519 RID: 1305 RVA: 0x0000FB61 File Offset: 0x0000DD61
		public bool IsFilter
		{
			get
			{
				return this.m_filter;
			}
			set
			{
				base.CheckWriteable();
				this.m_filter = value;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x0000FB70 File Offset: 0x0000DD70
		// (set) Token: 0x0600051B RID: 1307 RVA: 0x0000FB78 File Offset: 0x0000DD78
		public bool OmitSecurityFilters
		{
			get
			{
				return this.m_omitSecurityFilters;
			}
			set
			{
				base.CheckWriteable();
				this.m_omitSecurityFilters = value;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x0000FB88 File Offset: 0x0000DD88
		public bool IsIdentifying
		{
			get
			{
				ModelEntity entity = base.Entity;
				if (entity != null)
				{
					foreach (AttributeReference attributeReference in entity.IdentifyingAttributes)
					{
						if (attributeReference.Path.IsEmpty && attributeReference.Attribute == this)
						{
							return true;
						}
					}
					return false;
				}
				return false;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x0000FBFC File Offset: 0x0000DDFC
		// (set) Token: 0x0600051E RID: 1310 RVA: 0x0000FC04 File Offset: 0x0000DE04
		public SortDirection SortDirection
		{
			get
			{
				return this.m_sortDirection;
			}
			set
			{
				if (!EnumUtil.IsDefined<SortDirection>(value))
				{
					throw new InvalidEnumArgumentException();
				}
				base.CheckWriteable();
				this.m_sortDirection = value;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x0000FC24 File Offset: 0x0000DE24
		// (set) Token: 0x06000520 RID: 1312 RVA: 0x0000FC8A File Offset: 0x0000DE8A
		public int Width
		{
			get
			{
				if (this.m_width == 0)
				{
					switch (this.m_dataType)
					{
					case DataType.String:
						return 20;
					case DataType.Integer:
					case DataType.Decimal:
					case DataType.Float:
						return 8;
					case DataType.Boolean:
						return 6;
					case DataType.DateTime:
						return 10;
					case DataType.Binary:
						return 1;
					case DataType.EntityKey:
						return 128;
					case DataType.Time:
						return 8;
					}
				}
				return this.m_width;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException(DevExceptionMessages.ModelAttribute_WidthLessThanZero);
				}
				base.CheckWriteable();
				this.m_width = value;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x0000FCA8 File Offset: 0x0000DEA8
		public bool IsWidthSet
		{
			get
			{
				return this.m_width != 0;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x0000FCB3 File Offset: 0x0000DEB3
		// (set) Token: 0x06000523 RID: 1315 RVA: 0x0000FCBB File Offset: 0x0000DEBB
		public Alignment Alignment
		{
			get
			{
				return this.m_alignment;
			}
			set
			{
				if (!EnumUtil.IsDefined<Alignment>(value))
				{
					throw new InvalidEnumArgumentException();
				}
				base.CheckWriteable();
				this.m_alignment = value;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x0000FCD8 File Offset: 0x0000DED8
		// (set) Token: 0x06000525 RID: 1317 RVA: 0x0000FCE0 File Offset: 0x0000DEE0
		public string Format
		{
			get
			{
				return this.m_format;
			}
			set
			{
				base.CheckWriteable();
				this.m_format = value ?? string.Empty;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x0000FCF8 File Offset: 0x0000DEF8
		// (set) Token: 0x06000527 RID: 1319 RVA: 0x0000FD00 File Offset: 0x0000DF00
		public string MimeType
		{
			get
			{
				return this.m_mimeType;
			}
			set
			{
				base.CheckWriteable();
				this.m_mimeType = value ?? string.Empty;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x0000FD18 File Offset: 0x0000DF18
		// (set) Token: 0x06000529 RID: 1321 RVA: 0x0000FD46 File Offset: 0x0000DF46
		public CultureInfo DataCulture
		{
			get
			{
				if (this.m_dataCulture != null)
				{
					return this.m_dataCulture;
				}
				SemanticModel model = this.Model;
				if (model == null)
				{
					return null;
				}
				return model.Culture;
			}
			set
			{
				base.CheckWriteable();
				this.m_dataCulture = ((value == null) ? null : CultureInfo.ReadOnly(value));
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x0000FD60 File Offset: 0x0000DF60
		public bool IsDataCultureSet
		{
			get
			{
				return this.m_dataCulture != null;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x0000FD6B File Offset: 0x0000DF6B
		// (set) Token: 0x0600052C RID: 1324 RVA: 0x0000FD73 File Offset: 0x0000DF73
		public bool DiscourageGrouping
		{
			get
			{
				return this.m_discourageGrouping;
			}
			set
			{
				base.CheckWriteable();
				this.m_discourageGrouping = value;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x0000FD82 File Offset: 0x0000DF82
		// (set) Token: 0x0600052E RID: 1326 RVA: 0x0000FD8A File Offset: 0x0000DF8A
		public bool EnableDrillthrough
		{
			get
			{
				return this.m_enableDrillthrough;
			}
			set
			{
				base.CheckWriteable();
				this.m_enableDrillthrough = value;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x0000FD99 File Offset: 0x0000DF99
		// (set) Token: 0x06000530 RID: 1328 RVA: 0x0000FDA1 File Offset: 0x0000DFA1
		public AttributeContextualName ContextualName
		{
			get
			{
				return this.m_contextualName;
			}
			set
			{
				if (!EnumUtil.IsDefined<AttributeContextualName>(value))
				{
					throw new InvalidEnumArgumentException();
				}
				base.CheckWriteable();
				this.m_contextualName = value;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x0000FDC0 File Offset: 0x0000DFC0
		// (set) Token: 0x06000532 RID: 1330 RVA: 0x0000FE29 File Offset: 0x0000E029
		public ModelAttribute DefaultAggregate
		{
			get
			{
				if (this.__defaultAggAttr == null && this.BaseItem != null && this.BaseItem.DefaultAggregate != null)
				{
					if (this.Model == null)
					{
						throw new InternalModelingException("BaseItem is non-null but Model is null");
					}
					this.__defaultAggAttr = this.Model.LookupItemByID(this.BaseItem.DefaultAggregate.ID) as ModelAttribute;
				}
				return this.__defaultAggAttr;
			}
			set
			{
				base.CheckWriteable();
				this.__defaultAggAttr = value;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x0000FE38 File Offset: 0x0000E038
		// (set) Token: 0x06000534 RID: 1332 RVA: 0x0000FE40 File Offset: 0x0000E040
		public AttributeValueSelection ValueSelection
		{
			get
			{
				return this.m_valueSelection;
			}
			set
			{
				if (!EnumUtil.IsDefined<AttributeValueSelection>(value))
				{
					throw new InvalidEnumArgumentException();
				}
				base.CheckWriteable();
				this.m_valueSelection = value;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x0000FE5D File Offset: 0x0000E05D
		// (set) Token: 0x06000536 RID: 1334 RVA: 0x0000FE65 File Offset: 0x0000E065
		public ColumnBinding Binding
		{
			get
			{
				return this.m_binding;
			}
			set
			{
				base.CheckWriteable();
				if (value != null)
				{
					value.SetContext(this);
				}
				if (this.m_binding != null)
				{
					this.m_binding.SetContext(null);
				}
				this.m_binding = value;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x0000FE92 File Offset: 0x0000E092
		internal override string ElementName
		{
			get
			{
				return "Attribute";
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x0000FE99 File Offset: 0x0000E099
		internal new ModelAttribute BaseItem
		{
			get
			{
				return (ModelAttribute)base.BaseItem;
			}
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0000FEA6 File Offset: 0x0000E0A6
		public ResultType GetResultType()
		{
			return new ResultType(this.m_dataType, Cardinality.One, this.m_nullable);
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0000FEBA File Offset: 0x0000E0BA
		public bool IsAnchored()
		{
			return !this.m_aggregate;
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0000FEC8 File Offset: 0x0000E0C8
		public Expression CreateReplacementExpression(bool inSetArgumentContext)
		{
			if (this.m_expression == null)
			{
				throw new InvalidOperationException();
			}
			Expression expression2 = this.m_expression.Clone();
			if (inSetArgumentContext)
			{
				bool needToWrap;
				if (!this.m_aggregate)
				{
					needToWrap = !this.m_expression.Path.IsEmpty;
				}
				else if (this.m_expression.NodeAsFunction == null)
				{
					needToWrap = !this.m_expression.Path.IsEmpty;
				}
				else
				{
					needToWrap = false;
					this.m_expression.NodeAsFunction.VisitAggregationFloatPoints(delegate(Expression expression, bool allowExprModification)
					{
						if (!expression.Path.IsEmpty || expression.NodeAsFunction != null)
						{
							needToWrap = true;
						}
						return null;
					}, false);
				}
				if (needToWrap)
				{
					expression2 = new Expression(new FunctionNode(FunctionName.Evaluate, new Expression[] { expression2 }));
				}
			}
			if (this.m_omitSecurityFilters)
			{
				expression2.MarkAsSkipSecurityFilters();
			}
			return expression2;
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x0000FF9C File Offset: 0x0000E19C
		public void UpdateFromExpression()
		{
			if (this.m_expression == null)
			{
				throw new InvalidOperationException();
			}
			ResultType resultType = this.m_expression.GetResultType();
			if (resultType.DataType != DataType.Null)
			{
				this.DataType = resultType.DataType;
			}
			this.Nullable = resultType.Nullable;
			if (this.m_expression.IsSubtreeAnchored())
			{
				this.IsAggregate = false;
			}
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x0000FFFC File Offset: 0x0000E1FC
		public void UpdateFromBinding()
		{
			if (this.m_binding == null)
			{
				throw new InvalidOperationException();
			}
			DsvColumn column = this.m_binding.GetColumn();
			if (column == null)
			{
				throw new InvalidOperationException();
			}
			if (column.ModelingDataType == null)
			{
				throw new InvalidOperationException();
			}
			this.DataType = column.ModelingDataType.Value;
			this.Nullable = column.Nullable;
			this.IsAggregate = false;
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00010069 File Offset: 0x0000E269
		protected override string GetAutoName()
		{
			Expression expression = this.m_expression;
			return string.Empty;
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00010077 File Offset: 0x0000E277
		public static IEnumerable<ColumnBinding> ListPotentialBindings(ModelEntity entity)
		{
			return ColumnBinding.ListBindings((entity != null) ? entity.Binding : null);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0001008C File Offset: 0x0000E28C
		internal override bool LoadXmlElement(ModelingXmlReader xr)
		{
			if (xr.IsDefaultNamespace)
			{
				string localName = xr.LocalName;
				if (localName != null)
				{
					int length = localName.Length;
					switch (length)
					{
					case 5:
						if (localName == "Width")
						{
							this.m_width = xr.ReadValueAsInt();
							return true;
						}
						break;
					case 6:
					{
						char c = localName[0];
						if (c != 'C')
						{
							if (c == 'F')
							{
								if (localName == "Format")
								{
									this.m_format = xr.ReadValueAsString();
									return true;
								}
							}
						}
						else if (localName == "Column")
						{
							this.Binding = ColumnBinding.FromReader(xr);
							return true;
						}
						break;
					}
					case 7:
					case 12:
					case 15:
					case 16:
					case 17:
						break;
					case 8:
					{
						char c = localName[0];
						if (c <= 'I')
						{
							if (c != 'D')
							{
								if (c == 'I')
								{
									if (localName == "IsFilter")
									{
										this.m_filter = xr.ReadValueAsBoolean();
										return true;
									}
								}
							}
							else if (localName == "DataType")
							{
								this.m_dataType = xr.ReadValueAsEnum<DataType>();
								return true;
							}
						}
						else if (c != 'M')
						{
							if (c == 'N')
							{
								if (localName == "Nullable")
								{
									this.m_nullable = xr.ReadValueAsBoolean();
									return true;
								}
							}
						}
						else if (localName == "MimeType")
						{
							this.m_mimeType = xr.ReadValueAsString();
							return true;
						}
						break;
					}
					case 9:
						if (localName == "Alignment")
						{
							this.m_alignment = xr.ReadValueAsEnum<Alignment>();
							return true;
						}
						break;
					case 10:
						if (localName == "Expression")
						{
							this.m_expression = new Expression();
							this.m_expression.Load(xr, false);
							return true;
						}
						break;
					case 11:
					{
						char c = localName[0];
						if (c != 'D')
						{
							if (c == 'I')
							{
								if (localName == "IsAggregate")
								{
									this.m_aggregate = xr.ReadValueAsBoolean();
									return true;
								}
							}
						}
						else if (localName == "DataCulture")
						{
							this.m_dataCulture = xr.ReadValueAsCultureInfo();
							return true;
						}
						break;
					}
					case 13:
						if (localName == "SortDirection")
						{
							this.m_sortDirection = xr.ReadValueAsEnum<SortDirection>();
							return true;
						}
						break;
					case 14:
					{
						char c = localName[0];
						if (c != 'C')
						{
							if (c == 'V')
							{
								if (localName == "ValueSelection")
								{
									this.m_valueSelection = xr.ReadValueAsEnum<AttributeValueSelection>();
									return true;
								}
							}
						}
						else if (localName == "ContextualName")
						{
							this.m_contextualName = xr.ReadValueAsEnum<AttributeContextualName>();
							return true;
						}
						break;
					}
					case 18:
					{
						char c = localName[0];
						if (c != 'D')
						{
							if (c == 'E')
							{
								if (localName == "EnableDrillthrough")
								{
									this.m_enableDrillthrough = xr.ReadValueAsBoolean();
									return true;
								}
							}
						}
						else if (localName == "DiscourageGrouping")
						{
							this.m_discourageGrouping = xr.ReadValueAsBoolean();
							return true;
						}
						break;
					}
					case 19:
						if (localName == "OmitSecurityFilters")
						{
							this.m_omitSecurityFilters = xr.ReadValueAsBoolean();
							return true;
						}
						break;
					default:
						if (length == 27)
						{
							if (localName == "DefaultAggregateAttributeID")
							{
								xr.Context.AddReference(this, xr.ReadReferenceByID("DefaultAggregateAttributeID", false));
								return true;
							}
						}
						break;
					}
				}
			}
			return base.LoadXmlElement(xr);
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00010449 File Offset: 0x0000E649
		internal override bool ProcessDeserializationReference(ModelingReference reference, DeserializationContext ctx)
		{
			if (reference.PropertyName == "DefaultAggregateAttributeID")
			{
				this.DefaultAggregate = ctx.CurrentModel.TryGetModelItem<ModelAttribute>(reference, ctx.Validation);
				return true;
			}
			return base.ProcessDeserializationReference(reference, ctx);
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00010480 File Offset: 0x0000E680
		internal override void WriteXmlElements(ModelingXmlWriter xw)
		{
			base.WriteXmlElements(xw);
			xw.WriteElement("DataType", this.m_dataType);
			xw.WriteElementIfNonDefault<bool>("Nullable", this.m_nullable);
			if (this.m_expression != null)
			{
				this.m_expression.WriteTo(xw);
			}
			xw.WriteElementIfNonDefault<bool>("IsAggregate", this.m_aggregate);
			xw.WriteElementIfNonDefault<bool>("IsFilter", this.m_filter);
			xw.WriteElementIfNonDefault<bool>("OmitSecurityFilters", this.m_omitSecurityFilters);
			xw.WriteElementIfNonDefault<SortDirection>("SortDirection", this.m_sortDirection);
			xw.WriteElementIfNonDefault<int>("Width", this.m_width);
			xw.WriteElementIfNonDefault<Alignment>("Alignment", this.m_alignment);
			xw.WriteElementIfNonDefault<string>("Format", this.m_format);
			xw.WriteElementIfNonDefault<string>("MimeType", this.m_mimeType);
			if (this.m_dataCulture != null && string.Empty != this.m_dataCulture.Name)
			{
				xw.WriteElementIfNonDefault<CultureInfo>("DataCulture", this.m_dataCulture);
			}
			xw.WriteElementIfNonDefault<bool>("DiscourageGrouping", this.m_discourageGrouping);
			xw.WriteElementIfNonDefault<bool>("EnableDrillthrough", this.m_enableDrillthrough);
			xw.WriteElementIfNonDefault<AttributeContextualName>("ContextualName", this.m_contextualName);
			xw.WriteReferenceElement("DefaultAggregateAttributeID", this.DefaultAggregate);
			xw.WriteElementIfNonDefault<AttributeValueSelection>("ValueSelection", this.m_valueSelection);
			base.WriteVariations(xw);
			if (this.m_binding != null && xw.ShouldWriteBindings)
			{
				this.m_binding.WriteTo(xw);
			}
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00010600 File Offset: 0x0000E800
		internal override void CompileCore(CompilationContext ctx)
		{
			base.CompileCore(ctx);
			if (this.m_expression != null)
			{
				ResultType? resultType = this.CompileExpression(ctx, this.m_expression);
				if (resultType != null)
				{
					ResultType value = resultType.Value;
					if (value.DataType != this.m_dataType && value.DataType != DataType.Null)
					{
						ctx.AddScopedError(ModelingErrorCode.ExpressionDataTypeMismatch, SRErrors.ExpressionDataTypeMismatch(ctx.CurrentObjectDescriptor, ctx.CurrentObjectDescriptor.ObjectName, this.m_dataType, value.DataType));
					}
					if (value.Nullable && !this.m_nullable)
					{
						ctx.AddScopedError(ModelingErrorCode.ExpressionNullableMismatch, SRErrors.ExpressionNullableMismatch(ctx.CurrentObjectDescriptor, ctx.CurrentObjectDescriptor.ObjectName));
					}
					if (value.Cardinality == Cardinality.Many)
					{
						ctx.AddScopedError(ModelingErrorCode.TopLevelSetExpression, SRErrors.TopLevelSetExpression_Attribute(ctx.CurrentObjectDescriptor, ctx.ContextEntityDescriptor));
					}
				}
			}
			if (this.m_filter && this.m_dataType != DataType.Boolean)
			{
				ctx.AddScopedError(ModelingErrorCode.NonBooleanFilterAttribute, SRErrors.NonBooleanFilterAttribute(ctx.CurrentObjectDescriptor, this.m_dataType));
			}
			if (this.m_dataType == DataType.Binary && string.IsNullOrEmpty(this.m_mimeType))
			{
				ctx.AddScopedWarning(ModelingErrorCode.MissingMimeType, SRErrors.MissingMimeType(ctx.CurrentObjectDescriptor));
			}
			if (this.DefaultAggregate != null)
			{
				if (this.m_aggregate)
				{
					ctx.AddScopedError(ModelingErrorCode.IsAggregateWithDefaultAggregate, SRErrors.IsAggregateWithDefaultAggregate(ctx.CurrentObjectDescriptor));
				}
				if (ctx.ShouldCheckInvalidRefsDuringCompilation && this.DefaultAggregate.IsInvalidRefTarget)
				{
					ctx.AddScopedError(ModelingErrorCode.ItemNotFound, SRErrors.ItemNotFound("DefaultAggregateAttributeID", ctx.CurrentObjectDescriptor, this.DefaultAggregate.ID.ToString()));
					return;
				}
				if (!this.DefaultAggregate.IsAggregate)
				{
					ctx.AddScopedError(ModelingErrorCode.NonAggregateAsDefaultAggregate, SRErrors.NonAggregateAsDefaultAggregate(ctx.CurrentObjectDescriptor, SRObjectDescriptor.FromScope(this.DefaultAggregate)));
				}
				if (!base.Variations.Contains(this.DefaultAggregate))
				{
					ctx.AddScopedError(ModelingErrorCode.NonVariationAsDefaultAggregate, SRErrors.NonVariationAsDefaultAggregate(ctx.CurrentObjectDescriptor, ctx.CurrentObjectDescriptor.ObjectName, SRObjectDescriptor.FromScope(this.DefaultAggregate)));
				}
			}
			if (ctx.ShouldCheckBindings)
			{
				this.CompileCheckBindings(ctx);
			}
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x00010818 File Offset: 0x0000EA18
		private void CompileCheckBindings(CompilationContext ctx)
		{
			DsvColumn dsvColumn;
			DataType dataType;
			if (this.m_binding == null)
			{
				if (this.m_expression == null)
				{
					ctx.AddScopedError(ModelingErrorCode.MissingBinding, SRErrors.MissingBinding_Attribute(ctx.CurrentObjectDescriptor));
					return;
				}
			}
			else if (this.m_binding.CheckBinding(ctx, false, out dsvColumn, out dataType))
			{
				if (dataType != this.m_dataType && (this.m_dataType != DataType.Decimal || dataType != DataType.Integer))
				{
					ctx.AddScopedError(ModelingErrorCode.ColumnDataTypeMismatch, SRErrors.ColumnDataTypeMismatch(ctx.CurrentObjectDescriptor, this.m_binding.GetColumnDescriptor(), this.m_dataType, dataType));
				}
				if (dsvColumn.Nullable && !this.m_nullable)
				{
					ctx.AddScopedError(ModelingErrorCode.ColumnNullableMismatch, SRErrors.ColumnNullableMismatch(ctx.CurrentObjectDescriptor, this.m_binding.GetColumnDescriptor()));
				}
				if (this.m_aggregate)
				{
					ctx.AddScopedError(ModelingErrorCode.IsAggregateWithColumn, SRErrors.IsAggregateWithColumn(ctx.CurrentObjectDescriptor));
				}
			}
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x000108E6 File Offset: 0x0000EAE6
		private ResultType? CompileExpression(CompilationContext ctx, Expression expression)
		{
			if (expression == null)
			{
				throw new InternalModelingException("expression is null");
			}
			if (this.m_aggregate)
			{
				return expression.Compile(ctx, ExpressionCompilationFlags.AggregateAttribute);
			}
			return expression.Compile(ctx, ExpressionCompilationFlags.ScalarAttribute);
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00010918 File Offset: 0x0000EB18
		bool IQueryAttributeInternal.CheckReference(CompilationContext ctx, string propertyName, bool multipleInScope)
		{
			if (!ctx.CheckContextEntityMatch(this, propertyName, multipleInScope))
			{
				return false;
			}
			if (!((IQueryAttributeInternal)this).ReplaceWithExpression)
			{
				return true;
			}
			Expression expression;
			if (!base.IsCompiled && ctx.ShouldNormalize)
			{
				expression = this.CreateReplacementExpression(false);
			}
			else
			{
				expression = this.m_expression;
			}
			return this.CompileExpression(ctx, expression) != null;
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0001096D File Offset: 0x0000EB6D
		internal override ModelItem CreateLazyClone(bool markAsHidden)
		{
			return new ModelAttribute(this, markAsHidden);
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00010978 File Offset: 0x0000EB78
		internal override bool ResolveRequiredReferences(SemanticModel newModel)
		{
			if (!base.ResolveRequiredReferences(newModel))
			{
				return false;
			}
			if (this.m_expression != null)
			{
				throw new InternalModelingException("ResolveRequiredReferences: expression already initialized on ModelAttribute");
			}
			if (this.BaseItem.Expression != null)
			{
				this.m_expression = this.BaseItem.Expression.CloneFor(newModel);
			}
			return true;
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x000109C8 File Offset: 0x0000EBC8
		internal override void Serialize(IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ModelAttribute.Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.DataType)
				{
					if (memberName != MemberName.Nullable)
					{
						switch (memberName)
						{
						case MemberName.ContextualName:
							writer.Write((byte)this.m_contextualName);
							continue;
						case MemberName.Binding:
							writer.Write(this.Binding);
							continue;
						case MemberName.Expression:
							writer.Write(this.m_expression);
							continue;
						case MemberName.Aggregate:
							writer.Write(this.m_aggregate);
							continue;
						case MemberName.Filter:
							writer.Write(this.m_filter);
							continue;
						case MemberName.OmitSecurityFilters:
							writer.Write(this.m_omitSecurityFilters);
							continue;
						case MemberName.SortDirection:
							writer.Write((byte)this.m_sortDirection);
							continue;
						case MemberName.Width:
							writer.Write(this.m_width);
							continue;
						case MemberName.Alignment:
							writer.Write((byte)this.m_alignment);
							continue;
						case MemberName.Format:
							writer.Write(this.m_format);
							continue;
						case MemberName.MimeType:
							writer.Write(this.m_mimeType);
							continue;
						case MemberName.DataCulture:
							writer.Write((this.m_dataCulture != null && string.Empty != this.m_dataCulture.Name) ? this.m_dataCulture.ToString() : null);
							continue;
						case MemberName.DiscourageGrouping:
							writer.Write(this.m_discourageGrouping);
							continue;
						case MemberName.EnableDrillthrough:
							writer.Write(this.m_enableDrillthrough);
							continue;
						case MemberName.DefaultAggAttr:
							PersistenceHelper.WriteModelingObjectReference<ModelAttribute>(ref writer, this.DefaultAggregate);
							continue;
						case MemberName.ValueSelection:
							writer.Write((byte)this.m_valueSelection);
							continue;
						}
						throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
					}
					writer.Write(this.m_nullable);
				}
				else
				{
					writer.Write((byte)this.m_dataType);
				}
			}
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x00010BF0 File Offset: 0x0000EDF0
		internal override void Deserialize(IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			using (base.AllowWriteOperations())
			{
				reader.RegisterDeclaration(ModelAttribute.Declaration);
				while (reader.NextMember())
				{
					MemberName memberName = reader.CurrentMember.MemberName;
					if (memberName != MemberName.DataType)
					{
						if (memberName != MemberName.Nullable)
						{
							switch (memberName)
							{
							case MemberName.ContextualName:
								this.m_contextualName = (AttributeContextualName)reader.ReadByte();
								continue;
							case MemberName.Binding:
								this.Binding = (ColumnBinding)reader.ReadRIFObject();
								continue;
							case MemberName.Expression:
								this.m_expression = (Expression)reader.ReadRIFObject();
								continue;
							case MemberName.Aggregate:
								this.m_aggregate = reader.ReadBoolean();
								continue;
							case MemberName.Filter:
								this.m_filter = reader.ReadBoolean();
								continue;
							case MemberName.OmitSecurityFilters:
								this.m_omitSecurityFilters = reader.ReadBoolean();
								continue;
							case MemberName.SortDirection:
								this.m_sortDirection = (SortDirection)reader.ReadByte();
								continue;
							case MemberName.Width:
								this.m_width = reader.ReadInt32();
								continue;
							case MemberName.Alignment:
								this.m_alignment = (Alignment)reader.ReadByte();
								continue;
							case MemberName.Format:
								this.m_format = reader.ReadString();
								continue;
							case MemberName.MimeType:
								this.m_mimeType = reader.ReadString();
								continue;
							case MemberName.DataCulture:
							{
								string text = reader.ReadString();
								if (!string.IsNullOrEmpty(text))
								{
									this.m_dataCulture = CultureInfo.GetCultureInfo(text);
									continue;
								}
								continue;
							}
							case MemberName.DiscourageGrouping:
								this.m_discourageGrouping = reader.ReadBoolean();
								continue;
							case MemberName.EnableDrillthrough:
								this.m_enableDrillthrough = reader.ReadBoolean();
								continue;
							case MemberName.DefaultAggAttr:
								this.DefaultAggregate = PersistenceHelper.ReadModelingObjectReference<ModelAttribute>(ref reader, this);
								continue;
							case MemberName.ValueSelection:
								this.m_valueSelection = (AttributeValueSelection)reader.ReadByte();
								continue;
							}
							throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
						}
						this.m_nullable = reader.ReadBoolean();
					}
					else
					{
						this.m_dataType = (DataType)reader.ReadByte();
					}
				}
			}
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00010E40 File Offset: 0x0000F040
		internal override void ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
		{
			using (base.AllowWriteOperations())
			{
				List<MemberReference> list;
				if (memberReferencesCollection.TryGetValue(ModelAttribute.Declaration.ObjectType, out list))
				{
					foreach (MemberReference memberReference in list)
					{
						if (memberReference.MemberName != MemberName.DefaultAggAttr)
						{
							throw new InternalModelingException("Unexpected member: " + memberReference.MemberName.ToString());
						}
						this.DefaultAggregate = PersistenceHelper.ResolveModelingObjectReference<ModelAttribute>(referenceableItems[memberReference.RefID]);
					}
				}
			}
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00010F04 File Offset: 0x0000F104
		internal override ObjectType GetObjectType()
		{
			return ObjectType.ModelAttribute;
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x00010F08 File Offset: 0x0000F108
		private static Declaration Declaration
		{
			get
			{
				return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref ModelAttribute.__declaration, ModelAttribute.__declarationLock, () => new Declaration(ObjectType.ModelAttribute, ObjectType.ModelField, new List<MemberInfo>
				{
					new MemberInfo(MemberName.DataType, Token.Byte),
					new MemberInfo(MemberName.Nullable, Token.Boolean),
					new MemberInfo(MemberName.Expression, ObjectType.Expression),
					new MemberInfo(MemberName.Aggregate, Token.Boolean),
					new MemberInfo(MemberName.Filter, Token.Boolean),
					new MemberInfo(MemberName.OmitSecurityFilters, Token.Boolean),
					new MemberInfo(MemberName.SortDirection, Token.Byte),
					new MemberInfo(MemberName.Width, Token.Int32),
					new MemberInfo(MemberName.Alignment, Token.Byte),
					new MemberInfo(MemberName.Format, Token.String),
					new MemberInfo(MemberName.MimeType, Token.String),
					new MemberInfo(MemberName.DataCulture, Token.String),
					new MemberInfo(MemberName.DiscourageGrouping, Token.Boolean),
					new MemberInfo(MemberName.EnableDrillthrough, Token.Boolean),
					new MemberInfo(MemberName.ContextualName, Token.Byte),
					new MemberInfo(MemberName.DefaultAggAttr, ObjectType.ModelAttribute, Token.Reference),
					new MemberInfo(MemberName.ValueSelection, Token.Byte),
					new MemberInfo(MemberName.Binding, ObjectType.ColumnBinding)
				}));
			}
		}

		// Token: 0x0400029A RID: 666
		internal const string AttributeElem = "Attribute";

		// Token: 0x0400029B RID: 667
		private const string DataTypeElem = "DataType";

		// Token: 0x0400029C RID: 668
		private const string NullableElem = "Nullable";

		// Token: 0x0400029D RID: 669
		private const string IsAggregateElem = "IsAggregate";

		// Token: 0x0400029E RID: 670
		private const string IsFilterElem = "IsFilter";

		// Token: 0x0400029F RID: 671
		private const string OmitSecurityFiltersElem = "OmitSecurityFilters";

		// Token: 0x040002A0 RID: 672
		private const string SortDirectionElem = "SortDirection";

		// Token: 0x040002A1 RID: 673
		private const string WidthElem = "Width";

		// Token: 0x040002A2 RID: 674
		private const string AlignmentElem = "Alignment";

		// Token: 0x040002A3 RID: 675
		private const string FormatElem = "Format";

		// Token: 0x040002A4 RID: 676
		private const string MimeTypeElem = "MimeType";

		// Token: 0x040002A5 RID: 677
		private const string DataCultureElem = "DataCulture";

		// Token: 0x040002A6 RID: 678
		private const string DiscourageGroupingElem = "DiscourageGrouping";

		// Token: 0x040002A7 RID: 679
		private const string EnableDrillthroughElem = "EnableDrillthrough";

		// Token: 0x040002A8 RID: 680
		private const string ContextualNameElem = "ContextualName";

		// Token: 0x040002A9 RID: 681
		private const string DefaultAggregateAttributeIdElem = "DefaultAggregateAttributeID";

		// Token: 0x040002AA RID: 682
		private const string ValueSelectionElem = "ValueSelection";

		// Token: 0x040002AB RID: 683
		private const int DefaultStringWidth = 20;

		// Token: 0x040002AC RID: 684
		private const int DefaultNumericWidth = 8;

		// Token: 0x040002AD RID: 685
		private const int DefaultBooleanWidth = 6;

		// Token: 0x040002AE RID: 686
		private const int DefaultDateTimeWidth = 10;

		// Token: 0x040002AF RID: 687
		private const int DefaultTimeWidth = 8;

		// Token: 0x040002B0 RID: 688
		private const int DefaultBinaryWidth = 1;

		// Token: 0x040002B1 RID: 689
		private const int DefaultEntityKeyWidth = 128;

		// Token: 0x040002B2 RID: 690
		private DataType m_dataType;

		// Token: 0x040002B3 RID: 691
		private bool m_nullable;

		// Token: 0x040002B4 RID: 692
		private Expression m_expression;

		// Token: 0x040002B5 RID: 693
		private bool m_aggregate;

		// Token: 0x040002B6 RID: 694
		private bool m_filter;

		// Token: 0x040002B7 RID: 695
		private bool m_omitSecurityFilters;

		// Token: 0x040002B8 RID: 696
		private SortDirection m_sortDirection;

		// Token: 0x040002B9 RID: 697
		private int m_width;

		// Token: 0x040002BA RID: 698
		private Alignment m_alignment;

		// Token: 0x040002BB RID: 699
		private string m_format;

		// Token: 0x040002BC RID: 700
		private string m_mimeType;

		// Token: 0x040002BD RID: 701
		private CultureInfo m_dataCulture;

		// Token: 0x040002BE RID: 702
		private bool m_discourageGrouping;

		// Token: 0x040002BF RID: 703
		private bool m_enableDrillthrough;

		// Token: 0x040002C0 RID: 704
		private AttributeContextualName m_contextualName;

		// Token: 0x040002C1 RID: 705
		private ModelAttribute __defaultAggAttr;

		// Token: 0x040002C2 RID: 706
		private AttributeValueSelection m_valueSelection;

		// Token: 0x040002C3 RID: 707
		private ColumnBinding m_binding;

		// Token: 0x040002C4 RID: 708
		private static Declaration __declaration;

		// Token: 0x040002C5 RID: 709
		private static readonly object __declarationLock = new object();
	}
}
