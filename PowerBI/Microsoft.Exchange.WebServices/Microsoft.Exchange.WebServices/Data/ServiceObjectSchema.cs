using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020001AB RID: 427
	[EditorBrowsable(1)]
	public abstract class ServiceObjectSchema : IEnumerable<PropertyDefinition>, IEnumerable
	{
		// Token: 0x06001478 RID: 5240 RVA: 0x000376E0 File Offset: 0x000366E0
		internal static void ForeachPublicStaticPropertyFieldInType(Type type, ServiceObjectSchema.PropertyFieldInfoDelegate propFieldDelegate)
		{
			FieldInfo[] fields = type.GetFields(26);
			foreach (FieldInfo fieldInfo in fields)
			{
				if (fieldInfo.FieldType == typeof(PropertyDefinition) || fieldInfo.FieldType.IsSubclassOf(typeof(PropertyDefinition)))
				{
					PropertyDefinition propertyDefinition = (PropertyDefinition)fieldInfo.GetValue(null);
					propFieldDelegate(propertyDefinition, fieldInfo);
				}
			}
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x00037810 File Offset: 0x00036810
		internal static void AddSchemaPropertiesToDictionary(Type type, Dictionary<string, PropertyDefinitionBase> propDefDictionary)
		{
			ServiceObjectSchema.ForeachPublicStaticPropertyFieldInType(type, delegate(PropertyDefinition propertyDefinition, FieldInfo fieldInfo)
			{
				if (!string.IsNullOrEmpty(propertyDefinition.Uri))
				{
					PropertyDefinitionBase propertyDefinitionBase;
					if (propDefDictionary.TryGetValue(propertyDefinition.Uri, ref propertyDefinitionBase))
					{
						EwsUtilities.Assert(propertyDefinitionBase == propertyDefinition, "Schema.allSchemaProperties.delegate", string.Format("There are at least two distinct property definitions with the following URI: {0}", propertyDefinition.Uri));
						return;
					}
					propDefDictionary.Add(propertyDefinition.Uri, propertyDefinition);
					List<PropertyDefinition> associatedInternalProperties = propertyDefinition.GetAssociatedInternalProperties();
					foreach (PropertyDefinition propertyDefinition2 in associatedInternalProperties)
					{
						propDefDictionary.Add(propertyDefinition2.Uri, propertyDefinition2);
					}
				}
			});
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x00037858 File Offset: 0x00036858
		private static void AddSchemaPropertyNamesToDictionary(Type type, Dictionary<PropertyDefinition, string> propertyNameDictionary)
		{
			ServiceObjectSchema.ForeachPublicStaticPropertyFieldInType(type, delegate(PropertyDefinition propertyDefinition, FieldInfo fieldInfo)
			{
				propertyNameDictionary.Add(propertyDefinition, fieldInfo.Name);
			});
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x00037884 File Offset: 0x00036884
		internal ServiceObjectSchema()
		{
			this.RegisterProperties();
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x000378D4 File Offset: 0x000368D4
		internal static PropertyDefinitionBase FindPropertyDefinition(string uri)
		{
			return ServiceObjectSchema.allSchemaProperties.Member[uri];
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x000378F4 File Offset: 0x000368F4
		internal static void InitializeSchemaPropertyNames()
		{
			lock (ServiceObjectSchema.lockObject)
			{
				foreach (Type type in ServiceObjectSchema.allSchemaTypes.Member)
				{
					ServiceObjectSchema.ForeachPublicStaticPropertyFieldInType(type, delegate(PropertyDefinition propDef, FieldInfo fieldInfo)
					{
						propDef.Name = fieldInfo.Name;
					});
				}
			}
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x00037988 File Offset: 0x00036988
		private void RegisterProperty(PropertyDefinition property, bool isInternal)
		{
			this.properties.Add(property.XmlElementName, property);
			if (!isInternal)
			{
				this.visibleProperties.Add(property);
			}
			if (!property.HasFlag(PropertyDefinitionFlags.MustBeExplicitlyLoaded))
			{
				this.firstClassProperties.Add(property);
			}
			if (property.HasFlag(PropertyDefinitionFlags.CanFind))
			{
				this.firstClassSummaryProperties.Add(property);
			}
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x000379E2 File Offset: 0x000369E2
		internal void RegisterProperty(PropertyDefinition property)
		{
			this.RegisterProperty(property, false);
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x000379EC File Offset: 0x000369EC
		internal void RegisterInternalProperty(PropertyDefinition property)
		{
			this.RegisterProperty(property, true);
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x000379F6 File Offset: 0x000369F6
		internal void RegisterIndexedProperty(IndexedPropertyDefinition indexedProperty)
		{
			this.indexedProperties.Add(indexedProperty);
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x00037A04 File Offset: 0x00036A04
		internal virtual void RegisterProperties()
		{
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001483 RID: 5251 RVA: 0x00037A06 File Offset: 0x00036A06
		internal List<PropertyDefinition> FirstClassProperties
		{
			get
			{
				return this.firstClassProperties;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001484 RID: 5252 RVA: 0x00037A0E File Offset: 0x00036A0E
		internal List<PropertyDefinition> FirstClassSummaryProperties
		{
			get
			{
				return this.firstClassSummaryProperties;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001485 RID: 5253 RVA: 0x00037A16 File Offset: 0x00036A16
		internal List<IndexedPropertyDefinition> IndexedProperties
		{
			get
			{
				return this.indexedProperties;
			}
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x00037A1E File Offset: 0x00036A1E
		internal bool TryGetPropertyDefinition(string xmlElementName, out PropertyDefinition propertyDefinition)
		{
			return this.properties.TryGetValue(xmlElementName, ref propertyDefinition);
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x00037A2D File Offset: 0x00036A2D
		public IEnumerator<PropertyDefinition> GetEnumerator()
		{
			return this.visibleProperties.GetEnumerator();
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x00037A3F File Offset: 0x00036A3F
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.visibleProperties.GetEnumerator();
		}

		// Token: 0x04000A10 RID: 2576
		private static object lockObject = new object();

		// Token: 0x04000A11 RID: 2577
		private static LazyMember<List<Type>> allSchemaTypes = new LazyMember<List<Type>>(delegate
		{
			List<Type> list = new List<Type>();
			list.Add(typeof(AppointmentSchema));
			list.Add(typeof(CalendarResponseObjectSchema));
			list.Add(typeof(CancelMeetingMessageSchema));
			list.Add(typeof(ContactGroupSchema));
			list.Add(typeof(ContactSchema));
			list.Add(typeof(ConversationSchema));
			list.Add(typeof(EmailMessageSchema));
			list.Add(typeof(FolderSchema));
			list.Add(typeof(ItemSchema));
			list.Add(typeof(MeetingMessageSchema));
			list.Add(typeof(MeetingRequestSchema));
			list.Add(typeof(MeetingCancellationSchema));
			list.Add(typeof(MeetingResponseSchema));
			list.Add(typeof(PostItemSchema));
			list.Add(typeof(PostReplySchema));
			list.Add(typeof(ResponseMessageSchema));
			list.Add(typeof(ResponseObjectSchema));
			list.Add(typeof(ServiceObjectSchema));
			list.Add(typeof(SearchFolderSchema));
			list.Add(typeof(TaskSchema));
			return list;
		});

		// Token: 0x04000A12 RID: 2578
		private static LazyMember<Dictionary<string, PropertyDefinitionBase>> allSchemaProperties = new LazyMember<Dictionary<string, PropertyDefinitionBase>>(delegate
		{
			Dictionary<string, PropertyDefinitionBase> dictionary = new Dictionary<string, PropertyDefinitionBase>();
			foreach (Type type in ServiceObjectSchema.allSchemaTypes.Member)
			{
				ServiceObjectSchema.AddSchemaPropertiesToDictionary(type, dictionary);
			}
			return dictionary;
		});

		// Token: 0x04000A13 RID: 2579
		public static readonly PropertyDefinition ExtendedProperties = new ComplexPropertyDefinition<ExtendedPropertyCollection>("ExtendedProperty", PropertyDefinitionFlags.AutoInstantiateOnRead | PropertyDefinitionFlags.ReuseInstance | PropertyDefinitionFlags.CanSet | PropertyDefinitionFlags.CanUpdate, ExchangeVersion.Exchange2007_SP1, () => new ExtendedPropertyCollection());

		// Token: 0x04000A14 RID: 2580
		private Dictionary<string, PropertyDefinition> properties = new Dictionary<string, PropertyDefinition>();

		// Token: 0x04000A15 RID: 2581
		private List<PropertyDefinition> visibleProperties = new List<PropertyDefinition>();

		// Token: 0x04000A16 RID: 2582
		private List<PropertyDefinition> firstClassProperties = new List<PropertyDefinition>();

		// Token: 0x04000A17 RID: 2583
		private List<PropertyDefinition> firstClassSummaryProperties = new List<PropertyDefinition>();

		// Token: 0x04000A18 RID: 2584
		private List<IndexedPropertyDefinition> indexedProperties = new List<IndexedPropertyDefinition>();

		// Token: 0x020001AC RID: 428
		// (Invoke) Token: 0x0600148F RID: 5263
		internal delegate void PropertyFieldInfoDelegate(PropertyDefinition propertyDefinition, FieldInfo fieldInfo);
	}
}
