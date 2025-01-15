using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Modeling;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000016 RID: 22
	internal sealed class PersistenceHelper : IRIFObjectCreator
	{
		// Token: 0x06000064 RID: 100 RVA: 0x00002E3E File Offset: 0x0000103E
		internal PersistenceHelper()
		{
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002E5C File Offset: 0x0000105C
		internal NameTable NameTable
		{
			get
			{
				return this.m_nameTable;
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002E64 File Offset: 0x00001064
		IPersistable IRIFObjectCreator.CreateRIFObject(ObjectType objectType, ref IntermediateFormatReader context)
		{
			if (objectType == ObjectType.Null)
			{
				return null;
			}
			IPersistable persistable;
			switch (objectType)
			{
			case ObjectType.DsvColumn:
				persistable = PersistenceHelper.RefHelper.CreateForDeserialization(DsvColumn.FromBinary());
				goto IL_02A1;
			case ObjectType.DsvColumnCollection:
				persistable = DsvColumnCollection.FromBinary();
				goto IL_02A1;
			case ObjectType.DsvTable:
				persistable = PersistenceHelper.RefHelper.CreateForDeserialization(DsvTable.FromBinary());
				goto IL_02A1;
			case ObjectType.DsvRelation:
				persistable = PersistenceHelper.RefHelper.CreateForDeserialization(DsvRelation.FromBinary());
				goto IL_02A1;
			case ObjectType.DsvForeignKeyConstraint:
				persistable = PersistenceHelper.RefHelper.CreateForDeserialization(DsvForeignKeyConstraint.FromBinary());
				goto IL_02A1;
			case ObjectType.DsvUniqueConstraint:
				persistable = PersistenceHelper.RefHelper.CreateForDeserialization(DsvUniqueConstraint.FromBinary());
				goto IL_02A1;
			case ObjectType.DsvConstraintCollection:
				persistable = DsvConstraintCollection.FromBinary();
				goto IL_02A1;
			case ObjectType.DsvRelationCollection:
				persistable = DsvRelationCollection.FromBinary();
				goto IL_02A1;
			case ObjectType.DsvTableCollection:
				persistable = DsvTableCollection.FromBinary();
				goto IL_02A1;
			case ObjectType.DataSourceView:
				persistable = DataSourceView.FromBinary();
				goto IL_02A1;
			case ObjectType.DataSourceViewSchema:
				persistable = DataSourceView.CreateDsvSchemaPersistable();
				goto IL_02A1;
			case ObjectType.Perspective:
				persistable = PersistenceHelper.RefHelper.CreateForDeserialization(new Perspective());
				goto IL_02A1;
			case ObjectType.TableBinding:
				persistable = new TableBinding();
				goto IL_02A1;
			case ObjectType.ColumnBinding:
				persistable = new ColumnBinding();
				goto IL_02A1;
			case ObjectType.RelationBinding:
				persistable = new RelationBinding();
				goto IL_02A1;
			case ObjectType.Expression:
				persistable = new Expression();
				goto IL_02A1;
			case ObjectType.ResultTypePersistable:
				persistable = new Expression.ResultTypePersistable();
				goto IL_02A1;
			case ObjectType.FunctionNode:
				persistable = new FunctionNode();
				goto IL_02A1;
			case ObjectType.AttributeRefNode:
				persistable = new AttributeRefNode();
				goto IL_02A1;
			case ObjectType.EntityRefNode:
				persistable = new EntityRefNode();
				goto IL_02A1;
			case ObjectType.LiteralNode:
				persistable = new LiteralNode();
				goto IL_02A1;
			case ObjectType.NullNode:
				persistable = new NullNode();
				goto IL_02A1;
			case ObjectType.RolePathItem:
				persistable = new RolePathItem();
				goto IL_02A1;
			case ObjectType.InheritancePathItem:
				persistable = new InheritancePathItem();
				goto IL_02A1;
			case ObjectType.ModelRole:
				persistable = PersistenceHelper.RefHelper.CreateForDeserialization(new ModelRole());
				goto IL_02A1;
			case ObjectType.CustomProperty:
				persistable = new CustomProperty();
				goto IL_02A1;
			case ObjectType.ModelFieldFolder:
				persistable = PersistenceHelper.RefHelper.CreateForDeserialization(new ModelFieldFolder());
				goto IL_02A1;
			case ObjectType.ModelAttribute:
				persistable = PersistenceHelper.RefHelper.CreateForDeserialization(new ModelAttribute());
				goto IL_02A1;
			case ObjectType.AttributeReference:
				persistable = PersistenceHelper.RefHelper.CreateForDeserialization(new AttributeReference());
				goto IL_02A1;
			case ObjectType.SortAttribute:
				persistable = PersistenceHelper.RefHelper.CreateForDeserialization(new SortAttribute());
				goto IL_02A1;
			case ObjectType.EntityInheritance:
				persistable = new EntityInheritance();
				goto IL_02A1;
			case ObjectType.ModelEntity:
				persistable = PersistenceHelper.RefHelper.CreateForDeserialization(new ModelEntity());
				goto IL_02A1;
			case ObjectType.ModelEntityFolder:
				persistable = PersistenceHelper.RefHelper.CreateForDeserialization(new ModelEntityFolder());
				goto IL_02A1;
			case ObjectType.SemanticModel:
				persistable = new SemanticModel();
				goto IL_02A1;
			}
			throw new InternalModelingException("Unexpected object type: " + objectType.ToString());
			IL_02A1:
			persistable.Deserialize(context);
			return persistable;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003120 File Offset: 0x00001320
		internal static bool ReadQName(ref IntermediateFormatReader reader, ref QName qName)
		{
			MemberName memberName = reader.CurrentMember.MemberName;
			if (memberName != MemberName.QName_Name)
			{
				if (memberName != MemberName.QName_Namespace)
				{
					return false;
				}
				qName.Namespace = reader.PersistenceHelper.NameTable.Add(reader.ReadString());
			}
			else
			{
				qName.Name = reader.PersistenceHelper.NameTable.Add(reader.ReadString());
			}
			return true;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003184 File Offset: 0x00001384
		internal static bool WriteQName(ref IntermediateFormatWriter writer, QName qName)
		{
			MemberName memberName = writer.CurrentMember.MemberName;
			if (memberName != MemberName.QName_Name)
			{
				if (memberName != MemberName.QName_Namespace)
				{
					return false;
				}
				writer.Write(qName.Namespace);
			}
			else
			{
				writer.Write(qName.Name);
			}
			return true;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000031CA File Offset: 0x000013CA
		internal static void DeclareQName(List<MemberInfo> members)
		{
			members.Add(new MemberInfo(MemberName.QName_Name, Token.String));
			members.Add(new MemberInfo(MemberName.QName_Namespace, Token.String));
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000031F0 File Offset: 0x000013F0
		internal static T ReadModelingObject<T>(ref IntermediateFormatReader reader) where T : ModelingObject, IPersistable
		{
			PersistenceHelper.RefHelper refHelper = (PersistenceHelper.RefHelper)reader.ReadRIFObject();
			if (refHelper != null)
			{
				return (T)((object)refHelper.Object);
			}
			return default(T);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003224 File Offset: 0x00001424
		internal static void ReadListOfModelingObjects<T>(ref IntermediateFormatReader reader, IList<T> items) where T : ModelingObject, IPersistable
		{
			reader.ReadListOfRIFObjects<PersistenceHelper.RefHelper>(delegate(PersistenceHelper.RefHelper refHelper)
			{
				T t = (T)((object)refHelper.Object);
				using (t.AllowWriteOperations())
				{
					items.Add(t);
				}
			});
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003250 File Offset: 0x00001450
		internal static T[] ReadDsvItemArray<T>(ref IntermediateFormatReader reader) where T : DsvItem
		{
			return reader.ReadArrayOfRIFObjects<PersistenceHelper.RefHelper, T>((PersistenceHelper.RefHelper refHelper) => (T)((object)refHelper.Object));
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003278 File Offset: 0x00001478
		internal static void WriteModelingObject<T>(ref IntermediateFormatWriter writer, T modelingObject) where T : ModelingObject, IPersistable
		{
			PersistenceHelper.RefHelper refHelper = null;
			if (modelingObject != null)
			{
				refHelper = PersistenceHelper.RefHelper.CreateForSerialization(writer.PersistenceHelper.GetOrCreatePersistenceID(modelingObject), modelingObject);
			}
			writer.Write(refHelper);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000032B4 File Offset: 0x000014B4
		internal static void WriteListOfModelingObjects<T>(ref IntermediateFormatWriter writer, IList<T> items) where T : ModelingObject, IPersistable
		{
			List<PersistenceHelper.RefHelper> list = null;
			if (items != null)
			{
				list = new List<PersistenceHelper.RefHelper>(items.Count);
				for (int i = 0; i < items.Count; i++)
				{
					IPersistable persistable = items[i];
					list.Add(PersistenceHelper.RefHelper.CreateForSerialization(writer.PersistenceHelper.GetOrCreatePersistenceID(persistable), persistable));
				}
			}
			writer.WriteRIFList<PersistenceHelper.RefHelper>(list);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003310 File Offset: 0x00001510
		internal static void WriteDsvItemArray<T>(ref IntermediateFormatWriter writer, T[] items) where T : DsvItem
		{
			PersistenceHelper.RefHelper[] array = null;
			if (items != null)
			{
				array = new PersistenceHelper.RefHelper[items.Length];
				for (int i = 0; i < items.Length; i++)
				{
					IPersistable persistable = items[i];
					array[i] = PersistenceHelper.RefHelper.CreateForSerialization(writer.PersistenceHelper.GetOrCreatePersistenceID(persistable), persistable);
				}
			}
			IPersistable[] array2 = array;
			writer.Write(array2);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003364 File Offset: 0x00001564
		internal static T ReadModelingObjectReference<T>(ref IntermediateFormatReader reader, IPersistable refOwner) where T : ModelingObject, IPersistable
		{
			return PersistenceHelper.ReadReferencableItemReference<T>(ref reader, refOwner);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000336D File Offset: 0x0000156D
		internal static IQueryEntityInternal ReadIQueryEntityReference(ref IntermediateFormatReader reader, IPersistable refOwner)
		{
			return PersistenceHelper.ReadReferencableItemReference<IQueryEntityInternal>(ref reader, refOwner);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003376 File Offset: 0x00001576
		internal static IQueryAttributeInternal ReadIQueryAttributeReference(ref IntermediateFormatReader reader, IPersistable refOwner)
		{
			return PersistenceHelper.ReadReferencableItemReference<IQueryAttributeInternal>(ref reader, refOwner);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000337F File Offset: 0x0000157F
		internal static T ReadDsvItemReference<T>(ref IntermediateFormatReader reader, IPersistable refOwner) where T : DsvItem
		{
			return PersistenceHelper.ReadReferencableItemReference<T>(ref reader, refOwner);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003388 File Offset: 0x00001588
		internal static T ResolveModelingObjectReference<T>(IReferenceable referencable) where T : ModelingObject, IPersistable
		{
			return PersistenceHelper.ResolveReferencableItemReference<T>(referencable);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003390 File Offset: 0x00001590
		internal static IQueryEntityInternal ResolveIQueryEntityReference(IReferenceable referencable)
		{
			return PersistenceHelper.ResolveReferencableItemReference<IQueryEntityInternal>(referencable);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003398 File Offset: 0x00001598
		internal static IQueryAttributeInternal ResolveIQueryAttributeReference(IReferenceable referencable)
		{
			return PersistenceHelper.ResolveReferencableItemReference<IQueryAttributeInternal>(referencable);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000033A0 File Offset: 0x000015A0
		internal static T ResolveDsvItemReference<T>(IReferenceable referencable) where T : DsvItem
		{
			return PersistenceHelper.ResolveReferencableItemReference<T>(referencable);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000033A8 File Offset: 0x000015A8
		internal static void WriteModelingObjectReference<T>(ref IntermediateFormatWriter writer, T modelingObject) where T : ModelingObject, IPersistable
		{
			PersistenceHelper.WriteReferencableItemReference(ref writer, modelingObject);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000033B6 File Offset: 0x000015B6
		internal static void WriteIQueryEntityReference(ref IntermediateFormatWriter writer, IQueryEntity modelingObject)
		{
			if (modelingObject != null && !(modelingObject is IQueryEntityInternal))
			{
				throw new InternalModelingException("modelingObject is not an expected implementation of IQueryEntityInternal.");
			}
			PersistenceHelper.WriteReferencableItemReference(ref writer, (IQueryEntityInternal)modelingObject);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000033DA File Offset: 0x000015DA
		internal static void WriteIQueryAttributeReference(ref IntermediateFormatWriter writer, IQueryAttribute modelingObject)
		{
			if (modelingObject != null && !(modelingObject is IQueryAttributeInternal))
			{
				throw new InternalModelingException("modelingObject is not an expected implementation of IQueryAttributeInternal.");
			}
			PersistenceHelper.WriteReferencableItemReference(ref writer, (IQueryAttributeInternal)modelingObject);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000033FE File Offset: 0x000015FE
		internal static void WriteDsvItemReference(ref IntermediateFormatWriter writer, DsvItem dsvItem)
		{
			PersistenceHelper.WriteReferencableItemReference(ref writer, dsvItem);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003407 File Offset: 0x00001607
		internal static void WriteModelingObjectReferences<T>(ref IntermediateFormatWriter writer, IList<T> references) where T : ModelingObject, IPersistable
		{
			PersistenceHelper.WriteReferancableItemReferences<T>(ref writer, references);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003410 File Offset: 0x00001610
		internal static void WriteDsvItemReferences<T>(ref IntermediateFormatWriter writer, IList<T> references) where T : DsvItem
		{
			PersistenceHelper.WriteReferancableItemReferences<T>(ref writer, references);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x0000341C File Offset: 0x0000161C
		internal static IDictionary ReadPropertyCollection(ref IntermediateFormatReader reader, Predicate<string> allowKey)
		{
			NameTable nameTable = reader.PersistenceHelper.NameTable;
			PersistenceHelper.ArrayDictionary arrayDictionary = reader.ReadStringObjectHashtable<PersistenceHelper.ArrayDictionary>((int dictionarySize) => new PersistenceHelper.ArrayDictionary(dictionarySize), allowKey, (string name) => nameTable.Add(name), (object value) => nameTable.Add((string)value));
			arrayDictionary.FinishInitialization();
			return arrayDictionary;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003484 File Offset: 0x00001684
		internal static void WriteProperyCollection(ref IntermediateFormatWriter writer, IDictionary propertyCollection, Action<IDictionary> cleanProperties)
		{
			Hashtable hashtable = null;
			if (propertyCollection.Count > 0)
			{
				hashtable = new Hashtable();
				foreach (object obj in propertyCollection)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					hashtable.Add(dictionaryEntry.Key.ToString(), dictionaryEntry.Value.ToString());
				}
			}
			cleanProperties(hashtable);
			writer.WriteStringObjectHashtable(hashtable);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003510 File Offset: 0x00001710
		private int GetOrCreatePersistenceID(object key)
		{
			int num;
			if (!this.m_persistenceIDs.TryGetValue(key, out num))
			{
				num = this.m_persistenceIDs.Count + 1;
				this.m_persistenceIDs.Add(key, num);
			}
			return num;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000354C File Offset: 0x0000174C
		private static T ReadReferencableItemReference<T>(ref IntermediateFormatReader reader, IPersistable refOwner) where T : class, IPersistable
		{
			PersistenceHelper.RefHelper refHelper = reader.ReadReference<PersistenceHelper.RefHelper>(refOwner);
			if (refHelper != null)
			{
				return PersistenceHelper.ResolveReferencableItemReference<T>(refHelper);
			}
			return default(T);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003574 File Offset: 0x00001774
		private static T ResolveReferencableItemReference<T>(IReferenceable referencable) where T : IPersistable
		{
			return (T)((object)((PersistenceHelper.RefHelper)referencable).Object);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003588 File Offset: 0x00001788
		private static void WriteReferencableItemReference(ref IntermediateFormatWriter writer, IPersistable item)
		{
			PersistenceHelper.RefHelper refHelper = null;
			if (item != null)
			{
				refHelper = PersistenceHelper.RefHelper.CreateForSerialization(writer.PersistenceHelper.GetOrCreatePersistenceID(item), item);
			}
			writer.WriteReference(refHelper);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000035B4 File Offset: 0x000017B4
		private static void WriteReferancableItemReferences<T>(ref IntermediateFormatWriter writer, IList<T> references) where T : IPersistable
		{
			PersistenceHelper.RefHelper[] array = null;
			if (references != null)
			{
				array = new PersistenceHelper.RefHelper[references.Count];
				for (int i = 0; i < references.Count; i++)
				{
					IPersistable persistable = references[i];
					array[i] = PersistenceHelper.RefHelper.CreateForSerialization(writer.PersistenceHelper.GetOrCreatePersistenceID(persistable), persistable);
				}
			}
			writer.WriteListOfReferences(array);
		}

		// Token: 0x040000D4 RID: 212
		private readonly Dictionary<object, int> m_persistenceIDs = new Dictionary<object, int>();

		// Token: 0x040000D5 RID: 213
		private readonly NameTable m_nameTable = new NameTable();

		// Token: 0x0200010D RID: 269
		private sealed class RefHelper : IPersistable, IReferenceable
		{
			// Token: 0x06000D56 RID: 3414 RVA: 0x0002C91F File Offset: 0x0002AB1F
			internal static PersistenceHelper.RefHelper CreateForSerialization(int id, IPersistable obj)
			{
				return new PersistenceHelper.RefHelper(id, obj);
			}

			// Token: 0x06000D57 RID: 3415 RVA: 0x0002C928 File Offset: 0x0002AB28
			internal static PersistenceHelper.RefHelper CreateForDeserialization(IPersistable obj)
			{
				return new PersistenceHelper.RefHelper(-1, obj);
			}

			// Token: 0x06000D58 RID: 3416 RVA: 0x0002C931 File Offset: 0x0002AB31
			private RefHelper(int id, IPersistable obj)
			{
				if (obj == null)
				{
					throw new ArgumentNullException("obj");
				}
				this.m_id = id;
				this.m_object = obj;
			}

			// Token: 0x1700030D RID: 781
			// (get) Token: 0x06000D59 RID: 3417 RVA: 0x0002C955 File Offset: 0x0002AB55
			internal IPersistable Object
			{
				get
				{
					return this.m_object;
				}
			}

			// Token: 0x06000D5A RID: 3418 RVA: 0x0002C960 File Offset: 0x0002AB60
			void IPersistable.Serialize(IntermediateFormatWriter writer)
			{
				writer.RegisterDeclaration(PersistenceHelper.RefHelper.Declaration);
				while (writer.NextMember())
				{
					if (writer.CurrentMember.MemberName != MemberName.ID)
					{
						throw new InternalModelingException("Unexpected member: " + writer.CurrentMember.MemberName.ToString());
					}
					writer.Write(this.m_id);
				}
				this.m_object.Serialize(writer);
			}

			// Token: 0x06000D5B RID: 3419 RVA: 0x0002C9D8 File Offset: 0x0002ABD8
			void IPersistable.Deserialize(IntermediateFormatReader reader)
			{
				reader.RegisterDeclaration(PersistenceHelper.RefHelper.Declaration);
				while (reader.NextMember())
				{
					if (reader.CurrentMember.MemberName != MemberName.ID)
					{
						throw new InternalModelingException("Unexpected member: " + reader.CurrentMember.MemberName.ToString());
					}
					this.m_id = reader.ReadInt32();
				}
				this.m_object.Deserialize(reader);
			}

			// Token: 0x06000D5C RID: 3420 RVA: 0x0002CA50 File Offset: 0x0002AC50
			void IPersistable.ResolveReferences(Dictionary<ObjectType, List<MemberReference>> memberReferencesCollection, Dictionary<int, IReferenceable> referenceableItems)
			{
				throw new InternalModelingException("ResolveReferences is not supported.");
			}

			// Token: 0x06000D5D RID: 3421 RVA: 0x0002CA5C File Offset: 0x0002AC5C
			ObjectType IPersistable.GetObjectType()
			{
				return this.m_object.GetObjectType();
			}

			// Token: 0x1700030E RID: 782
			// (get) Token: 0x06000D5E RID: 3422 RVA: 0x0002CA69 File Offset: 0x0002AC69
			int IReferenceable.ID
			{
				get
				{
					return this.m_id;
				}
			}

			// Token: 0x06000D5F RID: 3423 RVA: 0x0002CA71 File Offset: 0x0002AC71
			ObjectType IReferenceable.GetObjectType()
			{
				return this.m_object.GetObjectType();
			}

			// Token: 0x1700030F RID: 783
			// (get) Token: 0x06000D60 RID: 3424 RVA: 0x0002CA7E File Offset: 0x0002AC7E
			private static Declaration Declaration
			{
				get
				{
					return ThreadingUtil.ReturnOnDemandValue<Declaration>(ref PersistenceHelper.RefHelper.__declaration, PersistenceHelper.RefHelper.__declarationLock, () => new Declaration(ObjectType.RefHelper, ObjectType.None, new List<MemberInfo>
					{
						new MemberInfo(MemberName.ID, Token.Int32)
					}));
				}
			}

			// Token: 0x04000598 RID: 1432
			private int m_id;

			// Token: 0x04000599 RID: 1433
			private IPersistable m_object;

			// Token: 0x0400059A RID: 1434
			private static Declaration __declaration;

			// Token: 0x0400059B RID: 1435
			private static readonly object __declarationLock = new object();
		}

		// Token: 0x0200010E RID: 270
		private sealed class ArrayDictionary : IDictionary, ICollection, IEnumerable
		{
			// Token: 0x06000D62 RID: 3426 RVA: 0x0002CABA File Offset: 0x0002ACBA
			internal ArrayDictionary(int dictionaryLength)
			{
				this.m_array = new KeyValuePair<string, object>[dictionaryLength];
			}

			// Token: 0x06000D63 RID: 3427 RVA: 0x0002CAD0 File Offset: 0x0002ACD0
			internal void FinishInitialization()
			{
				int num = this.m_array.Length - 1;
				while (num >= 0 && this.m_array[num].Key == null)
				{
					num--;
				}
				num++;
				if (num < this.m_array.Length)
				{
					KeyValuePair<string, object>[] array = new KeyValuePair<string, object>[num];
					for (int i = 0; i < num; i++)
					{
						array[i] = this.m_array[i];
					}
					this.m_array = array;
				}
			}

			// Token: 0x06000D64 RID: 3428 RVA: 0x0002CB44 File Offset: 0x0002AD44
			void IDictionary.Add(object key, object value)
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				string text = (string)key;
				for (int i = 0; i < this.m_array.Length; i++)
				{
					if (this.m_array[i].Key == null)
					{
						this.m_array[i] = new KeyValuePair<string, object>(text, value);
						return;
					}
				}
				throw new IndexOutOfRangeException();
			}

			// Token: 0x06000D65 RID: 3429 RVA: 0x0002CBA5 File Offset: 0x0002ADA5
			void IDictionary.Clear()
			{
				throw new InternalModelingException("Clear is not supported.");
			}

			// Token: 0x06000D66 RID: 3430 RVA: 0x0002CBB4 File Offset: 0x0002ADB4
			bool IDictionary.Contains(object key)
			{
				object obj;
				return this.Find((string)key, out obj) >= 0;
			}

			// Token: 0x06000D67 RID: 3431 RVA: 0x0002CBD5 File Offset: 0x0002ADD5
			IDictionaryEnumerator IDictionary.GetEnumerator()
			{
				throw new InternalModelingException("GetEnumerator is not supported.");
			}

			// Token: 0x17000310 RID: 784
			// (get) Token: 0x06000D68 RID: 3432 RVA: 0x0002CBE1 File Offset: 0x0002ADE1
			bool IDictionary.IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000311 RID: 785
			// (get) Token: 0x06000D69 RID: 3433 RVA: 0x0002CBE4 File Offset: 0x0002ADE4
			bool IDictionary.IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000312 RID: 786
			// (get) Token: 0x06000D6A RID: 3434 RVA: 0x0002CBE7 File Offset: 0x0002ADE7
			ICollection IDictionary.Keys
			{
				get
				{
					throw new InternalModelingException("Keys is not supported.");
				}
			}

			// Token: 0x06000D6B RID: 3435 RVA: 0x0002CBF3 File Offset: 0x0002ADF3
			void IDictionary.Remove(object key)
			{
				throw new InternalModelingException("Remove is not supported.");
			}

			// Token: 0x17000313 RID: 787
			// (get) Token: 0x06000D6C RID: 3436 RVA: 0x0002CBFF File Offset: 0x0002ADFF
			ICollection IDictionary.Values
			{
				get
				{
					throw new InternalModelingException("Values is not supported.");
				}
			}

			// Token: 0x17000314 RID: 788
			object IDictionary.this[object key]
			{
				get
				{
					object obj;
					if (this.Find((string)key, out obj) >= 0)
					{
						return obj;
					}
					return null;
				}
				set
				{
					object obj;
					int num = this.Find((string)key, out obj);
					if (num >= 0)
					{
						this.m_array[num] = new KeyValuePair<string, object>((string)key, value);
						return;
					}
					throw new InternalModelingException("this[string].Set does not support adding new keys.");
				}
			}

			// Token: 0x06000D6F RID: 3439 RVA: 0x0002CC73 File Offset: 0x0002AE73
			void ICollection.CopyTo(Array array, int index)
			{
				throw new InternalModelingException("CopyTo is not supported.");
			}

			// Token: 0x17000315 RID: 789
			// (get) Token: 0x06000D70 RID: 3440 RVA: 0x0002CC7F File Offset: 0x0002AE7F
			int ICollection.Count
			{
				get
				{
					return this.m_array.Length;
				}
			}

			// Token: 0x17000316 RID: 790
			// (get) Token: 0x06000D71 RID: 3441 RVA: 0x0002CC89 File Offset: 0x0002AE89
			bool ICollection.IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000317 RID: 791
			// (get) Token: 0x06000D72 RID: 3442 RVA: 0x0002CC8C File Offset: 0x0002AE8C
			object ICollection.SyncRoot
			{
				get
				{
					return this.m_array;
				}
			}

			// Token: 0x06000D73 RID: 3443 RVA: 0x0002CC94 File Offset: 0x0002AE94
			IEnumerator IEnumerable.GetEnumerator()
			{
				foreach (KeyValuePair<string, object> keyValuePair in this.m_array)
				{
					yield return new DictionaryEntry(keyValuePair.Key, keyValuePair.Value);
				}
				KeyValuePair<string, object>[] array = null;
				yield break;
			}

			// Token: 0x06000D74 RID: 3444 RVA: 0x0002CCA4 File Offset: 0x0002AEA4
			private int Find(string key, out object value)
			{
				for (int i = 0; i < this.m_array.Length; i++)
				{
					if (string.CompareOrdinal(this.m_array[i].Key, key) == 0)
					{
						value = this.m_array[i].Value;
						return i;
					}
				}
				value = null;
				return -1;
			}

			// Token: 0x0400059C RID: 1436
			private KeyValuePair<string, object>[] m_array;
		}
	}
}
