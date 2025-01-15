using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x020001E4 RID: 484
	internal class CrossLink<TOwner, TObject> : ObjectLinkBase<TOwner, TObject> where TOwner : MetadataObject where TObject : MetadataObject
	{
		// Token: 0x06001C49 RID: 7241 RVA: 0x000C539E File Offset: 0x000C359E
		public CrossLink(TOwner owner, string propertyName)
			: base(owner, propertyName)
		{
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001C4A RID: 7242 RVA: 0x000C53A8 File Offset: 0x000C35A8
		public override LinkType LinkType
		{
			get
			{
				return LinkType.CrossLink;
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001C4B RID: 7243 RVA: 0x000C53AB File Offset: 0x000C35AB
		// (set) Token: 0x06001C4C RID: 7244 RVA: 0x000C53C3 File Offset: 0x000C35C3
		public override TObject Object
		{
			get
			{
				if (!this.IsResolved)
				{
					this.TryResolveByPathImpl(null);
				}
				return base.Object;
			}
			set
			{
				base.Object = value;
				this.Path = null;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001C4D RID: 7245 RVA: 0x000C53D4 File Offset: 0x000C35D4
		public override bool IsResolved
		{
			get
			{
				return base.Object != null || (base.ObjectID.IsNull && this.Path == null);
			}
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x000C540C File Offset: 0x000C360C
		public void SerializeToJsonObject(bool isNewJsonStyle, string propName, ObjectType pathTopObjectType, JsonObject jsonObject, int jsonPropRelOrder = 0, bool isReadOnly = false)
		{
			if (base.Object == null)
			{
				return;
			}
			IList<MetadataObject> pathForSerialization = CrossLink<TOwner, TObject>.GetPathForSerialization(pathTopObjectType, base.Object);
			if (pathForSerialization.Count == 1)
			{
				jsonObject[propName, TomPropCategory.Regular, jsonPropRelOrder, isReadOnly] = ((NamedMetadataObject)pathForSerialization[0]).Name;
				return;
			}
			if (isNewJsonStyle)
			{
				JsonObject jsonObject2 = new JsonObject();
				for (int i = pathForSerialization.Count - 1; i >= 0; i--)
				{
					jsonObject2[JsonPropertyName.ObjectPath.GetObjectPathPropertyName(pathForSerialization[i].ObjectType), TomPropCategory.Regular, pathForSerialization.Count - i - 1, isReadOnly] = ((NamedMetadataObject)pathForSerialization[i]).Name;
				}
				jsonObject[propName, TomPropCategory.Regular, jsonPropRelOrder, isReadOnly] = jsonObject2.ToDictObject();
				return;
			}
			for (int j = pathForSerialization.Count - 1; j >= 0; j--)
			{
				jsonObject[JsonPropertyName.ObjectPath.Get1200SyleObjectPathPropertyName(propName, pathForSerialization[j].ObjectType), TomPropCategory.Regular, jsonPropRelOrder, isReadOnly] = ((NamedMetadataObject)pathForSerialization[j]).Name;
			}
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x000C5508 File Offset: 0x000C3708
		public static void WriteMetadataSchema(ObjectType targetType, ObjectType rootObjectType, bool writeAsObjectPath, string propertyName, bool isReadOnly, IMetadataSchemaWriter writer)
		{
			IList<ObjectType> pathForSerialization = CrossLink<TOwner, TObject>.GetPathForSerialization(rootObjectType, targetType);
			if (pathForSerialization.Count == 1)
			{
				writer.WriteProperty(propertyName, isReadOnly ? (MetadataPropertyNature.TypeProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly) : MetadataPropertyNature.CrossLinkProperty, typeof(string));
				return;
			}
			if (writeAsObjectPath)
			{
				using (writer.CreateComplexPropertyScope(propertyName, isReadOnly ? (MetadataPropertyNature.TypeProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly) : MetadataPropertyNature.CrossLinkProperty, null, null, new bool?(false)))
				{
					for (int i = pathForSerialization.Count - 1; i >= 0; i--)
					{
						writer.WriteProperty(JsonPropertyName.ObjectPath.GetObjectPathPropertyName(pathForSerialization[i]), MetadataPropertyNature.RegularProperty, typeof(string));
					}
					return;
				}
			}
			MetadataPropertyNature metadataPropertyNature = (isReadOnly ? (MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly) : MetadataPropertyNature.RegularProperty);
			for (int j = pathForSerialization.Count - 1; j >= 0; j--)
			{
				writer.WriteProperty(JsonPropertyName.ObjectPath.Get1200SyleObjectPathPropertyName(propertyName, pathForSerialization[j]), metadataPropertyNature, typeof(string));
			}
		}

		// Token: 0x06001C50 RID: 7248 RVA: 0x000C55F8 File Offset: 0x000C37F8
		public void WriteToMetadataStream(ObjectType rootObjectType, bool writeAsObjectPath, string propertyName, bool isReadOnly, IMetadataWriter writer)
		{
			foreach (MetadataProperty metadataProperty in this.WriteToMetadataStreamImpl(rootObjectType, writeAsObjectPath, propertyName, isReadOnly))
			{
				writer.WriteProperty(metadataProperty.Name, metadataProperty.Nature, metadataProperty.ValueType, metadataProperty.Value);
			}
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x000C5664 File Offset: 0x000C3864
		internal IEnumerable<MetadataProperty> WriteToMetadataStreamImpl(ObjectType rootObjectType, bool writeAsObjectPath, string propertyName, bool isReadOnly)
		{
			if (base.Object == null)
			{
				yield break;
			}
			IList<MetadataObject> path = CrossLink<TOwner, TObject>.GetPathForSerialization(rootObjectType, base.Object);
			if (path.Count == 1)
			{
				yield return new MetadataProperty(propertyName, isReadOnly ? (MetadataPropertyNature.TypeProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly) : MetadataPropertyNature.CrossLinkProperty, typeof(string), ((NamedMetadataObject)path[0]).Name);
			}
			else if (writeAsObjectPath)
			{
				yield return new MetadataProperty(propertyName, isReadOnly ? (MetadataPropertyNature.TypeProperty | MetadataPropertyNature.ParentProperty | MetadataPropertyNature.ReadOnly) : MetadataPropertyNature.CrossLinkProperty, typeof(ObjectPath), new ObjectPath(path.Select((MetadataObject o) => new KeyValuePair<ObjectType, string>(o.ObjectType, ((NamedMetadataObject)o).Name)), true));
			}
			else
			{
				MetadataPropertyNature nature = (isReadOnly ? (MetadataPropertyNature.TypeProperty | MetadataPropertyNature.NameProperty | MetadataPropertyNature.ReadOnly) : MetadataPropertyNature.RegularProperty);
				int num;
				for (int i = path.Count - 1; i >= 0; i = num - 1)
				{
					yield return new MetadataProperty(JsonPropertyName.ObjectPath.Get1200SyleObjectPathPropertyName(propertyName, path[i].ObjectType), nature, typeof(string), ((NamedMetadataObject)path[i]).Name);
					num = i;
				}
			}
			yield break;
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001C52 RID: 7250 RVA: 0x000C5691 File Offset: 0x000C3891
		// (set) Token: 0x06001C53 RID: 7251 RVA: 0x000C5699 File Offset: 0x000C3899
		internal ObjectPath Path { get; set; }

		// Token: 0x06001C54 RID: 7252 RVA: 0x000C56A2 File Offset: 0x000C38A2
		internal override ObjectPath GetPathOrNull()
		{
			return this.Path;
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x000C56AA File Offset: 0x000C38AA
		public bool TryResolveByPath()
		{
			return this.TryResolveByPathImpl(null);
		}

		// Token: 0x06001C56 RID: 7254 RVA: 0x000C56B4 File Offset: 0x000C38B4
		public bool TryResolveAfterCopy(CopyContext copyContext)
		{
			MetadataObject metadataObject;
			if (base.Object == null)
			{
				if (this.Path != null)
				{
					this.TryResolveByPathImpl(copyContext);
				}
			}
			else if (copyContext.OriginalToCloneObjectMap.TryGetValue(base.Object, out metadataObject))
			{
				this.Object = (TObject)((object)metadataObject);
			}
			else if ((copyContext.Flags & CopyFlags.UserClone) == CopyFlags.UserClone)
			{
				this.TryResolveByPathImpl(copyContext);
			}
			return this.IsResolved;
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x000C572C File Offset: 0x000C392C
		public override void CopyFrom(ObjectLinkBase<TOwner, TObject> other, CopyContext context)
		{
			CrossLink<TOwner, TObject> crossLink = (CrossLink<TOwner, TObject>)other;
			base.CopyFrom(other, context);
			this.Path = null;
			if (!crossLink.IsResolved && crossLink.Path != null)
			{
				base.Object = default(TObject);
				this.Path = crossLink.Path.Clone();
			}
		}

		// Token: 0x06001C58 RID: 7256 RVA: 0x000C5780 File Offset: 0x000C3980
		public override bool IsEqualTo(ObjectLinkBase<TOwner, TObject> other, CopyContext context)
		{
			CrossLink<TOwner, TObject> crossLink = (CrossLink<TOwner, TObject>)other;
			MetadataObject metadataObject = base.Object;
			MetadataObject metadataObject2 = other.Object;
			if (!this.IsResolved && !other.IsResolved)
			{
				return this.Path != null && crossLink.Path != null && this.Path.Equals(crossLink.Path);
			}
			MetadataObject metadataObject3;
			if (this.IsResolved && other.IsResolved && metadataObject2 != null && context.OriginalToCloneObjectMap.TryGetValue(metadataObject2, out metadataObject3))
			{
				metadataObject2 = (TObject)((object)metadataObject3);
			}
			return ((context.Flags & CopyFlags.IncludeObjectIds) != CopyFlags.IncludeObjectIds || !(base.ObjectID != other.ObjectID)) && metadataObject == metadataObject2;
		}

		// Token: 0x06001C59 RID: 7257 RVA: 0x000C5834 File Offset: 0x000C3A34
		public override bool IsEqualTo(ObjectLinkBase<TOwner, TObject> other)
		{
			CrossLink<TOwner, TObject> crossLink = (CrossLink<TOwner, TObject>)other;
			if (!this.IsResolved && !other.IsResolved)
			{
				return this.Path != null && crossLink.Path != null && this.Path.Equals(crossLink.Path);
			}
			return base.Object == other.Object;
		}

		// Token: 0x06001C5A RID: 7258 RVA: 0x000C5894 File Offset: 0x000C3A94
		internal void Validate(ValidationResult result, bool throwOnError)
		{
			if (!this.IsResolved)
			{
				this.TryResolveByPathImpl(null);
			}
			ValidationUtil.ValidateLink(base.Owner, base.PropertyName, this.Object, this.Path, result, throwOnError);
		}

		// Token: 0x06001C5B RID: 7259 RVA: 0x000C58D0 File Offset: 0x000C3AD0
		private static IDictionary<ObjectType, IList<ObjectType>> CreatePathsByTargetType()
		{
			return new Dictionary<ObjectType, IList<ObjectType>>
			{
				{
					ObjectType.DataSource,
					new List<ObjectType> { ObjectType.DataSource }
				},
				{
					ObjectType.Table,
					new List<ObjectType> { ObjectType.Table }
				},
				{
					ObjectType.Column,
					new List<ObjectType>
					{
						ObjectType.Table,
						ObjectType.Column
					}
				},
				{
					ObjectType.AttributeHierarchy,
					new List<ObjectType>
					{
						ObjectType.Table,
						ObjectType.Column,
						ObjectType.AttributeHierarchy
					}
				},
				{
					ObjectType.Partition,
					new List<ObjectType>
					{
						ObjectType.Table,
						ObjectType.Partition
					}
				},
				{
					ObjectType.Relationship,
					new List<ObjectType> { ObjectType.Relationship }
				},
				{
					ObjectType.Measure,
					new List<ObjectType>
					{
						ObjectType.Table,
						ObjectType.Measure
					}
				},
				{
					ObjectType.Hierarchy,
					new List<ObjectType>
					{
						ObjectType.Table,
						ObjectType.Hierarchy
					}
				},
				{
					ObjectType.Level,
					new List<ObjectType>
					{
						ObjectType.Table,
						ObjectType.Hierarchy,
						ObjectType.Level
					}
				},
				{
					ObjectType.KPI,
					new List<ObjectType>
					{
						ObjectType.Table,
						ObjectType.Measure,
						ObjectType.KPI
					}
				},
				{
					ObjectType.Culture,
					new List<ObjectType> { ObjectType.Culture }
				},
				{
					ObjectType.ObjectTranslation,
					new List<ObjectType>
					{
						ObjectType.Culture,
						ObjectType.ObjectTranslation
					}
				},
				{
					ObjectType.LinguisticMetadata,
					new List<ObjectType>
					{
						ObjectType.Culture,
						ObjectType.LinguisticMetadata
					}
				},
				{
					ObjectType.Perspective,
					new List<ObjectType> { ObjectType.Perspective }
				},
				{
					ObjectType.PerspectiveTable,
					new List<ObjectType>
					{
						ObjectType.Perspective,
						ObjectType.PerspectiveTable
					}
				},
				{
					ObjectType.PerspectiveColumn,
					new List<ObjectType>
					{
						ObjectType.Perspective,
						ObjectType.PerspectiveTable,
						ObjectType.PerspectiveColumn
					}
				},
				{
					ObjectType.PerspectiveHierarchy,
					new List<ObjectType>
					{
						ObjectType.Perspective,
						ObjectType.PerspectiveTable,
						ObjectType.PerspectiveHierarchy
					}
				},
				{
					ObjectType.PerspectiveMeasure,
					new List<ObjectType>
					{
						ObjectType.Perspective,
						ObjectType.PerspectiveTable,
						ObjectType.PerspectiveMeasure
					}
				},
				{
					ObjectType.Role,
					new List<ObjectType> { ObjectType.Role }
				},
				{
					ObjectType.RoleMembership,
					new List<ObjectType>
					{
						ObjectType.Role,
						ObjectType.RoleMembership
					}
				},
				{
					ObjectType.TablePermission,
					new List<ObjectType>
					{
						ObjectType.Role,
						ObjectType.TablePermission
					}
				},
				{
					ObjectType.Variation,
					new List<ObjectType>
					{
						ObjectType.Table,
						ObjectType.Column,
						ObjectType.Variation
					}
				},
				{
					ObjectType.Set,
					new List<ObjectType>
					{
						ObjectType.Table,
						ObjectType.Set
					}
				},
				{
					ObjectType.PerspectiveSet,
					new List<ObjectType>
					{
						ObjectType.Perspective,
						ObjectType.PerspectiveTable,
						ObjectType.PerspectiveSet
					}
				},
				{
					ObjectType.Expression,
					new List<ObjectType> { ObjectType.Expression }
				},
				{
					ObjectType.ColumnPermission,
					new List<ObjectType>
					{
						ObjectType.Role,
						ObjectType.TablePermission,
						ObjectType.ColumnPermission
					}
				},
				{
					ObjectType.RelatedColumnDetails,
					new List<ObjectType>
					{
						ObjectType.Table,
						ObjectType.Column,
						ObjectType.RelatedColumnDetails
					}
				},
				{
					ObjectType.GroupByColumn,
					new List<ObjectType>
					{
						ObjectType.Table,
						ObjectType.Column,
						ObjectType.RelatedColumnDetails,
						ObjectType.GroupByColumn
					}
				},
				{
					ObjectType.CalculationGroup,
					new List<ObjectType>
					{
						ObjectType.Table,
						ObjectType.CalculationGroup
					}
				},
				{
					ObjectType.CalculationItem,
					new List<ObjectType>
					{
						ObjectType.Table,
						ObjectType.CalculationGroup,
						ObjectType.CalculationItem
					}
				},
				{
					ObjectType.AlternateOf,
					new List<ObjectType>
					{
						ObjectType.Table,
						ObjectType.Column,
						ObjectType.AlternateOf
					}
				},
				{
					ObjectType.RefreshPolicy,
					new List<ObjectType>
					{
						ObjectType.Table,
						ObjectType.RefreshPolicy
					}
				},
				{
					ObjectType.QueryGroup,
					new List<ObjectType> { ObjectType.QueryGroup }
				},
				{
					ObjectType.AnalyticsAIMetadata,
					new List<ObjectType> { ObjectType.AnalyticsAIMetadata }
				},
				{
					ObjectType.DataCoverageDefinition,
					new List<ObjectType>
					{
						ObjectType.Table,
						ObjectType.Partition,
						ObjectType.DataCoverageDefinition
					}
				},
				{
					ObjectType.CalculationExpression,
					new List<ObjectType>
					{
						ObjectType.Table,
						ObjectType.CalculationGroup,
						ObjectType.CalculationExpression
					}
				},
				{
					ObjectType.Calendar,
					new List<ObjectType>
					{
						ObjectType.Table,
						ObjectType.Calendar
					}
				},
				{
					ObjectType.TimeUnitColumnAssociation,
					new List<ObjectType>
					{
						ObjectType.Table,
						ObjectType.Calendar,
						ObjectType.TimeUnitColumnAssociation
					}
				},
				{
					ObjectType.CalendarColumnReference,
					new List<ObjectType>
					{
						ObjectType.Table,
						ObjectType.Calendar,
						ObjectType.TimeUnitColumnAssociation,
						ObjectType.CalendarColumnReference
					}
				},
				{
					ObjectType.Function,
					new List<ObjectType> { ObjectType.Function }
				},
				{
					ObjectType.BindingInfo,
					new List<ObjectType> { ObjectType.BindingInfo }
				}
			};
		}

		// Token: 0x06001C5C RID: 7260 RVA: 0x000C5DA0 File Offset: 0x000C3FA0
		private static IList<MetadataObject> GetPathForSerialization(ObjectType rootObjectType, MetadataObject @object)
		{
			List<MetadataObject> list = new List<MetadataObject>();
			while (@object != null)
			{
				list.Add(@object);
				@object = ((@object.ObjectType != rootObjectType) ? @object.Parent : null);
			}
			return list;
		}

		// Token: 0x06001C5D RID: 7261 RVA: 0x000C5DD4 File Offset: 0x000C3FD4
		private static IList<ObjectType> GetPathForSerialization(ObjectType rootObjectType, ObjectType target)
		{
			List<ObjectType> list = new List<ObjectType>();
			IList<ObjectType> list2;
			if (!CrossLink<TOwner, TObject>.pathsByTargetType.TryGetValue(target, out list2))
			{
				throw TomInternalException.Create("Trying to get serialization path on an unsupported target - target={0}", new object[] { target });
			}
			for (int i = list2.Count - 1; i >= 0; i = ((list2[i] != rootObjectType) ? (i - 1) : (-1)))
			{
				list.Add(list2[i]);
			}
			return list;
		}

		// Token: 0x06001C5E RID: 7262 RVA: 0x000C5E40 File Offset: 0x000C4040
		private static bool TryResolveObjectByPath(MetadataObject owner, ObjectPath path, out MetadataObject @object)
		{
			if (path.Count == 0)
			{
				@object = owner.Model;
			}
			else
			{
				bool flag = false;
				Func<IMetadataObjectCollection, bool> <>9__0;
				MetadataObject metadataObject;
				for (metadataObject = owner; metadataObject != null; metadataObject = metadataObject.Parent)
				{
					if (metadataObject.ObjectType == path[0].Key)
					{
						if (!string.IsNullOrEmpty(path[0].Value))
						{
							NamedMetadataObject namedMetadataObject = metadataObject as NamedMetadataObject;
							if (namedMetadataObject == null || string.Compare(path[0].Value, namedMetadataObject.Name, StringComparison.OrdinalIgnoreCase) != 0)
							{
								goto IL_00C9;
							}
						}
						flag = true;
						break;
					}
					IEnumerable<IMetadataObjectCollection> childrenCollections = metadataObject.GetChildrenCollections(false);
					Func<IMetadataObjectCollection, bool> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = (IMetadataObjectCollection coll) => coll.ItemType == path[0].Key);
					}
					if (childrenCollections.Any(func))
					{
						break;
					}
					IL_00C9:;
				}
				if (metadataObject != null)
				{
					@object = ObjectTreeHelper.LocateObjectByPathImpl(path, metadataObject, flag);
				}
				else
				{
					@object = null;
				}
			}
			return @object != null;
		}

		// Token: 0x06001C5F RID: 7263 RVA: 0x000C5F40 File Offset: 0x000C4140
		private bool TryResolveByPathImpl(CopyContext context)
		{
			if (this.Path == null)
			{
				if (base.Object != null && base.Owner.FindRoot() != base.Object.FindRoot())
				{
					ObjectPath path = base.Object.GetPath(null);
					MetadataObject metadataObject;
					if (CrossLink<TOwner, TObject>.TryResolveObjectByPath(base.Owner, path, out metadataObject))
					{
						this.Object = (TObject)((object)metadataObject);
					}
					else
					{
						base.Object = default(TObject);
						this.Path = path;
					}
				}
				return base.IsResolved;
			}
			this.Path.Normalize();
			MetadataObject metadataObject2;
			if (!CrossLink<TOwner, TObject>.TryResolveObjectByPath(base.Owner, this.Path, out metadataObject2))
			{
				return false;
			}
			base.Object = (TObject)((object)metadataObject2);
			return true;
		}

		// Token: 0x0400067C RID: 1660
		private static readonly IDictionary<ObjectType, IList<ObjectType>> pathsByTargetType = CrossLink<TOwner, TObject>.CreatePathsByTargetType();
	}
}
