using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.OData.Client.Metadata;

namespace Microsoft.OData.Client.Materialization
{
	// Token: 0x020000FB RID: 251
	internal class EntryValueMaterializationPolicy : StructuralValueMaterializationPolicy
	{
		// Token: 0x06000AA3 RID: 2723 RVA: 0x00027C77 File Offset: 0x00025E77
		internal EntryValueMaterializationPolicy(IODataMaterializerContext context, EntityTrackingAdapter entityTrackingAdapter, SimpleLazy<PrimitivePropertyConverter> lazyPrimitivePropertyConverter, Dictionary<IEnumerable, DataServiceQueryContinuation> nextLinkTable)
			: base(context, lazyPrimitivePropertyConverter)
		{
			this.nextLinkTable = nextLinkTable;
			this.EntityTrackingAdapter = entityTrackingAdapter;
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x00027C90 File Offset: 0x00025E90
		// (set) Token: 0x06000AA5 RID: 2725 RVA: 0x00027C98 File Offset: 0x00025E98
		internal EntityTrackingAdapter EntityTrackingAdapter { get; private set; }

		// Token: 0x06000AA6 RID: 2726 RVA: 0x00027CA1 File Offset: 0x00025EA1
		internal static void ValidatePropertyMatch(ClientPropertyAnnotation property, ODataNestedResourceInfo link)
		{
			EntryValueMaterializationPolicy.ValidatePropertyMatch(property, link, null, false);
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x00027CAD File Offset: 0x00025EAD
		internal static void ValidatePropertyMatch(ClientPropertyAnnotation property, ODataProperty atomProperty)
		{
			EntryValueMaterializationPolicy.ValidatePropertyMatch(property, atomProperty, null, false);
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x00027CB8 File Offset: 0x00025EB8
		internal static Type ValidatePropertyMatch(ClientPropertyAnnotation property, ODataNestedResourceInfo link, ClientEdmModel model, bool performEntityCheck)
		{
			Type type = null;
			if (link.IsCollection != null)
			{
				if (link.IsCollection.Value)
				{
					if (!property.IsResourceSet)
					{
						throw Error.InvalidOperation(Strings.Deserialize_MismatchAtomLinkFeedPropertyNotCollection(property.PropertyName));
					}
					type = property.ResourceSetItemType;
				}
				else
				{
					if (property.IsResourceSet)
					{
						throw Error.InvalidOperation(Strings.Deserialize_MismatchAtomLinkEntryPropertyIsCollection(property.PropertyName));
					}
					type = property.PropertyType;
				}
			}
			if (type != null && performEntityCheck && !ClientTypeUtil.TypeIsStructured(type, model))
			{
				throw Error.InvalidOperation(Strings.AtomMaterializer_InvalidNonEntityType(type.ToString()));
			}
			return type;
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00027D50 File Offset: 0x00025F50
		internal static void ValidatePropertyMatch(ClientPropertyAnnotation property, ODataProperty atomProperty, ClientEdmModel model, bool performEntityCheck)
		{
			ODataResourceSet odataResourceSet = atomProperty.Value as ODataResourceSet;
			ODataResource odataResource = atomProperty.Value as ODataResource;
			if (property.IsKnownType && (odataResourceSet != null || odataResource != null))
			{
				throw Error.InvalidOperation(Strings.Deserialize_MismatchAtomLinkLocalSimple);
			}
			Type type = null;
			if (odataResourceSet != null)
			{
				if (!property.IsEntityCollection)
				{
					throw Error.InvalidOperation(Strings.Deserialize_MismatchAtomLinkFeedPropertyNotCollection(property.PropertyName));
				}
				type = property.EntityCollectionItemType;
			}
			if (odataResource != null)
			{
				if (property.IsEntityCollection)
				{
					throw Error.InvalidOperation(Strings.Deserialize_MismatchAtomLinkEntryPropertyIsCollection(property.PropertyName));
				}
				type = property.PropertyType;
			}
			if (type != null && performEntityCheck && !ClientTypeUtil.TypeIsEntity(type, model))
			{
				throw Error.InvalidOperation(Strings.AtomMaterializer_InvalidNonEntityType(type.ToString()));
			}
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x00027DFC File Offset: 0x00025FFC
		internal void Materialize(MaterializerEntry entry, Type expectedEntryType, bool includeLinks)
		{
			if ((entry.IsTracking && entry.Id == null) || !this.EntityTrackingAdapter.TryResolveExistingEntity(entry, expectedEntryType))
			{
				ClientTypeAnnotation clientTypeAnnotation = base.MaterializerContext.ResolveTypeForMaterialization(expectedEntryType, entry.Entry.TypeName);
				this.ResolveByCreatingWithType(entry, clientTypeAnnotation.ElementType);
			}
			this.MaterializeResolvedEntry(entry, includeLinks);
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00027E5C File Offset: 0x0002605C
		internal void ApplyItemsToCollection(MaterializerEntry entry, ClientPropertyAnnotation property, IEnumerable items, Uri nextLink, ProjectionPlan continuationPlan, bool isContinuation)
		{
			IEnumerable<object> enumerable = ODataEntityMaterializer.EnumerateAsElementType<object>(items);
			object obj = this.PopulateCollectionProperty(entry, property, enumerable, nextLink, continuationPlan);
			if (this.EntityTrackingAdapter.MergeOption == MergeOption.OverwriteChanges || this.EntityTrackingAdapter.MergeOption == MergeOption.PreserveChanges)
			{
				var enumerable2 = from l in this.EntityTrackingAdapter.EntityTracker.GetLinks(entry.ResolvedObject, property.PropertyName)
					select new { l.Target, l.IsModified };
				if (obj != null && !property.IsDictionary)
				{
					object[] array = ODataEntityMaterializer.EnumerateAsElementType<object>((IEnumerable)obj).Except(enumerable2.Select(i => i.Target)).Except(enumerable)
						.ToArray<object>();
					foreach (object obj2 in array)
					{
						property.RemoveValue(obj, obj2);
					}
				}
				if (!isContinuation)
				{
					IEnumerable<object> enumerable3;
					if (this.EntityTrackingAdapter.MergeOption == MergeOption.OverwriteChanges)
					{
						enumerable3 = enumerable2.Select(i => i.Target);
					}
					else
					{
						enumerable3 = from i in enumerable2
							where !i.IsModified
							select i.Target;
					}
					enumerable3 = enumerable3.Except(enumerable);
					foreach (object obj3 in enumerable3)
					{
						if (obj != null)
						{
							property.RemoveValue(obj, obj3);
						}
						this.EntityTrackingAdapter.MaterializationLog.RemovedLink(entry, property.PropertyName, obj3);
					}
				}
			}
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x00028044 File Offset: 0x00026244
		internal void FoundNextLinkForCollection(IEnumerable collection, Uri link, ProjectionPlan plan)
		{
			if (collection != null && !this.nextLinkTable.ContainsKey(collection))
			{
				DataServiceQueryContinuation dataServiceQueryContinuation = DataServiceQueryContinuation.Create(link, plan);
				this.nextLinkTable.Add(collection, dataServiceQueryContinuation);
				Util.SetNextLinkForCollection(collection, dataServiceQueryContinuation);
			}
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0002807E File Offset: 0x0002627E
		internal void FoundNextLinkForUnmodifiedCollection(IEnumerable collection)
		{
			if (collection != null && !this.nextLinkTable.ContainsKey(collection))
			{
				this.nextLinkTable.Add(collection, null);
			}
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x000280A0 File Offset: 0x000262A0
		internal void ResolveByCreatingWithType(MaterializerEntry entry, Type type)
		{
			ClientEdmModel model = base.MaterializerContext.Model;
			entry.ActualType = model.GetClientTypeAnnotation(model.GetOrCreateEdmType(type));
			entry.ResolvedObject = this.CreateNewInstance(entry.ActualType.EdmTypeReference, type);
			entry.CreatedByMaterializer = true;
			entry.ShouldUpdateFromPayload = true;
			entry.EntityHasBeenResolved = true;
			this.EntityTrackingAdapter.MaterializationLog.CreatedInstance(entry);
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0002810C File Offset: 0x0002630C
		private static void ValidateCollectionElementTypeIsItemType(Type itemType, Type collectionElementType)
		{
			if (!collectionElementType.IsAssignableFrom(itemType))
			{
				string text = Strings.AtomMaterializer_EntryIntoCollectionMismatch(itemType.FullName, collectionElementType.FullName);
				throw new InvalidOperationException(text);
			}
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x0002813C File Offset: 0x0002633C
		private static void ApplyLinkProperties(ClientTypeAnnotation actualType, MaterializerEntry entry)
		{
			if (entry.ShouldUpdateFromPayload)
			{
				foreach (ClientPropertyAnnotation clientPropertyAnnotation in from p in actualType.Properties()
					where p.PropertyType == typeof(DataServiceStreamLink)
					select p)
				{
					string propertyName = clientPropertyAnnotation.PropertyName;
					StreamDescriptor streamDescriptor;
					if (entry.EntityDescriptor.TryGetNamedStreamInfo(propertyName, out streamDescriptor))
					{
						clientPropertyAnnotation.SetValue(entry.ResolvedObject, streamDescriptor.StreamLink, propertyName, true);
					}
				}
			}
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x000281DC File Offset: 0x000263DC
		private object PopulateCollectionProperty(MaterializerEntry entry, ClientPropertyAnnotation property, IEnumerable<object> items, Uri nextLink, ProjectionPlan continuationPlan)
		{
			object obj = null;
			ClientEdmModel model = base.MaterializerContext.Model;
			ClientTypeAnnotation clientTypeAnnotation = model.GetClientTypeAnnotation(model.GetOrCreateEdmType(property.ResourceSetItemType));
			if (entry.ShouldUpdateFromPayload)
			{
				obj = this.GetOrCreateCollectionProperty(entry.ResolvedObject, property, entry.ForLoadProperty);
				foreach (object obj2 in items)
				{
					EntryValueMaterializationPolicy.ValidateCollectionElementTypeIsItemType(obj2.GetType(), clientTypeAnnotation.ElementType);
					property.SetValue(obj, obj2, property.PropertyName, true);
					this.EntityTrackingAdapter.MaterializationLog.AddedLink(entry, property.PropertyName, obj2);
				}
				this.FoundNextLinkForCollection(obj as IEnumerable, nextLink, continuationPlan);
			}
			else
			{
				foreach (object obj3 in items)
				{
					EntryValueMaterializationPolicy.ValidateCollectionElementTypeIsItemType(obj3.GetType(), clientTypeAnnotation.ElementType);
				}
				this.FoundNextLinkForUnmodifiedCollection(property.GetValue(entry.ResolvedObject) as IEnumerable);
			}
			return obj;
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0002830C File Offset: 0x0002650C
		private object GetOrCreateCollectionProperty(object instance, ClientPropertyAnnotation property, bool forLoadProperty)
		{
			object obj = property.GetValue(instance);
			if (obj == null)
			{
				Type type = property.PropertyType;
				if (forLoadProperty)
				{
					if (BindingEntityInfo.IsDataServiceCollection(type, base.MaterializerContext.Model))
					{
						obj = Activator.CreateInstance(WebUtil.GetDataServiceCollectionOfT(new Type[] { property.EntityCollectionItemType }), new object[]
						{
							null,
							TrackingMode.None
						});
					}
					else
					{
						Type type2 = typeof(List<>).MakeGenericType(new Type[] { property.EntityCollectionItemType });
						if (type.IsAssignableFrom(type2))
						{
							type = type2;
						}
						obj = Activator.CreateInstance(type);
					}
				}
				else
				{
					if (type.IsInterface())
					{
						type = typeof(Collection<>).MakeGenericType(new Type[] { property.EntityCollectionItemType });
					}
					obj = this.CreateNewInstance(property.EdmProperty.Type, type);
				}
				property.SetValue(instance, obj, property.PropertyName, false);
			}
			return obj;
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x000283EC File Offset: 0x000265EC
		private void ApplyFeedToCollection(MaterializerEntry entry, ClientPropertyAnnotation property, ODataResourceSet feed, bool includeLinks)
		{
			ClientEdmModel model = base.MaterializerContext.Model;
			ClientTypeAnnotation clientTypeAnnotation = model.GetClientTypeAnnotation(model.GetOrCreateEdmType(property.ResourceSetItemType));
			IEnumerable<ODataResource> entries = MaterializerFeed.GetFeed(feed).Entries;
			foreach (ODataResource odataResource in entries)
			{
				this.Materialize(MaterializerEntry.GetEntry(odataResource), clientTypeAnnotation.ElementType, includeLinks);
			}
			ProjectionPlan projectionPlan = (includeLinks ? ODataEntityMaterializer.CreatePlanForDirectMaterialization(property.ResourceSetItemType) : ODataEntityMaterializer.CreatePlanForShallowMaterialization(property.ResourceSetItemType));
			this.ApplyItemsToCollection(entry, property, entries.Select((ODataResource e) => MaterializerEntry.GetEntry(e).ResolvedObject), feed.NextPageLink, projectionPlan, false);
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x000284CC File Offset: 0x000266CC
		private void MaterializeResolvedEntry(MaterializerEntry entry, bool includeLinks)
		{
			ClientTypeAnnotation actualType = entry.ActualType;
			if (!actualType.IsStructuredType)
			{
				throw Error.InvalidOperation(Strings.AtomMaterializer_InvalidNonEntityType(actualType.ElementTypeName));
			}
			base.MaterializeDataValues(actualType, entry.Properties, base.MaterializerContext.UndeclaredPropertyBehavior);
			if (entry.NestedResourceInfos != null)
			{
				foreach (ODataNestedResourceInfo odataNestedResourceInfo in entry.NestedResourceInfos)
				{
					ClientPropertyAnnotation property = actualType.GetProperty(odataNestedResourceInfo.Name, UndeclaredPropertyBehavior.Support);
					if (property != null)
					{
						EntryValueMaterializationPolicy.ValidatePropertyMatch(property, odataNestedResourceInfo, base.MaterializerContext.Model, true);
					}
				}
				foreach (ODataNestedResourceInfo odataNestedResourceInfo2 in entry.NestedResourceInfos)
				{
					MaterializerNavigationLink link = MaterializerNavigationLink.GetLink(odataNestedResourceInfo2);
					if (link != null)
					{
						ClientPropertyAnnotation property2 = actualType.GetProperty(odataNestedResourceInfo2.Name, base.MaterializerContext.UndeclaredPropertyBehavior);
						if (property2 != null && (includeLinks || (!property2.IsEntityCollection && !(property2.EntityCollectionItemType != null))))
						{
							if (link.Feed != null)
							{
								this.ApplyFeedToCollection(entry, property2, link.Feed, includeLinks);
							}
							else if (link.Entry != null)
							{
								MaterializerEntry entry2 = link.Entry;
								if (entry2.Entry != null)
								{
									this.Materialize(entry2, property2.PropertyType, includeLinks);
								}
								if (entry.ShouldUpdateFromPayload)
								{
									property2.SetValue(entry.ResolvedObject, entry2.ResolvedObject, odataNestedResourceInfo2.Name, true);
									if (!base.MaterializerContext.Context.DisableInstanceAnnotationMaterialization && entry2.ShouldUpdateFromPayload)
									{
										base.InstanceAnnotationMaterializationPolicy.SetInstanceAnnotations(property2.PropertyName, entry2.Entry, entry.ActualType.ElementType, entry.ResolvedObject);
										base.InstanceAnnotationMaterializationPolicy.SetInstanceAnnotations(entry2.Entry, entry2.ResolvedObject);
									}
									this.EntityTrackingAdapter.MaterializationLog.SetLink(entry, property2.PropertyName, entry2.ResolvedObject);
								}
							}
						}
					}
				}
			}
			foreach (ODataProperty odataProperty in entry.Properties)
			{
				if (!(odataProperty.Value is ODataStreamReferenceValue))
				{
					ClientPropertyAnnotation property3 = actualType.GetProperty(odataProperty.Name, base.MaterializerContext.UndeclaredPropertyBehavior);
					if (property3 != null && entry.ShouldUpdateFromPayload)
					{
						EntryValueMaterializationPolicy.ValidatePropertyMatch(property3, odataProperty, base.MaterializerContext.Model, true);
						base.ApplyDataValue(actualType, odataProperty, entry.ResolvedObject);
					}
				}
			}
			EntryValueMaterializationPolicy.ApplyLinkProperties(actualType, entry);
			BaseEntityType baseEntityType = entry.ResolvedObject as BaseEntityType;
			if (baseEntityType != null)
			{
				baseEntityType.Context = this.EntityTrackingAdapter.Context;
			}
			base.MaterializerContext.ResponsePipeline.FireEndEntryEvents(entry);
		}

		// Token: 0x04000615 RID: 1557
		private readonly Dictionary<IEnumerable, DataServiceQueryContinuation> nextLinkTable;
	}
}
