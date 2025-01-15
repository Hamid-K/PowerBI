using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.OData.Client.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Client.Materialization
{
	// Token: 0x02000106 RID: 262
	internal abstract class ODataEntityMaterializer : ODataMaterializer
	{
		// Token: 0x06000B07 RID: 2823 RVA: 0x000298F8 File Offset: 0x00027AF8
		public ODataEntityMaterializer(IODataMaterializerContext materializerContext, EntityTrackingAdapter entityTrackingAdapter, QueryComponents queryComponents, Type expectedType, ProjectionPlan materializeEntryPlan)
			: base(materializerContext, expectedType)
		{
			this.materializeEntryPlan = materializeEntryPlan ?? ODataEntityMaterializer.CreatePlan(queryComponents);
			this.EntityTrackingAdapter = entityTrackingAdapter;
			SimpleLazy<PrimitivePropertyConverter> simpleLazy = new SimpleLazy<PrimitivePropertyConverter>(() => new PrimitivePropertyConverter());
			this.entryValueMaterializationPolicy = new EntryValueMaterializationPolicy(base.MaterializerContext, this.EntityTrackingAdapter, simpleLazy, this.nextLinkTable);
			this.entryValueMaterializationPolicy.CollectionValueMaterializationPolicy = base.CollectionValueMaterializationPolicy;
			this.entryValueMaterializationPolicy.InstanceAnnotationMaterializationPolicy = base.InstanceAnnotationMaterializationPolicy;
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000B08 RID: 2824 RVA: 0x0002998C File Offset: 0x00027B8C
		// (set) Token: 0x06000B09 RID: 2825 RVA: 0x00029994 File Offset: 0x00027B94
		internal EntityTrackingAdapter EntityTrackingAdapter { get; private set; }

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000B0A RID: 2826 RVA: 0x0002999D File Offset: 0x00027B9D
		// (set) Token: 0x06000B0B RID: 2827 RVA: 0x000299AA File Offset: 0x00027BAA
		internal object TargetInstance
		{
			get
			{
				return this.EntityTrackingAdapter.TargetInstance;
			}
			set
			{
				this.EntityTrackingAdapter.TargetInstance = value;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000B0C RID: 2828 RVA: 0x000299B8 File Offset: 0x00027BB8
		internal sealed override object CurrentValue
		{
			get
			{
				return this.currentValue;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x000299C0 File Offset: 0x00027BC0
		internal sealed override ProjectionPlan MaterializeEntryPlan
		{
			get
			{
				return this.materializeEntryPlan;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x000299C8 File Offset: 0x00027BC8
		protected EntryValueMaterializationPolicy EntryValueMaterializationPolicy
		{
			get
			{
				return this.entryValueMaterializationPolicy;
			}
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x000299D0 File Offset: 0x00027BD0
		internal static IEnumerable<T> EnumerateAsElementType<T>(IEnumerable source)
		{
			IEnumerable<T> enumerable = source as IEnumerable<T>;
			if (enumerable != null)
			{
				return enumerable;
			}
			return ODataEntityMaterializer.EnumerateAsElementTypeInternal<T>(source);
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x000299EF File Offset: 0x00027BEF
		internal static IEnumerable<T> EnumerateAsElementTypeInternal<T>(IEnumerable source)
		{
			foreach (object obj in source)
			{
				yield return (T)((object)obj);
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00029A00 File Offset: 0x00027C00
		internal static List<TTarget> ListAsElementType<T, TTarget>(ODataEntityMaterializer materializer, IEnumerable<T> source) where T : TTarget
		{
			List<TTarget> list = source as List<TTarget>;
			if (list != null)
			{
				return list;
			}
			IList list2 = source as IList;
			List<TTarget> list3;
			if (list2 != null)
			{
				list3 = new List<TTarget>(list2.Count);
			}
			else
			{
				list3 = new List<TTarget>();
			}
			foreach (T t in source)
			{
				list3.Add((TTarget)((object)t));
			}
			DataServiceQueryContinuation dataServiceQueryContinuation;
			if (materializer.nextLinkTable.TryGetValue(source, out dataServiceQueryContinuation))
			{
				materializer.nextLinkTable[list3] = dataServiceQueryContinuation;
			}
			return list3;
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x00029AA4 File Offset: 0x00027CA4
		internal static ProjectionPlan CreatePlanForDirectMaterialization(Type lastSegmentType)
		{
			return new ProjectionPlan
			{
				Plan = new Func<object, object, Type, object>(ODataEntityMaterializerInvoker.DirectMaterializePlan),
				ProjectedType = lastSegmentType,
				LastSegmentType = lastSegmentType
			};
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x00029AD8 File Offset: 0x00027CD8
		internal static ProjectionPlan CreatePlanForShallowMaterialization(Type lastSegmentType)
		{
			return new ProjectionPlan
			{
				Plan = new Func<object, object, Type, object>(ODataEntityMaterializerInvoker.ShallowMaterializePlan),
				ProjectedType = lastSegmentType,
				LastSegmentType = lastSegmentType
			};
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x00029B0C File Offset: 0x00027D0C
		internal static bool ProjectionCheckValueForPathIsNull(MaterializerEntry entry, Type expectedType, ProjectionPath path)
		{
			if (path.Count == 0 || (path.Count == 1 && path[0].Member == null))
			{
				return entry.Entry == null;
			}
			bool flag = false;
			IEnumerable<ODataNestedResourceInfo> enumerable = entry.NestedResourceInfos;
			ClientEdmModel model = entry.EntityDescriptor.Model;
			for (int i = 0; i < path.Count; i++)
			{
				ProjectionPathSegment projectionPathSegment = path[i];
				if (projectionPathSegment.Member != null)
				{
					bool flag2 = i == path.Count - 1;
					string propertyName = projectionPathSegment.Member;
					if (projectionPathSegment.SourceTypeAs != null)
					{
						expectedType = projectionPathSegment.SourceTypeAs;
						if (!enumerable.Any((ODataNestedResourceInfo p) => p.Name == propertyName))
						{
							return true;
						}
					}
					IEdmType orCreateEdmType = model.GetOrCreateEdmType(expectedType);
					ClientPropertyAnnotation property = model.GetClientTypeAnnotation(orCreateEdmType).GetProperty(propertyName, UndeclaredPropertyBehavior.ThrowException);
					MaterializerNavigationLink propertyOrThrow = ODataEntityMaterializer.GetPropertyOrThrow(enumerable, propertyName);
					EntryValueMaterializationPolicy.ValidatePropertyMatch(property, propertyOrThrow.Link);
					if (propertyOrThrow.Feed != null)
					{
						flag = false;
					}
					else
					{
						if (propertyOrThrow.Entry == null)
						{
							return true;
						}
						if (flag2)
						{
							flag = propertyOrThrow.Entry.Entry == null;
						}
						else
						{
							entry = propertyOrThrow.Entry;
							enumerable = entry.NestedResourceInfos;
						}
					}
					expectedType = property.PropertyType;
				}
			}
			return flag;
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x00029C60 File Offset: 0x00027E60
		internal static IEnumerable ProjectionSelect(ODataEntityMaterializer materializer, MaterializerEntry entry, Type expectedType, Type resultType, ProjectionPath path, Func<object, object, Type, object> selector)
		{
			ClientEdmModel model = materializer.MaterializerContext.Model;
			ClientTypeAnnotation clientTypeAnnotation = entry.ActualType ?? model.GetClientTypeAnnotation(model.GetOrCreateEdmType(expectedType));
			IEnumerable enumerable = (IEnumerable)Util.ActivatorCreateInstance(typeof(List<>).MakeGenericType(new Type[] { resultType }), new object[0]);
			MaterializerNavigationLink materializerNavigationLink = null;
			ClientPropertyAnnotation clientPropertyAnnotation = null;
			for (int i = 0; i < path.Count; i++)
			{
				ProjectionPathSegment projectionPathSegment = path[i];
				if (projectionPathSegment.SourceTypeAs != null)
				{
					clientTypeAnnotation = model.GetClientTypeAnnotation(model.GetOrCreateEdmType(projectionPathSegment.SourceTypeAs));
				}
				if (projectionPathSegment.Member != null)
				{
					string member = projectionPathSegment.Member;
					clientPropertyAnnotation = clientTypeAnnotation.GetProperty(member, UndeclaredPropertyBehavior.ThrowException);
					materializerNavigationLink = ODataEntityMaterializer.GetPropertyOrThrow(entry.NestedResourceInfos, member);
					if (materializerNavigationLink.Entry != null)
					{
						entry = materializerNavigationLink.Entry;
						clientTypeAnnotation = model.GetClientTypeAnnotation(model.GetOrCreateEdmType(clientPropertyAnnotation.PropertyType));
					}
				}
			}
			EntryValueMaterializationPolicy.ValidatePropertyMatch(clientPropertyAnnotation, materializerNavigationLink.Link);
			MaterializerFeed feed = MaterializerFeed.GetFeed(materializerNavigationLink.Feed);
			Action<object, object> addToCollectionDelegate = ClientTypeUtil.GetAddToCollectionDelegate(enumerable.GetType());
			foreach (ODataResource odataResource in feed.Entries)
			{
				object obj = selector(materializer, odataResource, clientPropertyAnnotation.EntityCollectionItemType);
				addToCollectionDelegate(enumerable, obj);
			}
			ProjectionPlan projectionPlan = new ProjectionPlan();
			projectionPlan.LastSegmentType = clientPropertyAnnotation.EntityCollectionItemType;
			projectionPlan.Plan = selector;
			projectionPlan.ProjectedType = resultType;
			materializer.EntryValueMaterializationPolicy.FoundNextLinkForCollection(enumerable, feed.NextPageLink, projectionPlan);
			return enumerable;
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x00029E1C File Offset: 0x0002801C
		internal static ODataResource ProjectionGetEntry(MaterializerEntry entry, string name)
		{
			MaterializerNavigationLink propertyOrThrow = ODataEntityMaterializer.GetPropertyOrThrow(entry.NestedResourceInfos, name);
			MaterializerEntry entry2 = propertyOrThrow.Entry;
			if (entry2 == null)
			{
				throw new InvalidOperationException(Strings.AtomMaterializer_PropertyNotExpectedEntry(name));
			}
			ODataEntityMaterializer.CheckEntryToAccessNotNull(entry2, name);
			return entry2.Entry;
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x00029E5C File Offset: 0x0002805C
		[SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = "Need to throw the type that the expression would throw with other providers")]
		internal static object ProjectionInitializeEntity(ODataEntityMaterializer materializer, MaterializerEntry entry, Type expectedType, Type resultType, string[] properties, Func<object, object, Type, object>[] propertyValues)
		{
			if (entry.Entry == null)
			{
				throw new NullReferenceException(Strings.AtomMaterializer_EntryToInitializeIsNull(resultType.FullName));
			}
			if (!entry.EntityHasBeenResolved)
			{
				ODataEntityMaterializer.ProjectionEnsureEntryAvailableOfType(materializer, entry, resultType);
			}
			else if (!resultType.IsAssignableFrom(entry.ActualType.ElementType))
			{
				string text = Strings.AtomMaterializer_ProjectEntityTypeMismatch(resultType.FullName, entry.ActualType.ElementType.FullName, entry.Id);
				throw new InvalidOperationException(text);
			}
			object resolvedObject = entry.ResolvedObject;
			for (int i = 0; i < properties.Length; i++)
			{
				string propertyName = properties[i];
				ClientPropertyAnnotation property = entry.ActualType.GetProperty(propertyName, materializer.MaterializerContext.UndeclaredPropertyBehavior);
				object obj = propertyValues[i](materializer, entry.Entry, expectedType);
				ODataProperty odataProperty = entry.Entry.Properties.Where((ODataProperty p) => p.Name == propertyName).FirstOrDefault<ODataProperty>();
				StreamDescriptor streamDescriptor;
				if (((odataProperty == null && entry.NestedResourceInfos != null) ? entry.NestedResourceInfos.Where((ODataNestedResourceInfo l) => l.Name == propertyName).FirstOrDefault<ODataNestedResourceInfo>() : null) != null || odataProperty != null || entry.EntityDescriptor.TryGetNamedStreamInfo(propertyName, out streamDescriptor))
				{
					if (entry.ShouldUpdateFromPayload)
					{
						if (property.EdmProperty.Type.TypeKind() == EdmTypeKind.Entity)
						{
							materializer.EntityTrackingAdapter.MaterializationLog.SetLink(entry, property.PropertyName, obj);
						}
						if (!property.IsEntityCollection)
						{
							if (!property.IsPrimitiveOrEnumOrComplexCollection)
							{
								property.SetValue(resolvedObject, obj, property.PropertyName, false);
							}
						}
						else
						{
							IEnumerable enumerable = (IEnumerable)obj;
							DataServiceQueryContinuation dataServiceQueryContinuation = materializer.nextLinkTable[enumerable];
							Uri uri = ((dataServiceQueryContinuation == null) ? null : dataServiceQueryContinuation.NextLinkUri);
							ProjectionPlan projectionPlan = ((dataServiceQueryContinuation == null) ? null : dataServiceQueryContinuation.Plan);
							materializer.MergeLists(entry, property, enumerable, uri, projectionPlan);
						}
					}
					else if (property.IsEntityCollection)
					{
						materializer.EntryValueMaterializationPolicy.FoundNextLinkForUnmodifiedCollection(property.GetValue(entry.ResolvedObject) as IEnumerable);
					}
				}
			}
			return resolvedObject;
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0002A070 File Offset: 0x00028270
		internal static void ProjectionEnsureEntryAvailableOfType(ODataEntityMaterializer materializer, MaterializerEntry entry, Type requiredType)
		{
			if (entry.Id == null || !materializer.EntityTrackingAdapter.TryResolveAsExistingEntry(entry, requiredType))
			{
				materializer.EntryValueMaterializationPolicy.ResolveByCreatingWithType(entry, requiredType);
				return;
			}
			if (!requiredType.IsAssignableFrom(entry.ResolvedObject.GetType()))
			{
				throw Error.InvalidOperation(Strings.Deserialize_Current(requiredType, entry.ResolvedObject.GetType()));
			}
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0002A0D2 File Offset: 0x000282D2
		internal static object DirectMaterializePlan(ODataEntityMaterializer materializer, MaterializerEntry entry, Type expectedEntryType)
		{
			materializer.entryValueMaterializationPolicy.Materialize(entry, expectedEntryType, true);
			return entry.ResolvedObject;
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0002A0E8 File Offset: 0x000282E8
		internal static object ShallowMaterializePlan(ODataEntityMaterializer materializer, MaterializerEntry entry, Type expectedEntryType)
		{
			materializer.entryValueMaterializationPolicy.Materialize(entry, expectedEntryType, false);
			return entry.ResolvedObject;
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0002A100 File Offset: 0x00028300
		[SuppressMessage("Microsoft.Maintainability", "CA1502", Justification = "cyclomatic complexity")]
		[SuppressMessage("Microsoft.Maintainability", "CA1506:MethodCoupledWithTooManyTypesFromDifferentNamespaces", Justification = "should refactor the method in the future.")]
		internal object ProjectionValueForPath(MaterializerEntry entry, Type expectedType, ProjectionPath path)
		{
			if (path.Count == 0 || (path.Count == 1 && path[0].Member == null))
			{
				if (!entry.EntityHasBeenResolved)
				{
					this.EntryValueMaterializationPolicy.Materialize(entry, expectedType, false);
				}
				return entry.ResolvedObject;
			}
			object obj = null;
			ICollection<ODataNestedResourceInfo> collection = entry.NestedResourceInfos;
			IEnumerable<ODataProperty> enumerable = entry.Entry.Properties;
			ClientEdmModel model = base.MaterializerContext.Model;
			for (int i = 0; i < path.Count; i++)
			{
				ProjectionPathSegment projectionPathSegment = path[i];
				if (projectionPathSegment.Member != null)
				{
					bool flag = i == path.Count - 1;
					string propertyName = projectionPathSegment.Member;
					expectedType = projectionPathSegment.SourceTypeAs ?? expectedType;
					ClientPropertyAnnotation property = model.GetClientTypeAnnotation(model.GetOrCreateEdmType(expectedType)).GetProperty(propertyName, UndeclaredPropertyBehavior.ThrowException);
					if (property.IsStreamLinkProperty)
					{
						StreamDescriptor streamDescriptor = entry.EntityDescriptor.StreamDescriptors.Where((StreamDescriptor sd) => sd.Name == propertyName).SingleOrDefault<StreamDescriptor>();
						if (streamDescriptor == null)
						{
							if (projectionPathSegment.SourceTypeAs != null)
							{
								obj = WebUtil.GetDefaultValue<DataServiceStreamLink>();
								break;
							}
							throw new InvalidOperationException(Strings.AtomMaterializer_PropertyMissing(propertyName));
						}
						else
						{
							obj = streamDescriptor.StreamLink;
						}
					}
					else
					{
						if (projectionPathSegment.SourceTypeAs != null && !collection.Any((ODataNestedResourceInfo p) => p.Name == propertyName) && !enumerable.Any((ODataProperty p) => p.Name == propertyName) && flag)
						{
							obj = WebUtil.GetDefaultValue(property.PropertyType);
							break;
						}
						ODataProperty odataProperty = enumerable.Where((ODataProperty p) => p.Name == propertyName).FirstOrDefault<ODataProperty>();
						ODataNestedResourceInfo odataNestedResourceInfo = ((odataProperty == null && collection != null) ? collection.Where((ODataNestedResourceInfo p) => p.Name == propertyName).FirstOrDefault<ODataNestedResourceInfo>() : null);
						if (odataNestedResourceInfo == null && odataProperty == null)
						{
							throw new InvalidOperationException(Strings.AtomMaterializer_PropertyMissing(propertyName));
						}
						if (odataNestedResourceInfo != null)
						{
							EntryValueMaterializationPolicy.ValidatePropertyMatch(property, odataNestedResourceInfo);
							MaterializerNavigationLink link = MaterializerNavigationLink.GetLink(odataNestedResourceInfo);
							if (link.Feed != null)
							{
								MaterializerFeed feed = MaterializerFeed.GetFeed(link.Feed);
								Type type = ClientTypeUtil.GetImplementationType(projectionPathSegment.ProjectionType, typeof(ICollection<>));
								if (type == null)
								{
									type = ClientTypeUtil.GetImplementationType(projectionPathSegment.ProjectionType, typeof(IEnumerable<>));
								}
								Type type2 = type.GetGenericArguments()[0];
								Type type3 = projectionPathSegment.ProjectionType;
								if (type3.IsInterface() || ClientTypeUtil.IsDataServiceCollection(type3))
								{
									type3 = typeof(Collection<>).MakeGenericType(new Type[] { type2 });
								}
								IEnumerable enumerable2 = (IEnumerable)Util.ActivatorCreateInstance(type3, new object[0]);
								ODataEntityMaterializer.MaterializeToList(this, enumerable2, type2, feed.Entries);
								if (ClientTypeUtil.IsDataServiceCollection(projectionPathSegment.ProjectionType))
								{
									Type dataServiceCollectionOfT = WebUtil.GetDataServiceCollectionOfT(new Type[] { type2 });
									enumerable2 = (IEnumerable)Util.ActivatorCreateInstance(dataServiceCollectionOfT, new object[]
									{
										enumerable2,
										TrackingMode.None
									});
								}
								ProjectionPlan projectionPlan = ODataEntityMaterializer.CreatePlanForShallowMaterialization(type2);
								this.EntryValueMaterializationPolicy.FoundNextLinkForCollection(enumerable2, feed.Feed.NextPageLink, projectionPlan);
								obj = enumerable2;
							}
							else if (link.Entry != null)
							{
								MaterializerEntry entry2 = link.Entry;
								if (flag)
								{
									if (entry2.Entry != null && !entry2.EntityHasBeenResolved)
									{
										this.EntryValueMaterializationPolicy.Materialize(entry2, property.PropertyType, false);
										if (!base.MaterializerContext.Context.DisableInstanceAnnotationMaterialization && entry2.ShouldUpdateFromPayload)
										{
											base.InstanceAnnotationMaterializationPolicy.SetInstanceAnnotations(entry2.Entry, entry2.ResolvedObject);
										}
									}
								}
								else
								{
									ODataEntityMaterializer.CheckEntryToAccessNotNull(entry2, propertyName);
								}
								if (!base.MaterializerContext.Context.DisableInstanceAnnotationMaterialization && entry2.ShouldUpdateFromPayload)
								{
									base.InstanceAnnotationMaterializationPolicy.SetInstanceAnnotations(propertyName, entry2.Entry, expectedType, entry.ResolvedObject);
								}
								enumerable = entry2.Properties;
								collection = entry2.NestedResourceInfos;
								obj = entry2.ResolvedObject;
								entry = entry2;
							}
						}
						else
						{
							if (odataProperty.Value is ODataStreamReferenceValue)
							{
								obj = null;
								collection = ODataMaterializer.EmptyLinks;
								enumerable = ODataMaterializer.EmptyProperties;
								goto IL_0579;
							}
							EntryValueMaterializationPolicy.ValidatePropertyMatch(property, odataProperty);
							if (ClientTypeUtil.TypeOrElementTypeIsEntity(property.PropertyType))
							{
								throw Error.InvalidOperation(Strings.AtomMaterializer_InvalidEntityType(property.EntityCollectionItemType ?? property.PropertyType));
							}
							if (property.IsPrimitiveOrEnumOrComplexCollection)
							{
								object obj2;
								if ((obj2 = obj) == null)
								{
									obj2 = entry.ResolvedObject ?? base.CollectionValueMaterializationPolicy.CreateNewInstance(property.EdmProperty.Type.Definition.ToEdmTypeReference(true), expectedType);
								}
								object obj3 = obj2;
								this.entryValueMaterializationPolicy.ApplyDataValue(model.GetClientTypeAnnotation(model.GetOrCreateEdmType(obj3.GetType())), odataProperty, obj3);
								collection = ODataMaterializer.EmptyLinks;
								enumerable = ODataMaterializer.EmptyProperties;
							}
							else if (odataProperty.Value is ODataEnumValue)
							{
								base.EnumValueMaterializationPolicy.MaterializeEnumTypeProperty(property.PropertyType, odataProperty);
								collection = ODataMaterializer.EmptyLinks;
								enumerable = ODataMaterializer.EmptyProperties;
							}
							else
							{
								if (odataProperty.Value == null && !ClientTypeUtil.CanAssignNull(property.NullablePropertyType))
								{
									throw new InvalidOperationException(Strings.AtomMaterializer_CannotAssignNull(odataProperty.Name, property.NullablePropertyType));
								}
								this.entryValueMaterializationPolicy.MaterializePrimitiveDataValue(property.NullablePropertyType, odataProperty);
								collection = ODataMaterializer.EmptyLinks;
								enumerable = ODataMaterializer.EmptyProperties;
							}
							obj = odataProperty.GetMaterializedValue();
							if (!base.MaterializerContext.Context.DisableInstanceAnnotationMaterialization)
							{
								base.InstanceAnnotationMaterializationPolicy.SetInstanceAnnotations(odataProperty, expectedType, entry.ResolvedObject);
							}
						}
					}
					expectedType = property.PropertyType;
				}
				IL_0579:;
			}
			return obj;
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0002A69A File Offset: 0x0002889A
		internal sealed override void ClearLog()
		{
			this.EntityTrackingAdapter.MaterializationLog.Clear();
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0002A6AC File Offset: 0x000288AC
		internal sealed override void ApplyLogToContext()
		{
			this.EntityTrackingAdapter.MaterializationLog.ApplyToContext();
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0002A6C0 File Offset: 0x000288C0
		internal void PropagateContinuation<T>(IEnumerable<T> from, DataServiceCollection<T> to)
		{
			DataServiceQueryContinuation dataServiceQueryContinuation;
			if (this.nextLinkTable.TryGetValue(from, out dataServiceQueryContinuation))
			{
				this.nextLinkTable.Add(to, dataServiceQueryContinuation);
				Util.SetNextLinkForCollection(to, dataServiceQueryContinuation);
			}
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0002A6F4 File Offset: 0x000288F4
		protected override bool ReadImplementation()
		{
			this.nextLinkTable.Clear();
			bool flag = this.CurrentFeed == null;
			if (!this.ReadNextFeedOrEntry())
			{
				return false;
			}
			if (this.CurrentEntry == null)
			{
				this.currentValue = null;
				return true;
			}
			MaterializerEntry entry = MaterializerEntry.GetEntry(this.CurrentEntry);
			entry.ResolvedObject = this.TargetInstance;
			this.currentValue = this.materializeEntryPlan.Run(this, this.CurrentEntry, base.ExpectedType);
			if (!base.MaterializerContext.Context.DisableInstanceAnnotationMaterialization)
			{
				if (flag && this.CurrentFeed != null && base.SetInstanceAnnotations != null)
				{
					base.SetInstanceAnnotations(base.InstanceAnnotationMaterializationPolicy.ConvertToClrInstanceAnnotations(this.CurrentFeed.InstanceAnnotations));
				}
				if ((this.CurrentEntry != null && entry.ResolvedObject == null) || entry.ShouldUpdateFromPayload)
				{
					base.InstanceAnnotationMaterializationPolicy.SetInstanceAnnotations(this.CurrentEntry, this.currentValue);
				}
			}
			return true;
		}

		// Token: 0x06000B20 RID: 2848
		protected abstract bool ReadNextFeedOrEntry();

		// Token: 0x06000B21 RID: 2849 RVA: 0x0002A7E0 File Offset: 0x000289E0
		[SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes", Justification = "Need to throw the type that the expression would throw with other providers")]
		private static void CheckEntryToAccessNotNull(MaterializerEntry entry, string name)
		{
			if (entry.Entry == null)
			{
				throw new NullReferenceException(Strings.AtomMaterializer_EntryToAccessIsNull(name));
			}
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0002A7F8 File Offset: 0x000289F8
		private static ProjectionPlan CreatePlan(QueryComponents queryComponents)
		{
			LambdaExpression projection = queryComponents.Projection;
			ProjectionPlan projectionPlan;
			if (projection == null)
			{
				projectionPlan = ODataEntityMaterializer.CreatePlanForDirectMaterialization(queryComponents.LastSegmentType);
			}
			else
			{
				projectionPlan = ProjectionPlanCompiler.CompilePlan(projection, queryComponents.NormalizerRewrites);
				projectionPlan.LastSegmentType = queryComponents.LastSegmentType;
			}
			return projectionPlan;
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0002A838 File Offset: 0x00028A38
		private static void MaterializeToList(ODataEntityMaterializer materializer, IEnumerable list, Type nestedExpectedType, IEnumerable<ODataResource> entries)
		{
			Action<object, object> addToCollectionDelegate = ClientTypeUtil.GetAddToCollectionDelegate(list.GetType());
			foreach (ODataResource odataResource in entries)
			{
				MaterializerEntry entry = MaterializerEntry.GetEntry(odataResource);
				if (!entry.EntityHasBeenResolved)
				{
					materializer.EntryValueMaterializationPolicy.Materialize(entry, nestedExpectedType, false);
				}
				addToCollectionDelegate(list, entry.ResolvedObject);
			}
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x0002A8B0 File Offset: 0x00028AB0
		private static MaterializerNavigationLink GetPropertyOrThrow(IEnumerable<ODataNestedResourceInfo> links, string propertyName)
		{
			ODataNestedResourceInfo odataNestedResourceInfo = null;
			if (links != null)
			{
				odataNestedResourceInfo = links.Where((ODataNestedResourceInfo p) => p.Name == propertyName).FirstOrDefault<ODataNestedResourceInfo>();
			}
			if (odataNestedResourceInfo == null)
			{
				throw new InvalidOperationException(Strings.AtomMaterializer_PropertyMissing(propertyName));
			}
			return MaterializerNavigationLink.GetLink(odataNestedResourceInfo);
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x0002A904 File Offset: 0x00028B04
		private void MergeLists(MaterializerEntry entry, ClientPropertyAnnotation property, IEnumerable list, Uri nextLink, ProjectionPlan plan)
		{
			object value = property.GetValue(entry.ResolvedObject);
			if (entry.ShouldUpdateFromPayload && property.NullablePropertyType == list.GetType() && (value == null || ODataEntityMaterializer.NeedToAssignCollectionDirectly(value)))
			{
				property.SetValue(entry.ResolvedObject, list, property.PropertyName, false);
				this.EntryValueMaterializationPolicy.FoundNextLinkForCollection(list, nextLink, plan);
				foreach (object obj in list)
				{
					this.EntityTrackingAdapter.MaterializationLog.AddedLink(entry, property.PropertyName, obj);
				}
				return;
			}
			this.EntryValueMaterializationPolicy.ApplyItemsToCollection(entry, property, list, nextLink, plan, false);
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0002A9D4 File Offset: 0x00028BD4
		private static bool NeedToAssignCollectionDirectly(object collection)
		{
			Type type = collection.GetType();
			PropertyInfo propertyInfo = type.GetPublicProperties(true).SingleOrDefault((PropertyInfo property) => property.Name == "Count");
			PropertyInfo propertyInfo2 = type.GetNonPublicProperties(true, false).SingleOrDefault((PropertyInfo property) => property.Name == "IsTracking");
			if (propertyInfo == null)
			{
				return false;
			}
			int num = (int)propertyInfo.GetValue(collection, null);
			if (propertyInfo2 == null)
			{
				return false;
			}
			bool flag = (bool)propertyInfo2.GetValue(collection, null);
			return num == 0 && !flag;
		}

		// Token: 0x0400062C RID: 1580
		protected object currentValue;

		// Token: 0x0400062D RID: 1581
		private readonly ProjectionPlan materializeEntryPlan;

		// Token: 0x0400062E RID: 1582
		private readonly EntryValueMaterializationPolicy entryValueMaterializationPolicy;
	}
}
