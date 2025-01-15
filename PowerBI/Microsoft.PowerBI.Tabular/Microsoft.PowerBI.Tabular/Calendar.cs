using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200003D RID: 61
	[EditorBrowsable(EditorBrowsableState.Never)]
	[CompatibilityRequirement("Preview")]
	public sealed class Calendar : NamedMetadataObject, IMetadataObjectWithLineage
	{
		// Token: 0x060001BE RID: 446 RVA: 0x0000D2D8 File Offset: 0x0000B4D8
		public Calendar()
		{
			this.InitBodyAndCollections();
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000D2E6 File Offset: 0x0000B4E6
		internal Calendar(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections();
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000D2F4 File Offset: 0x0000B4F4
		private void InitBodyAndCollections()
		{
			this.body = new Calendar.ObjectBody(this);
			this.body.Name = string.Empty;
			this.body.Description = string.Empty;
			this.body.LineageTag = string.Empty;
			this.body.SourceLineageTag = string.Empty;
			this._TimeUnitColumnAssociations = new TimeUnitColumnAssociationCollection(this);
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000D359 File Offset: 0x0000B559
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.Calendar;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000D35D File Offset: 0x0000B55D
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x0000D36F File Offset: 0x0000B56F
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
					MetadataObject.UpdateMetadataObjectParent<Calendar, Table>(this.body.TableID, (Table)value, null, null);
				}
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x0000D39C File Offset: 0x0000B59C
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.TableID.ObjectID;
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000D3B0 File Offset: 0x0000B5B0
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.Calendar, null, "Calendar object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
				{
					writer.WriteProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, typeof(string));
				}
				if (writer.ShouldIncludeProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
				{
					writer.WriteProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, typeof(string));
				}
				if (writer.ShouldIncludeProperty("lineageTag", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("lineageTag", MetadataPropertyNature.RegularProperty, typeof(string));
				}
				if (writer.ShouldIncludeProperty("sourceLineageTag", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("sourceLineageTag", MetadataPropertyNature.RegularProperty, typeof(string));
				}
				if (writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
				{
					writer.WriteProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
				}
				if (CompatibilityRestrictions.TimeUnitColumnAssociation.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("timeUnitColumnAssociations", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "timeUnitColumnAssociations", MetadataPropertyNature.ChildCollection, ObjectType.TimeUnitColumnAssociation);
				}
			}
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000D4FC File Offset: 0x0000B6FC
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			requiredLevel = CompatibilityRestrictions.Calendar[mode];
			requestingPath = ((!CompatibilityRestrictionSet.IsUnbound(requiredLevel)) ? string.Format("[{0}]", this.GetFormattedObjectPath()) : string.Empty);
			int num = requiredLevel;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x0000D533 File Offset: 0x0000B733
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x0000D53B File Offset: 0x0000B73B
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (Calendar.ObjectBody)value;
			}
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000D549 File Offset: 0x0000B749
		internal override ITxObjectBody CreateBody()
		{
			return new Calendar.ObjectBody(this);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000D551 File Offset: 0x0000B751
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new Calendar();
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000D558 File Offset: 0x0000B758
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return ((Table)parent).Calendars;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000D568 File Offset: 0x0000B768
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			Table table = MetadataObject.ResolveMetadataObjectParentById<Calendar, Table>(this.body.TableID, objectMap, throwIfCantResolve, null, null);
			if (table != null)
			{
				table.Calendars.Add(this);
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000D599 File Offset: 0x0000B799
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000D59B File Offset: 0x0000B79B
		internal override IEnumerable<IMetadataObjectCollection> GetChildrenCollections(bool isLogicalStructure)
		{
			yield return this._TimeUnitColumnAssociations;
			yield break;
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0000D5AB File Offset: 0x0000B7AB
		public TimeUnitColumnAssociationCollection TimeUnitColumnAssociations
		{
			get
			{
				return this._TimeUnitColumnAssociations;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x0000D5B3 File Offset: 0x0000B7B3
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x0000D5C0 File Offset: 0x0000B7C0
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
					if (!Utils.IsSyntacticallyValidName(value, ObjectType.Calendar, out text))
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

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x0000D643 File Offset: 0x0000B843
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x0000D650 File Offset: 0x0000B850
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

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x0000D6C0 File Offset: 0x0000B8C0
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x0000D6D0 File Offset: 0x0000B8D0
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
					ObjectChangeTracker.RegisterPropertyChanging(this, "LineageTag", typeof(string), this.body.LineageTag, value);
					string lineageTag = this.body.LineageTag;
					this.body.LineageTag = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "LineageTag", typeof(string), lineageTag, value);
				}
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x0000D740 File Offset: 0x0000B940
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x0000D750 File Offset: 0x0000B950
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
					ObjectChangeTracker.RegisterPropertyChanging(this, "SourceLineageTag", typeof(string), this.body.SourceLineageTag, value);
					string sourceLineageTag = this.body.SourceLineageTag;
					this.body.SourceLineageTag = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "SourceLineageTag", typeof(string), sourceLineageTag, value);
				}
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x0000D7C0 File Offset: 0x0000B9C0
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x0000D7D0 File Offset: 0x0000B9D0
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

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001DA RID: 474 RVA: 0x0000D854 File Offset: 0x0000BA54
		// (set) Token: 0x060001DB RID: 475 RVA: 0x0000D868 File Offset: 0x0000BA68
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

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0000D8EC File Offset: 0x0000BAEC
		// (set) Token: 0x060001DD RID: 477 RVA: 0x0000D8FE File Offset: 0x0000BAFE
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

		// Token: 0x060001DE RID: 478 RVA: 0x0000D914 File Offset: 0x0000BB14
		internal void CopyFrom(Calendar other, CopyContext context)
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
				this.TimeUnitColumnAssociations.CopyFrom(other.TimeUnitColumnAssociations, context);
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000D9C4 File Offset: 0x0000BBC4
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((Calendar)other, context);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000D9D3 File Offset: 0x0000BBD3
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(Calendar other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000D9EF File Offset: 0x0000BBEF
		public void CopyTo(Calendar other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000DA0B File Offset: 0x0000BC0B
		public Calendar Clone()
		{
			return base.CloneInternal<Calendar>();
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000DA14 File Offset: 0x0000BC14
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.Calendar.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object Calendar is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			if (this.body.TableID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "TableID", this.body.TableID.Object);
			}
			if (!string.IsNullOrEmpty(this.body.Name))
			{
				writer.WriteProperty<string>(options, "Name", this.body.Name);
			}
			if (!string.IsNullOrEmpty(this.body.Description))
			{
				writer.WriteProperty<string>(options, "Description", this.body.Description);
			}
			if (!string.IsNullOrEmpty(this.body.LineageTag))
			{
				writer.WriteProperty<string>(options, "LineageTag", this.body.LineageTag);
			}
			if (!string.IsNullOrEmpty(this.body.SourceLineageTag))
			{
				writer.WriteProperty<string>(options, "SourceLineageTag", this.body.SourceLineageTag);
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000DB34 File Offset: 0x0000BD34
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("TableID", out objectId))
			{
				this.body.TableID.ObjectID = objectId;
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
			if (reader.TryReadProperty<string>("LineageTag", out text3))
			{
				this.body.LineageTag = text3;
			}
			string text4;
			if (reader.TryReadProperty<string>("SourceLineageTag", out text4))
			{
				this.body.SourceLineageTag = text4;
			}
			DateTime dateTime;
			if (reader.TryReadProperty<DateTime>("ModifiedTime", out dateTime))
			{
				this.body.ModifiedTime = dateTime;
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000DBF4 File Offset: 0x0000BDF4
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.Calendar.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object Calendar is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataBodyProperties(context, writer);
			if (this.body.TableID.Object != null && writer.ShouldIncludeProperty("TableID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly))
			{
				writer.WriteObjectIdProperty("TableID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly, this.body.TableID.Object);
			}
			if (!string.IsNullOrEmpty(this.body.Name) && writer.ShouldIncludeProperty("Name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("Name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.Name);
			}
			if (!string.IsNullOrEmpty(this.body.Description) && writer.ShouldIncludeProperty("Description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("Description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, this.body.Description);
			}
			if (!string.IsNullOrEmpty(this.body.LineageTag) && writer.ShouldIncludeProperty("LineageTag", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("LineageTag", MetadataPropertyNature.RegularProperty, this.body.LineageTag);
			}
			if (!string.IsNullOrEmpty(this.body.SourceLineageTag) && writer.ShouldIncludeProperty("SourceLineageTag", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("SourceLineageTag", MetadataPropertyNature.RegularProperty, this.body.SourceLineageTag);
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000DD80 File Offset: 0x0000BF80
		private protected override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.Calendar.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object Calendar is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataTree(context, writer);
			if (!string.IsNullOrEmpty(this.body.Name) && writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.Name);
			}
			if (!string.IsNullOrEmpty(this.body.Description) && writer.ShouldIncludeProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, this.body.Description);
			}
			if (!string.IsNullOrEmpty(this.body.LineageTag) && writer.ShouldIncludeProperty("lineageTag", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("lineageTag", MetadataPropertyNature.RegularProperty, this.body.LineageTag);
			}
			if (!string.IsNullOrEmpty(this.body.SourceLineageTag) && writer.ShouldIncludeProperty("sourceLineageTag", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteStringProperty("sourceLineageTag", MetadataPropertyNature.RegularProperty, this.body.SourceLineageTag);
			}
			if (this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.ModifiedTime);
			}
			if (this.TimeUnitColumnAssociations.Count > 0)
			{
				if (!CompatibilityRestrictions.TimeUnitColumnAssociation.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("A child TimeUnitColumnAssociation is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("timeUnitColumnAssociations", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "timeUnitColumnAssociations", MetadataPropertyNature.ChildCollection, this.TimeUnitColumnAssociations);
				}
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000DF80 File Offset: 0x0000C180
		private protected override bool TryReadNextMetadataProperty(SerializationActivityContext context, IMetadataReader reader, out UnexpectedPropertyClassification classification)
		{
			if (base.TryReadNextMetadataProperty(context, reader, out classification))
			{
				return true;
			}
			string propertyName = reader.PropertyName;
			if (propertyName != null)
			{
				int length = propertyName.Length;
				switch (length)
				{
				case 4:
				{
					char c = propertyName[0];
					if (c != 'N')
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
					else if (!(propertyName == "Name"))
					{
						break;
					}
					this.body.Name = reader.ReadStringProperty();
					return true;
				}
				case 5:
				case 6:
				case 8:
				case 9:
					break;
				case 7:
					if (propertyName == "TableID")
					{
						this.body.TableID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					break;
				case 10:
				{
					char c = propertyName[0];
					if (c != 'L')
					{
						if (c != 'l')
						{
							break;
						}
						if (!(propertyName == "lineageTag"))
						{
							break;
						}
					}
					else if (!(propertyName == "LineageTag"))
					{
						break;
					}
					this.body.LineageTag = reader.ReadStringProperty();
					return true;
				}
				case 11:
				{
					char c = propertyName[0];
					if (c != 'D')
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
					if (c != 'M')
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
					else if (!(propertyName == "ModifiedTime"))
					{
						break;
					}
					this.body.ModifiedTime = reader.ReadDateTimeProperty();
					return true;
				}
				default:
					if (length == 16)
					{
						char c = propertyName[0];
						if (c != 'S')
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
						else if (!(propertyName == "SourceLineageTag"))
						{
							break;
						}
						this.body.SourceLineageTag = reader.ReadStringProperty();
						return true;
					}
					if (length == 26)
					{
						if (propertyName == "timeUnitColumnAssociations")
						{
							if (!CompatibilityRestrictions.TimeUnitColumnAssociation.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
							{
								classification = UnexpectedPropertyClassification.IncompatibleProperty;
								return false;
							}
							using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
							{
								foreach (TimeUnitColumnAssociation timeUnitColumnAssociation in reader.ReadChildCollectionProperty<TimeUnitColumnAssociation>(context))
								{
									try
									{
										this.TimeUnitColumnAssociations.Add(timeUnitColumnAssociation);
									}
									catch (Exception ex)
									{
										MetadataObject metadataObject = timeUnitColumnAssociation;
										string text = "TimeUnitColumnAssociation";
										TimeUnitColumnAssociation timeUnitColumnAssociation2 = timeUnitColumnAssociation;
										throw reader.CreateInvalidChildException(context, metadataObject, TomSR.Exception_FailedAddDeserializedNamedObject(text, (timeUnitColumnAssociation2 != null) ? ((IKeyedMetadataObject)timeUnitColumnAssociation2).LogicalPathElement : null, ex.Message), ex);
									}
								}
							}
							return true;
						}
					}
					break;
				}
			}
			classification = UnexpectedPropertyClassification.UnknownProperty;
			return false;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000E2B8 File Offset: 0x0000C4B8
		[Obsolete("Deprecated. Use RequestRename method instead.", false)]
		public void Rename(string newName)
		{
			this.RequestRename(newName);
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000E2C1 File Offset: 0x0000C4C1
		public void RequestRename(string newName)
		{
			ObjectChangeTracker.RegisterObjectRenaming(this);
			this.Name = newName;
			this.body.RenameRequestedThroughAPI = true;
			ObjectChangeTracker.RegisterObjectRenamed(this);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000E2E4 File Offset: 0x0000C4E4
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.Calendar.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object Calendar is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			if (!string.IsNullOrEmpty(this.body.Name))
			{
				result["name", TomPropCategory.Name, 2, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Name, SplitMultilineOptions.None);
			}
			if (!string.IsNullOrEmpty(this.body.Description))
			{
				result["description", TomPropCategory.Regular, 3, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Description, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.LineageTag))
			{
				result["lineageTag", TomPropCategory.Regular, 4, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.LineageTag, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.SourceLineageTag))
			{
				result["sourceLineageTag", TomPropCategory.Regular, 5, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.SourceLineageTag, SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["modifiedTime", TomPropCategory.Regular, 6, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.ModifiedTime);
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations && !options.IncludeTranslatablePropertiesOnly)
			{
				IEnumerable<TimeUnitColumnAssociation> enumerable;
				if (!options.IgnoreInferredObjects)
				{
					IEnumerable<TimeUnitColumnAssociation> timeUnitColumnAssociations = this.TimeUnitColumnAssociations;
					enumerable = timeUnitColumnAssociations;
				}
				else
				{
					enumerable = this.TimeUnitColumnAssociations.Where((TimeUnitColumnAssociation o) => !ObjectTreeHelper.IsInferredObject(o));
				}
				IEnumerable<TimeUnitColumnAssociation> enumerable2 = enumerable;
				if (enumerable2.Any<TimeUnitColumnAssociation>())
				{
					if (!CompatibilityRestrictions.TimeUnitColumnAssociation.IsCompatible(mode, dbCompatibilityLevel))
					{
						throw TomInternalException.Create("A child TimeUnitColumnAssociation is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
					}
					object[] array = enumerable2.Select((TimeUnitColumnAssociation obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
					object[] array2 = array;
					result["timeUnitColumnAssociations", TomPropCategory.ChildCollection, 60, false] = array2;
				}
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0000E588 File Offset: 0x0000C788
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (name == "name")
			{
				this.body.Name = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
				return true;
			}
			if (name == "description")
			{
				this.body.Description = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
				return true;
			}
			if (name == "lineageTag")
			{
				this.body.LineageTag = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
				return true;
			}
			if (name == "sourceLineageTag")
			{
				this.body.SourceLineageTag = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
				return true;
			}
			if (name == "modifiedTime")
			{
				this.body.ModifiedTime = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
				return true;
			}
			if (!(name == "timeUnitColumnAssociations"))
			{
				return false;
			}
			if (!JsonPropertyHelper.IsEmptyObjectCollection(jsonProp.Value) && !CompatibilityRestrictions.TimeUnitColumnAssociation.IsCompatible(mode, dbCompatibilityLevel))
			{
				return false;
			}
			JsonPropertyHelper.ReadObjectCollection(this.TimeUnitColumnAssociations, jsonProp.Value, options, mode, dbCompatibilityLevel);
			return true;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0000E69D File Offset: 0x0000C89D
		internal override string GetFormattedObjectPath()
		{
			if (this.Table != null)
			{
				return TomSR.ObjectPath_Calendar_2Args(this.Name, this.Table.Name);
			}
			return TomSR.ObjectPath_Calendar_1Arg(this.Name);
		}

		// Token: 0x040000DA RID: 218
		internal Calendar.ObjectBody body;

		// Token: 0x040000DB RID: 219
		private TimeUnitColumnAssociationCollection _TimeUnitColumnAssociations;

		// Token: 0x0200023B RID: 571
		internal class ObjectBody : NamedMetadataObjectBody<Calendar>
		{
			// Token: 0x06001F33 RID: 7987 RVA: 0x000CE6EB File Offset: 0x000CC8EB
			public ObjectBody(Calendar owner)
				: base(owner)
			{
				this.ModifiedTime = DateTime.MinValue;
				this.TableID = new ParentLink<Calendar, Table>(owner, "Table");
			}

			// Token: 0x06001F34 RID: 7988 RVA: 0x000CE710 File Offset: 0x000CC910
			public override string GetObjectName()
			{
				return this.Name;
			}

			// Token: 0x06001F35 RID: 7989 RVA: 0x000CE718 File Offset: 0x000CC918
			internal bool IsEqualTo(Calendar.ObjectBody other, CopyContext context)
			{
				return PropertyHelper.AreValuesIdentical(this.Name, other.Name) && PropertyHelper.AreValuesIdentical(this.Description, other.Description) && PropertyHelper.AreValuesIdentical(this.LineageTag, other.LineageTag) && PropertyHelper.AreValuesIdentical(this.SourceLineageTag, other.SourceLineageTag) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.TableID.IsEqualTo(other.TableID, context));
			}

			// Token: 0x06001F36 RID: 7990 RVA: 0x000CE7CC File Offset: 0x000CC9CC
			internal void CopyFromImpl(Calendar.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
				base.Owner.Name = other.Name;
				this.Description = other.Description;
				base.Owner.LineageTag = other.LineageTag;
				this.SourceLineageTag = other.SourceLineageTag;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ModifiedTime = other.ModifiedTime;
				}
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.TableID.CopyFrom(other.TableID, context);
				}
			}

			// Token: 0x06001F37 RID: 7991 RVA: 0x000CE86C File Offset: 0x000CCA6C
			internal void CopyFromImpl(Calendar.ObjectBody other)
			{
				this.Name = other.Name;
				this.Description = other.Description;
				this.LineageTag = other.LineageTag;
				this.SourceLineageTag = other.SourceLineageTag;
				this.ModifiedTime = other.ModifiedTime;
				this.TableID.CopyFrom(other.TableID, ObjectChangeTracker.BodyCloneContext);
			}

			// Token: 0x06001F38 RID: 7992 RVA: 0x000CE8CB File Offset: 0x000CCACB
			public override void CopyFrom(MetadataObjectBody<Calendar> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((Calendar.ObjectBody)other, context);
			}

			// Token: 0x06001F39 RID: 7993 RVA: 0x000CE8E4 File Offset: 0x000CCAE4
			internal bool IsEqualTo(Calendar.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.Name, other.Name) && PropertyHelper.AreValuesIdentical(this.Description, other.Description) && PropertyHelper.AreValuesIdentical(this.LineageTag, other.LineageTag) && PropertyHelper.AreValuesIdentical(this.SourceLineageTag, other.SourceLineageTag) && PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime) && this.TableID.IsEqualTo(other.TableID);
			}

			// Token: 0x06001F3A RID: 7994 RVA: 0x000CE970 File Offset: 0x000CCB70
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((Calendar.ObjectBody)other);
			}

			// Token: 0x06001F3B RID: 7995 RVA: 0x000CE98C File Offset: 0x000CCB8C
			internal void CompareWith(Calendar.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.Name, other.Name))
				{
					context.RegisterPropertyChange(base.Owner, "Name", typeof(string), base.RenameRequestedThroughAPI ? PropertyFlags.User : PropertyFlags.DdlAndUser, other.Name, this.Name);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Description, other.Description))
				{
					context.RegisterPropertyChange(base.Owner, "Description", typeof(string), PropertyFlags.DdlAndUser, other.Description, this.Description);
				}
				if (!PropertyHelper.AreValuesIdentical(this.LineageTag, other.LineageTag))
				{
					context.RegisterPropertyChange(base.Owner, "LineageTag", typeof(string), PropertyFlags.DdlAndUser, other.LineageTag, this.LineageTag);
				}
				if (!PropertyHelper.AreValuesIdentical(this.SourceLineageTag, other.SourceLineageTag))
				{
					context.RegisterPropertyChange(base.Owner, "SourceLineageTag", typeof(string), PropertyFlags.DdlAndUser, other.SourceLineageTag, this.SourceLineageTag);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "ModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ModifiedTime, this.ModifiedTime);
				}
				this.TableID.CompareWith(other.TableID, "TableID", "Table", PropertyFlags.ReadOnly, context);
			}

			// Token: 0x06001F3C RID: 7996 RVA: 0x000CEAF2 File Offset: 0x000CCCF2
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((Calendar.ObjectBody)other, context);
			}

			// Token: 0x0400076E RID: 1902
			internal string Name;

			// Token: 0x0400076F RID: 1903
			internal string Description;

			// Token: 0x04000770 RID: 1904
			internal string LineageTag;

			// Token: 0x04000771 RID: 1905
			internal string SourceLineageTag;

			// Token: 0x04000772 RID: 1906
			internal DateTime ModifiedTime;

			// Token: 0x04000773 RID: 1907
			internal ParentLink<Calendar, Table> TableID;
		}
	}
}
