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
	// Token: 0x02000037 RID: 55
	[CompatibilityRequirement("1470")]
	public sealed class CalculationGroup : MetadataObject
	{
		// Token: 0x06000111 RID: 273 RVA: 0x000084E4 File Offset: 0x000066E4
		public CalculationGroup()
		{
			this.InitBodyAndCollections(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000084F7 File Offset: 0x000066F7
		internal CalculationGroup(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections(comparer);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00008508 File Offset: 0x00006708
		private void InitBodyAndCollections(IEqualityComparer<string> comparer)
		{
			this.body = new CalculationGroup.ObjectBody(this);
			this.body.Description = string.Empty;
			this._Annotations = new CalculationGroupAnnotationCollection(this, comparer);
			this._CalculationItems = new CalculationItemCollection(this, comparer);
			this._CalculationExpressions = new CalculationGroupExpressionCollection(this);
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00008557 File Offset: 0x00006757
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.CalculationGroup;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000115 RID: 277 RVA: 0x0000855B File Offset: 0x0000675B
		// (set) Token: 0x06000116 RID: 278 RVA: 0x0000856D File Offset: 0x0000676D
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
					MetadataObject.UpdateMetadataObjectParent<CalculationGroup, Table>(this.body.TableID, (Table)value, "CalculationGroup", CompatibilityRestrictions.Table_CalculationGroup);
				}
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000117 RID: 279 RVA: 0x000085A2 File Offset: 0x000067A2
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.TableID.ObjectID;
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000085B4 File Offset: 0x000067B4
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.CalculationGroup, null, "CalculationGroup object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
				{
					writer.WriteProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, typeof(string));
				}
				if (writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
				{
					writer.WriteProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
				}
				if (writer.ShouldIncludeProperty("precedence", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteProperty("precedence", MetadataPropertyNature.RegularProperty, typeof(int));
				}
				CalculationGroup.WriteCalculationGroupExpressionsMetadataSchema(context, writer);
				if (CompatibilityRestrictions.CalculationItem.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && writer.ShouldIncludeProperty("calculationItems", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "calculationItems", MetadataPropertyNature.ChildCollection, ObjectType.CalculationItem);
				}
				if (writer.ShouldIncludeProperty("annotations", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "annotations", MetadataPropertyNature.ChildCollection, ObjectType.Annotation);
				}
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000086C8 File Offset: 0x000068C8
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void GetCompatibilityRequirementByMembers(CompatibilityMode mode, out int requiredLevel, out string requestingPath)
		{
			requiredLevel = CompatibilityRestrictions.CalculationGroup[mode];
			requestingPath = ((!CompatibilityRestrictionSet.IsUnbound(requiredLevel)) ? string.Format("[{0}]", this.GetFormattedObjectPath()) : string.Empty);
			int num = requiredLevel;
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600011A RID: 282 RVA: 0x000086FF File Offset: 0x000068FF
		// (set) Token: 0x0600011B RID: 283 RVA: 0x00008707 File Offset: 0x00006907
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (CalculationGroup.ObjectBody)value;
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00008715 File Offset: 0x00006915
		internal override ITxObjectBody CreateBody()
		{
			return new CalculationGroup.ObjectBody(this);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000871D File Offset: 0x0000691D
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new CalculationGroup();
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00008724 File Offset: 0x00006924
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return null;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00008728 File Offset: 0x00006928
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			Table table = MetadataObject.ResolveMetadataObjectParentById<CalculationGroup, Table>(this.body.TableID, objectMap, throwIfCantResolve, "CalculationGroup", CompatibilityRestrictions.Table_CalculationGroup);
			if (table != null && table.Model != null)
			{
				foreach (MetadataObject metadataObject in base.GetChildren(false))
				{
					table.Model.NotifySubtreeAdded(metadataObject);
				}
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000087A4 File Offset: 0x000069A4
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000087A6 File Offset: 0x000069A6
		internal override IEnumerable<MetadataObject> GetDirectChildren(bool isLogicalStructure)
		{
			if (isLogicalStructure)
			{
				if (this.MultipleOrEmptySelectionExpression != null)
				{
					yield return this.multipleOrEmptySelectionExpression;
				}
				if (this.NoSelectionExpression != null)
				{
					yield return this.noSelectionExpression;
				}
			}
			yield break;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000087BD File Offset: 0x000069BD
		internal override IEnumerable<IMetadataObjectCollection> GetChildrenCollections(bool isLogicalStructure)
		{
			yield return this._Annotations;
			yield return this._CalculationItems;
			if (!isLogicalStructure)
			{
				yield return this._CalculationExpressions;
			}
			yield break;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000123 RID: 291 RVA: 0x000087D4 File Offset: 0x000069D4
		public CalculationGroupAnnotationCollection Annotations
		{
			get
			{
				return this._Annotations;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000124 RID: 292 RVA: 0x000087DC File Offset: 0x000069DC
		public CalculationItemCollection CalculationItems
		{
			get
			{
				return this._CalculationItems;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000125 RID: 293 RVA: 0x000087E4 File Offset: 0x000069E4
		[CompatibilityRequirement("1605")]
		internal CalculationGroupExpressionCollection CalculationExpressions
		{
			get
			{
				return this._CalculationExpressions;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000126 RID: 294 RVA: 0x000087EC File Offset: 0x000069EC
		// (set) Token: 0x06000127 RID: 295 RVA: 0x000087FC File Offset: 0x000069FC
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

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000128 RID: 296 RVA: 0x0000886C File Offset: 0x00006A6C
		// (set) Token: 0x06000129 RID: 297 RVA: 0x0000887C File Offset: 0x00006A7C
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

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00008900 File Offset: 0x00006B00
		// (set) Token: 0x0600012B RID: 299 RVA: 0x00008910 File Offset: 0x00006B10
		public int Precedence
		{
			get
			{
				return this.body.Precedence;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.Precedence, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "Precedence", typeof(int), this.body.Precedence, value);
					int precedence = this.body.Precedence;
					this.body.Precedence = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "Precedence", typeof(int), precedence, value);
				}
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00008994 File Offset: 0x00006B94
		// (set) Token: 0x0600012D RID: 301 RVA: 0x000089A8 File Offset: 0x00006BA8
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

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00008A2C File Offset: 0x00006C2C
		// (set) Token: 0x0600012F RID: 303 RVA: 0x00008A3E File Offset: 0x00006C3E
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

		// Token: 0x06000130 RID: 304 RVA: 0x00008A54 File Offset: 0x00006C54
		internal void CopyFrom(CalculationGroup other, CopyContext context)
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
				this.CalculationItems.CopyFrom(other.CalculationItems, context);
				this.CalculationExpressions.CopyFrom(other.CalculationExpressions, context);
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00008B28 File Offset: 0x00006D28
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((CalculationGroup)other, context);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00008B37 File Offset: 0x00006D37
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(CalculationGroup other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00008B53 File Offset: 0x00006D53
		public void CopyTo(CalculationGroup other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00008B6F File Offset: 0x00006D6F
		public CalculationGroup Clone()
		{
			return base.CloneInternal<CalculationGroup>();
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00008B78 File Offset: 0x00006D78
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.CalculationGroup.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object CalculationGroup is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			if (this.body.TableID.Object != null)
			{
				MetadataObject.WriteObjectId(writer, options, "TableID", this.body.TableID.Object);
			}
			if (!string.IsNullOrEmpty(this.body.Description))
			{
				writer.WriteProperty<string>(options, "Description", this.body.Description);
			}
			if (this.body.Precedence != 0)
			{
				writer.WriteProperty<int>(options, "Precedence", this.body.Precedence);
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00008C40 File Offset: 0x00006E40
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("TableID", out objectId))
			{
				this.body.TableID.ObjectID = objectId;
			}
			string text;
			if (reader.TryReadProperty<string>("Description", out text))
			{
				this.body.Description = text;
			}
			DateTime dateTime;
			if (reader.TryReadProperty<DateTime>("ModifiedTime", out dateTime))
			{
				this.body.ModifiedTime = dateTime;
			}
			int num;
			if (reader.TryReadProperty<int>("Precedence", out num))
			{
				this.body.Precedence = num;
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00008CC8 File Offset: 0x00006EC8
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.CalculationGroup.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object CalculationGroup is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataBodyProperties(context, writer);
			if (this.body.TableID.Object != null && writer.ShouldIncludeProperty("TableID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly))
			{
				writer.WriteObjectIdProperty("TableID", MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly, this.body.TableID.Object);
			}
			if (!string.IsNullOrEmpty(this.body.Description) && writer.ShouldIncludeProperty("Description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("Description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, this.body.Description);
			}
			if (this.body.Precedence != 0 && writer.ShouldIncludeProperty("Precedence", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteInt32Property("Precedence", MetadataPropertyNature.RegularProperty, this.body.Precedence);
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00008DD8 File Offset: 0x00006FD8
		private protected override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (!CompatibilityRestrictions.CalculationGroup.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object CalculationGroup is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
			}
			base.WriteMetadataTree(context, writer);
			if (!string.IsNullOrEmpty(this.body.Description) && writer.ShouldIncludeProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.MultilineString, this.body.Description);
			}
			if (this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.ModifiedTime);
			}
			if (this.body.Precedence != 0 && writer.ShouldIncludeProperty("precedence", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteInt32Property("precedence", MetadataPropertyNature.RegularProperty, this.body.Precedence);
			}
			this.WriteCalculationGroupExpressionsToMetadataStream(context, writer);
			if (this.CalculationItems.Count > 0)
			{
				if (!CompatibilityRestrictions.CalculationItem.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("A child CalculationItem is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				if (writer.ShouldIncludeProperty("calculationItems", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "calculationItems", MetadataPropertyNature.ChildCollection, this.CalculationItems);
				}
			}
			if (this.Annotations.Count > 0 && writer.ShouldIncludeProperty("annotations", MetadataPropertyNature.ChildCollection))
			{
				writer.WriteChildCollection(context, "annotations", MetadataPropertyNature.ChildCollection, this.Annotations);
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00008F94 File Offset: 0x00007194
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
				case 7:
					if (propertyName == "TableID")
					{
						this.body.TableID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
					break;
				case 8:
				case 9:
					break;
				case 10:
				{
					char c = propertyName[0];
					if (c != 'P')
					{
						if (c != 'p')
						{
							break;
						}
						if (!(propertyName == "precedence"))
						{
							break;
						}
					}
					else if (!(propertyName == "Precedence"))
					{
						break;
					}
					this.body.Precedence = reader.ReadInt32Property();
					return true;
				}
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
						if (propertyName == "calculationItems")
						{
							if (!CompatibilityRestrictions.CalculationItem.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
							{
								classification = UnexpectedPropertyClassification.IncompatibleProperty;
								return false;
							}
							using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
							{
								foreach (CalculationItem calculationItem in reader.ReadChildCollectionProperty<CalculationItem>(context))
								{
									try
									{
										this.CalculationItems.Add(calculationItem);
									}
									catch (Exception ex2)
									{
										throw reader.CreateInvalidChildException(context, calculationItem, TomSR.Exception_FailedAddDeserializedNamedObject("CalculationItem", (calculationItem != null) ? calculationItem.Name : null, ex2.Message), ex2);
									}
								}
							}
							return true;
						}
					}
					break;
				}
			}
			if (context.SerializationMode != MetadataSerializationMode.Xmla && this.TryReadCalculationGroupExpressionsFromMetadataStream(context, reader))
			{
				return true;
			}
			classification = UnexpectedPropertyClassification.UnknownProperty;
			return false;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000092D0 File Offset: 0x000074D0
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!CompatibilityRestrictions.CalculationGroup.IsCompatible(mode, dbCompatibilityLevel))
			{
				throw TomInternalException.Create("Object CalculationGroup is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
			}
			if (!options.IncludeTranslatablePropertiesOnly && !string.IsNullOrEmpty(this.body.Description))
			{
				result["description", TomPropCategory.Regular, 2, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Description, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["modifiedTime", TomPropCategory.Regular, 3, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.ModifiedTime);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.Precedence != 0)
			{
				result["precedence", TomPropCategory.Regular, 4, false] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<int>(this.body.Precedence);
			}
			this.SerializeAdditionalDataToJsonObject(result, options, mode, dbCompatibilityLevel);
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations && !options.IncludeTranslatablePropertiesOnly)
			{
				IEnumerable<CalculationItem> enumerable;
				if (!options.IgnoreInferredObjects)
				{
					IEnumerable<CalculationItem> calculationItems = this.CalculationItems;
					enumerable = calculationItems;
				}
				else
				{
					enumerable = this.CalculationItems.Where((CalculationItem o) => !ObjectTreeHelper.IsInferredObject(o));
				}
				IEnumerable<CalculationItem> enumerable2 = enumerable;
				if (enumerable2.Any<CalculationItem>())
				{
					if (!CompatibilityRestrictions.CalculationItem.IsCompatible(mode, dbCompatibilityLevel))
					{
						throw TomInternalException.Create("A child CalculationItem is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
					}
					object[] array = enumerable2.Select((CalculationItem obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
					object[] array2 = array;
					result["calculationItems", TomPropCategory.ChildCollection, 46, false] = array2;
				}
			}
			if (!options.IgnoreChildren && !options.IncludeTranslatablePropertiesOnly && this.Annotations.Any<Annotation>())
			{
				object[] array = this.Annotations.Select((Annotation obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
				object[] array3 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array3;
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00009584 File Offset: 0x00007784
		private void SerializeAdditionalDataToJsonObject(JsonObject jsonObj, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations)
			{
				if (this.MultipleOrEmptySelectionExpression != null && !options.IncludeTranslatablePropertiesOnly && (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(this.MultipleOrEmptySelectionExpression)))
				{
					if (!CompatibilityRestrictions.CalculationGroupExpression.IsCompatible(mode, dbCompatibilityLevel))
					{
						throw TomInternalException.Create("Member MultipleOrEmptySelectionExpression is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
					}
					jsonObj["multipleOrEmptySelectionExpression", TomPropCategory.ChildLink, 5, false] = this.MultipleOrEmptySelectionExpression.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject();
				}
				if (this.NoSelectionExpression != null && !options.IncludeTranslatablePropertiesOnly && (!options.IgnoreInferredObjects || !ObjectTreeHelper.IsInferredObject(this.NoSelectionExpression)))
				{
					if (!CompatibilityRestrictions.CalculationGroupExpression.IsCompatible(mode, dbCompatibilityLevel))
					{
						throw TomInternalException.Create("Member NoSelectionExpression is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
					}
					jsonObj["noSelectionExpression", TomPropCategory.ChildLink, 6, false] = this.NoSelectionExpression.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject();
				}
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000969C File Offset: 0x0000789C
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (name == "description")
			{
				this.body.Description = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
				return true;
			}
			if (name == "modifiedTime")
			{
				this.body.ModifiedTime = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
				return true;
			}
			if (name == "precedence")
			{
				this.body.Precedence = JsonPropertyHelper.ConvertJsonValueToPrimitive<int>(jsonProp.Value);
				return true;
			}
			if (!(name == "calculationItems"))
			{
				if (!(name == "annotations"))
				{
					bool flag = false;
					this.ReadAdditionalPropertyFromJson(jsonProp, options, mode, dbCompatibilityLevel, ref flag);
					return flag;
				}
				JsonPropertyHelper.ReadObjectCollection(this.Annotations, jsonProp.Value, options, mode, dbCompatibilityLevel);
				return true;
			}
			else
			{
				if (!JsonPropertyHelper.IsEmptyObjectCollection(jsonProp.Value) && !CompatibilityRestrictions.CalculationItem.IsCompatible(mode, dbCompatibilityLevel))
				{
					return false;
				}
				JsonPropertyHelper.ReadObjectCollection(this.CalculationItems, jsonProp.Value, options, mode, dbCompatibilityLevel);
				return true;
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000097A0 File Offset: 0x000079A0
		private void ReadAdditionalPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel, ref bool wasRead)
		{
			string name = jsonProp.Name;
			if (!(name == "noSelectionExpression"))
			{
				if (!(name == "multipleOrEmptySelectionExpression"))
				{
					wasRead = false;
					return;
				}
				if (!CompatibilityRestrictions.CalculationGroupExpression.IsCompatible(mode, dbCompatibilityLevel))
				{
					wasRead = false;
					return;
				}
				CalculationGroupExpression calculationGroupExpression = new CalculationGroupExpression();
				calculationGroupExpression.DeserializeFromJsonObject(jsonProp.Value, options, mode, dbCompatibilityLevel);
				this.MultipleOrEmptySelectionExpression = calculationGroupExpression;
				wasRead = true;
				return;
			}
			else
			{
				if (!CompatibilityRestrictions.CalculationGroupExpression.IsCompatible(mode, dbCompatibilityLevel))
				{
					wasRead = false;
					return;
				}
				CalculationGroupExpression calculationGroupExpression2 = new CalculationGroupExpression();
				calculationGroupExpression2.DeserializeFromJsonObject(jsonProp.Value, options, mode, dbCompatibilityLevel);
				this.NoSelectionExpression = calculationGroupExpression2;
				wasRead = true;
				return;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00009840 File Offset: 0x00007A40
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00009890 File Offset: 0x00007A90
		[CompatibilityRequirement("1605")]
		public CalculationGroupExpression MultipleOrEmptySelectionExpression
		{
			get
			{
				if (this.multipleOrEmptySelectionExpression == null)
				{
					this.multipleOrEmptySelectionExpression = this._CalculationExpressions.Where((CalculationGroupExpression ce) => ce.SelectionMode == CalculationGroupSelectionMode.MultipleOrEmptySelection).FirstOrDefault<CalculationGroupExpression>();
				}
				return this.multipleOrEmptySelectionExpression;
			}
			set
			{
				if (this.multipleOrEmptySelectionExpression != value || this.multipleOrEmptySelectionExpression == null)
				{
					this.UpdateCalculationGroupExpressionChild(value, CalculationGroupSelectionMode.MultipleOrEmptySelection);
					this.multipleOrEmptySelectionExpression = value;
				}
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000140 RID: 320 RVA: 0x000098B4 File Offset: 0x00007AB4
		// (set) Token: 0x06000141 RID: 321 RVA: 0x00009904 File Offset: 0x00007B04
		[CompatibilityRequirement("1605")]
		public CalculationGroupExpression NoSelectionExpression
		{
			get
			{
				if (this.noSelectionExpression == null)
				{
					this.noSelectionExpression = this._CalculationExpressions.Where((CalculationGroupExpression ce) => ce.SelectionMode == CalculationGroupSelectionMode.NoSelection).FirstOrDefault<CalculationGroupExpression>();
				}
				return this.noSelectionExpression;
			}
			set
			{
				if (this.noSelectionExpression != value || this.noSelectionExpression == null)
				{
					this.UpdateCalculationGroupExpressionChild(value, CalculationGroupSelectionMode.NoSelection);
					this.noSelectionExpression = value;
				}
			}
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00009926 File Offset: 0x00007B26
		internal override void OnBeforeDeserialize(DeserializeOptions options)
		{
			base.OnBeforeDeserialize(options);
			this.multipleOrEmptySelectionExpression = null;
			this.noSelectionExpression = null;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00009940 File Offset: 0x00007B40
		private protected override void SetDirectChildImpl(MetadataObject child)
		{
			if (child.ObjectType != ObjectType.CalculationExpression)
			{
				base.SetDirectChildImpl(child);
			}
			CalculationGroupExpression calculationGroupExpression = (CalculationGroupExpression)child;
			Utils.Verify(calculationGroupExpression.SelectionMode > CalculationGroupSelectionMode.Unknown, "Using SetDirectChildImpl for a CalculationGroupExpression requires setting the expression's SelectionMode!");
			this.RemoveExistingExpressions(calculationGroupExpression.SelectionMode);
			this._CalculationExpressions.Add(calculationGroupExpression);
			CalculationGroupSelectionMode selectionMode = calculationGroupExpression.SelectionMode;
			if (selectionMode == CalculationGroupSelectionMode.MultipleOrEmptySelection)
			{
				this.multipleOrEmptySelectionExpression = calculationGroupExpression;
				return;
			}
			if (selectionMode != CalculationGroupSelectionMode.NoSelection)
			{
				return;
			}
			this.noSelectionExpression = calculationGroupExpression;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000099B0 File Offset: 0x00007BB0
		private protected override void RemoveDirectChildImpl(MetadataObject child)
		{
			if (child.ObjectType != ObjectType.CalculationExpression)
			{
				base.RemoveDirectChildImpl(child);
			}
			CalculationGroupExpression calculationGroupExpression = (CalculationGroupExpression)child;
			if (this._CalculationExpressions.Remove(calculationGroupExpression))
			{
				CalculationGroupSelectionMode selectionMode = calculationGroupExpression.SelectionMode;
				if (selectionMode == CalculationGroupSelectionMode.MultipleOrEmptySelection)
				{
					this.multipleOrEmptySelectionExpression = null;
					return;
				}
				if (selectionMode != CalculationGroupSelectionMode.NoSelection)
				{
					return;
				}
				this.noSelectionExpression = null;
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00009A04 File Offset: 0x00007C04
		private static void WriteCalculationGroupExpressionsMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			if (CompatibilityRestrictions.CalculationGroupExpression.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
			{
				if (writer.ShouldIncludeProperty("multipleOrEmptySelectionExpression", MetadataPropertyNature.ChildProperty))
				{
					writer.WriteSingleChild(context, "multipleOrEmptySelectionExpression", MetadataPropertyNature.ChildProperty, ObjectType.CalculationExpression);
				}
				if (writer.ShouldIncludeProperty("noSelectionExpression", MetadataPropertyNature.ChildProperty))
				{
					writer.WriteSingleChild(context, "noSelectionExpression", MetadataPropertyNature.ChildProperty, ObjectType.CalculationExpression);
				}
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00009A64 File Offset: 0x00007C64
		private void WriteCalculationGroupExpressionsToMetadataStream(SerializationActivityContext context, IMetadataWriter writer)
		{
			if (this.MultipleOrEmptySelectionExpression != null && writer.ShouldIncludeProperty("multipleOrEmptySelectionExpression", MetadataPropertyNature.ChildProperty))
			{
				if (!CompatibilityRestrictions.CalculationGroupExpression.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member MultiSelectionExpression is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				writer.WriteSingleChild(context, "multipleOrEmptySelectionExpression", MetadataPropertyNature.ChildProperty, this.MultipleOrEmptySelectionExpression);
			}
			if (this.NoSelectionExpression != null && writer.ShouldIncludeProperty("noSelectionExpression", MetadataPropertyNature.ChildProperty))
			{
				if (!CompatibilityRestrictions.CalculationGroupExpression.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					throw TomInternalException.Create("Member NoSelectionExpression is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { context.CompatibilityMode, context.DbCompatibilityLevel });
				}
				writer.WriteSingleChild(context, "noSelectionExpression", MetadataPropertyNature.ChildProperty, this.NoSelectionExpression);
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00009B50 File Offset: 0x00007D50
		private bool TryReadCalculationGroupExpressionsFromMetadataStream(SerializationActivityContext context, IMetadataReader reader)
		{
			string propertyName = reader.PropertyName;
			if (!(propertyName == "noSelectionExpression"))
			{
				if (!(propertyName == "multipleOrEmptySelectionExpression"))
				{
					return false;
				}
				if (!CompatibilityRestrictions.CalculationGroupExpression.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					return false;
				}
				using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
				{
					CalculationGroupExpression calculationGroupExpression = reader.ReadSingleChildProperty<CalculationGroupExpression>(context);
					try
					{
						this.MultipleOrEmptySelectionExpression = calculationGroupExpression;
					}
					catch (Exception ex)
					{
						MetadataObject metadataObject = calculationGroupExpression;
						string text = "CalculationGroupExpression";
						CalculationGroupExpression calculationGroupExpression2 = calculationGroupExpression;
						throw reader.CreateInvalidChildException(context, metadataObject, TomSR.Exception_FailedAddDeserializedNamedObject(text, (calculationGroupExpression2 != null) ? ((IKeyedMetadataObject)calculationGroupExpression2).LogicalPathElement : null, ex.Message), ex);
					}
				}
				return true;
			}
			else
			{
				if (!CompatibilityRestrictions.CalculationGroupExpression.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel))
				{
					return false;
				}
				using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
				{
					CalculationGroupExpression calculationGroupExpression3 = reader.ReadSingleChildProperty<CalculationGroupExpression>(context);
					try
					{
						this.NoSelectionExpression = calculationGroupExpression3;
					}
					catch (Exception ex2)
					{
						MetadataObject metadataObject2 = calculationGroupExpression3;
						string text2 = "CalculationGroupExpression";
						CalculationGroupExpression calculationGroupExpression4 = calculationGroupExpression3;
						throw reader.CreateInvalidChildException(context, metadataObject2, TomSR.Exception_FailedAddDeserializedNamedObject(text2, (calculationGroupExpression4 != null) ? ((IKeyedMetadataObject)calculationGroupExpression4).LogicalPathElement : null, ex2.Message), ex2);
					}
				}
				return true;
			}
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00009C98 File Offset: 0x00007E98
		private void RemoveExistingExpressions(CalculationGroupSelectionMode mode)
		{
			List<CalculationGroupExpression> list = new List<CalculationGroupExpression>(this._CalculationExpressions.Where((CalculationGroupExpression ce) => ce.SelectionMode == mode));
			for (int i = 0; i < list.Count; i++)
			{
				this._CalculationExpressions.Remove(list[i]);
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00009CF3 File Offset: 0x00007EF3
		private void UpdateCalculationGroupExpressionChild(CalculationGroupExpression expression, CalculationGroupSelectionMode mode)
		{
			this.RemoveExistingExpressions(mode);
			if (expression != null)
			{
				expression.SelectionMode = mode;
				this._CalculationExpressions.Add(expression);
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00009D12 File Offset: 0x00007F12
		internal override string GetFormattedObjectPath()
		{
			if (this.Parent == null)
			{
				return TomSR.ObjectPath_CalculationGroup_0Args;
			}
			return TomSR.ObjectPath_CalculationGroup_1Args(((Table)this.Parent).Name);
		}

		// Token: 0x040000D1 RID: 209
		internal CalculationGroup.ObjectBody body;

		// Token: 0x040000D2 RID: 210
		private CalculationGroupAnnotationCollection _Annotations;

		// Token: 0x040000D3 RID: 211
		private CalculationItemCollection _CalculationItems;

		// Token: 0x040000D4 RID: 212
		private CalculationGroupExpressionCollection _CalculationExpressions;

		// Token: 0x040000D5 RID: 213
		private CalculationGroupExpression multipleOrEmptySelectionExpression;

		// Token: 0x040000D6 RID: 214
		private CalculationGroupExpression noSelectionExpression;

		// Token: 0x02000230 RID: 560
		internal class ObjectBody : MetadataObjectBody<CalculationGroup>
		{
			// Token: 0x06001EEA RID: 7914 RVA: 0x000CD3D7 File Offset: 0x000CB5D7
			public ObjectBody(CalculationGroup owner)
				: base(owner)
			{
				this.ModifiedTime = DateTime.MinValue;
				this.TableID = new ParentLink<CalculationGroup, Table>(owner, "Table");
			}

			// Token: 0x06001EEB RID: 7915 RVA: 0x000CD3FC File Offset: 0x000CB5FC
			internal bool IsEqualTo(CalculationGroup.ObjectBody other, CopyContext context)
			{
				return PropertyHelper.AreValuesIdentical(this.Description, other.Description) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime)) && PropertyHelper.AreValuesIdentical(this.Precedence, other.Precedence) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.TableID.IsEqualTo(other.TableID, context));
			}

			// Token: 0x06001EEC RID: 7916 RVA: 0x000CD488 File Offset: 0x000CB688
			internal void CopyFromImpl(CalculationGroup.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
				this.Description = other.Description;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ModifiedTime = other.ModifiedTime;
				}
				this.Precedence = other.Precedence;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.TableID.CopyFrom(other.TableID, context);
				}
			}

			// Token: 0x06001EED RID: 7917 RVA: 0x000CD504 File Offset: 0x000CB704
			internal void CopyFromImpl(CalculationGroup.ObjectBody other)
			{
				this.Description = other.Description;
				this.ModifiedTime = other.ModifiedTime;
				this.Precedence = other.Precedence;
				this.TableID.CopyFrom(other.TableID, ObjectChangeTracker.BodyCloneContext);
			}

			// Token: 0x06001EEE RID: 7918 RVA: 0x000CD540 File Offset: 0x000CB740
			public override void CopyFrom(MetadataObjectBody<CalculationGroup> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((CalculationGroup.ObjectBody)other, context);
			}

			// Token: 0x06001EEF RID: 7919 RVA: 0x000CD558 File Offset: 0x000CB758
			internal bool IsEqualTo(CalculationGroup.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.Description, other.Description) && PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime) && PropertyHelper.AreValuesIdentical(this.Precedence, other.Precedence) && this.TableID.IsEqualTo(other.TableID);
			}

			// Token: 0x06001EF0 RID: 7920 RVA: 0x000CD5BA File Offset: 0x000CB7BA
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((CalculationGroup.ObjectBody)other);
			}

			// Token: 0x06001EF1 RID: 7921 RVA: 0x000CD5D4 File Offset: 0x000CB7D4
			internal void CompareWith(CalculationGroup.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.Description, other.Description))
				{
					context.RegisterPropertyChange(base.Owner, "Description", typeof(string), PropertyFlags.DdlAndUser, other.Description, this.Description);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "ModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ModifiedTime, this.ModifiedTime);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Precedence, other.Precedence))
				{
					context.RegisterPropertyChange(base.Owner, "Precedence", typeof(int), PropertyFlags.DdlAndUser, other.Precedence, this.Precedence);
				}
				this.TableID.CompareWith(other.TableID, "TableID", "Table", PropertyFlags.ReadOnly, context);
			}

			// Token: 0x06001EF2 RID: 7922 RVA: 0x000CD6C3 File Offset: 0x000CB8C3
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((CalculationGroup.ObjectBody)other, context);
			}

			// Token: 0x0400073C RID: 1852
			internal string Description;

			// Token: 0x0400073D RID: 1853
			internal DateTime ModifiedTime;

			// Token: 0x0400073E RID: 1854
			internal int Precedence;

			// Token: 0x0400073F RID: 1855
			internal ParentLink<CalculationGroup, Table> TableID;
		}
	}
}
