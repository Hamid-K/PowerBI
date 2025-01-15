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
	// Token: 0x0200008C RID: 140
	public sealed class ObjectTranslationCollection : MetadataObjectCollection<ObjectTranslation, Culture>
	{
		// Token: 0x06000870 RID: 2160 RVA: 0x00047E90 File Offset: 0x00046090
		internal ObjectTranslationCollection(Culture parent)
			: base(ObjectType.ObjectTranslation, parent, false)
		{
			this.body = new ObjectTranslationCollection.ObjectCollectionBody(this);
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x00047EA8 File Offset: 0x000460A8
		private protected override void CompareWith(MetadataObjectCollection<ObjectTranslation, Culture> other, CopyContext context, IList<ObjectTranslation> removedItems, IList<ObjectTranslation> addedItems, IList<KeyValuePair<ObjectTranslation, ObjectTranslation>> matchedItems)
		{
			Utils.CompareLinkedObjectCollections<ObjectTranslation>(this, other, context, false, removedItems, addedItems, matchedItems);
		}

		// Token: 0x1700021F RID: 543
		public ObjectTranslation this[MetadataObject translatedObj, TranslatedProperty translatedProp]
		{
			get
			{
				ObjectTranslationCollection.TranslatedEntity translatedEntity = new ObjectTranslationCollection.TranslatedEntity(translatedObj, translatedProp);
				if (this.body.MapByObjectAndProperty.ContainsKey(translatedEntity))
				{
					return this.body.MapByObjectAndProperty[translatedEntity];
				}
				return null;
			}
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x00047EF4 File Offset: 0x000460F4
		public void SetTranslation(MetadataObject translatedObj, TranslatedProperty translatedProp, string value)
		{
			ObjectTranslationCollection.TranslatedEntity translatedEntity = new ObjectTranslationCollection.TranslatedEntity(translatedObj, translatedProp);
			if (string.IsNullOrEmpty(value))
			{
				if (this.body.MapByObjectAndProperty.ContainsKey(translatedEntity))
				{
					base.Remove(this.body.MapByObjectAndProperty[translatedEntity]);
					return;
				}
			}
			else
			{
				if (this.body.MapByObjectAndProperty.ContainsKey(translatedEntity))
				{
					this.body.MapByObjectAndProperty[translatedEntity].Value = value;
					return;
				}
				base.Add(new ObjectTranslation
				{
					Object = translatedObj,
					Property = translatedProp,
					Value = value
				});
			}
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x00047F88 File Offset: 0x00046188
		internal override void OnItemAdding(ObjectTranslation item)
		{
			base.OnItemAdding(item);
			if (item.Object == null && item.body.ObjectID.IsResolved)
			{
				throw new ArgumentException(TomSR.Exception_TranslatedObjectNull);
			}
			if (item.Value == null)
			{
				throw new ArgumentNullException("item.Value");
			}
			if (item.body.ObjectID.IsResolved && this.body.MapByObjectAndProperty.ContainsKey(new ObjectTranslationCollection.TranslatedEntity(item.Object, item._ObjectID, item.Property)))
			{
				throw new ArgumentException(TomSR.Exception_ObjectTranslationAlreadyContainsTranslation(item.Property.ToString(), item.Object.GetFormattedObjectPath()));
			}
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x00048039 File Offset: 0x00046239
		internal override void OnItemAdded(ObjectTranslation item)
		{
			base.OnItemAdded(item);
			this.AddToIndex(item);
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x00048049 File Offset: 0x00046249
		internal override void OnItemRemoved(ObjectTranslation item)
		{
			base.OnItemRemoved(item);
			if (item.body.ObjectID.IsResolved)
			{
				this.RemoveFromIndex(item);
			}
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0004806B File Offset: 0x0004626B
		internal void AddToIndex(ObjectTranslation item)
		{
			this.body.AddToIndex(item);
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x00048079 File Offset: 0x00046279
		internal void RemoveFromIndex(ObjectTranslation item)
		{
			this.body.RemoveFromIndex(item);
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x00048087 File Offset: 0x00046287
		internal void RebuildIndex()
		{
			this.body.RebuildIndex();
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x0600087A RID: 2170 RVA: 0x00048094 File Offset: 0x00046294
		// (set) Token: 0x0600087B RID: 2171 RVA: 0x0004809C File Offset: 0x0004629C
		internal override MetadataObjectCollection<ObjectTranslation, Culture>.ObjectCollectionBody Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = (ObjectTranslationCollection.ObjectCollectionBody)value;
			}
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x000480AA File Offset: 0x000462AA
		internal override MetadataObjectCollection<ObjectTranslation, Culture>.ObjectCollectionBody CreateBody()
		{
			return new ObjectTranslationCollection.ObjectCollectionBody(this);
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x000480B4 File Offset: 0x000462B4
		internal JsonObject SerializeToJsonObject()
		{
			SparseSerializerSettings sparseSerializerSettings = new SparseSerializerSettings
			{
				SerializeObject = new SparseSerializerSettings.SerializeObjectDelegate(this.SerializeTranslationToJsonObject)
			};
			return Utils.SerializeSparseCollection((from tr in this
				where tr.Object != null
				select tr.Object).Distinct<MetadataObject>(), sparseSerializerSettings);
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x00048130 File Offset: 0x00046330
		internal void DeserializeFromJsonObject(JObject jObj, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			JsonObjectTreeReaderSettings jsonObjectTreeReaderSettings = new JsonObjectTreeReaderSettings(new JsonObjectTreeReaderSettings.ReadObjectMethod(this.ReadObjectTranslation), mode, dbCompatibilityLevel)
			{
				ReadObjectFilter = new Predicate<ObjectType>(ObjectTreeHelper.HasTranslatableDescendants),
				ReadCollectionFilter = new Predicate<ObjectType>(ObjectTreeHelper.HasTranslatableDescendants)
			};
			foreach (JProperty jproperty in jObj.Properties())
			{
				if (!(jproperty.Name == "model"))
				{
					throw JsonSerializationUtil.CreateException(TomSR.Exception_UnexpectedJsonProperty(jproperty.Name), jproperty, null);
				}
				jproperty.Value.VerifyTokenType(1);
				JsonObjectTreeReader.ReadModel((JObject)jproperty.Value, new ObjectPath(Array.Empty<KeyValuePair<ObjectType, string>>()), jsonObjectTreeReaderSettings);
			}
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x000481FC File Offset: 0x000463FC
		private void SerializeTranslationToJsonObject(MetadataObject translatedObject, JsonObject result, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			foreach (TranslatablePropertyInfo translatablePropertyInfo in ObjectTreeHelper.GetTranslatedProperties(translatedObject.ObjectType, mode, dbCompatibilityLevel))
			{
				ObjectTranslation objectTranslation = this[translatedObject, translatablePropertyInfo.Property];
				if (objectTranslation != null)
				{
					result[JsonPropertyName.Misc.GetTranslatedPropertyName(translatablePropertyInfo.Property), TomPropCategory.Regular, 0, false] = objectTranslation.Value;
				}
			}
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x00048278 File Offset: 0x00046478
		private void ReadObjectTranslation(JObject jObj, ObjectType type, CompatibilityMode mode, int dbCompatibilityLevel, ObjectPath currentPath)
		{
			foreach (TranslatablePropertyInfo translatablePropertyInfo in ObjectTreeHelper.GetTranslatedProperties(type, mode, dbCompatibilityLevel))
			{
				string translatedPropertyName = JsonPropertyName.Misc.GetTranslatedPropertyName(translatablePropertyInfo.Property);
				if (jObj.ContainsProperty(translatedPropertyName))
				{
					string text = JsonPropertyHelper.ConvertJsonValueToString(jObj[translatedPropertyName]);
					base.Add(new ObjectTranslation
					{
						Value = text,
						Property = translatablePropertyInfo.Property,
						body = 
						{
							ObjectID = 
							{
								Path = currentPath.Clone()
							}
						}
					});
				}
			}
		}

		// Token: 0x04000147 RID: 327
		private ObjectTranslationCollection.ObjectCollectionBody body;

		// Token: 0x0200029A RID: 666
		internal class TranslatedEntity : IEquatable<ObjectTranslationCollection.TranslatedEntity>
		{
			// Token: 0x060021B5 RID: 8629 RVA: 0x000DAA20 File Offset: 0x000D8C20
			public TranslatedEntity(MetadataObject translatedObj, ObjectId translatedObjectId, TranslatedProperty translatedProp)
			{
				this.Object = translatedObj;
				this.ObjectId = translatedObjectId;
				this.Property = translatedProp;
			}

			// Token: 0x060021B6 RID: 8630 RVA: 0x000DAA3D File Offset: 0x000D8C3D
			public TranslatedEntity(MetadataObject translatedObj, TranslatedProperty translatedProp)
			{
				this.Object = translatedObj;
				this.ObjectId = translatedObj.Id;
				this.Property = translatedProp;
			}

			// Token: 0x1700071E RID: 1822
			// (get) Token: 0x060021B7 RID: 8631 RVA: 0x000DAA5F File Offset: 0x000D8C5F
			// (set) Token: 0x060021B8 RID: 8632 RVA: 0x000DAA67 File Offset: 0x000D8C67
			public MetadataObject Object { get; private set; }

			// Token: 0x1700071F RID: 1823
			// (get) Token: 0x060021B9 RID: 8633 RVA: 0x000DAA70 File Offset: 0x000D8C70
			// (set) Token: 0x060021BA RID: 8634 RVA: 0x000DAA78 File Offset: 0x000D8C78
			public ObjectId ObjectId { get; private set; }

			// Token: 0x17000720 RID: 1824
			// (get) Token: 0x060021BB RID: 8635 RVA: 0x000DAA81 File Offset: 0x000D8C81
			// (set) Token: 0x060021BC RID: 8636 RVA: 0x000DAA89 File Offset: 0x000D8C89
			public TranslatedProperty Property { get; private set; }

			// Token: 0x060021BD RID: 8637 RVA: 0x000DAA94 File Offset: 0x000D8C94
			public override int GetHashCode()
			{
				return ((this.Object != null) ? this.Object.GetHashCode() : 0) ^ this.ObjectId.GetHashCode() ^ this.Property.GetHashCode();
			}

			// Token: 0x060021BE RID: 8638 RVA: 0x000DAAE4 File Offset: 0x000D8CE4
			public override bool Equals(object obj)
			{
				ObjectTranslationCollection.TranslatedEntity translatedEntity = obj as ObjectTranslationCollection.TranslatedEntity;
				return translatedEntity != null && this.Equals(translatedEntity);
			}

			// Token: 0x060021BF RID: 8639 RVA: 0x000DAB04 File Offset: 0x000D8D04
			public bool Equals(ObjectTranslationCollection.TranslatedEntity other)
			{
				return this.Object == other.Object && this.ObjectId == other.ObjectId && this.Property == other.Property;
			}
		}

		// Token: 0x0200029B RID: 667
		internal new class ObjectCollectionBody : MetadataObjectCollection<ObjectTranslation, Culture>.ObjectCollectionBody
		{
			// Token: 0x17000721 RID: 1825
			// (get) Token: 0x060021C0 RID: 8640 RVA: 0x000DAB37 File Offset: 0x000D8D37
			// (set) Token: 0x060021C1 RID: 8641 RVA: 0x000DAB3F File Offset: 0x000D8D3F
			internal Dictionary<ObjectTranslationCollection.TranslatedEntity, ObjectTranslation> MapByObjectAndProperty { get; private set; }

			// Token: 0x060021C2 RID: 8642 RVA: 0x000DAB48 File Offset: 0x000D8D48
			public ObjectCollectionBody(ObjectTranslationCollection owner)
				: base(owner)
			{
				this.MapByObjectAndProperty = new Dictionary<ObjectTranslationCollection.TranslatedEntity, ObjectTranslation>();
			}

			// Token: 0x060021C3 RID: 8643 RVA: 0x000DAB5C File Offset: 0x000D8D5C
			internal void CopyFrom(ObjectTranslationCollection.ObjectCollectionBody other, CopyContext context)
			{
				base.CopyFrom(other, context);
				this.MapByObjectAndProperty.Clear();
				foreach (KeyValuePair<ObjectTranslationCollection.TranslatedEntity, ObjectTranslation> keyValuePair in other.MapByObjectAndProperty)
				{
					this.MapByObjectAndProperty.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}

			// Token: 0x060021C4 RID: 8644 RVA: 0x000DABD4 File Offset: 0x000D8DD4
			internal override void CopyFrom(MetadataObjectCollection<ObjectTranslation, Culture>.ObjectCollectionBody other, CopyContext context)
			{
				this.CopyFrom((ObjectTranslationCollection.ObjectCollectionBody)other, context);
			}

			// Token: 0x060021C5 RID: 8645 RVA: 0x000DABE4 File Offset: 0x000D8DE4
			internal void AddToIndex(ObjectTranslation item)
			{
				if (item.body.ObjectID.IsResolved)
				{
					ObjectTranslationCollection.TranslatedEntity translatedEntity = new ObjectTranslationCollection.TranslatedEntity(item.Object, item._ObjectID, item.Property);
					this.MapByObjectAndProperty[translatedEntity] = item;
				}
			}

			// Token: 0x060021C6 RID: 8646 RVA: 0x000DAC28 File Offset: 0x000D8E28
			internal void RemoveFromIndex(ObjectTranslation item)
			{
				ObjectTranslationCollection.TranslatedEntity translatedEntity = new ObjectTranslationCollection.TranslatedEntity(item.Object, item._ObjectID, item.Property);
				if (this.MapByObjectAndProperty.ContainsKey(translatedEntity))
				{
					this.MapByObjectAndProperty.Remove(translatedEntity);
				}
			}

			// Token: 0x060021C7 RID: 8647 RVA: 0x000DAC68 File Offset: 0x000D8E68
			internal void RebuildIndex()
			{
				this.MapByObjectAndProperty.Clear();
				foreach (ObjectTranslation objectTranslation in base.Owner)
				{
					this.AddToIndex(objectTranslation);
				}
			}
		}
	}
}
