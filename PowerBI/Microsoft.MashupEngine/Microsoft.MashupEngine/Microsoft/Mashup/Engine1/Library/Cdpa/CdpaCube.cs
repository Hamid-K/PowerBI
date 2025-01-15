using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Host;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000D8B RID: 3467
	internal class CdpaCube : ICube, ICube2
	{
		// Token: 0x06005E3B RID: 24123 RVA: 0x0014557C File Offset: 0x0014377C
		public CdpaCube(CdpaService service)
		{
			this.service = service;
			this.dynamicPrefix = Guid.NewGuid().ToString();
			this.measuresDimensionName = QualifiedName.New("measures");
			this.dynamicMeasuresDimensionName = this.measuresDimensionName.Qualify(this.dynamicPrefix);
			this.dynamicAttributesDimensionName = QualifiedName.New("dimensions").Qualify(this.dynamicPrefix);
			this.functionCubeExpressionNormalizer = new CdpaCube.NormalizeFunctionsCubeExpressionVisitor();
			this.functionExpressionNormalizer = new CdpaCube.NormalizeFunctionsExpressionVisitor();
			this.attributeInliner = new InlineAttributesVisitor(this);
			this.dimensions = new Dictionary<QualifiedName, CdpaDimension>();
			this.attributes = new Dictionary<QualifiedName, CdpaDimensionAttribute>();
			this.attributeLevels = new Dictionary<CdpaDimensionAttribute, IList<CdpaHierarchyLevel>>();
			this.dynamicAttributes = new CdpaDynamicDimension(this, this.dynamicAttributesDimensionName, "Dynamic Dimension Attributes");
			this.measures = new Dictionary<QualifiedName, CdpaMeasure>();
			this.dynamicMeasures = new Dictionary<QualifiedName, CdpaMeasure>();
			this.parameters = new Dictionary<QualifiedName, CdpaParameter>();
			this.timestampAttributes = new HashSet<QualifiedName>();
			ICulture culture = this.Service.EngineHost.QueryService<ICultureService>().GetCulture(CultureInfo.InvariantCulture.Name);
			this.invariantTypeTransforms = new TransformTypesHelper(this.service.EngineHost, culture);
			try
			{
				this.PopulateCube();
			}
			catch (RuntimeException)
			{
				throw;
			}
			catch (Exception ex) when (SafeExceptions.IsSafeException(ex))
			{
				throw this.service.NewServiceError(Strings.Cdpa_CantLoadMetadata, ex);
			}
		}

		// Token: 0x17001BB9 RID: 7097
		// (get) Token: 0x06005E3C RID: 24124 RVA: 0x00145708 File Offset: 0x00143908
		public CdpaService Service
		{
			get
			{
				return this.service;
			}
		}

		// Token: 0x17001BBA RID: 7098
		// (get) Token: 0x06005E3D RID: 24125 RVA: 0x00145710 File Offset: 0x00143910
		public IdentifierCubeExpression Identifier
		{
			get
			{
				return new IdentifierCubeExpression("Cube");
			}
		}

		// Token: 0x17001BBB RID: 7099
		// (get) Token: 0x06005E3E RID: 24126 RVA: 0x0014571C File Offset: 0x0014391C
		public IResource Resource
		{
			get
			{
				return this.service.Resource;
			}
		}

		// Token: 0x17001BBC RID: 7100
		// (get) Token: 0x06005E3F RID: 24127 RVA: 0x00145729 File Offset: 0x00143929
		public IDictionary<QualifiedName, CdpaDimension> Dimensions
		{
			get
			{
				return this.dimensions;
			}
		}

		// Token: 0x17001BBD RID: 7101
		// (get) Token: 0x06005E40 RID: 24128 RVA: 0x00145731 File Offset: 0x00143931
		public IDictionary<QualifiedName, CdpaDimensionAttribute> Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x17001BBE RID: 7102
		// (get) Token: 0x06005E41 RID: 24129 RVA: 0x00145739 File Offset: 0x00143939
		public IDictionary<CdpaDimensionAttribute, IList<CdpaHierarchyLevel>> AttributeLevels
		{
			get
			{
				return this.attributeLevels;
			}
		}

		// Token: 0x17001BBF RID: 7103
		// (get) Token: 0x06005E42 RID: 24130 RVA: 0x00145741 File Offset: 0x00143941
		public IDictionary<QualifiedName, CdpaMeasure> Measures
		{
			get
			{
				return this.measures;
			}
		}

		// Token: 0x17001BC0 RID: 7104
		// (get) Token: 0x06005E43 RID: 24131 RVA: 0x00145749 File Offset: 0x00143949
		public IDictionary<QualifiedName, CdpaParameter> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x06005E44 RID: 24132 RVA: 0x00145754 File Offset: 0x00143954
		public bool TryGetObject(IdentifierCubeExpression identifier, out ICubeObject obj)
		{
			CdpaDimensionAttribute cdpaDimensionAttribute;
			if (this.attributes.TryGetValue(QualifiedName.From(identifier), out cdpaDimensionAttribute))
			{
				obj = cdpaDimensionAttribute;
				return true;
			}
			CdpaMeasure cdpaMeasure;
			if (this.measures.TryGetValue(QualifiedName.From(identifier), out cdpaMeasure) || this.dynamicMeasures.TryGetValue(QualifiedName.From(identifier), out cdpaMeasure))
			{
				obj = cdpaMeasure;
				return true;
			}
			if (MeasureValue.Count.Measure.Equals(identifier))
			{
				obj = this.rowCountMeasure;
				return true;
			}
			obj = null;
			return false;
		}

		// Token: 0x06005E45 RID: 24133 RVA: 0x001457CC File Offset: 0x001439CC
		public bool TryCreateMeasure(CubeExpression expression, out ICubeMeasure measure)
		{
			try
			{
				expression = this.functionCubeExpressionNormalizer.NormalizeFunctions(expression);
				string text = this.NextId();
				QualifiedName qualifiedName = this.dynamicMeasuresDimensionName.Qualify(text);
				string text2 = "(measure " + text + ")";
				CdpaMeasure measure2 = this.GetMeasure(qualifiedName, text2, TypeValue.Any, expression);
				this.dynamicMeasures.Add(measure2.QualifiedName, measure2);
				measure = measure2;
				return true;
			}
			catch (NotSupportedException)
			{
			}
			measure = null;
			return false;
		}

		// Token: 0x06005E46 RID: 24134 RVA: 0x00145850 File Offset: 0x00143A50
		public bool IsIndependent(ICubeLevel level)
		{
			if (level is CdpaSignalDimensionAttribute)
			{
				return true;
			}
			CdpaHierarchyLevel cdpaHierarchyLevel = level as CdpaHierarchyLevel;
			if (cdpaHierarchyLevel != null)
			{
				return this.IsIndependent(cdpaHierarchyLevel.Attribute);
			}
			CdpaRelatedDimensionAttribute cdpaRelatedDimensionAttribute = level as CdpaRelatedDimensionAttribute;
			return cdpaRelatedDimensionAttribute != null && cdpaRelatedDimensionAttribute.RelatedAttributes.Any((CdpaDimensionAttribute a) => this.IsIndependent(a));
		}

		// Token: 0x06005E47 RID: 24135 RVA: 0x001458A4 File Offset: 0x00143AA4
		public bool AreRelated(ICubeLevel level1, ICubeLevel level2)
		{
			if (level1.Equals(level2))
			{
				return true;
			}
			CdpaRelatedDimensionAttribute cdpaRelatedDimensionAttribute = level1 as CdpaRelatedDimensionAttribute;
			CdpaDimensionAttribute cdpaDimensionAttribute = level2 as CdpaDimensionAttribute;
			if (cdpaRelatedDimensionAttribute != null && cdpaRelatedDimensionAttribute.RelatedAttributes.Contains(cdpaDimensionAttribute))
			{
				return true;
			}
			CdpaRelatedDimensionAttribute cdpaRelatedDimensionAttribute2 = level2 as CdpaRelatedDimensionAttribute;
			CdpaDimensionAttribute cdpaDimensionAttribute2 = level1 as CdpaDimensionAttribute;
			return cdpaRelatedDimensionAttribute2 != null && cdpaRelatedDimensionAttribute2.RelatedAttributes.Contains(cdpaDimensionAttribute2);
		}

		// Token: 0x06005E48 RID: 24136 RVA: 0x001458FF File Offset: 0x00143AFF
		public bool TryGetTimePart(CubeExpression expression, out FunctionValue partFunction, out IdentifierCubeExpression timestampAttribute)
		{
			expression = this.attributeInliner.InlineAttributes(expression);
			return this.TryGetDateTimePart(expression, out partFunction, out timestampAttribute);
		}

		// Token: 0x06005E49 RID: 24137 RVA: 0x00145918 File Offset: 0x00143B18
		public bool TryGetFinestTimePart(CubeExpression expression, out FunctionValue finestPartFunction, out IdentifierCubeExpression timestampAttribute)
		{
			expression = this.attributeInliner.InlineAttributes(expression);
			return this.TryGetFinestDateTimePart(expression, out finestPartFunction, out timestampAttribute);
		}

		// Token: 0x06005E4A RID: 24138 RVA: 0x00145931 File Offset: 0x00143B31
		public bool TryGetTimeGranularity(CdpaDimensionAttribute attribute, out IdentifierCubeExpression timestampAttribute, out ITimeGranularity timeGranularity)
		{
			return this.TryGetTimeGranularity(attribute.QualifiedName.ToExpression(), out timestampAttribute, out timeGranularity);
		}

		// Token: 0x06005E4B RID: 24139 RVA: 0x00145948 File Offset: 0x00143B48
		public bool TryGetTimeGranularity(CubeExpression expression, out IdentifierCubeExpression timestampAttribute, out ITimeGranularity timeGranularity)
		{
			expression = this.attributeInliner.InlineAttributes(expression);
			FunctionValue functionValue;
			short num;
			Func<short, TimePartsTimeGranularity> func;
			if (this.TryGetDateTimePartGranularity(expression, out timestampAttribute, out functionValue, out num) && CdpaCube.partGranularityCtors.TryGetValue(functionValue, out func))
			{
				timeGranularity = func(num);
				return timeGranularity != null;
			}
			if (this.TryGetDateTimeGranularity(expression, out timestampAttribute, out timeGranularity))
			{
				return true;
			}
			if (expression.TryGetIdentifier(out timestampAttribute) && this.IsTimestampAttribute(timestampAttribute))
			{
				timeGranularity = TimePartsTimeGranularity.Finest;
				return true;
			}
			timestampAttribute = null;
			timeGranularity = null;
			return false;
		}

		// Token: 0x06005E4C RID: 24140 RVA: 0x001459BF File Offset: 0x00143BBF
		public bool IsTimestampAttribute(IdentifierCubeExpression identifier)
		{
			return this.timestampAttributes.Contains(QualifiedName.From(identifier));
		}

		// Token: 0x06005E4D RID: 24141 RVA: 0x001459D2 File Offset: 0x00143BD2
		public void AddTimestampAttribute(CdpaDimensionAttribute attribute)
		{
			this.timestampAttributes.Add(attribute.QualifiedName);
		}

		// Token: 0x06005E4E RID: 24142 RVA: 0x001459E8 File Offset: 0x00143BE8
		public Value TransformTypeInvariant(TypeValue targetType, Value fromValue)
		{
			return this.invariantTypeTransforms.GetFunctionValueFromType(targetType, targetType, false).Invoke(fromValue);
		}

		// Token: 0x06005E4F RID: 24143 RVA: 0x00145A0C File Offset: 0x00143C0C
		public static TypeValue GetMType(string tomType)
		{
			string text = tomType.ToUpperInvariant();
			if (text == "BOOLEAN")
			{
				return TypeValue.Logical;
			}
			if (text == "STRING")
			{
				return TypeValue.Text;
			}
			if (text == "INT64")
			{
				return TypeValue.Int64;
			}
			if (text == "DOUBLE")
			{
				return TypeValue.Double;
			}
			if (text == "DECIMAL")
			{
				return TypeValue.Decimal;
			}
			if (!(text == "DATETIME"))
			{
				return TypeValue.Any;
			}
			return TypeValue.DateTime;
		}

		// Token: 0x06005E50 RID: 24144 RVA: 0x00145A9C File Offset: 0x00143C9C
		private void PopulateCube()
		{
			Value value = this.service.ExecuteMetadataQuery();
			Value value2;
			if (value.TryGetValue("parameters", out value2))
			{
				foreach (IValueReference valueReference in value2.AsList)
				{
					RecordValue asRecord = valueReference.Value.AsRecord;
					TypeValue mtype = CdpaCube.GetMType(asRecord["type"].AsString);
					Value value3 = CdpaCube.GetValueOrDefault(asRecord, "default", null);
					if (value3 != null)
					{
						value3 = this.TransformTypeInvariant(mtype, value3);
					}
					List<Value> list = null;
					Value valueOrDefault = CdpaCube.GetValueOrDefault(asRecord, "availableValues", null);
					if (valueOrDefault != null)
					{
						list = new List<Value>();
						foreach (IValueReference valueReference2 in valueOrDefault.AsList)
						{
							list.Add(this.TransformTypeInvariant(mtype, valueReference2.Value));
						}
					}
					CdpaParameter cdpaParameter = new CdpaParameter(this, asRecord["name"].AsString, asRecord["name"].AsString, mtype, CdpaCube.GetValueOrDefault(asRecord, "required", LogicalValue.False).AsBoolean, list, value3);
					this.parameters.Add(cdpaParameter.QualifiedName, cdpaParameter);
				}
			}
			foreach (IValueReference valueReference3 in value["signalTables"].AsList)
			{
				RecordValue asRecord2 = valueReference3.Value.AsRecord;
				if (!asRecord2.Keys.Contains("signalTableNames"))
				{
					CdpaDimension cdpaDimension = new CdpaSignalDimension(this, asRecord2);
					this.AddDimensionAndAttributes(cdpaDimension);
				}
			}
			foreach (IValueReference valueReference4 in value["dimensionTables"].AsList)
			{
				RecordValue asRecord3 = valueReference4.Value.AsRecord;
				CdpaDimension cdpaDimension2 = new CdpaVirtualDimension(this, asRecord3);
				this.AddDimensionAndAttributes(cdpaDimension2);
			}
			CdpaRelationships cdpaRelationships = new CdpaRelationships(value["relationships"].AsList);
			foreach (CdpaDimensionAttribute cdpaDimensionAttribute in this.attributes.Values)
			{
				CdpaRelatedDimensionAttribute cdpaRelatedDimensionAttribute = cdpaDimensionAttribute as CdpaRelatedDimensionAttribute;
				if (cdpaRelatedDimensionAttribute != null)
				{
					foreach (QualifiedName qualifiedName in cdpaRelationships.GetRelatedAttributes(cdpaRelatedDimensionAttribute.QualifiedName))
					{
						CdpaDimensionAttribute cdpaDimensionAttribute2;
						if (this.attributes.TryGetValue(qualifiedName, out cdpaDimensionAttribute2))
						{
							cdpaRelatedDimensionAttribute.RelatedAttributes.Add(cdpaDimensionAttribute2);
						}
					}
				}
			}
			foreach (QualifiedName qualifiedName2 in this.timestampAttributes.ToArray<QualifiedName>())
			{
				foreach (QualifiedName qualifiedName3 in cdpaRelationships.GetRelatedAttributes(qualifiedName2))
				{
					this.timestampAttributes.Add(qualifiedName3);
				}
			}
			Value value4;
			if (value.TryGetValue("measures", out value4))
			{
				foreach (IValueReference valueReference5 in value4.AsList)
				{
					RecordValue asRecord4 = valueReference5.Value.AsRecord;
					QualifiedName qualifiedName4 = this.measuresDimensionName.Qualify(asRecord4["name"].AsString);
					string asString = asRecord4["name"].AsString;
					string asString2 = asRecord4["mExpression"].AsString;
					Value value5;
					if (!asRecord4.TryGetValue("type", out value5) || !value5.IsText)
					{
						value5 = TextValue.New("any");
					}
					CdpaMeasure cdpaMeasure = this.GetMeasure(qualifiedName4, asString, CdpaCube.GetMType(value5.AsString).Nullable, asString2);
					Value value6;
					Value value7;
					if (asRecord4.TryGetValue("topCount", out value6) && asRecord4.TryGetValue("topOrder", out value7))
					{
						long asInteger = value6.AsNumber.AsInteger64;
						bool flag = value7.AsString == "ascending";
						cdpaMeasure = new OrderByTopCdpaMeasure(cdpaMeasure, asInteger, flag);
					}
					this.measures.Add(cdpaMeasure.QualifiedName, cdpaMeasure);
				}
			}
			this.rowCountMeasure = new CdpaRowCountMeasure(this, QualifiedName.From(MeasureValue.Count.Measure), "(row count)", this.dimensions.Values.ToArray<CdpaDimension>());
		}

		// Token: 0x06005E51 RID: 24145 RVA: 0x00145FF8 File Offset: 0x001441F8
		private static Value GetValueOrDefault(RecordValue record, string field, Value defaultValue)
		{
			Value value;
			if (!record.TryGetValue(field, out value))
			{
				value = defaultValue;
			}
			return value;
		}

		// Token: 0x06005E52 RID: 24146 RVA: 0x00146014 File Offset: 0x00144214
		private void AddDimensionAndAttributes(CdpaDimension dimension)
		{
			this.dimensions.Add(dimension.QualifiedName, dimension);
			foreach (CdpaDimensionAttribute cdpaDimensionAttribute in dimension.Attributes.Values)
			{
				this.attributes.Add(cdpaDimensionAttribute.QualifiedName, cdpaDimensionAttribute);
			}
		}

		// Token: 0x06005E53 RID: 24147 RVA: 0x00146084 File Offset: 0x00144284
		private string NextId()
		{
			this.nextId++;
			return this.nextId.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06005E54 RID: 24148 RVA: 0x001460A4 File Offset: 0x001442A4
		public static RecordTypeValue GetRecordType(IEnumerable<CdpaDimensionAttribute> attributes)
		{
			RecordBuilder recordBuilder = new RecordBuilder(8);
			foreach (CdpaDimensionAttribute cdpaDimensionAttribute in attributes)
			{
				RecordValue recordValue = RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
				{
					cdpaDimensionAttribute.Type,
					LogicalValue.False
				});
				recordBuilder.Add(cdpaDimensionAttribute.QualifiedName.AsString, recordValue, TypeValue.Record);
			}
			return RecordTypeValue.New(recordBuilder.ToRecord(), false);
		}

		// Token: 0x06005E55 RID: 24149 RVA: 0x00146134 File Offset: 0x00144334
		public CubeExpression GetCubeExpression(RecordTypeValue rowType, string expression, QualifiedName prefix = null)
		{
			IFunctionExpression functionExpression = this.Compile(expression);
			if (prefix != null)
			{
				functionExpression = new CdpaCube.PrefixColumnNamesVisitor(prefix).PrefixColumnNames(functionExpression);
			}
			QueryExpression queryExpression;
			if (!QueryExpressionBuilder.TryToQueryExpression(rowType, functionExpression, out queryExpression))
			{
				throw new NotSupportedException();
			}
			CubeExpression cubeExpression = new QueryExpressionToCubeExpressionVisitor(rowType.AsRecordType.Fields.Keys.Select((string k) => new IdentifierCubeExpression(k)).ToArray<IdentifierCubeExpression>()).Visit(queryExpression);
			return this.functionCubeExpressionNormalizer.NormalizeFunctions(cubeExpression);
		}

		// Token: 0x06005E56 RID: 24150 RVA: 0x001461C0 File Offset: 0x001443C0
		private bool TryGetDateTimeGranularity(CubeExpression expression, out IdentifierCubeExpression timestampAttribute, out ITimeGranularity timeGranularity)
		{
			timestampAttribute = null;
			Value value = NumberValue.New(CdpaTimeRange.UtcEarliest.Year);
			IList<CubeExpression> list;
			if (expression.TryGetInvocation(Library.DateTime.datetime, 6, out list))
			{
				short num;
				short num2;
				short num3;
				short num4;
				short num5;
				short num6;
				if (this.TryGetDateTimePartGranularityOrConstant(list[0], Library.Date.Year, value, ref timestampAttribute, out num) && this.TryGetDateTimePartGranularityOrOne(list[1], Library.Date.Month, ref timestampAttribute, out num2) && this.TryGetDateTimePartGranularityOrOne(list[2], Library.Date.Day, ref timestampAttribute, out num3) && this.TryGetDateTimePartGranularityOrZero(list[3], Library.Time.Hour, ref timestampAttribute, out num4) && this.TryGetDateTimePartGranularityOrZero(list[4], Library.Time.Minute, ref timestampAttribute, out num5) && this.TryGetDateTimePartGranularityOrZero(list[5], Library.Time.Second, ref timestampAttribute, out num6))
				{
					timeGranularity = new TimePartsTimeGranularity
					{
						Years = num,
						Months = num2,
						Days = num3,
						Hours = num4,
						Minutes = num5,
						Seconds = num6
					};
					DateTime utcNow = DateTime.UtcNow;
					DateTime dateTime = new DateTime((int)CdpaCube.GetValueOrEveryOrCoarsest(num, (short)utcNow.Year, 1), (int)CdpaCube.GetValueOrEveryOrCoarsest(num2, (short)utcNow.Month, 1), (int)CdpaCube.GetValueOrEveryOrCoarsest(num3, (short)utcNow.Day, 1), (int)CdpaCube.GetValueOrEveryOrCoarsest(num4, (short)utcNow.Hour, 0), (int)CdpaCube.GetValueOrEveryOrCoarsest(num5, (short)utcNow.Minute, 0), (int)CdpaCube.GetValueOrEveryOrCoarsest(num6, (short)utcNow.Second, 0), DateTimeKind.Utc);
					if (utcNow - dateTime >= CdpaSetContextProvider.ResolutionAsTimeSpan)
					{
						timeGranularity = new AnchoredTimeGranularity
						{
							Anchor = dateTime,
							Granularity = timeGranularity
						};
					}
					return true;
				}
				short num7 = -1;
				FunctionValue functionValue;
				CubeExpression cubeExpression;
				short num8;
				if (this.TryGetDateTimePart(list[0], out functionValue, out cubeExpression) && Library.Date.Year.Equals(functionValue) && this.TryGetDayOfWeek(cubeExpression, ref timestampAttribute, ref num7) && this.TryGetDateTimePart(list[1], out functionValue, out cubeExpression) && Library.Date.Month.Equals(functionValue) && this.TryGetDayOfWeek(cubeExpression, ref timestampAttribute, ref num7) && this.TryGetDateTimePart(list[2], out functionValue, out cubeExpression) && Library.Date.Day.Equals(functionValue) && this.TryGetDayOfWeek(cubeExpression, ref timestampAttribute, ref num7) && this.TryGetDateTimePartGranularityOrZero(list[3], Library.Time.Hour, ref timestampAttribute, out num8) && num8 == TimePartsTimeGranularity.CoarsestPart && this.TryGetDateTimePartGranularityOrZero(list[4], Library.Time.Minute, ref timestampAttribute, out num8) && num8 == TimePartsTimeGranularity.CoarsestPart && this.TryGetDateTimePartGranularityOrZero(list[5], Library.Time.Second, ref timestampAttribute, out num8) && num8 == TimePartsTimeGranularity.CoarsestPart)
				{
					DateTime dateTime2 = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, 0, 0, 0, DateTimeKind.Utc);
					dateTime2 = dateTime2.AddDays((double)((DayOfWeek)num7 - dateTime2.DayOfWeek));
					timeGranularity = new AnchoredTimeGranularity
					{
						Anchor = dateTime2,
						Granularity = MetricGranularityTimeGranularity.P7D
					};
					return true;
				}
			}
			timeGranularity = null;
			return false;
		}

		// Token: 0x06005E57 RID: 24151 RVA: 0x001464EB File Offset: 0x001446EB
		private static short GetValueOrEveryOrCoarsest(short part, short every, short coarsest)
		{
			if (part == TimePartsTimeGranularity.CoarsestPart)
			{
				return coarsest;
			}
			if (part != 1)
			{
				return part;
			}
			return every;
		}

		// Token: 0x06005E58 RID: 24152 RVA: 0x001464FE File Offset: 0x001446FE
		private bool TryGetDateTimePartGranularityOrZero(CubeExpression expression, FunctionValue partFunction, ref IdentifierCubeExpression timestampAttribute, out short partGranularity)
		{
			return this.TryGetDateTimePartGranularityOrConstant(expression, partFunction, NumberValue.Zero, ref timestampAttribute, out partGranularity);
		}

		// Token: 0x06005E59 RID: 24153 RVA: 0x00146510 File Offset: 0x00144710
		private bool TryGetDateTimePartGranularityOrOne(CubeExpression expression, FunctionValue partFunction, ref IdentifierCubeExpression timestampAttribute, out short partGranularity)
		{
			return this.TryGetDateTimePartGranularityOrConstant(expression, partFunction, NumberValue.One, ref timestampAttribute, out partGranularity);
		}

		// Token: 0x06005E5A RID: 24154 RVA: 0x00146524 File Offset: 0x00144724
		private bool TryGetDateTimePartGranularityOrConstant(CubeExpression expression, FunctionValue partFunction, Value allowedConstant, ref IdentifierCubeExpression timestampAttribute, out short partGranularity)
		{
			IdentifierCubeExpression identifierCubeExpression;
			FunctionValue functionValue;
			if (this.TryGetDateTimePartGranularity(expression, out identifierCubeExpression, out functionValue, out partGranularity) && functionValue.Equals(partFunction) && (timestampAttribute == null || timestampAttribute.Equals(identifierCubeExpression)))
			{
				timestampAttribute = identifierCubeExpression;
				return true;
			}
			Value value;
			if (expression.TryGetConstant(out value) && value.Equals(allowedConstant))
			{
				partGranularity = TimePartsTimeGranularity.CoarsestPart;
				return true;
			}
			partGranularity = 0;
			return false;
		}

		// Token: 0x06005E5B RID: 24155 RVA: 0x00146584 File Offset: 0x00144784
		private bool TryGetDateTimePartGranularity(CubeExpression expression, out IdentifierCubeExpression timestampAttribute, out FunctionValue partFunction, out short granularity)
		{
			CubeExpression cubeExpression;
			CubeExpression cubeExpression2;
			Value value;
			IList<CubeExpression> list;
			CubeExpression cubeExpression3;
			CubeExpression cubeExpression4;
			Value value2;
			if (expression.TryGetBinary(BinaryOperator2.Multiply, out cubeExpression, out cubeExpression2) && cubeExpression2.TryGetConstant(out value) && cubeExpression.TryGetInvocation(Library.Number.RoundDown, 1, out list) && list[0].TryGetBinary(BinaryOperator2.Divide, out cubeExpression3, out cubeExpression4) && cubeExpression4.TryGetConstant(out value2) && this.TryGetDateTimePart(cubeExpression3, out partFunction, out timestampAttribute) && value.Equals(value2) && value.IsNumber)
			{
				granularity = (short)value.AsNumber.AsInteger64;
				return true;
			}
			if (this.TryGetDateTimePart(expression, out partFunction, out timestampAttribute))
			{
				granularity = 1;
				return true;
			}
			timestampAttribute = null;
			partFunction = null;
			granularity = 0;
			return false;
		}

		// Token: 0x06005E5C RID: 24156 RVA: 0x00146624 File Offset: 0x00144824
		private bool TryGetDateTimePart(CubeExpression expression, out FunctionValue partFunction, out IdentifierCubeExpression timestampAttribute)
		{
			CubeExpression cubeExpression;
			if (this.TryGetDateTimePart(expression, out partFunction, out cubeExpression) && cubeExpression.TryGetIdentifier(out timestampAttribute) && this.IsTimestampAttribute(timestampAttribute))
			{
				return true;
			}
			partFunction = null;
			timestampAttribute = null;
			return false;
		}

		// Token: 0x06005E5D RID: 24157 RVA: 0x0014665C File Offset: 0x0014485C
		private bool TryGetDateTimePart(CubeExpression expression, out FunctionValue partFunction, out CubeExpression arg)
		{
			InvocationCubeExpression invocationCubeExpression = expression as InvocationCubeExpression;
			Value value;
			if (invocationCubeExpression != null && invocationCubeExpression.Function.TryGetConstant(out value) && value.IsFunction && invocationCubeExpression.Arguments.Count == 1)
			{
				partFunction = value.AsFunction;
				if (CdpaCube.partGranularityCtors.ContainsKey(partFunction))
				{
					arg = invocationCubeExpression.Arguments[0];
					return true;
				}
			}
			partFunction = null;
			arg = null;
			return false;
		}

		// Token: 0x06005E5E RID: 24158 RVA: 0x001466C8 File Offset: 0x001448C8
		private bool TryGetFinestDateTimePart(CubeExpression expression, out FunctionValue finestPartFunction, out IdentifierCubeExpression timestampAttribute)
		{
			CubeExpression cubeExpression;
			if (this.TryGetFinestDateTimePart(expression, out finestPartFunction, out cubeExpression) && cubeExpression.TryGetIdentifier(out timestampAttribute) && this.IsTimestampAttribute(timestampAttribute))
			{
				return true;
			}
			finestPartFunction = null;
			timestampAttribute = null;
			return false;
		}

		// Token: 0x06005E5F RID: 24159 RVA: 0x00146700 File Offset: 0x00144900
		private bool TryGetFinestDateTimePart(CubeExpression expression, out FunctionValue finestPartFunction, out CubeExpression arg)
		{
			finestPartFunction = null;
			arg = null;
			InvocationCubeExpression invocationCubeExpression = expression as InvocationCubeExpression;
			Value value;
			if (invocationCubeExpression != null && invocationCubeExpression.Function.TryGetConstant(out value) && value.Equals(Library.DateTime.datetime) && invocationCubeExpression.Arguments.Count == 6)
			{
				bool flag = false;
				for (int i = 0; i < CdpaQueryGenerator.partFunctions.Length; i++)
				{
					CubeExpression cubeExpression = invocationCubeExpression.Arguments[i];
					FunctionValue functionValue;
					CubeExpression cubeExpression2;
					if (!flag && this.TryGetDateTimePart(cubeExpression, out functionValue, out cubeExpression2) && functionValue.Equals(CdpaQueryGenerator.partFunctions[i]) && (arg == null || cubeExpression2.Equals(arg)))
					{
						arg = cubeExpression2;
						finestPartFunction = functionValue;
					}
					else
					{
						Value value2;
						if (!cubeExpression.TryGetConstant(out value2) || ((i > 2 || !value2.Equals(NumberValue.One)) && !value2.Equals(NumberValue.Zero)))
						{
							finestPartFunction = null;
							arg = null;
							break;
						}
						flag = true;
					}
				}
			}
			return finestPartFunction != null && arg != null;
		}

		// Token: 0x06005E60 RID: 24160 RVA: 0x001467F8 File Offset: 0x001449F8
		private bool TryGetDayOfWeek(CubeExpression expression, ref IdentifierCubeExpression timestampAttribute, ref short dayOfWeek)
		{
			InvocationCubeExpression invocationCubeExpression = expression as InvocationCubeExpression;
			Value value;
			IdentifierCubeExpression identifierCubeExpression;
			if (invocationCubeExpression != null && invocationCubeExpression.Function.TryGetConstant(out value) && CultureSpecificFunction.DateStartOfWeek.Equals(value.AsFunction) && invocationCubeExpression.Arguments.Count >= 1 && invocationCubeExpression.Arguments[0].TryGetIdentifier(out identifierCubeExpression) && this.IsTimestampAttribute(identifierCubeExpression) && (timestampAttribute == null || timestampAttribute.Equals(identifierCubeExpression)))
			{
				short num = -1;
				Value value2;
				if (invocationCubeExpression.Arguments.Count == 1)
				{
					num = 0;
				}
				else if (invocationCubeExpression.Arguments.Count == 2 && invocationCubeExpression.Arguments[1].TryGetConstant(out value2) && value2.IsNumber)
				{
					NumberValue asNumber = value2.AsNumber;
					if (asNumber.IsInteger64)
					{
						long asInteger = asNumber.AsInteger64;
						if (asInteger >= 0L && asInteger < 7L)
						{
							num = (short)asInteger;
						}
					}
				}
				if (num != -1 && (dayOfWeek == -1 || num == dayOfWeek))
				{
					timestampAttribute = identifierCubeExpression;
					dayOfWeek = num;
					return true;
				}
			}
			timestampAttribute = null;
			dayOfWeek = -1;
			return false;
		}

		// Token: 0x06005E61 RID: 24161 RVA: 0x0014690C File Offset: 0x00144B0C
		private CdpaMeasure GetMeasure(QualifiedName qualifiedName, string caption, TypeValue type, CubeExpression expression)
		{
			FunctionValue functionValue;
			IList<CubeExpression> list;
			IdentifierCubeExpression identifierCubeExpression;
			ICubeObject cubeObject;
			if (expression.TryGetInvocation(out functionValue, out list) && list.Count == 1 && list[0].TryGetIdentifier(out identifierCubeExpression) && this.TryGetObject(identifierCubeExpression, out cubeObject))
			{
				CdpaDimensionAttribute cdpaDimensionAttribute = cubeObject as CdpaDimensionAttribute;
				if (cdpaDimensionAttribute != null)
				{
					return this.GetImplicitMeasure(qualifiedName, caption, type, functionValue, cdpaDimensionAttribute);
				}
			}
			throw new NotSupportedException();
		}

		// Token: 0x06005E62 RID: 24162 RVA: 0x00146968 File Offset: 0x00144B68
		private CdpaMeasure GetMeasure(QualifiedName qualifiedName, string caption, TypeValue type, string expression)
		{
			IFunctionExpression functionExpression = this.Compile(expression);
			Dictionary<string, IExpression> dictionary;
			Identifier identifier;
			Value value;
			IExpression expression2;
			if (CdpaCube.invocationPattern.TryMatch(functionExpression, out dictionary) && dictionary.TryGetIdentifier("cube", out identifier) && dictionary.TryGetConstant("func", out value) && value.IsFunction && dictionary.TryGetValue("arg0", out expression2))
			{
				Identifier identifier2 = null;
				Dictionary<string, IExpression> dictionary2;
				IExpression expression3;
				if (!CdpaCube.dimensionPattern.TryMatch(expression2, out dictionary2) && (!expression2.TryGetFieldAccess(out expression3, out identifier2) || !CdpaCube.dimensionPattern.TryMatch(expression3, out dictionary2)))
				{
					identifier2 = null;
					dictionary2 = null;
				}
				Identifier identifier3;
				Value value2;
				Value value3;
				if (dictionary2 != null && dictionary2.TryGetIdentifier("cube", out identifier3) && identifier.Name == identifier3.Name && dictionary2.TryGetConstant("dimensionRecord", out value2) && value2.IsRecord && value2.AsRecord.TryGetValue("Id", out value3) && value3.IsText)
				{
					QualifiedName qualifiedName2 = QualifiedName.New(value3.AsString);
					CdpaDimension cdpaDimension;
					if (this.dimensions.TryGetValue(qualifiedName2, out cdpaDimension))
					{
						if (!(identifier2 != null))
						{
							return new CdpaRowCountMeasure(this, qualifiedName, caption, new CdpaDimension[] { cdpaDimension });
						}
						string text = identifier2.Name;
						if (text.StartsWith(cdpaDimension.QualifiedName.AsString + ".", StringComparison.Ordinal))
						{
							text = text.Substring(cdpaDimension.QualifiedName.AsString.Length + 1);
						}
						QualifiedName qualifiedName3 = qualifiedName2.Qualify(text);
						CdpaDimensionAttribute cdpaDimensionAttribute;
						if (this.attributes.TryGetValue(qualifiedName3, out cdpaDimensionAttribute) && cdpaDimensionAttribute.Dimension.Equals(cdpaDimension))
						{
							return this.GetImplicitMeasure(qualifiedName, caption, type, value.AsFunction, cdpaDimensionAttribute);
						}
					}
				}
			}
			throw new NotSupportedException();
		}

		// Token: 0x06005E63 RID: 24163 RVA: 0x00146B48 File Offset: 0x00144D48
		private CdpaMeasure GetImplicitMeasure(QualifiedName qualifiedName, string caption, TypeValue type, FunctionValue function, CdpaDimensionAttribute attribute)
		{
			if (CdpaQueryGenerator.operationCtors.ContainsKey(function))
			{
				CubeExpression cubeExpression = new InvocationCubeExpression(new ConstantCubeExpression(function), new CubeExpression[] { attribute.QualifiedName.ToExpression() });
				return new CdpaProjectedMeasure(this, qualifiedName, caption, type, cubeExpression);
			}
			throw new NotSupportedException();
		}

		// Token: 0x06005E64 RID: 24164 RVA: 0x00146B98 File Offset: 0x00144D98
		private IFunctionExpression Compile(string expression)
		{
			ITokens tokens = Engines.Version1.Tokenize(expression);
			IFunctionExpression functionExpression = LanguageLibrary.Evaluate(((IExpressionDocument)Engines.Version1.Parse(tokens, new TextDocumentHost(expression), delegate(IError e)
			{
			})).Expression, CdpaCube.libraryForExpressions, EmptyArray<Module>.Instance).Expression as IFunctionExpression;
			if (functionExpression != null)
			{
				functionExpression = NormalizationVisitor.Normalize(functionExpression, true) as IFunctionExpression;
			}
			if (functionExpression != null)
			{
				functionExpression = this.functionExpressionNormalizer.NormalizeFunctions(functionExpression) as IFunctionExpression;
			}
			if (functionExpression != null)
			{
				return functionExpression;
			}
			throw new NotSupportedException();
		}

		// Token: 0x040033D5 RID: 13269
		private static readonly Dictionary<FunctionValue, Func<short, TimePartsTimeGranularity>> partGranularityCtors = new Dictionary<FunctionValue, Func<short, TimePartsTimeGranularity>>
		{
			{
				Library.Date.Year,
				(short g) => new TimePartsTimeGranularity
				{
					Years = g
				}
			},
			{
				Library.Date.Month,
				(short g) => new TimePartsTimeGranularity
				{
					Months = g
				}
			},
			{
				Library.Date.Day,
				(short g) => new TimePartsTimeGranularity
				{
					Days = g
				}
			},
			{
				Library.Time.Hour,
				(short g) => new TimePartsTimeGranularity
				{
					Hours = g
				}
			},
			{
				Library.Time.Minute,
				(short g) => new TimePartsTimeGranularity
				{
					Minutes = g
				}
			},
			{
				Library.Time.Second,
				(short g) => new TimePartsTimeGranularity
				{
					Seconds = g
				}
			}
		};

		// Token: 0x040033D6 RID: 13270
		private static readonly RecordValue libraryForExpressions = LanguageLibrary.LinkLibrary(EngineHost.Empty, new Module[] { Modules.All });

		// Token: 0x040033D7 RID: 13271
		private readonly CdpaService service;

		// Token: 0x040033D8 RID: 13272
		private readonly string dynamicPrefix;

		// Token: 0x040033D9 RID: 13273
		private readonly QualifiedName measuresDimensionName;

		// Token: 0x040033DA RID: 13274
		private readonly QualifiedName dynamicMeasuresDimensionName;

		// Token: 0x040033DB RID: 13275
		private readonly QualifiedName dynamicAttributesDimensionName;

		// Token: 0x040033DC RID: 13276
		private readonly CdpaCube.NormalizeFunctionsCubeExpressionVisitor functionCubeExpressionNormalizer;

		// Token: 0x040033DD RID: 13277
		private readonly CdpaCube.NormalizeFunctionsExpressionVisitor functionExpressionNormalizer;

		// Token: 0x040033DE RID: 13278
		private readonly InlineAttributesVisitor attributeInliner;

		// Token: 0x040033DF RID: 13279
		private readonly Dictionary<QualifiedName, CdpaDimension> dimensions;

		// Token: 0x040033E0 RID: 13280
		private readonly Dictionary<QualifiedName, CdpaDimensionAttribute> attributes;

		// Token: 0x040033E1 RID: 13281
		private readonly Dictionary<CdpaDimensionAttribute, IList<CdpaHierarchyLevel>> attributeLevels;

		// Token: 0x040033E2 RID: 13282
		private readonly CdpaDimension dynamicAttributes;

		// Token: 0x040033E3 RID: 13283
		private readonly Dictionary<QualifiedName, CdpaMeasure> measures;

		// Token: 0x040033E4 RID: 13284
		private readonly Dictionary<QualifiedName, CdpaMeasure> dynamicMeasures;

		// Token: 0x040033E5 RID: 13285
		private readonly Dictionary<QualifiedName, CdpaParameter> parameters;

		// Token: 0x040033E6 RID: 13286
		private readonly HashSet<QualifiedName> timestampAttributes;

		// Token: 0x040033E7 RID: 13287
		private readonly TransformTypesHelper invariantTypeTransforms;

		// Token: 0x040033E8 RID: 13288
		private CdpaRowCountMeasure rowCountMeasure;

		// Token: 0x040033E9 RID: 13289
		private int nextId;

		// Token: 0x040033EA RID: 13290
		private static ExpressionPattern invocationPattern = new ExpressionPattern(new string[] { "(__cube) => __func(__arg0)" });

		// Token: 0x040033EB RID: 13291
		private static ExpressionPattern dimensionPattern = new ExpressionPattern(new string[] { "Cube.Dimensions(__cube){__dimensionRecord}[Data]" });

		// Token: 0x02000D8C RID: 3468
		private sealed class NormalizeFunctionsCubeExpressionVisitor : CubeExpressionVisitor
		{
			// Token: 0x06005E67 RID: 24167 RVA: 0x00072ED2 File Offset: 0x000710D2
			public CubeExpression NormalizeFunctions(CubeExpression expression)
			{
				return this.Visit(expression);
			}

			// Token: 0x06005E68 RID: 24168 RVA: 0x00146D44 File Offset: 0x00144F44
			protected override CubeExpression VisitInvocation(InvocationCubeExpression invocation)
			{
				CubeExpression cubeExpression = base.VisitInvocation(invocation);
				IList<CubeExpression> list;
				IList<CubeExpression> list2;
				if (cubeExpression.TryGetInvocation(LanguageLibrary.List.Count, 1, out list) && list[0].TryGetInvocation(LanguageLibrary.List.Distinct, 1, out list2))
				{
					cubeExpression = new InvocationCubeExpression(new ConstantCubeExpression(Library.List.CountOfDistinct), list2.ToArray<CubeExpression>());
				}
				IList<CubeExpression> list3;
				Value value;
				if (cubeExpression.TryGetInvocation(Library.DateTimeZone.datetimezone, 8, out list3) && list3[6].TryGetConstant(out value) && value.Equals(NumberValue.Zero) && list3[7].TryGetConstant(out value) && value.Equals(NumberValue.Zero))
				{
					cubeExpression = new InvocationCubeExpression(new ConstantCubeExpression(Library.DateTime.datetime), list3.Take(6).ToArray<CubeExpression>());
				}
				return cubeExpression;
			}
		}

		// Token: 0x02000D8D RID: 3469
		private sealed class NormalizeFunctionsExpressionVisitor : LogicalAstVisitor<object>
		{
			// Token: 0x06005E6A RID: 24170 RVA: 0x00146DFF File Offset: 0x00144FFF
			public IExpression NormalizeFunctions(IExpression expression)
			{
				return this.VisitExpression(expression);
			}

			// Token: 0x06005E6B RID: 24171 RVA: 0x00146E08 File Offset: 0x00145008
			protected override IExpression VisitInvocation(IInvocationExpression invocation)
			{
				IExpression expression = base.VisitInvocation(invocation);
				IList<IExpression> list;
				IList<IExpression> list2;
				if (expression.TryGetInvocation(LanguageLibrary.List.Count, 1, out list) && list[0].TryGetInvocation(LanguageLibrary.List.Distinct, 1, out list2))
				{
					expression = new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(Library.List.CountOfDistinct), list2[0]);
				}
				IList<IExpression> list3;
				Value value;
				if (expression.TryGetInvocation(Library.DateTimeZone.datetimezone, 8, out list3) && list3[6].TryGetConstant(out value) && value.Equals(NumberValue.Zero) && list3[7].TryGetConstant(out value) && value.Equals(NumberValue.Zero))
				{
					expression = new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(Library.DateTime.datetime), list3.Take(6).ToArray<IExpression>());
				}
				return expression;
			}

			// Token: 0x06005E6C RID: 24172 RVA: 0x00146EC4 File Offset: 0x001450C4
			protected override IExpression VisitFunction(IFunctionExpression function)
			{
				return base.VisitFunction(function, new object[function.FunctionType.Parameters.Count]);
			}

			// Token: 0x06005E6D RID: 24173 RVA: 0x00146EE2 File Offset: 0x001450E2
			protected override IExpression VisitLet(ILetExpression let)
			{
				return base.VisitLet(let, new object[let.Variables.Count]);
			}

			// Token: 0x06005E6E RID: 24174 RVA: 0x00146EFB File Offset: 0x001450FB
			protected override IExpression VisitRecord(IRecordExpression record)
			{
				return base.VisitRecord(record, null, new object[record.Members.Count]);
			}

			// Token: 0x06005E6F RID: 24175 RVA: 0x00146F15 File Offset: 0x00145115
			protected override TryCatchExceptionCase VisitTryCatchExceptionCase(TryCatchExceptionCase tryCatchExceptionCase)
			{
				return base.VisitTryCatchExceptionCase(tryCatchExceptionCase, null);
			}

			// Token: 0x06005E70 RID: 24176 RVA: 0x00146F1F File Offset: 0x0014511F
			protected override ISection VisitModule(ISection module)
			{
				return base.VisitModule(module, new object[module.Members.Count]);
			}
		}

		// Token: 0x02000D8E RID: 3470
		private class PrefixColumnNamesVisitor : LogicalAstVisitor<bool>
		{
			// Token: 0x06005E72 RID: 24178 RVA: 0x00146F40 File Offset: 0x00145140
			public PrefixColumnNamesVisitor(QualifiedName prefix)
			{
				this.prefix = prefix;
			}

			// Token: 0x06005E73 RID: 24179 RVA: 0x00146F4F File Offset: 0x0014514F
			public IFunctionExpression PrefixColumnNames(IFunctionExpression function)
			{
				if (function.FunctionType.Parameters.Count != 1)
				{
					throw new NotSupportedException();
				}
				return base.VisitFunction(function, new bool[] { true });
			}

			// Token: 0x06005E74 RID: 24180 RVA: 0x00146F7C File Offset: 0x0014517C
			protected override IExpression VisitFieldAccess(IFieldAccessExpression fieldAccess)
			{
				fieldAccess = (IFieldAccessExpression)base.VisitFieldAccess(fieldAccess);
				IIdentifierExpression identifierExpression = fieldAccess.Expression as IIdentifierExpression;
				if (identifierExpression != null && base.Environment.GetValue(identifierExpression.Name, identifierExpression.IsInclusive))
				{
					Identifier identifier = Microsoft.Mashup.Engine.Interface.Identifier.New(this.prefix.Qualify(fieldAccess.MemberName.Name).AsString);
					if (fieldAccess.IsOptional)
					{
						fieldAccess = new OptionalFieldAccessExpressionSyntaxNode(fieldAccess.Expression, identifier);
					}
					else
					{
						fieldAccess = new RequiredFieldAccessExpressionSyntaxNode(fieldAccess.Expression, identifier);
					}
				}
				return fieldAccess;
			}

			// Token: 0x06005E75 RID: 24181 RVA: 0x00147007 File Offset: 0x00145207
			protected override IExpression VisitFunction(IFunctionExpression function)
			{
				return base.VisitFunction(function, new bool[function.FunctionType.Parameters.Count]);
			}

			// Token: 0x06005E76 RID: 24182 RVA: 0x00147025 File Offset: 0x00145225
			protected override IExpression VisitLet(ILetExpression let)
			{
				return base.VisitLet(let, new bool[let.Variables.Count]);
			}

			// Token: 0x06005E77 RID: 24183 RVA: 0x0014703E File Offset: 0x0014523E
			protected override IExpression VisitRecord(IRecordExpression record)
			{
				return base.VisitRecord(record, false, new bool[record.Members.Count]);
			}

			// Token: 0x06005E78 RID: 24184 RVA: 0x00147058 File Offset: 0x00145258
			protected override TryCatchExceptionCase VisitTryCatchExceptionCase(TryCatchExceptionCase tryCatchExceptionCase)
			{
				return base.VisitTryCatchExceptionCase(tryCatchExceptionCase, false);
			}

			// Token: 0x06005E79 RID: 24185 RVA: 0x00147062 File Offset: 0x00145262
			protected override ISection VisitModule(ISection module)
			{
				return base.VisitModule(module, new bool[module.Members.Count]);
			}

			// Token: 0x040033EC RID: 13292
			private readonly QualifiedName prefix;
		}
	}
}
