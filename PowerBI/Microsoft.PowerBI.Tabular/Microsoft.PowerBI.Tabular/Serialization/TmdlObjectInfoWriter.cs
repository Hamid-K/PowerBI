using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Metadata;
using Microsoft.AnalysisServices.Tabular.Tmdl;
using Microsoft.AnalysisServices.Tabular.Tmdl.Schema;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Serialization
{
	// Token: 0x02000168 RID: 360
	internal sealed class TmdlObjectInfoWriter : IMetadataSchemaWriter
	{
		// Token: 0x0600168E RID: 5774 RVA: 0x000953C0 File Offset: 0x000935C0
		public TmdlObjectInfoWriter(IMetadataFilter filter, ObjectType type)
			: this(null, filter, type, false)
		{
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x000953CC File Offset: 0x000935CC
		private TmdlObjectInfoWriter(IDictionary<ObjectType, TmdlObjectInfo> metadataObjects, IMetadataFilter filter, ObjectType type, bool isSingleChild)
		{
			this.metadataObjects = metadataObjects;
			this.filter = filter;
			this.rootObjectInfo = TmdlObjectInfoWriter.CreateObjectInfo(type, isSingleChild, null);
			this.state = TmdlObjectInfoWriter.InfoWriteState.Start;
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x000953F8 File Offset: 0x000935F8
		public static IReadOnlyDictionary<ObjectType, TmdlObjectInfo> BuildFullMetadataSchema(IMetadataFilter filter, SerializationActivityContext context)
		{
			Dictionary<ObjectType, TmdlObjectInfo> dictionary = new Dictionary<ObjectType, TmdlObjectInfo>();
			TmdlObjectInfoWriter tmdlObjectInfoWriter = new TmdlObjectInfoWriter(dictionary, filter, ObjectType.Database, true);
			Database.WriteMetadataSchema(context, tmdlObjectInfoWriter);
			TmdlObjectInfo tmdlObjectInfo = tmdlObjectInfoWriter.ExtractObjectInfo(true);
			Utils.Verify(tmdlObjectInfo.ObjectType == ObjectType.Database);
			dictionary.Add(ObjectType.Database, tmdlObjectInfo);
			return dictionary;
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x00095448 File Offset: 0x00093648
		public void StartMetadataObjectScope(ObjectType objectType, string choiceOption, string description)
		{
			TmdlObjectInfoWriter.InfoWriteState infoWriteState = this.state;
			if (infoWriteState == TmdlObjectInfoWriter.InfoWriteState.Start)
			{
				Utils.Verify(string.IsNullOrEmpty(choiceOption));
				Utils.Verify(string.IsNullOrEmpty(this.choiceOption));
				this.rootObjectInfo.Description = description;
				this.state = TmdlObjectInfoWriter.InfoWriteState.RootObject;
				return;
			}
			if (infoWriteState != TmdlObjectInfoWriter.InfoWriteState.RootObjectChoice)
			{
				throw TomInternalException.Create("Invalid state for starting a metadata-object scope - state={0}, valid states=[Start, RootObjectChoice]", new object[] { this.state });
			}
			Utils.Verify(!string.IsNullOrEmpty(choiceOption));
			Utils.Verify(string.IsNullOrEmpty(this.choiceOption));
			this.rootObjectInfo.AddVariant(choiceOption, TmdlObjectInfoWriter.CreateObjectInfo(objectType, this.rootObjectInfo.IsSingleton, description));
			this.choiceOption = choiceOption;
			this.state = TmdlObjectInfoWriter.InfoWriteState.RootObject;
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x00095500 File Offset: 0x00093700
		public void CompleteMetadataObjectScope(bool? additionalProperties)
		{
			if (this.state != TmdlObjectInfoWriter.InfoWriteState.RootObject)
			{
				throw TomInternalException.Create("Invalid state for completing a metadata-object scope - state={0}, valid states=[RootObject]", new object[] { this.state });
			}
			if (string.IsNullOrEmpty(this.choiceOption))
			{
				this.state = TmdlObjectInfoWriter.InfoWriteState.End;
				return;
			}
			this.choiceOption = null;
			this.state = TmdlObjectInfoWriter.InfoWriteState.RootObjectChoice;
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x00095558 File Offset: 0x00093758
		public void StartComplexPropertyScope(string propertyName, MetadataPropertyNature propertyNature, string choiceOption, string description)
		{
			TmdlObjectInfoWriter.InfoWriteState infoWriteState = this.state;
			if (infoWriteState != TmdlObjectInfoWriter.InfoWriteState.RootObject && infoWriteState - TmdlObjectInfoWriter.InfoWriteState.InnerScope > 1)
			{
				throw TomInternalException.Create("Invalid state for starting a complex property scope - state={0}, valid states=[RootObject, InnerScope, SupplementaryScope]", new object[] { this.state });
			}
			if (!string.IsNullOrEmpty(choiceOption))
			{
				this.SetSupplementaryScopeChoiceOption(choiceOption, description);
			}
			if ((propertyNature & MetadataPropertyNature.PropertyCategoryMask) == MetadataPropertyNature.CrossLinkProperty)
			{
				this.PushScope(TmdlObjectInfoWriter.InnerScope.CreateCrossLinkScope(propertyName));
				return;
			}
			ObjectType objectType = this.rootObjectInfo.ObjectType;
			if (objectType != ObjectType.Partition)
			{
				if (objectType != ObjectType.Culture)
				{
					throw TomInternalException.Create("A named complex property that is not a single-child of the current object - currentObjectType={0}, propertyName='{1}'", new object[]
					{
						this.rootObjectInfo.ObjectType,
						propertyName
					});
				}
				ObjectType currentObjectType = this.GetCurrentObjectType();
				if (string.Compare(propertyName, "translations", StringComparison.Ordinal) == 0)
				{
					this.PushScope(TmdlObjectInfoWriter.InnerScope.CreatePropertyScope(propertyName));
					return;
				}
				if (string.Compare(propertyName, "model", StringComparison.Ordinal) == 0)
				{
					this.PushScope(TmdlObjectInfoWriter.InnerScope.CreateObjectInfoScope(propertyName, ObjectType.Model, true));
					return;
				}
				if (string.IsNullOrEmpty(propertyName))
				{
					this.PushScope(TmdlObjectInfoWriter.InnerScope.CreateObjectInfoScope(null, currentObjectType, false));
					return;
				}
				ObjectType objectType2;
				bool flag;
				if (ObjectTreeHelper.IsChildJsonPropertyName(currentObjectType, propertyName, out objectType2, out flag))
				{
					this.PushScope(TmdlObjectInfoWriter.InnerScope.CreateObjectInfoScope(propertyName, objectType2, flag));
					return;
				}
				throw TomInternalException.Create("A named complex property that is not a single-child of the current object - currentObjectType={0}, propertyName='{1}'", new object[] { currentObjectType, propertyName });
			}
			else
			{
				if (string.Compare(propertyName, "source", StringComparison.Ordinal) == 0)
				{
					this.PushScope(TmdlObjectInfoWriter.InnerScope.CreatePropertyScope(propertyName));
					return;
				}
				throw TomInternalException.Create("A named complex property that is not a single-child of the current object - currentObjectType={0}, propertyName='{1}'", new object[]
				{
					ObjectType.Partition,
					propertyName
				});
			}
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x000956CC File Offset: 0x000938CC
		public void CompleteComplexPropertyScope(bool? additionalProperties)
		{
			if (this.state != TmdlObjectInfoWriter.InfoWriteState.InnerScope)
			{
				throw TomInternalException.Create("Invalid state for completing a complex property scope - state={0}, valid states=[InnerScope]", new object[] { this.state });
			}
			TmdlObjectInfoWriter.InnerScope innerScope;
			this.PopScope(out innerScope);
			this.AddCompletedScopeToCurrentScope(innerScope);
			if (this.state == TmdlObjectInfoWriter.InfoWriteState.SupplementaryScope && this.scopes[this.scopes.Count - 1].IsSupplementaryScope && this.scopes[this.scopes.Count - 1].ChoiceOption != null)
			{
				this.ResetSupplementaryScopeChoiceOption();
			}
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x00095764 File Offset: 0x00093964
		public void StartCollectionScope(string collectionName, MetadataPropertyNature collectionNature)
		{
			TmdlObjectInfoWriter.InfoWriteState infoWriteState = this.state;
			if (infoWriteState != TmdlObjectInfoWriter.InfoWriteState.InnerScope)
			{
				if (infoWriteState != TmdlObjectInfoWriter.InfoWriteState.SupplementaryScope)
				{
					throw TomInternalException.Create("Invalid state for starting a collection scope - state={0}, valid states=[InnerScope, SupplementaryScope]", new object[] { this.state });
				}
				this.UpdateSupplementaryScope(2);
				return;
			}
			else
			{
				ObjectType currentObjectType = this.GetCurrentObjectType();
				ObjectType objectType;
				bool flag;
				if (ObjectTreeHelper.IsChildJsonPropertyName(currentObjectType, collectionName, out objectType, out flag))
				{
					this.PushSupplementaryScope(collectionName, objectType, 2);
					return;
				}
				throw TomInternalException.Create("A complex property collection that is not a child-collection of the current object - currentObjectType={0}, collectionName='{1}'", new object[] { currentObjectType, collectionName });
			}
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x000957E4 File Offset: 0x000939E4
		public void CompleteCollectionScope()
		{
			if (this.state != TmdlObjectInfoWriter.InfoWriteState.SupplementaryScope)
			{
				throw TomInternalException.Create("Invalid state for completing a collection scope - state={0}, valid states=[SupplementaryScope]", new object[] { this.state });
			}
			string collectionName;
			int num;
			Utils.Verify(this.PopSupplementaryScope(false, out collectionName, out num));
			Utils.Verify(num == 2);
			if (this.state == TmdlObjectInfoWriter.InfoWriteState.RootObject)
			{
				TmdlPropertyInfo tmdlPropertyInfo = this.rootObjectInfo.Properties.FirstOrDefault((TmdlPropertyInfo p) => string.Compare(p.Name, collectionName, StringComparison.Ordinal) == 0);
				Utils.Verify(tmdlPropertyInfo != null);
				if (tmdlPropertyInfo.Children.Count == 1)
				{
					this.rootObjectInfo.RemoveProperty(tmdlPropertyInfo);
					tmdlPropertyInfo = tmdlPropertyInfo.Children.Single<TmdlPropertyInfo>();
					tmdlPropertyInfo.CanBeDuplicated = true;
					this.rootObjectInfo.AddProperty(tmdlPropertyInfo);
				}
			}
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x000958A8 File Offset: 0x00093AA8
		public void StartChoiceScope()
		{
			TmdlObjectInfoWriter.InfoWriteState infoWriteState = this.state;
			if (infoWriteState == TmdlObjectInfoWriter.InfoWriteState.Start)
			{
				this.state = TmdlObjectInfoWriter.InfoWriteState.RootObjectChoice;
				return;
			}
			if (infoWriteState != TmdlObjectInfoWriter.InfoWriteState.SupplementaryScope)
			{
				throw TomInternalException.Create("Invalid state for starting a choice scope - state={0}, valid states=[Start, SupplementaryScope]", new object[] { this.state });
			}
			this.UpdateSupplementaryScope(1);
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x000958F4 File Offset: 0x00093AF4
		public void CompleteChoiceScope()
		{
			TmdlObjectInfoWriter.InfoWriteState infoWriteState = this.state;
			if (infoWriteState == TmdlObjectInfoWriter.InfoWriteState.RootObjectChoice)
			{
				this.state = TmdlObjectInfoWriter.InfoWriteState.End;
				return;
			}
			if (infoWriteState != TmdlObjectInfoWriter.InfoWriteState.SupplementaryScope)
			{
				throw TomInternalException.Create("Invalid state for completing a choice scope - state={0}, valid states=[RootObjectChoice, SupplementaryScope]", new object[] { this.state });
			}
			string text;
			int num;
			if (this.PopSupplementaryScope(true, out text, out num))
			{
				Utils.Verify(num == 1);
				return;
			}
			Utils.Verify(num == 3);
			this.UpdateSupplementaryScope(2);
		}

		// Token: 0x06001699 RID: 5785 RVA: 0x00095960 File Offset: 0x00093B60
		public bool ShouldIncludeProperty(string propertyName, MetadataPropertyNature propertyNature)
		{
			TmdlObjectInfoWriter.InfoWriteState infoWriteState = this.state;
			if (infoWriteState == TmdlObjectInfoWriter.InfoWriteState.RootObject || infoWriteState - TmdlObjectInfoWriter.InfoWriteState.InnerScope <= 1)
			{
				return !this.filter.IgnoreProperty(this.GetCurrentObjectType(), propertyName, propertyNature);
			}
			throw TomInternalException.Create("Invalid state for writing a new property - state={0}, valid states=[RootObject, InnerScope, SupplementaryScope]", new object[] { this.state });
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x000959B4 File Offset: 0x00093BB4
		public void WriteProperty(string propertyName, MetadataPropertyNature propertyNature, Type type)
		{
			TmdlObjectInfoWriter.InfoWriteState infoWriteState = this.state;
			if (infoWriteState != TmdlObjectInfoWriter.InfoWriteState.RootObject && infoWriteState - TmdlObjectInfoWriter.InfoWriteState.InnerScope > 1)
			{
				throw TomInternalException.Create("Invalid state for writing a property - state={0}, valid states=[RootObject, InnerScope, SupplementaryScope]", new object[] { this.state });
			}
			bool flag = (propertyNature & MetadataPropertyNature.DefaultProperty) == MetadataPropertyNature.DefaultProperty;
			bool flag2 = (propertyNature & MetadataPropertyNature.Translation) == MetadataPropertyNature.Translation;
			if (type == null)
			{
				this.PushSupplementaryScope(propertyName, ObjectType.Null, -1);
				return;
			}
			if (type == typeof(string))
			{
				switch (propertyNature & MetadataPropertyNature.PropertyCategoryMask)
				{
				case MetadataPropertyNature.NameProperty:
				{
					TmdlObjectInfoWriter.PropertyKind propertyKind = TmdlObjectInfoWriter.PropertyKind.Name;
					TmdlValueType tmdlValueType = TmdlValueType.String;
					TmdlScalarValueType? tmdlScalarValueType = null;
					TmdlExpressionLanguage? tmdlExpressionLanguage = null;
					TmdlExpressionLanguage? tmdlExpressionLanguage2 = tmdlExpressionLanguage;
					Type type2 = null;
					ObjectType? objectType = null;
					this.AddPropertyToCurrentScope(propertyKind, new TmdlPropertyInfo(propertyName, tmdlValueType, tmdlScalarValueType, tmdlExpressionLanguage2, type2, objectType, null, false, false));
					return;
				}
				case MetadataPropertyNature.RegularProperty:
				{
					TmdlExpressionLanguage? tmdlExpressionLanguage;
					ObjectType? objectType;
					if (!flag2 && string.Compare(propertyName, "description", StringComparison.Ordinal) == 0)
					{
						Utils.Verify(this.state == TmdlObjectInfoWriter.InfoWriteState.RootObject, "The Description property is only valid on the MetadataObject level!");
						TmdlObjectInfoWriter.PropertyKind propertyKind2 = TmdlObjectInfoWriter.PropertyKind.Description;
						TmdlValueType tmdlValueType2 = TmdlValueType.String;
						TmdlScalarValueType? tmdlScalarValueType2 = null;
						tmdlExpressionLanguage = null;
						TmdlExpressionLanguage? tmdlExpressionLanguage3 = tmdlExpressionLanguage;
						Type type3 = null;
						objectType = null;
						this.AddPropertyToCurrentScope(propertyKind2, new TmdlPropertyInfo(propertyName, tmdlValueType2, tmdlScalarValueType2, tmdlExpressionLanguage3, type3, objectType, null, false, false));
						return;
					}
					ObjectType currentObjectType = this.GetCurrentObjectType();
					if (currentObjectType == ObjectType.TimeUnitColumnAssociation && (string.Compare(propertyName, "primaryColumn", StringComparison.Ordinal) == 0 || string.Compare(propertyName, "associatedColumn", StringComparison.Ordinal) == 0))
					{
						TmdlObjectInfoWriter.PropertyKind propertyKind3 = TmdlObjectInfoWriter.PropertyKind.Regular;
						TmdlValueType tmdlValueType3 = TmdlValueType.ModelReference;
						objectType = new ObjectType?(ObjectType.Column);
						TmdlScalarValueType? tmdlScalarValueType3 = null;
						tmdlExpressionLanguage = null;
						this.AddPropertyToCurrentScope(propertyKind3, new TmdlPropertyInfo(propertyName, tmdlValueType3, tmdlScalarValueType3, tmdlExpressionLanguage, null, objectType, null, false, false));
						return;
					}
					TmdlExpressionLanguage? tmdlExpressionLanguage4;
					if (!flag2 && currentObjectType == ObjectType.Partition && this.state == TmdlObjectInfoWriter.InfoWriteState.InnerScope && this.scopes != null && this.scopes.Count > 0 && string.Compare(this.scopes[this.scopes.Count - 1].Name, "source", StringComparison.InvariantCultureIgnoreCase) == 0 && string.Compare(propertyName, "expression", StringComparison.InvariantCultureIgnoreCase) == 0)
					{
						string text = this.scopes[this.scopes.Count - 2].ChoiceOption;
						if (!(text == "CalculatedPartitionSource"))
						{
							if (!(text == "MPartitionSource"))
							{
								tmdlExpressionLanguage4 = new TmdlExpressionLanguage?(TmdlExpressionLanguage.Other);
							}
							else
							{
								tmdlExpressionLanguage4 = new TmdlExpressionLanguage?(TmdlExpressionLanguage.M);
								flag = true;
							}
						}
						else
						{
							tmdlExpressionLanguage4 = new TmdlExpressionLanguage?(TmdlExpressionLanguage.Dax);
							flag = true;
						}
					}
					else
					{
						TmdlExpressionLanguage? tmdlExpressionLanguage5;
						if (!flag2)
						{
							tmdlExpressionLanguage5 = TmdlObjectInfoWriter.IdentifyExpressionLanguage(currentObjectType, propertyName, propertyNature);
						}
						else
						{
							tmdlExpressionLanguage = null;
							tmdlExpressionLanguage5 = tmdlExpressionLanguage;
						}
						tmdlExpressionLanguage4 = tmdlExpressionLanguage5;
					}
					TmdlObjectInfoWriter.PropertyKind propertyKind4 = (flag ? TmdlObjectInfoWriter.PropertyKind.Default : TmdlObjectInfoWriter.PropertyKind.Regular);
					TmdlValueType tmdlValueType4 = TmdlValueType.String;
					tmdlExpressionLanguage = tmdlExpressionLanguage4;
					bool flag3 = flag;
					TmdlScalarValueType? tmdlScalarValueType4 = null;
					TmdlExpressionLanguage? tmdlExpressionLanguage6 = tmdlExpressionLanguage;
					Type type4 = null;
					objectType = null;
					this.AddPropertyToCurrentScope(propertyKind4, new TmdlPropertyInfo(propertyName, tmdlValueType4, tmdlScalarValueType4, tmdlExpressionLanguage6, type4, objectType, null, flag3, false));
					return;
				}
				case MetadataPropertyNature.ParentProperty:
					return;
				case MetadataPropertyNature.CrossLinkProperty:
				{
					ObjectType objectType2;
					Utils.Verify(TmdlObjectReader.TryIdentifyCrossReferenceTargetType(this.rootObjectInfo.ObjectType, propertyName, out objectType2));
					TmdlObjectInfoWriter.PropertyKind propertyKind5 = (flag ? TmdlObjectInfoWriter.PropertyKind.Default : TmdlObjectInfoWriter.PropertyKind.Regular);
					TmdlValueType tmdlValueType5 = TmdlValueType.ModelReference;
					ObjectType? objectType = new ObjectType?(objectType2);
					bool flag3 = flag;
					TmdlScalarValueType? tmdlScalarValueType5 = null;
					TmdlExpressionLanguage? tmdlExpressionLanguage = null;
					this.AddPropertyToCurrentScope(propertyKind5, new TmdlPropertyInfo(propertyName, tmdlValueType5, tmdlScalarValueType5, tmdlExpressionLanguage, null, objectType, null, flag3, false));
					return;
				}
				case MetadataPropertyNature.ChildProperty:
				{
					Utils.Verify((propertyNature & MetadataPropertyNature.JsonString) == MetadataPropertyNature.JsonString, "A string child-property is only valid for custom JSOM properties, and required to be marked accordingly!");
					Utils.Verify(this.state == TmdlObjectInfoWriter.InfoWriteState.RootObject, "Custom JSON properties are only valid on the MetadataObject level!");
					TmdlObjectInfo tmdlObjectInfo = new TmdlObjectInfo(propertyName);
					TmdlObjectInfo tmdlObjectInfo2 = tmdlObjectInfo;
					string text2 = "additionalProperties";
					TmdlValueType tmdlValueType6 = TmdlValueType.String;
					TmdlExpressionLanguage? tmdlExpressionLanguage = new TmdlExpressionLanguage?(TmdlExpressionLanguage.Json);
					TmdlScalarValueType? tmdlScalarValueType6 = null;
					TmdlExpressionLanguage? tmdlExpressionLanguage7 = tmdlExpressionLanguage;
					Type type5 = null;
					ObjectType? objectType = null;
					tmdlObjectInfo2.DefaultProperty = new TmdlPropertyInfo(text2, tmdlValueType6, tmdlScalarValueType6, tmdlExpressionLanguage7, type5, objectType, null, true, false);
					ObjectType objectType3 = this.rootObjectInfo.ObjectType;
					if (objectType3 != ObjectType.Model)
					{
						if (objectType3 != ObjectType.DataSource)
						{
							throw TomInternalException.Create("Invalid custom JSON property - owner={0}, name='{1}'", new object[]
							{
								this.rootObjectInfo.ObjectType,
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
								TmdlObjectInfoWriter.AddStructuredDataSourceCredentialProperties(tmdlObjectInfo);
							}
							else
							{
								TmdlObjectInfoWriter.AddStructuredDataSourceOptionsProperties(tmdlObjectInfo);
							}
						}
						else
						{
							TmdlObjectInfoWriter.AddStructuredDataSourceConnectionDetailsProperties(tmdlObjectInfo);
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
						TmdlObjectInfoWriter.AddModelAutomaticAggregationOptionsProperties(tmdlObjectInfo);
					}
					else
					{
						TmdlObjectInfoWriter.AddModelDataAccessOptionsProperties(tmdlObjectInfo);
					}
					if (string.IsNullOrEmpty(this.choiceOption))
					{
						this.rootObjectInfo.AddChildObject(tmdlObjectInfo);
					}
					else
					{
						this.rootObjectInfo.Variants[this.choiceOption].AddChildObject(tmdlObjectInfo);
					}
					TmdlValueType tmdlValueType7 = TmdlValueType.Struct;
					TmdlScalarValueType? tmdlScalarValueType7 = null;
					tmdlExpressionLanguage = null;
					TmdlExpressionLanguage? tmdlExpressionLanguage8 = tmdlExpressionLanguage;
					Type type6 = null;
					objectType = null;
					TmdlPropertyInfo tmdlPropertyInfo = new TmdlPropertyInfo(propertyName, tmdlValueType7, tmdlScalarValueType7, tmdlExpressionLanguage8, type6, objectType, null, false, false);
					if (tmdlObjectInfo.HasAnyProperty(false, false))
					{
						foreach (TmdlPropertyInfo tmdlPropertyInfo2 in tmdlObjectInfo.Properties)
						{
							tmdlPropertyInfo.AddChildProperty(tmdlPropertyInfo2);
						}
					}
					tmdlPropertyInfo.MarkAsDeprecated("The custom JSON properties had been changed to objects in TMDL RC.");
					this.AddPropertyToCurrentScope(TmdlObjectInfoWriter.PropertyKind.Regular, tmdlPropertyInfo);
					return;
				}
				default:
					return;
				}
			}
			else
			{
				if (type == typeof(int) || type == typeof(uint))
				{
					TmdlObjectInfoWriter.PropertyKind propertyKind6 = (flag ? TmdlObjectInfoWriter.PropertyKind.Default : TmdlObjectInfoWriter.PropertyKind.Regular);
					TmdlValueType tmdlValueType8 = TmdlValueType.Scalar;
					TmdlScalarValueType? tmdlScalarValueType8 = new TmdlScalarValueType?(TmdlScalarValueType.Int);
					bool flag3 = flag;
					TmdlExpressionLanguage? tmdlExpressionLanguage = null;
					TmdlExpressionLanguage? tmdlExpressionLanguage9 = tmdlExpressionLanguage;
					Type type7 = null;
					ObjectType? objectType = null;
					this.AddPropertyToCurrentScope(propertyKind6, new TmdlPropertyInfo(propertyName, tmdlValueType8, tmdlScalarValueType8, tmdlExpressionLanguage9, type7, objectType, null, flag3, false));
					return;
				}
				if (type == typeof(long) || type == typeof(ulong))
				{
					TmdlObjectInfoWriter.PropertyKind propertyKind7 = (flag ? TmdlObjectInfoWriter.PropertyKind.Default : TmdlObjectInfoWriter.PropertyKind.Regular);
					TmdlValueType tmdlValueType9 = TmdlValueType.Scalar;
					TmdlScalarValueType? tmdlScalarValueType9 = new TmdlScalarValueType?(TmdlScalarValueType.Long);
					bool flag3 = flag;
					TmdlExpressionLanguage? tmdlExpressionLanguage = null;
					TmdlExpressionLanguage? tmdlExpressionLanguage10 = tmdlExpressionLanguage;
					Type type8 = null;
					ObjectType? objectType = null;
					this.AddPropertyToCurrentScope(propertyKind7, new TmdlPropertyInfo(propertyName, tmdlValueType9, tmdlScalarValueType9, tmdlExpressionLanguage10, type8, objectType, null, flag3, false));
					return;
				}
				if (type == typeof(double))
				{
					TmdlObjectInfoWriter.PropertyKind propertyKind8 = (flag ? TmdlObjectInfoWriter.PropertyKind.Default : TmdlObjectInfoWriter.PropertyKind.Regular);
					TmdlValueType tmdlValueType10 = TmdlValueType.Scalar;
					TmdlScalarValueType? tmdlScalarValueType10 = new TmdlScalarValueType?(TmdlScalarValueType.Double);
					bool flag3 = flag;
					TmdlExpressionLanguage? tmdlExpressionLanguage = null;
					TmdlExpressionLanguage? tmdlExpressionLanguage11 = tmdlExpressionLanguage;
					Type type9 = null;
					ObjectType? objectType = null;
					this.AddPropertyToCurrentScope(propertyKind8, new TmdlPropertyInfo(propertyName, tmdlValueType10, tmdlScalarValueType10, tmdlExpressionLanguage11, type9, objectType, null, flag3, false));
					return;
				}
				if (type == typeof(DateTime))
				{
					TmdlObjectInfoWriter.PropertyKind propertyKind9 = (flag ? TmdlObjectInfoWriter.PropertyKind.Default : TmdlObjectInfoWriter.PropertyKind.Regular);
					TmdlValueType tmdlValueType11 = TmdlValueType.Scalar;
					TmdlScalarValueType? tmdlScalarValueType11 = new TmdlScalarValueType?(TmdlScalarValueType.Date);
					bool flag3 = flag;
					TmdlExpressionLanguage? tmdlExpressionLanguage = null;
					TmdlExpressionLanguage? tmdlExpressionLanguage12 = tmdlExpressionLanguage;
					Type type10 = null;
					ObjectType? objectType = null;
					this.AddPropertyToCurrentScope(propertyKind9, new TmdlPropertyInfo(propertyName, tmdlValueType11, tmdlScalarValueType11, tmdlExpressionLanguage12, type10, objectType, null, flag3, false));
					return;
				}
				if (type == typeof(bool))
				{
					TmdlObjectInfoWriter.PropertyKind propertyKind10 = (flag ? TmdlObjectInfoWriter.PropertyKind.Default : TmdlObjectInfoWriter.PropertyKind.Regular);
					TmdlValueType tmdlValueType12 = TmdlValueType.Scalar;
					TmdlScalarValueType? tmdlScalarValueType12 = new TmdlScalarValueType?(TmdlScalarValueType.Bool);
					bool flag3 = flag;
					TmdlExpressionLanguage? tmdlExpressionLanguage = null;
					TmdlExpressionLanguage? tmdlExpressionLanguage13 = tmdlExpressionLanguage;
					Type type11 = null;
					ObjectType? objectType = null;
					this.AddPropertyToCurrentScope(propertyKind10, new TmdlPropertyInfo(propertyName, tmdlValueType12, tmdlScalarValueType12, tmdlExpressionLanguage13, type11, objectType, null, flag3, false));
					return;
				}
				throw TomInternalException.Create("Invalid type for a metadata property - {0} is not a supported type", new object[] { type.FullName });
			}
		}

		// Token: 0x0600169B RID: 5787 RVA: 0x00096090 File Offset: 0x00094290
		public void WriteEnumProperty<TEnum>(string propertyName, MetadataPropertyNature propertyNature, IEnumerable<TEnum> values) where TEnum : Enum
		{
			TmdlObjectInfoWriter.InfoWriteState infoWriteState = this.state;
			if (infoWriteState == TmdlObjectInfoWriter.InfoWriteState.RootObject || infoWriteState == TmdlObjectInfoWriter.InfoWriteState.InnerScope)
			{
				Type typeFromHandle = typeof(TEnum);
				Utils.Verify(typeFromHandle.IsEnum);
				ObjectType currentObjectType = this.GetCurrentObjectType();
				if (currentObjectType != ObjectType.Partition)
				{
					if (currentObjectType == ObjectType.TimeUnitColumnAssociation)
					{
						if (string.Compare(propertyName, "timeUnit", StringComparison.Ordinal) == 0)
						{
							Utils.Verify(typeof(TEnum) == typeof(TimeUnit));
							return;
						}
					}
				}
				else if (string.Compare(propertyName, "type", StringComparison.Ordinal) == 0)
				{
					propertyName = "sourceType";
				}
				bool flag = (propertyNature & MetadataPropertyNature.DefaultProperty) == MetadataPropertyNature.DefaultProperty;
				TmdlObjectInfoWriter.PropertyKind propertyKind = (flag ? TmdlObjectInfoWriter.PropertyKind.Default : TmdlObjectInfoWriter.PropertyKind.Regular);
				string text = propertyName;
				TmdlValueType tmdlValueType = TmdlValueType.Scalar;
				TmdlScalarValueType? tmdlScalarValueType = new TmdlScalarValueType?(TmdlScalarValueType.Enum);
				Type type = typeFromHandle;
				bool flag2 = flag;
				this.AddPropertyToCurrentScope(propertyKind, new TmdlPropertyInfo(text, tmdlValueType, tmdlScalarValueType, null, type, null, null, flag2, false));
				return;
			}
			throw TomInternalException.Create("Invalid state for writing an enum property - state={0}, valid states=[RootObject, InnerScope]", new object[] { this.state });
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x00096184 File Offset: 0x00094384
		public void WriteSingleChild(SerializationActivityContext context, string propertyName, MetadataPropertyNature propertyNature, ObjectType objectType)
		{
			if (this.state != TmdlObjectInfoWriter.InfoWriteState.RootObject)
			{
				throw TomInternalException.Create("Invalid state for writing a single-child property - state={0}, valid states=[RootObject]", new object[] { this.state });
			}
			TmdlObjectInfo tmdlObjectInfo;
			if (this.metadataObjects != null)
			{
				if (!this.metadataObjects.TryGetValue(objectType, out tmdlObjectInfo))
				{
					TmdlObjectInfoWriter tmdlObjectInfoWriter = new TmdlObjectInfoWriter(this.metadataObjects, this.filter, objectType, true);
					MetadataObject.WriteMetadataObjectSchema(context, objectType, tmdlObjectInfoWriter);
					tmdlObjectInfo = tmdlObjectInfoWriter.ExtractObjectInfo(true);
					this.metadataObjects.Add(objectType, tmdlObjectInfo);
				}
				if (string.Compare(objectType.ToString("G"), propertyName, StringComparison.InvariantCultureIgnoreCase) != 0)
				{
					tmdlObjectInfo = tmdlObjectInfo.Clone(propertyName, true);
				}
			}
			else
			{
				TmdlObjectInfoWriter tmdlObjectInfoWriter2 = new TmdlObjectInfoWriter(null, this.filter, objectType, true);
				MetadataObject.WriteMetadataObjectSchema(context, objectType, tmdlObjectInfoWriter2);
				tmdlObjectInfo = tmdlObjectInfoWriter2.ExtractObjectInfo(false);
				if (string.Compare(objectType.ToString("G"), propertyName, StringComparison.InvariantCultureIgnoreCase) != 0)
				{
					tmdlObjectInfo.PropertyName = propertyName;
				}
				tmdlObjectInfo.MakeReadOnly();
			}
			TmdlValueType tmdlValueType = TmdlValueType.MetadataObject;
			ObjectType? objectType2 = new ObjectType?(objectType);
			TmdlPropertyInfo tmdlPropertyInfo = new TmdlPropertyInfo(propertyName, tmdlValueType, null, null, null, objectType2, null, false, false);
			TmdlPropertyInfo tmdlPropertyInfo2;
			if (tmdlObjectInfo.IsDefaultPropertyAllowed(out tmdlPropertyInfo2))
			{
				tmdlPropertyInfo.AddChildProperty(tmdlPropertyInfo2);
			}
			if (tmdlObjectInfo.HasVariants)
			{
				if (tmdlObjectInfo.Variants.Count > 1)
				{
					Dictionary<string, TmdlPropertyInfo> dictionary = new Dictionary<string, TmdlPropertyInfo>(StringComparer.InvariantCultureIgnoreCase);
					using (IEnumerator<KeyValuePair<string, TmdlObjectInfo>> enumerator = tmdlObjectInfo.Variants.Where((KeyValuePair<string, TmdlObjectInfo> v) => v.Value.HasAnyProperty(false, false)).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							KeyValuePair<string, TmdlObjectInfo> keyValuePair = enumerator.Current;
							foreach (TmdlPropertyInfo tmdlPropertyInfo3 in keyValuePair.Value.Properties)
							{
								TmdlPropertyInfo tmdlPropertyInfo4;
								if (dictionary.TryGetValue(tmdlPropertyInfo3.Name, out tmdlPropertyInfo4))
								{
									if (tmdlPropertyInfo3.Type != tmdlPropertyInfo4.Type)
									{
										throw TomInternalException.Create("There is a conflict between the properties of the variants - variant {0} has property named '{1}' of type {2}, while another variant has that property with type {3}", new object[] { keyValuePair.Key, tmdlPropertyInfo3.Name, tmdlPropertyInfo3.Type, tmdlPropertyInfo4.Type });
									}
									TmdlValueType type = tmdlPropertyInfo4.Type;
									if (type != TmdlValueType.Scalar)
									{
										if (type == TmdlValueType.MetadataObject)
										{
											objectType2 = tmdlPropertyInfo3.MetadataObjectType;
											ObjectType value = objectType2.Value;
											objectType2 = tmdlPropertyInfo4.MetadataObjectType;
											if (value != objectType2.Value)
											{
												string text = "There is a conflict between the properties of the variants - variant {0} has property named '{1}' of metadata-object type {2}, while another variant has that property with metadata-object type {3}";
												object[] array = new object[4];
												array[0] = keyValuePair.Key;
												array[1] = tmdlPropertyInfo3.Name;
												int num = 2;
												objectType2 = tmdlPropertyInfo3.MetadataObjectType;
												array[num] = objectType2.Value;
												int num2 = 3;
												objectType2 = tmdlPropertyInfo4.MetadataObjectType;
												array[num2] = objectType2.Value;
												throw TomInternalException.Create(text, array);
											}
										}
									}
									else
									{
										if (tmdlPropertyInfo3.ScalarValueType.Value != tmdlPropertyInfo4.ScalarValueType.Value)
										{
											throw TomInternalException.Create("There is a conflict between the properties of the variants - variant {0} has property named '{1}' of scalar type {2}, while another variant has that property with scalar type {3}", new object[]
											{
												keyValuePair.Key,
												tmdlPropertyInfo3.Name,
												tmdlPropertyInfo3.ScalarValueType.Value,
												tmdlPropertyInfo4.ScalarValueType.Value
											});
										}
										if (tmdlPropertyInfo4.ScalarValueType.Value == TmdlScalarValueType.Enum && tmdlPropertyInfo3.EnumType != tmdlPropertyInfo4.EnumType)
										{
											string text2 = "There is a conflict between the properties of the variants - variant {0} has property named '{1}' of enum type {2}, while another variant has that property with enum type {3}";
											object[] array2 = new object[4];
											array2[0] = keyValuePair.Key;
											array2[1] = tmdlPropertyInfo3.Name;
											int num3 = 2;
											Type enumType = tmdlPropertyInfo3.EnumType;
											array2[num3] = ((enumType != null) ? enumType.Name : null);
											int num4 = 3;
											Type enumType2 = tmdlPropertyInfo4.EnumType;
											array2[num4] = ((enumType2 != null) ? enumType2.Name : null);
											throw TomInternalException.Create(text2, array2);
										}
									}
								}
								else
								{
									tmdlPropertyInfo.AddChildProperty(tmdlPropertyInfo3);
									dictionary.Add(tmdlPropertyInfo3.Name, tmdlPropertyInfo3);
								}
							}
						}
						goto IL_0482;
					}
				}
				TmdlObjectInfo value2 = tmdlObjectInfo.Variants.Single<KeyValuePair<string, TmdlObjectInfo>>().Value;
				if (!value2.HasAnyProperty(false, false))
				{
					goto IL_0482;
				}
				using (IEnumerator<TmdlPropertyInfo> enumerator2 = value2.Properties.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						TmdlPropertyInfo tmdlPropertyInfo5 = enumerator2.Current;
						tmdlPropertyInfo.AddChildProperty(tmdlPropertyInfo5);
					}
					goto IL_0482;
				}
			}
			if (tmdlObjectInfo.HasAnyProperty(false, false))
			{
				foreach (TmdlPropertyInfo tmdlPropertyInfo6 in tmdlObjectInfo.Properties)
				{
					tmdlPropertyInfo.AddChildProperty(tmdlPropertyInfo6);
				}
			}
			IL_0482:
			tmdlPropertyInfo.MarkAsDeprecated("The single-child properties were removed in TMDL RC.");
			if (string.IsNullOrEmpty(this.choiceOption))
			{
				this.rootObjectInfo.AddProperty(tmdlPropertyInfo);
				this.rootObjectInfo.AddChildObject(tmdlObjectInfo);
				return;
			}
			this.rootObjectInfo.Variants[this.choiceOption].AddProperty(tmdlPropertyInfo);
			this.rootObjectInfo.Variants[this.choiceOption].AddChildObject(tmdlObjectInfo);
		}

		// Token: 0x0600169D RID: 5789 RVA: 0x000966E0 File Offset: 0x000948E0
		public void WriteChildCollection(SerializationActivityContext context, string propertyName, MetadataPropertyNature propertyNature, ObjectType objectType)
		{
			if (this.state != TmdlObjectInfoWriter.InfoWriteState.RootObject)
			{
				throw TomInternalException.Create("Invalid state for writing a child-collection property - state={0}, valid states=[RootObject]", new object[] { this.state });
			}
			ObjectType objectType2 = this.rootObjectInfo.ObjectType;
			if (objectType2 != ObjectType.Role)
			{
				if (objectType2 == ObjectType.RelatedColumnDetails)
				{
					if (string.Compare(propertyName, "groupByColumns", StringComparison.Ordinal) == 0)
					{
						string text = "groupByColumn";
						TmdlValueType tmdlValueType = TmdlValueType.ModelReference;
						ObjectType? objectType3 = new ObjectType?(ObjectType.Column);
						TmdlPropertyInfo tmdlPropertyInfo = new TmdlPropertyInfo(text, tmdlValueType, null, null, null, objectType3, null, false, true);
						this.rootObjectInfo.AddProperty(tmdlPropertyInfo);
						TmdlValueType tmdlValueType2 = TmdlValueType.Collection;
						TmdlScalarValueType? tmdlScalarValueType = null;
						TmdlExpressionLanguage? tmdlExpressionLanguage = null;
						Type type = null;
						objectType3 = null;
						TmdlPropertyInfo tmdlPropertyInfo2 = new TmdlPropertyInfo(propertyName, tmdlValueType2, tmdlScalarValueType, tmdlExpressionLanguage, type, objectType3, null, false, false);
						TmdlPropertyInfo tmdlPropertyInfo3 = tmdlPropertyInfo2;
						string text2 = "groupingColumn";
						TmdlValueType tmdlValueType3 = TmdlValueType.ModelReference;
						objectType3 = new ObjectType?(ObjectType.Column);
						tmdlPropertyInfo3.AddChildProperty(new TmdlPropertyInfo(text2, tmdlValueType3, null, null, null, objectType3, null, false, false));
						tmdlPropertyInfo2.MarkAsDeprecated("GroupByColumns are serialized as separate properties starting the RC.");
						this.rootObjectInfo.AddProperty(tmdlPropertyInfo2);
						return;
					}
				}
			}
			else if (string.Compare(propertyName, "members", StringComparison.Ordinal) == 0)
			{
				ObjectType? objectType3;
				TmdlObjectInfo tmdlObjectInfo;
				if (this.metadataObjects != null)
				{
					if (!this.metadataObjects.TryGetValue(ObjectType.RoleMembership, out tmdlObjectInfo))
					{
						tmdlObjectInfo = new TmdlObjectInfo(ObjectType.RoleMembership);
						tmdlObjectInfo.Description = "ModelRoleMember object of Tabular Object Model (TOM)";
						TmdlObjectInfo tmdlObjectInfo2 = tmdlObjectInfo;
						string text3 = "memberId";
						TmdlValueType tmdlValueType4 = TmdlValueType.String;
						TmdlScalarValueType? tmdlScalarValueType2 = null;
						TmdlExpressionLanguage? tmdlExpressionLanguage2 = null;
						Type type2 = null;
						objectType3 = null;
						tmdlObjectInfo2.NameProperty = new TmdlPropertyInfo(text3, tmdlValueType4, tmdlScalarValueType2, tmdlExpressionLanguage2, type2, objectType3, null, false, false);
						TmdlObjectInfo tmdlObjectInfo3 = tmdlObjectInfo;
						string text4 = "memberType";
						TmdlValueType tmdlValueType5 = TmdlValueType.Scalar;
						TmdlScalarValueType? tmdlScalarValueType3 = new TmdlScalarValueType?(TmdlScalarValueType.Enum);
						Type type3 = typeof(TmdlRoleMemberType);
						TmdlExpressionLanguage? tmdlExpressionLanguage3 = null;
						Type type4 = type3;
						objectType3 = null;
						tmdlObjectInfo3.DefaultProperty = new TmdlPropertyInfo(text4, tmdlValueType5, tmdlScalarValueType3, tmdlExpressionLanguage3, type4, objectType3, null, true, false);
						TmdlObjectInfo tmdlObjectInfo4 = tmdlObjectInfo;
						string text5 = "identityProvider";
						TmdlValueType tmdlValueType6 = TmdlValueType.String;
						TmdlScalarValueType? tmdlScalarValueType4 = null;
						TmdlExpressionLanguage? tmdlExpressionLanguage4 = null;
						Type type5 = null;
						objectType3 = null;
						tmdlObjectInfo4.AddProperty(new TmdlPropertyInfo(text5, tmdlValueType6, tmdlScalarValueType4, tmdlExpressionLanguage4, type5, objectType3, null, false, false));
						if (CompatibilityRestrictions.ExtendedProperty.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && !this.filter.IgnoreProperty(ObjectType.RoleMembership, "extendedProperties", MetadataPropertyNature.ChildCollection))
						{
							TmdlObjectInfo tmdlObjectInfo5;
							if (!this.metadataObjects.TryGetValue(ObjectType.ExtendedProperty, out tmdlObjectInfo5))
							{
								TmdlObjectInfoWriter tmdlObjectInfoWriter = new TmdlObjectInfoWriter(this.metadataObjects, this.filter, ObjectType.ExtendedProperty, false);
								MetadataObject.WriteMetadataObjectSchema(context, ObjectType.ExtendedProperty, tmdlObjectInfoWriter);
								tmdlObjectInfo5 = tmdlObjectInfoWriter.ExtractObjectInfo(true);
								this.metadataObjects.Add(ObjectType.ExtendedProperty, tmdlObjectInfo5);
							}
							tmdlObjectInfo.AddChildObject(tmdlObjectInfo5);
						}
						if (!this.filter.IgnoreProperty(ObjectType.RoleMembership, "annotations", MetadataPropertyNature.ChildCollection))
						{
							TmdlObjectInfo tmdlObjectInfo6;
							if (!this.metadataObjects.TryGetValue(ObjectType.Annotation, out tmdlObjectInfo6))
							{
								TmdlObjectInfoWriter tmdlObjectInfoWriter2 = new TmdlObjectInfoWriter(null, this.filter, ObjectType.Annotation, false);
								MetadataObject.WriteMetadataObjectSchema(context, ObjectType.Annotation, tmdlObjectInfoWriter2);
								tmdlObjectInfo6 = tmdlObjectInfoWriter2.ExtractObjectInfo(true);
								this.metadataObjects.Add(ObjectType.Annotation, tmdlObjectInfo6);
							}
							tmdlObjectInfo.AddChildObject(tmdlObjectInfo6);
						}
						this.metadataObjects.Add(ObjectType.RoleMembership, tmdlObjectInfo);
					}
				}
				else
				{
					tmdlObjectInfo = new TmdlObjectInfo(ObjectType.RoleMembership);
					tmdlObjectInfo.Description = "ModelRoleMember object of Tabular Object Model (TOM)";
					TmdlObjectInfo tmdlObjectInfo7 = tmdlObjectInfo;
					string text6 = "memberId";
					TmdlValueType tmdlValueType7 = TmdlValueType.String;
					TmdlScalarValueType? tmdlScalarValueType5 = null;
					TmdlExpressionLanguage? tmdlExpressionLanguage5 = null;
					Type type6 = null;
					objectType3 = null;
					tmdlObjectInfo7.NameProperty = new TmdlPropertyInfo(text6, tmdlValueType7, tmdlScalarValueType5, tmdlExpressionLanguage5, type6, objectType3, null, false, false);
					TmdlObjectInfo tmdlObjectInfo8 = tmdlObjectInfo;
					string text7 = "memberType";
					TmdlValueType tmdlValueType8 = TmdlValueType.Scalar;
					TmdlScalarValueType? tmdlScalarValueType6 = new TmdlScalarValueType?(TmdlScalarValueType.Enum);
					Type type3 = typeof(TmdlRoleMemberType);
					TmdlExpressionLanguage? tmdlExpressionLanguage6 = null;
					Type type7 = type3;
					objectType3 = null;
					tmdlObjectInfo8.DefaultProperty = new TmdlPropertyInfo(text7, tmdlValueType8, tmdlScalarValueType6, tmdlExpressionLanguage6, type7, objectType3, null, true, false);
					TmdlObjectInfo tmdlObjectInfo9 = tmdlObjectInfo;
					string text8 = "identityProvider";
					TmdlValueType tmdlValueType9 = TmdlValueType.String;
					TmdlScalarValueType? tmdlScalarValueType7 = null;
					TmdlExpressionLanguage? tmdlExpressionLanguage7 = null;
					Type type8 = null;
					objectType3 = null;
					tmdlObjectInfo9.AddProperty(new TmdlPropertyInfo(text8, tmdlValueType9, tmdlScalarValueType7, tmdlExpressionLanguage7, type8, objectType3, null, false, false));
					if (CompatibilityRestrictions.ExtendedProperty.IsCompatible(context.CompatibilityMode, context.DbCompatibilityLevel) && !this.filter.IgnoreProperty(ObjectType.RoleMembership, "extendedProperties", MetadataPropertyNature.ChildCollection))
					{
						TmdlObjectInfoWriter tmdlObjectInfoWriter3 = new TmdlObjectInfoWriter(null, this.filter, ObjectType.ExtendedProperty, false);
						MetadataObject.WriteMetadataObjectSchema(context, ObjectType.ExtendedProperty, tmdlObjectInfoWriter3);
						tmdlObjectInfo.AddChildObject(tmdlObjectInfoWriter3.ExtractObjectInfo(true));
					}
					if (!this.filter.IgnoreProperty(ObjectType.RoleMembership, "annotations", MetadataPropertyNature.ChildCollection))
					{
						TmdlObjectInfoWriter tmdlObjectInfoWriter4 = new TmdlObjectInfoWriter(null, this.filter, ObjectType.Annotation, false);
						MetadataObject.WriteMetadataObjectSchema(context, ObjectType.Annotation, tmdlObjectInfoWriter4);
						tmdlObjectInfo.AddChildObject(tmdlObjectInfoWriter4.ExtractObjectInfo(true));
					}
				}
				tmdlObjectInfo.MakeReadOnly();
				this.rootObjectInfo.AddChildObject(tmdlObjectInfo);
				TmdlValueType tmdlValueType10 = TmdlValueType.Collection;
				TmdlScalarValueType? tmdlScalarValueType8 = null;
				TmdlExpressionLanguage? tmdlExpressionLanguage8 = null;
				Type type9 = null;
				objectType3 = null;
				TmdlPropertyInfo tmdlPropertyInfo4 = new TmdlPropertyInfo(propertyName, tmdlValueType10, tmdlScalarValueType8, tmdlExpressionLanguage8, type9, objectType3, null, false, false);
				TmdlPropertyInfo tmdlPropertyInfo5 = tmdlPropertyInfo4;
				string text9 = "adMember";
				TmdlValueType tmdlValueType11 = TmdlValueType.String;
				TmdlScalarValueType? tmdlScalarValueType9 = null;
				TmdlExpressionLanguage? tmdlExpressionLanguage9 = null;
				Type type10 = null;
				objectType3 = null;
				tmdlPropertyInfo5.AddChildProperty(new TmdlPropertyInfo(text9, tmdlValueType11, tmdlScalarValueType9, tmdlExpressionLanguage9, type10, objectType3, null, false, false));
				TmdlPropertyInfo tmdlPropertyInfo6 = tmdlPropertyInfo4;
				string text10 = "user";
				TmdlValueType tmdlValueType12 = TmdlValueType.String;
				TmdlScalarValueType? tmdlScalarValueType10 = null;
				TmdlExpressionLanguage? tmdlExpressionLanguage10 = null;
				Type type11 = null;
				objectType3 = null;
				tmdlPropertyInfo6.AddChildProperty(new TmdlPropertyInfo(text10, tmdlValueType12, tmdlScalarValueType10, tmdlExpressionLanguage10, type11, objectType3, null, false, false));
				TmdlPropertyInfo tmdlPropertyInfo7 = tmdlPropertyInfo4;
				string text11 = "group";
				TmdlValueType tmdlValueType13 = TmdlValueType.String;
				TmdlScalarValueType? tmdlScalarValueType11 = null;
				TmdlExpressionLanguage? tmdlExpressionLanguage11 = null;
				Type type12 = null;
				objectType3 = null;
				tmdlPropertyInfo7.AddChildProperty(new TmdlPropertyInfo(text11, tmdlValueType13, tmdlScalarValueType11, tmdlExpressionLanguage11, type12, objectType3, null, false, false));
				TmdlPropertyInfo tmdlPropertyInfo8 = tmdlPropertyInfo4;
				string text12 = "external";
				TmdlValueType tmdlValueType14 = TmdlValueType.String;
				TmdlScalarValueType? tmdlScalarValueType12 = null;
				TmdlExpressionLanguage? tmdlExpressionLanguage12 = null;
				Type type13 = null;
				objectType3 = null;
				tmdlPropertyInfo8.AddChildProperty(new TmdlPropertyInfo(text12, tmdlValueType14, tmdlScalarValueType12, tmdlExpressionLanguage12, type13, objectType3, null, false, false));
				TmdlPropertyInfo tmdlPropertyInfo9 = tmdlPropertyInfo4;
				string text13 = "identityProvider";
				TmdlValueType tmdlValueType15 = TmdlValueType.String;
				TmdlScalarValueType? tmdlScalarValueType13 = null;
				TmdlExpressionLanguage? tmdlExpressionLanguage13 = null;
				Type type14 = null;
				objectType3 = null;
				tmdlPropertyInfo9.AddChildProperty(new TmdlPropertyInfo(text13, tmdlValueType15, tmdlScalarValueType13, tmdlExpressionLanguage13, type14, objectType3, null, false, false));
				tmdlPropertyInfo4.MarkAsDeprecated("Role-Members are serialized as objects starting the RC.");
				this.rootObjectInfo.AddProperty(tmdlPropertyInfo4);
				return;
			}
			TmdlObjectInfo tmdlObjectInfo10;
			if (this.metadataObjects != null)
			{
				if (!this.metadataObjects.TryGetValue(objectType, out tmdlObjectInfo10))
				{
					TmdlObjectInfoWriter tmdlObjectInfoWriter5 = new TmdlObjectInfoWriter(this.metadataObjects, this.filter, objectType, false);
					MetadataObject.WriteMetadataObjectSchema(context, objectType, tmdlObjectInfoWriter5);
					tmdlObjectInfo10 = tmdlObjectInfoWriter5.ExtractObjectInfo(true);
					this.metadataObjects.Add(objectType, tmdlObjectInfo10);
				}
			}
			else
			{
				TmdlObjectInfoWriter tmdlObjectInfoWriter6 = new TmdlObjectInfoWriter(null, this.filter, objectType, false);
				MetadataObject.WriteMetadataObjectSchema(context, objectType, tmdlObjectInfoWriter6);
				tmdlObjectInfo10 = tmdlObjectInfoWriter6.ExtractObjectInfo(true);
			}
			if (string.IsNullOrEmpty(this.choiceOption))
			{
				this.rootObjectInfo.AddChildObject(tmdlObjectInfo10);
				return;
			}
			this.rootObjectInfo.Variants[this.choiceOption].AddChildObject(tmdlObjectInfo10);
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x00096D1C File Offset: 0x00094F1C
		public TmdlObjectInfo ExtractObjectInfo(bool makeReadOnly = true)
		{
			Utils.Verify(this.state == TmdlObjectInfoWriter.InfoWriteState.End);
			TmdlObjectInfo tmdlObjectInfo11;
			try
			{
				if (this.rootObjectInfo.HasVariants && this.rootObjectInfo.Variants.Count > 1)
				{
					List<KeyValuePair<string, TmdlPropertyInfo>> list = new List<KeyValuePair<string, TmdlPropertyInfo>>(from v in this.rootObjectInfo.Variants
						where v.Value.DefaultProperty != null
						select new KeyValuePair<string, TmdlPropertyInfo>(v.Key, v.Value.DefaultProperty));
					if (list.Count > 1)
					{
						for (int i = 1; i < list.Count; i++)
						{
							if (string.Compare(list[0].Value.Name, list[i].Value.Name, StringComparison.Ordinal) != 0)
							{
								throw TomInternalException.Create("There is a conflict between the default properties of the variants - variant {0} has {1} as default property, variant {2} has {3} as default property", new object[]
								{
									list[0].Key,
									list[0].Value.Name,
									list[i].Key,
									list[i].Value.Name
								});
							}
							if (list[0].Value.Type != list[i].Value.Type)
							{
								throw TomInternalException.Create("There is a conflict between the default properties of the variants - variant {0} has default property of type {1}, variant {2} has default property of type {3}", new object[]
								{
									list[0].Key,
									list[0].Value.Type,
									list[i].Key,
									list[i].Value.Type
								});
							}
							if (list[0].Value.Type == TmdlValueType.Scalar)
							{
								if (list[0].Value.ScalarValueType.Value != list[i].Value.ScalarValueType.Value)
								{
									throw TomInternalException.Create("There is a conflict between the default properties of the variants - variant {0} has default property of scalar-type {1}, variant {2} has default property of scalar-type {3}", new object[]
									{
										list[0].Key,
										list[0].Value.ScalarValueType.Value,
										list[i].Key,
										list[i].Value.ScalarValueType.Value
									});
								}
								if (list[0].Value.ScalarValueType.Value == TmdlScalarValueType.Enum && list[0].Value.EnumType != list[i].Value.EnumType)
								{
									string text = "There is a conflict between the default properties of the variants - variant {0} has default property of enum-type {1}, variant {2} has default property of enum-type {3}";
									object[] array = new object[4];
									array[0] = list[0].Key;
									int num = 1;
									Type enumType = list[0].Value.EnumType;
									array[num] = ((enumType != null) ? enumType.Name : null);
									array[2] = list[i].Key;
									int num2 = 3;
									Type enumType2 = list[i].Value.EnumType;
									array[num2] = ((enumType2 != null) ? enumType2.Name : null);
									throw TomInternalException.Create(text, array);
								}
							}
						}
					}
					List<KeyValuePair<string, ICollection<TmdlObjectInfo>>> list2 = new List<KeyValuePair<string, ICollection<TmdlObjectInfo>>>(this.rootObjectInfo.Variants.Count);
					foreach (KeyValuePair<string, TmdlObjectInfo> keyValuePair in this.rootObjectInfo.Variants)
					{
						List<TmdlObjectInfo> list3;
						if (keyValuePair.Value.HasAnyChild(false))
						{
							list3 = new List<TmdlObjectInfo>(keyValuePair.Value.ChildObjects.Where((TmdlObjectInfo c) => c.ObjectType > ObjectType.Null));
						}
						else
						{
							list3 = new List<TmdlObjectInfo>(0);
						}
						list2.Add(new KeyValuePair<string, ICollection<TmdlObjectInfo>>(keyValuePair.Key, list3));
					}
					Dictionary<ObjectType, TmdlObjectInfo> dictionary = new Dictionary<ObjectType, TmdlObjectInfo>(list2[0].Value.Count);
					foreach (TmdlObjectInfo tmdlObjectInfo in list2[0].Value)
					{
						dictionary.Add(tmdlObjectInfo.ObjectType, tmdlObjectInfo);
					}
					for (int j = 1; j < list2.Count; j++)
					{
						if (list2[j].Value.Count != dictionary.Count)
						{
							throw TomInternalException.Create("There is a conflict between the set of children of the variants - variant {0} has {1} children, variant {2} has {3} children", new object[]
							{
								list2[0].Key,
								dictionary.Count,
								list2[j].Key,
								list2[j].Value.Count
							});
						}
						foreach (TmdlObjectInfo tmdlObjectInfo2 in list2[j].Value)
						{
							TmdlObjectInfo tmdlObjectInfo3;
							if (!dictionary.TryGetValue(tmdlObjectInfo2.ObjectType, out tmdlObjectInfo3))
							{
								throw TomInternalException.Create("There is a conflict between the set of children of the variants - variant {0} do not has {1} as child, variant {2} has {1} as child", new object[]
								{
									list2[0].Key,
									tmdlObjectInfo2.ObjectType.ToString("G"),
									list2[j].Key
								});
							}
							if (!((IEquatable<TmdlObjectInfo>)tmdlObjectInfo3).Equals(tmdlObjectInfo2))
							{
								throw TomInternalException.Create("There is a conflict between the set of children of the variants - variant {0} has {1} as a child, but the definition in variant {2} is different", new object[]
								{
									list2[0].Key,
									tmdlObjectInfo2.ObjectType.ToString("G"),
									list2[j].Key
								});
							}
						}
					}
				}
				ObjectType objectType = this.rootObjectInfo.ObjectType;
				if (objectType <= ObjectType.Table)
				{
					if (objectType != ObjectType.Model)
					{
						if (objectType == ObjectType.Table)
						{
							this.rootObjectInfo.AddProperty(new TmdlPropertyInfo("ordinal", TmdlValueType.Scalar, new TmdlScalarValueType?(TmdlScalarValueType.Int), null, null, null, null, false, false).ApplyDeprecationStatus("The property was removed in TMDL Preview-7."));
						}
					}
					else
					{
						this.rootObjectInfo.AddProperty(new TmdlPropertyInfo("id", TmdlValueType.String, null, null, null, null, null, false, false).ApplyDeprecationStatus("The property was removed in TMDL Preview-7."));
						this.rootObjectInfo.AddProperty(new TmdlPropertyInfo("compatibilityLevel", TmdlValueType.Scalar, new TmdlScalarValueType?(TmdlScalarValueType.Int), null, null, null, null, false, false).ApplyDeprecationStatus("The property was removed in TMDL Preview-7."));
						this.rootObjectInfo.AddProperty(new TmdlPropertyInfo("language", TmdlValueType.Scalar, new TmdlScalarValueType?(TmdlScalarValueType.Int), null, null, null, null, false, false).ApplyDeprecationStatus("The property was removed in TMDL Preview-7."));
					}
				}
				else
				{
					switch (objectType)
					{
					case ObjectType.PerspectiveTable:
					{
						TmdlObjectInfo tmdlObjectInfo4 = this.rootObjectInfo;
						string text2 = "Table".ToJsonCase();
						TmdlValueType tmdlValueType = TmdlValueType.ModelReference;
						ObjectType? objectType2 = new ObjectType?(ObjectType.Table);
						tmdlObjectInfo4.AddProperty(new TmdlPropertyInfo(text2, tmdlValueType, null, null, null, objectType2, null, false, false).ApplyDeprecationStatus("The property was removed in TMDL Preview-3."));
						break;
					}
					case ObjectType.PerspectiveColumn:
					{
						TmdlObjectInfo tmdlObjectInfo5 = this.rootObjectInfo;
						string text3 = "Column".ToJsonCase();
						TmdlValueType tmdlValueType2 = TmdlValueType.ModelReference;
						ObjectType? objectType2 = new ObjectType?(ObjectType.Column);
						tmdlObjectInfo5.AddProperty(new TmdlPropertyInfo(text3, tmdlValueType2, null, null, null, objectType2, null, false, false).ApplyDeprecationStatus("The property was removed in TMDL Preview-3."));
						break;
					}
					case ObjectType.PerspectiveHierarchy:
					{
						TmdlObjectInfo tmdlObjectInfo6 = this.rootObjectInfo;
						string text4 = "Hierarchy".ToJsonCase();
						TmdlValueType tmdlValueType3 = TmdlValueType.ModelReference;
						ObjectType? objectType2 = new ObjectType?(ObjectType.Hierarchy);
						tmdlObjectInfo6.AddProperty(new TmdlPropertyInfo(text4, tmdlValueType3, null, null, null, objectType2, null, false, false).ApplyDeprecationStatus("The property was removed in TMDL Preview-3."));
						break;
					}
					case ObjectType.PerspectiveMeasure:
					{
						TmdlObjectInfo tmdlObjectInfo7 = this.rootObjectInfo;
						string text5 = "Measure".ToJsonCase();
						TmdlValueType tmdlValueType4 = TmdlValueType.ModelReference;
						ObjectType? objectType2 = new ObjectType?(ObjectType.Measure);
						tmdlObjectInfo7.AddProperty(new TmdlPropertyInfo(text5, tmdlValueType4, null, null, null, objectType2, null, false, false).ApplyDeprecationStatus("The property was removed in TMDL Preview-3."));
						break;
					}
					case ObjectType.Role:
					case ObjectType.RoleMembership:
						break;
					case ObjectType.TablePermission:
					{
						TmdlObjectInfo tmdlObjectInfo8 = this.rootObjectInfo;
						string text6 = "Table".ToJsonCase();
						TmdlValueType tmdlValueType5 = TmdlValueType.ModelReference;
						ObjectType? objectType2 = new ObjectType?(ObjectType.Table);
						tmdlObjectInfo8.AddProperty(new TmdlPropertyInfo(text6, tmdlValueType5, null, null, null, objectType2, null, false, false).ApplyDeprecationStatus("The property was removed in TMDL Preview-7."));
						break;
					}
					default:
						if (objectType == ObjectType.ColumnPermission)
						{
							TmdlObjectInfo tmdlObjectInfo9 = this.rootObjectInfo;
							string text7 = "Column".ToJsonCase();
							TmdlValueType tmdlValueType6 = TmdlValueType.ModelReference;
							ObjectType? objectType2 = new ObjectType?(ObjectType.Column);
							tmdlObjectInfo9.AddProperty(new TmdlPropertyInfo(text7, tmdlValueType6, null, null, null, objectType2, null, false, false).ApplyDeprecationStatus("The property was removed in TMDL Preview-7."));
						}
						break;
					}
				}
				bool flag;
				bool flag2;
				if (TmdlObjectInfoWriter.IsObjectWithDeprecatedStateProperties(this.rootObjectInfo.ObjectType, out flag, out flag2))
				{
					if (this.rootObjectInfo.HasVariants)
					{
						using (IEnumerator<TmdlObjectInfo> enumerator2 = this.rootObjectInfo.Variants.Values.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								TmdlObjectInfo tmdlObjectInfo10 = enumerator2.Current;
								if (flag)
								{
									tmdlObjectInfo10.AddProperty(TmdlObjectInfoWriter.deprecatedStateProperty);
								}
								if (flag2)
								{
									tmdlObjectInfo10.AddProperty(TmdlObjectInfoWriter.deprecatedErrorMessageProperty);
								}
							}
							goto IL_0A1F;
						}
					}
					if (flag)
					{
						this.rootObjectInfo.AddProperty(TmdlObjectInfoWriter.deprecatedStateProperty);
					}
					if (flag2)
					{
						this.rootObjectInfo.AddProperty(TmdlObjectInfoWriter.deprecatedErrorMessageProperty);
					}
				}
				IL_0A1F:
				if (makeReadOnly)
				{
					this.rootObjectInfo.MakeReadOnly();
				}
				tmdlObjectInfo11 = this.rootObjectInfo;
			}
			finally
			{
				this.rootObjectInfo = null;
				this.state = TmdlObjectInfoWriter.InfoWriteState.Completed;
			}
			return tmdlObjectInfo11;
		}

		// Token: 0x0600169F RID: 5791 RVA: 0x000977F0 File Offset: 0x000959F0
		private static void AddModelDataAccessOptionsProperties(TmdlObjectInfo customJsonProperty)
		{
			customJsonProperty.AddProperty(new TmdlPropertyInfo("fastCombine", TmdlValueType.Scalar, new TmdlScalarValueType?(TmdlScalarValueType.Bool), null, null, null, null, false, false));
			customJsonProperty.AddProperty(new TmdlPropertyInfo("legacyRedirects", TmdlValueType.Scalar, new TmdlScalarValueType?(TmdlScalarValueType.Bool), null, null, null, null, false, false));
			customJsonProperty.AddProperty(new TmdlPropertyInfo("returnErrorValuesAsNull", TmdlValueType.Scalar, new TmdlScalarValueType?(TmdlScalarValueType.Bool), null, null, null, null, false, false));
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x00097884 File Offset: 0x00095A84
		private static void AddModelAutomaticAggregationOptionsProperties(TmdlObjectInfo customJsonProperty)
		{
			customJsonProperty.AddProperty(new TmdlPropertyInfo("queryCoverage", TmdlValueType.Scalar, new TmdlScalarValueType?(TmdlScalarValueType.Double), null, null, null, null, false, false));
			customJsonProperty.AddProperty(new TmdlPropertyInfo("detailTableMinRows", TmdlValueType.Scalar, new TmdlScalarValueType?(TmdlScalarValueType.Long), null, null, null, null, false, false));
			customJsonProperty.AddProperty(new TmdlPropertyInfo("aggregationTableMaxRows", TmdlValueType.Scalar, new TmdlScalarValueType?(TmdlScalarValueType.Long), null, null, null, null, false, false));
			customJsonProperty.AddProperty(new TmdlPropertyInfo("aggregationTableSizeLimit", TmdlValueType.Scalar, new TmdlScalarValueType?(TmdlScalarValueType.Long), null, null, null, null, false, false));
		}

		// Token: 0x060016A1 RID: 5793 RVA: 0x00097948 File Offset: 0x00095B48
		private static void AddStructuredDataSourceConnectionDetailsProperties(TmdlObjectInfo customJsonProperty)
		{
			customJsonProperty.AddProperty(new TmdlPropertyInfo("protocol", TmdlValueType.String, null, null, null, null, null, false, false));
			TmdlPropertyInfo tmdlPropertyInfo = new TmdlPropertyInfo("address", TmdlValueType.Struct, null, null, null, null, null, false, false);
			tmdlPropertyInfo.AddChildProperty(new TmdlPropertyInfo("server", TmdlValueType.String, null, null, null, null, null, false, false));
			tmdlPropertyInfo.AddChildProperty(new TmdlPropertyInfo("database", TmdlValueType.String, null, null, null, null, null, false, false));
			tmdlPropertyInfo.AddChildProperty(new TmdlPropertyInfo("model", TmdlValueType.String, null, null, null, null, null, false, false));
			tmdlPropertyInfo.AddChildProperty(new TmdlPropertyInfo("schema", TmdlValueType.String, null, null, null, null, null, false, false));
			tmdlPropertyInfo.AddChildProperty(new TmdlPropertyInfo("object", TmdlValueType.String, null, null, null, null, null, false, false));
			tmdlPropertyInfo.AddChildProperty(new TmdlPropertyInfo("url", TmdlValueType.String, null, null, null, null, null, false, false));
			tmdlPropertyInfo.AddChildProperty(new TmdlPropertyInfo("contentType", TmdlValueType.String, null, null, null, null, null, false, false));
			tmdlPropertyInfo.AddChildProperty(new TmdlPropertyInfo("resource", TmdlValueType.String, null, null, null, null, null, false, false));
			tmdlPropertyInfo.AddChildProperty(new TmdlPropertyInfo("path", TmdlValueType.String, null, null, null, null, null, false, false));
			tmdlPropertyInfo.AddChildProperty(new TmdlPropertyInfo("domain", TmdlValueType.String, null, null, null, null, null, false, false));
			tmdlPropertyInfo.AddChildProperty(new TmdlPropertyInfo("account", TmdlValueType.String, null, null, null, null, null, false, false));
			tmdlPropertyInfo.AddChildProperty(new TmdlPropertyInfo("emailAddress", TmdlValueType.String, null, null, null, null, null, false, false));
			tmdlPropertyInfo.AddChildProperty(new TmdlPropertyInfo("connectionstring", TmdlValueType.String, null, null, null, null, null, false, false));
			tmdlPropertyInfo.AddChildProperty(new TmdlPropertyInfo("property", TmdlValueType.String, null, null, null, null, null, false, false));
			tmdlPropertyInfo.AddChildProperty(new TmdlPropertyInfo("view", TmdlValueType.String, null, null, null, null, null, false, false));
			customJsonProperty.AddProperty(tmdlPropertyInfo);
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x00097C87 File Offset: 0x00095E87
		private static void AddStructuredDataSourceOptionsProperties(TmdlObjectInfo customJsonProperty)
		{
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x00097C8C File Offset: 0x00095E8C
		private static void AddStructuredDataSourceCredentialProperties(TmdlObjectInfo customJsonProperty)
		{
			customJsonProperty.AddProperty(new TmdlPropertyInfo("AuthenticationKind", TmdlValueType.String, null, null, null, null, null, false, false));
			customJsonProperty.AddProperty(new TmdlPropertyInfo("PrivacySetting", TmdlValueType.String, null, null, null, null, null, false, false));
			customJsonProperty.AddProperty(new TmdlPropertyInfo("Username", TmdlValueType.String, null, null, null, null, null, false, false));
			customJsonProperty.AddProperty(new TmdlPropertyInfo("Password", TmdlValueType.String, null, null, null, null, null, false, false));
			customJsonProperty.AddProperty(new TmdlPropertyInfo("EncryptConnection", TmdlValueType.Scalar, new TmdlScalarValueType?(TmdlScalarValueType.Bool), null, null, null, null, false, false));
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x00097D88 File Offset: 0x00095F88
		private static bool IsObjectWithDeprecatedStateProperties(ObjectType objectType, out bool hasState, out bool hasErrorMessage)
		{
			if (objectType <= ObjectType.DetailRowsDefinition)
			{
				switch (objectType)
				{
				case ObjectType.Column:
				case ObjectType.Partition:
				case ObjectType.Relationship:
				case ObjectType.Hierarchy:
					hasState = true;
					hasErrorMessage = false;
					goto IL_005A;
				case ObjectType.AttributeHierarchy:
					goto IL_0054;
				case ObjectType.Measure:
					break;
				default:
					if (objectType != ObjectType.TablePermission && objectType != ObjectType.DetailRowsDefinition)
					{
						goto IL_0054;
					}
					break;
				}
			}
			else if (objectType != ObjectType.CalculationItem && objectType != ObjectType.FormatStringDefinition && objectType - ObjectType.DataCoverageDefinition > 1)
			{
				goto IL_0054;
			}
			hasState = true;
			hasErrorMessage = true;
			goto IL_005A;
			IL_0054:
			hasState = false;
			hasErrorMessage = false;
			IL_005A:
			return hasState | hasErrorMessage;
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x00097DF4 File Offset: 0x00095FF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static TmdlObjectInfo CreateObjectInfo(ObjectType type, bool isSingleChild, string description)
		{
			TmdlObjectInfo tmdlObjectInfo = new TmdlObjectInfo(type);
			if (isSingleChild)
			{
				tmdlObjectInfo.IsSingleton = true;
			}
			if (!string.IsNullOrEmpty(description))
			{
				tmdlObjectInfo.Description = description;
			}
			return tmdlObjectInfo;
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x00097E24 File Offset: 0x00096024
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static TmdlExpressionLanguage? IdentifyExpressionLanguage(ObjectType type, string propertyName, MetadataPropertyNature propertyNature)
		{
			if (type <= ObjectType.LinguisticMetadata)
			{
				switch (type)
				{
				case ObjectType.Column:
					if (propertyName == "expression")
					{
						return new TmdlExpressionLanguage?(TmdlExpressionLanguage.Dax);
					}
					break;
				case ObjectType.AttributeHierarchy:
				case ObjectType.Relationship:
					break;
				case ObjectType.Partition:
					if (propertyName == "query")
					{
						return new TmdlExpressionLanguage?(TmdlExpressionLanguage.NativeQuery);
					}
					if (propertyName == "expression")
					{
						return new TmdlExpressionLanguage?(TmdlExpressionLanguage.Other);
					}
					break;
				case ObjectType.Measure:
					if (propertyName == "expression")
					{
						return new TmdlExpressionLanguage?(TmdlExpressionLanguage.Dax);
					}
					break;
				default:
					if (type != ObjectType.KPI)
					{
						if (type == ObjectType.LinguisticMetadata)
						{
							if (propertyName == "content")
							{
								return new TmdlExpressionLanguage?(TmdlExpressionLanguage.XmlOrJson);
							}
						}
					}
					else if (propertyName == "statusExpression" || propertyName == "targetExpression" || propertyName == "trendExpression")
					{
						return new TmdlExpressionLanguage?(TmdlExpressionLanguage.Dax);
					}
					break;
				}
			}
			else if (type <= ObjectType.AnalyticsAIMetadata)
			{
				if (type != ObjectType.TablePermission)
				{
					switch (type)
					{
					case ObjectType.ExtendedProperty:
						if (propertyName == "value" && (propertyNature & MetadataPropertyNature.JsonString) == MetadataPropertyNature.JsonString)
						{
							return new TmdlExpressionLanguage?(TmdlExpressionLanguage.Json);
						}
						break;
					case ObjectType.Expression:
						if (propertyName == "expression")
						{
							return new TmdlExpressionLanguage?(TmdlExpressionLanguage.Dax);
						}
						break;
					case ObjectType.DetailRowsDefinition:
						if (propertyName == "expression")
						{
							return new TmdlExpressionLanguage?(TmdlExpressionLanguage.Dax);
						}
						break;
					case ObjectType.CalculationItem:
						if (propertyName == "expression")
						{
							return new TmdlExpressionLanguage?(TmdlExpressionLanguage.Dax);
						}
						break;
					case ObjectType.RefreshPolicy:
						if (propertyName == "sourceExpression" || propertyName == "pollingExpression")
						{
							return new TmdlExpressionLanguage?(TmdlExpressionLanguage.M);
						}
						break;
					case ObjectType.FormatStringDefinition:
						if (propertyName == "expression")
						{
							return new TmdlExpressionLanguage?(TmdlExpressionLanguage.Dax);
						}
						break;
					case ObjectType.AnalyticsAIMetadata:
						if (propertyName == "measureAnalysisDefinition")
						{
							return new TmdlExpressionLanguage?(TmdlExpressionLanguage.Json);
						}
						break;
					}
				}
				else if (propertyName == "filterExpression")
				{
					return new TmdlExpressionLanguage?(TmdlExpressionLanguage.Dax);
				}
			}
			else if (type != ObjectType.DataCoverageDefinition)
			{
				if (type == ObjectType.CalculationExpression)
				{
					if (propertyName == "expression")
					{
						return new TmdlExpressionLanguage?(TmdlExpressionLanguage.Dax);
					}
				}
			}
			else if (propertyName == "expression")
			{
				return new TmdlExpressionLanguage?(TmdlExpressionLanguage.Dax);
			}
			return null;
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x0009806C File Offset: 0x0009626C
		private static TmdlTranslationElementInfo ConvertObjectInfoToTranslationElementInfo(TmdlObjectInfo objectInfo)
		{
			TmdlTranslationElementInfo tmdlTranslationElementInfo = new TmdlTranslationElementInfo(objectInfo.ObjectType);
			if (objectInfo.NameProperty != null)
			{
				tmdlTranslationElementInfo.NameProperty = objectInfo.NameProperty;
			}
			if (objectInfo.IsSingleton)
			{
				tmdlTranslationElementInfo.IsSingleton = true;
			}
			if (objectInfo.HasAnyProperty(false, false))
			{
				foreach (TmdlPropertyInfo tmdlPropertyInfo in objectInfo.Properties.Where((TmdlPropertyInfo p) => p.Type != TmdlValueType.MetadataObject))
				{
					tmdlTranslationElementInfo.AddProperty(tmdlPropertyInfo);
				}
			}
			if (objectInfo.HasAnyChild(true))
			{
				foreach (TmdlObjectInfo tmdlObjectInfo in objectInfo.ChildObjects)
				{
					tmdlTranslationElementInfo.AddChildElement(TmdlObjectInfoWriter.ConvertObjectInfoToTranslationElementInfo(tmdlObjectInfo));
				}
			}
			return tmdlTranslationElementInfo;
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x00098164 File Offset: 0x00096364
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private ObjectType GetCurrentObjectType()
		{
			switch (this.state)
			{
			case TmdlObjectInfoWriter.InfoWriteState.Start:
			case TmdlObjectInfoWriter.InfoWriteState.RootObjectChoice:
			case TmdlObjectInfoWriter.InfoWriteState.RootObject:
			case TmdlObjectInfoWriter.InfoWriteState.CrossLink:
			case TmdlObjectInfoWriter.InfoWriteState.End:
				return this.rootObjectInfo.ObjectType;
			case TmdlObjectInfoWriter.InfoWriteState.InnerScope:
			case TmdlObjectInfoWriter.InfoWriteState.SupplementaryScope:
			{
				if (this.scopes[this.scopes.Count - 1].ObjectType != ObjectType.Null)
				{
					return this.scopes[this.scopes.Count - 1].ObjectType;
				}
				for (int i = this.scopes.Count - 2; i >= 0; i--)
				{
					if (this.scopes[i].IsObjectInfo)
					{
						return this.scopes[i].GetObjectInfo().ObjectType;
					}
				}
				return this.rootObjectInfo.ObjectType;
			}
			default:
				throw TomInternalException.Create("Invalid state for an object-type query - state={0}", new object[] { this.state });
			}
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x00098260 File Offset: 0x00096460
		private void AddPropertyToCurrentScope(TmdlObjectInfoWriter.PropertyKind kind, TmdlPropertyInfo property)
		{
			TmdlObjectInfo tmdlObjectInfo;
			ICollection<TmdlPropertyInfo> collection;
			switch (this.state)
			{
			case TmdlObjectInfoWriter.InfoWriteState.RootObject:
				tmdlObjectInfo = (string.IsNullOrEmpty(this.choiceOption) ? this.rootObjectInfo : this.rootObjectInfo.Variants[this.choiceOption]);
				collection = null;
				goto IL_0240;
			case TmdlObjectInfoWriter.InfoWriteState.InnerScope:
				if (this.scopes[this.scopes.Count - 1].IsObjectInfo)
				{
					tmdlObjectInfo = this.scopes[this.scopes.Count - 1].GetObjectInfo();
					collection = null;
					goto IL_0240;
				}
				collection = this.scopes[this.scopes.Count - 1].GetPropertyInfos();
				tmdlObjectInfo = null;
				goto IL_0240;
			case TmdlObjectInfoWriter.InfoWriteState.SupplementaryScope:
			{
				int scopeType = this.scopes[0].ScopeType;
				if (scopeType == 1)
				{
					if (property.IsDefaultProperty)
					{
						if (this.rootObjectInfo.DefaultProperty == null)
						{
							this.rootObjectInfo.DefaultProperty = property;
							return;
						}
					}
					else if (!this.rootObjectInfo.Properties.Any((TmdlPropertyInfo p) => string.Compare(p.Name, property.Name, StringComparison.Ordinal) == 0))
					{
						this.rootObjectInfo.AddProperty(property);
						return;
					}
					return;
				}
				if (scopeType != 2)
				{
					throw TomInternalException.Create("The write state of the TMDL scheme writer is {0}\\{1} and it cannot be used to write properties at that state!", new object[]
					{
						this.state,
						this.scopes[0].ScopeType
					});
				}
				TmdlPropertyInfo tmdlPropertyInfo = this.rootObjectInfo.Properties.FirstOrDefault((TmdlPropertyInfo p) => string.Compare(p.Name, this.scopes[0].Name, StringComparison.Ordinal) == 0);
				if (tmdlPropertyInfo == null)
				{
					tmdlPropertyInfo = new TmdlPropertyInfo(this.scopes[0].Name, TmdlValueType.Struct, null, null, null, null, null, false, true);
					this.rootObjectInfo.AddProperty(tmdlPropertyInfo);
				}
				tmdlPropertyInfo.AddChildProperty(property);
				return;
			}
			}
			throw TomInternalException.Create("The write state of the TMDL scheme writer is {0} and it cannot be used to write properties at that state!", new object[] { this.state });
			IL_0240:
			Utils.Verify((tmdlObjectInfo != null) ^ (collection != null));
			switch (kind)
			{
			case TmdlObjectInfoWriter.PropertyKind.Regular:
				if (tmdlObjectInfo != null)
				{
					tmdlObjectInfo.AddProperty(property);
					return;
				}
				collection.Add(property);
				return;
			case TmdlObjectInfoWriter.PropertyKind.Default:
			{
				if (tmdlObjectInfo != null)
				{
					tmdlObjectInfo.DefaultProperty = property;
					return;
				}
				Utils.Verify(this.GetCurrentObjectType() == ObjectType.Partition);
				Utils.Verify(this.state == TmdlObjectInfoWriter.InfoWriteState.InnerScope && this.scopes != null && this.scopes.Count > 0 && string.Compare(this.scopes[this.scopes.Count - 1].Name, "source", StringComparison.InvariantCultureIgnoreCase) == 0);
				string name = property.Name;
				if (name == "sourceType")
				{
					this.rootObjectInfo.DefaultProperty = property;
					return;
				}
				if (!(name == "expression"))
				{
					throw TomInternalException.Create("Invalid default property for PartitionSource: '{0}' is not supported as a default proterty", new object[] { property.Name });
				}
				collection.Add(property);
				return;
			}
			case TmdlObjectInfoWriter.PropertyKind.Name:
				tmdlObjectInfo.NameProperty = property;
				return;
			case TmdlObjectInfoWriter.PropertyKind.Description:
				tmdlObjectInfo.DescriptionProperty = property;
				return;
			default:
				throw TomInternalException.Create("Invalid PropertyKind {0}", new object[] { kind });
			}
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x00098608 File Offset: 0x00096808
		private void PushScope(TmdlObjectInfoWriter.InnerScope scope)
		{
			if (this.scopes == null)
			{
				this.scopes = new List<TmdlObjectInfoWriter.InnerScope>();
			}
			if (this.scopes.Count == 0 || this.scopes[this.scopes.Count - 1].ScopeType != -1 || string.Compare(this.scopes[this.scopes.Count - 1].Name, scope.Name, StringComparison.Ordinal) != 0)
			{
				this.scopes.Add(scope);
			}
			else
			{
				this.scopes[this.scopes.Count - 1] = scope;
			}
			this.state = TmdlObjectInfoWriter.InfoWriteState.InnerScope;
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x000986B8 File Offset: 0x000968B8
		private void PopScope(out TmdlObjectInfoWriter.InnerScope scope)
		{
			scope = this.scopes[this.scopes.Count - 1];
			this.scopes.RemoveAt(this.scopes.Count - 1);
			if (this.scopes.Count == 0)
			{
				this.state = TmdlObjectInfoWriter.InfoWriteState.RootObject;
				return;
			}
			if (!this.scopes[this.scopes.Count - 1].IsSupplementaryScope)
			{
				this.state = TmdlObjectInfoWriter.InfoWriteState.InnerScope;
				return;
			}
			this.state = TmdlObjectInfoWriter.InfoWriteState.SupplementaryScope;
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x00098744 File Offset: 0x00096944
		private void AddCompletedScopeToCurrentScope(TmdlObjectInfoWriter.InnerScope scope)
		{
			string text = null;
			TmdlObjectInfo tmdlObjectInfo;
			switch (this.state)
			{
			case TmdlObjectInfoWriter.InfoWriteState.RootObject:
			{
				tmdlObjectInfo = this.rootObjectInfo;
				ICollection<TmdlPropertyInfo> collection = null;
				string text2 = this.choiceOption;
				goto IL_0211;
			}
			case TmdlObjectInfoWriter.InfoWriteState.InnerScope:
			{
				if (this.scopes[this.scopes.Count - 1].IsObjectInfo)
				{
					tmdlObjectInfo = this.scopes[this.scopes.Count - 1].GetObjectInfo();
					ICollection<TmdlPropertyInfo> collection = null;
				}
				else
				{
					ICollection<TmdlPropertyInfo> collection = this.scopes[this.scopes.Count - 1].GetPropertyInfos();
					tmdlObjectInfo = null;
				}
				string text2 = ((this.scopes.Count > 1) ? this.scopes[this.scopes.Count - 2].ChoiceOption : this.choiceOption);
				goto IL_0211;
			}
			case TmdlObjectInfoWriter.InfoWriteState.SupplementaryScope:
			{
				ICollection<TmdlPropertyInfo> collection;
				string text2;
				if (this.scopes.Count > 1)
				{
					if (this.scopes[this.scopes.Count - 2].IsObjectInfo)
					{
						tmdlObjectInfo = this.scopes[this.scopes.Count - 2].GetObjectInfo();
						collection = null;
					}
					else
					{
						collection = this.scopes[this.scopes.Count - 2].GetPropertyInfos();
						tmdlObjectInfo = null;
					}
					text2 = this.scopes[this.scopes.Count - 1].ChoiceOption;
					text = this.scopes[this.scopes.Count - 1].Description;
					goto IL_0211;
				}
				tmdlObjectInfo = this.rootObjectInfo;
				collection = null;
				text2 = this.scopes[0].ChoiceOption;
				text = this.scopes[0].Description;
				goto IL_0211;
			}
			}
			throw TomInternalException.Create("The write state of the TMDL schema writer is {0} and it cannot be used to add children at that state!", new object[] { this.state });
			IL_0211:
			if (scope.IsCrossLink)
			{
				IEnumerable<TmdlPropertyInfo> propertyInfos = scope.GetPropertyInfos();
				ObjectPath objectPath = new ObjectPath(Array.Empty<KeyValuePair<ObjectType, string>>());
				foreach (TmdlPropertyInfo tmdlPropertyInfo in propertyInfos)
				{
					objectPath.Push(JsonPropertyName.ObjectPath.GetObjectTypeFromPropertyName(tmdlPropertyInfo.Name), tmdlPropertyInfo.Name);
				}
				if (objectPath.Count > 1)
				{
					objectPath.Normalize();
				}
				if (tmdlObjectInfo != null)
				{
					string name = scope.Name;
					TmdlValueType tmdlValueType = TmdlValueType.ModelReference;
					ObjectType? objectType = new ObjectType?(objectPath[objectPath.Count - 1].Key);
					TmdlPropertyInfo tmdlPropertyInfo2 = new TmdlPropertyInfo(name, tmdlValueType, null, null, null, objectType, null, false, false);
					string text2;
					if (string.IsNullOrEmpty(text2))
					{
						tmdlObjectInfo.AddProperty(tmdlPropertyInfo2);
						return;
					}
					tmdlObjectInfo.Variants[text2].AddProperty(tmdlPropertyInfo2);
					return;
				}
				else
				{
					ICollection<TmdlPropertyInfo> collection;
					string text2;
					ObjectType? objectType;
					if (string.IsNullOrEmpty(text2))
					{
						ICollection<TmdlPropertyInfo> collection2 = collection;
						string name2 = scope.Name;
						TmdlValueType tmdlValueType2 = TmdlValueType.ModelReference;
						objectType = new ObjectType?(objectPath[objectPath.Count - 1].Key);
						collection2.Add(new TmdlPropertyInfo(name2, tmdlValueType2, null, null, null, objectType, null, false, false));
						return;
					}
					TmdlPropertyInfo tmdlPropertyInfo3 = collection.FirstOrDefault((TmdlPropertyInfo p) => string.Compare(p.Name, scope.Name, StringComparison.Ordinal) == 0);
					if (tmdlPropertyInfo3 == null)
					{
						string name3 = scope.Name;
						TmdlValueType tmdlValueType3 = TmdlValueType.Struct;
						TmdlScalarValueType? tmdlScalarValueType = null;
						TmdlExpressionLanguage? tmdlExpressionLanguage = null;
						Type type = null;
						objectType = null;
						tmdlPropertyInfo3 = new TmdlPropertyInfo(name3, tmdlValueType3, tmdlScalarValueType, tmdlExpressionLanguage, type, objectType, null, false, false);
						collection.Add(tmdlPropertyInfo3);
					}
					TmdlPropertyInfo tmdlPropertyInfo4 = tmdlPropertyInfo3;
					string text3 = text2;
					TmdlValueType tmdlValueType4 = TmdlValueType.ModelReference;
					objectType = new ObjectType?(objectPath[objectPath.Count - 1].Key);
					tmdlPropertyInfo4.AddChildProperty(new TmdlPropertyInfo(text3, tmdlValueType4, null, null, null, objectType, null, false, false));
					return;
				}
			}
			else if (string.IsNullOrEmpty(scope.Name) && tmdlObjectInfo != null)
			{
				string text2;
				if (string.IsNullOrEmpty(text2))
				{
					tmdlObjectInfo.AddChildObject(scope.GetObjectInfo());
					return;
				}
				tmdlObjectInfo.AddVariant(text2, scope.GetObjectInfo());
				return;
			}
			else
			{
				ICollection<TmdlPropertyInfo> collection;
				if (scope.IsObjectInfo && this.rootObjectInfo.ObjectType == ObjectType.Culture && this.state == TmdlObjectInfoWriter.InfoWriteState.InnerScope && this.scopes.Count == 1 && string.Compare(this.scopes[0].Name, "translations", StringComparison.Ordinal) == 0 && string.Compare(scope.Name, "model", StringComparison.Ordinal) == 0)
				{
					TmdlTranslationElementInfo tmdlTranslationElementInfo = TmdlObjectInfoWriter.ConvertObjectInfoToTranslationElementInfo(scope.GetObjectInfo());
					ICollection<TmdlPropertyInfo> collection3 = collection;
					string name4 = scope.Name;
					TmdlValueType tmdlValueType5 = TmdlValueType.TranslationRoot;
					TmdlTranslationElementInfo tmdlTranslationElementInfo2 = tmdlTranslationElementInfo;
					TmdlScalarValueType? tmdlScalarValueType2 = null;
					TmdlExpressionLanguage? tmdlExpressionLanguage2 = null;
					Type type2 = null;
					ObjectType? objectType = null;
					collection3.Add(new TmdlPropertyInfo(name4, tmdlValueType5, tmdlScalarValueType2, tmdlExpressionLanguage2, type2, objectType, tmdlTranslationElementInfo2, false, false));
					return;
				}
				string text2;
				if (scope.IsProperty && this.rootObjectInfo.ObjectType == ObjectType.Partition && string.Compare(scope.Name, "source", StringComparison.Ordinal) == 0)
				{
					TmdlObjectInfo tmdlObjectInfo2 = new TmdlObjectInfo("source");
					using (IEnumerator<TmdlPropertyInfo> enumerator = scope.GetPropertyInfos().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							TmdlPropertyInfo property = enumerator.Current;
							if (property.IsDefaultProperty)
							{
								tmdlObjectInfo2.DefaultProperty = property;
							}
							else
							{
								tmdlObjectInfo2.AddProperty(property);
							}
							if (!this.rootObjectInfo.Properties.Any((TmdlPropertyInfo p) => string.Compare(p.Name, property.Name, StringComparison.InvariantCultureIgnoreCase) == 0))
							{
								TmdlScalarValueType? tmdlScalarValueType3 = null;
								TmdlExpressionLanguage? tmdlExpressionLanguage3 = null;
								Type type3 = null;
								TmdlValueType type4 = property.Type;
								if (type4 != TmdlValueType.String)
								{
									if (type4 == TmdlValueType.Scalar)
									{
										if (property.ScalarValueType.Value == TmdlScalarValueType.Enum)
										{
											tmdlScalarValueType3 = new TmdlScalarValueType?(TmdlScalarValueType.Enum);
											type3 = property.EnumType;
										}
										else
										{
											tmdlScalarValueType3 = new TmdlScalarValueType?(property.ScalarValueType.Value);
										}
									}
								}
								else
								{
									tmdlExpressionLanguage3 = ((string.Compare(property.Name, "expression", StringComparison.InvariantCultureIgnoreCase) == 0) ? new TmdlExpressionLanguage?(TmdlExpressionLanguage.Other) : property.ExpressionLanguage);
								}
								string name5 = property.Name;
								TmdlValueType type5 = property.Type;
								TmdlScalarValueType? tmdlScalarValueType4 = tmdlScalarValueType3;
								TmdlExpressionLanguage? tmdlExpressionLanguage4 = tmdlExpressionLanguage3;
								Type type6 = type3;
								ObjectType? objectType = null;
								TmdlPropertyInfo tmdlPropertyInfo5 = new TmdlPropertyInfo(name5, type5, tmdlScalarValueType4, tmdlExpressionLanguage4, type6, objectType, null, false, false).ApplyDeprecationStatus("The properties of the Partition.Source on the partition level had been deprecated in TMDL RC.");
								this.rootObjectInfo.AddProperty(tmdlPropertyInfo5);
							}
						}
					}
					TmdlObjectInfo tmdlObjectInfo3;
					if (!this.rootObjectInfo.TryGetObjectInfo("source", out tmdlObjectInfo3))
					{
						tmdlObjectInfo3 = new TmdlObjectInfo("source");
						this.rootObjectInfo.AddChildObject(tmdlObjectInfo3);
					}
					tmdlObjectInfo2.Description = text;
					tmdlObjectInfo3.AddVariant(text2, tmdlObjectInfo2);
					return;
				}
				if (scope.IsObjectInfo)
				{
					TmdlObjectInfo objectInfo = scope.GetObjectInfo();
					if (tmdlObjectInfo != null)
					{
						ObjectType? objectType;
						if (string.IsNullOrEmpty(text2))
						{
							TmdlObjectInfo tmdlObjectInfo4 = tmdlObjectInfo;
							string name6 = scope.Name;
							TmdlValueType tmdlValueType6 = TmdlValueType.MetadataObject;
							objectType = new ObjectType?(objectInfo.ObjectType);
							tmdlObjectInfo4.AddProperty(new TmdlPropertyInfo(name6, tmdlValueType6, null, null, null, objectType, null, false, false).WithProperties(objectInfo.Properties));
							tmdlObjectInfo.AddChildObject(objectInfo);
							return;
						}
						TmdlPropertyInfo tmdlPropertyInfo6 = tmdlObjectInfo.Properties.FirstOrDefault((TmdlPropertyInfo p) => string.Compare(p.Name, scope.Name, StringComparison.Ordinal) == 0);
						if (tmdlPropertyInfo6 == null)
						{
							string name7 = scope.Name;
							TmdlValueType tmdlValueType7 = TmdlValueType.Struct;
							TmdlScalarValueType? tmdlScalarValueType5 = null;
							TmdlExpressionLanguage? tmdlExpressionLanguage5 = null;
							Type type7 = null;
							objectType = null;
							tmdlPropertyInfo6 = new TmdlPropertyInfo(name7, tmdlValueType7, tmdlScalarValueType5, tmdlExpressionLanguage5, type7, objectType, null, false, false);
							tmdlObjectInfo.AddProperty(tmdlPropertyInfo6);
						}
						TmdlPropertyInfo tmdlPropertyInfo7 = tmdlPropertyInfo6;
						string text4 = text2;
						TmdlValueType tmdlValueType8 = TmdlValueType.MetadataObject;
						objectType = new ObjectType?(objectInfo.ObjectType);
						tmdlPropertyInfo7.AddChildProperty(new TmdlPropertyInfo(text4, tmdlValueType8, null, null, null, objectType, null, false, false).WithProperties(objectInfo.Properties));
						return;
					}
					else
					{
						ObjectType? objectType;
						if (string.IsNullOrEmpty(text2))
						{
							ICollection<TmdlPropertyInfo> collection4 = collection;
							string name8 = scope.Name;
							TmdlValueType tmdlValueType9 = TmdlValueType.MetadataObject;
							objectType = new ObjectType?(objectInfo.ObjectType);
							collection4.Add(new TmdlPropertyInfo(name8, tmdlValueType9, null, null, null, objectType, null, false, false).WithProperties(objectInfo.Properties));
							return;
						}
						TmdlPropertyInfo tmdlPropertyInfo8 = collection.FirstOrDefault((TmdlPropertyInfo p) => string.Compare(p.Name, scope.Name, StringComparison.Ordinal) == 0);
						if (tmdlPropertyInfo8 == null)
						{
							string name9 = scope.Name;
							TmdlValueType tmdlValueType10 = TmdlValueType.Struct;
							TmdlScalarValueType? tmdlScalarValueType6 = null;
							TmdlExpressionLanguage? tmdlExpressionLanguage6 = null;
							Type type8 = null;
							objectType = null;
							tmdlPropertyInfo8 = new TmdlPropertyInfo(name9, tmdlValueType10, tmdlScalarValueType6, tmdlExpressionLanguage6, type8, objectType, null, false, false);
							collection.Add(tmdlPropertyInfo8);
						}
						TmdlPropertyInfo tmdlPropertyInfo9 = tmdlPropertyInfo8;
						string text5 = text2;
						TmdlValueType tmdlValueType11 = TmdlValueType.MetadataObject;
						objectType = new ObjectType?(objectInfo.ObjectType);
						tmdlPropertyInfo9.AddChildProperty(new TmdlPropertyInfo(text5, tmdlValueType11, null, null, null, objectType, null, false, false).WithProperties(objectInfo.Properties));
						return;
					}
				}
				else
				{
					ICollection<TmdlPropertyInfo> propertyInfos2 = scope.GetPropertyInfos();
					if (tmdlObjectInfo != null)
					{
						ObjectType? objectType;
						if (!string.IsNullOrEmpty(text2))
						{
							TmdlPropertyInfo tmdlPropertyInfo10 = tmdlObjectInfo.Properties.FirstOrDefault((TmdlPropertyInfo p) => string.Compare(p.Name, scope.Name, StringComparison.Ordinal) == 0);
							if (tmdlPropertyInfo10 == null)
							{
								string name10 = scope.Name;
								TmdlValueType tmdlValueType12 = TmdlValueType.Struct;
								TmdlScalarValueType? tmdlScalarValueType7 = null;
								TmdlExpressionLanguage? tmdlExpressionLanguage7 = null;
								Type type9 = null;
								objectType = null;
								tmdlPropertyInfo10 = new TmdlPropertyInfo(name10, tmdlValueType12, tmdlScalarValueType7, tmdlExpressionLanguage7, type9, objectType, null, false, false);
								tmdlObjectInfo.AddProperty(tmdlPropertyInfo10);
							}
							TmdlPropertyInfo tmdlPropertyInfo11 = tmdlPropertyInfo10;
							string text6 = text2;
							TmdlValueType tmdlValueType13 = TmdlValueType.Struct;
							TmdlScalarValueType? tmdlScalarValueType8 = null;
							TmdlExpressionLanguage? tmdlExpressionLanguage8 = null;
							Type type10 = null;
							objectType = null;
							tmdlPropertyInfo11.AddChildProperty(new TmdlPropertyInfo(text6, tmdlValueType13, tmdlScalarValueType8, tmdlExpressionLanguage8, type10, objectType, null, false, false).WithProperties(propertyInfos2));
							return;
						}
						if (tmdlObjectInfo.ObjectType == ObjectType.Culture && string.Compare(scope.Name, "translations", StringComparison.Ordinal) == 0)
						{
							Utils.Verify(propertyInfos2.Count == 1);
							TmdlPropertyInfo tmdlPropertyInfo12 = propertyInfos2.Single<TmdlPropertyInfo>();
							Utils.Verify(tmdlPropertyInfo12.Type == TmdlValueType.TranslationRoot && tmdlPropertyInfo12.RootElementInfo != null);
							TmdlObjectInfo tmdlObjectInfo5 = tmdlObjectInfo;
							string name11 = scope.Name;
							TmdlValueType tmdlValueType14 = TmdlValueType.TranslationRoot;
							TmdlTranslationElementInfo tmdlTranslationElementInfo2 = tmdlPropertyInfo12.RootElementInfo;
							TmdlScalarValueType? tmdlScalarValueType9 = null;
							TmdlExpressionLanguage? tmdlExpressionLanguage9 = null;
							Type type11 = null;
							objectType = null;
							tmdlObjectInfo5.AddProperty(new TmdlPropertyInfo(name11, tmdlValueType14, tmdlScalarValueType9, tmdlExpressionLanguage9, type11, objectType, tmdlTranslationElementInfo2, false, false));
							return;
						}
						TmdlObjectInfo tmdlObjectInfo6 = tmdlObjectInfo;
						string name12 = scope.Name;
						TmdlValueType tmdlValueType15 = TmdlValueType.Struct;
						TmdlScalarValueType? tmdlScalarValueType10 = null;
						TmdlExpressionLanguage? tmdlExpressionLanguage10 = null;
						Type type12 = null;
						objectType = null;
						tmdlObjectInfo6.AddProperty(new TmdlPropertyInfo(name12, tmdlValueType15, tmdlScalarValueType10, tmdlExpressionLanguage10, type12, objectType, null, false, false).WithProperties(propertyInfos2));
						return;
					}
					else
					{
						ObjectType? objectType;
						if (string.IsNullOrEmpty(text2))
						{
							ICollection<TmdlPropertyInfo> collection5 = collection;
							string name13 = scope.Name;
							TmdlValueType tmdlValueType16 = TmdlValueType.Struct;
							TmdlScalarValueType? tmdlScalarValueType11 = null;
							TmdlExpressionLanguage? tmdlExpressionLanguage11 = null;
							Type type13 = null;
							objectType = null;
							collection5.Add(new TmdlPropertyInfo(name13, tmdlValueType16, tmdlScalarValueType11, tmdlExpressionLanguage11, type13, objectType, null, false, false).WithProperties(propertyInfos2));
							return;
						}
						TmdlPropertyInfo tmdlPropertyInfo13 = collection.FirstOrDefault((TmdlPropertyInfo p) => string.Compare(p.Name, scope.Name, StringComparison.Ordinal) == 0);
						if (tmdlPropertyInfo13 == null)
						{
							string name14 = scope.Name;
							TmdlValueType tmdlValueType17 = TmdlValueType.Struct;
							TmdlScalarValueType? tmdlScalarValueType12 = null;
							TmdlExpressionLanguage? tmdlExpressionLanguage12 = null;
							Type type14 = null;
							objectType = null;
							tmdlPropertyInfo13 = new TmdlPropertyInfo(name14, tmdlValueType17, tmdlScalarValueType12, tmdlExpressionLanguage12, type14, objectType, null, false, false);
							collection.Add(tmdlPropertyInfo13);
						}
						TmdlPropertyInfo tmdlPropertyInfo14 = tmdlPropertyInfo13;
						string text7 = text2;
						TmdlValueType tmdlValueType18 = TmdlValueType.Struct;
						TmdlScalarValueType? tmdlScalarValueType13 = null;
						TmdlExpressionLanguage? tmdlExpressionLanguage13 = null;
						Type type15 = null;
						objectType = null;
						tmdlPropertyInfo14.AddChildProperty(new TmdlPropertyInfo(text7, tmdlValueType18, tmdlScalarValueType13, tmdlExpressionLanguage13, type15, objectType, null, false, false).WithProperties(propertyInfos2));
						return;
					}
				}
			}
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x00099304 File Offset: 0x00097504
		private void PushSupplementaryScope(string collectionOrPropertyName, ObjectType objectType, int scopeType)
		{
			if (this.scopes == null)
			{
				this.scopes = new List<TmdlObjectInfoWriter.InnerScope>();
			}
			this.scopes.Add(TmdlObjectInfoWriter.InnerScope.CreateSupplementaryScope(collectionOrPropertyName, objectType));
			this.state = TmdlObjectInfoWriter.InfoWriteState.SupplementaryScope;
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x00099334 File Offset: 0x00097534
		private void UpdateSupplementaryScope(int scopeType)
		{
			this.scopes[this.scopes.Count - 1] = this.scopes[this.scopes.Count - 1].ConvertSupplementaryScope(scopeType);
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x0009937C File Offset: 0x0009757C
		private void SetSupplementaryScopeChoiceOption(string choiceOption, string description)
		{
			this.scopes[this.scopes.Count - 1] = this.scopes[this.scopes.Count - 1].SetChoiceOption(choiceOption, description);
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x000993C4 File Offset: 0x000975C4
		private void ResetSupplementaryScopeChoiceOption()
		{
			this.scopes[this.scopes.Count - 1] = this.scopes[this.scopes.Count - 1].ResetChoiceOption();
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x0009940C File Offset: 0x0009760C
		private bool PopSupplementaryScope(bool isChoiseScopeCompletion, out string collectionOrPropertyName, out int scopeType)
		{
			collectionOrPropertyName = this.scopes[this.scopes.Count - 1].Name;
			scopeType = this.scopes[this.scopes.Count - 1].ScopeType;
			if (isChoiseScopeCompletion)
			{
				if (scopeType != 1)
				{
					return false;
				}
			}
			else if (scopeType != 2)
			{
				return false;
			}
			this.scopes.RemoveAt(this.scopes.Count - 1);
			if (this.scopes.Count == 0)
			{
				this.state = TmdlObjectInfoWriter.InfoWriteState.RootObject;
			}
			else
			{
				Utils.Verify(!this.scopes[this.scopes.Count - 1].IsSupplementaryScope, "CollectionOrChoice of complex properties is only allowed under the root object or a complex-property, not under another collection\\choice!");
				this.state = TmdlObjectInfoWriter.InfoWriteState.InnerScope;
			}
			return true;
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x000994D4 File Offset: 0x000976D4
		// Note: this type is marked as 'beforefieldinit'.
		static TmdlObjectInfoWriter()
		{
			string text = "state";
			TmdlValueType tmdlValueType = TmdlValueType.Scalar;
			TmdlScalarValueType? tmdlScalarValueType = new TmdlScalarValueType?(TmdlScalarValueType.Enum);
			Type typeFromHandle = typeof(ObjectState);
			TmdlObjectInfoWriter.deprecatedStateProperty = new TmdlPropertyInfo(text, tmdlValueType, tmdlScalarValueType, null, typeFromHandle, null, null, false, false).ApplyDeprecationStatus("The state property is inferred and was removed from TMDL serialization.").WithReadOnlyStatus<TmdlPropertyInfo>();
			TmdlObjectInfoWriter.deprecatedErrorMessageProperty = new TmdlPropertyInfo("errorMessage", TmdlValueType.String, null, null, null, null, null, false, false).ApplyDeprecationStatus("The error-message property is inferred and was removed from TMDL serialization.").WithReadOnlyStatus<TmdlPropertyInfo>();
		}

		// Token: 0x0400041F RID: 1055
		internal const string PartitionSourceTypePropertyName = "sourceType";

		// Token: 0x04000420 RID: 1056
		private const string DeprecatedStatePropertyName = "state";

		// Token: 0x04000421 RID: 1057
		private const string DeprecatedErrorMessagePropertyName = "errorMessage";

		// Token: 0x04000422 RID: 1058
		private const int ScopeType_Pending = -1;

		// Token: 0x04000423 RID: 1059
		private const int ScopeType_Choice = 1;

		// Token: 0x04000424 RID: 1060
		private const int ScopeType_Collection = 2;

		// Token: 0x04000425 RID: 1061
		private const int ScopeType_CollectionOfChoices = 3;

		// Token: 0x04000426 RID: 1062
		private const int ScopeType_Object = 4;

		// Token: 0x04000427 RID: 1063
		private const int ScopeType_Property = 8;

		// Token: 0x04000428 RID: 1064
		private const int ScopeType_CrossLink = 16;

		// Token: 0x04000429 RID: 1065
		private static readonly TmdlPropertyInfo deprecatedStateProperty;

		// Token: 0x0400042A RID: 1066
		private static readonly TmdlPropertyInfo deprecatedErrorMessageProperty;

		// Token: 0x0400042B RID: 1067
		private readonly IDictionary<ObjectType, TmdlObjectInfo> metadataObjects;

		// Token: 0x0400042C RID: 1068
		private readonly IMetadataFilter filter;

		// Token: 0x0400042D RID: 1069
		private TmdlObjectInfo rootObjectInfo;

		// Token: 0x0400042E RID: 1070
		private string choiceOption;

		// Token: 0x0400042F RID: 1071
		private List<TmdlObjectInfoWriter.InnerScope> scopes;

		// Token: 0x04000430 RID: 1072
		private TmdlObjectInfoWriter.InfoWriteState state;

		// Token: 0x0200034B RID: 843
		private enum InfoWriteState
		{
			// Token: 0x04000E58 RID: 3672
			Start,
			// Token: 0x04000E59 RID: 3673
			RootObjectChoice,
			// Token: 0x04000E5A RID: 3674
			RootObject,
			// Token: 0x04000E5B RID: 3675
			CrossLink,
			// Token: 0x04000E5C RID: 3676
			InnerScope,
			// Token: 0x04000E5D RID: 3677
			SupplementaryScope,
			// Token: 0x04000E5E RID: 3678
			End,
			// Token: 0x04000E5F RID: 3679
			Completed
		}

		// Token: 0x0200034C RID: 844
		private enum PropertyKind
		{
			// Token: 0x04000E61 RID: 3681
			Regular,
			// Token: 0x04000E62 RID: 3682
			Default,
			// Token: 0x04000E63 RID: 3683
			Name,
			// Token: 0x04000E64 RID: 3684
			Description
		}

		// Token: 0x0200034D RID: 845
		private struct InnerScope
		{
			// Token: 0x0600259D RID: 9629 RVA: 0x000E88D3 File Offset: 0x000E6AD3
			private InnerScope(string name, int scopeType, ObjectType objectType, string choiceOption, TmdlObjectInfo objectInfo, ICollection<TmdlPropertyInfo> propertyInfos, string description)
			{
				this.name = name;
				this.scopeType = scopeType;
				this.objectType = objectType;
				this.choiceOption = choiceOption;
				this.objectInfo = objectInfo;
				this.propertyInfos = propertyInfos;
				this.description = description;
			}

			// Token: 0x17000794 RID: 1940
			// (get) Token: 0x0600259E RID: 9630 RVA: 0x000E890A File Offset: 0x000E6B0A
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x17000795 RID: 1941
			// (get) Token: 0x0600259F RID: 9631 RVA: 0x000E8912 File Offset: 0x000E6B12
			public int ScopeType
			{
				get
				{
					return this.scopeType;
				}
			}

			// Token: 0x17000796 RID: 1942
			// (get) Token: 0x060025A0 RID: 9632 RVA: 0x000E891A File Offset: 0x000E6B1A
			public ObjectType ObjectType
			{
				get
				{
					return this.objectType;
				}
			}

			// Token: 0x17000797 RID: 1943
			// (get) Token: 0x060025A1 RID: 9633 RVA: 0x000E8922 File Offset: 0x000E6B22
			public string ChoiceOption
			{
				get
				{
					return this.choiceOption;
				}
			}

			// Token: 0x17000798 RID: 1944
			// (get) Token: 0x060025A2 RID: 9634 RVA: 0x000E892A File Offset: 0x000E6B2A
			public string Description
			{
				get
				{
					return this.description;
				}
			}

			// Token: 0x17000799 RID: 1945
			// (get) Token: 0x060025A3 RID: 9635 RVA: 0x000E8932 File Offset: 0x000E6B32
			public bool IsProperty
			{
				get
				{
					return this.scopeType == 8;
				}
			}

			// Token: 0x1700079A RID: 1946
			// (get) Token: 0x060025A4 RID: 9636 RVA: 0x000E893D File Offset: 0x000E6B3D
			public bool IsCrossLink
			{
				get
				{
					return this.scopeType == 16;
				}
			}

			// Token: 0x1700079B RID: 1947
			// (get) Token: 0x060025A5 RID: 9637 RVA: 0x000E8949 File Offset: 0x000E6B49
			public bool IsObjectInfo
			{
				get
				{
					return this.scopeType == 4;
				}
			}

			// Token: 0x1700079C RID: 1948
			// (get) Token: 0x060025A6 RID: 9638 RVA: 0x000E8954 File Offset: 0x000E6B54
			public bool IsSupplementaryScope
			{
				get
				{
					return this.scopeType == -1 || (this.scopeType > 0 && this.scopeType < 4);
				}
			}

			// Token: 0x1700079D RID: 1949
			// (get) Token: 0x060025A7 RID: 9639 RVA: 0x000E8975 File Offset: 0x000E6B75
			internal bool IsValid
			{
				get
				{
					return this.scopeType != 0;
				}
			}

			// Token: 0x060025A8 RID: 9640 RVA: 0x000E8980 File Offset: 0x000E6B80
			public static TmdlObjectInfoWriter.InnerScope CreatePropertyScope(string name)
			{
				return new TmdlObjectInfoWriter.InnerScope(name, 8, ObjectType.Null, null, null, new List<TmdlPropertyInfo>(), null);
			}

			// Token: 0x060025A9 RID: 9641 RVA: 0x000E8992 File Offset: 0x000E6B92
			public static TmdlObjectInfoWriter.InnerScope CreateCrossLinkScope(string name)
			{
				return new TmdlObjectInfoWriter.InnerScope(name, 16, ObjectType.Null, null, null, new List<TmdlPropertyInfo>(), null);
			}

			// Token: 0x060025AA RID: 9642 RVA: 0x000E89A5 File Offset: 0x000E6BA5
			public static TmdlObjectInfoWriter.InnerScope CreateObjectInfoScope(string name, ObjectType objectType, bool isSingleChild)
			{
				return new TmdlObjectInfoWriter.InnerScope(name, 4, objectType, null, TmdlObjectInfoWriter.CreateObjectInfo(objectType, isSingleChild, null), null, null);
			}

			// Token: 0x060025AB RID: 9643 RVA: 0x000E89BA File Offset: 0x000E6BBA
			public static TmdlObjectInfoWriter.InnerScope CreateSupplementaryScope(string name, ObjectType objectType)
			{
				return new TmdlObjectInfoWriter.InnerScope(name, (objectType == ObjectType.Null) ? (-1) : 2, objectType, null, null, null, null);
			}

			// Token: 0x060025AC RID: 9644 RVA: 0x000E89CE File Offset: 0x000E6BCE
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public TmdlObjectInfo GetObjectInfo()
			{
				Utils.Verify(this.objectInfo != null);
				return this.objectInfo;
			}

			// Token: 0x060025AD RID: 9645 RVA: 0x000E89E4 File Offset: 0x000E6BE4
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public ICollection<TmdlPropertyInfo> GetPropertyInfos()
			{
				Utils.Verify(this.propertyInfos != null);
				return this.propertyInfos;
			}

			// Token: 0x060025AE RID: 9646 RVA: 0x000E89FC File Offset: 0x000E6BFC
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public TmdlObjectInfoWriter.InnerScope ConvertSupplementaryScope(int scopeType)
			{
				switch (this.scopeType)
				{
				case -1:
					return new TmdlObjectInfoWriter.InnerScope(this.name, scopeType, this.objectType, null, null, null, null);
				case 2:
					Utils.Verify(scopeType == 1, "The only valid transition from a collection is to add choice scope");
					return new TmdlObjectInfoWriter.InnerScope(this.name, 3, this.objectType, null, null, null, null);
				case 3:
					Utils.Verify(scopeType == 2, "The only valid transition from a collection of choices is to remove the choice scope");
					return new TmdlObjectInfoWriter.InnerScope(this.name, 2, this.objectType, null, null, null, null);
				}
				throw TomInternalException.Create("Invalid transition of collection-or-choice - current scope type={0}, update={1}", new object[] { this.scopeType, scopeType });
			}

			// Token: 0x060025AF RID: 9647 RVA: 0x000E8AB8 File Offset: 0x000E6CB8
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public TmdlObjectInfoWriter.InnerScope SetChoiceOption(string choiceOption, string description)
			{
				int num = this.scopeType;
				if (num == 1 || num == 3)
				{
					return new TmdlObjectInfoWriter.InnerScope(this.name, this.scopeType, this.objectType, choiceOption, null, null, description);
				}
				throw TomInternalException.Create("Invalid request to set the choice-option - current scope type={0}", new object[] { this.scopeType });
			}

			// Token: 0x060025B0 RID: 9648 RVA: 0x000E8B10 File Offset: 0x000E6D10
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public TmdlObjectInfoWriter.InnerScope ResetChoiceOption()
			{
				int num = this.scopeType;
				if (num == 1 || num == 3)
				{
					return new TmdlObjectInfoWriter.InnerScope(this.name, this.scopeType, this.objectType, null, null, null, null);
				}
				throw TomInternalException.Create("Invalid request to reset the choice-option - current scope type={0}", new object[] { this.scopeType });
			}

			// Token: 0x04000E65 RID: 3685
			private readonly string name;

			// Token: 0x04000E66 RID: 3686
			private readonly int scopeType;

			// Token: 0x04000E67 RID: 3687
			private readonly ObjectType objectType;

			// Token: 0x04000E68 RID: 3688
			private readonly string choiceOption;

			// Token: 0x04000E69 RID: 3689
			private readonly TmdlObjectInfo objectInfo;

			// Token: 0x04000E6A RID: 3690
			private readonly ICollection<TmdlPropertyInfo> propertyInfos;

			// Token: 0x04000E6B RID: 3691
			private readonly string description;
		}
	}
}
