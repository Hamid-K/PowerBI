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
	// Token: 0x0200007E RID: 126
	public sealed class ModelRole : NamedMetadataObject
	{
		// Token: 0x06000778 RID: 1912 RVA: 0x000404C7 File Offset: 0x0003E6C7
		public ModelRole()
		{
			this.InitBodyAndCollections(StringComparer.OrdinalIgnoreCase);
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x000404DA File Offset: 0x0003E6DA
		internal ModelRole(IEqualityComparer<string> comparer)
		{
			this.InitBodyAndCollections(comparer);
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x000404EC File Offset: 0x0003E6EC
		private void InitBodyAndCollections(IEqualityComparer<string> comparer)
		{
			this.body = new ModelRole.ObjectBody(this);
			this.body.Name = string.Empty;
			this.body.Description = string.Empty;
			this.body.ModelPermission = ModelPermission.None;
			this._Members = new ModelRoleMemberCollection(this, comparer);
			this._TablePermissions = new TablePermissionCollection(this, comparer);
			this._Annotations = new ModelRoleAnnotationCollection(this, comparer);
			this._ExtendedProperties = new ModelRoleExtendedPropertyCollection(this, comparer);
		}

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x0600077B RID: 1915 RVA: 0x00040565 File Offset: 0x0003E765
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.Role;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x00040569 File Offset: 0x0003E769
		// (set) Token: 0x0600077D RID: 1917 RVA: 0x0004057B File Offset: 0x0003E77B
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
					MetadataObject.UpdateMetadataObjectParent<ModelRole, Model>(this.body.ModelID, (Model)value, null, null);
				}
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x000405A8 File Offset: 0x0003E7A8
		internal override ObjectId ParentId
		{
			get
			{
				return this.body.ModelID.ObjectID;
			}
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x000405BC File Offset: 0x0003E7BC
		internal static void WriteMetadataSchema(SerializationActivityContext context, IMetadataSchemaWriter writer)
		{
			using (writer.CreateMetadataObjectScope(ObjectType.Role, null, "ModelRole object of Tabular Object Model (TOM)", new bool?(false)))
			{
				if (writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
				{
					writer.WriteProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, typeof(string));
				}
				if (writer.ShouldIncludeProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
				{
					writer.WriteProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, typeof(string));
				}
				if (writer.ShouldIncludeProperty("modelPermission", MetadataPropertyNature.RegularProperty))
				{
					writer.WriteEnumProperty<ModelPermission>("modelPermission", MetadataPropertyNature.RegularProperty, null);
				}
				if (writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
				{
					writer.WriteProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, typeof(DateTime));
				}
				if (writer.ShouldIncludeProperty("members", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "members", MetadataPropertyNature.ChildCollection, ObjectType.RoleMembership);
				}
				if (writer.ShouldIncludeProperty("tablePermissions", MetadataPropertyNature.ChildCollection))
				{
					writer.WriteChildCollection(context, "tablePermissions", MetadataPropertyNature.ChildCollection, ObjectType.TablePermission);
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

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x00040734 File Offset: 0x0003E934
		// (set) Token: 0x06000781 RID: 1921 RVA: 0x0004073C File Offset: 0x0003E93C
		internal override IMetadataObjectBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (ModelRole.ObjectBody)value;
			}
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0004074A File Offset: 0x0003E94A
		internal override ITxObjectBody CreateBody()
		{
			return new ModelRole.ObjectBody(this);
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00040752 File Offset: 0x0003E952
		internal override MetadataObject CreateObjectOfSameType()
		{
			return new ModelRole();
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x00040759 File Offset: 0x0003E959
		internal override IMetadataObjectCollection GetParentCollection(MetadataObject parent)
		{
			return ((Model)parent).Roles;
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00040768 File Offset: 0x0003E968
		internal override void ResolveLinks(IDictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
			Model model = MetadataObject.ResolveMetadataObjectParentById<ModelRole, Model>(this.body.ModelID, objectMap, throwIfCantResolve, null, null);
			if (model != null)
			{
				model.Roles.Add(this);
			}
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00040799 File Offset: 0x0003E999
		internal override void ResolveCrossLinks(Dictionary<ObjectId, MetadataObject> objectMap, bool throwIfCantResolve)
		{
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0004079B File Offset: 0x0003E99B
		internal override IEnumerable<IMetadataObjectCollection> GetChildrenCollections(bool isLogicalStructure)
		{
			yield return this._Members;
			yield return this._TablePermissions;
			yield return this._Annotations;
			yield return this._ExtendedProperties;
			yield break;
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x000407AB File Offset: 0x0003E9AB
		public ModelRoleMemberCollection Members
		{
			get
			{
				return this._Members;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000789 RID: 1929 RVA: 0x000407B3 File Offset: 0x0003E9B3
		public TablePermissionCollection TablePermissions
		{
			get
			{
				return this._TablePermissions;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x000407BB File Offset: 0x0003E9BB
		public ModelRoleAnnotationCollection Annotations
		{
			get
			{
				return this._Annotations;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600078B RID: 1931 RVA: 0x000407C3 File Offset: 0x0003E9C3
		[CompatibilityRequirement("1400")]
		public ModelRoleExtendedPropertyCollection ExtendedProperties
		{
			get
			{
				return this._ExtendedProperties;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600078C RID: 1932 RVA: 0x000407CB File Offset: 0x0003E9CB
		// (set) Token: 0x0600078D RID: 1933 RVA: 0x000407D8 File Offset: 0x0003E9D8
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
					if (!Utils.IsSyntacticallyValidName(value, ObjectType.Role, out text))
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

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x0004085B File Offset: 0x0003EA5B
		// (set) Token: 0x0600078F RID: 1935 RVA: 0x00040868 File Offset: 0x0003EA68
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

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x000408D8 File Offset: 0x0003EAD8
		// (set) Token: 0x06000791 RID: 1937 RVA: 0x000408E8 File Offset: 0x0003EAE8
		public ModelPermission ModelPermission
		{
			get
			{
				return this.body.ModelPermission;
			}
			set
			{
				if (!PropertyHelper.AreValuesIdentical(this.body.ModelPermission, value))
				{
					ObjectChangeTracker.RegisterPropertyChanging(this, "ModelPermission", typeof(ModelPermission), this.body.ModelPermission, value);
					ModelPermission modelPermission = this.body.ModelPermission;
					this.body.ModelPermission = value;
					ObjectChangeTracker.RegisterPropertyChanged(this, "ModelPermission", typeof(ModelPermission), modelPermission, value);
				}
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x0004096C File Offset: 0x0003EB6C
		// (set) Token: 0x06000793 RID: 1939 RVA: 0x0004097C File Offset: 0x0003EB7C
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

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x00040A00 File Offset: 0x0003EC00
		// (set) Token: 0x06000795 RID: 1941 RVA: 0x00040A12 File Offset: 0x0003EC12
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

		// Token: 0x06000796 RID: 1942 RVA: 0x00040A28 File Offset: 0x0003EC28
		internal void CopyFrom(ModelRole other, CopyContext context)
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
				this.Members.CopyFrom(other.Members, context);
				this.TablePermissions.CopyFrom(other.TablePermissions, context);
				this.Annotations.CopyFrom(other.Annotations, context);
				this.ExtendedProperties.CopyFrom(other.ExtendedProperties, context);
			}
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00040B0E File Offset: 0x0003ED0E
		internal override void CopyFrom(MetadataObject other, CopyContext context)
		{
			this.CopyFrom((ModelRole)other, context);
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x00040B1D File Offset: 0x0003ED1D
		[Obsolete("Deprecated. Use CopyTo method instead.", false)]
		public void CopyFrom(ModelRole other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			base.CopyFrom(other, CopyFlags.UserCopy);
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x00040B39 File Offset: 0x0003ED39
		public void CopyTo(ModelRole other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			other.CopyFrom(this, CopyFlags.UserCopy);
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00040B55 File Offset: 0x0003ED55
		public ModelRole Clone()
		{
			return base.CloneInternal<ModelRole>();
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x00040B60 File Offset: 0x0003ED60
		internal override void WriteAllBodyProperties(IPropertyWriter writer, WriteOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.WriteAllBodyProperties(writer, options, mode, dbCompatibilityLevel);
			if (!string.IsNullOrEmpty(this.body.Name))
			{
				writer.WriteProperty<string>(options, "Name", this.body.Name);
			}
			if (!string.IsNullOrEmpty(this.body.Description))
			{
				writer.WriteProperty<string>(options, "Description", this.body.Description);
			}
			if (this.body.ModelPermission != ModelPermission.None)
			{
				writer.WriteProperty<ModelPermission>(options, "ModelPermission", this.body.ModelPermission);
			}
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00040BF0 File Offset: 0x0003EDF0
		internal override void ReadAllBodyProperties(IPropertyReader reader, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			base.ReadAllBodyProperties(reader, mode, dbCompatibilityLevel);
			ObjectId objectId;
			if (reader.TryReadProperty<ObjectId>("ModelID", out objectId))
			{
				this.body.ModelID.ObjectID = objectId;
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
			ModelPermission modelPermission;
			if (reader.TryReadProperty<ModelPermission>("ModelPermission", out modelPermission))
			{
				this.body.ModelPermission = modelPermission;
			}
			DateTime dateTime;
			if (reader.TryReadProperty<DateTime>("ModifiedTime", out dateTime))
			{
				this.body.ModifiedTime = dateTime;
			}
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00040C94 File Offset: 0x0003EE94
		private protected override void WriteMetadataBodyProperties(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataBodyProperties(context, writer);
			if (!string.IsNullOrEmpty(this.body.Name) && writer.ShouldIncludeProperty("Name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("Name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.Name);
			}
			if (!string.IsNullOrEmpty(this.body.Description) && writer.ShouldIncludeProperty("Description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("Description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, this.body.Description);
			}
			if (this.body.ModelPermission != ModelPermission.None && writer.ShouldIncludeProperty("ModelPermission", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteEnumProperty<ModelPermission>("ModelPermission", MetadataPropertyNature.RegularProperty, this.body.ModelPermission);
			}
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00040D5C File Offset: 0x0003EF5C
		private protected override void WriteMetadataTree(SerializationActivityContext context, IMetadataWriter writer)
		{
			base.WriteMetadataTree(context, writer);
			if (!string.IsNullOrEmpty(this.body.Name) && writer.ShouldIncludeProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable))
			{
				writer.WriteStringProperty("name", MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable, this.body.Name);
			}
			if (!string.IsNullOrEmpty(this.body.Description) && writer.ShouldIncludeProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString))
			{
				writer.WriteStringProperty("description", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.Translatable | MetadataPropertyNature.MultilineString, this.body.Description);
			}
			if (this.body.ModelPermission != ModelPermission.None && writer.ShouldIncludeProperty("modelPermission", MetadataPropertyNature.RegularProperty))
			{
				writer.WriteEnumProperty<ModelPermission>("modelPermission", MetadataPropertyNature.RegularProperty, this.body.ModelPermission);
			}
			if (this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0 && writer.ShouldIncludeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp))
			{
				writer.WriteDateTimeProperty("modifiedTime", MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly | MetadataPropertyNature.Inferred | MetadataPropertyNature.Timestamp, this.body.ModifiedTime);
			}
			if (this.Members.Count > 0 && writer.ShouldIncludeProperty("members", MetadataPropertyNature.ChildCollection))
			{
				writer.WriteChildCollection(context, "members", MetadataPropertyNature.ChildCollection, this.Members);
			}
			if (this.TablePermissions.Count > 0 && writer.ShouldIncludeProperty("tablePermissions", MetadataPropertyNature.ChildCollection))
			{
				writer.WriteChildCollection(context, "tablePermissions", MetadataPropertyNature.ChildCollection, this.TablePermissions);
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

		// Token: 0x0600079F RID: 1951 RVA: 0x00040F68 File Offset: 0x0003F168
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
				if (length == 4)
				{
					char c = propertyName[0];
					if (c != 'N')
					{
						if (c != 'n')
						{
							goto IL_0492;
						}
						if (!(propertyName == "name"))
						{
							goto IL_0492;
						}
					}
					else if (!(propertyName == "Name"))
					{
						goto IL_0492;
					}
					this.body.Name = reader.ReadStringProperty();
					return true;
				}
				if (length != 7)
				{
					switch (length)
					{
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
					case 15:
					{
						char c = propertyName[0];
						if (c != 'M')
						{
							if (c != 'm')
							{
								break;
							}
							if (!(propertyName == "modelPermission"))
							{
								break;
							}
						}
						else if (!(propertyName == "ModelPermission"))
						{
							break;
						}
						this.body.ModelPermission = reader.ReadEnumProperty<ModelPermission>();
						return true;
					}
					case 16:
						if (propertyName == "tablePermissions")
						{
							using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
							{
								foreach (TablePermission tablePermission in reader.ReadChildCollectionProperty<TablePermission>(context))
								{
									try
									{
										this.TablePermissions.Add(tablePermission);
									}
									catch (Exception ex2)
									{
										throw reader.CreateInvalidChildException(context, tablePermission, TomSR.Exception_FailedAddDeserializedNamedObject("TablePermission", (tablePermission != null) ? tablePermission.Name : null, ex2.Message), ex2);
									}
								}
							}
							return true;
						}
						break;
					case 18:
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
						break;
					}
				}
				else
				{
					char c = propertyName[0];
					if (c != 'M')
					{
						if (c == 'm')
						{
							if (propertyName == "members")
							{
								using (new SerializationActivityInfoScope(context, "SerializationActivity::ChildDeserialization"))
								{
									foreach (ModelRoleMember modelRoleMember in reader.ReadChildCollectionProperty<ModelRoleMember>(context))
									{
										try
										{
											this.Members.Add(modelRoleMember);
										}
										catch (Exception ex4)
										{
											throw reader.CreateInvalidChildException(context, modelRoleMember, TomSR.Exception_FailedAddDeserializedNamedObject("ModelRoleMember", (modelRoleMember != null) ? modelRoleMember.Name : null, ex4.Message), ex4);
										}
									}
								}
								return true;
							}
						}
					}
					else if (propertyName == "ModelID")
					{
						this.body.ModelID.ObjectID = reader.ReadObjectIdProperty();
						return true;
					}
				}
			}
			IL_0492:
			classification = UnexpectedPropertyClassification.UnknownProperty;
			return false;
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x000414A0 File Offset: 0x0003F6A0
		[Obsolete("Deprecated. Use RequestRename method instead.", false)]
		public void Rename(string newName)
		{
			this.RequestRename(newName);
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x000414A9 File Offset: 0x0003F6A9
		public void RequestRename(string newName)
		{
			ObjectChangeTracker.RegisterObjectRenaming(this);
			this.Name = newName;
			this.body.RenameRequestedThroughAPI = true;
			ObjectChangeTracker.RegisterObjectRenamed(this);
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x000414CC File Offset: 0x0003F6CC
		internal override void SerializeToJsonObject(JsonObject result, SerializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			if (!string.IsNullOrEmpty(this.body.Name))
			{
				result["name", TomPropCategory.Name, 2, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Name, SplitMultilineOptions.None);
			}
			if (!string.IsNullOrEmpty(this.body.Description))
			{
				result["description", TomPropCategory.Regular, 3, false] = JsonPropertyHelper.ConvertStringToJsonValue(this.body.Description, options.SplitMultilineStrings ? SplitMultilineOptions.Split : SplitMultilineOptions.None);
			}
			if (!options.IncludeTranslatablePropertiesOnly && this.body.ModelPermission != ModelPermission.None)
			{
				result["modelPermission", TomPropCategory.Regular, 4, false] = JsonPropertyHelper.ConvertEnumToJsonValue<ModelPermission>(this.body.ModelPermission);
			}
			if (!options.IncludeTranslatablePropertiesOnly && !options.IgnoreTimestamps && !options.IgnoreInferredProperties && this.body.ModifiedTime.CompareTo(DateTime.MinValue) != 0)
			{
				result["modifiedTime", TomPropCategory.Regular, 5, true] = JsonPropertyHelper.ConvertPrimitiveToJsonValue<DateTime>(this.body.ModifiedTime);
			}
			if (!options.IgnoreChildren && !options.IgnoreChildrenExceptAnnotations)
			{
				if (!options.IncludeTranslatablePropertiesOnly)
				{
					IEnumerable<ModelRoleMember> enumerable;
					if (!options.IgnoreInferredObjects)
					{
						IEnumerable<ModelRoleMember> members = this.Members;
						enumerable = members;
					}
					else
					{
						enumerable = this.Members.Where((ModelRoleMember o) => !ObjectTreeHelper.IsInferredObject(o));
					}
					IEnumerable<ModelRoleMember> enumerable2 = enumerable;
					if (enumerable2.Any<ModelRoleMember>())
					{
						object[] array = enumerable2.Select((ModelRoleMember obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
						object[] array2 = array;
						result["members", TomPropCategory.ChildCollection, 34, false] = array2;
					}
				}
				if (!options.IncludeTranslatablePropertiesOnly)
				{
					IEnumerable<TablePermission> enumerable3;
					if (!options.IgnoreInferredObjects)
					{
						IEnumerable<TablePermission> tablePermissions = this.TablePermissions;
						enumerable3 = tablePermissions;
					}
					else
					{
						enumerable3 = this.TablePermissions.Where((TablePermission o) => !ObjectTreeHelper.IsInferredObject(o));
					}
					IEnumerable<TablePermission> enumerable4 = enumerable3;
					if (enumerable4.Any<TablePermission>())
					{
						object[] array = enumerable4.Select((TablePermission obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
						object[] array3 = array;
						result["tablePermissions", TomPropCategory.ChildCollection, 35, false] = array3;
					}
				}
				if (!options.IncludeTranslatablePropertiesOnly)
				{
					IEnumerable<ExtendedProperty> enumerable5;
					if (!options.IgnoreInferredObjects)
					{
						IEnumerable<ExtendedProperty> extendedProperties = this.ExtendedProperties;
						enumerable5 = extendedProperties;
					}
					else
					{
						enumerable5 = this.ExtendedProperties.Where((ExtendedProperty o) => !ObjectTreeHelper.IsInferredObject(o));
					}
					IEnumerable<ExtendedProperty> enumerable6 = enumerable5;
					if (enumerable6.Any<ExtendedProperty>())
					{
						if (!CompatibilityRestrictions.ExtendedProperty.IsCompatible(mode, dbCompatibilityLevel))
						{
							throw TomInternalException.Create("A child ExtendedProperty is incompatible with the restrictions at {0} mode and compatibility-level {1}", new object[] { mode, dbCompatibilityLevel });
						}
						object[] array = enumerable6.Select((ExtendedProperty obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
						object[] array4 = array;
						result["extendedProperties", TomPropCategory.ChildCollection, 39, false] = array4;
					}
				}
			}
			if (!options.IgnoreChildren && !options.IncludeTranslatablePropertiesOnly && this.Annotations.Any<Annotation>())
			{
				object[] array = this.Annotations.Select((Annotation obj) => obj.SerializeToNewJsonObject(options, mode, dbCompatibilityLevel).ToDictObject()).ToArray<IDictionary<string, object>>();
				object[] array5 = array;
				result["annotations", TomPropCategory.ChildCollection, 1000, false] = array5;
			}
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x00041858 File Offset: 0x0003FA58
		internal override bool ReadPropertyFromJson(JProperty jsonProp, DeserializeOptions options, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			string name = jsonProp.Name;
			if (name != null)
			{
				int length = name.Length;
				if (length != 4)
				{
					if (length != 7)
					{
						switch (length)
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
							if (name == "modifiedTime")
							{
								this.body.ModifiedTime = JsonPropertyHelper.ConvertJsonValueToPrimitive<DateTime>(jsonProp.Value);
								return true;
							}
							break;
						case 15:
							if (name == "modelPermission")
							{
								this.body.ModelPermission = JsonPropertyHelper.ConvertJsonValueToEnum<ModelPermission>(jsonProp.Value);
								return true;
							}
							break;
						case 16:
							if (name == "tablePermissions")
							{
								JsonPropertyHelper.ReadObjectCollection(this.TablePermissions, jsonProp.Value, options, mode, dbCompatibilityLevel);
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
						}
					}
					else if (name == "members")
					{
						JsonPropertyHelper.ReadObjectCollection(this.Members, jsonProp.Value, options, mode, dbCompatibilityLevel);
						return true;
					}
				}
				else if (name == "name")
				{
					this.body.Name = JsonPropertyHelper.ConvertJsonValueToString(jsonProp.Value);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x00041A4C File Offset: 0x0003FC4C
		internal override void OnAfterDeserialize(DeserializeOptions options)
		{
			foreach (MetadataObject metadataObject in base.GetAllDescendants())
			{
				metadataObject.BuildIndirectNameCrossLinkPathIfNeeded();
			}
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00041A98 File Offset: 0x0003FC98
		internal override string GetFormattedObjectPath()
		{
			return TomSR.ObjectPath_Role_1Arg(this.Name);
		}

		// Token: 0x04000137 RID: 311
		internal ModelRole.ObjectBody body;

		// Token: 0x04000138 RID: 312
		private ModelRoleMemberCollection _Members;

		// Token: 0x04000139 RID: 313
		private TablePermissionCollection _TablePermissions;

		// Token: 0x0400013A RID: 314
		private ModelRoleAnnotationCollection _Annotations;

		// Token: 0x0400013B RID: 315
		private ModelRoleExtendedPropertyCollection _ExtendedProperties;

		// Token: 0x0200028C RID: 652
		internal class ObjectBody : NamedMetadataObjectBody<ModelRole>
		{
			// Token: 0x06002157 RID: 8535 RVA: 0x000D90CB File Offset: 0x000D72CB
			public ObjectBody(ModelRole owner)
				: base(owner)
			{
				this.ModifiedTime = DateTime.MinValue;
				this.ModelID = new ParentLink<ModelRole, Model>(owner, "Model");
			}

			// Token: 0x06002158 RID: 8536 RVA: 0x000D90F0 File Offset: 0x000D72F0
			public override string GetObjectName()
			{
				return this.Name;
			}

			// Token: 0x06002159 RID: 8537 RVA: 0x000D90F8 File Offset: 0x000D72F8
			internal bool IsEqualTo(ModelRole.ObjectBody other, CopyContext context)
			{
				return PropertyHelper.AreValuesIdentical(this.Name, other.Name) && PropertyHelper.AreValuesIdentical(this.Description, other.Description) && PropertyHelper.AreValuesIdentical(this.ModelPermission, other.ModelPermission) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime)) && ((context.Flags & CopyFlags.IgnoreInferredProperties) == CopyFlags.IgnoreInferredProperties || this.ModelID.IsEqualTo(other.ModelID, context));
			}

			// Token: 0x0600215A RID: 8538 RVA: 0x000D9198 File Offset: 0x000D7398
			internal void CopyFromImpl(ModelRole.ObjectBody other, CopyContext context)
			{
				if ((context.Flags & CopyFlags.CloningBody) == CopyFlags.CloningBody)
				{
					this.CopyFromImpl(other);
					return;
				}
				base.Owner.Name = other.Name;
				this.Description = other.Description;
				this.ModelPermission = other.ModelPermission;
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ModifiedTime = other.ModifiedTime;
				}
				if ((context.Flags & CopyFlags.IgnoreInferredProperties) != CopyFlags.IgnoreInferredProperties)
				{
					this.ModelID.CopyFrom(other.ModelID, context);
				}
			}

			// Token: 0x0600215B RID: 8539 RVA: 0x000D9228 File Offset: 0x000D7428
			internal void CopyFromImpl(ModelRole.ObjectBody other)
			{
				this.Name = other.Name;
				this.Description = other.Description;
				this.ModelPermission = other.ModelPermission;
				this.ModifiedTime = other.ModifiedTime;
				this.ModelID.CopyFrom(other.ModelID, ObjectChangeTracker.BodyCloneContext);
			}

			// Token: 0x0600215C RID: 8540 RVA: 0x000D927B File Offset: 0x000D747B
			public override void CopyFrom(MetadataObjectBody<ModelRole> other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.CopyFromImpl((ModelRole.ObjectBody)other, context);
			}

			// Token: 0x0600215D RID: 8541 RVA: 0x000D9294 File Offset: 0x000D7494
			internal bool IsEqualTo(ModelRole.ObjectBody other)
			{
				return PropertyHelper.AreValuesIdentical(this.Name, other.Name) && PropertyHelper.AreValuesIdentical(this.Description, other.Description) && PropertyHelper.AreValuesIdentical(this.ModelPermission, other.ModelPermission) && PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime) && this.ModelID.IsEqualTo(other.ModelID);
			}

			// Token: 0x0600215E RID: 8542 RVA: 0x000D930B File Offset: 0x000D750B
			public override bool IsEqualTo(IMetadataObjectBody other)
			{
				return base.IsEqualTo(other) && this.IsEqualTo((ModelRole.ObjectBody)other);
			}

			// Token: 0x0600215F RID: 8543 RVA: 0x000D9324 File Offset: 0x000D7524
			internal void CompareWith(ModelRole.ObjectBody other, CompareContext context)
			{
				if (!PropertyHelper.AreValuesIdentical(this.Name, other.Name))
				{
					context.RegisterPropertyChange(base.Owner, "Name", typeof(string), base.RenameRequestedThroughAPI ? PropertyFlags.User : PropertyFlags.DdlAndUser, other.Name, this.Name);
				}
				if (!PropertyHelper.AreValuesIdentical(this.Description, other.Description))
				{
					context.RegisterPropertyChange(base.Owner, "Description", typeof(string), PropertyFlags.DdlAndUser, other.Description, this.Description);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ModelPermission, other.ModelPermission))
				{
					context.RegisterPropertyChange(base.Owner, "ModelPermission", typeof(ModelPermission), PropertyFlags.DdlAndUser, other.ModelPermission, this.ModelPermission);
				}
				if (!PropertyHelper.AreValuesIdentical(this.ModifiedTime, other.ModifiedTime))
				{
					context.RegisterPropertyChange(base.Owner, "ModifiedTime", typeof(DateTime), PropertyFlags.Ddl | PropertyFlags.User | PropertyFlags.ReadOnly, other.ModifiedTime, this.ModifiedTime);
				}
				this.ModelID.CompareWith(other.ModelID, "ModelID", "Model", PropertyFlags.ReadOnly, context);
			}

			// Token: 0x06002160 RID: 8544 RVA: 0x000D9459 File Offset: 0x000D7659
			public override void CompareWith(IMetadataObjectBody other, CompareContext context)
			{
				base.CompareWith(other, context);
				this.CompareWith((ModelRole.ObjectBody)other, context);
			}

			// Token: 0x04000921 RID: 2337
			internal string Name;

			// Token: 0x04000922 RID: 2338
			internal string Description;

			// Token: 0x04000923 RID: 2339
			internal ModelPermission ModelPermission;

			// Token: 0x04000924 RID: 2340
			internal DateTime ModifiedTime;

			// Token: 0x04000925 RID: 2341
			internal ParentLink<ModelRole, Model> ModelID;
		}
	}
}
