using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200006A RID: 106
	public sealed class KPI : MetadataObject
	{
		// Token: 0x0600059E RID: 1438 RVA: 0x0002AE1D File Offset: 0x0002901D
		public KPI()
		{
			this.InitBodyAndCollections(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x0002AE30 File Offset: 0x00029030
		internal KPI(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections(comparer);
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x0002AE40 File Offset: 0x00029040
		private void InitBodyAndCollections(IEqualityComparer<string> comparer)
		{
			this.body = new KPI.ObjectBody(this);
			this.body.Description = string.Empty;
			this.body.TargetDescription = string.Empty;
			this.body.TargetExpression = string.Empty;
			this.body.TargetFormatString = string.Empty;
			this.body.StatusGraphic = string.Empty;
			this.body.StatusDescription = string.Empty;
			this.body.StatusExpression = string.Empty;
			this.body.TrendGraphic = string.Empty;
			this.body.TrendDescription = string.Empty;
			this.body.TrendExpression = string.Empty;
			this._Annotations = new KPIAnnotationCollection(this, comparer);
			this._ExtendedProperties = new KPIExtendedPropertyCollection(this, comparer);
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x0002AF13 File Offset: 0x00029113
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.KPI;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x0002AF17 File Offset: 0x00029117
		// (set) Token: 0x060005A3 RID: 1443 RVA: 0x0002AF29 File Offset: 0x00029129
		public override MetadataObject Parent
		{
			get
			{
				return this.body.MeasureID.Object;
			}
			internal set
			{
				if (this.body.MeasureID.Object != value)
				{
					MetadataObject.UpdateMetadataObjectParent<KPI, Measure>(this.body.MeasureID, (Measure)value, "KPI", null);
				}
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x0002AF5A File Offset: 0x0002915A
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.MeasureID.ObjectID;
			}
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0002AF6C File Offset: 0x0002916C
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.KPI, null, "KPI object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
				{
					writer.WriteProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, typeof(string));
				}
				if (writer.ShouldIncludeProperty("targetDescription", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
				{
					writer.WriteProperty("targetDescription", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, typeof(string));
				}
				if (writer.ShouldIncludeProperty("targetExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
				{
					writer.WriteProperty("targetExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, typeof(string));
				}
				if (writer.ShouldIncludeProperty("targetFormatString", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("targetFormatString", MetadataPropertyNature.RegularProperty, typeof(string));
				}
				if (writer.ShouldIncludeProperty("statusGraphic", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("statusGraphic", MetadataPropertyNature.RegularProperty, typeof(string));
				}
				if (writer.ShouldIncludeProperty("statusDescription", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
				{
					writer.WriteProperty("statusDescription", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, typeof(string));
				}
				if (writer.ShouldIncludeProperty("statusExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
				{
					writer.WriteProperty("statusExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, typeof(string));
				}
				if (writer.ShouldIncludeProperty("trendGraphic", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("trendGraphic", MetadataPropertyNature.RegularProperty, typeof(string));
				}
				if (writer.ShouldIncludeProperty("trendDescription", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
				{
					writer.WriteProperty("trendDescription", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, typeof(string));
				}
				if (writer.ShouldIncludeProperty("trendExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
				{
					writer.WriteProperty("trendExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, typeof(string));
				}
				if (writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
				{
					writer.WriteProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
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

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0002B1D4 File Offset: 0x000293D4
		// (set) Token: 0x060005A7 RID: 1447 RVA: 0x0002B1DC File Offset: 0x000293DC
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (KPI.ObjectBody)value;
			}
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0002B1EA File Offset: 0x000293EA
		internal override ITxObjectBody CreateBody()
		{
			return new KPI.ObjectBody(this);
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0002B1F2 File Offset: 0x000293F2
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new KPI();
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0002B1F9 File Offset: 0x000293F9
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return null;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0002B1FC File Offset: 0x000293FC
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			Measure measure = MetadataObject.ResolveMetadataObjectParentById<KPI, Measure>(this.body.MeasureID, objectMap, throwIfCantResolve, "KPI", null);
			if (measure != null && measure.Model != null)
			{
				foreach (MetadataObject metadataObject in base.GetChildren(false))
				{
					measure.Model.NotifySubtreeAdded(metadataObject);
				}
			}
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0002B274 File Offset: 0x00029474
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0002B276 File Offset: 0x00029476
		internal override IEnumerable<IMetadataObjectCollection> GetChildrenCollections(bool isLogicalStructure)
		{
			yield return this._Annotations;
			yield return this._ExtendedProperties;
			yield break;
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x0002B286 File Offset: 0x00029486
		public KPIAnnotationCollection Annotations
		{
			get
			{
				return this._Annotations;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x0002B28E File Offset: 0x0002948E
		[CompatibilityRequirement("1400")]
		public KPIExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return this._ExtendedProperties;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0002B296 File Offset: 0x00029496
		// (set) Token: 0x060005B1 RID: 1457 RVA: 0x0002B2A4 File Offset: 0x000294A4
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

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0002B314 File Offset: 0x00029514
		// (set) Token: 0x060005B3 RID: 1459 RVA: 0x0002B324 File Offset: 0x00029524
		public string TargetDescription
		{
			get
			{
				return this.body.TargetDescription;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.TargetDescription, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "TargetDescription", typeof(string), this.body.TargetDescription, value);
					string targetDescription = this.body.TargetDescription;
					this.body.TargetDescription = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "TargetDescription", typeof(string), targetDescription, value);
				}
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x0002B394 File Offset: 0x00029594
		// (set) Token: 0x060005B5 RID: 1461 RVA: 0x0002B3A4 File Offset: 0x000295A4
		public string TargetExpression
		{
			get
			{
				return this.body.TargetExpression;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.TargetExpression, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "TargetExpression", typeof(string), this.body.TargetExpression, value);
					string targetExpression = this.body.TargetExpression;
					this.body.TargetExpression = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "TargetExpression", typeof(string), targetExpression, value);
				}
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x0002B414 File Offset: 0x00029614
		// (set) Token: 0x060005B7 RID: 1463 RVA: 0x0002B424 File Offset: 0x00029624
		public string TargetFormatString
		{
			get
			{
				return this.body.TargetFormatString;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.TargetFormatString, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "TargetFormatString", typeof(string), this.body.TargetFormatString, value);
					string targetFormatString = this.body.TargetFormatString;
					this.body.TargetFormatString = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "TargetFormatString", typeof(string), targetFormatString, value);
				}
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x0002B494 File Offset: 0x00029694
		// (set) Token: 0x060005B9 RID: 1465 RVA: 0x0002B4A4 File Offset: 0x000296A4
		public string StatusGraphic
		{
			get
			{
				return this.body.StatusGraphic;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.StatusGraphic, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "StatusGraphic", typeof(string), this.body.StatusGraphic, value);
					string statusGraphic = this.body.StatusGraphic;
					this.body.StatusGraphic = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "StatusGraphic", typeof(string), statusGraphic, value);
				}
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x0002B514 File Offset: 0x00029714
		// (set) Token: 0x060005BB RID: 1467 RVA: 0x0002B524 File Offset: 0x00029724
		public string StatusDescription
		{
			get
			{
				return this.body.StatusDescription;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.StatusDescription, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "StatusDescription", typeof(string), this.body.StatusDescription, value);
					string statusDescription = this.body.StatusDescription;
					this.body.StatusDescription = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "StatusDescription", typeof(string), statusDescription, value);
				}
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x0002B594 File Offset: 0x00029794
		// (set) Token: 0x060005BD RID: 1469 RVA: 0x0002B5A4 File Offset: 0x000297A4
		public string StatusExpression
		{
			get
			{
				return this.body.StatusExpression;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.StatusExpression, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "StatusExpression", typeof(string), this.body.StatusExpression, value);
					string statusExpression = this.body.StatusExpression;
					this.body.StatusExpression = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "StatusExpression", typeof(string), statusExpression, value);
				}
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x0002B614 File Offset: 0x00029814
		// (set) Token: 0x060005BF RID: 1471 RVA: 0x0002B624 File Offset: 0x00029824
		public string TrendGraphic
		{
			get
			{
				return this.body.TrendGraphic;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.TrendGraphic, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "TrendGraphic", typeof(string), this.body.TrendGraphic, value);
					string trendGraphic = this.body.TrendGraphic;
					this.body.TrendGraphic = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "TrendGraphic", typeof(string), trendGraphic, value);
				}
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x0002B694 File Offset: 0x00029894
		// (set) Token: 0x060005C1 RID: 1473 RVA: 0x0002B6A4 File Offset: 0x000298A4
		public string TrendDescription
		{
			get
			{
				return this.body.TrendDescription;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.TrendDescription, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "TrendDescription", typeof(string), this.body.TrendDescription, value);
					string trendDescription = this.body.TrendDescription;
					this.body.TrendDescription = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "TrendDescription", typeof(string), trendDescription, value);
				}
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060005C2 RID: 1474 RVA: 0x0002B714 File Offset: 0x00029914
		// (set) Token: 0x060005C3 RID: 1475 RVA: 0x0002B724 File Offset: 0x00029924
		public string TrendExpression
		{
			get
			{
				return this.body.TrendExpression;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.TrendExpression, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "TrendExpression", typeof(string), this.body.TrendExpression, value);
					string trendExpression = this.body.TrendExpression;
					this.body.TrendExpression = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "TrendExpression", typeof(string), trendExpression, value);
				}
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x0002B794 File Offset: 0x00029994
		// (set) Token: 0x060005C5 RID: 1477 RVA: 0x0002B7A4 File Offset: 0x000299A4
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

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x0002B828 File Offset: 0x00029A28
		// (set) Token: 0x060005C7 RID: 1479 RVA: 0x0002B83C File Offset: 0x00029A3C
		public Measure Measure
		{
			get
			{
				return this.body.MeasureID.Object;
			}
			internal set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.MeasureID.Object, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Measure", typeof(Measure), this.body.MeasureID.Object, value);
					Measure @object = this.body.MeasureID.Object;
					this.body.MeasureID.Object = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Measure", typeof(Measure), @object, value);
				}
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x0002B8C0 File Offset: 0x00029AC0
		// (set) Token: 0x060005C9 RID: 1481 RVA: 0x0002B8D2 File Offset: 0x00029AD2
		internal ObjectId _MeasureID
		{
			get
			{
				return this.body.MeasureID.ObjectID;
			}
			set
			{
				this.body.MeasureID.ObjectID = value;
			}
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0002B8E8 File Offset: 0x00029AE8
		internal void CopyFrom(KPI other, CopyContext context)
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
			}
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0002B9AA File Offset: 0x00029BAA
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((KPI)other, context);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0002B9B9 File Offset: 0x00029BB9
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(KPI other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0002B9D5 File Offset: 0x00029BD5
		public void CopyTo(KPI other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0002B9F1 File Offset: 0x00029BF1
		public KPI Clone()
		{
			return base.CloneInternal<KPI>();
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0002B9FC File Offset: 0x00029BFC
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			if (this.body.MeasureID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "MeasureID", this.body.MeasureID.Object);
			}
			if (!string.IsNullOrEmpty(this.body.Description))
			{
				writer.WriteProperty<string>(options, "Description", this.body.Description);
			}
			if (!string.IsNullOrEmpty(this.body.TargetDescription))
			{
				writer.WriteProperty<string>(options, "TargetDescription", this.body.TargetDescription);
			}
			if (!string.IsNullOrEmpty(this.body.TargetExpression))
			{
				writer.WriteProperty<string>(options, "TargetExpression", this.body.TargetExpression);
			}
			if (!string.IsNullOrEmpty(this.body.TargetFormatString))
			{
				writer.WriteProperty<string>(options, "TargetFormatString", this.body.TargetFormatString);
			}
			if (!string.IsNullOrEmpty(this.body.StatusGraphic))
			{
				writer.WriteProperty<string>(options, "StatusGraphic", this.body.StatusGraphic);
			}
			if (!string.IsNullOrEmpty(this.body.StatusDescription))
			{
				writer.WriteProperty<string>(options, "StatusDescription", this.body.StatusDescription);
			}
			if (!string.IsNullOrEmpty(this.body.StatusExpression))
			{
				writer.WriteProperty<string>(options, "StatusExpression", this.body.StatusExpression);
			}
			if (!string.IsNullOrEmpty(this.body.TrendGraphic))
			{
				writer.WriteProperty<string>(options, "TrendGraphic", this.body.TrendGraphic);
			}
			if (!string.IsNullOrEmpty(this.body.TrendDescription))
			{
				writer.WriteProperty<string>(options, "TrendDescription", this.body.TrendDescription);
			}
			if (!string.IsNullOrEmpty(this.body.TrendExpression))
			{
				writer.WriteProperty<string>(options, "TrendExpression", this.body.TrendExpression);
			}
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0002BBDC File Offset: 0x00029DDC
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("MeasureID", out objectId))
			{
				this.body.MeasureID.ObjectID = objectId;
			}
			string text;
			if (reader.TryReadProperty<string>("Description", out text))
			{
				this.body.Description = text;
			}
			string text2;
			if (reader.TryReadProperty<string>("TargetDescription", out text2))
			{
				this.body.TargetDescription = text2;
			}
			string text3;
			if (reader.TryReadProperty<string>("TargetExpression", out text3))
			{
				this.body.TargetExpression = text3;
			}
			string text4;
			if (reader.TryReadProperty<string>("TargetFormatString", out text4))
			{
				this.body.TargetFormatString = text4;
			}
			string text5;
			if (reader.TryReadProperty<string>("StatusGraphic", out text5))
			{
				this.body.StatusGraphic = text5;
			}
			string text6;
			if (reader.TryReadProperty<string>("StatusDescription", out text6))
			{
				this.body.StatusDescription = text6;
			}
			string text7;
			if (reader.TryReadProperty<string>("StatusExpression", out text7))
			{
				this.body.StatusExpression = text7;
			}
			string text8;
			if (reader.TryReadProperty<string>("TrendGraphic", out text8))
			{
				this.body.TrendGraphic = text8;
			}
			string text9;
			if (reader.TryReadProperty<string>("TrendDescription", out text9))
			{
				this.body.TrendDescription = text9;
			}
			string text10;
			if (reader.TryReadProperty<string>("TrendExpression", out text10))
			{
				this.body.TrendExpression = text10;
			}
			DateTime dateTime;
			if (reader.TryReadProperty<DateTime>("ModifiedTime", out dateTime))
			{
				this.body.ModifiedTime = dateTime;
			}
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x0002BD44 File Offset: 0x00029F44
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataBodyProperties(context, writer);
			if (this.body.MeasureID.Object != null && writer.ShouldIncludeProperty("MeasureID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly))
			{
				writer.WriteObjectIdProperty("MeasureID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly, this.body.MeasureID.Object);
			}
			if (!string.IsNullOrEmpty(this.body.Description) && writer.ShouldIncludeProperty("Description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("Description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, this.body.Description);
			}
			if (!string.IsNullOrEmpty(this.body.TargetDescription) && writer.ShouldIncludeProperty("TargetDescription", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("TargetDescription", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, this.body.TargetDescription);
			}
			if (!string.IsNullOrEmpty(this.body.TargetExpression) && writer.ShouldIncludeProperty("TargetExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("TargetExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, this.body.TargetExpression);
			}
			if (!string.IsNullOrEmpty(this.body.TargetFormatString) && writer.ShouldIncludeProperty("TargetFormatString", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("TargetFormatString", MetadataPropertyNature.RegularProperty, this.body.TargetFormatString);
			}
			if (!string.IsNullOrEmpty(this.body.StatusGraphic) && writer.ShouldIncludeProperty("StatusGraphic", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("StatusGraphic", MetadataPropertyNature.RegularProperty, this.body.StatusGraphic);
			}
			if (!string.IsNullOrEmpty(this.body.StatusDescription) && writer.ShouldIncludeProperty("StatusDescription", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("StatusDescription", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, this.body.StatusDescription);
			}
			if (!string.IsNullOrEmpty(this.body.StatusExpression) && writer.ShouldIncludeProperty("StatusExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("StatusExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, this.body.StatusExpression);
			}
			if (!string.IsNullOrEmpty(this.body.TrendGraphic) && writer.ShouldIncludeProperty("TrendGraphic", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("TrendGraphic", MetadataPropertyNature.RegularProperty, this.body.TrendGraphic);
			}
			if (!string.IsNullOrEmpty(this.body.TrendDescription) && writer.ShouldIncludeProperty("TrendDescription", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("TrendDescription", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, this.body.TrendDescription);
			}
			if (!string.IsNullOrEmpty(this.body.TrendExpression) && writer.ShouldIncludeProperty("TrendExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("TrendExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, this.body.TrendExpression);
			}
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0002BFFC File Offset: 0x0002A1FC
		private protected override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataTree(context, writer);
			if (!string.IsNullOrEmpty(this.body.Description) && writer.ShouldIncludeProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, this.body.Description);
			}
			if (!string.IsNullOrEmpty(this.body.TargetDescription) && writer.ShouldIncludeProperty("targetDescription", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("targetDescription", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, this.body.TargetDescription);
			}
			if (!string.IsNullOrEmpty(this.body.TargetExpression) && writer.ShouldIncludeProperty("targetExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("targetExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, this.body.TargetExpression);
			}
			if (!string.IsNullOrEmpty(this.body.TargetFormatString) && writer.ShouldIncludeProperty("targetFormatString", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("targetFormatString", MetadataPropertyNature.RegularProperty, this.body.TargetFormatString);
			}
			if (!string.IsNullOrEmpty(this.body.StatusGraphic) && writer.ShouldIncludeProperty("statusGraphic", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("statusGraphic", MetadataPropertyNature.RegularProperty, this.body.StatusGraphic);
			}
			if (!string.IsNullOrEmpty(this.body.StatusDescription) && writer.ShouldIncludeProperty("statusDescription", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("statusDescription", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, this.body.StatusDescription);
			}
			if (!string.IsNullOrEmpty(this.body.StatusExpression) && writer.ShouldIncludeProperty("statusExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("statusExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, this.body.StatusExpression);
			}
			if (!string.IsNullOrEmpty(this.body.TrendGraphic) && writer.ShouldIncludeProperty("trendGraphic", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("trendGraphic", MetadataPropertyNature.RegularProperty, this.body.TrendGraphic);
			}
			if (!string.IsNullOrEmpty(this.body.TrendDescription) && writer.ShouldIncludeProperty("trendDescription", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("trendDescription", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, this.body.TrendDescription);
			}
			if (!string.IsNullOrEmpty(this.body.TrendExpression) && writer.ShouldIncludeProperty("trendExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("trendExpression", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, this.body.TrendExpression);
			}
			if (this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.ModifiedTime);
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

		// Token: 0x060005D3 RID: 1491 RVA: 0x0002C358 File Offset: 0x0002A558
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
				case 9:
					if (propertyName == "MeasureID")
					{
						this.body.MeasureID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					break;
				case 11:
				{
					char c = propertyName[0];
					if (c != 'D')
					{
						if (c != 'a')
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
					}
					else if (!(propertyName == "Description"))
					{
						break;
					}
					this.body.Description = reader.ReadStringProperty();
					return true;
				}
				case 12:
				{
					char c = propertyName[0];
					if (c <= 'T')
					{
						if (c != 'M')
						{
							if (c != 'T')
							{
								break;
							}
							if (!(propertyName == "TrendGraphic"))
							{
								break;
							}
						}
						else
						{
							if (!(propertyName == "ModifiedTime"))
							{
								break;
							}
							goto IL_0459;
						}
					}
					else if (c != 'm')
					{
						if (c != 't')
						{
							break;
						}
						if (!(propertyName == "trendGraphic"))
						{
							break;
						}
					}
					else
					{
						if (!(propertyName == "modifiedTime"))
						{
							break;
						}
						goto IL_0459;
					}
					this.body.TrendGraphic = reader.ReadStringProperty();
					return true;
					IL_0459:
					this.body.ModifiedTime = reader.ReadDateTimeProperty();
					return true;
				}
				case 13:
				{
					char c = propertyName[0];
					if (c != 'S')
					{
						if (c != 's')
						{
							break;
						}
						if (!(propertyName == "statusGraphic"))
						{
							break;
						}
					}
					else if (!(propertyName == "StatusGraphic"))
					{
						break;
					}
					this.body.StatusGraphic = reader.ReadStringProperty();
					return true;
				}
				case 15:
				{
					char c = propertyName[0];
					if (c != 'T')
					{
						if (c != 't')
						{
							break;
						}
						if (!(propertyName == "trendExpression"))
						{
							break;
						}
					}
					else if (!(propertyName == "TrendExpression"))
					{
						break;
					}
					this.body.TrendExpression = reader.ReadStringProperty();
					return true;
				}
				case 16:
				{
					char c = propertyName[0];
					if (c <= 'T')
					{
						if (c != 'S')
						{
							if (c != 'T')
							{
								break;
							}
							if (!(propertyName == "TargetExpression"))
							{
								if (!(propertyName == "TrendDescription"))
								{
									break;
								}
								goto IL_0433;
							}
						}
						else
						{
							if (!(propertyName == "StatusExpression"))
							{
								break;
							}
							goto IL_040D;
						}
					}
					else if (c != 's')
					{
						if (c != 't')
						{
							break;
						}
						if (!(propertyName == "targetExpression"))
						{
							if (!(propertyName == "trendDescription"))
							{
								break;
							}
							goto IL_0433;
						}
					}
					else
					{
						if (!(propertyName == "statusExpression"))
						{
							break;
						}
						goto IL_040D;
					}
					this.body.TargetExpression = reader.ReadStringProperty();
					return true;
					IL_040D:
					this.body.StatusExpression = reader.ReadStringProperty();
					return true;
					IL_0433:
					this.body.TrendDescription = reader.ReadStringProperty();
					return true;
				}
				case 17:
				{
					char c = propertyName[0];
					if (c <= 'T')
					{
						if (c != 'S')
						{
							if (c != 'T')
							{
								break;
							}
							if (!(propertyName == "TargetDescription"))
							{
								break;
							}
						}
						else
						{
							if (!(propertyName == "StatusDescription"))
							{
								break;
							}
							goto IL_03FA;
						}
					}
					else if (c != 's')
					{
						if (c != 't')
						{
							break;
						}
						if (!(propertyName == "targetDescription"))
						{
							break;
						}
					}
					else
					{
						if (!(propertyName == "statusDescription"))
						{
							break;
						}
						goto IL_03FA;
					}
					this.body.TargetDescription = reader.ReadStringProperty();
					return true;
					IL_03FA:
					this.body.StatusDescription = reader.ReadStringProperty();
					return true;
				}
				case 18:
				{
					char c = propertyName[0];
					if (c != 'T')
					{
						if (c != 'e')
						{
							if (c != 't')
							{
								break;
							}
							if (!(propertyName == "targetFormatString"))
							{
								break;
							}
						}
						else
						{
							if (!(propertyName == "extendedProperties"))
							{
								break;
							}
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
					else if (!(propertyName == "TargetFormatString"))
					{
						break;
					}
					this.body.TargetFormatString = reader.ReadStringProperty();
					return true;
				}
				}
			}
			classification = UnexpectedPropertyClassification.UnknownProperty;
			return false;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0002C948 File Offset: 0x0002AB48
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!string.IsNullOrEmpty(this.body.Description))
			{
				result["description", TomPropCategory.Regular, 2, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Description, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.TargetDescription))
			{
				result["targetDescription", TomPropCategory.Regular, 3, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.TargetDescription, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.TargetExpression))
			{
				result["targetExpression", TomPropCategory.Regular, 4, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.TargetExpression, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.TargetFormatString))
			{
				result["targetFormatString", TomPropCategory.Regular, 5, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.TargetFormatString, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.StatusGraphic))
			{
				result["statusGraphic", TomPropCategory.Regular, 6, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.StatusGraphic, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.StatusDescription))
			{
				result["statusDescription", TomPropCategory.Regular, 7, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.StatusDescription, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.StatusExpression))
			{
				result["statusExpression", TomPropCategory.Regular, 8, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.StatusExpression, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.TrendGraphic))
			{
				result["trendGraphic", TomPropCategory.Regular, 9, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.TrendGraphic, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.TrendDescription))
			{
				result["trendDescription", TomPropCategory.Regular, 10, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.TrendDescription, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.TrendExpression))
			{
				result["trendExpression", TomPropCategory.Regular, 11, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.TrendExpression, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["modifiedTime", TomPropCategory.Regular, 12, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.ModifiedTime);
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

		// Token: 0x060005D5 RID: 1493 RVA: 0x0002CDE8 File Offset: 0x0002AFE8
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (name != null)
			{
				switch (name.Length)
				{
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
					if (c != 'm')
					{
						if (c == 't')
						{
							if (name == "trendGraphic")
							{
								this.body.TrendGraphic = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
								return true;
							}
						}
					}
					else if (name == "modifiedTime")
					{
						this.body.ModifiedTime = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
						return true;
					}
					break;
				}
				case 13:
					if (name == "statusGraphic")
					{
						this.body.StatusGraphic = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
						return true;
					}
					break;
				case 15:
					if (name == "trendExpression")
					{
						this.body.TrendExpression = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
						return true;
					}
					break;
				case 16:
				{
					char c = name[1];
					if (c != 'a')
					{
						if (c != 'r')
						{
							if (c == 't')
							{
								if (name == "statusExpression")
								{
									this.body.StatusExpression = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
									return true;
								}
							}
						}
						else if (name == "trendDescription")
						{
							this.body.TrendDescription = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
							return true;
						}
					}
					else if (name == "targetExpression")
					{
						this.body.TargetExpression = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
						return true;
					}
					break;
				}
				case 17:
				{
					char c = name[0];
					if (c != 's')
					{
						if (c == 't')
						{
							if (name == "targetDescription")
							{
								this.body.TargetDescription = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
								return true;
							}
						}
					}
					else if (name == "statusDescription")
					{
						this.body.StatusDescription = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
						return true;
					}
					break;
				}
				case 18:
				{
					char c = name[0];
					if (c != 'e')
					{
						if (c == 't')
						{
							if (name == "targetFormatString")
							{
								this.body.TargetFormatString = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
								return true;
							}
						}
					}
					else if (name == "extendedProperties")
					{
						if (!JsonPropertyHelper.IsEmptyObjectCollection(jsonProp.Value) && !CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
						{
							return false;
						}
						JsonPropertyHelper.ReadObjectCollection(this.ExtendedProperties, jsonProp.Value, options, mode, dbCompatibilityLevel);
						return true;
					}
					break;
				}
				}
			}
			return false;
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0002D138 File Offset: 0x0002B338
		internal override string GetFormattedObjectPath()
		{
			if (this.Measure != null && this.Measure.Table != null)
			{
				return TomSR.ObjectPath_KPI_2Args(this.Measure.Name, this.Measure.Table.Name);
			}
			if (this.Measure != null)
			{
				return TomSR.ObjectPath_KPI_1Arg(this.Measure.Name);
			}
			return TomSR.ObjectPath_KPI_0Args;
		}

		// Token: 0x04000109 RID: 265
		internal KPI.ObjectBody body;

		// Token: 0x0400010A RID: 266
		private KPIAnnotationCollection _Annotations;

		// Token: 0x0400010B RID: 267
		private KPIExtendedPropertyCollection _ExtendedProperties;

		// Token: 0x02000272 RID: 626
		internal class ObjectBody : MetadataObjectBody<KPI>
		{
			// Token: 0x0600208B RID: 8331 RVA: 0x000D5520 File Offset: 0x000D3720
			public ObjectBody(KPI owner)
				: base(owner)
			{
				this.ModifiedTime = DateTime.MinValue;
				this.MeasureID = new ParentLink<KPI, Measure>(owner, "Measure");
			}

			// Token: 0x0600208C RID: 8332 RVA: 0x000D5548 File Offset: 0x000D3748
			internal bool IsEqualTo(KPI.ObjectBody other, CopyContext context)
			{
				return PropertyHelper.AreValuesIdentical(this.Description, other.Description) && PropertyHelper.AreValuesIdentical(this.TargetDescription, other.TargetDescription) && PropertyHelper.AreValuesIdentical(this.TargetExpression, other.TargetExpression) && PropertyHelper.AreValuesIdentical(this.TargetFormatString, other.TargetFormatString) && PropertyHelper.AreValuesIdentical(this.StatusGraphic, other.StatusGraphic) && PropertyHelper.AreValuesIdentical(this.StatusDescription, other.StatusDescription) && PropertyHelper.AreValuesIdentical(this.StatusExpression, other.StatusExpression) && PropertyHelper.AreValuesIdentical(this.TrendGraphic, other.TrendGraphic) && PropertyHelper.AreValuesIdentical(this.TrendDescription, other.TrendDescription) && PropertyHelper.AreValuesIdentical(this.TrendExpression, other.TrendExpression) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.MeasureID.IsEqualTo(other.MeasureID, context));
			}

			// Token: 0x0600208D RID: 8333 RVA: 0x000D567C File Offset: 0x000D387C
			internal void CopyFromImpl(KPI.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
				this.Description = other.Description;
				this.TargetDescription = other.TargetDescription;
				this.TargetExpression = other.TargetExpression;
				this.TargetFormatString = other.TargetFormatString;
				this.StatusGraphic = other.StatusGraphic;
				this.StatusDescription = other.StatusDescription;
				this.StatusExpression = other.StatusExpression;
				this.TrendGraphic = other.TrendGraphic;
				this.TrendDescription = other.TrendDescription;
				this.TrendExpression = other.TrendExpression;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ModifiedTime = other.ModifiedTime;
				}
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.MeasureID.CopyFrom(other.MeasureID, context);
				}
			}

			// Token: 0x0600208E RID: 8334 RVA: 0x000D5758 File Offset: 0x000D3958
			internal void CopyFromImpl(KPI.ObjectBody other)
			{
				this.Description = other.Description;
				this.TargetDescription = other.TargetDescription;
				this.TargetExpression = other.TargetExpression;
				this.TargetFormatString = other.TargetFormatString;
				this.StatusGraphic = other.StatusGraphic;
				this.StatusDescription = other.StatusDescription;
				this.StatusExpression = other.StatusExpression;
				this.TrendGraphic = other.TrendGraphic;
				this.TrendDescription = other.TrendDescription;
				this.TrendExpression = other.TrendExpression;
				this.ModifiedTime = other.ModifiedTime;
				this.MeasureID.CopyFrom(other.MeasureID, ObjectChangeTracker.BodyCloneContext);
			}

			// Token: 0x0600208F RID: 8335 RVA: 0x000D57FF File Offset: 0x000D39FF
			public override void CopyFrom(MetadataObjectBody<KPI> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((KPI.ObjectBody)other, context);
			}

			// Token: 0x06002090 RID: 8336 RVA: 0x000D5818 File Offset: 0x000D3A18
			internal bool IsEqualTo(KPI.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.Description, other.Description) && PropertyHelper.AreValuesIdentical(this.TargetDescription, other.TargetDescription) && PropertyHelper.AreValuesIdentical(this.TargetExpression, other.TargetExpression) && PropertyHelper.AreValuesIdentical(this.TargetFormatString, other.TargetFormatString) && PropertyHelper.AreValuesIdentical(this.StatusGraphic, other.StatusGraphic) && PropertyHelper.AreValuesIdentical(this.StatusDescription, other.StatusDescription) && PropertyHelper.AreValuesIdentical(this.StatusExpression, other.StatusExpression) && PropertyHelper.AreValuesIdentical(this.TrendGraphic, other.TrendGraphic) && PropertyHelper.AreValuesIdentical(this.TrendDescription, other.TrendDescription) && PropertyHelper.AreValuesIdentical(this.TrendExpression, other.TrendExpression) && PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime) && this.MeasureID.IsEqualTo(other.MeasureID);
			}

			// Token: 0x06002091 RID: 8337 RVA: 0x000D5922 File Offset: 0x000D3B22
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((KPI.ObjectBody)other);
			}

			// Token: 0x06002092 RID: 8338 RVA: 0x000D593C File Offset: 0x000D3B3C
			internal void CompareWith(KPI.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.Description, other.Description))
				{
					context.RegisterPropertyChange(base.Owner, "Description", typeof(string), PropertyFlags.DdlAndUser, other.Description, this.Description);
				}
				if (!PropertyHelper.AreValuesIdentical(this.TargetDescription, other.TargetDescription))
				{
					context.RegisterPropertyChange(base.Owner, "TargetDescription", typeof(string), PropertyFlags.DdlAndUser, other.TargetDescription, this.TargetDescription);
				}
				if (!PropertyHelper.AreValuesIdentical(this.TargetExpression, other.TargetExpression))
				{
					context.RegisterPropertyChange(base.Owner, "TargetExpression", typeof(string), PropertyFlags.DdlAndUser, other.TargetExpression, this.TargetExpression);
				}
				if (!PropertyHelper.AreValuesIdentical(this.TargetFormatString, other.TargetFormatString))
				{
					context.RegisterPropertyChange(base.Owner, "TargetFormatString", typeof(string), PropertyFlags.DdlAndUser, other.TargetFormatString, this.TargetFormatString);
				}
				if (!PropertyHelper.AreValuesIdentical(this.StatusGraphic, other.StatusGraphic))
				{
					context.RegisterPropertyChange(base.Owner, "StatusGraphic", typeof(string), PropertyFlags.DdlAndUser, other.StatusGraphic, this.StatusGraphic);
				}
				if (!PropertyHelper.AreValuesIdentical(this.StatusDescription, other.StatusDescription))
				{
					context.RegisterPropertyChange(base.Owner, "StatusDescription", typeof(string), PropertyFlags.DdlAndUser, other.StatusDescription, this.StatusDescription);
				}
				if (!PropertyHelper.AreValuesIdentical(this.StatusExpression, other.StatusExpression))
				{
					context.RegisterPropertyChange(base.Owner, "StatusExpression", typeof(string), PropertyFlags.DdlAndUser, other.StatusExpression, this.StatusExpression);
				}
				if (!PropertyHelper.AreValuesIdentical(this.TrendGraphic, other.TrendGraphic))
				{
					context.RegisterPropertyChange(base.Owner, "TrendGraphic", typeof(string), PropertyFlags.DdlAndUser, other.TrendGraphic, this.TrendGraphic);
				}
				if (!PropertyHelper.AreValuesIdentical(this.TrendDescription, other.TrendDescription))
				{
					context.RegisterPropertyChange(base.Owner, "TrendDescription", typeof(string), PropertyFlags.DdlAndUser, other.TrendDescription, this.TrendDescription);
				}
				if (!PropertyHelper.AreValuesIdentical(this.TrendExpression, other.TrendExpression))
				{
					context.RegisterPropertyChange(base.Owner, "TrendExpression", typeof(string), PropertyFlags.DdlAndUser, other.TrendExpression, this.TrendExpression);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "ModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ModifiedTime, this.ModifiedTime);
				}
				this.MeasureID.CompareWith(other.MeasureID, "MeasureID", "Measure", PropertyFlags.ReadOnly, context);
			}

			// Token: 0x06002093 RID: 8339 RVA: 0x000D5BF9 File Offset: 0x000D3DF9
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((KPI.ObjectBody)other, context);
			}

			// Token: 0x0400086D RID: 2157
			internal string Description;

			// Token: 0x0400086E RID: 2158
			internal string TargetDescription;

			// Token: 0x0400086F RID: 2159
			internal string TargetExpression;

			// Token: 0x04000870 RID: 2160
			internal string TargetFormatString;

			// Token: 0x04000871 RID: 2161
			internal string StatusGraphic;

			// Token: 0x04000872 RID: 2162
			internal string StatusDescription;

			// Token: 0x04000873 RID: 2163
			internal string StatusExpression;

			// Token: 0x04000874 RID: 2164
			internal string TrendGraphic;

			// Token: 0x04000875 RID: 2165
			internal string TrendDescription;

			// Token: 0x04000876 RID: 2166
			internal string TrendExpression;

			// Token: 0x04000877 RID: 2167
			internal DateTime ModifiedTime;

			// Token: 0x04000878 RID: 2168
			internal ParentLink<KPI, Measure> MeasureID;
		}
	}
}
