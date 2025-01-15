using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json.Linq;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Tmdl;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x0200016A RID: 362
	internal sealed class TmdlObjectWriter : IMetadataWriter
	{
		// Token: 0x06001701 RID: 5889 RVA: 0x0009D79A File Offset: 0x0009B99A
		public TmdlObjectWriter(TmdlSerializationConfiguration config, ObjectType type)
		{
			this.config = config;
			this.rootObject = new TmdlObject(type);
			this.state = TmdlObjectWriter.WriteState.RootObject;
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x0009D7BC File Offset: 0x0009B9BC
		public bool ShouldIncludeProperty(string propertyName, MetadataPropertyNature propertyNature)
		{
			TmdlObjectWriter.WriteState writeState = this.state;
			if (writeState - TmdlObjectWriter.WriteState.RootObject <= 2)
			{
				return !this.config.Filter.IgnoreProperty(this.GetCurrentObjectType(), propertyName, propertyNature);
			}
			throw TomInternalException.Create("Invalid state for writing a new property - state={0}, valid states=[RootObject, ComplexProperty, ComplexPropertyCollection]", new object[] { this.state });
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x0009D810 File Offset: 0x0009BA10
		public void WriteObjectIdProperty(string propertyName, MetadataPropertyNature propertyNature, MetadataObject @object)
		{
			throw new TomInternalException("WriteObjectIdProperty should never be called in TMDL serialization");
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x0009D81C File Offset: 0x0009BA1C
		public void WriteObjectTypeProperty(string propertyName, MetadataPropertyNature propertyNature, ObjectType objectType)
		{
			throw new TomInternalException("WriteObjectTypeProperty should never be called in TMDL serialization");
		}

		// Token: 0x06001705 RID: 5893 RVA: 0x0009D828 File Offset: 0x0009BA28
		public void WriteStringProperty(string propertyName, MetadataPropertyNature propertyNature, string value)
		{
			TmdlObjectWriter.WriteState writeState = this.state;
			if (writeState - TmdlObjectWriter.WriteState.RootObject <= 1)
			{
				this.WriteStringPropertyImpl(propertyName, propertyNature, value);
				return;
			}
			throw TomInternalException.Create("Invalid state for writing a string property - state={0}, valid states=[RootObject, ComplexProperty]", new object[] { this.state });
		}

		// Token: 0x06001706 RID: 5894 RVA: 0x0009D86C File Offset: 0x0009BA6C
		public void WriteInt32Property(string propertyName, MetadataPropertyNature propertyNature, int value)
		{
			TmdlObjectWriter.WriteState writeState = this.state;
			if (writeState - TmdlObjectWriter.WriteState.RootObject <= 1)
			{
				this.WriteInt32PropertyImpl(propertyName, propertyNature, value);
				return;
			}
			throw TomInternalException.Create("Invalid state for writing an int property - state={0}, valid states=[RootObject, ComplexProperty]", new object[] { this.state });
		}

		// Token: 0x06001707 RID: 5895 RVA: 0x0009D8B0 File Offset: 0x0009BAB0
		public void WriteUInt32Property(string propertyName, MetadataPropertyNature propertyNature, uint value)
		{
			TmdlObjectWriter.WriteState writeState = this.state;
			if (writeState - TmdlObjectWriter.WriteState.RootObject <= 1)
			{
				this.WriteUInt32PropertyImpl(propertyName, propertyNature, value);
				return;
			}
			throw TomInternalException.Create("Invalid state for writing an uint property - state={0}, valid states=[RootObject, ComplexProperty]", new object[] { this.state });
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x0009D8F4 File Offset: 0x0009BAF4
		public void WriteInt64Property(string propertyName, MetadataPropertyNature propertyNature, long value)
		{
			TmdlObjectWriter.WriteState writeState = this.state;
			if (writeState - TmdlObjectWriter.WriteState.RootObject <= 1)
			{
				this.WriteInt64PropertyImpl(propertyName, propertyNature, value);
				return;
			}
			throw TomInternalException.Create("Invalid state for writing a long property - state={0}, valid states=[RootObject, ComplexProperty]", new object[] { this.state });
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x0009D938 File Offset: 0x0009BB38
		public void WriteUInt64Property(string propertyName, MetadataPropertyNature propertyNature, ulong value)
		{
			TmdlObjectWriter.WriteState writeState = this.state;
			if (writeState - TmdlObjectWriter.WriteState.RootObject <= 1)
			{
				this.WriteUInt64PropertyImpl(propertyName, propertyNature, value);
				return;
			}
			throw TomInternalException.Create("Invalid state for writing a ulong property - state={0}, valid states=[RootObject, ComplexProperty]", new object[] { this.state });
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x0009D97C File Offset: 0x0009BB7C
		public void WriteBooleanProperty(string propertyName, MetadataPropertyNature propertyNature, bool value)
		{
			TmdlObjectWriter.WriteState writeState = this.state;
			if (writeState - TmdlObjectWriter.WriteState.RootObject <= 1)
			{
				this.WriteBooleanPropertyImpl(propertyName, propertyNature, value);
				return;
			}
			throw TomInternalException.Create("Invalid state for writing a bool property - state={0}, valid states=[RootObject, ComplexProperty]", new object[] { this.state });
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x0009D9C0 File Offset: 0x0009BBC0
		public void WriteDoubleProperty(string propertyName, MetadataPropertyNature propertyNature, double value)
		{
			TmdlObjectWriter.WriteState writeState = this.state;
			if (writeState - TmdlObjectWriter.WriteState.RootObject <= 1)
			{
				this.WriteDoublePropertyImpl(propertyName, propertyNature, value);
				return;
			}
			throw TomInternalException.Create("Invalid state for writing a double property - state={0}, valid states=[RootObject, ComplexProperty]", new object[] { this.state });
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x0009DA04 File Offset: 0x0009BC04
		public void WriteDateTimeProperty(string propertyName, MetadataPropertyNature propertyNature, DateTime value)
		{
			TmdlObjectWriter.WriteState writeState = this.state;
			if (writeState - TmdlObjectWriter.WriteState.RootObject <= 1)
			{
				this.WriteDateTimePropertyImpl(propertyName, propertyNature, value);
				return;
			}
			throw TomInternalException.Create("Invalid state for writing a DateTime property - state={0}, valid states=[RootObject, ComplexProperty]", new object[] { this.state });
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x0009DA48 File Offset: 0x0009BC48
		public void WriteEnumProperty<TEnum>(string propertyName, MetadataPropertyNature propertyNature, TEnum value) where TEnum : Enum
		{
			TmdlObjectWriter.WriteState writeState = this.state;
			if (writeState - TmdlObjectWriter.WriteState.RootObject <= 1)
			{
				this.WriteEnumPropertyImpl<TEnum>(propertyName, propertyNature, value);
				return;
			}
			throw TomInternalException.Create("Invalid state for writing an enum property - state={0}, valid states=[RootObject, ComplexProperty]", new object[] { this.state });
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x0009DA8C File Offset: 0x0009BC8C
		public void WriteProperty(string propertyName, MetadataPropertyNature propertyNature, Type type, object value)
		{
			TmdlObjectWriter.WriteState writeState = this.state;
			if (writeState - TmdlObjectWriter.WriteState.RootObject <= 1)
			{
				this.WritePropertyImpl(propertyName, propertyNature, type, value);
				return;
			}
			throw TomInternalException.Create("Invalid state for writing a regular property - state={0}, valid states=[RootObject, ComplexProperty]", new object[] { this.state });
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x0009DAD0 File Offset: 0x0009BCD0
		public void WriteProperty<TValue>(string propertyName, MetadataPropertyNature propertyNature, TValue value)
		{
			TmdlObjectWriter.WriteState writeState = this.state;
			if (writeState - TmdlObjectWriter.WriteState.RootObject <= 1)
			{
				this.WritePropertyImpl(propertyName, propertyNature, typeof(TValue), value);
				return;
			}
			throw TomInternalException.Create("Invalid state for writing a regular property - state={0}, valid states=[RootObject, ComplexProperty]", new object[] { this.state });
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x0009DB24 File Offset: 0x0009BD24
		public void WriteCrossLinkProperty(string propertyName, MetadataPropertyNature propertyNature, ObjectPath value)
		{
			TmdlObjectWriter.WriteState writeState = this.state;
			if (writeState - TmdlObjectWriter.WriteState.RootObject <= 1)
			{
				this.WriteCrossLinkPropertyImpl(propertyName, propertyNature, value);
				return;
			}
			throw TomInternalException.Create("Invalid state for writing a cross-link property - state={0}, valid states=[RootObject, ComplexProperty]", new object[] { this.state });
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x0009DB68 File Offset: 0x0009BD68
		public void WriteCustomJsonProperty(string propertyName, MetadataPropertyNature propertyNature, JToken token)
		{
			if (this.state != TmdlObjectWriter.WriteState.RootObject)
			{
				throw TomInternalException.Create("Invalid state for writing a custom JSON property property - state={0}, valid states=[RootObject]", new object[] { this.state });
			}
			TmdlObject tmdlObject = new TmdlObject(propertyName);
			ObjectType objectType = this.rootObject.ObjectType;
			if (objectType != ObjectType.Model)
			{
				if (objectType != ObjectType.DataSource)
				{
					throw TomInternalException.Create("Invalid custom JSON property - owner={0}, name='{1}'", new object[]
					{
						this.rootObject.ObjectType,
						propertyName
					});
				}
				if (!(propertyName == "connectionDetails"))
				{
					if (!(propertyName == "options"))
					{
						if (!(propertyName == "credential"))
						{
							throw TomInternalException.Create("Invalid custom JSON property - owner={0}, name='{1}'", new object[]
							{
								ObjectType.DataSource,
								propertyName
							});
						}
						TmdlObjectWriter.AddStructuredDataSourceCredentialProperties(tmdlObject, this.config.IncludeRestrictedInformation ? token : PropertyHelper.GetCuratedValueForCredential(token));
					}
					else
					{
						TmdlObjectWriter.AddStructuredDataSourceOptionsProperties(tmdlObject, token);
					}
				}
				else
				{
					TmdlObjectWriter.AddStructuredDataSourceConnectionDetailsProperties(tmdlObject, token);
				}
			}
			else if (!(propertyName == "dataAccessOptions"))
			{
				if (!(propertyName == "automaticAggregationOptions"))
				{
					throw TomInternalException.Create("Invalid custom JSON property - owner={0}, name='{1}'", new object[]
					{
						ObjectType.Model,
						propertyName
					});
				}
				TmdlObjectWriter.AddModelAutomaticAggregationOptionsProperties(tmdlObject, token);
			}
			else
			{
				TmdlObjectWriter.AddModelDataAccessOptionsProperties(tmdlObject, token);
			}
			if (tmdlObject.HasAnyProperty(true))
			{
				this.rootObject.Children.Add(tmdlObject);
			}
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x0009DCC8 File Offset: 0x0009BEC8
		public void WriteSingleChild(SerializationActivityContext context, string propertyName, MetadataPropertyNature propertyNature, MetadataObject @object)
		{
			if (this.state != TmdlObjectWriter.WriteState.RootObject)
			{
				throw TomInternalException.Create("Invalid state for writing a single-child property - state={0}, valid states=[RootObject]", new object[] { this.state });
			}
			if (this.config.Filter.IgnoreChild(this.GetCurrentObjectType(), propertyName, propertyNature, @object))
			{
				return;
			}
			TmdlObjectWriter tmdlObjectWriter = new TmdlObjectWriter(this.config, @object.ObjectType);
			@object.SaveMetadata(context, tmdlObjectWriter);
			TmdlObject tmdlObject = tmdlObjectWriter.ExtractObject();
			if (string.Compare(@object.ObjectType.ToString("G"), propertyName, StringComparison.InvariantCultureIgnoreCase) != 0)
			{
				tmdlObject.Name = new ObjectName(new string[] { propertyName });
			}
			this.rootObject.Children.Add(tmdlObject);
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x0009DD84 File Offset: 0x0009BF84
		public void WriteChildCollection(SerializationActivityContext context, string propertyName, MetadataPropertyNature propertyNature, IEnumerable<MetadataObject> objects)
		{
			if (this.state != TmdlObjectWriter.WriteState.RootObject)
			{
				throw TomInternalException.Create("Invalid state for writing a child-collection property - state={0}, valid states=[RootObject]", new object[] { this.state });
			}
			ObjectType objectType2 = this.rootObject.ObjectType;
			bool flag;
			if (objectType2 <= ObjectType.Hierarchy)
			{
				if (objectType2 == ObjectType.Table)
				{
					if (string.Compare(propertyName, "partitions", StringComparison.Ordinal) == 0 && this.rootObject.HasAnyChild(false))
					{
						if (this.rootObject.Children.Any((TmdlObject c) => c.ObjectType == ObjectType.CalculationGroup))
						{
							List<MetadataObject> list = new List<MetadataObject>();
							foreach (MetadataObject metadataObject in objects)
							{
								Partition partition = metadataObject as Partition;
								if (partition == null)
								{
									throw TomInternalException.Create("Only partitions are expected to be included in the {0} child collection; we encountered a '{1}' object", new object[]
									{
										propertyName,
										metadataObject.GetType().Name
									});
								}
								if (partition.SourceType != PartitionSourceType.CalculationGroup || partition.Mode != ModeType.Import)
								{
									list.Add(partition);
								}
							}
							if (list.Count == 0)
							{
								return;
							}
							objects = list;
						}
					}
					flag = false;
					goto IL_05EA;
				}
				if (objectType2 == ObjectType.Hierarchy)
				{
					flag = string.Compare(propertyName, "levels", StringComparison.Ordinal) == 0;
					goto IL_05EA;
				}
			}
			else if (objectType2 != ObjectType.Role)
			{
				if (objectType2 != ObjectType.RelatedColumnDetails)
				{
					if (objectType2 == ObjectType.CalculationGroup)
					{
						flag = string.Compare(propertyName, "calculationItems", StringComparison.Ordinal) == 0;
						goto IL_05EA;
					}
				}
				else
				{
					if (string.Compare(propertyName, "groupByColumns", StringComparison.Ordinal) == 0)
					{
						Utils.Verify(!objects.Any((MetadataObject c) => c.ObjectType != ObjectType.GroupByColumn));
						IEnumerable<GroupByColumn> enumerable = objects.Select((MetadataObject c) => (GroupByColumn)c);
						Func<GroupByColumn, bool> <>9__8;
						Func<GroupByColumn, bool> func;
						if ((func = <>9__8) == null)
						{
							func = (<>9__8 = (GroupByColumn o) => o.GroupingColumn != null && !this.config.Filter.IgnoreChild(ObjectType.RelatedColumnDetails, propertyName, propertyNature, o));
						}
						foreach (GroupByColumn groupByColumn in enumerable.Where(func))
						{
							this.rootObject.Properties.Add(new TmdlProperty("groupByColumn", new TmdlModelReferenceValue(new ObjectName(new string[] { groupByColumn.GroupingColumn.Name }))));
						}
						return;
					}
					flag = false;
					goto IL_05EA;
				}
			}
			else
			{
				if (string.Compare(propertyName, "members", StringComparison.Ordinal) == 0)
				{
					Utils.Verify(!objects.Any((MetadataObject c) => c.ObjectType != ObjectType.RoleMembership));
					new TmdlCollectionValue();
					IEnumerable<ModelRoleMember> enumerable2 = objects.Select((MetadataObject c) => (ModelRoleMember)c);
					Func<ModelRoleMember, bool> <>9__4;
					Func<ModelRoleMember, bool> func2;
					if ((func2 = <>9__4) == null)
					{
						func2 = (<>9__4 = (ModelRoleMember o) => !string.IsNullOrEmpty(o.MemberName) && !this.config.Filter.IgnoreChild(ObjectType.Role, propertyName, propertyNature, o));
					}
					Func<ExtendedProperty, bool> <>9__5;
					Func<Annotation, bool> <>9__6;
					foreach (ModelRoleMember modelRoleMember in enumerable2.Where(func2))
					{
						TmdlObject tmdlObject = new TmdlObject(ObjectType.RoleMembership);
						tmdlObject.Name = new ObjectName(new string[] { modelRoleMember.MemberName });
						if (modelRoleMember is WindowsModelRoleMember)
						{
							tmdlObject.DefaultProperty = new TmdlProperty("memberType", TmdlValue.FromEnum<TmdlRoleMemberType>(TmdlRoleMemberType.ActiveDirectory));
						}
						else
						{
							ExternalModelRoleMember externalModelRoleMember = modelRoleMember as ExternalModelRoleMember;
							if (externalModelRoleMember != null)
							{
								switch (externalModelRoleMember.MemberType)
								{
								case RoleMemberType.Auto:
									goto IL_02B0;
								case RoleMemberType.User:
									break;
								case RoleMemberType.Group:
									tmdlObject.DefaultProperty = new TmdlProperty("memberType", TmdlValue.FromEnum<TmdlRoleMemberType>(TmdlRoleMemberType.Group));
									break;
								default:
									goto IL_02B0;
								}
								IL_02C7:
								if (string.Compare(externalModelRoleMember.IdentityProvider, "AzureAD", StringComparison.InvariantCulture) != 0)
								{
									tmdlObject.Properties.Add(new TmdlProperty("identityProvider", TmdlValue.FromString(externalModelRoleMember.IdentityProvider)));
									goto IL_02FD;
								}
								goto IL_02FD;
								IL_02B0:
								tmdlObject.DefaultProperty = new TmdlProperty("memberType", TmdlValue.FromEnum<TmdlRoleMemberType>(TmdlRoleMemberType.Auto));
								goto IL_02C7;
							}
						}
						IL_02FD:
						if (CompatibilityRestrictions.ExtendedProperty.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && !this.config.Filter.IgnoreProperty(ObjectType.RoleMembership, "extendedProperties", MetadataPropertyNature.ChildCollection) && modelRoleMember.ExtendedProperties.Count > 0)
						{
							IEnumerable<ExtendedProperty> extendedProperties = modelRoleMember.ExtendedProperties;
							Func<ExtendedProperty, bool> func3;
							if ((func3 = <>9__5) == null)
							{
								func3 = (<>9__5 = (ExtendedProperty o) => !this.config.Filter.IgnoreChild(ObjectType.RoleMembership, "extendedProperties", MetadataPropertyNature.ChildCollection, o));
							}
							foreach (MetadataObject metadataObject2 in extendedProperties.Where(func3))
							{
								TmdlObjectWriter tmdlObjectWriter = new TmdlObjectWriter(this.config, metadataObject2.ObjectType);
								metadataObject2.SaveMetadata(context, tmdlObjectWriter);
								TmdlObject tmdlObject2 = tmdlObjectWriter.ExtractObject();
								tmdlObject.Children.Add(tmdlObject2);
							}
						}
						if (!this.config.Filter.IgnoreProperty(ObjectType.RoleMembership, "annotations", MetadataPropertyNature.ChildCollection) && modelRoleMember.Annotations.Count > 0)
						{
							IEnumerable<Annotation> annotations = modelRoleMember.Annotations;
							Func<Annotation, bool> func4;
							if ((func4 = <>9__6) == null)
							{
								func4 = (<>9__6 = (Annotation o) => !this.config.Filter.IgnoreChild(ObjectType.RoleMembership, "annotations", MetadataPropertyNature.ChildCollection, o));
							}
							foreach (MetadataObject metadataObject3 in annotations.Where(func4))
							{
								TmdlObjectWriter tmdlObjectWriter2 = new TmdlObjectWriter(this.config, metadataObject3.ObjectType);
								metadataObject3.SaveMetadata(context, tmdlObjectWriter2);
								TmdlObject tmdlObject3 = tmdlObjectWriter2.ExtractObject();
								tmdlObject.Children.Add(tmdlObject3);
							}
						}
						this.rootObject.Children.Add(tmdlObject);
					}
					return;
				}
				flag = false;
				goto IL_05EA;
			}
			flag = false;
			IL_05EA:
			ObjectType objectType = this.GetCurrentObjectType();
			if (flag)
			{
				List<TmdlObject> list2 = new List<TmdlObject>();
				IEnumerable<MetadataObject> enumerable3 = objects;
				Func<MetadataObject, bool> <>9__9;
				Func<MetadataObject, bool> func5;
				if ((func5 = <>9__9) == null)
				{
					func5 = (<>9__9 = (MetadataObject o) => !this.config.Filter.IgnoreChild(objectType, propertyName, propertyNature, o));
				}
				foreach (MetadataObject metadataObject4 in enumerable3.Where(func5))
				{
					TmdlObjectWriter tmdlObjectWriter3 = new TmdlObjectWriter(this.config, metadataObject4.ObjectType);
					metadataObject4.SaveMetadata(context, tmdlObjectWriter3);
					list2.Add(tmdlObjectWriter3.ExtractObject());
				}
				list2.Sort(TmdlObjectWriter.sortByOrdinal);
				for (int i = 0; i < list2.Count; i++)
				{
					this.rootObject.Children.Add(list2[i]);
				}
				return;
			}
			IEnumerable<MetadataObject> enumerable4 = objects;
			Func<MetadataObject, bool> <>9__10;
			Func<MetadataObject, bool> func6;
			if ((func6 = <>9__10) == null)
			{
				func6 = (<>9__10 = (MetadataObject o) => !this.config.Filter.IgnoreChild(objectType, propertyName, propertyNature, o));
			}
			foreach (MetadataObject metadataObject5 in enumerable4.Where(func6))
			{
				TmdlObjectWriter tmdlObjectWriter4 = new TmdlObjectWriter(this.config, metadataObject5.ObjectType);
				metadataObject5.SaveMetadata(context, tmdlObjectWriter4);
				TmdlObject tmdlObject4 = tmdlObjectWriter4.ExtractObject();
				this.rootObject.Children.Add(tmdlObject4);
			}
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x0009E584 File Offset: 0x0009C784
		public void WriteComplexProperty(string propertyName, MetadataPropertyNature propertyNature, IEnumerable<MetadataProperty> properties)
		{
			TmdlObjectWriter.WriteState writeState = this.state;
			if (writeState - TmdlObjectWriter.WriteState.RootObject > 2)
			{
				throw TomInternalException.Create("Invalid state for writing a complex property - state={0}, valid states=[RootObject, ComplexProperty, ComplexPropertyCollection]", new object[] { this.state });
			}
			TmdlObjectWriter.ComplexProperty complexProperty;
			if (!this.IsValidComplexProperty(propertyName, propertyNature, out complexProperty))
			{
				throw TomInternalException.Create("A named complex property that is not a single-child of the current object - currentObjectType={0}, propertyName='{1}'", new object[]
				{
					this.GetCurrentObjectType(),
					propertyName
				});
			}
			this.PushComplexProperty(complexProperty);
			foreach (MetadataProperty metadataProperty in properties)
			{
				this.WritePropertyImpl(metadataProperty.Name, metadataProperty.Nature, metadataProperty.ValueType, metadataProperty.Value);
			}
			this.PopComplexProperty(out complexProperty);
			this.AddComplexPropertyToCurrentScope(complexProperty);
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x0009E658 File Offset: 0x0009C858
		public void StartComplexProperty(string propertyName, MetadataPropertyNature propertyNature)
		{
			TmdlObjectWriter.WriteState writeState = this.state;
			if (writeState - TmdlObjectWriter.WriteState.RootObject > 2)
			{
				throw TomInternalException.Create("Invalid state for starting a complex property - state={0}, valid states=[RootObject, ComplexProperty, ComplexPropertyCollection]", new object[] { this.state });
			}
			TmdlObjectWriter.ComplexProperty complexProperty;
			if (!this.IsValidComplexProperty(propertyName, propertyNature, out complexProperty))
			{
				throw TomInternalException.Create("A named complex property that is not a single-child of the current object - currentObjectType={0}, propertyName='{1}'", new object[]
				{
					this.GetCurrentObjectType(),
					propertyName
				});
			}
			this.PushComplexProperty(complexProperty);
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x0009E6C8 File Offset: 0x0009C8C8
		public void CompleteComplexProperty()
		{
			if (this.state != TmdlObjectWriter.WriteState.ComplexProperty)
			{
				throw TomInternalException.Create("Invalid state for completing a complex property - state={0}, valid states=[ComplexProperty]", new object[] { this.state });
			}
			TmdlObjectWriter.ComplexProperty complexProperty;
			this.PopComplexProperty(out complexProperty);
			this.AddComplexPropertyToCurrentScope(complexProperty);
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x0009E70C File Offset: 0x0009C90C
		public void StartComplexPropertyCollection(string collectionName, MetadataPropertyNature collectionNature)
		{
			TmdlObjectWriter.WriteState writeState = this.state;
			if (writeState - TmdlObjectWriter.WriteState.RootObject > 1)
			{
				throw TomInternalException.Create("Invalid state for starting a complex property collection - state={0}, valid states=[RootObject, ComplexProperty]", new object[] { this.state });
			}
			ObjectType currentObjectType = this.GetCurrentObjectType();
			ObjectType objectType;
			bool flag;
			if (ObjectTreeHelper.IsChildJsonPropertyName(currentObjectType, collectionName, out objectType, out flag))
			{
				this.PushComplexPropertyCollection(objectType, collectionName);
				return;
			}
			throw TomInternalException.Create("A complex property collection that is not a child-collection of the current object - currentObjectType={0}, collectionName='{1}'", new object[] { currentObjectType, collectionName });
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x0009E780 File Offset: 0x0009C980
		public void CompleteComplexPropertyCollection()
		{
			if (this.state != TmdlObjectWriter.WriteState.ComplexPropertyCollection)
			{
				throw TomInternalException.Create("Invalid state for completing a complex property collection - state={0}, valid states=[ComplexPropertyCollection]", new object[] { this.state });
			}
			string text;
			this.PopComplexPropertyCollection(out text);
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x0009E7C0 File Offset: 0x0009C9C0
		public TmdlObject ExtractObject()
		{
			Utils.Verify(this.state == TmdlObjectWriter.WriteState.RootObject);
			TmdlObject tmdlObject4;
			try
			{
				ObjectType objectType = this.rootObject.ObjectType;
				if (objectType <= ObjectType.PerspectiveTable)
				{
					switch (objectType)
					{
					case ObjectType.Model:
						if (this.rootObject.HasAnyChild(false))
						{
							TmdlObjectWriter.SortModelChildren(this.rootObject.Children);
							goto IL_0586;
						}
						goto IL_0586;
					case ObjectType.DataSource:
					case ObjectType.AttributeHierarchy:
						goto IL_0586;
					case ObjectType.Table:
					{
						if (!this.rootObject.HasAnyChild(true))
						{
							goto IL_0586;
						}
						if (this.rootObject.Children.Any((TmdlObject c) => c.ObjectType == ObjectType.CalculationGroup))
						{
							List<TmdlObject> list = new List<TmdlObject>(1);
							foreach (TmdlObject tmdlObject in this.rootObject.Children.Where((TmdlObject c) => c.ObjectType == ObjectType.Partition))
							{
								if (TmdlObjectReader.IsCalcGroupPartition(tmdlObject))
								{
									list.Add(tmdlObject);
								}
							}
							for (int i = 0; i < list.Count; i++)
							{
								this.rootObject.Children.Remove(list[i]);
							}
						}
						TmdlObjectWriter.SortTableChildren(this.rootObject.Children);
						if (!this.rootObject.Children.Any(delegate(TmdlObject c)
						{
							if (c.ObjectType == ObjectType.Partition && c.DefaultProperty != null && c.DefaultProperty.Value.Type == TmdlValueType.Scalar)
							{
								TmdlEnumValue tmdlEnumValue3 = c.DefaultProperty.Value as TmdlEnumValue;
								if (tmdlEnumValue3 != null && tmdlEnumValue3.Value != null)
								{
									object value3 = tmdlEnumValue3.Value;
									if (value3 is PartitionSourceType)
									{
										PartitionSourceType partitionSourceType = (PartitionSourceType)value3;
										return partitionSourceType == PartitionSourceType.Calculated;
									}
								}
							}
							return false;
						}))
						{
							goto IL_0586;
						}
						using (IEnumerator<TmdlObject> enumerator = this.rootObject.Children.Where((TmdlObject c) => c.ObjectType == ObjectType.Column).GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								TmdlObject tmdlObject2 = enumerator.Current;
								TmdlProperty propertyByName = tmdlObject2.GetPropertyByName("type", StringComparison.Ordinal);
								if (propertyByName != null && (ColumnType)propertyByName.Value.Value == ColumnType.CalculatedTableColumn)
								{
									tmdlObject2.Properties.Remove(propertyByName);
								}
							}
							goto IL_0586;
						}
						break;
					}
					case ObjectType.Column:
						break;
					case ObjectType.Partition:
					{
						if (!this.rootObject.HasAnyProperty(false))
						{
							goto IL_0586;
						}
						TmdlProperty mode = this.rootObject.Properties.FirstOrDefault((TmdlProperty p) => string.Compare(p.Name, "mode", StringComparison.Ordinal) == 0);
						TmdlProperty source = this.rootObject.Properties.FirstOrDefault((TmdlProperty p) => string.Compare(p.Name, "source", StringComparison.Ordinal) == 0);
						List<TmdlProperty> list2 = new List<TmdlProperty>(this.rootObject.Properties.Where((TmdlProperty p) => p != mode && p != source));
						this.rootObject.Properties.Clear();
						if (mode != null)
						{
							this.rootObject.Properties.Add(mode);
						}
						foreach (TmdlProperty tmdlProperty in list2)
						{
							this.rootObject.Properties.Add(tmdlProperty);
						}
						if (source == null)
						{
							goto IL_0586;
						}
						TmdlStructValue tmdlStructValue = source.Value as TmdlStructValue;
						if (tmdlStructValue != null && !tmdlStructValue.IsEmpty)
						{
							TmdlObject tmdlObject3 = new TmdlObject("source");
							foreach (TmdlProperty tmdlProperty2 in tmdlStructValue.Properties)
							{
								if (string.Compare(tmdlProperty2.Name, "expression", StringComparison.Ordinal) == 0)
								{
									tmdlObject3.DefaultProperty = tmdlProperty2;
								}
								else
								{
									tmdlObject3.Properties.Add(tmdlProperty2);
								}
							}
							this.rootObject.Children.Add(tmdlObject3);
							goto IL_0586;
						}
						goto IL_0586;
					}
					default:
						if (objectType != ObjectType.PerspectiveTable)
						{
							goto IL_0586;
						}
						if (this.rootObject.HasAnyChild(false))
						{
							TmdlObjectWriter.SortPerspectiveTableChildren(this.rootObject.Children);
							goto IL_0586;
						}
						goto IL_0586;
					}
					TmdlProperty propertyByName2 = this.rootObject.GetPropertyByName("type", StringComparison.Ordinal);
					if (propertyByName2 != null && this.rootObject.DefaultProperty != null)
					{
						this.rootObject.Properties.Remove(propertyByName2);
					}
				}
				else if (objectType != ObjectType.Role)
				{
					if (objectType == ObjectType.ExtendedProperty)
					{
						TmdlProperty propertyByName3 = this.rootObject.GetPropertyByName("type", StringComparison.Ordinal);
						if (propertyByName3 != null)
						{
							TmdlEnumValue tmdlEnumValue = propertyByName3.Value as TmdlEnumValue;
							if (tmdlEnumValue != null)
							{
								object value = tmdlEnumValue.Value;
								if (value is ExtendedPropertyType)
								{
									ExtendedPropertyType extendedPropertyType = (ExtendedPropertyType)value;
									if (extendedPropertyType == ExtendedPropertyType.Json)
									{
										this.rootObject.Properties.Remove(propertyByName3);
										goto IL_0586;
									}
									goto IL_0586;
								}
							}
							string text = "Invalid value was serialized as ExtendedProperty.Type - expected type is {0}, actual type is {1}";
							object[] array = new object[2];
							array[0] = typeof(ExtendedPropertyType).FullName;
							int num = 1;
							TmdlEnumValue tmdlEnumValue2 = propertyByName3.Value as TmdlEnumValue;
							object obj;
							if (tmdlEnumValue2 == null)
							{
								obj = null;
							}
							else
							{
								object value2 = tmdlEnumValue2.Value;
								obj = ((value2 != null) ? value2.GetType().FullName : null);
							}
							array[num] = obj;
							throw TomInternalException.Create(text, array);
						}
						TmdlProperty defaultProperty = this.rootObject.DefaultProperty;
						TmdlValue tmdlValue = ((defaultProperty != null) ? defaultProperty.Value : null);
						JToken jtoken;
						if (tmdlValue == null || TmdlSerializationHelper.TryParseJsonObject(tmdlValue.RawValue, out jtoken))
						{
							this.rootObject.Properties.Add(new TmdlProperty("type", TmdlValue.FromEnum<ExtendedPropertyType>(ExtendedPropertyType.String)));
						}
					}
				}
				else if (this.rootObject.HasAnyChild(false))
				{
					TmdlObjectWriter.SortRoleChildren(this.rootObject.Children);
				}
				IL_0586:
				tmdlObject4 = this.rootObject;
			}
			finally
			{
				this.rootObject = null;
				this.state = TmdlObjectWriter.WriteState.Completed;
			}
			return tmdlObject4;
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x0009EDEC File Offset: 0x0009CFEC
		private static void SortModelChildren(ICollection<TmdlObject> children)
		{
			List<TmdlObject> list = new List<TmdlObject>();
			List<TmdlObject> list2 = new List<TmdlObject>();
			List<TmdlObject> list3 = new List<TmdlObject>();
			List<TmdlObject> list4 = new List<TmdlObject>();
			List<TmdlObject> list5 = new List<TmdlObject>();
			List<TmdlObject> list6 = new List<TmdlObject>();
			List<TmdlObject> list7 = new List<TmdlObject>();
			List<TmdlObject> list8 = new List<TmdlObject>();
			List<TmdlObject> list9 = new List<TmdlObject>();
			List<TmdlObject> list10 = new List<TmdlObject>();
			List<TmdlObject> list11 = new List<TmdlObject>();
			foreach (TmdlObject tmdlObject in children)
			{
				ObjectType objectType = tmdlObject.ObjectType;
				if (objectType <= ObjectType.Culture)
				{
					if (objectType <= ObjectType.Table)
					{
						if (objectType == ObjectType.Null)
						{
							list.Add(tmdlObject);
							continue;
						}
						if (objectType == ObjectType.Table)
						{
							list3.Add(tmdlObject);
							continue;
						}
					}
					else
					{
						if (objectType == ObjectType.Relationship)
						{
							list4.Add(tmdlObject);
							continue;
						}
						if (objectType == ObjectType.Annotation)
						{
							list10.Add(tmdlObject);
							continue;
						}
						if (objectType == ObjectType.Culture)
						{
							list7.Add(tmdlObject);
							continue;
						}
					}
				}
				else if (objectType <= ObjectType.Role)
				{
					if (objectType == ObjectType.Perspective)
					{
						list6.Add(tmdlObject);
						continue;
					}
					if (objectType == ObjectType.Role)
					{
						list5.Add(tmdlObject);
						continue;
					}
				}
				else
				{
					if (objectType == ObjectType.Expression)
					{
						list8.Add(tmdlObject);
						continue;
					}
					if (objectType == ObjectType.QueryGroup)
					{
						list9.Add(tmdlObject);
						continue;
					}
					if (objectType == ObjectType.AnalyticsAIMetadata)
					{
						list2.Add(tmdlObject);
						continue;
					}
				}
				list11.Add(tmdlObject);
			}
			children.Clear();
			for (int i = 0; i < list.Count; i++)
			{
				children.Add(list[i]);
			}
			for (int j = 0; j < list2.Count; j++)
			{
				children.Add(list2[j]);
			}
			for (int k = 0; k < list3.Count; k++)
			{
				children.Add(list3[k]);
			}
			for (int l = 0; l < list4.Count; l++)
			{
				children.Add(list4[l]);
			}
			for (int m = 0; m < list5.Count; m++)
			{
				children.Add(list5[m]);
			}
			for (int n = 0; n < list6.Count; n++)
			{
				children.Add(list6[n]);
			}
			for (int num = 0; num < list7.Count; num++)
			{
				children.Add(list7[num]);
			}
			for (int num2 = 0; num2 < list8.Count; num2++)
			{
				children.Add(list8[num2]);
			}
			for (int num3 = 0; num3 < list9.Count; num3++)
			{
				children.Add(list9[num3]);
			}
			for (int num4 = 0; num4 < list11.Count; num4++)
			{
				children.Add(list11[num4]);
			}
			for (int num5 = 0; num5 < list10.Count; num5++)
			{
				children.Add(list10[num5]);
			}
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x0009F0F0 File Offset: 0x0009D2F0
		private static void SortTableChildren(ICollection<TmdlObject> children)
		{
			List<TmdlObject> list = new List<TmdlObject>();
			List<TmdlObject> list2 = new List<TmdlObject>();
			List<TmdlObject> list3 = new List<TmdlObject>();
			List<TmdlObject> list4 = new List<TmdlObject>();
			List<TmdlObject> list5 = new List<TmdlObject>();
			List<TmdlObject> list6 = new List<TmdlObject>();
			List<TmdlObject> list7 = new List<TmdlObject>();
			foreach (TmdlObject tmdlObject in children)
			{
				ObjectType objectType = tmdlObject.ObjectType;
				if (objectType <= ObjectType.DetailRowsDefinition)
				{
					switch (objectType)
					{
					case ObjectType.Column:
						list3.Add(tmdlObject);
						continue;
					case ObjectType.AttributeHierarchy:
					case ObjectType.Relationship:
					case ObjectType.Level:
						goto IL_00CD;
					case ObjectType.Partition:
						list5.Add(tmdlObject);
						continue;
					case ObjectType.Measure:
						list2.Add(tmdlObject);
						continue;
					case ObjectType.Hierarchy:
						list4.Add(tmdlObject);
						continue;
					case ObjectType.Annotation:
						list6.Add(tmdlObject);
						continue;
					default:
						if (objectType != ObjectType.DetailRowsDefinition)
						{
							goto IL_00CD;
						}
						break;
					}
				}
				else if (objectType != ObjectType.CalculationGroup && objectType != ObjectType.RefreshPolicy)
				{
					goto IL_00CD;
				}
				list.Add(tmdlObject);
				continue;
				IL_00CD:
				list7.Add(tmdlObject);
			}
			children.Clear();
			for (int i = 0; i < list.Count; i++)
			{
				children.Add(list[i]);
			}
			for (int j = 0; j < list2.Count; j++)
			{
				children.Add(list2[j]);
			}
			for (int k = 0; k < list3.Count; k++)
			{
				children.Add(list3[k]);
			}
			for (int l = 0; l < list4.Count; l++)
			{
				children.Add(list4[l]);
			}
			for (int m = 0; m < list5.Count; m++)
			{
				children.Add(list5[m]);
			}
			for (int n = 0; n < list7.Count; n++)
			{
				children.Add(list7[n]);
			}
			for (int num = 0; num < list6.Count; num++)
			{
				children.Add(list6[num]);
			}
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x0009F300 File Offset: 0x0009D500
		private static void SortPerspectiveTableChildren(ICollection<TmdlObject> children)
		{
			List<TmdlObject> list = new List<TmdlObject>();
			List<TmdlObject> list2 = new List<TmdlObject>();
			List<TmdlObject> list3 = new List<TmdlObject>();
			List<TmdlObject> list4 = new List<TmdlObject>();
			List<TmdlObject> list5 = new List<TmdlObject>();
			foreach (TmdlObject tmdlObject in children)
			{
				ObjectType objectType = tmdlObject.ObjectType;
				if (objectType != ObjectType.Annotation)
				{
					switch (objectType)
					{
					case ObjectType.PerspectiveColumn:
						list2.Add(tmdlObject);
						break;
					case ObjectType.PerspectiveHierarchy:
						list3.Add(tmdlObject);
						break;
					case ObjectType.PerspectiveMeasure:
						list.Add(tmdlObject);
						break;
					default:
						list5.Add(tmdlObject);
						break;
					}
				}
				else
				{
					list4.Add(tmdlObject);
				}
			}
			children.Clear();
			for (int i = 0; i < list.Count; i++)
			{
				children.Add(list[i]);
			}
			for (int j = 0; j < list2.Count; j++)
			{
				children.Add(list2[j]);
			}
			for (int k = 0; k < list3.Count; k++)
			{
				children.Add(list3[k]);
			}
			for (int l = 0; l < list5.Count; l++)
			{
				children.Add(list5[l]);
			}
			for (int m = 0; m < list4.Count; m++)
			{
				children.Add(list4[m]);
			}
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x0009F478 File Offset: 0x0009D678
		private static void SortRoleChildren(ICollection<TmdlObject> children)
		{
			List<TmdlObject> list = new List<TmdlObject>();
			List<TmdlObject> list2 = new List<TmdlObject>();
			List<TmdlObject> list3 = new List<TmdlObject>();
			List<TmdlObject> list4 = new List<TmdlObject>();
			foreach (TmdlObject tmdlObject in children)
			{
				ObjectType objectType = tmdlObject.ObjectType;
				if (objectType != ObjectType.Annotation)
				{
					if (objectType != ObjectType.RoleMembership)
					{
						if (objectType != ObjectType.TablePermission)
						{
							list4.Add(tmdlObject);
						}
						else
						{
							list.Add(tmdlObject);
						}
					}
					else
					{
						list2.Add(tmdlObject);
					}
				}
				else
				{
					list3.Add(tmdlObject);
				}
			}
			children.Clear();
			for (int i = 0; i < list.Count; i++)
			{
				children.Add(list[i]);
			}
			for (int j = 0; j < list2.Count; j++)
			{
				children.Add(list2[j]);
			}
			for (int k = 0; k < list4.Count; k++)
			{
				children.Add(list4[k]);
			}
			for (int l = 0; l < list3.Count; l++)
			{
				children.Add(list3[l]);
			}
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x0009F5AC File Offset: 0x0009D7AC
		private static void AddModelDataAccessOptionsProperties(TmdlObject customJsonProperty, JToken token)
		{
			JObject jobject = (JObject)token;
			JToken jtoken;
			if (jobject.TryGetValue("fastCombine", StringComparison.InvariantCultureIgnoreCase, ref jtoken))
			{
				bool flag;
				if (CustomJsonPropertyHelper.TryConvertTokenToPrimitiveValue<bool>(jtoken, out flag) && flag)
				{
					customJsonProperty.Properties.Add(new TmdlProperty("fastCombine", new TmdlScalarValue<bool>(string.Empty, null)));
				}
				jtoken.Parent.Remove();
			}
			JToken jtoken2;
			if (jobject.TryGetValue("legacyRedirects", StringComparison.InvariantCultureIgnoreCase, ref jtoken2))
			{
				bool flag2;
				if (CustomJsonPropertyHelper.TryConvertTokenToPrimitiveValue<bool>(jtoken2, out flag2) && flag2)
				{
					customJsonProperty.Properties.Add(new TmdlProperty("legacyRedirects", new TmdlScalarValue<bool>(string.Empty, null)));
				}
				jtoken2.Parent.Remove();
			}
			JToken jtoken3;
			if (jobject.TryGetValue("returnErrorValuesAsNull", StringComparison.InvariantCultureIgnoreCase, ref jtoken3))
			{
				bool flag3;
				if (CustomJsonPropertyHelper.TryConvertTokenToPrimitiveValue<bool>(jtoken3, out flag3) && flag3)
				{
					customJsonProperty.Properties.Add(new TmdlProperty("returnErrorValuesAsNull", new TmdlScalarValue<bool>(string.Empty, null)));
				}
				jtoken3.Parent.Remove();
			}
			if (jobject.Count > 0)
			{
				customJsonProperty.DefaultProperty = new TmdlProperty("additionalProperties", new TmdlStringValue(TmdlParser.TrimExpressionWhitespaces(TmdlExpressionTrimStyle.TrimTrailingWhitespaces | TmdlExpressionTrimStyle.TrimLeadingCommonWhitespaces, null, jobject.ToString().SplitLines()), TmdlStringFormat.Block, true));
			}
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x0009F6F0 File Offset: 0x0009D8F0
		private static void AddModelAutomaticAggregationOptionsProperties(TmdlObject customJsonProperty, JToken token)
		{
			JObject jobject = (JObject)token;
			JToken jtoken;
			if (jobject.TryGetValue("queryCoverage", StringComparison.InvariantCultureIgnoreCase, ref jtoken))
			{
				double num;
				if (CustomJsonPropertyHelper.TryConvertTokenToPrimitiveValue<double>(jtoken, out num) && num != 0.0)
				{
					customJsonProperty.Properties.Add(new TmdlProperty("queryCoverage", TmdlValue.FromScalar<double>(num)));
				}
				jtoken.Parent.Remove();
			}
			JToken jtoken2;
			if (jobject.TryGetValue("detailTableMinRows", StringComparison.InvariantCultureIgnoreCase, ref jtoken2))
			{
				long num2;
				if (CustomJsonPropertyHelper.TryConvertTokenToPrimitiveValue<long>(jtoken2, out num2) && num2 != 0L)
				{
					customJsonProperty.Properties.Add(new TmdlProperty("detailTableMinRows", TmdlValue.FromScalar<long>(num2)));
				}
				jtoken2.Parent.Remove();
			}
			JToken jtoken3;
			if (jobject.TryGetValue("aggregationTableMaxRows", StringComparison.InvariantCultureIgnoreCase, ref jtoken3))
			{
				long num3;
				if (CustomJsonPropertyHelper.TryConvertTokenToPrimitiveValue<long>(jtoken3, out num3) && num3 != 0L)
				{
					customJsonProperty.Properties.Add(new TmdlProperty("aggregationTableMaxRows", TmdlValue.FromScalar<long>(num3)));
				}
				jtoken3.Parent.Remove();
			}
			JToken jtoken4;
			if (jobject.TryGetValue("aggregationTableSizeLimit", StringComparison.InvariantCultureIgnoreCase, ref jtoken4))
			{
				long num4;
				if (CustomJsonPropertyHelper.TryConvertTokenToPrimitiveValue<long>(jtoken4, out num4) && num4 != 0L)
				{
					customJsonProperty.Properties.Add(new TmdlProperty("aggregationTableSizeLimit", TmdlValue.FromScalar<long>(num4)));
				}
				jtoken4.Parent.Remove();
			}
			if (jobject.Count > 0)
			{
				customJsonProperty.DefaultProperty = new TmdlProperty("additionalProperties", new TmdlStringValue(TmdlParser.TrimExpressionWhitespaces(TmdlExpressionTrimStyle.TrimTrailingWhitespaces | TmdlExpressionTrimStyle.TrimLeadingCommonWhitespaces, null, jobject.ToString().SplitLines()), TmdlStringFormat.Block, true));
			}
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x0009F860 File Offset: 0x0009DA60
		private static void AddStructuredDataSourceConnectionDetailsProperties(TmdlObject customJsonProperty, JToken token)
		{
			JObject jobject = (JObject)token;
			JToken jtoken;
			if (jobject.TryGetValue("protocol", StringComparison.InvariantCultureIgnoreCase, ref jtoken))
			{
				string text;
				if (CustomJsonPropertyHelper.TryConvertTokenToPrimitiveValue<string>(jtoken, out text) && !string.IsNullOrEmpty(text))
				{
					customJsonProperty.Properties.Add(new TmdlProperty("protocol", new TmdlStringValue(text, null, false)));
				}
				jtoken.Parent.Remove();
			}
			JToken jtoken2;
			if (jobject.TryGetValue("address", StringComparison.InvariantCultureIgnoreCase, ref jtoken2))
			{
				JObject jobject2 = (JObject)jtoken2;
				TmdlStructValue tmdlStructValue = new TmdlStructValue();
				JToken jtoken3;
				if (jobject2.TryGetValue("server", StringComparison.InvariantCultureIgnoreCase, ref jtoken3))
				{
					string text2;
					if (CustomJsonPropertyHelper.TryConvertTokenToPrimitiveValue<string>(jtoken3, out text2) && !string.IsNullOrEmpty(text2))
					{
						tmdlStructValue.Properties.Add(new TmdlProperty("server", new TmdlStringValue(text2, null, false)));
					}
					jtoken3.Parent.Remove();
				}
				JToken jtoken4;
				if (jobject2.TryGetValue("database", StringComparison.InvariantCultureIgnoreCase, ref jtoken4))
				{
					string text3;
					if (CustomJsonPropertyHelper.TryConvertTokenToPrimitiveValue<string>(jtoken4, out text3) && !string.IsNullOrEmpty(text3))
					{
						tmdlStructValue.Properties.Add(new TmdlProperty("database", new TmdlStringValue(text3, null, false)));
					}
					jtoken4.Parent.Remove();
				}
				JToken jtoken5;
				if (jobject2.TryGetValue("model", StringComparison.InvariantCultureIgnoreCase, ref jtoken5))
				{
					string text4;
					if (CustomJsonPropertyHelper.TryConvertTokenToPrimitiveValue<string>(jtoken5, out text4) && !string.IsNullOrEmpty(text4))
					{
						tmdlStructValue.Properties.Add(new TmdlProperty("model", new TmdlStringValue(text4, null, false)));
					}
					jtoken5.Parent.Remove();
				}
				JToken jtoken6;
				if (jobject2.TryGetValue("schema", StringComparison.InvariantCultureIgnoreCase, ref jtoken6))
				{
					string text5;
					if (CustomJsonPropertyHelper.TryConvertTokenToPrimitiveValue<string>(jtoken6, out text5) && !string.IsNullOrEmpty(text5))
					{
						tmdlStructValue.Properties.Add(new TmdlProperty("schema", new TmdlStringValue(text5, null, false)));
					}
					jtoken6.Parent.Remove();
				}
				JToken jtoken7;
				if (jobject2.TryGetValue("object", StringComparison.InvariantCultureIgnoreCase, ref jtoken7))
				{
					string text6;
					if (CustomJsonPropertyHelper.TryConvertTokenToPrimitiveValue<string>(jtoken7, out text6) && !string.IsNullOrEmpty(text6))
					{
						tmdlStructValue.Properties.Add(new TmdlProperty("object", new TmdlStringValue(text6, null, false)));
					}
					jtoken7.Parent.Remove();
				}
				JToken jtoken8;
				if (jobject2.TryGetValue("url", StringComparison.InvariantCultureIgnoreCase, ref jtoken8))
				{
					string text7;
					if (CustomJsonPropertyHelper.TryConvertTokenToPrimitiveValue<string>(jtoken8, out text7) && !string.IsNullOrEmpty(text7))
					{
						tmdlStructValue.Properties.Add(new TmdlProperty("url", new TmdlStringValue(text7, null, false)));
					}
					jtoken8.Parent.Remove();
				}
				JToken jtoken9;
				if (jobject2.TryGetValue("contentType", StringComparison.InvariantCultureIgnoreCase, ref jtoken9))
				{
					string text8;
					if (CustomJsonPropertyHelper.TryConvertTokenToPrimitiveValue<string>(jtoken9, out text8) && !string.IsNullOrEmpty(text8))
					{
						tmdlStructValue.Properties.Add(new TmdlProperty("contentType", new TmdlStringValue(text8, null, false)));
					}
					jtoken9.Parent.Remove();
				}
				JToken jtoken10;
				if (jobject2.TryGetValue("resource", StringComparison.InvariantCultureIgnoreCase, ref jtoken10))
				{
					string text9;
					if (CustomJsonPropertyHelper.TryConvertTokenToPrimitiveValue<string>(jtoken10, out text9) && !string.IsNullOrEmpty(text9))
					{
						tmdlStructValue.Properties.Add(new TmdlProperty("resource", new TmdlStringValue(text9, null, false)));
					}
					jtoken10.Parent.Remove();
				}
				JToken jtoken11;
				if (jobject2.TryGetValue("path", StringComparison.InvariantCultureIgnoreCase, ref jtoken11))
				{
					string text10;
					if (CustomJsonPropertyHelper.TryConvertTokenToPrimitiveValue<string>(jtoken11, out text10) && !string.IsNullOrEmpty(text10))
					{
						tmdlStructValue.Properties.Add(new TmdlProperty("path", new TmdlStringValue(text10, null, false)));
					}
					jtoken11.Parent.Remove();
				}
				JToken jtoken12;
				if (jobject2.TryGetValue("domain", StringComparison.InvariantCultureIgnoreCase, ref jtoken12))
				{
					string text11;
					if (CustomJsonPropertyHelper.TryConvertTokenToPrimitiveValue<string>(jtoken12, out text11) && !string.IsNullOrEmpty(text11))
					{
						tmdlStructValue.Properties.Add(new TmdlProperty("domain", new TmdlStringValue(text11, null, false)));
					}
					jtoken12.Parent.Remove();
				}
				JToken jtoken13;
				if (jobject2.TryGetValue("account", StringComparison.InvariantCultureIgnoreCase, ref jtoken13))
				{
					string text12;
					if (CustomJsonPropertyHelper.TryConvertTokenToPrimitiveValue<string>(jtoken13, out text12) && !string.IsNullOrEmpty(text12))
					{
						tmdlStructValue.Properties.Add(new TmdlProperty("account", new TmdlStringValue(text12, null, false)));
					}
					jtoken13.Parent.Remove();
				}
				JToken jtoken14;
				if (jobject2.TryGetValue("emailAddress", StringComparison.InvariantCultureIgnoreCase, ref jtoken14))
				{
					string text13;
					if (CustomJsonPropertyHelper.TryConvertTokenToPrimitiveValue<string>(jtoken14, out text13) && !string.IsNullOrEmpty(text13))
					{
						tmdlStructValue.Properties.Add(new TmdlProperty("emailAddress", new TmdlStringValue(text13, null, false)));
					}
					jtoken14.Parent.Remove();
				}
				JToken jtoken15;
				if (jobject2.TryGetValue("connectionstring", StringComparison.InvariantCultureIgnoreCase, ref jtoken15))
				{
					string text14;
					if (CustomJsonPropertyHelper.TryConvertTokenToPrimitiveValue<string>(jtoken15, out text14) && !string.IsNullOrEmpty(text14))
					{
						tmdlStructValue.Properties.Add(new TmdlProperty("connectionstring", new TmdlStringValue(text14, null, false)));
					}
					jtoken15.Parent.Remove();
				}
				JToken jtoken16;
				if (jobject2.TryGetValue("property", StringComparison.InvariantCultureIgnoreCase, ref jtoken16))
				{
					string text15;
					if (CustomJsonPropertyHelper.TryConvertTokenToPrimitiveValue<string>(jtoken16, out text15) && !string.IsNullOrEmpty(text15))
					{
						tmdlStructValue.Properties.Add(new TmdlProperty("property", new TmdlStringValue(text15, null, false)));
					}
					jtoken16.Parent.Remove();
				}
				JToken jtoken17;
				if (jobject2.TryGetValue("view", StringComparison.InvariantCultureIgnoreCase, ref jtoken17))
				{
					string text16;
					if (CustomJsonPropertyHelper.TryConvertTokenToPrimitiveValue<string>(jtoken17, out text16) && !string.IsNullOrEmpty(text16))
					{
						tmdlStructValue.Properties.Add(new TmdlProperty("view", new TmdlStringValue(text16, null, false)));
					}
					jtoken17.Parent.Remove();
				}
				if (jobject2.Count == 0)
				{
					jobject2.Parent.Remove();
				}
				if (!tmdlStructValue.IsEmpty)
				{
					customJsonProperty.Properties.Add(new TmdlProperty("address", tmdlStructValue));
				}
			}
			if (jobject.Count > 0)
			{
				customJsonProperty.DefaultProperty = new TmdlProperty("additionalProperties", new TmdlStringValue(TmdlParser.TrimExpressionWhitespaces(TmdlExpressionTrimStyle.TrimTrailingWhitespaces | TmdlExpressionTrimStyle.TrimLeadingCommonWhitespaces, null, jobject.ToString().SplitLines()), TmdlStringFormat.Block, true));
			}
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x0009FE00 File Offset: 0x0009E000
		private static void AddStructuredDataSourceOptionsProperties(TmdlObject customJsonProperty, JToken token)
		{
			JObject jobject = (JObject)token;
			if (jobject.Count > 0)
			{
				customJsonProperty.DefaultProperty = new TmdlProperty("additionalProperties", new TmdlStringValue(TmdlParser.TrimExpressionWhitespaces(TmdlExpressionTrimStyle.TrimTrailingWhitespaces | TmdlExpressionTrimStyle.TrimLeadingCommonWhitespaces, null, jobject.ToString().SplitLines()), TmdlStringFormat.Block, true));
			}
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x0009FE50 File Offset: 0x0009E050
		private static void AddStructuredDataSourceCredentialProperties(TmdlObject customJsonProperty, JToken token)
		{
			JObject jobject = (JObject)token;
			JToken jtoken;
			if (jobject.TryGetValue("AuthenticationKind", StringComparison.InvariantCultureIgnoreCase, ref jtoken))
			{
				string text;
				if (CustomJsonPropertyHelper.TryConvertTokenToPrimitiveValue<string>(jtoken, out text) && !string.IsNullOrEmpty(text))
				{
					customJsonProperty.Properties.Add(new TmdlProperty("AuthenticationKind", new TmdlStringValue(text, null, false)));
				}
				jtoken.Parent.Remove();
			}
			JToken jtoken2;
			if (jobject.TryGetValue("PrivacySetting", StringComparison.InvariantCultureIgnoreCase, ref jtoken2))
			{
				string text2;
				if (CustomJsonPropertyHelper.TryConvertTokenToPrimitiveValue<string>(jtoken2, out text2) && !string.IsNullOrEmpty(text2))
				{
					customJsonProperty.Properties.Add(new TmdlProperty("PrivacySetting", new TmdlStringValue(text2, null, false)));
				}
				jtoken2.Parent.Remove();
			}
			JToken jtoken3;
			if (jobject.TryGetValue("Username", StringComparison.InvariantCultureIgnoreCase, ref jtoken3))
			{
				string text3;
				if (CustomJsonPropertyHelper.TryConvertTokenToPrimitiveValue<string>(jtoken3, out text3) && !string.IsNullOrEmpty(text3))
				{
					customJsonProperty.Properties.Add(new TmdlProperty("Username", new TmdlStringValue(text3, null, false)));
				}
				jtoken3.Parent.Remove();
			}
			JToken jtoken4;
			if (jobject.TryGetValue("Password", StringComparison.InvariantCultureIgnoreCase, ref jtoken4))
			{
				string text4;
				if (CustomJsonPropertyHelper.TryConvertTokenToPrimitiveValue<string>(jtoken4, out text4) && !string.IsNullOrEmpty(text4))
				{
					customJsonProperty.Properties.Add(new TmdlProperty("Password", new TmdlStringValue(text4, null, false)));
				}
				jtoken4.Parent.Remove();
			}
			JToken jtoken5;
			if (jobject.TryGetValue("EncryptConnection", StringComparison.InvariantCultureIgnoreCase, ref jtoken5))
			{
				bool flag;
				if (CustomJsonPropertyHelper.TryConvertTokenToPrimitiveValue<bool>(jtoken5, out flag) && flag)
				{
					customJsonProperty.Properties.Add(new TmdlProperty("EncryptConnection", new TmdlScalarValue<bool>(string.Empty, null)));
				}
				jtoken5.Parent.Remove();
			}
			if (jobject.Count > 0)
			{
				customJsonProperty.DefaultProperty = new TmdlProperty("additionalProperties", new TmdlStringValue(TmdlParser.TrimExpressionWhitespaces(TmdlExpressionTrimStyle.TrimTrailingWhitespaces | TmdlExpressionTrimStyle.TrimLeadingCommonWhitespaces, null, jobject.ToString().SplitLines()), TmdlStringFormat.Block, true));
			}
		}

		// Token: 0x06001723 RID: 5923 RVA: 0x000A0024 File Offset: 0x0009E224
		private void WriteStringPropertyImpl(string propertyName, MetadataPropertyNature propertyNature, string value)
		{
			bool flag = (propertyNature & MetadataPropertyNature.DefaultProperty) == MetadataPropertyNature.DefaultProperty;
			bool flag2 = (propertyNature & (MetadataPropertyNature.MultilineString | MetadataPropertyNature.JsonString)) > MetadataPropertyNature.None;
			switch (propertyNature & MetadataPropertyNature.PropertyCategoryMask)
			{
			case MetadataPropertyNature.NameProperty:
				this.SetCurrentObjectName(value);
				return;
			case MetadataPropertyNature.RegularProperty:
			{
				if (this.state == TmdlObjectWriter.WriteState.RootObject && string.Compare(propertyName, "description", StringComparison.Ordinal) == 0)
				{
					this.rootObject.Description = value.SplitLines();
					return;
				}
				TmdlValue tmdlValue;
				if (value == null)
				{
					tmdlValue = TmdlStringValue.Null;
				}
				else if (value == string.Empty)
				{
					tmdlValue = TmdlStringValue.Empty;
				}
				else if (this.rootObject.ObjectType == ObjectType.TimeUnitColumnAssociation && string.Compare(propertyName, "primaryColumn", StringComparison.Ordinal) == 0)
				{
					tmdlValue = new TmdlModelReferenceValue(new ObjectName(new string[] { value }));
				}
				else if ((propertyNature & MetadataPropertyNature.Restricted) == MetadataPropertyNature.Restricted)
				{
					if (!this.config.IncludeRestrictedInformation && this.rootObject.ObjectType == ObjectType.DataSource)
					{
						if (!(propertyName == "connectionString"))
						{
							if (propertyName == "password")
							{
								value = PropertyHelper.GetCuratedValueForPassword(value);
							}
						}
						else
						{
							value = PropertyHelper.GetCuratedValueForConnectionString(value);
						}
					}
					tmdlValue = new TmdlStringValue(value, null, false);
				}
				else if (flag2)
				{
					string[] array = value.SplitLines();
					if (this.config.TrimStyle != TmdlExpressionTrimStyle.NoTrim)
					{
						array = TmdlParser.TrimExpressionWhitespaces(this.config.TrimStyle, null, array);
					}
					if (array.Length > 1 || string.IsNullOrEmpty(array[0]) || char.IsWhiteSpace(array[0], 0) || char.IsWhiteSpace(array[0], array[0].Length - 1))
					{
						tmdlValue = new TmdlStringValue(array, TmdlStringFormat.Block, true);
					}
					else
					{
						ObjectType currentObjectType = this.GetCurrentObjectType();
						bool flag3;
						if (currentObjectType <= ObjectType.KPI)
						{
							if (currentObjectType == ObjectType.Partition)
							{
								flag3 = string.Compare(propertyName, "query", StringComparison.Ordinal) == 0 || string.Compare(propertyName, "expression", StringComparison.Ordinal) == 0;
								goto IL_0288;
							}
							if (currentObjectType == ObjectType.KPI)
							{
								flag3 = string.Compare(propertyName, "statusExpression", StringComparison.Ordinal) == 0 || string.Compare(propertyName, "targetExpression", StringComparison.Ordinal) == 0 || string.Compare(propertyName, "trendExpression", StringComparison.Ordinal) == 0;
								goto IL_0288;
							}
						}
						else
						{
							if (currentObjectType == ObjectType.LinguisticMetadata)
							{
								flag3 = string.Compare(propertyName, "content", StringComparison.Ordinal) == 0;
								goto IL_0288;
							}
							if (currentObjectType == ObjectType.RefreshPolicy)
							{
								flag3 = string.Compare(propertyName, "sourceExpression", StringComparison.Ordinal) == 0 || string.Compare(propertyName, "pollingExpression", StringComparison.Ordinal) == 0;
								goto IL_0288;
							}
						}
						flag3 = flag;
						IL_0288:
						tmdlValue = new TmdlStringValue(array[0], null, flag3);
					}
				}
				else
				{
					tmdlValue = new TmdlStringValue(value, null, flag);
				}
				this.AddPropertyToCurrentScope(flag, new TmdlProperty(propertyName, tmdlValue));
				break;
			}
			case MetadataPropertyNature.ParentProperty:
				break;
			case MetadataPropertyNature.CrossLinkProperty:
				this.AddPropertyToCurrentScope(false, new TmdlProperty(propertyName, new TmdlModelReferenceValue(new ObjectName(new string[] { value }))));
				return;
			default:
				return;
			}
		}

		// Token: 0x06001724 RID: 5924 RVA: 0x000A02E0 File Offset: 0x0009E4E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void WriteInt32PropertyImpl(string propertyName, MetadataPropertyNature propertyNature, int value)
		{
			bool flag;
			if (this.state == TmdlObjectWriter.WriteState.RootObject)
			{
				ObjectType objectType = this.rootObject.ObjectType;
				if (objectType != ObjectType.Level)
				{
					flag = objectType == ObjectType.CalculationItem && string.Compare(propertyName, "ordinal", StringComparison.Ordinal) == 0;
				}
				else
				{
					flag = string.Compare(propertyName, "ordinal", StringComparison.Ordinal) == 0;
				}
			}
			else
			{
				flag = false;
			}
			if (flag)
			{
				this.rootObject.Ordinal = new int?(value);
				return;
			}
			this.AddPropertyToCurrentScope((propertyNature & MetadataPropertyNature.DefaultProperty) == MetadataPropertyNature.DefaultProperty, new TmdlProperty(propertyName, TmdlValue.FromScalar<int>(value)));
		}

		// Token: 0x06001725 RID: 5925 RVA: 0x000A036D File Offset: 0x0009E56D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void WriteUInt32PropertyImpl(string propertyName, MetadataPropertyNature propertyNature, uint value)
		{
			this.AddPropertyToCurrentScope((propertyNature & MetadataPropertyNature.DefaultProperty) == MetadataPropertyNature.DefaultProperty, new TmdlProperty(propertyName, TmdlValue.FromScalar<uint>(value)));
		}

		// Token: 0x06001726 RID: 5926 RVA: 0x000A038F File Offset: 0x0009E58F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void WriteInt64PropertyImpl(string propertyName, MetadataPropertyNature propertyNature, long value)
		{
			this.AddPropertyToCurrentScope((propertyNature & MetadataPropertyNature.DefaultProperty) == MetadataPropertyNature.DefaultProperty, new TmdlProperty(propertyName, TmdlValue.FromScalar<long>(value)));
		}

		// Token: 0x06001727 RID: 5927 RVA: 0x000A03B1 File Offset: 0x0009E5B1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void WriteUInt64PropertyImpl(string propertyName, MetadataPropertyNature propertyNature, ulong value)
		{
			this.AddPropertyToCurrentScope((propertyNature & MetadataPropertyNature.DefaultProperty) == MetadataPropertyNature.DefaultProperty, new TmdlProperty(propertyName, TmdlValue.FromScalar<ulong>(value)));
		}

		// Token: 0x06001728 RID: 5928 RVA: 0x000A03D4 File Offset: 0x0009E5D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void WriteBooleanPropertyImpl(string propertyName, MetadataPropertyNature propertyNature, bool value)
		{
			TmdlValue tmdlValue;
			if (value)
			{
				tmdlValue = new TmdlScalarValue<bool>(string.Empty, null);
			}
			else
			{
				tmdlValue = TmdlValue.FromScalar<bool>(value);
			}
			this.AddPropertyToCurrentScope((propertyNature & MetadataPropertyNature.DefaultProperty) == MetadataPropertyNature.DefaultProperty, new TmdlProperty(propertyName, tmdlValue));
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x000A041C File Offset: 0x0009E61C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void WriteDoublePropertyImpl(string propertyName, MetadataPropertyNature propertyNature, double value)
		{
			this.AddPropertyToCurrentScope((propertyNature & MetadataPropertyNature.DefaultProperty) == MetadataPropertyNature.DefaultProperty, new TmdlProperty(propertyName, TmdlValue.FromScalar<double>(value)));
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x000A043E File Offset: 0x0009E63E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void WriteDateTimePropertyImpl(string propertyName, MetadataPropertyNature propertyNature, DateTime value)
		{
			this.AddPropertyToCurrentScope((propertyNature & MetadataPropertyNature.DefaultProperty) == MetadataPropertyNature.DefaultProperty, new TmdlProperty(propertyName, TmdlValue.FromScalar<DateTime>(value)));
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x000A0460 File Offset: 0x0009E660
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void WriteEnumPropertyImpl<TEnum>(string propertyName, MetadataPropertyNature propertyNature, TEnum value) where TEnum : Enum
		{
			if ((propertyNature & MetadataPropertyNature.PropertyCategoryMask) == MetadataPropertyNature.NameProperty)
			{
				this.SetCurrentObjectName(Enum.GetName(typeof(TEnum), value));
				return;
			}
			ObjectType currentObjectType = this.GetCurrentObjectType();
			if (currentObjectType != ObjectType.DataSource)
			{
				if (currentObjectType != ObjectType.Partition)
				{
					if (currentObjectType == ObjectType.Expression)
					{
						if (string.Compare(propertyName, "kind", StringComparison.Ordinal) == 0)
						{
							if (!(value is ExpressionKind))
							{
								throw TomInternalException.Create("Invalid value was serialized as NamedExpression.Kind - expected type is {0}, actual type is {1}", new object[]
								{
									typeof(ExpressionKind).FullName,
									value.GetType().FullName
								});
							}
							if (value as ExpressionKind == ExpressionKind.M)
							{
								return;
							}
						}
					}
				}
				else if (string.Compare(propertyName, "type", StringComparison.Ordinal) == 0)
				{
					propertyName = "sourceType";
				}
			}
			else if (string.Compare(propertyName, "type", StringComparison.Ordinal) == 0)
			{
				if (!(value is DataSourceType))
				{
					throw TomInternalException.Create("Invalid value was serialized as DataSource.Type - expected type is {0}, actual type is {1}", new object[]
					{
						typeof(DataSourceType).FullName,
						value.GetType().FullName
					});
				}
				DataSourceType dataSourceType = value as DataSourceType;
				if (dataSourceType == DataSourceType.Structured)
				{
					return;
				}
			}
			this.AddPropertyToCurrentScope((propertyNature & MetadataPropertyNature.DefaultProperty) == MetadataPropertyNature.DefaultProperty, new TmdlProperty(propertyName, TmdlValue.FromEnum<TEnum>(value)));
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x000A05C0 File Offset: 0x0009E7C0
		private void WritePropertyImpl(string propertyName, MetadataPropertyNature propertyNature, Type type, object value)
		{
			if (type == typeof(string))
			{
				this.WriteStringPropertyImpl(propertyName, propertyNature, (string)value);
				return;
			}
			if (type == typeof(int))
			{
				this.WriteInt32PropertyImpl(propertyName, propertyNature, (int)value);
				return;
			}
			if (type == typeof(uint))
			{
				this.WriteUInt32PropertyImpl(propertyName, propertyNature, (uint)value);
				return;
			}
			if (type == typeof(long))
			{
				this.WriteInt64PropertyImpl(propertyName, propertyNature, (long)value);
				return;
			}
			if (type == typeof(ulong))
			{
				this.WriteUInt64PropertyImpl(propertyName, propertyNature, (ulong)value);
				return;
			}
			if (type == typeof(bool))
			{
				this.WriteBooleanPropertyImpl(propertyName, propertyNature, (bool)value);
				return;
			}
			if (type == typeof(DateTime))
			{
				this.WriteDateTimePropertyImpl(propertyName, propertyNature, (DateTime)value);
				return;
			}
			if (type.IsEnum)
			{
				this.AddPropertyToCurrentScope((propertyNature & MetadataPropertyNature.DefaultProperty) == MetadataPropertyNature.DefaultProperty, new TmdlProperty(propertyName, TmdlValue.FromEnum<Enum>((Enum)value)));
				return;
			}
			if (type == typeof(IEnumerable<string>))
			{
				IEnumerable<string> enumerable = (IEnumerable<string>)value;
				if (this.GetCurrentObjectType() == ObjectType.TimeUnitColumnAssociation)
				{
					if (string.Compare(propertyName, "associatedColumns", StringComparison.Ordinal) != 0)
					{
						throw TomInternalException.Create("Invalid value was serialized as {0} - the value type is {1} and it is not supported for this property!", new object[] { propertyName, type.FullName });
					}
					using (IEnumerator<string> enumerator = enumerable.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string text = enumerator.Current;
							this.AddPropertyToCurrentScope(false, new TmdlProperty("associatedColumn", new TmdlModelReferenceValue(new ObjectName(new string[] { text }))));
						}
						return;
					}
				}
				throw TomInternalException.Create("Invalid value was serialized as {0} - the value type is {1} and it is not supported for this property!", new object[] { propertyName, type.FullName });
			}
			if (type == typeof(ObjectPath))
			{
				this.WriteCrossLinkPropertyImpl(propertyName, propertyNature, (ObjectPath)value);
				return;
			}
			throw TomInternalException.Create("Invalid property type - {0} is not a valid type for TMDL serialization", new object[] { type.FullName });
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x000A07F0 File Offset: 0x0009E9F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void WriteCrossLinkPropertyImpl(string propertyName, MetadataPropertyNature propertyNature, ObjectPath value)
		{
			this.AddPropertyToCurrentScope(false, new TmdlProperty(propertyName, new TmdlModelReferenceValue(new ObjectName(value))));
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x000A080C File Offset: 0x0009EA0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ObjectType GetCurrentObjectType()
		{
			switch (this.state)
			{
			case TmdlObjectWriter.WriteState.RootObject:
				return this.rootObject.ObjectType;
			case TmdlObjectWriter.WriteState.ComplexProperty:
			{
				if (!this.complexPropertyStack[this.complexPropertyStack.Count - 1].IsProperty)
				{
					return this.complexPropertyStack[this.complexPropertyStack.Count - 1].ObjectType;
				}
				for (int i = this.complexPropertyStack.Count - 2; i >= 0; i--)
				{
					if (this.complexPropertyStack[i].IsTranslationElement)
					{
						return this.complexPropertyStack[i].GetTranslationElement().ObjectType;
					}
				}
				return this.rootObject.ObjectType;
			}
			case TmdlObjectWriter.WriteState.ComplexPropertyCollection:
				return this.complexPropertyStack[this.complexPropertyStack.Count - 1].ObjectType;
			default:
				throw TomInternalException.Create("Invalid state for an object-type query - state={0}", new object[] { this.state });
			}
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x000A091C File Offset: 0x0009EB1C
		private void SetCurrentObjectName(string name)
		{
			TmdlObjectWriter.WriteState writeState = this.state;
			if (writeState == TmdlObjectWriter.WriteState.RootObject)
			{
				this.rootObject.Name = new ObjectName(new string[] { name });
				return;
			}
			if (writeState != TmdlObjectWriter.WriteState.ComplexProperty)
			{
				throw TomInternalException.Create("The write state of the TMD writer is {0} and it cannot be used to set a name at that state!", new object[] { this.state });
			}
			this.complexPropertyStack[this.complexPropertyStack.Count - 1].GetTranslationElement().Name = new ObjectName(new string[] { name });
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x000A09A8 File Offset: 0x0009EBA8
		private void AddPropertyToCurrentScope(bool isDefaultProperty, TmdlProperty property)
		{
			TmdlObjectWriter.WriteState writeState = this.state;
			TmdlObject tmdlObject;
			TmdlObjectWriter.ComplexProperty complexProperty;
			if (writeState != TmdlObjectWriter.WriteState.RootObject)
			{
				if (writeState != TmdlObjectWriter.WriteState.ComplexProperty)
				{
					throw TomInternalException.Create("The write state of the TMDL writer is {0} and it cannot be used to write properties at that state!", new object[] { this.state });
				}
				tmdlObject = null;
				complexProperty = this.complexPropertyStack[this.complexPropertyStack.Count - 1];
			}
			else
			{
				tmdlObject = this.rootObject;
				complexProperty = default(TmdlObjectWriter.ComplexProperty);
			}
			Utils.Verify((tmdlObject != null) ^ complexProperty.IsValid);
			if (isDefaultProperty)
			{
				if (tmdlObject == null)
				{
					Utils.Verify(complexProperty.IsProperty);
					Utils.Verify(this.complexPropertyStack.Count == 1);
					tmdlObject = this.rootObject;
				}
				Utils.Verify(tmdlObject.DefaultProperty == null);
				tmdlObject.DefaultProperty = property;
				return;
			}
			if (tmdlObject != null)
			{
				tmdlObject.Properties.Add(property);
				return;
			}
			if (complexProperty.IsTranslationElement)
			{
				complexProperty.GetTranslationElement().Properties.Add(property);
				return;
			}
			complexProperty.GetValue().Properties.Add(property);
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x000A0AA4 File Offset: 0x0009ECA4
		private bool IsValidComplexProperty(string propertyName, MetadataPropertyNature propertyNature, out TmdlObjectWriter.ComplexProperty property)
		{
			ObjectType objectType = this.rootObject.ObjectType;
			if (objectType != ObjectType.Partition)
			{
				if (objectType == ObjectType.Culture)
				{
					ObjectType currentObjectType = this.GetCurrentObjectType();
					if (string.IsNullOrEmpty(propertyName) && this.state == TmdlObjectWriter.WriteState.ComplexPropertyCollection)
					{
						property = TmdlObjectWriter.ComplexProperty.CreateTranslationElement(propertyName, currentObjectType);
						return true;
					}
					ObjectType objectType2;
					bool flag;
					if (currentObjectType == ObjectType.Culture)
					{
						if (string.Compare(propertyName, "translations", StringComparison.Ordinal) == 0)
						{
							property = TmdlObjectWriter.ComplexProperty.CreateProperty(propertyName);
							return true;
						}
						if (string.Compare(propertyName, "model", StringComparison.Ordinal) == 0 && this.state == TmdlObjectWriter.WriteState.ComplexProperty && this.complexPropertyStack[this.complexPropertyStack.Count - 1].IsProperty && string.Compare(this.complexPropertyStack[this.complexPropertyStack.Count - 1].Name, "translations", StringComparison.Ordinal) == 0)
						{
							property = TmdlObjectWriter.ComplexProperty.CreateTranslationElement(propertyName, ObjectType.Model);
							return true;
						}
					}
					else if (ObjectTreeHelper.IsChildJsonPropertyName(currentObjectType, propertyName, out objectType2, out flag))
					{
						property = TmdlObjectWriter.ComplexProperty.CreateTranslationElement(propertyName, objectType2);
						return true;
					}
				}
			}
			else if (string.Compare(propertyName, "source", StringComparison.Ordinal) == 0)
			{
				property = TmdlObjectWriter.ComplexProperty.CreateProperty(propertyName);
				return true;
			}
			property = default(TmdlObjectWriter.ComplexProperty);
			return false;
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x000A0BD2 File Offset: 0x0009EDD2
		private void PushComplexProperty(TmdlObjectWriter.ComplexProperty property)
		{
			if (this.complexPropertyStack == null)
			{
				this.complexPropertyStack = new List<TmdlObjectWriter.ComplexProperty>();
			}
			this.complexPropertyStack.Add(property);
			this.state = TmdlObjectWriter.WriteState.ComplexProperty;
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x000A0BFC File Offset: 0x0009EDFC
		private void PopComplexProperty(out TmdlObjectWriter.ComplexProperty property)
		{
			property = this.complexPropertyStack[this.complexPropertyStack.Count - 1];
			this.complexPropertyStack.RemoveAt(this.complexPropertyStack.Count - 1);
			if (this.complexPropertyStack.Count == 0)
			{
				this.state = TmdlObjectWriter.WriteState.RootObject;
				return;
			}
			if (this.complexPropertyStack[this.complexPropertyStack.Count - 1].IsProperty || this.complexPropertyStack[this.complexPropertyStack.Count - 1].IsTranslationElement)
			{
				this.state = TmdlObjectWriter.WriteState.ComplexProperty;
				return;
			}
			this.state = TmdlObjectWriter.WriteState.ComplexPropertyCollection;
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x000A0CA8 File Offset: 0x0009EEA8
		private void AddComplexPropertyToCurrentScope(TmdlObjectWriter.ComplexProperty property)
		{
			TmdlObject tmdlObject;
			TmdlObjectWriter.ComplexProperty complexProperty;
			switch (this.state)
			{
			case TmdlObjectWriter.WriteState.RootObject:
				tmdlObject = this.rootObject;
				complexProperty = default(TmdlObjectWriter.ComplexProperty);
				break;
			case TmdlObjectWriter.WriteState.ComplexProperty:
				tmdlObject = null;
				complexProperty = this.complexPropertyStack[this.complexPropertyStack.Count - 1];
				break;
			case TmdlObjectWriter.WriteState.ComplexPropertyCollection:
				if (this.complexPropertyStack.Count > 1)
				{
					tmdlObject = null;
					complexProperty = this.complexPropertyStack[this.complexPropertyStack.Count - 2];
				}
				else
				{
					tmdlObject = this.rootObject;
					complexProperty = default(TmdlObjectWriter.ComplexProperty);
				}
				break;
			default:
				throw TomInternalException.Create("The write state of the TMDL writer is {0} and it cannot be used to add children at that state!", new object[] { this.state });
			}
			if (string.IsNullOrEmpty(property.Name) && complexProperty.IsTranslationElement)
			{
				complexProperty.GetTranslationElement().Children.Add(property.GetTranslationElement());
				return;
			}
			if (property.IsTranslationElement)
			{
				TmdlTranslationElement translationElement = property.GetTranslationElement();
				if (complexProperty.IsTranslationElement)
				{
					complexProperty.GetTranslationElement().Children.Add(translationElement);
					return;
				}
				complexProperty.GetValue().Properties.Add(new TmdlProperty(property.Name, new TmdlTranslationRootValue(translationElement)));
				return;
			}
			else
			{
				TmdlStructValue value = property.GetValue();
				if (tmdlObject == null)
				{
					complexProperty.GetValue().Properties.Add(new TmdlProperty(property.Name, value));
					return;
				}
				if (tmdlObject.ObjectType == ObjectType.Culture && string.Compare(property.Name, "translations", StringComparison.Ordinal) == 0)
				{
					Utils.Verify(value.Properties.Count == 1);
					TmdlProperty tmdlProperty = value.Properties.Single<TmdlProperty>();
					Utils.Verify(tmdlProperty.Value != null && tmdlProperty.Value.Type == TmdlValueType.TranslationRoot);
					tmdlObject.Properties.Add(new TmdlProperty(property.Name, tmdlProperty.Value));
					return;
				}
				tmdlObject.Properties.Add(new TmdlProperty(property.Name, value));
				return;
			}
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x000A0EA3 File Offset: 0x0009F0A3
		private void PushComplexPropertyCollection(ObjectType objectType, string collectionName)
		{
			if (this.complexPropertyStack == null)
			{
				this.complexPropertyStack = new List<TmdlObjectWriter.ComplexProperty>();
			}
			this.complexPropertyStack.Add(TmdlObjectWriter.ComplexProperty.CreateTranslationElementCollection(collectionName, objectType));
			this.state = TmdlObjectWriter.WriteState.ComplexPropertyCollection;
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x000A0ED4 File Offset: 0x0009F0D4
		private void PopComplexPropertyCollection(out string collectionName)
		{
			collectionName = this.complexPropertyStack[this.complexPropertyStack.Count - 1].Name;
			this.complexPropertyStack.RemoveAt(this.complexPropertyStack.Count - 1);
			if (this.complexPropertyStack.Count == 0)
			{
				this.state = TmdlObjectWriter.WriteState.RootObject;
				return;
			}
			Utils.Verify(this.complexPropertyStack[this.complexPropertyStack.Count - 1].IsProperty || this.complexPropertyStack[this.complexPropertyStack.Count - 1].IsTranslationElement, "Collection of complex properties is only allowed under the root object or a complex-property, not under another collection!");
			this.state = TmdlObjectWriter.WriteState.ComplexProperty;
		}

		// Token: 0x0400043C RID: 1084
		internal static readonly Comparison<TmdlObject> sortByOrdinal = delegate(TmdlObject x, TmdlObject y)
		{
			if (x.Ordinal == null)
			{
				return ((y.Ordinal != null) > false) ? 1 : 0;
			}
			if (y.Ordinal == null)
			{
				return -1;
			}
			return x.Ordinal.Value - y.Ordinal.Value;
		};

		// Token: 0x0400043D RID: 1085
		private readonly TmdlSerializationConfiguration config;

		// Token: 0x0400043E RID: 1086
		private TmdlObject rootObject;

		// Token: 0x0400043F RID: 1087
		private List<TmdlObjectWriter.ComplexProperty> complexPropertyStack;

		// Token: 0x04000440 RID: 1088
		private TmdlObjectWriter.WriteState state;

		// Token: 0x02000361 RID: 865
		private enum WriteState
		{
			// Token: 0x04000ED6 RID: 3798
			Start,
			// Token: 0x04000ED7 RID: 3799
			RootObject,
			// Token: 0x04000ED8 RID: 3800
			ComplexProperty,
			// Token: 0x04000ED9 RID: 3801
			ComplexPropertyCollection,
			// Token: 0x04000EDA RID: 3802
			Completed
		}

		// Token: 0x02000362 RID: 866
		private struct ComplexProperty
		{
			// Token: 0x06002698 RID: 9880 RVA: 0x000EB767 File Offset: 0x000E9967
			private ComplexProperty(string name, ObjectType objectType, TmdlStructValue value, TmdlTranslationElement translationElement)
			{
				this.name = name;
				this.objectType = objectType;
				this.value = value;
				this.translationElement = translationElement;
			}

			// Token: 0x170007BB RID: 1979
			// (get) Token: 0x06002699 RID: 9881 RVA: 0x000EB786 File Offset: 0x000E9986
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x170007BC RID: 1980
			// (get) Token: 0x0600269A RID: 9882 RVA: 0x000EB78E File Offset: 0x000E998E
			public ObjectType ObjectType
			{
				get
				{
					return this.objectType;
				}
			}

			// Token: 0x170007BD RID: 1981
			// (get) Token: 0x0600269B RID: 9883 RVA: 0x000EB796 File Offset: 0x000E9996
			public bool IsProperty
			{
				get
				{
					return this.value != null;
				}
			}

			// Token: 0x170007BE RID: 1982
			// (get) Token: 0x0600269C RID: 9884 RVA: 0x000EB7A1 File Offset: 0x000E99A1
			public bool IsTranslationElement
			{
				get
				{
					return this.translationElement != null;
				}
			}

			// Token: 0x170007BF RID: 1983
			// (get) Token: 0x0600269D RID: 9885 RVA: 0x000EB7AC File Offset: 0x000E99AC
			public bool IsTranslationElementCollection
			{
				get
				{
					return this.objectType != ObjectType.Null && this.value == null && this.translationElement == null;
				}
			}

			// Token: 0x170007C0 RID: 1984
			// (get) Token: 0x0600269E RID: 9886 RVA: 0x000EB7C9 File Offset: 0x000E99C9
			internal bool IsValid
			{
				get
				{
					return this.objectType != ObjectType.Null || !string.IsNullOrEmpty(this.name);
				}
			}

			// Token: 0x0600269F RID: 9887 RVA: 0x000EB7E3 File Offset: 0x000E99E3
			public static TmdlObjectWriter.ComplexProperty CreateProperty(string name)
			{
				return new TmdlObjectWriter.ComplexProperty(name, ObjectType.Null, new TmdlStructValue(), null);
			}

			// Token: 0x060026A0 RID: 9888 RVA: 0x000EB7F2 File Offset: 0x000E99F2
			public static TmdlObjectWriter.ComplexProperty CreateTranslationElement(string name, ObjectType objectType)
			{
				return new TmdlObjectWriter.ComplexProperty(name, objectType, null, new TmdlTranslationElement(objectType));
			}

			// Token: 0x060026A1 RID: 9889 RVA: 0x000EB802 File Offset: 0x000E9A02
			public static TmdlObjectWriter.ComplexProperty CreateTranslationElementCollection(string name, ObjectType objectType)
			{
				return new TmdlObjectWriter.ComplexProperty(name, objectType, null, null);
			}

			// Token: 0x060026A2 RID: 9890 RVA: 0x000EB80D File Offset: 0x000E9A0D
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public TmdlStructValue GetValue()
			{
				Utils.Verify(this.IsProperty);
				return this.value;
			}

			// Token: 0x060026A3 RID: 9891 RVA: 0x000EB820 File Offset: 0x000E9A20
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public TmdlTranslationElement GetTranslationElement()
			{
				Utils.Verify(this.IsTranslationElement);
				return this.translationElement;
			}

			// Token: 0x04000EDB RID: 3803
			private readonly string name;

			// Token: 0x04000EDC RID: 3804
			private readonly ObjectType objectType;

			// Token: 0x04000EDD RID: 3805
			private readonly TmdlStructValue value;

			// Token: 0x04000EDE RID: 3806
			private readonly TmdlTranslationElement translationElement;
		}
	}
}
